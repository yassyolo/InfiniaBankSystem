using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Transaction;


namespace Infinia.Core.ViewModels.Transaction
{
    public class TransactionWithinTheBankViewModel
    {
        [StringLength(ReceiverNameMaxLength, MinimumLength = ReceiverNameMinLength, ErrorMessage = LengthErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string ReceiverName { get; set; } = string.Empty;
      
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string ReceiverIBAN { get; set; } = null!;
        
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public decimal Amount { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        public string? Description { get; set; }

        [StringLength(ReasonMaxLength, MinimumLength = ReasonMinLength, ErrorMessage = LengthErrorMessage)]
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string Reason { get; set; } = string.Empty;

        public int AccountId { get; set; }

        public IEnumerable<AvailableAccountViewModels> AvailableAccounts { get; set; } = new List<AvailableAccountViewModels>();
    }
}
