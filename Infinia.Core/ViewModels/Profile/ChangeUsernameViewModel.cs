using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
namespace Infinia.Core.ViewModels
{
    public class ChangeUsernameViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string CurrentUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string NewUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Compare("NewUsername", ErrorMessage = UsernamesDoNotMatchErrorMessage)]
        public string ConfirmUsername { get; set; } = string.Empty;
    }
}
