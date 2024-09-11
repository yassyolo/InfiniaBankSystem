namespace Infinia.Core.ViewModels.Account
{
    public class DeleteAccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IBAN { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Balance { get; set; } 
    }
}
