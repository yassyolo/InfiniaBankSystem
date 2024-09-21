using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.LoanApplication;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Property status entity")]
    public class PropertyStatus
    {
        [Key]
        [Comment("Property status identifier")]
        public int Id { get; set; }

        [Comment("Has apartment or house")]
        [Required]
        public bool HasApartmentOrHouse { get; set; }

        [Comment("Has commercial property")]
        [Required]
        public bool HasCommercialProperty { get; set; }

        [Comment("Has land")]
        [Required]
        public bool HasLand { get; set; }

        [Comment("Has multiple properties")]
        [Required]
        public bool HasMultipleProperties { get; set; }

        [Comment("Has partial ownership")]
        [Required]
        public bool HasPartialOwnership { get; set; }

        [Comment("No property")]
        [Required]
        public bool NoProperty { get; set; }

        [Comment("Vehicle count")]
        [Required]
        public int VehicleCount { get; set; }
    }
}
