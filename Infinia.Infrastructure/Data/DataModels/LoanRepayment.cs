using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Loan repayment entity")]
    public class LoanRepayment
    {
        [Key]
        [Comment("Loan repayment identifier")]
        public int Id { get; set; }

        [Comment("Loan application identifier")]
        [ForeignKey(nameof(LoanApplication))]
        [Required]
        public int LoanApplicationId { get; set; }

        [Comment("Loan application")]
        public LoanApplication LoanApplication { get; set; } = null!;

        [Comment("Repayment amount")]
        [Required]        
        public decimal RepaymentAmount { get; set; }

        [Comment("Repayment date")]
        public DateTime? Date { get; set; }

        [Comment("Repayment status")]
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
