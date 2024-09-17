using Infinia.Core.ViewModels;
using Infinia.Core.ViewModels.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinia.Core.Contracts
{
    public interface ILoanService
    {
        Task ApplyForLoanAsync(LoanApplicationViewModel model, string userId);
        ChooseLoanTypeViewModel? GetLoanTypesAsync();
    }
}
