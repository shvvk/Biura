using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biura.Model;

namespace Biura.DAL
{
    public class OperatorsInDb
    {
        private readonly BiuraDbContext _db;

        public OperatorsInDb(BiuraDbContext db)
        {
            _db = db;
        }
        public void AddOperator(Operator op) //Create
        {
            if (IsInDb(op)) { Console.WriteLine("operator already exists in db"); } else { _db.Operators.Add(op); }
            _db.SaveChanges();
        }

        public List<Operator> AllOperators() //Read
        {
            return _db.Operators.ToList();
        }

        public void UpdateOperator(Operator op, int change, string nc = "", decimal comRate = 0m) //Update
        {
            switch (change)
            {
                case 0: op.Name = nc; break;
                case 1: op.Country = nc; break;
                case 2: op.CommisionRate = comRate; break;
                default: Console.WriteLine("0-change name, 1-change country, 2-change commision rate"); break;
            }
        }

        public void RemoveOperator(Operator op) //Delete
        {
            if (IsInDb(op)) { _db.Remove(op); } else { Console.WriteLine("operator not found in db"); }
            _db.SaveChanges();
        }

        public bool IsInDb(Operator op)
        {
            return AllOperators().Exists(x => x.Name == op.Name);
        }
        
    }
}
