using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.LoanApplication;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Marital status entity")]
    public class MaritalStatus
    {
        [Key]
        [Comment("Marital status identifier")]
        public int Id { get; set; }

        [Comment("Marital status")]
        [MaxLength(MaritalStatusMaxLength)]
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
