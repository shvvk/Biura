using Biura.Model;
using Buira.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratedReport = Biura.Reports.Generate.RegistryCount.RegistryCountReport;

namespace Biura.Reports.Generate
{
    public class RegistryCount
    {
        public Report<GeneratedReport> GenerateReport(Agency ag)
        {
            var reportDetails = new GeneratedReport()
            {
                RegCount = (int)ag.Registries.Count
            };

            return new Report<GeneratedReport>
            {
                GeneratedAt = DateTime.Now,
                Data = reportDetails
            };
        }

        public record RegistryCountReport
        {
            public required int RegCount { get; init; }
        }
    }
}