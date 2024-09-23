using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Loan;
using Infinia.Infrastructure.Repository;
using static Infinia.Core.Constants.LoanStatusConstants;
using static Infinia.Core.Constants.LoanRepaymentStatus;
using Microsoft.EntityFrameworkCore;
using Infinia.Core.ViewModels;
using Infinia.Infrastructure.Data.DataModels;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace Infinia.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository repository;
        private readonly IEncryptionService encryptionService;
        private readonly HttpClient httpClient;

        public LoanService(IRepository repository, IEncryptionService encryptionService)
        {
            this.repository = repository;
            this.encryptionService = encryptionService;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://creditscoringapi-czdnd3gvcydze4fw.canadacentral-01.azurewebsites.net/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task ApplyForLoanAsync(LoanApplicationViewModel model, string userId)
        {

            var education = new Education
            {
                EducationLevel = model.EducationLevel
            };
            await repository.AddAsync(education);
            await repository.SaveChangesAsync();
            var maritalStatus = new MaritalStatus
            {
                Status = model.MaritalStatus
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
                TotalWorkExperienceYears = model.TotalWorkExperienceYears,
                TotalWorkExperienceMonths = model.TotalWorkExperienceMonths
            };
            await repository.AddAsync(employerInfo);
            await repository.SaveChangesAsync();
            var householdInfo = new HouselholdInfo
            {
                NumberOfHouseholdMembers = model.NumberOfHouseholdMembers,
                Dependents = model.Dependents,
                MembersWithProvenIncome = model.MembersWithProvenIncome
            };
            await repository.AddAsync(householdInfo);
            await repository.SaveChangesAsync();
            var incomeInfo = new IncomeInfo
            {
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
            {
                HasApartmentOrHouse = model.HasApartmentOrHouse,
                HasCommercialProperty = model.HasCommercialProperty,
                HasLand = model.HasLand,
                HasMultipleProperties = model.HasMultipleProperties,
                HasPartialOwnership = model.HasPartialOwnership,
                NoProperty = model.NoProperty,
                VehicleCount = model.VehicleCount
            };
            await repository.AddAsync(propertyStatus);
            await repository.SaveChangesAsync();
            
            Account account = null;  
            var accounts = await repository.AllReadOnly<Account>().Where(x => x.CustomerId == userId).ToListAsync();
            foreach (var acc in accounts)
            {
                if (encryptionService.Decrypt(acc.EncryptedIBAN) == model.AccountIBAN && acc.Type == "Current")
                {
                    account = acc;
                }
            }
            var loanApplication = new LoanApplication
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
                Status = Infinia.Core.Constants.LoanStatusConstants.Pending
            };
            await repository.AddAsync(loanApplication);
            await repository.SaveChangesAsync();
            //TODO: Implement the logic for loan repayment
            var loanRepayment = new LoanRepayment
            {
                LoanApplicationId = loanApplication.Id,
                RepaymentAmount = CalculateEMI(model.LoanAmount, model.InterestRate, model.LoanTermMonths),
                Status = Infinia.Core.Constants.LoanRepaymentStatus.Pending
            };
            await repository.AddAsync(loanRepayment);
            await repository.SaveChangesAsync();

            var accountBalance = account.Balance;
            await ApproveLoanAsync(model, userId, accountBalance, loanRepayment.RepaymentAmount);
        }

        private decimal CalculateEMI(decimal loanAmount, decimal annualInterestRate, int termMonths)
        {
            decimal monthlyInterestRate = annualInterestRate / 12 / 100;
            decimal emi = (loanAmount * monthlyInterestRate * (decimal)Math.Pow((double)(1 + monthlyInterestRate), termMonths)) /
                           ((decimal)Math.Pow((double)(1 + monthlyInterestRate), termMonths) - 1);

            return emi;
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
                    InterestRate = x.InterestRate.ToString(),
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
                    InterestRate = x.InterestRate.ToString(),
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
                    InterestRate = x.InterestRate.ToString(),
                    Type = x.Type,
                    LoanRepaymentNumber = x.LoanRepaymentNumber,
                    Status = x.Status,
                })
                .ToListAsync();
        }

          
        public async Task<LoanApprovalViewModel> ApproveLoanAsync(LoanApplicationViewModel model, string userId, decimal accountBalance, decimal repaymentAmount)
        { 
            var paidAllLoansOnTime = await repository.All<LoanRepayment>().Where(x => x.LoanApplication.CustomerId == userId).Select(x => x.Status).AnyAsync(x => x.Contains(Overdue));
            var maritalStatus = model.MaritalStatus switch
            {
                "Женен" => 1,
                "Неженен" => 0,
                "Женена" => 1,
                "Неженена" => 0,
                _ => 0
            };
            var educationLevel = model.EducationLevel switch
            {
                "1" => 1,
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                _ => 1 
            };
            var jsonModel = JsonConvert.SerializeObject(new
            {
                NetMonthlyIncome = (int)model.NetMonthlyIncome,
                FixedMonthlyExpenses = (int)model.FixedMonthlyExpenses,
                PermanentContractIncome = (int)model.PermanentContractIncome,
                TemporaryContractIncome = (int)model.TemporaryContractIncome,
                CivilContractIncome = (int)model.CivilContractIncome,
                BusinessIncome = (int)model.BusinessIncome,
                PensionIncome = (int)model.PensionIncome,
                FreelanceIncome = (int)model.FreelanceIncome,
                OtherIncome = (int)model.OtherIncome,
                HasApartmentOrHouse = model.HasApartmentOrHouse ? 1 : 0,
                HasCommercialProperty = model.HasCommercialProperty ? 1 : 0,
                HasLand = model.HasLand ? 1 : 0,
                HasMultipleProperties = model.HasMultipleProperties ? 1 : 0,
                HasPartialOwnership = model.HasPartialOwnership ? 1 : 0,
                NoProperty = model.NoProperty ? 1 : 0,
                VehicleCount = (int)model.VehicleCount,
                MaritalStatus = maritalStatus,
                HasOtherCredits = model.HasOtherCredits ? 1 : 0,
                NumberOfHouseholdMembers = (int)model.NumberOfHouseholdMembers,
                MembersWithProvenIncome = (int)model.MembersWithProvenIncome,
                Dependents = (int)model.Dependents,
                IsRetired = model.IsRetired ? 1 : 0,
                YearsAtJob = (int)model.YearsAtJob,
                MonthsAtJob = (int)model.MonthsAtJob,
                TotalWorkExperienceYears = (int)model.TotalWorkExperienceYears,
                TotalWorkExperienceMonths = (int)model.TotalWorkExperienceMonths,
                EducationLevel = (int)educationLevel,
                LoanAmount = (int)model.LoanAmount,
                LoanTermMonths = (int)model.LoanTermMonths,
                InterestRate = (int)model.InterestRate,
                AccountBalance = (int)accountBalance,
                LoanRepayment = (int)repaymentAmount,
                HasLoans = model.HasOtherCredits ? 1 : 0,
                PaidAllLoansOnTime = paidAllLoansOnTime ? 1 : 0,
            });

            Console.WriteLine(jsonModel);
            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("predict", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var prediction = JsonConvert.DeserializeObject<LoanApprovalViewModel>(responseString);
            return prediction;
        }
        public async Task<bool> LoanApplicationExistsAsync(int id, string userId)
        {
            return await repository.All<LoanApplication>().AnyAsync(x => x.Id == id && x.CustomerId == userId);
        }

        public async Task GetMissingValuesForLoanApplicationAsync(LoanApplicationViewModel model)
        {
            var customer = await repository.All<Customer>().FirstOrDefaultAsync(x => x.UserName == "ivanivanov");
            var customerId = customer.Id;
            var identityCard = await repository.All<Customer>().Select(x => x.IdentityCard).FirstOrDefaultAsync();
            var cardNumber = encryptionService.Decrypt(identityCard.EncryptedCardNumber);
            var cardIssuer = encryptionService.Decrypt(identityCard.EncryptedIssuer);
            var cardDate = encryptionService.Decrypt(identityCard.EncryptedDateOfIssue);
            var ssn = encryptionService.Decrypt(identityCard.EncryptedSSN);
            var nationality = encryptionService.Decrypt(identityCard.EncryptedNationality);
            var sex = encryptionService.Decrypt(identityCard.EncryptedSex);
            var accountIBAN = await repository.All<Account>().Where(x => x.Id == 3 ).Select(x => encryptionService.Decrypt(x.EncryptedIBAN)).FirstOrDefaultAsync();
        }
    }
}
