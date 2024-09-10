using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Income information entity")]
    public class IncomeInfo
    {
        [Key]
        [Comment("Income information identifier")]
        public int Id { get; set; }

        [Comment("Net monthly income")]
        [Required]
        public decimal NetMonthlyIncome { get; set; }

        [Comment("Fixed monthly expenses")]
        [Required]
        public decimal FixedMonthlyExpenses { get; set; }

        [Comment("Permanent contract income")]
        [Required]
        public decimal PermanentContractIncome { get; set; }

        [Comment("Temporary contract income")]
        [Required]
        public decimal TemporaryContractIncome { get; set; }

        [Comment("Civil contract income")]
        [Required]
        public decimal CivilContractIncome { get; set; }

        [Comment("Business income")]
        [Required]
        public decimal BusinessIncome { get; set; }

        [Comment("Pension income")]
        [Required]
        public decimal PensionIncome { get; set; }

        [Comment("Freelance income")]
        [Required]
        public decimal FreelanceIncome { get; set; }

        [Comment("Other income")]
        [Required]
        public decimal OtherIncome { get; set; }

        [Comment("Has other credits")]
        [Required]
        public bool HasOtherCredits { get; set; }
    }
}
