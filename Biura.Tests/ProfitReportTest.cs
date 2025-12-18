using Biura.Model;
using Biura.Reports.Generate;

namespace Biura.Tests
{
    public class ProfitReportTest
    {
        [Fact]
        public void CheckReport()
        {
            var Exc = new Excursion(
                new Operator("TestOperator", "Land", 0.3m),
                "ExampleLocation",
                new DateTime(2026, 03, 24),
                3243.32m,
                3
                );
            var exc2 = new Excursion(new Operator(), "", DateTime.Now, 12345.32m, 2);
            Agency agencja = new Agency("travel", "Warszawa", "Jan Kowalski");
            agencja.NewRegistry(
                new List<Client>{},
                DateTime.Now,
                Exc,
                Exc.Cost,
                new Insurance()
                );
            agencja.NewRegistry(
                new List<Client> { },
                DateTime.Now,
                exc2,
                exc2.Cost,
                new Insurance());
            var RegistryReport = new RegistryProfit();
            var agencyProfit = RegistryReport.GenerateReport(agencja);
            Assert.Equal(Exc.Cost+exc2.Cost, agencyProfit.Data.Sum);

        }
    }
}