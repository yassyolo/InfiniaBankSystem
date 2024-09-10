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
        }
        public static class Customer
        {
            public const int NameMaxLength = 70;
            public const int NameMinLength = 2;
            public const int UsernameMaxLength = 15;
            public const int UsernameMinLength = 5;
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
        }
    }
}
