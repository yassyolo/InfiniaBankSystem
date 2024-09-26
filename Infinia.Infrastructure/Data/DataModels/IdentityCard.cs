using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Identity card entity")]
    public class IdentityCard
    {
        [Key]
        [Comment("Identity card identifier")]
        public int Id { get; set; }

        [Comment("Identity card SSN")]
        [Required]
        public byte[] EncryptedSSN { get; set; } = null!;

        [Comment("Identity card number")]
        [Required]
        public byte[] EncryptedCardNumber { get; set; } = null!;

        [Comment("Identity card issuer")]
        [Required]
        public byte[] EncryptedIssuer { get; set; } = null!;

        [Comment("Identity card nationality")]
        [Required]
        public byte[] EncryptedNationality { get; set; } = null!;

        [Comment("Identity card sex")]
        [Required]
        public byte[] EncryptedSex { get; set; } = null!;
    }
}
