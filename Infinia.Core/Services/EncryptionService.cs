using Infinia.Core.Contracts;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Infinia.Core.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] key;

        public EncryptionService(byte[] key)
        {
            this.key = key;
        }

        public EncryptionService(IConfiguration config)
        {
            string encryptionKey = config["Encryption:Key"];
            key = Convert.FromBase64String(encryptionKey);
        }

        public byte[] Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);

                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(text);
                        }
                    }
                    return ms.ToArray();
                }
            }
        }

        public string Decrypt(byte[] encryptedText)
        {
            if (encryptedText == null || encryptedText.Length < 16)
            {
                throw new ArgumentException("Invalid encrypted data");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                byte[] iv = new byte[16];
                byte[] ciphertext = new byte[encryptedText.Length - iv.Length];

                Array.Copy(encryptedText, 0, iv, 0, iv.Length);
                Array.Copy(encryptedText, iv.Length, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(ciphertext))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

