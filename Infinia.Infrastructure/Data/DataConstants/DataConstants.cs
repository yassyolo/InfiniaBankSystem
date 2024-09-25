namespace Infinia.Infrastructure.Data.DataConstants
{
    public static class DataConstants
    {
        public static class Account
        {
            public const int TypeMaxLength = 20;
            public const int TypeMinLength = 0;
            public const int StatusMaxLength = 20;
            public const int StatusMinLength = 0;
            public const int NameMaxLength = 40;
            public const int NameMinLength = 2;
            public const int BranchMaxLength = 20;
            public const int BranchMinLength = 2;
            public const double BalanceMinValue = 0;
            public const double BalanceMaxValue = 100000;
            
        }
        public static class Customer
        {
            public const int NameMaxLength = 70;
            public const int NameMinLength = 2;
            public const int UsernameMaxLength = 15;
            public const int UsernameMinLength = 5;
            public const int PasswordMaxLength = 15;
            public const int PasswordMinLength = 8;
        }
        public static class IdentityCard
        {
            public const int SSNMaxLength = 10;
            public const int SSNMinLength = 10;
            public const int CardNumberMaxLength = 10;
            public const int CardNumberMinLength = 10;
            public const int IssuerMaxLength = 20;
            public const int IssuerMinLength = 2;
            public const int NationalityMaxLength = 20;
            public const int NationalityMinLength = 2;
            public const int SexMaxLength = 10;
            public const int SexMinLength = 3;
        }
        public static class Address
        {
            public const int CityMaxLength = 20;
            public const int CityMinLength = 2;
            public const int StreetMaxLength = 100;
            public const int StreetMinLength = 2;
            public const int PostalCodeMaxLength = 4;
            public const int CountryMaxLength = 20;
            public const int CountryMinLength = 2;
        }
        public static class LoanApplication
        {
            public const int EducationLevelMaxLength = 20;
            public const int EducationLevelMinLength = 0;
            public const int MaritalStatusMaxLength = 20;
            public const int MaritalStatusMinLength = 0;
            public const int ResidenceStatusMaxLength = 20;
            public const int ResidenceStatusMinLength = 0;
            public const int EmployerMaxLength = 60;
            public const int EmployerMinLength = 0;
            public const int PositionMaxLength = 60;
            public const int PositionMinLength = 0;
            public const int YearsMinValue = 0;
            public const int YearsMaxValue = 60;
            public const int MonthsMinValue = 0;
            public const int MonthsMaxValue = 720;
            public const int HouseholdMembersMinValue = 0;
            public const int HouseholdMembersMaxValue = 20;
            public const int MonthlyIncomeMinValue = 0;
            public const double IncomeMaxValue = 20000;
            public const double IncomeMinValue = 0;
            public const double MonthlyExpensesMinValue = 0;
            public const double MonthlyExpensesMaxValue = 20000;
            public const int LoanAmountMaxValue = 200000;
            public const int LoanAmountMinValue = 0;
            public const int LoanTermMonthsMaxValue = 300;
            public const int LoanTermMonthsMinValue = 0;
            public const int LoanTypeMaxLength = 30;
            public const int LoanTypeMinLength = 0;
            public const double LoanRepaymentAmountMinValue = 0;
            public const double LoanRepaymentAmountMaxValue = 5000;
            public const int LoanRepaymentNumberMinValue = 1;
            public const int LoanRepaymentNumberMaxValue = 31;
            public const int VehicleCountMinValue = 0;  
            public const int VehicleCountMaxValue = 6;
        }
        public static class Transaction
        {
            public const int TypeMaxLength = 30;
            public const int TypeMinLength = 0;
            public const int ReceiverNameMaxLength = 50;
            public const int ReceiverNameMinLength = 0;
            public const int DescriptionMaxLength = 100;
            public const int DescriptionMinLength = 0;
            public const int ReasonMaxLength = 100;
            public const int ReasonMinLength = 0;
            public const decimal AmountMinValue = 0;
            public const decimal AmountMaxValue = 100000;
            public const int IBANMaxLength = 18;
            public const int EncryptedReceiverIBANMinLength = 18;
            public const int BicMaxLength = 4;
            public const int BicMinLength = 4;
        }
        public static class Notification
        {
            public const int ContentMaxLength = 200;
            public const int ContentMinLength = 0;
            public const int NotificationTitleMaxLength = 40;
            public const int NotificationTitleMinLength = 0;
        }
    }
}
