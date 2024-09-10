using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Infrastructure.SeedDb
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.Balance)
                .HasColumnType("decimal(18, 4)");
            builder.Property(x => x.MonthlyFee)
                .HasColumnType("decimal(18, 4)");
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
            var data = new SeedData();
            builder.HasData(new Account[] { data.Account1, data.SavingsAccountForAccount1, data.Account2, data.SavingAccountForBankAccount, data.BankAccount, data.SavingsAccountForAccount2 });
        }
    }
}
