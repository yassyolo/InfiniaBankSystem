using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Notification;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Notification entity")]
    public class Notification
    {
        [Key]
        [Comment("Notification identifier")]
        public int Id { get; set; }

        [Comment("Customer that the notification is associated with")]
        public Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(Customer))]
        [Comment("Customer notification identifier")]
        [Required]
        public string CustomerId { get; set; } = null!;

        [Comment("Notification message")]
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [Comment("Notification status")]
        [Required]
        public bool IsRead { get; set; }

        [Comment("Notification creation date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
