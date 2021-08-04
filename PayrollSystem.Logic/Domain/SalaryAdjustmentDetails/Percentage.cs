using PayrollSystem.Logic.Common;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.SalaryAdjustmentDetails
{
    internal class Percentage : Value<Percentage>
    {
        public float Ratio { get; }
        public decimal Value { get; }

        private Percentage() { }
        public Percentage(float ratio, decimal value)
        {
            Ratio = ratio;
            Value = value;
        }
        public Percentage(decimal totalCost, float ratio)
        {
            Ratio = ratio;
            Value = totalCost * (decimal)(1 + ratio);
        }
        public Percentage(decimal totalCost, decimal value)
        {
            Ratio = (float)(value / totalCost);
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ratio;
            yield return Value;
        }
    }
}
