using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.PayrollEntries.DTOs;
using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Domain
{
    public static class EntityFactory
    {
        internal static Employee ConstructEmployee(EmployeeDTO employee)
        {
            return new(
                employee.ID,
                new(employee.Name, employee.Address, employee.Gender, employee.BirthDate),
                new(employee.EmploymentStartDate, employee.EmploymentEndDate),
                employee.PositionID.HasValue && employee.PositionID.Value != 0 ? new(employee.PositionID.Value) : null);
        }

        internal static Employee ConstructEmployee(int id)
        {
            return new(id);
        }

        internal static Position ConstructPosition(PositionDTO position)
        {
            return new(
                position.ID,
                position.Name,
                position.RatePerHour);
        }

        internal static Position ConstructPosition(int id)
        {
            return new(id);
        }

        internal static SalaryAdjustment ConstructSalaryAdjustment(SalaryAdjustmentDTO salaryAdjustment)
        {
            return new(
                salaryAdjustment.ID,
                salaryAdjustment.Code,
                salaryAdjustment.Description);
        }

        internal static SalaryAdjustmentDetail ConstructSalaryAdjustmentDetail(SalaryAdjustmentDetailDTO salaryAdjustmentDetail)
        {
            return new(
                salaryAdjustmentDetail.ID,
                new(salaryAdjustmentDetail.SalaryAdjustment.ID),
                new(salaryAdjustmentDetail.Percentage, salaryAdjustmentDetail.Value));
        }

        internal static PayrollEntry ConstructPayrollEntry(CreatePayrollEntryDTO payrollEntry)
        {
            return new(
                new(payrollEntry.EmployeeID),
                new(payrollEntry.CurrentPositionID),
                payrollEntry.HoursWorked,
                payrollEntry.HoursOvertime,
                new(payrollEntry.PayPeriodStart, payrollEntry.PayPeriodEnd));
        }
    }
}
