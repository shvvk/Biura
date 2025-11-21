using Biura.Model;

namespace Biura.Tests
{
    public class Test
    {
        [Fact]
        public void MakeExcursion()
        {
            var Exc = new Excursion(
                new Operator("TestOperator", "Land", 0.3m),
                "ExampleLocation",
                new DateTime(2026,03,24),
                3243.32m,
                3
                );
            Assert.Equal("ExampleLocation", Exc.Location);

        }
    }
}