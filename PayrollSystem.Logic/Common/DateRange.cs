using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Common
{
    internal class DateRange : Value<DateRange>
    {
        public DateTime Start { get; }
        public DateTime? End { get; }

        private DateRange() { }
        public DateRange(DateTime start) : this(start, null) { }
        public DateRange(DateTime start, DateTime? end) =>
            (Start, End) = (start, end);

        public int Days => ElapsedDate.Days;
        public double TotalDays => ElapsedDate.TotalDays;
        private TimeSpan ElapsedDate => (End ?? DateTime.Now) - Start;

        public bool HasEndDate => End.HasValue;
        public int? DaysSinceEndDate => (DateTime.Now - End)?.Days;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
