using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Infrastructure.SeedDb
{
    public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
        {
            builder.HasOne(x => x.Education)
                .WithMany()
                .HasForeignKey(x => x.EducationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.IncomeInfo)
                .WithMany()
                .HasForeignKey(x => x.IncomeInfoId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.EmployerInfo)
                .WithMany()
                .HasForeignKey(x => x.EmployerInfoId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.MaritalStatus)
                .WithMany()
                .HasForeignKey(x => x.MaritalStatusId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.HouseholdInfo)
                .WithMany()
                .HasForeignKey(x => x.HouseholdInfoId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PropertyStatus)
                .WithMany()
                .HasForeignKey(x => x.PropertyStatusId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.LoanAmount)
                .HasColumnType("decimal(18, 4)");
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.LoanApplications)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
