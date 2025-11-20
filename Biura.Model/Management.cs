using Biura.model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Biura.Model
{
    public class Management : IManagement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        private List<Agency> _agencies = new List<Agency>();
        public List<Operator> TourOperators { get; set; }
        public List<Insurance> Insurances { get; set; }

        public Management()
        {
            Name = string.Empty;
            TourOperators = new List<Operator>();
            Insurances = new List<Insurance>();
        }
        public Management(string name)
        {
            Name = name;
            TourOperators = new List<Operator>();
            Insurances = new List<Insurance>();
        }

        public void AddAgency(Agency agency)
        {
            _agencies.Add(agency);
        }
        public void RemoveAgency(int num)
        {
            try{
                _agencies.Remove(_agencies[num]);
            }
            catch (ArgumentOutOfRangeException){
                Console.WriteLine("No agency with given number.");
            }
        }
        public void AddOperator(Operator tourOperator)
        {
            TourOperators.Add(tourOperator);
        }
        public void AddInsurance(Insurance insurance)
        {
            Insurances.Add(insurance);
        }
        public void ListAgencies()
        {
            Console.WriteLine($"Management {Name} has {_agencies.Count} agencies:");
            if (_agencies.Count != 0)
            {
                int i = 0;
                foreach (Agency agency in _agencies)
                {
                    Console.WriteLine($"{i}. {agency}");
                    i++;
                }
            }
        }

        public void ListAvilableExcursions()
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

    }
}
