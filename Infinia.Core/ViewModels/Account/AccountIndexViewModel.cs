namespace Infinia.Core.ViewModels.Account
{
    public class AccountIndexViewModel
    {
        public IEnumerable<AccountRowViewModel> Accounts { get; set; } = new List<AccountRowViewModel>();

        public decimal TotalBalance { get; set; }
    }
}
