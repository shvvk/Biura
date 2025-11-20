// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Biura.Model;

Management SiecBiur = new Management("Sieć");

Operator op = new Operator("Itaka", "Polska", 0.1m);
Operator op1 = new Operator("Rainbow", "Polska", 0.15m);
SiecBiur.AddAgency(new Agency("travel", "Warszawa", new Owner("Jan", "Kowalski")));
SiecBiur.AddOperator(op);
op.AddExcursion("Egipt", DateTime.Now, 3245.23m, 2);
op.ListExcursions();
SiecBiur.TourOperators[0].AddExcursion("Wręczyca Mała", DateTime.Now, 4235.23m, 3);
SiecBiur.TourOperators[0].ListExcursions();
SiecBiur.AddOperator(op1);
SiecBiur.TourOperators[1].AddExcursion("Monachium", DateTime.Now, 1234.56m, 4);
SiecBiur.TourOperators[1].AddExcursion("Berlin", DateTime.Now, 2345.67m, 2);
SiecBiur.TourOperators[1].AddExcursion("Praga", DateTime.Now, 3456.78m, 5);
SiecBiur.ListAgencies();
SiecBiur.ListAvilableExcursions();

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