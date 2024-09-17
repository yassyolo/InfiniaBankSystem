namespace Infinia.Core.MessageConstants
{
    public static class ErrorMessages
    {
        public const string RequiredFieldErrorMessage = "The field {0} is required.";
        public const string LengthErrorMessage = "The field {0} must be between {2} and {1} characters.";
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
        public const string PasswordsDoNotMatchErrorMessage = "The passwords do not match.";
        public const string InvalidIbanErrorMessage = "Invalid IBAN.";
        public const string InvalidReceiverIBANErrorMessage = "Account with this IBAN does not exist within the bank.";
        public const string InvalidReceivalAccountErrorMessage = "Receival account must not be the same as the sender account.";
        public const string InvalidUsernameErrorMessage = "Invalid username.";
        public const string InvalidAccountErrorMessage = "Invalid account.";
        public const string EmailNotConfirmedErrorMessage = "Email needs to be confirmed before logging in.";
        public const string InvalidAmountErrorMessage = "The amount must be between {2} and {1} levas.";
        public const string InvalidAccountDeletion = "Cannot delete account.";
        public const string InavlidCustomerIdentityCardErrorMessage = "Customer with this identity card does not exist.";
        public const string InavlidCustomerAddressErrorMessage = "Customer with this address does not exist.";
        public const string InavlidCustomerAccountIBANErrorMessage = "Customer with this account does not exist.";
        public const string InvalidIdentityCardNumberErrorMessage = "Invalid identity card.";
        public const string InvalidPasswordErrorMessage = "Invalid password.";
        public const string InvalidLoginAttemptErrorMessage = "Invalid login attempt.";
        public const string NewPasswordLikeCurrentPasswordErrorMessage = "New password must be different from the current password.";
        public const string UsernamesDoNotMatchErrorMessage = "The new username and confirmation username do not match.";
        public const string NewUsernameLikeCurrentUsernameErrorMessage = "New username must be different from the current username.";
        public const string EmailAlreadyExistsErrorMessage = "User with the same email exists.";
        public const string UsernameAlreadyExistsErrorMessage = "User with the same username exists.";
    }
}
