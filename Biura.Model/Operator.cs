using System;
using Biura.model.Abstract;
namespace Biura.Model
{
	public class Operator : IOperator
	{
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
		public string Country { get; set; }
        public List<Excursion> Excursions { get; private set; } = new List<Excursion>();
        public decimal CommisionRate { get; set; }

        public Operator()
		{
			Name = string.Empty;
			Country = string.Empty;
			CommisionRate = 0;
        }
		public Operator(Operator op)
		{
			Name = op.Name;
			Country = op.Country;
			CommisionRate = op.CommisionRate;
        }
        public Operator(string name, string country, decimal commisionRate)
		{
			Name = name;
			Country = country;
            CommisionRate = commisionRate;
        }

		public void AddExcursion(string place, DateTime date, decimal cost, int persons)
		{
			Excursions.Add(new Excursion(this, place, date, cost, persons));
        }


		public void ListExcursions(string b = "")
		{
			int i = 0;
            foreach (var excursion in Excursions)
			{
				Console.WriteLine($"{b}{i}. {excursion}");
				i++;
            }
        }
        public override string ToString()
		{
			return $"operator {Name} kraj {Country} prowizja {CommisionRate:P}";
        }
    }
}
