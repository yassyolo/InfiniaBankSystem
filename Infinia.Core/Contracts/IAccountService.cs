using Infinia.Core.ViewModels.Account;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Core.Contracts
{
    public interface IAccountService
    {
        Task<bool> AccountWithIdExistsAsync(int id);
        Task<bool> AmountGreaterThanSenderAccountBalance(int accountIdFromWhichWeWantToSendMoney, decimal amount);
        Task ChangeAccountNameAsync(int id, ChangeAccountNameViewModel model);
        Task CreateAccountAsync(CreateAccountViewModel model, string userId);
        Task DeleteAccountAsync(int id);
        Task<AccountDetailsViewModel?> GetAccountDetailsAsync(int id);
        Task<DeleteAccountViewModel?> GetAccountForDeleteAsync(int id);
        Task<ChangeAccountNameViewModel?> GetAccountNameAsync(int id);
        Task<AccountIndexViewModel?> GetAccountsForCustomerAsync(string userId);
        Task<Account> GetSavingsAccountAsync(string iBAN);
    }
}
