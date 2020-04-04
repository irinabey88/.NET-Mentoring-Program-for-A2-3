using System;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class StartWithVisitor : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;
        public StartWithVisitor(StringBuilder resultStringBuilder)
        {
            _resultStringBuilder = resultStringBuilder ?? throw new ArgumentNullException(nameof(resultStringBuilder));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");
            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append("(");
            _resultStringBuilder.Append(node.Value);
            _resultStringBuilder.Append("*)");

            return node;
        }
    }
}
