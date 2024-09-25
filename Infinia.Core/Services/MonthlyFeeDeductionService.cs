using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Transaction;
using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Microsoft.Extensions.DependencyInjection;
using static Infinia.Core.Constants.TransactionFeeConstants;
using Microsoft.Extensions.Hosting;

namespace Infinia.Core.Services
{
    public class MonthlyFeeDeductionService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MonthlyFeeDeductionService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentDate = DateTime.UtcNow;
                if (currentDate.Day == 23)
                {
                    await DeductMonthlyFeeAsync();
                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task DeductMonthlyFeeAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var encryptionService = scope.ServiceProvider.GetRequiredService<IEncryptionService>();
                var transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();

                var accounts = dbContext.Accounts.Where(x => x.Type == "Current").ToList();
                var todaysYear = DateTime.UtcNow.Year;
                var todaysMonth = DateTime.UtcNow.Month;
                var bankAccount = dbContext.Accounts.FirstOrDefault(x => x.Name == "Bank account");

                foreach (var account in accounts)
                {
                    if (
                        (account.LastMonthlyFeeDeduction == null ||
                         account.LastMonthlyFeeDeduction.Value.Month != todaysMonth ||
                         account.LastMonthlyFeeDeduction.Value.Year != todaysYear))
                    {

                        var model = new TransactionWithinTheBankViewModel
                        {
                            Reason = "Удържана месечна такса",
                            Description = $"Удържана месечна такса на {DateTime.UtcNow}",
                            ReceiverName = "Bank account",
                            Amount = MonthlyFeeDeductionFee, 
                            ReceiverIBAN = encryptionService.Decrypt(bankAccount.EncryptedIBAN),
                            AccountId = account.Id
                        };

                        await transactionService.MakeMonthlyFeeDeductionTransactionAsync(model, account.CustomerId);

                        account.LastMonthlyFeeDeduction = DateTime.UtcNow;

                        var notification = new Notification
                        {
                            CustomerId = account.CustomerId,
                            Content = $"Месечната такса беше удържана от вашата сметка с име {account.Name}.",
                            CreationDate = DateTime.UtcNow,
                            IsRead = false,
                            Title = "Месечна такса💰"
                        };

                        await dbContext.Notifications.AddAsync(notification);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
