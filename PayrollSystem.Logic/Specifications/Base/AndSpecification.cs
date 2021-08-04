using PayrollSystem.Logic.Common;
using System;
using System.Linq.Expressions;

namespace PayrollSystem.Logic.Specifications.Base
{
    internal sealed class AndSpecification<T> : Specification<T> where T : Entity
    {
        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            Query = ToQueryExpression(left, right);
        }

        private static Expression<Func<T, bool>> ToQueryExpression(Specification<T> left, Specification<T> right)
        {
            Func<T, bool> lFunc = left;
            Func<T, bool> rFunc = right;

            if (lFunc is null)
            {
                return right;
            }
            if(rFunc is null)
            {
                return left;
            }
            if (lFunc is null && rFunc is null)
            {
                return null;
            }

            Expression<Func<T, bool>> leftExpression = left;
            Expression<Func<T, bool>> rightExpression = right;

            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);
            return finalExpr;
        }
    }
}
