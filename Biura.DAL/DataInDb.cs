using Biura.Model;

namespace Biura.DAL
{
    public class Agencies
    {
        private readonly BiuraDbContext _db;

        public Agencies(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddEntry(Agency entry) //Create
        {
            if (IsInDb(entry)) { Console.WriteLine("Agency already exists in db"); } else { _db.Agencies.Add(entry); }
            _db.SaveChanges();
        }

        public List<Agency> AllEntries() //Read
        {
            return _db.Agencies.ToList();
        }
        //public void UpdateEntry(Agency entry)
        //{
        //    _db.Agencies.Update(entry);
        //    _db.SaveChanges();
        //}
        public void UpdateEntry(Agency entry, int change,
            string name = "",
            string location = "",
            string owner = ""
            ) //Update
        {
            switch (change)
            {
                case 0: entry.Name = name; break;
                case 1: entry.Location = location; break;
                case 2: entry.Owner = owner; break;
                default: Console.WriteLine("nothing changed"); break;
            }
            _db.SaveChanges();
        }

        public void AddOperatorToAgency(Agency agency, Operator tourOperator)
        {
            agency.TourOperators.Add(tourOperator);
            _db.SaveChanges();
        }

        public void RemoveEntry(Agency entry) //Delete
        {
            if (IsInDb(entry)) { _db.Remove(entry); } else { Console.WriteLine("operator not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Agency entry)
        {
            return AllEntries().Exists(x => x.Name == entry.Name && x.Owner == entry.Owner && x.Location == entry.Location);
        }

        //public void AddRegistryToAgency(Agency ag, Registry nowy)
        //{
        //    IsInDb(nowy);
        //    ag.Registries.Add(nowy);
        //    _db.SaveChanges();
        //}
    }

    public class Operators : IOperatorCRUD
    {
        private readonly BiuraDbContext _db;

        public Operators(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddEntry(Operator entry) //Create
        {
            if (IsInDb(entry)) { Console.WriteLine("operator already exists in db"); } 
            else { _db.Operators.Add(entry); }
            _db.SaveChanges();
        }

        public List<Operator> AllEntries() //Read
        {
            return _db.Operators.ToList();
        }

        public void updateEntry(Operator entry)
        {
            _db.Operators.Update(entry);
            _db.SaveChanges();
        }

        public void UpdateEntry(Operator entry, int change, string nc = "", decimal comRate = 0m) //Update
        {
            switch (change)
            {
                case 1: entry.Name = nc; break;
                case 2: entry.Country = nc; break;
                case 3: entry.CommisionRate = comRate; break;
                default: Console.WriteLine("0-change name, 1-change country, 2-change commision rate"); break;
            }
            _db.SaveChanges();
        }

        public void RemoveEntry(Operator entry) //Delete
        {
            if (IsInDb(entry)) { _db.Remove(entry); } else { Console.WriteLine("operator not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Operator entry)
        {
            return AllEntries().Exists(x => x.Name == entry.Name && x.Country == entry.Country);
        }
    }
    public class Registries // system ewidencji 
    {
        private readonly BiuraDbContext _db;

        public Registries(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddEntry(Registry entry) //Create
        {
            if (IsInDb(entry)) { Console.WriteLine("registry already exists in db"); } else { _db.Registries.Add(entry); }
            _db.SaveChanges();
        }

        public List<Registry> AllEntries() //Read
        {
            return _db.Registries.ToList();
        }

        public void updateEntry(Registry entry)
        {
            _db.Registries.Update(entry);
        }
        public void UpdateEntry( // remove and add this logic in biura.logic then call updateEntry
            Registry reg, int change,
            DateTime date = default,
            List<Client> clients = null,
            decimal initialPayment = 0m,
            bool additionalPayment = false,
            decimal afterpayment = 0m,
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

        public void RemoveEntry(Registry entry) //Delete
        {
            if (IsInDb(entry)) { _db.Remove(entry); } else { Console.WriteLine("Registry not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Registry entry)
        {
            return AllEntries().Exists(x => x.Date == entry.Date && x.Clients == entry.Clients);
        }

    }
    public class Excursions
    {
        private readonly BiuraDbContext _db;

        public Excursions(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddEntry(Excursion entry) //Create
        {
            if (IsInDb(entry)) { Console.WriteLine("registry already exists in db"); } else { _db.Excursions.Add(entry); }
            _db.SaveChanges();
        }

        public List<Excursion> AllEntries() //Read
        {
            return _db.Excursions.ToList();
        }

        public void updateEntry(Excursion entry)
        {
            _db.Excursions.Update(entry);
        }

        public void UpdateEntry(Excursion entry)
        {
            _db.Update(entry);
        }
        public void UpdateEntry( // remove and add this logic in biura.logic then call updateEntry
            Registry reg, int change,
            DateTime date = default,
            List<Client> clients = null,
            decimal initialPayment = 0m,
            bool additionalPayment = false,
            decimal afterpayment = 0m,
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

        public void RemoveEntry(Excursion entry) //Delete
        {
            if (IsInDb(entry)) { _db.Remove(entry); } else { Console.WriteLine("Excursion not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Excursion entry)
        {
            return AllEntries().Exists(x => x.TripOperator == entry.TripOperator && x.Date == entry.Date);
        }
    }
}
 