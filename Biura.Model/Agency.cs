using Biura.model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Biura.Model
{

    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Owner { get; set; }
        public List<Operator> TourOperators { get; set; }
        public List<Insurance> Insurances { get; set; }
        public List<Registry> Registries { get; set; } = new List<Registry>();


        public Agency()
        {
            Name = string.Empty;
            Location = string.Empty;
            Owner = string.Empty;
            TourOperators = new List<Operator>();
            Insurances = new List<Insurance>();
        }

        public Agency(string name, string location, string owner)
        {
            Name = name;
            Location = location;
            Owner = owner;
            TourOperators = new List<Operator>();
            Insurances = new List<Insurance>();
        }

        public void AddOperator(Operator tourOperator)
        {
            TourOperators.Add(tourOperator);
        }
        public void AddInsurance(Insurance insurance)
        {
            Insurances.Add(insurance);
        }

        public void ListAllAvilableExcursions()
        {
            Console.WriteLine($"Dostepne wycieczki");
            Console.WriteLine($"Znaleziono {TourOperators.Count} operatorów:");
            int i = 0;
            foreach (var tourOperator in TourOperators)
            {
                Console.WriteLine($"{i}. {tourOperator.Name} oferuje {tourOperator.Excursions.Count} wycieczek");
                tourOperator.ListExcursions("\t");
                i++;
            }
        }

        public void ListAllRegistries()
        {
            Console.WriteLine($"Rejestry wycieczek w agencji {Name}");
            Console.WriteLine($"Znaleziono {Registries.Count} rejestrów:");
            int i = 0;
            foreach (var registry in Registries)
            {
                Console.WriteLine($"{i}. {registry}");
                i++;
            }
        }

        public void NewRegistry(List<Client> clients, DateTime date, Excursion tripDetails, decimal initialPayment, Insurance insuranceDetails, bool surr = false)
        {
            Registry newRegistry = new Registry
            {
                Clients = clients,
                Date = date,
                TripDetails = tripDetails,
                InitialPayment = initialPayment,
                InsuranceDetails = insuranceDetails
            };
            Registries.Add(newRegistry);
        }

        public override string ToString()
        {
            return $"biuro wycieczkowe {Name} ul {Location} wlasciciel nazywa sie {Owner}";
        }
    }
}
