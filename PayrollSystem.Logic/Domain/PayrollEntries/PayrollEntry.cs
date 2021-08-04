using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.PayrollEntries
{
    internal class PayrollEntry : Entity
    {
        private List<SalaryAdjustmentDetail> salaryAdjustmentDetails;

        public float HoursWorked { get; private set; }
        public float HoursOvertime { get; private set; }
        public DateRange Date { get; private set; }

        public Employee Employee { get; private set; }
        public Position CurrentPosition { get; private set; }
        public IReadOnlyCollection<SalaryAdjustmentDetail> SalaryAdjustmentDetails => salaryAdjustmentDetails;

        private PayrollEntry() : base(default) { }
        public PayrollEntry(int payrollEntryID) : base(payrollEntryID) { }
        public PayrollEntry(int payrollEntryID, Employee employee, Position position, float hoursWorked, float hoursOvertime, DateRange date) 
            : this(payrollEntryID)
        {
            Employee = Guard.Against.Null(employee, nameof(employee));
            HoursWorked = Guard.Against.Negative(hoursWorked, nameof(hoursWorked));
            HoursOvertime = Guard.Against.Negative(hoursOvertime, nameof(hoursOvertime));
            Date = Guard.Against.Null(date, nameof(date));

            SetEmployee(employee);
            SetPosition(position);
            salaryAdjustmentDetails = new();
        }
        public PayrollEntry(Employee employee, Position position, float hoursWorked, float hoursOvertime, DateRange date) 
            : this(default, employee, position, hoursWorked, hoursOvertime, date)
        {

        }

        public void SetEmployee(Employee employee)
        {
            Employee = Guard.Against.Null(employee, nameof(employee));
        }

        public void SetPosition(Position position)
        {
            CurrentPosition = Guard.Against.Null(position, nameof(position));
        }

        public void SetSalaryAdjustmentDetails(List<SalaryAdjustmentDetail> salaryAdjustmentDetails)
        {
            this.salaryAdjustmentDetails = Guard.Against.Null(salaryAdjustmentDetails, nameof(salaryAdjustmentDetails));
        }
    }
}
