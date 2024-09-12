using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Core.ViewModels.Account;
using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text;



namespace Infinia.Tests
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private IEncryptionService encryptionService;

        [SetUp]
        public void Setup()
        {
            var inMemoryConfiguration = new Dictionary<string, string>
            {
                {"Encryption:Key", Convert.ToBase64String(Encoding.UTF8.GetBytes("1234567890123456"))}
            };
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemoryConfiguration).Build();
            encryptionService = new EncryptionService(configuration);
        }
        [Test]
        public void DecryptSfouldReturnOriginalText()
        {
            var text = "Test";
            var encryptedText = encryptionService.Encrypt(text);
            var decryptedText = encryptionService.Decrypt(encryptedText);
            Assert.AreEqual(text, decryptedText);
        }
        [Test]
        public void EncryptShouldEncryptText()
        {
            string plainText = "Test";
            byte[] encryptedBytes = encryptionService.Encrypt(plainText);

            Assert.IsNotNull(encryptedBytes);
            Assert.IsTrue(encryptedBytes.Length > 0);
        }

    }
}