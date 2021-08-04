using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace PayrollSystem.Logic.Specifications
{
    internal class EmployeeActiveSpec : Specification<Employee>
    {
        public EmployeeActiveSpec(bool isActive)
        {
            Query = isActive ? null : e => e.EmploymentDate.End == null || e.EmploymentDate.End.Value >= DateTime.Now;
        }
    }
}
