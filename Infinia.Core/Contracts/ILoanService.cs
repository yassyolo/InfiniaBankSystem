using Infinia.Core.ViewModels;
using Infinia.Core.ViewModels.Loan;


namespace Infinia.Core.Contracts
{
    public interface ILoanService
    {
        Task ApplyForLoanAsync(LoanApplicationViewModel model, string userId);
        Task<IEnumerable<CurrentLoanViewModel>?> GetCurrentLoansForCustomerAsync(string userId);
        Task<LoanApplicationHistoryViewModel?> GetLoanApplicationDetailsAsync(int id, string userId);
        Task<IEnumerable<LoanApplicationHistoryViewModel>?> GetLoanApplicationHistoryForCustomerAsync(string userId);
        Task GetMissingValuesForLoanApplicationAsync(LoanApplicationViewModel model);
        Task<bool> LoanApplicationExistsAsync(int id, string userId);
    }
}
