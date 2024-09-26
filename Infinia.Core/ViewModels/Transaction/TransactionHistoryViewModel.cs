namespace Infinia.Core.ViewModels.Transaction
{
    public class TransactionHistoryViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalTransactions { get; set; }
        public string EndBalance { get; set; } = string.Empty;

        public IEnumerable<TransactionRowViewModel> Transactions { get; set; } = new List<TransactionRowViewModel>();
    }
}
