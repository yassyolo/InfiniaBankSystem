using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Account;

namespace Infinia.Core.ViewModels.Account
{
    public class ChangeAccountNameViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
        public string CurrentName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
        public string NewName { get; set; } = string.Empty;
    }
}
