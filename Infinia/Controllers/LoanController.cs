﻿using Infinia.Core.Contracts;
using Infinia.Core.ViewModels;
using Infinia.Core.ViewModels.Loan;
using static Infinia.Core.MessageConstants.ErrorMessages;
using Microsoft.AspNetCore.Mvc;
using Infinia.Extensions;

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
        public async Task<IActionResult> ApplyForLoan(string type, double rate)
        {
            /*var model = new LoanApplicationViewModel
            {
                Type = type,
                InterestRate = rate
            };*/
            return View();
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
            return RedirectToAction(nameof(LoanApplicationHistory));
        }

        private object LoanApplicationHistory()
        {
            throw new NotImplementedException();
        }
    }
}
