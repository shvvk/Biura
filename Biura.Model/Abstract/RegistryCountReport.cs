using Biura.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratedReport = Buira.Report.Generate.RegistryCount.RegistryCountReport;

namespace Buira.Report.Generate
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