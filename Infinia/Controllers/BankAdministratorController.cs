using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.BankAdministrator;
using Microsoft.AspNetCore.Mvc;

namespace Infinia.Controllers
{
    public class BankAdministratorController : Controller
    {
        private readonly IBankAdministratorService bankAdministratorService;

        public BankAdministratorController(IBankAdministratorService bankAdministratorService)
        {
            this.bankAdministratorService = bankAdministratorService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BranchAnalysis()
        {
            var model = new BranchAnalysisSelectedBranchAndIntervalViewModel();
            return View(model);
        }
        public async Task<JsonResult> GetBranchAnalysis(string branchName, DateTime startDate, DateTime endDate)
        {
            var model = await bankAdministratorService.GetBranchAnalysisAsync(branchName, startDate, endDate);
            return Json(model);
        }
    }
}
