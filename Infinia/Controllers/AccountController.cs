using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Account;
using Infinia.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Infinia.Core.MessageConstants.ErrorMessages;

namespace Infinia.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IProfileService profileService;

        public AccountController(IAccountService accountService, IProfileService profileService)
        {
            this.profileService = profileService;
            this.accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var model = await accountService.GetAccountsForCustomerAsync(userId);
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateAccount()
        {
            var model = new CreateAccountViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            if (model.Balance < 0)
            {
                ModelState.AddModelError("Balance", NegativeBalanceErrorMessage);
                return View(model);
            }
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            if (await profileService.CustomerWithIdentityCardNumberExists(model.IdentityCardNumber, userId) == false)
            {
                ModelState.AddModelError("IdentityCardNumber", InvalidIdentityCardNumberErrorMessage);
                return View(model);
            }
            await accountService.CreateAccountAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ChangeAccountName(int id)
        {
            if (await accountService.AccountWithIdExistsAsync(id) == false)
            {
                return BadRequest();
            }
            var userId = User.GetId();
            if (await accountService.AccountBelongsToUserAsync(id, userId) == false)
            {
                return BadRequest();
            }
            var model = await accountService.GetAccountNameAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAccountName(int id, ChangeAccountNameViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            if (await accountService.AccountWithIdExistsAsync(id) == false)
            {
                return BadRequest();
            }
            var userId = User.GetId();  
            if (await accountService.AccountBelongsToUserAsync(id, userId) == false)
            {
                return BadRequest();
            }
            await accountService.ChangeAccountNameAsync(id, model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await accountService.AccountWithIdExistsAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await accountService.GetAccountForDeleteAsync(id);
            var savingsAccount = await accountService.GetSavingsAccountAsync(model.IBAN);

            if (model.Balance >= 0 || model.Balance < 0 || savingsAccount.Balance >= 0 || savingsAccount.Balance < 0)
            {
                TempData["InvalidOperation"] = InvalidAccountDeletion;
                return View(model);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, DeleteAccountViewModel model)
        {
            if (await accountService.AccountWithIdExistsAsync(id) == false)
            {
                return BadRequest();
            }
            await accountService.DeleteAccountAsync(model.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await accountService.AccountWithIdExistsAsync(id) == false)
            {
                return BadRequest();
            }
            var userId = User.GetId();
            if (await accountService.AccountBelongsToUserAsync(id, userId) == false)
            {
                return BadRequest();
            }
            var model = await accountService.GetAccountDetailsAsync(id);
            return View(model);
        }
    }
}
