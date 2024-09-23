namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class CashFlowCombinedWeeklyData
    {
        public List<WeeklyCashFlowViewModel> HistoricalData { get; set; } = new List<WeeklyCashFlowViewModel>();
        public decimal ForecastedIncome { get; set; }
    }
}
