using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Infrastructure.Repository;
using Infinia.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Core.ViewModels.Transaction;

namespace Infinia.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private ApplicationDbContext dbContext;
        private Repository repository;
        private EncryptionService encryptionService;
        private TransactionService transactionService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new ApplicationDbContext(options);
            repository = new Repository(dbContext);

            var inMemoryConfiguration = new Dictionary<string, string>
            {
                {"Encryption:Key", Convert.ToBase64String(Encoding.UTF8.GetBytes("1234567890123456"))}
            };
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemoryConfiguration).Build();
            encryptionService = new EncryptionService(configuration);
            transactionService = new TransactionService(repository, encryptionService);
        }

        [Test]
        public async Task CustomerWithNameAndIBANExistsAsync_ReturnsTrue_WhenCustomerExistsWithMatchingIBAN()
        {
            var receiverName = "John Doe";
            var receiverIBAN = "TESTIBAN1234";

            var customer = new Customer
            {
                Id = "customer1",
                Name = receiverName
            };

            var account = new Account
            {
                Id = 1,
                CustomerId = customer.Id,
                EncryptedIBAN = encryptionService.Encrypt(receiverIBAN),
                Type = "Current",
                Name = "Current Account",
                Branch = "Main",
                Balance = 500m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m
            };


            dbContext.Customers.Add(customer);
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var result = await transactionService.CustomerWithNameAndIBANExistsAsync(receiverIBAN, receiverName);

            Assert.IsTrue(result, "The customer should exist with the matching IBAN.");
        }
        [Test]
        public async Task GetAvailableAccountsAsync_ReturnsAvailableAccounts_WhenUserHasCurrentAccounts()
        {
            var userId = "user1";
            var currentAccount1 = new Account
            {
                Id = 1,
                CustomerId = userId,
                Type = "Current",
                Name = "Current Account 1",
                Balance = 1000m,
                EncryptedIBAN = encryptionService.Encrypt("IBAN1234")
            };

            var currentAccount2 = new Account
            {
                Id = 2,
                CustomerId = userId,
                Type = "Current",
                Name = "Current Account 2",
                Balance = 2000m,
                EncryptedIBAN = encryptionService.Encrypt("IBAN5678")
            };

            dbContext.Accounts.Add(currentAccount1);
            dbContext.Accounts.Add(currentAccount2);
            await dbContext.SaveChangesAsync();

            var result = await transactionService.GetAvailableAccountsAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Current Account 1", result.ElementAt(0).Name);
            Assert.AreEqual("1000", result.ElementAt(0).Balance);
            Assert.AreEqual("IBAN1234", result.ElementAt(0).IBAN);
            Assert.AreEqual("Current Account 2", result.ElementAt(1).Name);
            Assert.AreEqual("2000", result.ElementAt(1).Balance);
            Assert.AreEqual("IBAN5678", result.ElementAt(1).IBAN);
        }

        [Test]
        public async Task GetTransactionsForAccountAsync_ShouldReturnTransactionHistory_WhenTransactionsExist()
        {
            var userId = "User123";
            var iban = "IBAN12345";

            var account = new Account
            {
                Id = 1,
                Name = "Test Account",
                CustomerId = userId,
                Balance = 1000m,
                EncryptedIBAN = encryptionService.Encrypt(iban)
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var transaction1 = new Transaction
            {
                AccountId = account.Id,
                Amount = 100m,
                Description = "Deposit",
                Reason = "Initial Deposit",
                EncryptedReceiverIBAN = encryptionService.Encrypt(iban),
                TransactionDate = DateTime.UtcNow.AddDays(-1)
            };

            var transaction2 = new Transaction
            {
                AccountId = account.Id,
                Amount = 50m,
                Description = "Withdrawal",
                Reason = "ATM Withdrawal",
                EncryptedReceiverIBAN = encryptionService.Encrypt(iban),
                TransactionDate = DateTime.UtcNow
            };

            dbContext.Transactions.Add(transaction1);
            dbContext.Transactions.Add(transaction2);
            await dbContext.SaveChangesAsync();

            var result = await transactionService.GetTransactionsForAccountAsync(account.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Transactions.Count());
            Assert.AreEqual("150.00", result.EndBalance);
        }

        [Test]
        public async Task MakeTransactionBetweenMyAccountsAsync_ShouldUpdateBalancesCorrectly()
        {
            var senderAccount = new Account
            {
                Id = 1,
                CustomerId = "user123",
                Name = "Sender Account",
                Balance = 100m,
                EncryptedIBAN = encryptionService.Encrypt("IBAN12345")
            };

            var receiverAccount = new Account
            {
                Id = 2,
                CustomerId = "user123",
                Name = "Receiver Account",
                Balance = 50m,
                EncryptedIBAN = encryptionService.Encrypt("IBAN67890")
            };

            dbContext.Accounts.Add(senderAccount);
            dbContext.Accounts.Add(receiverAccount);
            await dbContext.SaveChangesAsync();

            var model = new TransactionBetweenMyAccountsViewModel
            {
                AccountIdFromWhichWeWantToSendMoney = senderAccount.Id,
                AccountIdFromWhichWeWantToReceiveMoney = receiverAccount.Id,
                Amount = 30m,
                Description = "Test transaction",
                Reason = "Transfer"
            };

            await transactionService.MakeTransactionBetweenMyAccountsAsync(model, senderAccount.CustomerId);

            var updatedSenderAccount = await dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == senderAccount.Id);
            var updatedReceiverAccount = await dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == receiverAccount.Id);

            Assert.AreEqual(70m, updatedSenderAccount.Balance);
            Assert.AreEqual(80m, updatedReceiverAccount.Balance);

            var transactions = await dbContext.Transactions.ToListAsync();
            Assert.AreEqual(1, transactions.Count);
            Assert.AreEqual(30m, transactions[0].Amount);
            Assert.AreEqual(senderAccount.Id, transactions[0].AccountId);
        }

        [Test]
        public async Task MakeTransactionToAnotherBankAsync_UpdatesBalanceAndAddsTransaction_WhenAccountIsValid()
        {
            var userId = "test_user";
            var accountId = 1;
            var initialBalance = 1000m;
            var transactionAmount = 100m;
            var transactionFee = 1.80m;

            var senderAccount = new Account
            {
                Id = accountId,
                CustomerId = userId,
                Balance = initialBalance,
                EncryptedIBAN = encryptionService.Encrypt("IBAN1234")
            };

            var model = new TransactionToAnotherBankViewModel
            {
                AccountId = accountId,
                ReceiverName = "John Doe",
                ReceiverIBAN = "IBAN5678",
                Amount = transactionAmount,
                Description = "Payment for services",
                Reason = "Service Payment",
                BIC = "BIC123"
            };

            dbContext.Accounts.Add(senderAccount);
            await dbContext.SaveChangesAsync();

            await transactionService.MakeTransactionToAnotherBankAsync(model, userId);

            var updatedAccount = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            Assert.IsNotNull(updatedAccount);
            Assert.AreEqual(initialBalance - transactionAmount - transactionFee, updatedAccount.Balance);

            var transaction = await dbContext.Transactions
                .FirstOrDefaultAsync(x => x.AccountId == accountId && x.ReceiverName == model.ReceiverName);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transactionAmount, transaction.Amount);
            Assert.AreEqual(model.Description, transaction.Description);
            Assert.AreEqual(model.Reason, transaction.Reason);
            Assert.AreEqual(model.BIC, transaction.Bic);
        }

        [Test]
        public void MakeTransactionToAnotherBankAsync_ShouldThrowInvalidOperationException_WhenReceiverNotFound()
        {
            var senderAccount = new Account
            {
                Id = 1,
                CustomerId = "user123",
                Name = "Sender Account",
                Balance = 100m,
                EncryptedIBAN = encryptionService.Encrypt("SENDER_IBAN12345")
            };

            dbContext.Accounts.Add(senderAccount);
            dbContext.SaveChanges();

            var model = new TransactionToAnotherBankViewModel
            {
                AccountId = senderAccount.Id,
                ReceiverIBAN = "INVALID_IBAN",
                ReceiverName = "NonExistentReceiver",
                Amount = 30m,
                Description = "Payment",
                Reason = "Transfer"
            };

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                transactionService.MakeTransactionToAnotherBankAsync(model, "wgfcerw"));

            Assert.AreEqual("Невалидна сметка.", ex.Message);
        }

        [Test]
        public void MakeTransactionToAnotherBankAsync_ShouldThrowInvalidOperationException_WhenSenderNotFound()
        {
            var receiverAccount = new Account
            {
                Id = 2,
                CustomerId = "user456",
                Name = "Receiver Account",
                Balance = 50m,
                EncryptedIBAN = encryptionService.Encrypt("RECEIVER_IBAN67890")
            };

            dbContext.Accounts.Add(receiverAccount);
            dbContext.SaveChanges();

            var model = new TransactionToAnotherBankViewModel
            {
                AccountId = 999,
                ReceiverIBAN = encryptionService.Decrypt(receiverAccount.EncryptedIBAN),
                ReceiverName = receiverAccount.Name,
                Amount = 30m,
                Description = "Payment",
                Reason = "Transfer"
            };


            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                transactionService.MakeTransactionToAnotherBankAsync(model, "user999")); // Invalid user ID

            Assert.AreEqual("Невалидна сметка.", ex.Message);
        }

        [Test]
        public async Task GenerateCSVFileForTransactionHistory_ReturnsCSV_WhenAccountHasTransactions()
        {
            int accountId = 1;
            var account = new Account
            {
                Id = accountId,
                EncryptedIBAN = encryptionService.Encrypt("IBAN1234")
            };

            var transaction1 = new Transaction
            {
                Id = 1,
                AccountId = accountId,
                EncryptedReceiverIBAN = encryptionService.Encrypt("IBAN5678"),
                Amount = 100.00m,
                Description = "Payment for services",
                Reason = "Service Payment",
                TransactionDate = new DateTime(2023, 9, 1)
            };

            var transaction2 = new Transaction
            {
                Id = 2,
                AccountId = accountId,
                EncryptedReceiverIBAN = encryptionService.Encrypt("IBAN8765"),
                Amount = 200.00m,
                Description = "Refund",
                Reason = "Refund Payment",
                TransactionDate = new DateTime(2023, 9, 2)
            };

            dbContext.Accounts.Add(account);
            dbContext.Transactions.Add(transaction1);
            dbContext.Transactions.Add(transaction2);
            await dbContext.SaveChangesAsync();

            var result = await transactionService.GenerateCSVFileForTransactionHistory(accountId);

            var expectedCsv = "Date,Reason,Description,Receiver/Sender IBAN,Amount\r\n" +
                              "01.09.2023,Service Payment,Payment for services,IBAN5678,100.00\r\n" +
                              "02.09.2023,Refund Payment,Refund,IBAN8765,200.00\r\n";

            Assert.AreEqual(expectedCsv, result);
        }


        [Test]
        public async Task MakeMonthlyFeeTransactionAsync_ProcessesTransactionAndUpdatesBalances_WhenAccountsAreValid()
        {
            var userId = "test_user";
            var senderAccountId = 1;
            var receiverAccountId = 2;
            var initialSenderBalance = 1000m;
            var initialReceiverBalance = 500m;
            var transactionAmount = 100m;
            var transactionFee = 2.5m;

            var senderAccount = new Account
            {
                Id = senderAccountId,
                CustomerId = userId,
                Balance = initialSenderBalance,
                EncryptedIBAN = encryptionService.Encrypt("IBAN_SENDER")
            };

            var receiverAccount = new Account
            {
                Id = receiverAccountId,
                CustomerId = "receiver_user",
                Balance = initialReceiverBalance,
                EncryptedIBAN = encryptionService.Encrypt("IBAN_RECEIVER"),
                Name = "Receiver Name"
            };

            var model = new TransactionWithinTheBankViewModel
            {
                AccountId = senderAccountId,
                ReceiverName = "Receiver Name",
                ReceiverIBAN = "IBAN_RECEIVER",
                Amount = transactionAmount,
                Description = "Monthly fee transaction",
                Reason = "Monthly Fee Deduction"
            };

            dbContext.Accounts.Add(senderAccount);
            dbContext.Accounts.Add(receiverAccount);
            await dbContext.SaveChangesAsync();

            await transactionService.MakeMonthlyFeeDeductionTransactionAsync(model, userId);

            var updatedSenderAccount = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == senderAccountId);
            var updatedReceiverAccount = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == receiverAccountId);
            Assert.IsNotNull(updatedSenderAccount);
            Assert.IsNotNull(updatedReceiverAccount);
            
            Assert.AreEqual(initialReceiverBalance + transactionAmount, updatedReceiverAccount.Balance);

            var transaction = await dbContext.Transactions
                .FirstOrDefaultAsync(x => x.AccountId == senderAccountId && x.ReceiverName == receiverAccount.Name);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transactionAmount, transaction.Amount);
            Assert.AreEqual(model.Description, transaction.Description);
            Assert.AreEqual(model.Reason, transaction.Reason);
        }

        [Test]
        public async Task GetAvailableCurrentAccountsForUserAsync_ReturnsCurrentAccounts_WhenUserHasAccounts()
        {
            var userId = "user1";
            var currentAccount1 = new Account
            {
                Id = 1,
                CustomerId = userId,
                Type = "Current",
                Name = "Current Account 1",
                Balance = 1000m,
                EncryptedIBAN = encryptionService.Encrypt("IBAN1234")
            };

            var currentAccount2 = new Account
            {
                Id = 2,
                CustomerId = userId,
                Type = "Current",
                Name = "Current Account 2",
                Balance = 2000m,
                EncryptedIBAN = encryptionService.Encrypt("IBAN5678")
            };

            dbContext.Accounts.Add(currentAccount1);
            dbContext.Accounts.Add(currentAccount2);
            await dbContext.SaveChangesAsync();


            var result = await transactionService.GetAvailableCurrentAccountsForUserAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Current Account 1", result.ElementAt(0).Name);
            Assert.AreEqual("1000", result.ElementAt(0).Balance);
            Assert.AreEqual("IBAN1234", result.ElementAt(0).IBAN);
            Assert.AreEqual("Current Account 2", result.ElementAt(1).Name);
            Assert.AreEqual("2000", result.ElementAt(1).Balance);
            Assert.AreEqual("IBAN5678", result.ElementAt(1).IBAN);
        }

        [Test]
        public async Task MakeTransactionWithinTheBankAsync_ValidTransaction_UpdatesBalancesAndCreatesTransaction()
        {
            var userId = "user1";
            var receiverName = "Receiver Name";
            var receiverIBAN = "RECEIVER_IBAN";
            var model = new TransactionWithinTheBankViewModel
            {
                ReceiverName = receiverName,
                ReceiverIBAN = receiverIBAN,
                Amount = 100m,
                Description = "Payment for services",
                Reason = "Service Payment",
                AccountId = 1
            };

            var senderAccount = new Account
            {
                Id = 1,
                CustomerId = userId,
                Balance = 200m,
                EncryptedIBAN = encryptionService.Encrypt("SENDER_IBAN"),
                Type = "Current"
            };

            var receiverAccount = new Account
            {
                Id = 2,
                CustomerId = "receiverCustomerId",
                Balance = 50m,
                EncryptedIBAN = encryptionService.Encrypt(receiverIBAN),
                Type = "Current"
            };

            var customer = new Customer
            {
                Id = "receiverCustomerId",
                Name = receiverName
            };

            dbContext.Accounts.Add(senderAccount);
            dbContext.Accounts.Add(receiverAccount);
            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            await transactionService.MakeTransactionWithinTheBankAsync(model, userId);

            var updatedSenderAccount = await dbContext.Accounts.FindAsync(1);
            var updatedReceiverAccount = await dbContext.Accounts.FindAsync(2);
            var transactions = await dbContext.Transactions.ToListAsync();

            Assert.AreEqual(150m, updatedReceiverAccount.Balance);
            Assert.AreEqual(200m - 100m - 0.90m, updatedSenderAccount.Balance);
            Assert.AreEqual(1, transactions.Count);
            Assert.AreEqual(model.Amount, transactions[0].Amount);
            Assert.AreEqual(senderAccount.Id, transactions[0].AccountId);

        }
    }
}
