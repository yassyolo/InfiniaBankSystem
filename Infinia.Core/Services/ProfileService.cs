﻿using Infinia.Core.Contracts;
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
            return await repository.AllReadOnly<Account>()
                .AnyAsync(x => x.EncryptedIBAN == encryptionService.Encrypt(accountIBAN)
                          && x.CustomerId == userId);
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
            return await repository.AllReadOnly<Customer>()
                .AnyAsync(x => x.IdentityCard.EncryptedCardNumber == encryptionService.Encrypt(model.IdentityCardNumber) &&
                               x.IdentityCard.EncryptedIssuer == encryptionService.Encrypt(model.IdentityCardIssuer) &&
                               x.IdentityCard.EncryptedDateOfIssue == encryptionService.Encrypt(model.IdentityCardIssueDate.ToString("dd.MM.yyyy")) &&
                               x.IdentityCard.EncryptedSex == encryptionService.Encrypt(model.IdentityCardSex) &&
                               x.IdentityCard.EncryptedNationality == encryptionService.Encrypt(model.IdentityCardNationality) &&
                               x.IdentityCard.EncryptedSSN == encryptionService.Encrypt(model.SSN) &&
                               x.Id == userId);
        }

        public async Task<bool> CustomerWithIdentityCardNumberExists(string identityCardNumber, string userId)
        {
            var identityCardNumberEncrypted = encryptionService.Encrypt(identityCardNumber);
            var identityCard = await repository.All<IdentityCard>()
                .FirstOrDefaultAsync(i => i.EncryptedCardNumber == identityCardNumberEncrypted);
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

        private string ObfuscateEmail(string email)
        {
            int index= email.IndexOf('@');
            return email.Substring(0, 2) + new string('*', index - 2) + email.Substring(index);
        }
    }
}
