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
    context.SaveChanges();
}

var AgencySource = new Agencies(context);
var OperatorSource = new Operators(context);
var ExcursionSource = new Excursions(context);

Operator op = new Operator("Itaka", "Polska", 0.1m);
Operator op1 = new Operator("Rainbow", "Polska", 0.15m);
Agency agencja = new Agency("travel", "Warszawa", "Jan Kowalski");
Console.WriteLine(agencja);
Insurance ins = new Insurance("4423423", "Allianz", DateTime.Now, DateTime.Now.AddMonths(6), 0.05m);
agencja.AddInsurance(ins);
op.AddExcursion("Egipt", DateTime.Now, 3245.23m, 2);
op.ListExcursions();
op.AddExcursion("Wręczyca Mała", DateTime.Now, 4235.23m, 3);
op.ListExcursions();
op1.AddExcursion("Monachium", DateTime.Now, 1234.56m, 4);
op1.AddExcursion("Berlin", DateTime.Now, 2345.67m, 2);
op1.AddExcursion("Praga", DateTime.Now, 3456.78m, 5);
agencja.AddOperator(op);
agencja.AddOperator(op1);
agencja.ListAllAvilableExcursions();
agencja.NewRegistry(
    new List<Client>
    {
        new Client("Anna", "Nowak", new DateTime(1990, 5, 15),"annanowak2@gmail.com"),
        new Client("Piotr", "Zalewski", new DateTime(1985, 8, 20), "piotrszalewski232@gmail.com")
    },new DateTime(2023,12,23), agencja.TourOperators[1].Excursions[0], 1200.00m, ins);

AgencySource.AddEntry(agencja);

foreach (var o in AgencySource.AllEntries())
{
    Console.WriteLine(o);
    foreach (var to in o.TourOperators)
    {
        Console.WriteLine($"\t{to}");
        foreach(var ex in to.Excursions)
        {
            Console.WriteLine($"\t\t{ex}");
        }
    }
    foreach (var r in o.Registries)
    {
        Console.WriteLine($"\t{r}");
    }
}

void ListAgencyData(Agency ag)
{
    Console.WriteLine($"Id: {ag.Id}");
    Console.WriteLine($"Nazwa agencji: {ag.Name}");
    Console.WriteLine($"adres: {ag.Location}");
    Console.WriteLine($"właściciel: {ag.Owner}");
    Console.WriteLine("dostępni operatorzy wycieczek i ich dostępne wycieczki");
    ag.ListAllAvilableExcursions();

}

void AgencyMenu(Agency ag)
{
    Console.WriteLine($"agency menu {ag.Name}");
    int option = -1;
    Console.WriteLine("1- pokaz dane, 2-edytuj dane,3-usun dane, 4-wroc do menu poczatkowego");
    int.TryParse(Console.ReadLine(), out option);
    switch (option)
    {
        case 1: ListAgencyData(ag); break;
        case 4: ShowMenu(); break;
        default: Console.WriteLine("all done"); break;
    }

}
void ShowMenu()
{
    Console.WriteLine("wybierz agencje z listy ");
    int agencynum = -1;
    var agencies = AgencySource.AllEntries();
    foreach (var a in agencies)
    {
        Console.WriteLine($"{a.Id}. {a.Name}");
    }
    int.TryParse(Console.ReadLine(), out agencynum);
    var agencja = agencies.Find(x => x.Id == agencynum);
    if (agencynum <= agencies.Count && agencynum > 0) { AgencyMenu(agencja); } else { Console.WriteLine("agencji o tym numerze nie ma na liście"); ShowMenu(); }

}

int option = 1;
while (option != 0)
{
    Console.WriteLine("witaj w bazie danych");
    ShowMenu();

}

/* 
var RegistryReport = new RegistryCount();
var ageReport = RegistryReport.GenerateReport(agencja);
Console.WriteLine(ageReport);
var output = ageReport.Data;
Console.WriteLine(output);
Console.WriteLine(output.RegCount);
*/

/*
foreach(var o in OperatorSource.AllEntries())
{
    Console.WriteLine(o);
}


foreach (var o in OperatorSource.AllEntries())
{
    Console.WriteLine(o);
}

*/