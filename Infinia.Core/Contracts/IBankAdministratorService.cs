
using Infinia.Core.ViewModels.BankAdministrator;

namespace Infinia.Core.Contracts
{
    public interface IBankAdministratorService
    {
        Task<CombinedBranchAnalysisStatisticsViewModel> GetBranchAnalysisAsync(string branchName, DateTime startDate, DateTime endDate);
        Task<CashFlowCombinedWeeklyData> GetHistoricalDataAsync(string branchName);
        Task<CashFlowCombinedWeeklyData> GenerateSyntheticHistoricalDataAsync(string branchName);
    }
}
