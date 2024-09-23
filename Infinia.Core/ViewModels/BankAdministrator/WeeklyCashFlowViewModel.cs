using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class WeeklyCashFlowViewModel
    {
        public string Date { get; set; } 
        public decimal TransactionFees { get; set; }
        public decimal LoanRepayments { get; set; }
        public decimal LoanDisbursements { get; set; }
        public decimal AccountMaintenanceFees { get; set; }
    }
}
