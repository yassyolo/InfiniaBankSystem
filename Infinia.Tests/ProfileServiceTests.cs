using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Core.ViewModels;
using Infinia.Infrastructure.Data.DataModels;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Infinia.Infrastructure.Repository;
using Infinia.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Text;
using NuGet.Frameworks;

namespace Infinia.Tests
{
    [TestFixture]
    public class ProfileServiceTests
    {
        private IRepository repository;
        private IEncryptionService encryptionService;
        private ProfileService _profileService;
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

            _profileService = new ProfileService(repository, encryptionService);
        }

        [Test]
        public async Task CustomerWithAccountIBANExists_ShouldReturnTrue_WhenCustomerWithIBANExists()
        {
            string userId = "user22q";
            string iban = "wgvwcref";

            var account = new Account
            {
                Id = 226,
                CustomerId = userId,
                EncryptedIBAN = encryptionService.Encrypt(iban),
                Type = "Current",
                Name = "Current",
                Branch = "Main",
                Balance = 1000m,
                CreationDate = DateTime.Now,
                Status = "Active",
                MonthlyFee = 0m,
                LastMonthlyFeeDeduction = null,
                Transactions = new List<Transaction>(),
            };
            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();


            var result = await _profileService.CustomerWithAccountIBANExists(iban, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CustomerWithAddressExists_ShouldReturnTrue_WhenCustomerWithAddressExists()
        {
            var model = new LoanApplicationViewModel
            {
                City = "City",
                Country = "Country",
                PostalCode = "12345",
                Street = "Main St"
            };
            string userId = "user1DFHTGD";

            var customer = new Customer
            {
                Id = userId,
                Address = new Address
                {
                    City = model.City,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    Street = model.Street
                }
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            var result = await _profileService.CustomerWithAddressExists(model, userId);
            var result2 = await _profileService.CustomerWithAddressExists(model, "qerfcqer");

            Assert.IsTrue(result);
            Assert.IsFalse(result2);
        }

        [Test]
        public async Task CustomerWithIdentityCardExists_ShouldReturnTrue_WhenIdentityCardExists()
        {
            var model = new LoanApplicationViewModel
            {
                IdentityCardNumber = "12345",
                IdentityCardIssuer = "Issuer",
                IdentityCardSex = "M",
                IdentityCardNationality = "Nationality",
                SSN = "SSN123"
            };
            string userId = "user14rdbvgcf";

            var customer = new Customer
            {
                Id = userId,
                IdentityCard = new IdentityCard
                {
                    EncryptedCardNumber = encryptionService.Encrypt(model.IdentityCardNumber),

                    EncryptedIssuer = encryptionService.Encrypt(model.IdentityCardIssuer),


                    EncryptedSex = encryptionService.Encrypt(model.IdentityCardSex),

                    EncryptedNationality = encryptionService.Encrypt(model.IdentityCardNationality),

                    EncryptedSSN = encryptionService.Encrypt(model.SSN)
                }
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            var result = await _profileService.CustomerWithIdentityCardExists(model, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CustomerWithIdentityCardNumberExists_ShouldReturnTrue_WhenIdentityCardNumberExists()
        {
            string identityCardNumber = "12345dnbyt";
            string userId = "user1gufhngb";

            var customer = new Customer
            {
                Id = userId,
                IdentityCard = new IdentityCard
                {
                    EncryptedCardNumber = encryptionService.Encrypt(identityCardNumber),
                    EncryptedIssuer = encryptionService.Encrypt("20.02.2002"),
                    EncryptedNationality = encryptionService.Encrypt("bg"),
                    EncryptedSex = encryptionService.Encrypt("sergvwer"),
                    EncryptedSSN = encryptionService.Encrypt("dvgre")
                }
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();


            var result = await _profileService.CustomerWithIdentityCardNumberExists(identityCardNumber, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CustomerWithIdExistsAsync_ShouldReturnTrue_WhenCustomerExists()
        {
            string userId = "user1";


            var crs = new Customer { Id = userId };

            dbContext.Customers.Add(crs);
            await dbContext.SaveChangesAsync();
            var result = await _profileService.CustomerWithIdExistsAsync(userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetProfileDetailsAsync_ShouldReturnProfileDetails_WhenCustomerExists()
        {
            string userId = "user1";
            var customer = new Customer
            {
                Id = userId,
                Name = "John Doe",
                Email = "john.doe@example.com",
                UserName = "johndoe"
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            var result = await _profileService.GetProfileDetailsAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.Name);
            Assert.AreEqual("jo******@example.com", result.Email);
            Assert.AreEqual("johndoe", result.Username);
        }

        [Test]
        public async Task ReturnCustomerAsync_ShouldCreateAndReturnCustomer()
        {
            var model = new RegisterViewModel
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Username = "johndoe",
                City = "City",
                Country = "Country",
                PostalCode = "12345",
                Street = "Main St",
                IdentityCardNumber = "12345",
                IdentityCardIssuer = "Issuer",
                IdentityCardSex = "M",
                IdentityCardNationality = "Nationality",
                SSN = "SSN123"
            };

            var result = await _profileService.ReturnCustomerAsync(model);

            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.Name);
            Assert.AreEqual("john.doe@example.com", result.Email);
            Assert.AreEqual("johndoe", result.UserName);
        }


    }
}
