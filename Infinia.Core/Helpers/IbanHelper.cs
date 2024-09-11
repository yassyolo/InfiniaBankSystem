using System.Numerics;
using System.Text;

namespace Infinia.Core.Helpers
{
    public static class IbanHelper
    {
        private const string IbanCountryCode = "BG";
        private const int AccountNumberLength = 10; 
        private const string BankCode = "INFN";
        private static readonly Random Random = new Random();

        public static string GenerateIban()
        {
            string accountNumber = GenerateRandomNumber();
            string bankCodePadded = BankCode.PadRight(4, '0');
            string ibanWithoutCheckDigits = $"{IbanCountryCode}{bankCodePadded}{accountNumber}";
            string checkDigits = CalculateCheckDigits(ibanWithoutCheckDigits);
            return $"{IbanCountryCode}{checkDigits}{bankCodePadded}{accountNumber}";
        }

        public static string GenerateRandomNumber()
        {
            StringBuilder sb = new StringBuilder(AccountNumberLength);

            for (int i = 0; i < AccountNumberLength; i++)
            {
                sb.Append(Random.Next(0, 10));
            }
            return sb.ToString();
        }

        private static string CalculateCheckDigits(string ibanWithoutCheckDigits)
        {
            string rearrangedIban = ibanWithoutCheckDigits.Substring(4) + ibanWithoutCheckDigits.Substring(0, 4);
            string ibanAsDigits = string.Concat(rearrangedIban.Select(x =>
                char.IsDigit(x) ? x.ToString() : (x - 'A' + 10).ToString()));

            BigInteger ibanNumber = BigInteger.Parse(ibanAsDigits);
            int checkDigits = (int)(98 - (ibanNumber % 97));

            return checkDigits.ToString("D2");
        }
    }
}

