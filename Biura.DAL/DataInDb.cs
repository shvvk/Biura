using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biura.model.Abstract;
using Biura.Model;

namespace Biura.DAL
{
    public class OperatorsInDb : IOperatorCRUD
    {
        private readonly BiuraDbContext _db;

        public OperatorsInDb(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddEntry(Operator entry) //Create
        {
            if (IsInDb(entry)) { Console.WriteLine("operator already exists in db"); } else { _db.Operators.Add(entry); }
            _db.SaveChanges();
        }

        public List<Operator> AllEntries() //Read
        {
            return _db.Operators.ToList();
        }

        public void UpdateEntry(Operator entry, int change, string nc = "", decimal comRate = 0m) //Update
        {
            switch (change)
            {
                case 0: entry.Name = nc; break;
                case 1: entry.Country = nc; break;
                case 2: entry.CommisionRate = comRate; break;
                default: Console.WriteLine("0-change name, 1-change country, 2-change commision rate"); break;
            }
        }

        public void RemoveEntry(Operator entry) //Delete
        {
            if (IsInDb(entry)) { _db.Remove(entry); } else { Console.WriteLine("operator not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Operator entry)
        {
            return AllEntries().Exists(x => x.Name == entry.Name);
        }
    }
    public class RegistriesInDb
    {
        private readonly BiuraDbContext _db;

        public RegistriesInDb(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddEntry(Registry reg) //Create
        {
            if (IsInDb(reg)) { Console.WriteLine("registry already exists in db"); } else { _db.Registries.Add(reg); }
            _db.SaveChanges();
        }

        public List<Registry> AllEntries() //Read
        {
            return _db.Registries.ToList();
        }

        public void UpdateEntry(
            Registry reg, int change,
            DateTime date = default,
            List<Client> clients = null,
            Payment initialPayment = null,
            bool additionalPayment = false,
            Payment afterpayment = null,
            Insurance insuranceDetails = null
            )
        {
            switch (change)
            {
                case 0: reg.Date = date; break;
                case 1: reg.Clients = clients; break;
                case 2: reg.TripDetails = null; break; // Placeholder, as TripDetails param is missing
                case 3: reg.InitialPayment = initialPayment; break;
                case 4: reg.AdditionalPayment = additionalPayment; break;
                case 5: reg.Afterpayment = afterpayment; break;
                case 6: reg.InsuranceDetails = insuranceDetails; break;

                default: Console.WriteLine("0-change date, 1-change clients, 2-change trip details, 3-change initial payment, 4-change additional payment, 5-change afterpayment, 6-change insurance details"); break;
            }
        }

        public void RemoveEntry(Registry reg) //Delete
        {
            if (IsInDb(reg)) { _db.Remove(reg); } else { Console.WriteLine("Registry not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Registry reg)
        {
            return AllEntries().Exists(x => x.Clients == reg.Clients);
        }

    }
}
 