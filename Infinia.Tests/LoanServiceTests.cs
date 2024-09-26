using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Core.ViewModels.Loan;
using Infinia.Infrastructure.Repository;
using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinia.Core.ViewModels;
using static Infinia.Core.Constants.LoanTypesConstants;
using static Infinia.Core.Constants.LoanInterestRateConstants;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Tests
{
    [TestFixture]
    public class LoanServiceTests
    {
        private ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensure unique database per test
                .Options;
            return new ApplicationDbContext(options);
        }

        private IEncryptionService CreateEncryptionService()
        {
            var inMemoryConfiguration = new Dictionary<string, string>
            {
                {"Encryption:Key", Convert.ToBase64String(Encoding.UTF8.GetBytes("1234567890123456"))}
            };
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemoryConfiguration).Build();
            return new EncryptionService(configuration);
        }

        [Test]
        public async Task GetCurrentLoansForCustomerAsync_ShouldReturnCurrentApprovedLoans()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new Repository(context);
            var encryptionService = CreateEncryptionService();
            var transactionService = new TransactionService(repository, encryptionService);
            var loanService = new LoanService(repository, encryptionService, transactionService);

            var customer = new Customer { Id = "user1", UserName = "ivanivanov", Name = "Ivan Ivanov" };
            var loanApplication = new LoanApplication
            {
                CustomerId = customer.Id,
                LoanAmount = 5000,
                LoanTermMonths = 24,
                InterestRate = 5.5m,
                Type = "Personal Loan",
                LoanRepaymentNumber = 12,
                Status = "Approved"
            };

            context.Customers.Add(customer);
            context.LoanApplications.Add(loanApplication);
            context.SaveChanges();

            // Act
            var loans = await loanService.GetCurrentLoansForCustomerAsync(customer.Id);

            // Assert
            Assert.NotNull(loans);
            Assert.AreEqual(1, loans.Count());
            Assert.AreEqual("5000.0", loans.First().LoanAmount);
        }

        [Test]
        public async Task GetLoanApplicationDetailsAsync_ShouldReturnLoanApplicationDetails()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new Repository(context);
            var encryptionService = CreateEncryptionService();
            var transactionService = new TransactionService(repository, encryptionService);
            var loanService = new LoanService(repository, encryptionService, transactionService);

            var customer = new Customer { Id = "user1", UserName = "ivanivanov", Name = "Ivan Ivanov" };

            // Adding related entities required for the query
            var education = new Education { EducationLevel = "Bachelor's" };
            var employerInfo = new EmployerInfo
            {
                IsRetired = false,
                EmployerName = "Tech Corp",
                Position = "Developer",
                YearsAtJob = 3,
                MonthsAtJob = 6,
                TotalWorkExperienceYears = 5,
                TotalWorkExperienceMonths = 2
            };
            var householdInfo = new Infinia.Infrastructure.Data.DataModels.HouselholdInfo
            {
                NumberOfHouseholdMembers = 4,
                MembersWithProvenIncome = 2,
                Dependents = 1
            };
            var incomeInfo = new IncomeInfo
            {
                NetMonthlyIncome = 3000,
                FixedMonthlyExpenses = 1000,
                PermanentContractIncome = 2500,
                TemporaryContractIncome = 500,
                CivilContractIncome = 0,
                BusinessIncome = 0,
                PensionIncome = 0,
                FreelanceIncome = 0,
                OtherIncome = 0,
                HasOtherCredits = false
            };
            var propertyStatus = new PropertyStatus
            {
                HasApartmentOrHouse = true,
                HasCommercialProperty = false,
                HasLand = false,
                HasMultipleProperties = false,
                HasPartialOwnership = false,
                NoProperty = false,
                VehicleCount = 1
            };
            var maritalStatus = new MaritalStatus { Status = "Single" };

            var loanApplication = new LoanApplication
            {
                Id = 1,
                CustomerId = customer.Id,
                LoanAmount = 5000,
                LoanTermMonths = 24,
                InterestRate = 5.5m,
                Type = "Personal Loan",
                LoanRepaymentNumber = 12,
                Status = "Pending",
                Education = education,
                EmployerInfo = employerInfo,
                HouseholdInfo = householdInfo,
                IncomeInfo = incomeInfo,
                PropertyStatus = propertyStatus,
                MaritalStatus = maritalStatus
            };

            context.Customers.Add(customer);
            context.LoanApplications.Add(loanApplication);
            await context.SaveChangesAsync();

            // Act
            var loanDetails = await loanService.GetLoanApplicationDetailsAsync(loanApplication.Id, customer.Id);

            // Assert
            Assert.NotNull(loanDetails);
            Assert.AreEqual("5000.0", loanDetails.LoanAmount);
            Assert.AreEqual(24, loanDetails.LoanTermMonths);
            Assert.AreEqual("Personal Loan", loanDetails.Type);
            Assert.AreEqual("Bachelor's", loanDetails.EducationLevel);
            Assert.AreEqual("Tech Corp", loanDetails.EmployerName);
            Assert.AreEqual(1, loanDetails.VehicleCount);
        }

        [Test]
        public async Task GetLoanApplicationHistoryForCustomerAsync_ShouldReturnLoanApplicationHistory()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new Repository(context);
            var encryptionService = CreateEncryptionService();
            var transactionService = new TransactionService(repository, encryptionService);
            var loanService = new LoanService(repository, encryptionService, transactionService);

            var customer = new Customer { Id = "user1", UserName = "ivanivanov", Name = "Ivan Ivanov" };
            var loanApplications = new List<LoanApplication>
            {
                new LoanApplication
                {
                    CustomerId = customer.Id,
                    LoanAmount = 5000,
                    LoanTermMonths = 24,
                    InterestRate = 5.5m,
                    Type = "Personal Loan",
                    LoanRepaymentNumber = 12,
                    Status = "Approved"
                },
                new LoanApplication
                {
                    CustomerId = customer.Id,
                    LoanAmount = 10000,
                    LoanTermMonths = 36,
                    InterestRate = 6.0m,
                    Type = "Car Loan",
                    LoanRepaymentNumber = 18,
                    Status = "Pending"
                }
            };

            context.Customers.Add(customer);
            context.LoanApplications.AddRange(loanApplications);
            context.SaveChanges();

            // Act
            var result = await loanService.GetLoanApplicationHistoryForCustomerAsync(customer.Id);

            // Assert
            Assert.NotNull(result);
            var loanHistory = result.ToList();
            Assert.AreEqual(2, loanHistory.Count);
            Assert.AreEqual("5000.0", loanHistory[0].LoanAmount);
            Assert.AreEqual("Car Loan", loanHistory[1].Type);
            Assert.AreEqual("Pending", loanHistory[1].Status);
        }

        [Test]
        public async Task LoanApplicationExistsAsync_ShouldReturnFalse_WhenLoanApplicationDoesNotExist()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new Repository(context);
            var encryptionService = CreateEncryptionService();
            var transactionService = new TransactionService(repository, encryptionService);
            var loanService = new LoanService(repository, encryptionService, transactionService);

            var nonExistingLoanApplicationId = 999;

            // Act
            var result = await loanService.LoanApplicationExistsAsync(nonExistingLoanApplicationId, "evcexw");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task LoanApplicationExistsAsync_ShouldReturnTrue_WhenLoanApplicationExists()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new Repository(context);
            var encryptionService = CreateEncryptionService();
            var transactionService = new TransactionService(repository, encryptionService);
            var loanService = new LoanService(repository, encryptionService, transactionService);

            var customer = new Customer { Id = "user1", UserName = "ivanivanov", Name = "Ivan Ivanov" };
            var loanApplication = new LoanApplication
            {
                CustomerId = customer.Id,
                LoanAmount = 5000,
                LoanTermMonths = 24,
                InterestRate = 5.5m,
                Type = "Personal Loan",
                LoanRepaymentNumber = 12
            };

            context.Customers.Add(customer);
            context.LoanApplications.Add(loanApplication);
            context.SaveChanges();

            // Act
            var result = await loanService.LoanApplicationExistsAsync(loanApplication.Id, customer.Id);

            // Assert
            Assert.IsTrue(result);
        }




    }
}
