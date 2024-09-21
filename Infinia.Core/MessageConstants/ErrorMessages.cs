namespace Infinia.Core.MessageConstants
{
    public static class ErrorMessages
    {
        public const string RequiredFieldErrorMessage = "Полето е задължително.";
        public const string LengthErrorMessage = "Полето трябва да е между {2} и {1} знака.";
        public const string InvalidEmailAddress = "Невалиден имейл адрес.";
        public const string PasswordLengthErrorMessage = "Паролата трябва да бъде между {2} и {1} знака.";
        public const string PasswordCharactersErrorMessage = "Паролата трябва да съдържа поне една глава буква, една малка буква, една цифра и един специален знак.";
        public const string InvalidHouseholdMembersErrorMessage = "Members must be between {1} and {2}.";
        public const string InvalidVehicleCountErrorMessage = "Vehicles count must be between {1} and {2}.";
        public const string InvalidIncomeErrorMessage = "Income must be between {1} and {2} levas.";
        public const string InvalidLoanAmountErrorMessage = "Loan amount must be between {1} and {2} levas.";
        public const string InvalidLoanTermMonthsErrorMessage = "Loan term months must be between {1} and {2}.";
        public const string InvalidLoanRepaymentNumberErrorMessage = "Loan term number must be between {1} and {2}.";
        public const string InvalidMonthlyExpensesErrorMessage = "Expenses must be between {1} and {2} levas.";
        public const string InvalidYearsErrorMessage = "Job years must be between {1} and {2}.";
        public const string InvalidMonthsErrorMessage = "Job months must be between {1} and {2}.";
        public const string InvalidBalanceErrorMessage = "Balance must be between {2} and {1} levas.";
        public const string NegativeBalanceErrorMessage = "Balance must not be negative.";
        public const string PasswordsDoNotMatchErrorMessage = "Паролите не съвпадат.";
        public const string InvalidIbanErrorMessage = "Invalid IBAN.";
        public const string InvalidReceiverIBANErrorMessage = "Сметка с такъв IBAN не съществува в банката.";
        public const string InvalidReceivalAccountErrorMessage = "Receival account must not be the same as the sender account.";
        public const string InvalidUsernameErrorMessage = "Невалидно потребителско име.";
        public const string InvalidAccountErrorMessage = "Невалидна сметка.";
        public const string EmailNotConfirmedErrorMessage = "Имейла Ви трябва да е потвърден преди да се логнете в системата.";
        public const string InvalidAmountErrorMessage = "Сумата трябва да е между {2} и {1} лева.";
        public const string InvalidAccountDeletion = "Cannot delete account.";
        public const string InavlidCustomerIdentityCardErrorMessage = "Customer with this identity card does not exist.";
        public const string InavlidCustomerAddressErrorMessage = "Customer with this address does not exist.";
        public const string InavlidCustomerAccountIBANErrorMessage = "Customer with this account does not exist.";
        public const string InvalidIdentityCardNumberErrorMessage = "Invalid identity card.";
        public const string InvalidPasswordErrorMessage = "Невалидна парола.";
        public const string InvalidLoginAttemptErrorMessage = "Нвалиден логин.";
        public const string NewPasswordLikeCurrentPasswordErrorMessage = "Новата Ви парола трябва да е различна от настоящата.";
        public const string UsernamesDoNotMatchErrorMessage = "Потребителските имена не съвпадат.";
        public const string NewUsernameLikeCurrentUsernameErrorMessage = "Новото потребителско име трябва да е различно от настоящето.";
        public const string EmailAlreadyExistsErrorMessage = "Потребител със същия имейл съществува.";
        public const string UsernameAlreadyExistsErrorMessage = "Потребител със същия юзърнейм съществува.";
        public const string InvalidUserWithEmailErrorMessage = "Потребител с този имейл адрес не съществува.";
    }
}
