using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Account;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Account entity")]
    public class Account
    {
        [Comment("Account identifier")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Comment("Account number")]
        public byte[] EncryptedIBAN { get; set; } = null!;

        [Comment("Account type")]
        [MaxLength(TypeMaxLength)]
        [Required]
        public string Type { get; set; } = string.Empty; 

        [Comment("Account name")]
        [MaxLength(NameMaxLength)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Comment("Branch that the country is associated with")]
        [MaxLength(BranchMaxLength)]
        [Required]
        public string Branch { get; set; } = string.Empty;

        [Comment("Account balance")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Balance { get; set; }

        [Comment("Account creation date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreationDate { get; set; }

        [Comment("Account customer")]
        public Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(Customer))]
        [Comment("customer account identifier")]
        public string CustomerId { get; set; } = string.Empty;

        [Comment("Account status")]
        [MaxLength(StatusMaxLength)]
        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Comment("Account monthly fee")]
        public decimal MonthlyFee { get; set; }

        [Comment("Last time a monthly fee was deducted")]
        [DataType(DataType.DateTime)]
        public DateTime? LastMonthlyFeeDeduction { get; set; }

        [Comment("List of transactions related to this account")]
        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
