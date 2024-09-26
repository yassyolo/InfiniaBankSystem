using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class GenderAnalysisRowViewModel
    {
            public string Gender { get; set; }
            public double AverageTransactionAmount { get; set; }
            public double TotalTransactionAmount { get; set; }
            public double LoanAmountRequested { get; set; }
            public double LoanAmountApproved { get; set; }
            public double AccountBalance { get; set; }
        

    }
}
