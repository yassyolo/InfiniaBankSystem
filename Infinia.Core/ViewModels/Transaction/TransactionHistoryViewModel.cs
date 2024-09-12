namespace Infinia.Core.ViewModels.Transaction
{
    public class TransactionHistoryViewModel
    {
        public string EndBalance { get; set; } = string.Empty;

        public IEnumerable<TransactionRowViewModel> Transactions { get; set; } = new List<TransactionRowViewModel>();
    }
}
