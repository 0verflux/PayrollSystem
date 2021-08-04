using PayrollSystem.Logic.Domain.SalaryAdjustments;
using PayrollSystem.Logic.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Specifications
{
    internal class SalaryAdjustmentExistSpec : Specification<SalaryAdjustment>
    {
        public SalaryAdjustmentExistSpec(int id, string code)
        {
            code = code?.Trim() ?? null;

            Query = e => e.Code == code && (id == 0 || e.ID != id);
        }
    }
}
