using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Account;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using static Infinia.Core.Helpers.IbanHelper;

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
            return await repository.AllReadOnly<Account>().AnyAsync(a => a.Id == id);
        }

        public async Task ChangeAccountNameAsync(int id, ChangeAccountNameViewModel model)
        {
            var account = await repository.All<Account>().FirstOrDefaultAsync(a => a.Id == id);
            account.Name = model.NewName;
            await repository.SaveChangesAsync();
        }

        public async Task CreateAccountAsync(CreateAccountViewModel model, string userId)
        {
            var accountCountForUser =  await repository.AllReadOnly<Account>().CountAsync(a => a.CustomerId == userId);
            var creationDate = DateTime.Now;
            var encryptedIban = encryptionService.Encrypt(GenerateIban());
            var currentAccount = new Account
            {
                Balance = model.Balance,
                CustomerId = userId,
                Branch = model.Branch,
                EncryptedIBAN = encryptedIban,
                Type = "Current",
                Name = $"CURRENT-{accountCountForUser+1}",
                CreationDate = creationDate,
                Status = "Active",
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
                Type = "Savings",
                Name = $"SAVINGS-{accountCountForUser+1}",
                CreationDate = creationDate,
                Status = "Active",
                MonthlyFee = 0m
            };
            await repository.AddAsync(savingsAccount);
            await repository.SaveChangesAsync();
            var notification = new Notification
            {
                CustomerId = userId,
                Content = $"Account {currentAccount.Name} was created successfully",
                CreationDate = creationDate,
                IsRead = false
            };
        }

        //TODO: Implement deletion of transaction related to account
        public async Task DeleteAccountAsync(int id)
        {
            var account = await repository.All<Account>().FirstOrDefaultAsync(x => x.Id == id);
            var savingsAccount = await repository.All<Account>().FirstOrDefaultAsync(x => x.EncryptedIBAN == account.EncryptedIBAN);
            await repository.DeleteAsync<Account>(account);
            await repository.DeleteAsync<Account>(savingsAccount);
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

        public async Task<Account> GetSavingsAccountAsync(string iBAN)
        {
            return await repository.AllReadOnly<Account>().FirstOrDefaultAsync(x => x.EncryptedIBAN == encryptionService.Encrypt(iBAN));
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
    }
}
