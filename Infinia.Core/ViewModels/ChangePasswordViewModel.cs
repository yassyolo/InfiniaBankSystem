using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
namespace Infinia.Core.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Compare("NewPassword", ErrorMessage = PasswordsDoNotMatchErrorMessage)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
