using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Customer;
namespace Infinia.Core.ViewModels
{
    public class ChangeUsernameViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = LengthErrorMessage)]
        public string CurrentUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = LengthErrorMessage)]
        public string NewUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Compare("NewUsername", ErrorMessage = UsernamesDoNotMatchErrorMessage)]
        public string ConfirmUsername { get; set; } = string.Empty;
    }
}
