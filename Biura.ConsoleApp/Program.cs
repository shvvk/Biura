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
var RegistrySource = new Registries(context);

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

void BasicAgInfo(Agency ag)
{
    Console.WriteLine($"Id: {ag.Id}");
    Console.WriteLine($"Nazwa agencji: {ag.Name}");
    Console.WriteLine($"adres: {ag.Location}");
    Console.WriteLine($"właściciel: {ag.Owner}");
}

void AgencyMenu(Agency ag)
{
    Console.WriteLine($"agency menu {ag.Name}");
    int option = -1;
    Console.WriteLine("1- pokaz dane, 2-edytuj dane, 3-wroc do menu poczatkowego");
    int.TryParse(Console.ReadLine(), out option);
    switch (option)
    {
        case 1:
            Console.WriteLine("1- pokaż podstawowe informacje, 2-pokaż operatorów i wyczieczki ,3-pokaż ewidencje, 4-wroc do menu poczatkowego");
            int.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case 1: // podstawowe dane agencji
                    BasicAgInfo(ag);
                    break;
                case 2: // operatorzy i wycieczki 
                    Console.WriteLine("dostępni operatorzy oraz wycieczek");
                    var allops = OperatorSource.AllEntries();
                    foreach (var o in allops)
                    {
                        Console.WriteLine(o);
                        var excs = ExcursionSource.AllEntries();
                        var selexcs = excs.Where(e => e.TripOperator.Id == o.Id).ToList();
                        foreach (var e in selexcs)
                        {
                            Console.WriteLine($"\t{e}");
                        }
                    }
                    break;
                case 3: // spredarze itp
                    var allregistries = RegistrySource.AllEntries();
                    foreach (var r in allregistries)
                    {
                        Console.WriteLine(r);
                    }
                    break;
                case 4: ShowMenu(); break;
                default: ; break;
            }
            break;
        case 2:
            Console.WriteLine("1- edytuj podstawowe informacje, 2-edutuj operatorów ,3-edutuj ewidencje, 4-wroc do menu początkowego");
            int.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case 1: // zmiana podstawowych danych agencji
                    Console.WriteLine($"aktualne podstawowe dane agencji:"); BasicAgInfo(ag);
                    Console.WriteLine("wybierz co chcesz edytowac: 1-nazwę agencji, 2-adres agencji, 3-właściciela agencji, 4-anuluj");
                    int.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("podaj nową nazwę:");
                            AgencySource.UpdateEntry(ag, 0, Console.ReadLine());
                            Console.WriteLine($"zmieniono nazwę agencji na {ag.Name}");
                            break;
                        case 2:
                            Console.WriteLine("podaj nową lokalizacje:");
                            AgencySource.UpdateEntry(ag, 1, Console.ReadLine());
                            Console.WriteLine($"zmieniono lokalizacje agencji na {ag.Location}");
                            break;
                        case 3:
                            Console.WriteLine("podaj nowego właściciela:");
                            AgencySource.UpdateEntry(ag, 2, Console.ReadLine());
                            Console.WriteLine($"zmieniono właściciela agencji na {ag.Owner}");
                            break;
                        case 4: AgencyMenu(ag); break;
                    }
                    break;
                case 2:// edycja operatorow
                    Console.WriteLine("1-dodaj operatora, 2-edytuj operatora,3-usun opearatora");
                    int.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 1: // dodawanie operatora
                            var nowy = new Operator();
                            Console.WriteLine("podaj nazwę operatora: ");
                            nowy.Name = Console.ReadLine();
                            Console.WriteLine("podaj kraj pochodzenia operatora: ");
                            nowy.Country = Console.ReadLine();
                            Console.WriteLine("podaj procent prowizji (zakres danych od 0-1)");
                            decimal cr = 0.00m;
                            Decimal.TryParse(Console.ReadLine(), out cr);
                            nowy.CommisionRate = cr;
                            OperatorSource.AddEntry(nowy);
                            AgencySource.AddOperatorToAgency(ag, nowy);
                            break;
                        case 2:// edytowanie operatora
                            Console.WriteLine("wybierz operatora do edycji:");
                            var alloperators = ag.TourOperators;
                            foreach (var o in alloperators)
                            {
                                Console.WriteLine($"{o.Id}. {o.Name}");
                            }
                            int.TryParse(Console.ReadLine(), out option);
                            var Oper = alloperators.Find(x => x.Id == option);
                            if (Oper != null)
                            {
                                Console.WriteLine("co chcesz edytować? 1-nazwę operatora, 2-kraj pochodzenia, 3-prowizje, 4-anuluj");
                                int change = -1;
                                int.TryParse(Console.ReadLine(), out change);
                                switch (change)
                                {
                                    case 1:
                                        Console.WriteLine("podaj nową nazwę operatora:");
                                        OperatorSource.UpdateEntry(Oper, change, Console.ReadLine());
                                        break;
                                    case 2:
                                        Console.WriteLine("podaj nowy kraj pochodzenia operatora:");
                                        OperatorSource.UpdateEntry( Oper, change, Console.ReadLine());
                                        break;
                                    case 3:
                                        Console.WriteLine("podaj nową prowizję operatora (zakres danych od 0-1):");
                                        decimal comm = 0.00m;
                                        Decimal.TryParse(Console.ReadLine(), out comm);
                                        OperatorSource.UpdateEntry(Oper, change, "", comm);
                                        break;
                                    case 4: AgencyMenu(ag);
                                        break;
                                }
                            }
                            else { Console.WriteLine("operator o podanym numerze nie istnieje"); }
                            break;
                        case 3://usuwanie operatora
                            Console.WriteLine("usuń operatora z listy");
                            if (ag.TourOperators.Count == 0 || ag.TourOperators == null) { Console.WriteLine("brak operatorów do usunięcia"); break; }
                            Console.WriteLine("wybierz operatora, którego chcesz usunąć: ");
                            var operators = ag.TourOperators;
                            foreach (var o in operators)
                            {
                                Console.WriteLine($"{o.Id}. {o.Name}");
                            }
                            int.TryParse(Console.ReadLine(), out option);
                            var oper = operators.Find(x => x.Id == option);
                            if (oper != null)
                            {
                                OperatorSource.RemoveEntry(oper);
                                Console.WriteLine("operator został usunięty");
                            }
                            break;
                    }
                    break;
                case 3:// edycja ewidencji biura
                    Console.WriteLine("1-dodaj ewidencję, 2-edytuj ewidencję ,3-usun ewidencię");
                    int.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 1: // dodawanie operatora
                            var nowy = new Registry();
                            Console.WriteLine("podaj datę sprzedarzy w formacie dd/mm/yyyy: ");

                            nowy.Date = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("podaj ilość osób: ");
                            int persons = 0;
                            for(int i = 0 ; i< persons; i++)
                            {
                                Console.WriteLine($"podaj imię osoby {i+1}: ");
                                string fname = Console.ReadLine();
                                Console.WriteLine($"podaj nazwisko osoby {i+1}: ");
                                string lname = Console.ReadLine();
                                Console.WriteLine($"podaj datę urodzenia osoby {i+1} w formacie dd/mm/yyyy: ");
                                DateTime dob = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine($"podaj email osoby {i+1}: ");
                                string email = Console.ReadLine();
                                nowy.Clients.Add( new Client( fname, lname, dob, email));
                            }
                            Console.WriteLine("1-wybierz wycieczke z listy, 2-dodaj nowa wyczieczkę: ");
                            int.TryParse(Console.ReadLine(), out option);
                            switch (option)
                            {
                                case 1:
                                    var avilableExcursions = ExcursionSource.AllEntries();
                                    Console.WriteLine("wybierz z listy");
                                    foreach (var e in avilableExcursions)
                                    {
                                        Console.WriteLine($"{e.Id} {e}");
                                    }
                                    int.TryParse(Console.ReadLine(), out option);
                                    var selected = avilableExcursions.Find(x => x.Id == option);
                                    nowy.TripDetails = new Excursion(
                                        selected.TripOperator,selected.Location,selected.Date,selected.Cost,selected.People
                                        );
                                    break;
                                case 2:
                                    var newExcursion = new Excursion();
                                    Console.WriteLine("wybierz operatora z listy");
                                    var avilableOperators = OperatorSource.AllEntries();
                                    Console.WriteLine("wybierz z listy");
                                    foreach (var e in avilableOperators)
                                    {
                                        Console.WriteLine($"{e.Id} {e}");
                                    }
                                    int.TryParse(Console.ReadLine(), out option);
                                    newExcursion.TripOperator = avilableOperators.Find(x => x.Id == option);
                                    Console.WriteLine("podaj miejsce wycieczki");
                                    newExcursion.Location = Console.ReadLine();
                                    Console.WriteLine("podaj datę wycieczki w formacie dd/mm/yyyy: ");
                                    newExcursion.Date = DateTime.Parse(Console.ReadLine());
                                    Console.WriteLine("podaj koszt wycieczki: ");
                                    decimal comm = 0.00m;
                                    Decimal.TryParse(Console.ReadLine(), out comm);
                                    newExcursion.Cost = comm;
                                    Console.WriteLine("podaj ilość osób: ");
                                    int people = 0;
                                    int.TryParse(Console.ReadLine(), out people);
                                    nowy.TripDetails = newExcursion;
                                    break;
                            }
                            Console.WriteLine("podaj kwotę wpłaty początkowej: ");
                            decimal initpay = 0.00m;
                            Decimal.TryParse(Console.ReadLine(), out initpay);
                            nowy.InitialPayment = initpay;
                            Console.WriteLine("czy ewidencja ma zawierać dopłatę? 1-tak, 2-nie");
                            int.TryParse(Console.ReadLine(), out option);
                            switch (option)
                            {
                                case 1: nowy.AdditionalPayment = true; break;
                                case 2: nowy.AdditionalPayment = false; break;
                            }
                            nowy.InsuranceDetails = new Insurance("4423423", "Allianz", DateTime.Now, DateTime.Now.AddMonths(6), 0.05m); ;
                            RegistrySource.AddEntry(nowy);
                            //AgencySource.AddRegistryToAgency(ag, nowy);
                            
                            break;
                            
                            //OperatorSource.AddEntry(nowy);
                            //AgencySource.AddOperatorToAgency(ag, nowy);
                            break;
                        case 2:// edytowanie ewidencji
                            Console.WriteLine("wybierz ewidencję do edycji:");
                            var allregistries = ag.Registries;
                            foreach (var r in allregistries)
                            {
                                Console.WriteLine($"{r.Id}. {r}");
                            }
                            int.TryParse(Console.ReadLine(), out option);
                            var Reg = allregistries.Find(x => x.Id == option);
                            if (Reg != null)
                            {
                                Console.WriteLine("co chcesz edytować? 1-datę sprzedaży, 2-klientów, 3-szczegóły wycieczki, 4-wpłatę początkową, 5-dopłatę, 6-szczegóły ubezpieczenia, 7-anuluj");
                                int change = -1;
                                int.TryParse(Console.ReadLine(), out change);
                                switch (change)
                                {
                                    case 1:
                                        Console.WriteLine("podaj nową datę sprzedaży w formacie dd/mm/yyyy:");
                                        DateTime newdate = DateTime.Parse(Console.ReadLine());
                                        RegistrySource.UpdateEntry(Reg, change, newdate);
                                        break;
                                    case 2:
                                        // brak implementacji zmiany klientów
                                        break;
                                    case 3:
                                        // brak implementacji zmiany szczegółów wycieczki
                                        break;
                                    case 4:
                                        Console.WriteLine("podaj nową wpłatę początkową:");
                                        decimal newinitpay = 0.00m;
                                        Decimal.TryParse(Console.ReadLine(), out newinitpay);
                                        RegistrySource.UpdateEntry(Reg, change, initialPayment: newinitpay);
                                        break;
                                    case 5:
                                        Console.WriteLine("czy ewidencja ma zawierać dopłatę? 1-tak, 2-nie");
                                        int dp = -1;
                                        int.TryParse(Console.ReadLine(), out dp);
                                        switch (dp)
                                        {
                                            case 1:
                                                RegistrySource.UpdateEntry(Reg, change, additionalPayment: true);
                                                break;
                                            case 2:
                                                RegistrySource.UpdateEntry(Reg, change, additionalPayment: false);
                                                break;
                                        }
                                        break;
                                    case 6:
                                        // brak implementacji zmiany szczegółów ubezpieczenia
                                        break;
                                    case 7: AgencyMenu(ag);
                                        break;
                                }
                            }
                            else { Console.WriteLine("ewidencja o podanym numerze nie istnieje"); }
                            break;
                        case 3://usuwanie ewidencji
                            Console.WriteLine("usuń ewidencję z listy");
                            Console.WriteLine("wybierz ewidencję, którą chcesz usunąć: ");
                            var registries = ag.Registries;
                            foreach (var r in registries)
                            {
                                Console.WriteLine($"{r.Id}. {r}");
                            }
                            int.TryParse(Console.ReadLine(), out option);
                            var reg = registries.Find(x => x.Id == option);
                            if (reg != null)
                            {
                                RegistrySource.RemoveEntry(reg);
                                Console.WriteLine("ewidencja została usunięta");
                            }
                            if (ag.Registries.Count == 0 || ag.Registries == null) { Console.WriteLine("brak ewidencji do usunięcia"); break; }
                            break;
                    }
                    break;
                case 4: ShowMenu(); break;
            }
            break;
        case 3: ShowMenu(); break;
        default: ; break;
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
void mainMenu()
{
    Console.WriteLine("witaj w bazie danych");
    Console.WriteLine("1- pokaż menu agencji, 2-wygeneruj raport, 0- wyjście z programu");
    int.TryParse(Console.ReadLine(), out option);
    switch (option)
    {
        case 1:
            ShowMenu();
            break;
        case 2:
            Console.WriteLine("wygeneruj raport: 1- ilosc ewidencji w biurze, 2- zysk biura");
            int.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case 1:
                    {
                        var RegistryReport = new RegistryCount();
                        var ageReport = RegistryReport.GenerateReport(agencja);
                        Console.WriteLine(ageReport);
                        var output = ageReport.Data;
                        Console.WriteLine(output);
                    }
                    break;
                case 2:
                    {
                        var ProfitReport = new RegistryProfit();
                        var profitReport = ProfitReport.GenerateReport(agencja);
                        Console.WriteLine(profitReport);
                        var output = profitReport.Data;
                        Console.WriteLine(output);
                    }
                    break;
            }
            break;

    }
}

while (option != 0)
{
   mainMenu();
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