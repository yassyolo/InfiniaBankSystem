namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class BranchAnalysisStatisticsViewModel
    {
        public int TotalAccounts { get; set; }
        public int OpenAccounts { get; set; }
        public int ClosedAccounts { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal TotalTransactionVolume { get; set; }
    }
}
