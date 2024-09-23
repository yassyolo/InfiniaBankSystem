
using Infinia.Core.ViewModels.Transaction;

namespace Infinia.Core.Contracts
{
    public interface ITransactionService
    {
        Task MakeMonthlyFeeDeductionTransactionAsync(TransactionWithinTheBankViewModel model, string userId);
        Task<bool> CustomerWithNameAndIBANExistsAsync(string receiverIBAN, string receiverName);
        Task<IEnumerable<AvailableAccountViewModels>?> GetAvailableAccountsAsync(string userId);
        Task<TransactionHistoryViewModel?> GetTransactionsForAccountAsync(int id);
        Task MakeTransactionBetweenMyAccountsAsync(TransactionBetweenMyAccountsViewModel model, string userId);
        Task MakeTransactionToAnotherBankAsync(TransactionToAnotherBankViewModel model, string userId);
        Task MakeTransactionWithinTheBankAsync(TransactionWithinTheBankViewModel model, string userId);
        Task<string> GenerateCSVFileForTransactionHistory(int id);
        Task<IEnumerable<AvailableAccountViewModels>> GetAvailableCurrentAccountsForUserAsync(string userId);
    }
}
