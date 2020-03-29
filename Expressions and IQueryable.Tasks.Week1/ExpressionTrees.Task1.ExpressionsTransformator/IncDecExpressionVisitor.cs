using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if(node == null)
            {
                return base.VisitBinary(node);
            }

            if (node.Right is ConstantExpression constantExpression 
                && constantExpression.Type == typeof(int)
                && (int)constantExpression.Value == 1)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Add:
                        return Expression.Increment(node.Left);
                    case ExpressionType.Subtract:
                        return Expression.Decrement(node.Left);
                    default:
                        return base.VisitBinary(node);
                }

            }
            return base.VisitBinary(node);
        }
    }
}
