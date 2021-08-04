using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Logic.Common
{
    internal abstract class Value<T> where T : Value<T>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj) =>
            (obj is T value) && GetEqualityComponents().SequenceEqual(value.GetEqualityComponents());

        public override int GetHashCode() =>
            GetEqualityComponents().Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));

        public static bool operator ==(Value<T> left, Value<T> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Value<T> left, Value<T> right)
        {
            return !(left == right);
        }
    }
}
