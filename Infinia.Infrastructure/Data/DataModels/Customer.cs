using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Customer;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Customer entity")]
    public class Customer : IdentityUser
    {
        [Comment("Customer name from identity card")]
        [MaxLength(NameMaxLength)]
        [Required]
        public string Name { get; set; } = string.Empty; 

        [Comment("Customer identity card")]
        public IdentityCard IdentityCard { get; set; } = null!;

        [Comment("Customer identity card identifier")]
        [ForeignKey(nameof(IdentityCard))]
        [Required]
        public int IdentityCardId { get; set; }

        [Comment("Customer address")]
        public Address Address { get; set; } = null!;

        [Comment("Customer address identifier")]
        [ForeignKey(nameof(Address))]
        [Required]
        public int AddressId { get; set; }

        [Comment("Customer accounts")]
        public IEnumerable<Account> Accounts { get; set; } = new List<Account>();

        [Comment("Customer loan application")]
        public IEnumerable<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();

        [Comment("Customer notifications")]
        public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
