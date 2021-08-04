using Ardalis.GuardClauses;
using PayrollSystem.Logic.Common;
using System;
using System.Linq.Expressions;

namespace PayrollSystem.Logic.Specifications.Base
{
    /* reference: https://github.com/vkhorikov/SpecificationPattern/blob/15086389101fc296e67d6d76102b611d40775b2b/SpecificationPattern/Specifications.cs */
    internal abstract class Specification<T> where T : Entity
    {
        protected Expression<Func<T, bool>> Query { get; set; }

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = Query.Compile();

            return predicate(entity);
        }

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification?.Query;
        }

        public static implicit operator Func<T, bool>(Specification<T> specification)
        {
            return specification.Query?.Compile();
        }
    }
}
