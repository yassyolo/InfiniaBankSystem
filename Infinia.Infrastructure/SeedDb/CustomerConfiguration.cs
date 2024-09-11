using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Infrastructure.SeedDb
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(x => x.Notifications)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.LoanApplications)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.IdentityCard)
                .WithOne()
                .HasForeignKey<Customer>(x => x.IdentityCardId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Address)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            var data = new SeedData();
            builder.HasData(new Customer[] { data.Customer });
        }
    }
}
