using Biura.model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Biura.Model
{

    public class Agency : IAgency
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Owner Owner { get; set; }
        private Management _management;
        public List<Registry> Registries { get; set; }

        public Agency()
        {
            Name = string.Empty;
            Location = string.Empty;
            Owner = new Owner();
            Registries = new List<Registry>();
        }

        public Agency(string name, string location, Owner owner)
        {
            Name = name;
            Location = location;
            Owner = owner;
            Registries = new List<Registry>();
        }

        public void AddManagement(Management management)
        {
            _management = management;
        }
        
        public override string ToString()
        {
            return $"biuro wycieczkowe {Name} ul {Location} wlasciciel {Owner}";
        }
    }
}
