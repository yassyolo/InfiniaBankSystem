namespace Infinia.Core.MessageConstants
{
    public static class ErrorMessages
    {
        public const string RequiredFieldErrorMessage = "Полето е задължително.";
        public const string LengthErrorMessage = "Полето трябва да е между {2} и {1} знака.";
        public const string InvalidEmailAddress = "Невалиден имейл адрес.";
        public const string PasswordLengthErrorMessage = "Паролата трябва да бъде между {2} и {1} знака.";
        public const string PasswordCharactersErrorMessage = "Паролата трябва да съдържа поне една глава буква, една малка буква, една цифра и един специален знак.";
        public const string InvalidHouseholdMembersErrorMessage = "Жителите трябва да бъдат между {1} и {2}.";
        public const string InvalidVehicleCountErrorMessage = "Колите трябва да са между {1} и {2}.";
        public const string InvalidIncomeErrorMessage = "Доходът трябва да е между {1} и {2} лева.";
        public const string InvalidLoanAmountErrorMessage = "Сумата за кредит трябва да е между {1} и {2} лева.";
        public const string InvalidLoanTermMonthsErrorMessage = "Месеците за погасяването трябва да бъдат между {1} и {2}.";
        public const string InvalidLoanRepaymentNumberErrorMessage = "Числото за погасяване на кредита трябва да е между {1} и {2}.";
        public const string InvalidMonthlyExpensesErrorMessage = "Месечните разходи трябва да бъдат между {1} и {2} лева.";
        public const string InvalidYearsErrorMessage = "Гоините стаж трябва да бъдат между {1} и {2}.";
        public const string InvalidMonthsErrorMessage = "Месеците стаж трябва да бъдат между {1} и {2}.";
        public const string InvalidBalanceErrorMessage = "Балансът по сметката трябва да е между {2} и {1} лева.";
        public const string NegativeBalanceErrorMessage = "Балансът по сметката не трябва да е негативен.";
        public const string PasswordsDoNotMatchErrorMessage = "Паролите не съвпадат.";
        public const string InvalidIbanErrorMessage = "Невалиден IBAN.";
        public const string InvalidReceiverIBANErrorMessage = "Сметка с такъв IBAN не съществува в банката.";
        public const string InvalidReceivalAccountErrorMessage = "Сметките трябва да са различни.";
        public const string AmountGreaterThanSenderAccountBalanceErrorMessage = "Сумата трябва да е по-малка от баланса по избраната сметка.";
        public const string InvalidUsernameErrorMessage = "Невалидно потребителско име.";
        public const string InvalidBICErrorMessage = "Невалиден BIC на банката на получателя.";
        public const string InvalidAccountErrorMessage = "Невалидна сметка.";
        public const string EmailNotConfirmedErrorMessage = "Имейла Ви трябва да е потвърден преди да се логнете в системата.";
        public const string InvalidAmountErrorMessage = "Сумата трябва да е между {2} и {1} лева.";
        public const string InvalidAccountDeletion = "Cannot delete account.";
        public const string InavlidCustomerIdentityCardErrorMessage = "Потребител с тази лична карта не съществува в системата.";
        public const string InavlidCustomerAddressErrorMessage = "Потребител с този адрес не съществува.";
        public const string InavlidCustomerAccountIBANErrorMessage = "Потребител с такава сметка не съществува.";
        public const string InvalidIdentityCardNumberErrorMessage = "Невалиден номер на лична карта.";
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
