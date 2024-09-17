using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Infinia.Core.Constants.TransactionTypeConstants;
using static Infinia.Core.Constants.TransactionFeeConstants;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Transaction;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants;

namespace Infinia.Core.Services
{
    public class LoanMonthlyRepaymentService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITransactionService transactionService;
        private readonly IEncryptionService encryptionService;

        public LoanMonthlyRepaymentService(IServiceProvider serviceProvider, ITransactionService transactionService, IEncryptionService encryptionService)
        {
            this.serviceProvider = serviceProvider;
            this.transactionService = transactionService;
            this.encryptionService = encryptionService;
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
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var bankAccount = dbContext.Accounts.FirstOrDefault(x => x.Name == "Bank account");
                var loanRepayments = dbContext.LoanRepayments.ToList();
                var currentDateNumber = DateTime.UtcNow.Day;
                foreach (var loanRepayment in loanRepayments)
                {
                    if (loanRepayment.LoanApplication.LoanRepaymentNumber == currentDateNumber)
                    {
                        var model = new TransactionWithinTheBankViewModel()
                        {
                            Reason = "Loan repayment",
                            Description = $"Loan repayment made on {DateTime.UtcNow.ToString()}",
                            ReceiverName = "Bank account",
                            Amount = loanRepayment.RepaymentAmount,
                            ReceiverIBAN = encryptionService.Decrypt(bankAccount.EncryptedIBAN),
                            AccountId = loanRepayment.LoanApplication.AccountId
                        };
                        await transactionService.MakeTransactionWithinTheBankAsync(model, loanRepayment.LoanApplication.CustomerId);
                        var notification = new Infrastructure.Data.DataModels.Notification
                        {
                            CustomerId = loanRepayment.LoanApplication.CustomerId,
                            Content = $"Monthly loan payment and transaction tax was deducted from your account with name {loanRepayment.LoanApplication.Account.Name}.",
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
