namespace Infinia.Core.ViewModels.Account
{
    public class DeleteAccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IBAN { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public int SavingsId { get; set; }
        public string SavingsName { get; set; } = string.Empty;
        public string SavingsIBAN { get; set; } = string.Empty;
        public string SavingsType { get; set; } = string.Empty;
        public decimal SavingsBalance { get; set; }
    }
}
