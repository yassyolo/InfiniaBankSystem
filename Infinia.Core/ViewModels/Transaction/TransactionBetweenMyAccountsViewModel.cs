using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Transaction;

namespace Infinia.Core.ViewModels.Transaction
{
    public class TransactionBetweenMyAccountsViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public int AccountIdFromWhichWeWantToSendMoney { get; set;}

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public int AccountIdFromWhichWeWantToReceiveMoney { get; set; }

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Range(AmountMaxValue, AmountMinValue, ErrorMessage = InvalidAmountErrorMessage)]
        public decimal Amount { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(ReasonMaxLength, MinimumLength = ReasonMinLength, ErrorMessage = LengthErrorMessage)]
        public string Reason { get; set; } = string.Empty;

        public IEnumerable<AvailableAccountViewModels> AvailableAccounts { get; set; } = new List<AvailableAccountViewModels>();
    }
}
