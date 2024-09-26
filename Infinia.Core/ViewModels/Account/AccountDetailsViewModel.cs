namespace Infinia.Core.ViewModels.Account
{
    public class AccountDetailsViewModel
    {
        public int Id { get; set; }
        public string IBAN { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Balance { get; set; } = string.Empty;
        public string CreationDate { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal MonthlyFee { get; set; }
    }
}
