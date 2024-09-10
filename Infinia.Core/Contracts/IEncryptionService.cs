namespace Infinia.Core.Contracts
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string text);
        string Decrypt(byte[] encryptedText);
    }
}
