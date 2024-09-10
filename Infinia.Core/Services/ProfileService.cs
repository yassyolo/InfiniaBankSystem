using Infinia.Core.Contracts;
using Infinia.Core.ViewModels;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository repository;
        private readonly IEncryptionService encryptionService;

        public ProfileService(IRepository repository, IEncryptionService encryptionService)
        {
            this.repository = repository;
            this.encryptionService = encryptionService;
        }

        public async Task<bool> CustomerWithIdExistsAsync(string userId)
        {
            return await repository.All<Customer>().AnyAsync(c => c.Id == userId);
        }

        public async Task<ProfileDetailsViewModel?> GetProfileDetailsAsync(string userId)
        {
            return await repository.All<Customer>()
                .Where(x => x.Id == userId)
                .Select(x => new ProfileDetailsViewModel
                {
                    Name = x.Name,
                    Email = ObfuscateEmail(x.Email),
                    Username = x.PhoneNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> ReturnCustomerAsync(RegisterViewModel model)
        {
            var address = new Address
            {
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                Street = model.Street
            };
            await repository.AddAsync(address);
            await repository.SaveChangesAsync();
            var identityCard = new IdentityCard
            {
                EncryptedCardNumber = encryptionService.Encrypt(model.IdentityCardNumber),
                EncryptedIssuer = encryptionService.Encrypt(model.IdentityCardIssuer),
                EncryptedDateOfIssue = encryptionService.Encrypt(model.IdentityCardIssueDate.ToString("dd.MM.yyyy")),
                EncryptedSex = encryptionService.Encrypt(model.IdentityCardSex),
                EncryptedNationality = encryptionService.Encrypt(model.IdentityCardNationality)
            };
            await repository.AddAsync(identityCard);
            await repository.SaveChangesAsync();
            return new Customer
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                AddressId = address.Id,
                IdentityCardId = identityCard.Id
            };
        }

        private string ObfuscateEmail(string email)
        {
            int index= email.IndexOf('@');
            return email.Substring(0, 2) + new string('*', index - 2) + email.Substring(index);
        }
    }
}
