using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.BankAdministrator;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Infinia.Controllers
{
    [Authorize(Roles = "BankAdministrator")]
    public class BankAdministratorController : Controller
    {
        private readonly IBankAdministratorService bankAdministratorService;
        private readonly HttpClient httpClient;
        public BankAdministratorController(IBankAdministratorService bankAdministratorService)
        {
            this.bankAdministratorService = bankAdministratorService;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://predictionapi-agghdjbsezg2dxgp.canadacentral-01.azurewebsites.net/");
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

        [HttpPost] 
        public async Task<IActionResult> BranchAnalysis(BranchAnalysisSelectedBranchAndIntervalViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("GetBranchResults", new
                {
                    branchName = model.BranchName,
                    startDate = model.StartInterval,
                    endDate = model.EndInterval
                });
            }

            return View(model); 
        }
        public IActionResult GetBranchResults(string branchName, DateTime startDate, DateTime endDate)
        {
            var model = new BranchAnalysisSelectedBranchAndIntervalViewModel()
            {
                BranchName = branchName,
                StartInterval = startDate,
                EndInterval = endDate
            };
            return View(model);
        }
        public async Task<JsonResult> GetBranchAnalysis(string branchName, DateTime startDate, DateTime endDate)
        {
            var model = await bankAdministratorService.GetBranchAnalysisAsync(branchName, startDate, endDate);
            return Json(model);
        }

        public async Task<JsonResult> ForecastCashflow(string branchName)
        {
            var jsonModel = new
            {
                Date = new List<string>
        {
            "2024-09-01",
            "2024-09-08",
            "2024-09-15",
            "2024-09-22",
            "2024-09-29",
            "2024-10-06"
        },
                TransactionFees = new List<int>
        {
            4521,
            3003,
            4120,
            2750,
            4700,
            1550
        },
                AccountMaintenanceFees = new List<int>
        {
            1502,
            2789,
            1024,
            3050,
            1890,
            1490
        },
                LoanDisbursements = new List<int>
        {
            36000,
            27000,
            4300,
            10000,
            23000,
            15000
        },
                LoanRepayments = new List<int>
        {
            1230,
            470,
            8300,
            9500,
            5400,
            6900
        }
            };

            var jsonString = JsonConvert.SerializeObject(jsonModel);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("forecast", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var prediction = JsonConvert.DeserializeObject<CashFlowForecastViewModel>(responseString);

            return Json(prediction);
        }
        public async Task<JsonResult> GenderAnalysis(string branchName)
        {
            var jsonModel = new GenderAnalysisModel
            {
                Gender = new List<string> { "Male", "Female", "Male", "Female", "Male", "Female", "Male" },
                TransactionFrequency = new List<int> { 2, 3, 2, 1, 3, 2, 3 },
                AverageTransactionAmount = new List<decimal> { 150.00m, 200.00m, 120.00m, 300.00m, 250.00m, 180.00m, 190.00m },
                TotalTransactionAmount = new List<decimal> { 6000.00m, 8000.00m, 5200.00m, 12000.00m, 10000.00m, 7200.00m, 8200.00m },
                LoanAmountRequested = new List<int> { 10000, 15000, 12000, 18000, 20000, 13000, 15000 },
                LoanAmountApproved = new List<int> { 9000, 14000, 11000, 17000, 18000, 12000, 13000 },
                AccountBalance = new List<int> { 5000, 7000, 6000, 12000, 8000, 9000, 9000 }
            };

            var jsonString = JsonConvert.SerializeObject(jsonModel);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("customer", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var prediction = JsonConvert.DeserializeObject<GenderAnalysisResultViewModel>(responseString);

            return Json(prediction);
        }
    }
}