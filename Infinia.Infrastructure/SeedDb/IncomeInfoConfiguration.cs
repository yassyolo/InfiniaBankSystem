using Infinia.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Infrastructure.SeedDb
{
    public class IncomeInfoConfiguration : IEntityTypeConfiguration<IncomeInfo>
    {
        public void Configure(EntityTypeBuilder<IncomeInfo> builder)
        {
            builder.Property(x => x.BusinessIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.OtherIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.NetMonthlyIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.FixedMonthlyExpenses)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.PensionIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.PermanentContractIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.TemporaryContractIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.OtherIncome)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.FreelanceIncome)
               .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.PensionIncome)
               .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.BusinessIncome)
               .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.CivilContractIncome)
               .HasColumnType("decimal(18, 4)");
        }
    }
}
