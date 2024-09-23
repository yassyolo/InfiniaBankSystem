using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.BankAdministrator;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Core.ViewModels.Loan;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;

namespace Infinia.Controllers
{
    public class BankAdministratorController : Controller
    {
        private readonly IBankAdministratorService bankAdministratorService;
        private readonly HttpClient httpClient;
        public BankAdministratorController(IBankAdministratorService bankAdministratorService)
        {
            this.bankAdministratorService = bankAdministratorService;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5000/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
        public async Task<JsonResult> ForecastCashflow(string branchName)
        {
            var historicalData = await bankAdministratorService.GetHistoricalDataAsync(branchName);
            var jsonModel = new
            {
                Date =historicalData.HistoricalData.Select(x => x.Date).ToList(),
                TransactionFees = historicalData.HistoricalData.Select(x => x.TransactionFees).ToList(),
                AccountMaintenanceFees = historicalData.HistoricalData.Select(x => x.AccountMaintenanceFees).ToList(),
                LoanDisbursements = historicalData.HistoricalData.Select(x => x.LoanDisbursements).ToList(),
                LoanRepayments = historicalData.HistoricalData.Select(x => x.LoanRepayments).ToList(),
            };
            var jsonString = JsonConvert.SerializeObject(jsonModel);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            var response = await httpClient.PostAsync("forecast", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var prediction = JsonConvert.DeserializeObject<CashFlowForecastViewModel>(responseString);
            return Json(prediction);
        }
    }
}
