using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Household information entity")]
    public class HouselholdInfo
    {
        [Key]
        [Comment("Household information identifier")]
        public int Id { get; set; }

        [Comment("Number of household members")]
        [Required]
        public int NumberOfHouseholdMembers { get; set; }

        [Comment("Number of household members with proven income")]
        [Required]
        public int MembersWithProvenIncome { get; set; }

        [Comment("Number of dependents that depend on the income of the applicant")]
        [Required]
        public int Dependents { get; set; } 
    }
}
