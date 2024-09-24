namespace Infinia.Core.ViewModels.Loan
{
    public class LoanApprovalResponseViewModel
    {
        public double credit_score { get; set; }
        public double probability { get; set; }
        public string risk_category { get; set; } = string.Empty;
    }
}
