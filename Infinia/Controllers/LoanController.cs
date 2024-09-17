using Infinia.Core.Contracts;
using Infinia.Core.ViewModels;
using Infinia.Core.ViewModels.Loan;
using static Infinia.Core.MessageConstants.ErrorMessages;
using Microsoft.AspNetCore.Mvc;
using Infinia.Extensions;
<<<<<<< HEAD
using Microsoft.AspNetCore.Http.Metadata;
=======
>>>>>>> origin/main

namespace Infinia.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService loanService;
        private readonly IProfileService profileService;

        public LoanController(ILoanService loanService, IProfileService profileService)
        {
            this.loanService = loanService;
            this.profileService = profileService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChooseLoanType()
        {
            var model = loanService.GetLoanTypesAsync();
            return View(model);
        }

        [HttpGet]
<<<<<<< HEAD
        public IActionResult ApplyForLoan(string type, double rate)
        {
            var model = new LoanApplicationViewModel
            {
                Type = type,
                InterestRate = rate
            };
            return View(model);
=======
        public async Task<IActionResult> ApplyForLoan(string type, double rate)
        {
            /*var model = new LoanApplicationViewModel
            {
                Type = type,
                InterestRate = rate
            };*/
            return View();
>>>>>>> origin/main
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyForLoan(LoanApplicationViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            if (await profileService.CustomerWithIdentityCardExists(model, userId) == false)
            {
                ModelState.AddModelError("", InavlidCustomerIdentityCardErrorMessage);
                return View(model);
            }
            if (await profileService.CustomerWithAddressExists(model, userId) == false)
            {
                ModelState.AddModelError("", InavlidCustomerAddressErrorMessage);
                return View(model);
            }
            if (await profileService.CustomerWithAccountIBANExists(model.AccountIBAN, userId) == false)
            {
                ModelState.AddModelError("AccountIBAN", InavlidCustomerAccountIBANErrorMessage);
                return View(model);
            }
            await loanService.ApplyForLoanAsync(model, userId);
<<<<<<< HEAD
            return RedirectToAction(nameof(LoanApplicationsHistory));
        }

        [HttpGet]
        public async Task<IActionResult> LoanApplicationsHistory()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var model = await loanService.GetLoanApplicationHistoryForCustomerAsync(userId);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LoanApplication(int id)
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            if (await loanService.LoanApplicationExistsAsync(id, userId) == false)
            {
                return BadRequest();
            }
            var model = await loanService.GetLoanApplicationDetailsAsync(id, userId);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CurrentLoans()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var model = await loanService.GetCurrentLoansForCustomerAsync(userId);
            return View(model);
        }

=======
            return RedirectToAction(nameof(LoanApplicationHistory));
        }

        private object LoanApplicationHistory()
        {
            throw new NotImplementedException();
        }
>>>>>>> origin/main
    }
}
