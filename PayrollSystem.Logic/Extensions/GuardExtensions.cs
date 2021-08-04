using Ardalis.GuardClauses;
using System;
using System.Globalization;

namespace PayrollSystem.Logic.Extensions
{
    internal static class GuardExtensions
    {
        public static string InvalidStringValueOrLength(this IGuardClause guardClause, string input, string parameterName, bool required, int maxLength, string? message = null)
        {
            if (required) Guard.Against.NullOrWhiteSpace(input, parameterName, message);
            Guard.Against.OutOfRange(input.Length, parameterName, 0, maxLength, message);

            return input.Trim();
        }

        public static char InvalidGender(this IGuardClause guardClause, char input, string parameterName, string? message = null)
        {
            return "MmFf".Contains(input) ?
                char.ToUpper(input, CultureInfo.CurrentCulture) :
                throw new ArgumentException(
                    message: message ?? $"{parameterName} is not a valid gender!",
                    paramName: parameterName);
        }
    }
}
