using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Infrastructure.SeedDb
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.Fee)
                .HasColumnType("decimal(18, 4)");
            builder.HasOne(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
