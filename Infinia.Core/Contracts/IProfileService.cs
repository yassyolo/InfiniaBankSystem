
using Infinia.Core.ViewModels;
using Infinia.Infrastructure.Data.DataModels;

namespace Infinia.Core.Contracts
{
    public interface IProfileService
    {
        Task<bool> CustomerWithIdentityCardNumberExists(string identityCardNumber, string userId);
        Task<bool> CustomerWithIdExistsAsync(string userId);
        Task<ProfileDetailsViewModel?> GetProfileDetailsAsync(string userId);
        Task<Customer> ReturnCustomerAsync(RegisterViewModel model);
    }
}
