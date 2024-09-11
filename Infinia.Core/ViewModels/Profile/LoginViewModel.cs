using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
namespace Infinia.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string Password { get; set; } = string.Empty;
    }
}
