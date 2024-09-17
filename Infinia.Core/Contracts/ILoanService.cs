using Infinia.Core.ViewModels;
using Infinia.Core.ViewModels.Loan;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> origin/main

namespace Infinia.Core.Contracts
{
    public interface ILoanService
    {
        Task ApplyForLoanAsync(LoanApplicationViewModel model, string userId);
<<<<<<< HEAD
        Task<IEnumerable<CurrentLoanViewModel>?> GetCurrentLoansForCustomerAsync(string userId);
        Task<LoanApplicationHistoryViewModel?> GetLoanApplicationDetailsAsync(int id, string userId);
        Task<IEnumerable<LoanApplicationHistoryViewModel>?> GetLoanApplicationHistoryForCustomerAsync(string userId);
        ChooseLoanTypeViewModel? GetLoanTypesAsync();
        Task<bool> LoanApplicationExistsAsync(int id, string userId);
=======
        ChooseLoanTypeViewModel? GetLoanTypesAsync();
>>>>>>> origin/main
    }
}
