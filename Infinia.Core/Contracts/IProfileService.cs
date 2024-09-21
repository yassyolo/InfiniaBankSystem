
using Infinia.Core.ViewModels;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Core.Contracts
{
    public interface IProfileService
    {
        Task<bool> CustomerWithAccountIBANExists(string accountIBAN, string userId);
        Task<bool> CustomerWithAddressExists(LoanApplicationViewModel model, string userId);
        Task<bool> CustomerWithIdentityCardExists(LoanApplicationViewModel model, string userId);
        Task<bool> CustomerWithIdentityCardNumberExists(string identityCardNumber, string userId);
        Task<bool> CustomerWithIdExistsAsync(string userId);
        Task<ProfileDetailsViewModel?> GetProfileDetailsAsync(string userId);
        Task<Customer> ReturnCustomerAsync(RegisterViewModel model);
    }
}
