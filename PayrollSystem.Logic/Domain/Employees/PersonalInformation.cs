using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using PayrollSystem.Logic.Extensions;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Domain.Employees
{
    internal class PersonalInformation : Value<PersonalInformation>
    {
        public string FullName { get; }
        public string Address { get; }
        public char Gender { get; }
        public DateTime BirthDate { get; }

        private PersonalInformation() { }
        public PersonalInformation(string fullName, string address, char gender, DateTime birthDate)
        {
            FullName = Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
            Address = Guard.Against.NullOrWhiteSpace(address, nameof(address));
            Gender = Guard.Against.InvalidGender(gender, nameof(gender));
            BirthDate = birthDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FullName;
            yield return Address;
            yield return Gender;
            yield return BirthDate;
        }
    }
}
