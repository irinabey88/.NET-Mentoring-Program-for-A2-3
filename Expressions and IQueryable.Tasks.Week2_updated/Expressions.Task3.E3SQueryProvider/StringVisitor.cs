using Expressions.Task3.E3SQueryProvider.Models.Entities;
using System;
using System.Linq.Expressions;

namespace Expressions.Task3.E3SQueryProvider
{
    public class StringVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node == null)
            {
                return base.Visit(node);
            }

            if ((node.Method.DeclaringType == typeof(string)
               && node.Method.Name == "StartsWith"))
            {
                var item = Expression.Parameter(typeof(EmployeeEntity));
                var property = Expression.Property(item, "Workstation");

                var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                var argument = node.Arguments[0];
                var startsWithExpr = Expression.Call(property, method, argument);

                var lambda = Expression.Lambda<Func<EmployeeEntity, bool>>(startsWithExpr, item);
                
                //What kind of type should be return here.
                return lambda;
            }

            return base.VisitMethodCall(node);
        }
    }
}
