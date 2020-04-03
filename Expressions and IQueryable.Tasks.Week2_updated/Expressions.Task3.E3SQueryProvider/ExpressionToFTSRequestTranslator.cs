using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }
            if (node.Method.DeclaringType == typeof(string)
               && node.Method.Name == "StartsWith")
            {
                //System.InvalidOperationException: 'When called from '', rewriting a node 
                //of type 'System.Linq.Expressions.MethodCallExpression'
                //must return a non-null value of the same type. 
                var resultExpression = new StringVisitor().VisitAndConvert(node, "");
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    Expression memberExpression;
                    Expression constantExpression;
                    if (node.Left.NodeType != ExpressionType.MemberAccess
                        && node.Right.NodeType != ExpressionType.MemberAccess)
                        throw new NotSupportedException($"One of operands should be property or field: {node.NodeType}");

                    if (node.Right.NodeType != ExpressionType.Constant
                        && node.Left.NodeType != ExpressionType.Constant)
                        throw new NotSupportedException($"One of operands should be constant: {node.NodeType}");

                    if(node.Left.NodeType == ExpressionType.MemberAccess)
                    {
                        memberExpression = node.Left;
                        constantExpression = node.Right;
                    }
                    else
                    {
                        memberExpression = node.Right;
                        constantExpression = node.Left;
                    }

                    Visit(memberExpression);
                    _resultStringBuilder.Append("(");
                    Visit(constantExpression);
                    _resultStringBuilder.Append(")");
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}
