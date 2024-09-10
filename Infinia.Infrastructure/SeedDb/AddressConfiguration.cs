using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Infrastructure.SeedDb
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasMany(x => x.Customers)
                .WithOne(x => x.Address)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            var data = new SeedData();
            builder.HasData(new Address[] { data.Address });
        }
    }
}
