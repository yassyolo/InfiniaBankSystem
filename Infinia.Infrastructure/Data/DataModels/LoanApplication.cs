using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.LoanApplication;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Loan application entity")]
    public class LoanApplication
    {
        [Key]
        [Comment("Loan application identifier")]
        public int Id { get; set; }

        [Comment("Education identifier")]
        [ForeignKey(nameof(Education))]
        [Required]
        public int EducationId { get; set; }

        [Comment("Education")]
        public Education Education { get; set; } = null!;

        [Comment("Marital status identifier")]
        [ForeignKey(nameof(MaritalStatus))]
        [Required]
        public int MaritalStatusId { get; set; }

        [Comment("Marital status")]
        public MaritalStatus MaritalStatus { get; set; } = null!;

        [Comment("Household information identifier")]
        [ForeignKey(nameof(HouseholdInfo))]
        [Required]
        public int HouseholdInfoId { get; set; }

        [Comment("Household information")]
        public HouselholdInfo HouseholdInfo { get; set; } = null!;

        [Comment("Property status identifier")]
        [ForeignKey(nameof(PropertyStatus))]
        [Required]
        public int PropertyStatusId { get; set; }

        [Comment("Property status")]
        public PropertyStatus PropertyStatus { get; set; } = null!;

        [Comment("Employer information identifier")]
        [ForeignKey(nameof(EmployerInfo))]
        [Required]
        public int EmployerInfoId { get; set; }

        [Comment("Employer information")]
        public EmployerInfo EmployerInfo { get; set; } = null!;

        [Comment("Income information identifier")]
        [ForeignKey(nameof(IncomeInfo))]
        [Required]
        public int IncomeInfoId { get; set; }

        [Comment("Income information")]
        public IncomeInfo IncomeInfo { get; set; } = null!;

        [Comment("Loan application date")]
        [Required]
        public DateTime ApplicationDate { get; set; }

        [Comment("Loan amount")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal LoanAmount { get; set; }

        [Comment("Loan term in months")]
        [Required]
        public int LoanTermMonths { get; set; }

        [Comment("Interest rate")]
        [Required]
        public decimal InterestRate { get; set; }

        [Comment("Loan application customer identifier")]
        [ForeignKey(nameof(Customer))]
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [Comment("Loan application customer")]
        public Customer Customer { get; set; } = null!;

        [Comment("Loan application account identifier")]
        [ForeignKey(nameof(Account))]
        [Required]
        public int AccountId { get; set; }

        [Comment("Loan application account")]
        public Account Account { get; set; } = null!;

        [Comment("Loan application status")]
        public string Status { get; set; } = string.Empty;

        [Comment("Loan application type")]
        [MaxLength(LoanTypeMaxLength)]
        [Required]
        public string Type { get; set; } = string.Empty;

        [Comment("Loan repayment number")]
        [Required]
        public int LoanRepaymentNumber { get; set; }

        [Comment("Loan repayments")]
        public IEnumerable<LoanRepayment> LoanRepayments { get; set; } = new List<LoanRepayment>();
    }
}
