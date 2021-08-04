using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Specifications.Base;

namespace PayrollSystem.Logic.Specifications
{
    internal class EmployeePositionIDSpec : Specification<Employee>
    {
        public EmployeePositionIDSpec(int id)
        {
            int? posID = id >= 0 ? id : null;

            Query = e => posID == 0 || (posID == null && e.Position == null) || e.Position.ID == id;
        }
    }
}
