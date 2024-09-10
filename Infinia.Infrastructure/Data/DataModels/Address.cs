using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Address;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Address entity")]
    public class Address
    {
        [Key]
        [Comment("Address identifier")]
        public int Id { get; set; }

        [MaxLength(StreetMaxLength)]
        [Comment("Street address, including house number")]
        [Required]
        public string Street { get; set; } = string.Empty;

        [MaxLength(CityMaxLength)]
        [Comment("City name")]
        [Required]
        public string City { get; set; } = string.Empty;

        [MaxLength(CountryMaxLength)]
        [Comment("Country name")]
        [Required]
        public string Country { get; set; } = string.Empty;

        [MaxLength(PostalCodeMaxLength)]
        [Comment("Postal or zip code")]
        [Required]
        public string PostalCode { get; set; } = string.Empty;

        [Comment("Customers living at this address")]
        public List<Customer> Customers { get; set; } = null!;
    }
}
