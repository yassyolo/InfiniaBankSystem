﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Transaction;
using Infinia.Infrastructure;


namespace Infinia.Core.Services
{
    public class LoanMonthlyRepaymentService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public LoanMonthlyRepaymentService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (stoppingToken.IsCancellationRequested == true) 
            { 
                var currentDate = DateTime.UtcNow;
                await DeductLoanRepayment();
            }
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }

        private async Task DeductLoanRepayment()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();
                var encryptionService = scope.ServiceProvider.GetRequiredService<IEncryptionService>();

                var bankAccount = dbContext.Accounts.FirstOrDefault(x => x.Name == "Bank account");
                var loanRepayments = dbContext.LoanRepayments.ToList();
                var currentDateNumber = DateTime.UtcNow.Day;
                foreach (var loanRepayment in loanRepayments)
                {
                    var loanApplication = dbContext.LoanApplications.FirstOrDefault(x => x.Id == loanRepayment.LoanApplicationId && x.Status == "Approved");
                    if (loanApplication.LoanRepaymentNumber == currentDateNumber)
                    {
                        var model = new TransactionWithinTheBankViewModel()
                        {
                            Reason = "Погасяване на кредит",
                            Description = $"Погасяване на кредит, направено на: {DateTime.UtcNow.ToString()}",
                            ReceiverName = "Bank account",
                            Amount = loanRepayment.RepaymentAmount,
                            ReceiverIBAN = encryptionService.Decrypt(bankAccount.EncryptedIBAN),
                            AccountId = loanRepayment.LoanApplication.AccountId
                        };
                        await transactionService.MakeTransactionWithinTheBankAsync(model, loanRepayment.LoanApplication.CustomerId);
                        var notification = new Infrastructure.Data.DataModels.Notification
                        {
                            Title = "Погасяване на кредит",
                            CustomerId = loanRepayment.LoanApplication.CustomerId,
                            Content = $"Успешно погасихте кредита си от сметка {loanRepayment.LoanApplication.Account.Name} и този месец!",
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
