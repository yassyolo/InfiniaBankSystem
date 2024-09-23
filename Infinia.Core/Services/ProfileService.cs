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

        public async Task<bool> CustomerWithAccountIBANExists(string accountIBAN, string userId)
        {
            var account = await repository.AllReadOnly<Account>().Where(x => x.CustomerId == userId)
                .Select(x => new
                {
                   EncryptedIBAN = x.EncryptedIBAN
                }).FirstOrDefaultAsync();

            var decryptedIBAN = encryptionService.Decrypt(account.EncryptedIBAN);

            return decryptedIBAN == accountIBAN;
        }

        public async Task<bool> CustomerWithAddressExists(LoanApplicationViewModel model, string userId)
        {
            return await repository.AllReadOnly<Customer>()
                .AnyAsync(x => x.Address.City == model.City &&
                               x.Address.Country == model.Country &&
                               x.Address.PostalCode == model.PostalCode &&
                               x.Address.Street == model.Street &&
                               x.Id == userId);
        }

        public async Task<bool> CustomerWithIdentityCardExists(LoanApplicationViewModel model, string userId)
        {
            var customer = await repository.AllReadOnly<Customer>()
                .Where(x => x.Id == userId)
                .Select(x => new 
                {
                    EncryptedCardNumber = x.IdentityCard.EncryptedCardNumber,
                    EncryptedIssuer = x.IdentityCard.EncryptedIssuer,
                    EncryptedSex = x.IdentityCard.EncryptedSex,
                    EncryptedNationality = x.IdentityCard.EncryptedNationality,
                    EncryptedSSN = x.IdentityCard.EncryptedSSN
                })
                .FirstOrDefaultAsync();

            var decryptedCardNumber = encryptionService.Decrypt(customer.EncryptedCardNumber);
            var decryptedIssuer = encryptionService.Decrypt(customer.EncryptedIssuer);
            var decryptedSex = encryptionService.Decrypt(customer.EncryptedSex);
            var decryptedNationality = encryptionService.Decrypt(customer.EncryptedNationality);
            var decryptedSSN = encryptionService.Decrypt(customer.EncryptedSSN);

            return decryptedCardNumber == model.IdentityCardNumber &&
                   decryptedIssuer == model.IdentityCardIssuer &&
                   decryptedSex == model.IdentityCardSex &&
                   decryptedNationality == model.IdentityCardNationality &&
                   decryptedSSN == model.SSN;
        }

        public async Task<bool> CustomerWithIdentityCardNumberExists(string identityCardNumber, string userId)
        {
            var identityCards = await repository.All<IdentityCard>().ToListAsync();
            var identityCard = identityCards.FirstOrDefault(i => encryptionService.Decrypt(i.EncryptedCardNumber) == identityCardNumber);

            if (identityCard == null)
            {
                return false;
            }

            return await repository.All<Customer>()
                .AnyAsync(x => x.IdentityCardId == identityCard.Id && x.Id == userId);
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
                    Username = x.UserName
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
                EncryptedNationality = encryptionService.Encrypt(model.IdentityCardNationality),
                EncryptedSSN = encryptionService.Encrypt(model.SSN)
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

        private static string ObfuscateEmail(string email)
        {
            int index= email.IndexOf('@');
            return email.Substring(0, 2) + new string('*', index - 2) + email.Substring(index);
        }

        public async Task<bool> UserWithEmailExistsAsync(string email)
        {
            return await repository.AllReadOnly<Customer>().AnyAsync(x => x.Email == email);
        }

        public Task<bool> UserWithUsernameExistsAsync(string username)
        {
            return repository.AllReadOnly<Customer>().AnyAsync(x => x.UserName == username);
        }
    }
}
