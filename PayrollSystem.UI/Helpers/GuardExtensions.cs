using Ardalis.GuardClauses;
using System;

namespace PayrollSystem.UI.Helpers
{
    public static class GuardExtensions
    {
        public static void InvalidPredicate(this IGuardClause guardClause, bool predicate, string propertyName, string? message = null)
        {
            if (predicate)
            {
                throw new ArgumentException(message: message, paramName: propertyName);
            }
        }
    }
}
