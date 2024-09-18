namespace Infinia.Core.ViewModels.Loan
{
    public class LoanApprovalViewModel
    {
        public double ProbabilityOfApproval { get; set; }

        public double CreditScore { get; set; }

        public string RiskGroup { get; set; } = string.Empty;
    }
}
