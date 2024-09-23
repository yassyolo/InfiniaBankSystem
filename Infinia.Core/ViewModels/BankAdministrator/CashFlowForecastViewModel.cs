using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinia.Core.ViewModels.BankAdministrator
{
    public class CashFlowForecastViewModel
    {
        public List<ForecastDataModel> ForecastData { get; set; }
        public Dictionary<string, decimal> HistoricalData { get; set; }
    }
}
