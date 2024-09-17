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
<<<<<<< HEAD
            var education = new Education
            {
                EducationLevel = model.EducationLevel
=======
            /*var education = new Education
            {
                EducationLevel = model.ApplicationEducationLevel
>>>>>>> origin/main
            };
            await repository.AddAsync(education);
            await repository.SaveChangesAsync();
            var maritalStatus = new MaritalStatus
            {
<<<<<<< HEAD
                Status = model.MaritalStatus
=======
                Status = model.MaritalStatusApplication
>>>>>>> origin/main
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
<<<<<<< HEAD
                TotalWorkExperienceYears = model.TotalWorkExperienceYears,
                TotalWorkExperienceMonths = model.TotalWorkExperienceMonths
=======
                TotalWorkExperienceYears = model.TotalWorkExperienceYearsApplication,
                TotalWorkExperienceMonths = model.TotalWorkExperienceMonthsApplication
>>>>>>> origin/main
            };
            await repository.AddAsync(employerInfo);
            await repository.SaveChangesAsync();
            var householdInfo = new HouselholdInfo
            {
<<<<<<< HEAD
                NumberOfHouseholdMembers = model.NumberOfHouseholdMembers,
                Dependents = model.Dependents,
                MembersWithProvenIncome = model.MembersWithProvenIncome
=======
                NumberOfHouseholdMembers = model.NumberOfHouseholdMembersApplication,
                Dependents = model.DependentsApplication,
                MembersWithProvenIncome = model.MembersWithProvenIncomeApplication
>>>>>>> origin/main
            };
            await repository.AddAsync(householdInfo);
            await repository.SaveChangesAsync();
            var incomeInfo = new IncomeInfo
            {
<<<<<<< HEAD
                NetMonthlyIncome = model.NetMonthlyIncome,
                FixedMonthlyExpenses = model.FixedMonthlyExpenses,
                PermanentContractIncome = model.PermanentContractIncome,
                TemporaryContractIncome = model.TemporaryContractIncome,
                CivilContractIncome = model.CivilContractIncome,
                BusinessIncome = model.BusinessIncome,
                PensionIncome = model.PensionIncome,
                FreelanceIncome = model.FreelanceIncome,
                OtherIncome = model.OtherIncome,
                HasOtherCredits = model.HasOtherCredits
            };
            await repository.AddAsync(incomeInfo);
            await repository.SaveChangesAsync();
            var propertyStatus = new PropertyStatus
=======
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
>>>>>>> origin/main
            {
                HasApartmentOrHouse = model.HasApartmentOrHouse,
                HasCommercialProperty = model.HasCommercialProperty,
                HasLand = model.HasLand,
                HasMultipleProperties = model.HasMultipleProperties,
                HasPartialOwnership = model.HasPartialOwnership,
                NoProperty = model.NoProperty,
                VehicleCount = model.VehicleCount
<<<<<<< HEAD
            };
            await repository.AddAsync(propertyStatus);
            await repository.SaveChangesAsync();
            var account = await repository.All<Account>().FirstOrDefaultAsync(x => x.CustomerId == userId && x.EncryptedIBAN == encryptionService.Encrypt(model.AccountIBAN));
            var loanApplication = new LoanApplication
=======
            };*/
            //await repository.AddAsync(propertyStatus);
            await repository.SaveChangesAsync();
            var account = await repository.All<Account>().FirstOrDefaultAsync(x => x.CustomerId == userId && x.EncryptedIBAN == encryptionService.Encrypt(model.AccountIBAN));
            /*var loanApplication = new LoanApplication
>>>>>>> origin/main
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
<<<<<<< HEAD
                RepaymentAmount = CalculateEMI(loanApplication.LoanAmount, loanApplication.InterestRate, loanApplication.LoanTermMonths),
                Status = Infinia.Core.Constants.LoanRepaymentStatus.Pending
            };
            await repository.AddAsync(loanRepayment);
            await repository.SaveChangesAsync();
        }
        private decimal CalculateEMI(decimal loanAmount, double annualInterestRate, int termMonths)
        {
            double monthlyInterestRate = annualInterestRate / 12 / 100;
            double emiDouble = (double)loanAmount * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, termMonths) /
                              (Math.Pow(1 + monthlyInterestRate, termMonths) - 1);

            return (decimal)emiDouble;
        }

        public async Task<IEnumerable<CurrentLoanViewModel>?> GetCurrentLoansForCustomerAsync(string userId)
        {
            var currentLoans = await repository.All<LoanApplication>()
                .Where(x => x.CustomerId == userId && x.Status == Approved)
                .Select(x => new CurrentLoanViewModel
                {
                    Id = x.Id,
                    LoanAmount = x.LoanAmount,
                    LoanTermMonths = x.LoanTermMonths,
                    InterestRate = x.InterestRate,
                    Type = x.Type,
                    LoanRepaymentNumber = x.LoanRepaymentNumber,
                })
                .ToListAsync();
            foreach (var loan in currentLoans)
            {
                loan.LoanRepaymentAmount = await repository.All<LoanRepayment>()
                    .Where(x => x.LoanApplicationId == loan.Id)
                    .Select(x => x.RepaymentAmount.ToString())
                    .FirstOrDefaultAsync();
            }
            return currentLoans;
        }

        public async Task<LoanApplicationHistoryViewModel?> GetLoanApplicationDetailsAsync(int id, string userId)
        {
            return await repository.All<LoanApplication>()
                .Where(x => x.Id == id && x.CustomerId == userId)
                .Select(x => new LoanApplicationHistoryViewModel
                {
                    Id = x.Id,
                    LoanAmount = x.LoanAmount,
                    LoanTermMonths = x.LoanTermMonths,
                    InterestRate = x.InterestRate,
                    Type = x.Type,
                    LoanRepaymentNumber = x.LoanRepaymentNumber,
                    Status = x.Status,
                    EducationLevel = x.Education.EducationLevel,
                    IsRetired = x.EmployerInfo.IsRetired,
                    EmployerName = x.EmployerInfo.EmployerName,
                    Position = x.EmployerInfo.Position,
                    YearsAtJob = x.EmployerInfo.YearsAtJob,
                    MonthsAtJob = x.EmployerInfo.MonthsAtJob,                        
                    TotalWorkExperienceYears = x.EmployerInfo.TotalWorkExperienceYears,
                    TotalWorkExperienceMonths = x.EmployerInfo.TotalWorkExperienceMonths,
                    NumberOfHouseholdMembers = x.HouseholdInfo.NumberOfHouseholdMembers,
                    MembersWithProvenIncome = x.HouseholdInfo.MembersWithProvenIncome,
                    Dependents = x.HouseholdInfo.Dependents,
                    NetMonthlyIncome = x.IncomeInfo.NetMonthlyIncome,
                    FixedMonthlyExpenses = x.IncomeInfo.FixedMonthlyExpenses,
                    PermanentContractIncome = x.IncomeInfo.PermanentContractIncome,
                    TemporaryContractIncome = x.IncomeInfo.TemporaryContractIncome,
                    CivilContractIncome = x.IncomeInfo.CivilContractIncome,
                    BusinessIncome = x.IncomeInfo.BusinessIncome,
                    PensionIncome = x.IncomeInfo.PensionIncome,
                    FreelanceIncome = x.IncomeInfo.FreelanceIncome,
                    OtherIncome = x.IncomeInfo.OtherIncome,
                    HasOtherCredits = x.IncomeInfo.HasOtherCredits,
                    HasApartmentOrHouse = x.PropertyStatus.HasApartmentOrHouse,
                    HasCommercialProperty = x.PropertyStatus.HasCommercialProperty,
                    HasLand = x.PropertyStatus.HasLand,
                    HasMultipleProperties = x.PropertyStatus.HasMultipleProperties,
                    HasPartialOwnership = x.PropertyStatus.HasPartialOwnership,
                    NoProperty = x.PropertyStatus.NoProperty,
                    VehicleCount = x.PropertyStatus.VehicleCount,
                    MaritalStatus = x.MaritalStatus.Status,  
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LoanApplicationHistoryViewModel>?> GetLoanApplicationHistoryForCustomerAsync(string userId)
        {
            return await repository.All<LoanApplication>()
                .Where(x => x.CustomerId == userId)
                .Select(x => new LoanApplicationHistoryViewModel
                {
                    Id = x.Id,
                    LoanAmount = x.LoanAmount,
                    LoanTermMonths = x.LoanTermMonths,
                    InterestRate = x.InterestRate,
                    Type = x.Type,
                    LoanRepaymentNumber = x.LoanRepaymentNumber,
                    Status = x.Status,
                })
                .ToListAsync();
        }
=======
                RepaymentAmount = loanApplication.LoanAmount,
                Status = Infinia.Core.Constants.LoanRepaymentStatus.Pending
            };
            await repository.AddAsync(loanRepayment);
            await repository.SaveChangesAsync();*/
        }

>>>>>>> origin/main

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
<<<<<<< HEAD

        public async Task<bool> LoanApplicationExistsAsync(int id, string userId)
        {
            return await repository.All<LoanApplication>().AnyAsync(x => x.Id == id && x.CustomerId == userId);
        }
=======
>>>>>>> origin/main
    }
}
