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
                if (currentDate.Day == 11)
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

                var accounts = dbContext.Accounts.ToList();
                var todaysYear = DateTime.UtcNow.Year;
                var todaysMonth = DateTime.UtcNow.Month;
                var bankAccount = dbContext.Accounts.FirstOrDefault(x => x.Name == "Bank account");

                foreach (var account in accounts)
                {
                    if (account.Type != "Bank" &&
                        (account.LastMonthlyFeeDeduction == null ||
                         account.LastMonthlyFeeDeduction.Value.Month != todaysMonth ||
                         account.LastMonthlyFeeDeduction.Value.Year != todaysYear))
                    {
                        var model = new TransactionWithinTheBankViewModel
                        {
                            Reason = "Monthly fee deduction",
                            Description = $"Monthly fee deduction made on {DateTime.UtcNow}",
                            ReceiverName = "Bank account",
                            Amount = MonthlyFeeDeductionFee, // Define MonthlyFeeDeductionFee in your class or configuration
                            ReceiverIBAN = encryptionService.Decrypt(bankAccount.EncryptedIBAN),
                            AccountId = account.Id
                        };

                        await transactionService.MakeTransactionWithinTheBankAsync(model, account.CustomerId);

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
