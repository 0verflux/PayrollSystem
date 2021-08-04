using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.DTOs;
using PayrollSystem.Logic.Domain.SalaryAdjustments;

namespace PayrollSystem.Logic.Domain.SalaryAdjustmentDetails
{
    internal class SalaryAdjustmentDetail : Entity
    {
        public Percentage Percentage { get; private set; }
        public SalaryAdjustment SalaryAdjustment { get; private set; }
        public virtual PayrollEntry PayrollEntry { get; private set; }

        private SalaryAdjustmentDetail() : base(default) { }
        public SalaryAdjustmentDetail(int salaryAdjustmentDetailID) : base(salaryAdjustmentDetailID) { }
        public SalaryAdjustmentDetail(int salaryAdjustmentDetailID, SalaryAdjustment salaryAdjustment, Percentage percentage) : this(salaryAdjustmentDetailID)
        {
            SetSalaryAdjustment(salaryAdjustment);
            SetPercentage(percentage);
        }
        public SalaryAdjustmentDetail(SalaryAdjustment salaryAdjustment, Percentage percentage) : this(default, salaryAdjustment, percentage) { }

        public void SetSalaryAdjustment(SalaryAdjustment salaryAdjustment)
        {
            SalaryAdjustment = Guard.Against.Null(salaryAdjustment, nameof(salaryAdjustment));
        }

        public void SetPercentage(Percentage percentage)
        {
            Percentage = Guard.Against.Null(percentage, nameof(percentage));
        }
    }
}
