namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class CombinedBranchAnalysisStatisticsViewModel
    {
        public BranchAnalysisStatisticsViewModel AllTimeStatistics { get; set; } = null!;
        public BranchAnalysisStatisticsViewModel SelectedIntervalStatistics { get; set; } = null!;
    }
}
