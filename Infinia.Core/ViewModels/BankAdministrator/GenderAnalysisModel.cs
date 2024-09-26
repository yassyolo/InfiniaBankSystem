using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class GenderAnalysisModel
    {
        public List<string> Gender { get; set; }
        public List<int> TransactionFrequency { get; set; }
        public List<decimal> AverageTransactionAmount { get; set; }
        public List<decimal> TotalTransactionAmount { get; set; }
        public List<int> LoanAmountRequested { get; set; }
        public List<int> LoanAmountApproved { get; set; }
        public List<int> AccountBalance { get; set; }
    }
}
