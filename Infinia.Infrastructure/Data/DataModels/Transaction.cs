using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Transaction;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Transaction entity")]
    public class Transaction
    {
        [Key]
        [Comment("Transaction identifier")]
        public int Id { get; set; }

        [Comment("Transaction type")]
        [Required]
        [MaxLength(TypeMaxLength)]
        public string Type { get; set; } = string.Empty; 

        [Comment("Full name of the receiver of the transaction")]
        [MaxLength(ReceiverNameMaxLength)]
        [Required]
        public string ReceiverName { get; set; } = string.Empty; 

        [Comment("IBAN of the receiver")]
        [Required]
        public byte[] EncryptedReceiverIBAN { get; set; } = null!;

        [Comment("Transaction amount")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }

        [Comment("Transaction description")]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Comment("Transaction reason")]
        [MaxLength(ReasonMaxLength)]
        [Required]
        public string Reason { get; set; } = string.Empty;

        [Comment("Transaction account")]
        public Account Account { get; set; } = null!;

        [ForeignKey(nameof(Account))]
        [Comment("Transaction account identifier")]
        [Required]
        public int AccountId { get; set; } 

        [Comment("Transaction date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime TransactionDate { get; set; }

        [Comment("Transaction BIC")]
        public string? Bic { get; set; } = string.Empty;

        [Comment("Transaction fee")]
        public decimal Fee { get; set; } 
    }
}
