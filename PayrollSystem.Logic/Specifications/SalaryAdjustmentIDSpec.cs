using PayrollSystem.Logic.Domain.SalaryAdjustments;
using PayrollSystem.Logic.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Specifications
{
    internal class SalaryAdjustmentIDSpec : Specification<SalaryAdjustment>
    {
        public SalaryAdjustmentIDSpec(int id)
        {
            Query = e => e.ID == id;
        }
    }
}
