using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Loan;
using Infinia.Infrastructure.Repository;
using static Infinia.Core.Constants.LoanTypesConstants;
using static Infinia.Core.Constants.LoanInterestRateConstants;
using static Infinia.Core.Constants.LoanStatusConstants;
using Microsoft.EntityFrameworkCore;
using Infinia.Core.ViewModels;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository repository;
        private readonly IEncryptionService encryptionService;

        public LoanService(IRepository repository, IEncryptionService encryptionService)
        {
            this.repository = repository;
            this.encryptionService = encryptionService;
        }

        public async Task ApplyForLoanAsync(LoanApplicationViewModel model, string userId)
        {
            /*var education = new Education
            {
                EducationLevel = model.ApplicationEducationLevel
            };
            await repository.AddAsync(education);
            await repository.SaveChangesAsync();
            var maritalStatus = new MaritalStatus
            {
                Status = model.MaritalStatusApplication
            };
            await repository.AddAsync(maritalStatus);
            await repository.SaveChangesAsync();
            var employerInfo = new EmployerInfo
            {
                EmployerName = model.EmployerName,
                Position = model.Position,
                IsRetired = model.IsRetired,
                YearsAtJob = model.YearsAtJob,
                MonthsAtJob = model.MonthsAtJob,
                TotalWorkExperienceYears = model.TotalWorkExperienceYearsApplication,
                TotalWorkExperienceMonths = model.TotalWorkExperienceMonthsApplication
            };
            await repository.AddAsync(employerInfo);
            await repository.SaveChangesAsync();
            var householdInfo = new HouselholdInfo
            {
                NumberOfHouseholdMembers = model.NumberOfHouseholdMembersApplication,
                Dependents = model.DependentsApplication,
                MembersWithProvenIncome = model.MembersWithProvenIncomeApplication
            };
            await repository.AddAsync(householdInfo);
            await repository.SaveChangesAsync();
            var incomeInfo = new IncomeInfo
            {
                NetMonthlyIncome = model.NetMonthlyIncomeApplication,
                FixedMonthlyExpenses = model.FixedMonthlyExpensesApplication,
                PermanentContractIncome = model.PermanentContractIncomeApplication,
                TemporaryContractIncome = model.TemporaryContractIncomeApplication,
                CivilContractIncome = model.CivilContractIncomeApplication,
                BusinessIncome = model.BusinessIncome,
                PensionIncome = model.PensionIncome,
                FreelanceIncome = model.FreelanceIncomeApplication,
                OtherIncome = model.OtherIncomeApplication,
                HasOtherCredits = model.HasOtherCreditsApplication
            };
            await repository.AddAsync(incomeInfo);
            await repository.SaveChangesAsync();*/
            /*var propertyStatus = new PropertyStatus
            {
                HasApartmentOrHouse = model.HasApartmentOrHouse,
                HasCommercialProperty = model.HasCommercialProperty,
                HasLand = model.HasLand,
                HasMultipleProperties = model.HasMultipleProperties,
                HasPartialOwnership = model.HasPartialOwnership,
                NoProperty = model.NoProperty,
                VehicleCount = model.VehicleCount
            };*/
            //await repository.AddAsync(propertyStatus);
            await repository.SaveChangesAsync();
            var account = await repository.All<Account>().FirstOrDefaultAsync(x => x.CustomerId == userId && x.EncryptedIBAN == encryptionService.Encrypt(model.AccountIBAN));
            /*var loanApplication = new LoanApplication
            {
                ApplicationDate = DateTime.UtcNow,
                EducationId = education.Id,
                MaritalStatusId = maritalStatus.Id,
                EmployerInfoId = employerInfo.Id,
                HouseholdInfoId = householdInfo.Id,
                IncomeInfoId = incomeInfo.Id,
                PropertyStatusId = propertyStatus.Id,
                CustomerId = userId,
                AccountId = account.Id,
                LoanAmount = model.LoanAmount,
                LoanTermMonths = model.LoanTermMonths,
                InterestRate = model.InterestRate,
                Type = model.Type,
                LoanRepaymentNumber = model.LoanRepaymentNumber,
                Status = Pending
            };
            await repository.AddAsync(loanApplication);
            await repository.SaveChangesAsync();
            var loanRepayment = new LoanRepayment
            {
                LoanApplicationId = loanApplication.Id,
                RepaymentAmount = loanApplication.LoanAmount,
                Status = Infinia.Core.Constants.LoanRepaymentStatus.Pending
            };
            await repository.AddAsync(loanRepayment);
            await repository.SaveChangesAsync();*/
        }


        public ChooseLoanTypeViewModel? GetLoanTypesAsync()
        {
            var model = new ChooseLoanTypeViewModel();

            model.LoanTypes.Add(PersonalLoan, PersonalLoanInterestRate);
            model.LoanTypes.Add(MortgageLoan, MorgageLoanInterestRate);
            model.LoanTypes.Add(CarLoan, CarLoanInterestRate);
            model.LoanTypes.Add(EducationLoan, EducationLoanInterestRate);
            model.LoanTypes.Add(BusinessLoan, BusinessLoanInterestRate);
            return model;
        }
    }
}
