using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.LoanApplication;

namespace Infinia.Infrastructure.Data.DataModels
{
    [Comment("Education entity")]
    public class Education
    {
        [Key]
        [Comment("Education identifier")]
        public int Id { get; set; }

        [Comment("Education level")]
        [MaxLength(EducationLevelMaxLength)]
        public string EducationLevel { get; set; } = string.Empty;
    }
}
