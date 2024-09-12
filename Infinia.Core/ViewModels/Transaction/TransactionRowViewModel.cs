namespace Infinia.Core.ViewModels.Transaction
{
    public class TransactionRowViewModel
    {
        public string CreatedOn { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string ReceiverOrSenderIBAN { get; set; } = string.Empty;
    }
}
