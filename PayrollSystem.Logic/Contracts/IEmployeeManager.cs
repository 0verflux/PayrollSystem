using PayrollSystem.Logic.Domain.Employees.DTOs;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Contracts
{
    public interface IEmployeeManager
    {
        void CreateEmployee(EmployeeDTO employee);
        void UpdateEmployee(EmployeeDTO employee);
        void DeleteEmployee(EmployeeDTO employee);
        void MarkAsEndEmploymentByEmployee(EmployeeDTO employee, DateTime? employmentEndDate = null);
        List<EmployeeDTO> GetAllEmployees();
        List<EmployeeDTO> GetEmployees(string search = null, int positionID = 0, bool isActive = false, bool includePositions = false, bool includePayrollEntries = false);
    }
}
