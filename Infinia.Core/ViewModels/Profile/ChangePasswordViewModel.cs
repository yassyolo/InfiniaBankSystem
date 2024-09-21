using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Customer;
namespace Infinia.Core.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = PasswordLengthErrorMessage)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.])[A-Za-z\d@$!%*?&.]{8,}$",
        ErrorMessage = PasswordCharactersErrorMessage)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Compare("NewPassword", ErrorMessage = PasswordsDoNotMatchErrorMessage)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
