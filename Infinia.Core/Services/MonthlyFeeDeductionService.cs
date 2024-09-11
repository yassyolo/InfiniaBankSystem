using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infinia.Core.Services
{
    public class MonthlyFeeDeductionService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public MonthlyFeeDeductionService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false) 
            {
                var currentDate = DateTime.UtcNow;
                if (currentDate.Day == 11)
                {
                    await DeductMonthlyFeeAsync();
                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task DeductMonthlyFeeAsync()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var accounts = dbContext.Accounts.ToList();
                var todaysYear = DateTime.UtcNow.Year;
                var todaysMonth = DateTime.UtcNow.Month;

                foreach (var account in accounts)
                {
                    if (account.Type != "Bank" && account.LastMonthlyFeeDeduction.Value.Month != todaysMonth ||
                        account.LastMonthlyFeeDeduction.Value.Year != todaysYear)
                    {
                        account.Balance -= account.MonthlyFee;
                        account.LastMonthlyFeeDeduction = DateTime.UtcNow;

                        var notification = new Notification
                        {
                            CustomerId = account.CustomerId,
                            Content = $"Monthly fee was deducted from your account with name {account.Name}.",
                            CreationDate = DateTime.UtcNow,
                            IsRead = false
                        };  
                        await dbContext.Notifications.AddAsync(notification);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
