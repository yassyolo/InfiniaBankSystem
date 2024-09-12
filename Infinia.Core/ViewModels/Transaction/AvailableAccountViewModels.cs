
namespace Infinia.Core.ViewModels.Transaction
{
    public class AvailableAccountViewModels
    {
        public string IBAN { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
    }
}
