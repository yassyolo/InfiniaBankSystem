using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Transaction;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using static Infinia.Core.Constants.TransactionTypeConstants;
using static Infinia.Core.Constants.TransactionFeeConstants;
using static Infinia.Core.Constants.AccountTypeConstants;
using static Infinia.Core.MessageConstants.ErrorMessages;
using System.Text;


namespace Infinia.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository repository;
        private readonly IEncryptionService encryptionService;

        public TransactionService(IRepository repository, IEncryptionService encryptionService)
        {
            this.repository = repository;
            this.encryptionService = encryptionService;
        }

        public async Task<bool> CustomerWithNameAndIBANExistsAsync(string receiverIBAN, string receiverName)
        {
            var accounts = await repository.AllReadOnly<Account>().ToListAsync();
            var customer = await repository.AllReadOnly<Customer>().FirstOrDefaultAsync(x => x.Name == receiverName);
            return accounts.Any(x => x.CustomerId == customer.Id && encryptionService.Decrypt(x.EncryptedIBAN) == receiverIBAN);
        }

        public async Task<IEnumerable<AvailableAccountViewModels>?> GetAvailableAccountsAsync(string userId)
        {
            return await repository.AllReadOnly<Account>().Where(x => x.CustomerId == userId && x.Type == Current)
                .Select(x => new AvailableAccountViewModels()
                {
                    Type = x.Type,
                    Name = x.Name,
                    Balance = x.Balance.ToString(),
                    AccountId = x.Id,
                    IBAN = encryptionService.Decrypt(x.EncryptedIBAN)
                }).ToListAsync();
        }

        public async Task<TransactionHistoryViewModel?> GetTransactionsForAccountAsync(int id)
        {
            var currentAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == id);
            var transactions = await repository.AllReadOnly<Transaction>().Where(x => x.AccountId == id)
                .Select(x => new TransactionRowViewModel()
                {
                    ReceiverOrSenderIBAN = encryptionService.Decrypt(x.EncryptedReceiverIBAN),
                    Amount = x.Amount.ToString("F2"),
                    Description = x.Description,
                    Reason = x.Reason,
                    CreatedOn = x.TransactionDate.ToString("dd.MM.yyyy"),
                }).ToListAsync();
            var endBalance = 0m;
            foreach (var transaction in transactions)
            {
                if (transaction.ReceiverOrSenderIBAN == encryptionService.Decrypt(currentAccount.EncryptedIBAN))
                {
                    endBalance += decimal.Parse(transaction.Amount);
                }
                else
                {
                    endBalance -= decimal.Parse(transaction.Amount);
                }
                
            }
            return new TransactionHistoryViewModel()
            {
                Transactions = transactions,
                EndBalance = endBalance.ToString("F2")
            };
        }

        public async Task MakeTransactionBetweenMyAccountsAsync(TransactionBetweenMyAccountsViewModel model, string userId)
        {
            var receiverAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == model.AccountIdFromWhichWeWantToReceiveMoney && x.CustomerId == userId);
            if (receiverAccount == null)
            {
                throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
            var senderAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == model.AccountIdFromWhichWeWantToSendMoney && x.CustomerId == userId);
            if(senderAccount == null)
            {
                   throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
            var transactionBetweenMyAccounts = new Transaction()
            {
                Type = BetweenMyAccounts,
                ReceiverName = receiverAccount.Name,
                EncryptedReceiverIBAN = receiverAccount.EncryptedIBAN,
                Amount = model.Amount,
                Description = model.Description,
                Reason = model.Reason,
                AccountId = model.AccountIdFromWhichWeWantToSendMoney,
                TransactionDate = DateTime.UtcNow,
                Fee = FeeBetweenMyAccounts
            };
            receiverAccount.Balance += model.Amount;
            senderAccount.Balance -= model.Amount;
            await repository.AddAsync(transactionBetweenMyAccounts);
            await repository.SaveChangesAsync();
        }

        public async Task MakeTransactionToAnotherBankAsync(TransactionToAnotherBankViewModel model, string userId)
        {
            var senderAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == model.AccountId && x.CustomerId == userId);
            if (senderAccount == null)
            {
                throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
            var transactionToAnotherBank = new Transaction()
            {
                Type = ToAnotherBank,
                ReceiverName = model.ReceiverName,
                EncryptedReceiverIBAN = encryptionService.Encrypt(model.ReceiverIBAN),
                Amount = model.Amount,
                Description = model.Description,
                Reason = model.Reason,
                AccountId = model.AccountId,
                TransactionDate = DateTime.UtcNow,
                Fee = FeeToAnotherBank,
                Bic = model.BIC
            };
            senderAccount.Balance -= model.Amount;
            senderAccount.Balance -= FeeToAnotherBank;
            await repository.AddAsync(transactionToAnotherBank);
            await repository.SaveChangesAsync();
        }
        //TODO: Implement the transaction history with you as receiver
        public async Task MakeTransactionWithinTheBankAsync(TransactionWithinTheBankViewModel model, string userId)
        {
            var receivers = await repository.AllReadOnly<Customer>().Where(x => x.Name == model.ReceiverName).ToListAsync();
            Account? receiverAccount = null;
            foreach (var receiver in receivers)
            {
                var receiverAccounts = await repository.AllReadOnly<Account>().Where(x => x.CustomerId == receiver.Id && x.Type == Current).ToListAsync();
                receiverAccount = receiverAccounts.FirstOrDefault(account => encryptionService.Decrypt(account.EncryptedIBAN) == model.ReceiverIBAN);

                if (receiverAccount != null)
                {
                    break; 
                }
            }

            if (receiverAccount == null)
            {
                throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
           
            var senderAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == model.AccountId && x.CustomerId == userId);
            if (senderAccount == null)
            {
                throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
            var transactionWithinTheBank = new Transaction()
            {
                Type = WithinTheBank,
                ReceiverName = receiverAccount.Name,
                EncryptedReceiverIBAN = encryptionService.Encrypt(model.ReceiverIBAN),
                Amount = model.Amount,
                Description = model.Description,
                Reason = model.Reason,
                AccountId = model.AccountId,
                TransactionDate = DateTime.UtcNow,
                Fee = FeeWithinTheBank
            };
            receiverAccount.Balance += model.Amount;
            senderAccount.Balance -= model.Amount;
            senderAccount.Balance -= FeeWithinTheBank;
            await repository.AddAsync(transactionWithinTheBank);
            await repository.SaveChangesAsync();
        }
        public async Task<string> GenerateCSVFileForTransactionHistory(int id)
        {
            var csv = new StringBuilder();
            var currentAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == id);
            var transactions = await repository.AllReadOnly<Transaction>().Where(x => x.AccountId == id)
                .Select(x => new TransactionRowViewModel()
                {
                    ReceiverOrSenderIBAN = encryptionService.Decrypt(x.EncryptedReceiverIBAN),
                    Amount = x.Amount.ToString("F2"),
                    Description = x.Description,
                    Reason = x.Reason,
                    CreatedOn = x.TransactionDate.ToString("dd.MM.yyyy"),
                }).ToListAsync();
            csv.AppendLine("Date,Reason,Description,Receiver/Sender IBAN,Amount");
            foreach (var transaction in transactions)
            {                
                var newLine = $"{transaction.CreatedOn},{transaction.Reason},{transaction.Description},{transaction.ReceiverOrSenderIBAN},{transaction.Amount}";
                csv.AppendLine(newLine);

            }
            

            return csv.ToString();
        }
        public async Task MakeMonthlyFeeTransactionAsync(TransactionWithinTheBankViewModel model, string userId)
        {
            var receiverAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.EncryptedIBAN == encryptionService.Encrypt(model.ReceiverIBAN) && x.Customer.Name == model.ReceiverName);
            if (receiverAccount == null)
            {
                throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
            var senderAccount = repository.AllReadOnly<Account>().FirstOrDefault(x => x.Id == model.AccountId && x.CustomerId == userId);
            if (senderAccount == null)
            {
                throw new InvalidOperationException(InvalidAccountErrorMessage);
            }
            var transactionToAnotherBank = new Transaction()
            {
                Type = WithinTheBank,
                ReceiverName = receiverAccount.Name,
                EncryptedReceiverIBAN = encryptionService.Encrypt(model.ReceiverIBAN),
                Amount = model.Amount,
                Description = model.Description,
                Reason = model.Reason,
                AccountId = model.AccountId,
                TransactionDate = DateTime.UtcNow,
                Fee = MonthlyFeeDeductionFee
            };
            senderAccount.Balance -= model.Amount;
            receiverAccount.Balance += model.Amount;
            await repository.AddAsync(transactionToAnotherBank);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AvailableAccountViewModels>> GetAvailableCurrentAccountsForUserAsync(string userId)
        {
            return await repository.AllReadOnly<Account>().Where(x => x.CustomerId == userId && x.Type == Current)
                .Select(x => new AvailableAccountViewModels()
                {
                    Type = x.Type,
                    Name = x.Name,
                    Balance = x.Balance.ToString(),
                    AccountId = x.Id,
                    IBAN = encryptionService.Decrypt(x.EncryptedIBAN)
                }).ToListAsync();
        }
    }
}
