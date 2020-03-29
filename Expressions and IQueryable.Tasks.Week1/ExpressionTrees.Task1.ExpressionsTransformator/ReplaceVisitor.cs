using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Dictionary<string, int> _replaceDictionary;

        public ReplaceVisitor(Dictionary<string, int> replaceDictionary)
        {
            _replaceDictionary = replaceDictionary ?? throw new ArgumentNullException(nameof(replaceDictionary));
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if(node == null)
            {
                return base.VisitParameter(node);
            }

            if (_replaceDictionary.ContainsKey(node.Name))
            {
                return Expression.Constant(_replaceDictionary[node.Name]);
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node) => Expression.Lambda(Visit(node.Body), node.Parameters);
    }
}
