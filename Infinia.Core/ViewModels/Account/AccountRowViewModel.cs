namespace Infinia.Core.ViewModels.Account
{
    public class AccountRowViewModel
    {
        public int Id { get; set; }

        public string IBAN { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public decimal Balance { get; set; }
        //TODO: Add transactions
    }
}
