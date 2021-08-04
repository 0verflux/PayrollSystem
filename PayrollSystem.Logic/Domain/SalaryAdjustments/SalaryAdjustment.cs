using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.Logic.Extensions;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.SalaryAdjustments
{
    internal class SalaryAdjustment : Entity
    {
        public string Code { get; private set; }
        public string? Description { get; private set; }
        public virtual IReadOnlyCollection<SalaryAdjustmentDetail> SalaryAdjustmentDetails { get; private set; }

        private SalaryAdjustment() : base(default) { }
        public SalaryAdjustment(int salaryAdjustmentID) : base(salaryAdjustmentID) { }
        public SalaryAdjustment(int salaryAdjustmentID, string code, string? description = null) : this(salaryAdjustmentID)
        {
            SetCode(code);
            SetDescription(description);
        }
        public SalaryAdjustment(string code, string? description = null) : this(default, code, description)
        {

        }

        public void SetCode(string code)
            => Code = Guard.Against.InvalidStringValueOrLength(code, nameof(code), true, 16);

        public void SetDescription(string? description = null)
        {
            Description = description;
        }
    }
}
