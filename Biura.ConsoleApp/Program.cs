using Biura.Model;
using Biura.Reports;
using Biura.Reports.Generate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Biura.DAL;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
    {
        var cns = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BiuraDbContext>(options => options.UseSqlServer(cns));
    }).Build();

var context = _host.Services.GetService<BiuraDbContext>();
if(context != null)
{
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Operator tripOperator = new Operator()
    {
        Name = "Itaka",
        Country = "Poland",
        CommisionRate = 0.1m
    };
    context.Operators.Add(tripOperator);
    context.SaveChanges();
}

Operator op = new Operator("Itaka", "Polska", 0.1m);
Operator op1 = new Operator("Rainbow", "Polska", 0.15m);
Agency agencja = new Agency("travel", "Warszawa", new Owner("Jan", "Kowalski"));
Insurance ins = new Insurance("4423423", "Allianz", DateTime.Now, DateTime.Now.AddMonths(6), 0.05m);
agencja.AddOperator(op);
op.AddExcursion("Egipt", DateTime.Now, 3245.23m, 2);
op.ListExcursions();
agencja.TourOperators[0].AddExcursion("Wręczyca Mała", DateTime.Now, 4235.23m, 3);
agencja.TourOperators[0].ListExcursions();
agencja.AddOperator(op1);
agencja.TourOperators[1].AddExcursion("Monachium", DateTime.Now, 1234.56m, 4);
agencja.TourOperators[1].AddExcursion("Berlin", DateTime.Now, 2345.67m, 2);
agencja.TourOperators[1].AddExcursion("Praga", DateTime.Now, 3456.78m, 5);
agencja.ListAllAvilableExcursions();
agencja.NewRegistry(
    new List<Client>
    {
        new Client("Anna", "Nowak", new DateTime(1990, 5, 15),"annanowak2@gmail.com"),
        new Client("Piotr", "Zalewski", new DateTime(1985, 8, 20), "piotrszalewski232@gmail.com")
    },new DateTime(2023,12,23), agencja.TourOperators[1].Excursions[0], 1200.00m, ins);
var RegistryReport = new RegistryCount();
var ageReport = RegistryReport.GenerateReport(agencja);
Console.WriteLine(ageReport);
var output = ageReport.Data;
Console.WriteLine(output);
Console.WriteLine(output.RegCount);




/*
Agency tplanetpl = new Agency("travel", "Czestochowa", new Person("Robert", "Dymski", 32));
List<Excursion> excursions = new List<Excursion>();
void ListExcursions()
{
    Console.WriteLine($"List contains {excursions.Count} excursions");
    foreach (Excursion ex in excursions)
    {
        Console.WriteLine(ex);
    }
}



Person p1 = new Person("Jan", "Kowalski", new DateTime(1997, 4, 12), "jankowalski@gmail.com");
Person p2 = new Person("Kamil", "Królikowski", new DateTime(2001, 4, 12), "KamilKrolikowski@gmail.com");
Person p3 = new Person("Igor", "Kowalczyk", new DateTime(1991, 7, 11), "igorkowalczyk@gmail.com");

DateTime d1 = new DateTime(2020, 12, 23);
Excursion e1 = new Excursion("Afryka", DateTime.Now, 3245.23, 2);
Excursion e2 = new Excursion("Zabrze", DateTime.Today, 324f, 3);
Excursion e3 = new Excursion("Monachium", d1, 4548f, 4);

Client c = new Client(p1, DateTime.Now);
Client c1 = new Client(p2, DateTime.Now);


excursions.Add(e1);

Console.WriteLine(e1);

Console.WriteLine(c1);
c1.ShowBookedTrip();
c1.TripBooked = e1;
c1.ShowBookedTrip();

Console.WriteLine(tplanetpl);

tplanetpl.ListClients();
ListExcursions();

tplanetpl.AddCLient(c1);
tplanetpl.AddClient(new Client("Karol", "Nowak", 32, DateTime.Now));
tplanetpl.AddClient(new Client(p3, d1));
tplanetpl.AddClient(p2);
tplanetpl.ListClients();
*/