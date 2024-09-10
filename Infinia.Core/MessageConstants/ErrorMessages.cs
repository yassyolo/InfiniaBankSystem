namespace Infinia.Core.MessageConstants
{
    public static class ErrorMessages
    {
        public const string RequiredFieldErrorMessage = "The field {0} is required.";
        public const string LengthErrorMessage = "The field {0} must be between {2} and {1} characters.";
        public const string PasswordsDoNotMatchErrorMessage = "The passwords do not match.";
        public const string InvalidUsernameErrorMessage = "Invalid username.";
        public const string InvalidPasswordErrorMessage = "Invalid password.";
        public const string InvalidLoginAttemptErrorMessage = "Invalid login attempt.";
        public const string NewPasswordLikeCurrentPasswordErrorMessage = "New password must be different from the current password.";
        public const string UsernamesDoNotMatchErrorMessage = "The new username and confirmation username do not match.";
        public const string NewUsernameLikeCurrentUsernameErrorMessage = "New username must be different from the current username.";
        public const string EmailAlreadyExistsErrorMessage = "User with the same email exists.";
        public const string UsernameAlreadyExistsErrorMessage = "User with the same username exists.";
    }
}
