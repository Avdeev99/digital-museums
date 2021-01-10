using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DigitalMuseums.Data.Extensions
{
    /// <summary>
    /// The expression extensions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// The include method name.
        /// </summary>
        private const string IncludeMethodName = nameof(EntityFrameworkQueryableExtensions.Include);

        /// <summary>
        /// The then include name.
        /// </summary>
        private const string ThenIncludeName = nameof(EntityFrameworkQueryableExtensions.ThenInclude);

        /// <summary>
        /// The select method name.
        /// </summary>
        private const string SelectMethodName = nameof(Enumerable.Select);

        /// <summary>
        /// The invalid member message.
        /// </summary>
        private const string InvalidMemberMessage = "Invalid member along the include path: {0}";

        /// <summary>
        /// The string type.
        /// </summary>
        private static readonly Type StringType = typeof(string);

        /// <summary>
        /// The open i enumerable type.
        /// </summary>
        private static readonly Type OpenIEnumerableType = typeof(IEnumerable<>);

        /// <summary>
        /// The lambda expression type.
        /// </summary>
        private static readonly Type LambdaExpressionType = typeof(LambdaExpression);

        /// <summary>
        /// The include method.
        /// </summary>
        private static readonly MethodInfo IncludeMethod;

        /// <summary>
        /// The then include for collection.
        /// </summary>
        private static readonly MethodInfo ThenIncludeForCollection;

        /// <summary>
        /// The then include for entity.
        /// </summary>
        private static readonly MethodInfo ThenIncludeForEntity;

        /// <summary>
        /// The select method.
        /// </summary>
        private static readonly MethodInfo SelectMethod;

        /// <summary>
        /// Initializes static members of the <see cref="IncludeExpressionConverter"/> class.
        /// </summary>
        static ExpressionExtensions()
        {
            var entityFrameworkQueryableExtensionsMethods = typeof(EntityFrameworkQueryableExtensions).GetMethods();

            IncludeMethod = entityFrameworkQueryableExtensionsMethods
                .Single(m => m.Name == IncludeMethodName && m.GetParameters().All(p => p.ParameterType != StringType));

            ThenIncludeForCollection = entityFrameworkQueryableExtensionsMethods
                .Single(m => m.Name == ThenIncludeName && m.GetParameters()[0].ParameterType.GetGenericArguments()[1].IsGenericType
                                                       && m.GetParameters()[0].ParameterType.GetGenericArguments()[1].GetGenericTypeDefinition() == typeof(IEnumerable<>));

            SelectMethod = typeof(Enumerable).GetMethods()
                .Single(m => m.Name == SelectMethodName && m.GetParameters()[1].ParameterType.GetGenericArguments().Count() == 2);

            ThenIncludeForEntity = entityFrameworkQueryableExtensionsMethods
                .Single(m => m.Name == ThenIncludeName && m != ThenIncludeForCollection);
        }

        /// <summary>
        /// Include method.
        /// </summary>
        /// <param name="queryable">The queryable.</param>
        /// <param name="navigationPropertyPath">The navigation property path.</param>
        /// <typeparam name="TEntity">The entity.</typeparam>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <returns>The <see cref="IQueryable"/>.</returns>
        public static IQueryable<TEntity> IncludeProperty<TEntity, TProperty>(
            this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            object query = queryable;
            Stack<LambdaExpression> includeExpressionParts = GetExpressionParts(navigationPropertyPath);

            LambdaExpression firstIncludePart = includeExpressionParts.Pop();

            MethodInfo includeMethod = IncludeMethod.MakeGenericMethod(firstIncludePart.Parameters.Single().Type, firstIncludePart.ReturnType);

            query = includeMethod.Invoke(null, new[] { query, firstIncludePart });

            while (includeExpressionParts.TryPop(out var subsequentIncludePart))
            {
                Type[] genericArguments = new[]
                {
                    query.GetType().GetGenericArguments()[0],
                    subsequentIncludePart.Parameters.Single().Type,
                    subsequentIncludePart.ReturnType
                };

                object[] includeArguments = new[] { query, subsequentIncludePart };

                if (query.GetType().GenericTypeArguments[1].GetInterfaces().Any(f => f.IsGenericType && f.GetGenericTypeDefinition() == OpenIEnumerableType))
                {
                    query = ThenIncludeForCollection.MakeGenericMethod(genericArguments).Invoke(null, includeArguments);
                }
                else
                {
                    query = ThenIncludeForEntity.MakeGenericMethod(genericArguments).Invoke(null, includeArguments);
                }
            }

            return (IQueryable<TEntity>)query;
        }

        /// <summary>
        /// The count of.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int CountOf(this string text, string pattern)
        {
            return (text.Length - text.Replace(pattern, string.Empty).Length) / pattern.Length;
        }

        /// <summary>
        /// Get expression parts.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The <see cref="Stack"/>.</returns>
        private static Stack<LambdaExpression> GetExpressionParts(Expression expression)
        {
            var stringExpression = expression.ToString();
            var expressionParts = new Stack<LambdaExpression>(stringExpression.CountOf(".") - stringExpression.CountOf($".{SelectMethodName}("));

            if (!(expression is LambdaExpression lambda))
            {
                throw new ArgumentException(nameof(expression), $"Parameter must be of type {LambdaExpressionType}");
            }

            GetExpressionParts(lambda.Body, expressionParts);
            return expressionParts;
        }

        /// <summary>
        /// Get expression parts.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="expressionParts">The expression parts.</param>
        private static void GetExpressionParts(Expression expression, Stack<LambdaExpression> expressionParts)
        {
            while (true)
            {
                if (expression is ParameterExpression)
                {
                    return;
                }

                if (expression is MemberExpression memberExpression)
                {
                    if (!(memberExpression.Member is PropertyInfo property))
                    {
                        throw new ArgumentException(string.Format(InvalidMemberMessage, memberExpression.Member));
                    }

                    ParameterExpression parameterExpression = Expression.Parameter(memberExpression.Expression.Type);
                    MemberExpression propertyExpression = Expression.Property(parameterExpression, property);

                    expressionParts.Push(Expression.Lambda(propertyExpression, parameterExpression));

                    expression = memberExpression.Expression;
                }
                else if (expression is MethodCallExpression methodCallExpression)
                {
                    if (methodCallExpression.Method.GetGenericMethodDefinition() != SelectMethod)
                    {
                        throw new ArgumentException(string.Format(InvalidMemberMessage, methodCallExpression));
                    }

                    if (!(methodCallExpression.Arguments[1] is LambdaExpression innerExpression))
                    {
                        throw new ArgumentException(string.Format(InvalidMemberMessage, methodCallExpression.Arguments[1]));
                    }

                    GetExpressionParts(innerExpression.Body, expressionParts);

                    expression = methodCallExpression.Arguments[0];
                }
                else
                {
                    throw new ArgumentException(string.Format(InvalidMemberMessage, expression));
                }
            }
        }
    }
}
