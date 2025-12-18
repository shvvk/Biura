using Biura.Model;
using Buira.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratedReport = Biura.Reports.Generate.RegistryProfit.RegistryProfitReport;

namespace Biura.Reports.Generate
{
    public class RegistryProfit
    {
        public Report<GeneratedReport> GenerateReport(Agency ag)
        {
            decimal profit = 0.00m;
            foreach (var Reg in ag.Registries)
            {
                profit += Reg.InitialPayment;
                if (Reg.AdditionalPayment)
                    {
                    profit += Reg.Afterpayment ?? 0m;
                    }
            }
            var reportDetails = new GeneratedReport()
            {
                Sum = profit
            };

            return new Report<GeneratedReport>
            {
                GeneratedAt = DateTime.Now,
                Data = reportDetails
            };
        }

        public record RegistryProfitReport
{
            public required decimal Sum { get; init; }
        }
    }
}