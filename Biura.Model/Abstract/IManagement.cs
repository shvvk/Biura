using Biura.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model.Abstract
{
    internal interface IManagement
    {
        // TODO: Add crud
        public void AddAgency(Agency agency);
        public void AddOperator(Operator tourOperator);
        public void AddInsurance(Insurance insurance);
    }
}
