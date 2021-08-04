using System.Linq.Expressions;

namespace PayrollSystem.Logic.Specifications.Base
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression parameters;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(parameters);
        }

        public ParameterReplacer(ParameterExpression parameters)
        {
            this.parameters = parameters;
        }
    }
}
