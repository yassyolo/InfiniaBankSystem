using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Core.ViewModels.Account;
using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text;



namespace Infinia.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
         private IRepository repository;
         private IAccountService accountService;
         private IEncryptionService encryptionService;
         private ApplicationDbContext dbContext;

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
             accountService = new AccountService(repository, encryptionService);
         }
        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted(); 
            dbContext.Dispose();
        }

        [Test]
        public async Task AccountWithIdExistsAsyncReturnsTrueWhenAccountExists()
        {
            var account = new Account
            {
                Id = 19,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();
            var result = await accountService.AccountWithIdExistsAsync(19);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task AccountWithIdExistsAsyncReturnsFalseWhenAccountExists()
        {
            var account = new Account
            {
                Id = 20,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();
            var result = await accountService.AccountWithIdExistsAsync(21);
            Assert.IsFalse(result);
        }
        [Test]
        public async Task ChangeAccountNameAsyncChangesName()
        {
            var account = new Account
            {
                Id = 21,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();
            var model = new ChangeAccountNameViewModel 
            {
                NewName = "New Name",
                CurrentName = account.Name
            };
            await accountService.ChangeAccountNameAsync(21, model);
            Assert.IsTrue(account.Name == "New Name");
        }
        [Test]
        public async Task CreateAccountAsyncCreatesCurerntAndSavingsAccount()
        {
            var model = new CreateAccountViewModel
            {
                Branch = "Main",
                Balance = 1000m,
                IdentityCardNumber = "1234567890123456",
            };
            await accountService.CreateAccountAsync(model, "user");
            var accounts = await dbContext.Accounts.ToListAsync();
            var currentAccount = accounts.FirstOrDefault(a => a.Type == "Current" && a.CustomerId == "user");

            Assert.AreEqual(2, accounts.Count); 
            Assert.IsTrue(accounts.Any(a => a.Type == "Current"));
            Assert.IsTrue(accounts.Any(a => a.Type == "Savings"));
            Assert.IsTrue(currentAccount.Balance == 1000m);
            Assert.IsTrue(currentAccount.Branch == "Main");
            Assert.IsTrue(currentAccount.Name == "CURRENT-1");
        }
        [Test]
        public async Task DeleteAccountAsyncDeactivatesCurrentAndSavingsAccount()
        {
            var iban = encryptionService.Encrypt("TESTIBAN");
            var currentAccount = new Account
            {
                Id = 22,
                CustomerId = "user",
                EncryptedIBAN = iban,
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(currentAccount);
            await dbContext.SaveChangesAsync();
            var savingsAccount = new Account
            {
                Id = 22,
                CustomerId = "user",
                EncryptedIBAN = iban,
                Type = "Savings",
                Name = "Savings",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            await accountService.DeleteAccountAsync(22);
            var currentAccountInDb = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == 22);
            var savingsAccountInDb = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == 22);
            Assert.IsTrue(currentAccountInDb.Status == "Inactive");
            Assert.IsTrue(savingsAccountInDb.Status == "Inactive");
        }
        [Test]
        public async Task GetAccountForDeleteAsyncReturnsDeleteAccountViewModel()
        {
            var account = new Account
            {
                Id = 23,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var result = await accountService.GetAccountForDeleteAsync(23);
            Assert.IsTrue(result.Id == 23);
            Assert.IsTrue(result.Name == "Current");
            Assert.IsTrue(result.Balance == 1000m);
            Assert.IsTrue(result.Type == "Current");
        }
        [Test]
        public async Task GetAccountNameAsyncReturnsChangeAccountNameViewModel()
        {
            var account = new Account
            {
                Id = 24,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var result = await accountService.GetAccountNameAsync(24);
            Assert.IsTrue(result.Id == 24);
            Assert.IsTrue(result.CurrentName == "Current");
        }
        [Test]
        public async Task GetAccountsForCustomerAsyncReturnsAccountIndexViewModel()
        {
            var account = new Account
            {
                Id = 25,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var result = await accountService.GetAccountsForCustomerAsync("user");
            Assert.IsTrue(result.Accounts.FirstOrDefault().Id == 25);
            Assert.IsTrue(result.Accounts.FirstOrDefault().Name == "Current");
            Assert.IsTrue(result.Accounts.FirstOrDefault().Balance == 1000m);
            Assert.IsTrue(result.Accounts.FirstOrDefault().Type == "Current");
            Assert.IsTrue(result.TotalBalance == 1000m);
        }
        [Test]
        public async Task GetSavingsAccountAsyncReturnsAccount()
        {
            var account = new Account
            {
                Id = 25,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Savings",
                Name = "Savings",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var result = await accountService.GetSavingsAccountAsync("TESTIBAN");
            Assert.IsTrue(result.Id == 25);
            Assert.IsTrue(result.Name == "Savings");
            Assert.IsTrue(result.Balance == 1000m);
        }
        [Test]
        public async Task GetAccountDetailsAsyncReturnsAccountDetailsViewModel()
        {
            var account = new Account
            {
                Id = 26,
                CustomerId = "user",
                EncryptedIBAN = encryptionService.Encrypt("TESTIBAN"),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>()
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            var result = await accountService.GetAccountDetailsAsync(26);
            Assert.IsTrue(result.Id == 26);
            Assert.IsTrue(result.Name == "Current");
            Assert.IsTrue(result.Balance == 1000m);
            Assert.IsTrue(result.Type == "Current");
        }
    }
}