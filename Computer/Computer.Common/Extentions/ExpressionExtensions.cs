using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Computer.Common.Extentions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> AndWith<T>(this Expression<Func<T, bool>> firstExp, Expression<Func<T, bool>> secondExp)
        {
            return CombineExpressions(firstExp, secondExp, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> OrWith<T>(this Expression<Func<T, bool>> firstExp, Expression<Func<T, bool>> secondExp)
        {
            return CombineExpressions(firstExp, secondExp, Expression.OrElse);
        }

        private static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> firstExp, Expression<Func<T, bool>> secondExp, Func<Expression, Expression, Expression> combine)
        {
            return Expression.Lambda<Func<T, bool>>(combine(firstExp.Body, new ExpressionVisitorWrapper(secondExp.Parameters, firstExp.Parameters).Visit(secondExp.Body)), firstExp.Parameters);
        }
    }

    public class ExpressionVisitorWrapper : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _expressionParams;

        public ExpressionVisitorWrapper(IList<ParameterExpression> fromParams, IList<ParameterExpression> toParams)
        {
            _expressionParams = new Dictionary<ParameterExpression, ParameterExpression>();

            for (var i = 0; i != fromParams.Count && i != toParams.Count; i++)
                _expressionParams.Add(fromParams[i], toParams[i]);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            ParameterExpression param;

            if (_expressionParams.TryGetValue(node, out param))
                node = param;

            return base.VisitParameter(node);
        }
    }
}
