using PayrollSystem.Logic.Common;
using System;
using System.Linq.Expressions;

namespace PayrollSystem.Logic.Specifications.Base
{
    internal sealed class OrSpecification<T> : Specification<T> where T : Entity
    {
        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            Query = ToQueryExpression(left, right);
        }

        private static Expression<Func<T, bool>> ToQueryExpression(Specification<T> left, Specification<T> right)
        {
            if (left is null)
            {
                return right;
            }
            if(right is null)
            {
                return left;
            }
            if(left is null && right is null)
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
