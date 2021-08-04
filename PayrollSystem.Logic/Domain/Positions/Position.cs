using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.Logic.Extensions;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.Positions
{
    internal class Position : Entity
    {
        public string Name { get; private set; }
        public decimal RatePerHour { get; private set; }

        public virtual IReadOnlyCollection<Employee> Employees { get; private set; }            // READ-ONLY
        public virtual IReadOnlyCollection<PayrollEntry> PayrollEntries { get; private set; }   // READ-ONLY

        private Position() : base(default) { }
        public Position(int positionID) : base(positionID) { }
        public Position(int positionID, string name, decimal ratePerhour) : this(positionID)
        {
            SetName(name);
            SetRatePerHour(ratePerhour);
        }
        public Position(string name, decimal ratePerHour) : this(default, name, ratePerHour) { }

        public void SetName(string name)
        {
            Name = Guard.Against.InvalidStringValueOrLength(name, nameof(name), true, 64).Trim();
        }

        public void SetRatePerHour(decimal ratePerHour)
        {
            RatePerHour = Guard.Against.Negative(ratePerHour, nameof(ratePerHour));
        }
    }
}
