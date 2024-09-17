namespace Infinia.Core.ViewModels.Loan
{
    public class CurrentLoanViewModel
    {
        public int Id { get; set; }

        public decimal LoanAmount { get; set; }

        public int LoanTermMonths { get; set; }

        public double InterestRate { get; set; }

        public string Type { get; set; } = string.Empty;

        public int LoanRepaymentNumber { get; set; }

        public string Status { get; set; } = string.Empty;

        public string LoanRepaymentAmount { get; set; } = string.Empty;
    }
}
