using Infinia.Infrastructure.Data.DataModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinia.Infrastructure.SeedDb
{
    public class SeedData
    {
        public Customer Customer { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public IdentityCard IdentityCard { get; set; } = null!;
        public Account Account1 { get; set; } = null!;
        public Account SavingsAccountForAccount1 { get; set; } = null!;
        public Account Account2 { get; set; } = null!;
        public Account SavingsAccountForAccount2 { get; set; } = null!;
        public Account BankAccount { get; set; } = null!;
        public Account SavingAccountForBankAccount { get; set; } = null!;
        public SeedData()
        {
            SeedAddress();
            SeedIdentityCard();
            SeedCustomer();
            SeedAccounts();
        }

        private void SeedAddress()
        {
            Address = new Address
            {
                Id = 1,
                City = "Sliven",
                Country = "Bulgaria",
                Street = "Sini kamani 28",
                PostalCode = "8800"
            };
        }


        private void SeedIdentityCard()
        {
            IdentityCard = new IdentityCard
            {
                Id = 1,
                EncryptedCardNumber = HexStringToByteArray("3122431D85A0CAC491B5C072E5713A16C1320402D2154B9A8C66A31C565C9749"),
                EncryptedSex = HexStringToByteArray("8BAFADF77FF4F89301D3946E02893FB6BE037A12BD37A29718110EF974624C8B"),
                EncryptedDateOfIssue = HexStringToByteArray("B35575EF425C3C614603C4DF5C705545AFADEB2A3C46B0A6D629FC9C8B6D8C82"),
                EncryptedIssuer = HexStringToByteArray("9E72A98B940715462F0E7B24C9ACCBE63AC8430F7FF9E89ACBED84502FDE5AFA"),
                EncryptedNationality = HexStringToByteArray("863743A4302CD2FE4EDAE304066198B4EA6A8DF74A0D271ADBADA1EE3DB7A9DD")
            };
        }
        private void SeedCustomer()
        {
            var hasher = new PasswordHasher<Customer>();
            Customer = new Customer
            {
                Id = "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                Name = "Ivan Ivanov",
                AddressId = Address.Id,
                IdentityCardId = IdentityCard.Id,
                EmailConfirmed = true,
                UserName = "ivanivanov",
                NormalizedUserName = "IVANIVANOV",
                Email = "ivanivanov@gmail.com",
                NormalizedEmail = "IVANIBANOV@GMAIL.COM",
                PasswordHash = hasher.HashPassword(Customer, "Ivan.Ivanov123"),
            };
        }
        private void SeedAccounts()
        {
            var date = DateTime.Now;
            var ibanForAccount1 = HexStringToByteArray("251827C2BB551943C338F78911DC20519052B81C13FD60B1FF6B183A1446DF97B178287FD07FF9364BDCB4C203C18BBD");
            var dateForAccount2 = DateTime.Now.AddDays(-20);
            var ibanForAccount2 = HexStringToByteArray("C10A8C677119F049BD8354F085338678FB69A037B9A1FDAF1FD13A70A9EE89FAC42730BEBB2A47177D90753245E00C7A");
            var dateForBankAccount = DateTime.Now.AddDays(-30);
            var ibanForBankAccount = HexStringToByteArray("1FCE6D878D47D17E2A65E7EC43ABF512645CD2DA4C9F654F0E83049D5BB4A29880DD6BE3C2A77EB04C9BA415E649DD0D");

            Account1 = new Account
            {
                Id = 3,
                Balance = 1000,
                CustomerId = Customer.Id,
                Type = "Current",
                Name = "Current acount",
                CreationDate = date,
                Status = "Open",
                MonthlyFee = 2,
                EncryptedIBAN = ibanForAccount1
            };
            SavingsAccountForAccount1 = new Account
            {
                Id = 4,
                Balance = 200,
                CustomerId = Customer.Id,
                Type = "Savings",
                Name = "Savings account",
                CreationDate = date,
                Status = "Open",
                MonthlyFee = 2,
                EncryptedIBAN = ibanForAccount1
            };  
            Account2 = new Account
            {
                Id = 5,
                Balance = 900,
                CustomerId = Customer.Id,
                Type = "Current",
                Name = "Current acount-2",
                CreationDate = dateForAccount2,
                Status = "Open",
                MonthlyFee = 2,
                EncryptedIBAN = ibanForAccount2
            };
            SavingsAccountForAccount2 = new Account
            {
                Id = 6,
                Balance = 100,
                CustomerId = Customer.Id,
                Type = "Savings",
                Name = "Savings account-2",
                CreationDate = dateForAccount2,
                Status = "Open",
                MonthlyFee = 2,
                EncryptedIBAN = ibanForAccount2
            };
            BankAccount = new Account
            {
                Id = 1,
                Balance = 100000000,
                CustomerId = Customer.Id,
                Type = "Bank",
                Name = "Bank account",
                CreationDate = dateForBankAccount,
                Status = "Open",
                MonthlyFee = 0,
                EncryptedIBAN = ibanForBankAccount
            };
            SavingAccountForBankAccount = new Account
            {
                Id = 2,
                Balance = 0,
                CustomerId = Customer.Id,
                Type = "Savings",
                Name = "Savings account for bank account",
                CreationDate = dateForBankAccount,
                Status = "Open",
                MonthlyFee = 0,
                EncryptedIBAN =  ibanForBankAccount
            };
        }
        private static byte[] HexStringToByteArray(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
