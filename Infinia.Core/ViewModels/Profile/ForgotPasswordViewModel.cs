using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;

namespace Infinia.Core.ViewModels.Profile
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [EmailAddress(ErrorMessage = InvalidEmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}
