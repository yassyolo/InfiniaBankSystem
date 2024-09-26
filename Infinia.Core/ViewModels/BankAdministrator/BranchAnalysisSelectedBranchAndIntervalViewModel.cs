namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class BranchAnalysisSelectedBranchAndIntervalViewModel
    {
        //[Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string BranchName { get; set; } = string.Empty;

        //[Required(ErrorMessage = RequiredFieldErrorMessage)]
        public DateTime StartInterval { get; set; }

        //[Required(ErrorMessage = RequiredFieldErrorMessage)]
        public DateTime EndInterval { get; set;}

    }
}
