using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biura.Model;

namespace Biura.DAL
{
    public interface IOperatorCRUD //make individual interfaces for each model?
    {
        public List<Operator> AllEntries();
        public void AddEntry(Operator entry);
        //public void UpdateEntry(Operator entry, int change);
        public void RemoveEntry(Operator entry);
        public bool IsInDb(Operator entry);
    }
}
