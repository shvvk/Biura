using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Biura.Model
{
    public class Excursion
    {
        public Operator TripOperator { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int People { get; set; }
        public Excursion()
        {
            TripOperator = new Operator();
            Location = string.Empty;
            Date = DateTime.MinValue;
            Cost = 0;
            People = 0;
        }
        public Excursion(Operator to,string location, DateTime date, decimal cost, int p)
        {
            TripOperator = to;
            Location = location;
            Date = date;
            Cost = cost;
            People = p;
        }

        public override string ToString()
        {
            return $"{Location} in {Date} run by {TripOperator.Name} will cost {Cost}. Vacation is for {People} people costing each {Cost/People} pln";
        }
    }
}
