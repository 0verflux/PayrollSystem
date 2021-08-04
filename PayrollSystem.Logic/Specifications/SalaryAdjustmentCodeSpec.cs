using PayrollSystem.Logic.Domain.SalaryAdjustments;
using PayrollSystem.Logic.Specifications.Base;

namespace PayrollSystem.Logic.Specifications
{
    internal class SalaryAdjustmentCodeSpec : Specification<SalaryAdjustment>
    {
        public SalaryAdjustmentCodeSpec(string search)
        {
            search = search?.Trim() ?? null;
            var searchCaps = search?.ToUpper() ?? null;

            Query = e => search == null || e.Code.Contains(search) || (e.Description != null && e.Description.ToUpper().Contains(searchCaps));
        }
    }
}
