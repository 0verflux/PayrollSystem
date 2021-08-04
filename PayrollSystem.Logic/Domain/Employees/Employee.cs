using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain.Employees.DTOs;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.Positions;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.Employees
{
    internal class Employee : Entity
    {
        public PersonalInformation PersonalInformation { get; private set; }
        public DateRange EmploymentDate { get; private set; }

        public Position Position { get; internal set; }
        public virtual IReadOnlyCollection<PayrollEntry> PayrollEntries { get; private set; }   // READ-ONLY

        private Employee() : base(default) { }
        public Employee(int employeeID) : base(employeeID) { }
        public Employee(int employeeID, PersonalInformation personalInformation, DateRange employmentDate, Position position) : this(employeeID)
        {
            SetPersonalInformation(personalInformation);
            SetPosition(position);
            SetEmploymentDate(employmentDate);
        }
        public Employee(PersonalInformation personalInformation, DateRange employmentDate, Position position)
            : this(default, personalInformation, employmentDate, position)
        {

        }

        public void SetPersonalInformation(PersonalInformation personalInformation)
        {
            PersonalInformation = Guard.Against.Null(personalInformation, nameof(personalInformation));
        }

        public void SetEmploymentDate(DateRange employmentDate)
        {
            EmploymentDate = Guard.Against.Null(employmentDate, nameof(employmentDate));
        }

        public void SetEmploymentEndDate(DateTime dateTime)
        {
            EmploymentDate = new(EmploymentDate.Start, dateTime);
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}
