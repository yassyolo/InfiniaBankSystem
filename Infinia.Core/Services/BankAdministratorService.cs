using Infinia.Core.Constants;
using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.BankAdministrator;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Core.Services
{
    public class BankAdministratorService : IBankAdministratorService
    {
        private readonly IRepository repository;

        public BankAdministratorService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CombinedBranchAnalysisStatisticsViewModel> GetBranchAnalysisAsync(string branchName, DateTime startDate, DateTime endDate)
        {
            var allTimeStatistics = new BranchAnalysisStatisticsViewModel();
            allTimeStatistics.TotalAccounts = await repository.AllReadOnly<Account>().CountAsync(x => x.Branch == branchName);
            allTimeStatistics.TotalBalance = await repository.AllReadOnly<Account>().Where(x => x.Branch == branchName).SumAsync(x => x.Balance);
            allTimeStatistics.OpenAccounts = await repository.AllReadOnly<Account>().CountAsync(x => x.Branch == branchName && x.Status == "Open");
            allTimeStatistics.ClosedAccounts = await repository.AllReadOnly<Account>().CountAsync(x => x.Branch == branchName && x.Status == AccountStatusConstants.Closed);
            allTimeStatistics.AverageBalance = allTimeStatistics.TotalBalance / allTimeStatistics.TotalAccounts;
            allTimeStatistics.TotalTransactionVolume = await repository.AllReadOnly<Transaction>().Where(x => x.Account.Branch == branchName).SumAsync(x => x.Amount);

            var selectedIntervalStatistics = new BranchAnalysisStatisticsViewModel();
            selectedIntervalStatistics.TotalAccounts = await repository.AllReadOnly<Account>().CountAsync(x => x.Branch == branchName && x.CreationDate >= startDate && x.CreationDate <= endDate) + 1;
            selectedIntervalStatistics.TotalBalance = await repository.AllReadOnly<Account>().Where(x => x.Branch == branchName && x.CreationDate >= startDate && x.CreationDate <= endDate).SumAsync(x => x.Balance);
            selectedIntervalStatistics.OpenAccounts = await repository.AllReadOnly<Account>().CountAsync(x => x.Branch == branchName && x.Status == AccountStatusConstants.Open && x.CreationDate >= startDate && x.CreationDate <= endDate);
            selectedIntervalStatistics.ClosedAccounts = await repository.AllReadOnly<Account>().CountAsync(x => x.Branch == branchName && x.Status == AccountStatusConstants.Closed && x.CreationDate >= startDate && x.CreationDate <= endDate);
            selectedIntervalStatistics.AverageBalance = selectedIntervalStatistics.TotalBalance / selectedIntervalStatistics.TotalAccounts;
            selectedIntervalStatistics.TotalTransactionVolume = await repository.AllReadOnly<Transaction>().Where(x => x.Account.Branch == branchName && x.TransactionDate >= startDate && x.TransactionDate <= endDate).SumAsync(x => x.Amount);
            return new CombinedBranchAnalysisStatisticsViewModel
            {
                AllTimeStatistics = allTimeStatistics,
                SelectedIntervalStatistics = selectedIntervalStatistics
            };
        }

        public async Task<CashFlowCombinedWeeklyData> GetHistoricalDataAsync(string branchName)
        {
            var today = DateTime.UtcNow;
            var model = new CashFlowCombinedWeeklyData();
            for (int i = 0; i < 6; i++)
            {
                var startDate = today.AddDays(-7 * i);
                var endDate = startDate.AddDays(7);
                model.HistoricalData.Add(new WeeklyCashFlowViewModel
                {
                    Date = startDate.ToString("yyyy-MM-dd"),
                    AccountMaintenanceFees = await repository.AllReadOnly<Transaction>().Where(x => x.Account.Branch == branchName && x.TransactionDate >= startDate && x.TransactionDate <= endDate && x.Reason == "Monthly fee deduction").SumAsync(x => x.Amount),
                    LoanRepayments = await repository.AllReadOnly<Transaction>().Where(x => x.Account.Branch == branchName && x.TransactionDate >= startDate && x.TransactionDate <= endDate && x.Reason == "Loan repayment").SumAsync(x => x.Amount),
                    TransactionFees = await repository.AllReadOnly<Transaction>().Where(x => x.Account.Branch == branchName && x.TransactionDate >= startDate && x.TransactionDate <= endDate).SumAsync(x => x.Fee),
                    LoanDisbursements = await repository.AllReadOnly<LoanApplication>().Where(x => x.Account.Branch == branchName && x.ApplicationDate >= startDate && x.ApplicationDate <= endDate && x.Status == "Approved").SumAsync(x => x.LoanAmount),
                });

            }
            return model;
        }
    }
}
