using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Specifications.Base;
using System.Globalization;

namespace PayrollSystem.Logic.Specifications
{
    internal class EmployeeNameSpec : Specification<Employee>
    {
        public EmployeeNameSpec(string search)
        {
            search = search?.Trim()?.ToUpper() ?? null;

            Query = e => search == null || e.PersonalInformation.FullName.ToUpper().Contains(search);
        }
    }
}
