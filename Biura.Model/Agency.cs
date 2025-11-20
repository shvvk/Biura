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
        public string Name { get; set; }
        public string Location { get; set; }
        public Owner Owner { get; set; }
        public List<Operator> TourOperators { get; set; }
        public List<Insurance> Insurances { get; set; }
        public List<Registry> Registries { get; set; } = new List<Registry>();


        public Agency()
        {
            Name = string.Empty;
            Location = string.Empty;
            Owner = new Owner();
            TourOperators = new List<Operator>();
            Insurances = new List<Insurance>();
        }

        public Agency(string name, string location, Owner owner)
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

        public void NewRegistry(List<Client> clients, Excursion tripDetails, Payment initialPayment, Insurance insuranceDetails)
        {
            Registry newRegistry = new Registry
            {
                Clients = clients,
                TripDetails = tripDetails,
                InitialPayment = initialPayment,
                InsuranceDetails = insuranceDetails
            };
            Registries.Add(newRegistry);
        }

        public override string ToString()
        {
            return $"biuro wycieczkowe {Name} ul {Location} wlasciciel {Owner}";
        }
    }
}
