using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public abstract class FilterPipe<T> where T : class
    {
        public Expression<Func<T, bool>> CombineExpressions(
            Expression<Func<T, bool>> predicateExpressionOld,
            Expression<Func<T, bool>> predicateExpressionNew)
        {
            if (predicateExpressionOld == null && predicateExpressionNew == null)
            {
                return null;
            }

            if (predicateExpressionOld == null)
            {
                return predicateExpressionNew;
            }

            Func<Expression, Expression, BinaryExpression> logicalFunction = Expression.AndAlso;

            var body = predicateExpressionOld.Body;
            body = logicalFunction(body, Expression.Invoke(predicateExpressionNew, predicateExpressionOld.Parameters));
            var filter = Expression.Lambda<Func<T, bool>>(body, predicateExpressionOld.Parameters);

            return filter;
        }
    }
}