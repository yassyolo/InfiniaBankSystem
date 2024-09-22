using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Core.ViewModels.Transaction;
using Infinia.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Transaction;

namespace Infinia.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IProfileService profileService;
        private readonly IAccountService accountService;

        public TransactionController(ITransactionService transactionService, IProfileService profileService, IAccountService accountService)
        {
            this.transactionService = transactionService;
            this.profileService = profileService;
            this.accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MakeTransactionBetweenMyAccounts()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var availableAccounts = await transactionService.GetAvailableAccountsAsync(userId);
            var model = new TransactionBetweenMyAccountsViewModel()
            {
                AvailableAccounts = availableAccounts
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeTransactionBetweenMyAccounts(TransactionBetweenMyAccountsViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            if (model.AccountIdFromWhichWeWantToSendMoney == model.AccountIdFromWhichWeWantToReceiveMoney) 
            { 
                ModelState.AddModelError("AccountIdFromWhichWeWantToReceiveMoney", InvalidReceivalAccountErrorMessage);
                return View(model);
            }
            if (model.Amount < AmountMinValue || model.Amount > AmountMaxValue )
            {
                ModelState.AddModelError("Amount", InvalidAmountErrorMessage);
                return View(model);
            }
            if (await accountService.AmountGreaterThanSenderAccountBalance(model.AccountIdFromWhichWeWantToSendMoney, model.Amount) == true)
            {
                ModelState.AddModelError("AccountIdFromWhichWeWantToSendMoney", AmountGreaterThanSenderAccountBalanceErrorMessage);
                return View(model);
            }
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            try
            {
                await transactionService.MakeTransactionBetweenMyAccountsAsync(model, userId);
                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> MakeTransactionWithinTheBank()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var availableAccounts = await transactionService.GetAvailableCurrentAccountsForUserAsync(userId);
            var model = new TransactionWithinTheBankViewModel();
            model.AvailableAccounts = availableAccounts;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeTransactionWithinTheBank(TransactionWithinTheBankViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            if (model.Amount < AmountMinValue || model.Amount > AmountMaxValue)
            {
                ModelState.AddModelError("Amount", InvalidAmountErrorMessage);
                return View(model);
            }
            if (await accountService.AmountGreaterThanSenderAccountBalance(model.AccountId, model.Amount) == true)
            {
                ModelState.AddModelError("AccountId", AmountGreaterThanSenderAccountBalanceErrorMessage);
                return View(model);
            }
            if (await transactionService.CustomerWithNameAndIBANExistsAsync(model.ReceiverIBAN, model.ReceiverName) == false)
            {
                ModelState.AddModelError("ReceiverIBAN", InvalidReceiverIBANErrorMessage);
                return View(model);
            }
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            try
            {
                await transactionService.MakeTransactionWithinTheBankAsync(model, userId);
                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        //TODO: Implement filtering by date and a set of transactions
        public async Task<IActionResult> MakeTransactionToAnotherBank()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var availableAccounts = await transactionService.GetAvailableAccountsAsync(userId);
            var model = new TransactionToAnotherBankViewModel()
            {
                AvailableAccounts = availableAccounts
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeTransactionToAnotherBank(TransactionToAnotherBankViewModel model)
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
            if (model.Amount < AmountMinValue || model.Amount > AmountMaxValue)
            {
                ModelState.AddModelError("Amount", InvalidAmountErrorMessage);
                return View(model);
            }
            if (model.ReceiverIBAN.Length != IBANMaxLength)
            {
                ModelState.AddModelError("ReceiverIBAN", InvalidIbanErrorMessage);
                return View(model);
            }
            if (model.ReceiverIBAN.Substring(4,4) != model.BIC)
            {
                ModelState.AddModelError("BIC", InvalidBICErrorMessage);
                return View(model);
            }
            if (await accountService.AmountGreaterThanSenderAccountBalance(model.AccountId, model.Amount) == true)
            {
                ModelState.AddModelError("AccountId", AmountGreaterThanSenderAccountBalanceErrorMessage);
                return View(model);
            }                       
            try
            {
                await transactionService.MakeTransactionToAnotherBankAsync(model, userId);
                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DownloadTransactionHistoryForAccount(int id)
        {
            if (await accountService.AccountWithIdExistsAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await transactionService.GenerateCSVFileForTransactionHistory(id);
            var fileName = $"TransactionHistory_{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss}.csv";
            var fileType = "text/csv";

            return File(Encoding.UTF8.GetBytes(model), fileType, fileName);
        }
        
    }
}
