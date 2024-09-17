using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.LoanApplication;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Address;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.IdentityCard;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Transaction;
using static Infinia.Core.MessageConstants.ErrorMessages;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Core.ViewModels
{
    public class LoanApplicationViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(EducationLevelMaxLength, MinimumLength = EducationLevelMinLength, ErrorMessage = LengthErrorMessage)]
        public string ApplicationEducationLevel { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool IsRetiredApplication { get; set; }

        [StringLength(EmployerMaxLength, MinimumLength = EmployerMinLength, ErrorMessage = LengthErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string EmployerNameApplication { get; set; } = string.Empty;

        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength, ErrorMessage = LengthErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string PositionApplication { get; set; } = string.Empty;

        [Range(YearsMinValue, YearsMinValue, ErrorMessage = InvalidYearsErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public int YearsAtJobApplicationApplication { get; set; }

        [Range(MonthsMinValue, MonthsMaxValue, ErrorMessage = InvalidMonthsErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public int MonthsAtJobApplicationApplication { get; set; }

        [Range(YearsMinValue, YearsMaxValue, ErrorMessage = InvalidYearsErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public int TotalWorkExperienceYearsApplication { get; set; }

        [Range(MonthsMinValue, MonthsMaxValue, ErrorMessage = InvalidMonthsErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public int TotalWorkExperienceMonthsApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(HouseholdMembersMinValue, HouseholdMembersMinValue, ErrorMessage = InvalidHouseholdMembersErrorMessage)]
        public int NumberOfHouseholdMembersApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(HouseholdMembersMinValue, HouseholdMembersMaxValue, ErrorMessage = InvalidHouseholdMembersErrorMessage)]
        public int MembersWithProvenIncomeApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(HouseholdMembersMinValue, HouseholdMembersMaxValue, ErrorMessage = InvalidHouseholdMembersErrorMessage)]
        public int DependentsApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        public decimal NetMonthlyIncomeApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(MonthlyExpensesMinValue, MonthlyExpensesMaxValue, ErrorMessage = InvalidMonthlyExpensesErrorMessage)]
        public decimal FixedMonthlyExpensesApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal PermanentContractIncomeApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal TemporaryContractIncomeApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal CivilContractIncomeApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal BusinessIncomeApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal PensionIncomeApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal FreelanceIncomeApplication { get; set; }

        [Range(IncomeMinValue, IncomeMaxValue, ErrorMessage = InvalidIncomeErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal OtherIncomeApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool HasOtherCreditsApplication { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength, ErrorMessage = LengthErrorMessage)]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength, ErrorMessage = LengthErrorMessage)]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(StreetMaxLength, MinimumLength = StreetMinLength, ErrorMessage = LengthErrorMessage)]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(PostalCodeMaxLength, ErrorMessage = LengthErrorMessage)]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CardNumberMaxLength, MinimumLength = CardNumberMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(IssuerMaxLength, MinimumLength = IssuerMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardIssuer { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]        
        public DateTime IdentityCardIssueDate { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardNationality { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(SexMaxLength, MinimumLength = SexMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardSex { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(SSNMaxLength, MinimumLength = SSNMinLength, ErrorMessage = LengthErrorMessage)]
        public string SSN { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool HasApartmentOrHouse { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool HasCommercialProperty { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool HasLand { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool HasMultipleProperties { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool HasPartialOwnership { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public bool NoProperty { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(VehicleCountMinValue, VehicleCountMinValue, ErrorMessage = InvalidVehicleCountErrorMessage)]
        public int VehicleCount { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(MaritalStatusMaxLength, MinimumLength = MaritalStatusMinLength, ErrorMessage = LengthErrorMessage)]
        public string MaritalStatusApplication { get; set; } = string.Empty;

        public int AccountId { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(IBANMaxLength, ErrorMessage = InvalidIbanErrorMessage)]
        public string AccountIBAN { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(LoanAmountMinValue, LoanAmountMaxValue, ErrorMessage = InvalidLoanAmountErrorMessage)]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(LoanTermMonthsMinValue, LoanTermMonthsMaxValue, ErrorMessage = InvalidLoanTermMonthsErrorMessage)]
        public int LoanTermMonths { get; set; }

        public double InterestRate { get; set; }

        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(LoanRepaymentNumberMinValue, LoanRepaymentNumberMaxValue, ErrorMessage = InvalidLoanRepaymentNumberErrorMessage)]
        public int LoanRepaymentNumber { get; set; }
    }
}

