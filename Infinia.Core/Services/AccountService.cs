using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Account;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using static Infinia.Core.Helpers.IbanHelper;
using static Infinia.Core.Constants.AccountStatusConstants;
using static Infinia.Core.Constants.AccountTypeConstants;

namespace Infinia.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repository;
        private readonly IEncryptionService encryptionService;

        public AccountService(IRepository repository, IEncryptionService encryptionService)
        {
            this.repository = repository;
            this.encryptionService = encryptionService;
        }

        public async Task<bool> AccountWithIdExistsAsync(int id)
        {
            return await repository.AllReadOnly<Account>().AnyAsync(x => x.Id == id);
        }

        public async Task ChangeAccountNameAsync(int id, ChangeAccountNameViewModel model)
        {
            var account = await repository.All<Account>().FirstOrDefaultAsync(x => x.Id == id);
            account.Name = model.NewName;
            var notification = new Notification
            {
                CustomerId = account.CustomerId,
                Content = $"Променихте името на сметката си на {model.NewName}.",
                CreationDate = DateTime.Now,
                IsRead = false,
                Title = "Промяна на име на сметка"
            };
            await repository.AddAsync(notification);
            await repository.SaveChangesAsync();
        }

        public async Task CreateAccountAsync(CreateAccountViewModel model, string userId)
        {
            var accountCountForUser =  await repository.AllReadOnly<Account>().CountAsync(x => x.CustomerId == userId && x.Type == Current);
            var creationDate = DateTime.Now;
            var encryptedIban = encryptionService.Encrypt(GenerateIban());
            var currentAccount = new Account
            {
                Balance = model.Balance,
                CustomerId = userId,
                Branch = model.Branch,
                EncryptedIBAN = encryptedIban,
                Type = Current,
                Name = $"РАЗПЛАЩАТЕЛНА-{accountCountForUser+1}",
                CreationDate = creationDate,
                Status = Open,
                MonthlyFee = 2m
            };
            await repository.AddAsync(currentAccount);
            await repository.SaveChangesAsync();
            var savingsAccount = new Account
            {
                Balance = 0,
                CustomerId = userId,
                Branch = model.Branch,
                EncryptedIBAN = encryptedIban,
                Type = Savings,
                Name = $"СПЕСТОВНА-{accountCountForUser+1}",
                CreationDate = creationDate,
                Status = Open,
                MonthlyFee = 0m
            };
            await repository.AddAsync(savingsAccount);
            await repository.SaveChangesAsync();
            var notification = new Notification
            {
                CustomerId = userId,
                Content = $"Открихте своята нова сметка: {currentAccount.Name}. Проверете раздел 'Средства'.",
                CreationDate = creationDate,
                IsRead = false,
                Title = "Нова сметка 🎉"
            };
            await repository.AddAsync(notification);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await repository.All<Account>().FirstOrDefaultAsync(x => x.Id == id);
            var savingsAccount = await repository.All<Account>().FirstOrDefaultAsync(x => x.EncryptedIBAN == account.EncryptedIBAN);
            account.Status = Closed;
            savingsAccount.Status = Closed;
            await repository.SaveChangesAsync();
        }

        public async Task<DeleteAccountViewModel?> GetAccountForDeleteAsync(int id)
        {
            return await repository.AllReadOnly<Account>()
                .Where(x => x.Id == id)
                .Select(x => new DeleteAccountViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Balance = x.Balance,
                    Type = x.Type,
                    IBAN = encryptionService.Decrypt(x.EncryptedIBAN)
                }).FirstOrDefaultAsync();
        }

        public async Task<ChangeAccountNameViewModel?> GetAccountNameAsync(int id)
        {
           return await repository.AllReadOnly<Account>()
                .Where(x => x.Id == id)
                .Select(x => new ChangeAccountNameViewModel
                {
                    Id = x.Id,
                    CurrentName = x.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<AccountIndexViewModel?> GetAccountsForCustomerAsync(string userId)
        {
            var accounts = await repository.AllReadOnly<Account>()
                .Where(x => x.CustomerId == userId)
                .Select(x => new AccountRowViewModel
                {
                    Id = x.Id,
                    IBAN = encryptionService.Decrypt(x.EncryptedIBAN),
                    Type = x.Type,
                    Name = x.Name,
                    Balance = x.Balance
                }).ToListAsync();
            return new AccountIndexViewModel
            {
                Accounts = accounts,
                TotalBalance = accounts.Sum(x => x.Balance)
            };  
        }
        //TODO: AVAILABLE ACCOUNT FOR TRANSACTION BETWEEN ACCPUNT CAN BE SAVING ALSO
        public async Task<Account> GetSavingsAccountAsync(string IBAN)
        {
            return await repository.AllReadOnly<Account>().FirstOrDefaultAsync(x => encryptionService.Decrypt(x.EncryptedIBAN) == IBAN);
        }

        public async Task<AccountDetailsViewModel?> GetAccountDetailsAsync(int id)
        {
            return await repository.AllReadOnly<Account>()
                .Where(x => x.Id == id)
                .Select(x => new AccountDetailsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IBAN = encryptionService.Decrypt(x.EncryptedIBAN),
                    Balance = x.Balance,
                    Branch = x.Branch,
                    Type = x.Type,
                    CreationDate = x.CreationDate,
                    Status = x.Status,
                    MonthlyFee = x.MonthlyFee
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> AmountGreaterThanSenderAccountBalance(int accountIdFromWhichWeWantToSendMoney, decimal amount)
        {
            return await repository.AllReadOnly<Account>().AnyAsync(x => x.Id == accountIdFromWhichWeWantToSendMoney && x.Balance < amount);
        }

        public async Task<bool> AccountBelongsToUserAsync(int id, string userId)
        {
            return await repository.AllReadOnly<Account>().AnyAsync(x => x.Id == id && x.CustomerId == userId);
        }
    }
}
