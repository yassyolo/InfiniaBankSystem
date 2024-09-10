using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.SeedDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new IncomeInfoConfiguration());
            builder.ApplyConfiguration(new LoanApplicationConfiguration());
            builder .ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new IdentityCardConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;
        public DbSet<Education> Educations { get; set; } = null!;
        public DbSet<IdentityCard> IdentityCards { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<EmployerInfo> EmployerInfos { get; set; } = null!;
        public DbSet<IncomeInfo> IncomeInfos { get; set; } = null!;
        public DbSet<HouselholdInfo> HouselholdInfos { get; set; } = null!;
        public DbSet<PropertyStatus> PropertyStatuses { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<LoanApplication> LoanApplications { get; set; } = null!;
    }
}
