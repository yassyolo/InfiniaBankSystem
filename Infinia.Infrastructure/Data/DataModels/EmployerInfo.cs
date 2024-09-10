using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.LoanApplication;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Employer information entity")]
    public class EmployerInfo
    {
        [Key]
        [Comment("Employer information identifier")]
        public int Id { get; set; }

        [Comment("Is the person retired")]
        [Required]
        public bool IsRetired { get; set; }

        [Comment("Employer name")]
        [MaxLength(EmployerMaxLength)]
        [Required]
        public string EmployerName { get; set; } = string.Empty; 

        [Comment("Position")]
        [MaxLength(PositionMaxLength)]
        [Required]
        public string Position { get; set; } = string.Empty;

        [Comment("Years at job")]
        [Required]
        public int YearsAtJob { get; set; }

        [Comment("Months at job")]
        [Required]
        public int MonthsAtJob { get; set; }

        [Comment("Total work experience years")]
        [Required]
        public int TotalWorkExperienceYears { get; set; }

        [Comment("Total work experience months")]
        [Required]
        public int TotalWorkExperienceMonths { get; set; }
    }
}
