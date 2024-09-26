namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class CombinedBranchAnalysisStatisticsViewModel
    {
        public BranchAnalysisStatisticsViewModel AllTimeStatistics { get; set; } = null!;
        public BranchAnalysisStatisticsViewModel SelectedIntervalStatistics { get; set; } = null!;

        public string BranchName { get; set; } = string.Empty;

        public DateTime StartInterval { get; set; }

        //[Required(ErrorMessage = RequiredFieldErrorMessage)]
        public DateTime EndInterval { get; set; }

    }
}
