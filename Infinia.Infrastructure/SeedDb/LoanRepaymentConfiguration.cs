using Infinia.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infinia.Infrastructure.SeedDb
{
    public class LoanRepaymentConfiguration : IEntityTypeConfiguration<LoanRepayment>
    {
        public void Configure(EntityTypeBuilder<LoanRepayment> builder)
        {
            builder.Property(x => x.RepaymentAmount)
                .HasColumnType("decimal(18,4)");
            builder.HasOne(x => x.LoanApplication)
                .WithMany(x => x.LoanRepayments)
                .HasForeignKey(x => x.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
