using Biura.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.DAL
{
    public class RegistriesInDb
    {
        private readonly BiuraDbContext _db;

        public RegistriesInDb(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddRegistry(Registry reg) //Create
        {
            if (IsInDb(reg)) { Console.WriteLine("registry already exists in db"); } else { _db.Insurances.Add(op); }
            _db.SaveChanges();
        }

        public List<Registry> AllRegistries() //Read
        {
            return _db.Registries.ToList();
        }

        public void UpdateRegistry(Registry reg, int change, string nc = "", decimal comRate = 0m) //Update
        {
            switch (change)
            {
                case 0: reg.Date = ; break;
                case 1: reg.Clients = ; break;
                case 2: reg.TripDetails = ; break;
                case 3: reg.InitialPayment = ; break;
                case 4: reg.AdditionalPayment = ; break;
                case 5: reg.Afterpayment = ; break;
                case 6: reg.InsuranceDetails = ; break;

                default: Console.WriteLine("0-change name, 1-change country, 2-change commision rate"); break;
            }
        }

        public void RemoveRegistry(Registry reg) //Delete
        {
            if (IsInDb(reg)) { _db.Remove(reg); } else { Console.WriteLine("Registry not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Registry reg)
        {
            return AllRegistries().Exists(x => x.Clients == reg.Clients);
        }

    }
}
