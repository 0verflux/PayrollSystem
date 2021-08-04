using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Specifications.Base;

namespace PayrollSystem.Logic.Specifications
{
    internal class EmployeeIDSpec : Specification<Employee>
    {
        public EmployeeIDSpec(int id)
        {
            Query = e => e.ID == id;
        }
    }
}
