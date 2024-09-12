using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.IdentityCard;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Account;

namespace Infinia.Core.ViewModels.Account
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(BalanceMinValue, BalanceMaxValue, ErrorMessage = InvalidBalanceErrorMessage)]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CardNumberMaxLength, MinimumLength = CardNumberMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(BranchMaxLength, MinimumLength = BranchMinLength, ErrorMessage = LengthErrorMessage)]
        public string Branch { get; set; } = string.Empty;
       
    }
}
