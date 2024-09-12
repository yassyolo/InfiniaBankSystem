using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Transaction;

namespace Infinia.Core.ViewModels.Transaction
{
    public class TransactionToAnotherBankViewModel : TransactionWithinTheBankViewModel
    {
        [StringLength(BicMaxLength, MinimumLength = BicMinLength, ErrorMessage = LengthErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string BIC { get; set; } = null!;
    }
}
