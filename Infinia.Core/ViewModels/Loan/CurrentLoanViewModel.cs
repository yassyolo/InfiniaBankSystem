namespace Infinia.Core.ViewModels.Loan
{
    public class CurrentLoanViewModel
    {
        public int Id { get; set; }

        public string LoanAmount { get; set; } = string.Empty;

        public int LoanTermMonths { get; set; }

        public string InterestRate { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public int LoanRepaymentNumber { get; set; }

        public string LoanRepaymentAmount { get; set; } = string.Empty;
    }
}
