using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static Infinia.Core.MessageConstants.ErrorMessages;
using System.Threading.Tasks;

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
