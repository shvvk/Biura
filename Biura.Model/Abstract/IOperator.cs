using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biura.Model;
namespace Biura.model.Abstract
{
    internal interface IOperator
    {
        public void AddExcursion(string place, DateTime date, decimal cost, int persons);
    }
}
