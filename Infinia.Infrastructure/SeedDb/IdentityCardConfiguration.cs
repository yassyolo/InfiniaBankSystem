using Infinia.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Infrastructure.SeedDb
{
    public class IdentityCardConfiguration : IEntityTypeConfiguration<IdentityCard>
    {

        public void Configure(EntityTypeBuilder<IdentityCard> builder)
        {
            var data = new SeedData();
            builder.HasData(new IdentityCard[] { data.IdentityCard });
        }
    }
}
