/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            Expression<Func<int, int>> sourceExp = (a) => a + (a + 1) + (a - 1);
            var resultExpression = new IncDecExpressionVisitor().VisitAndConvert(sourceExp, "");
            Console.WriteLine($"{0} : {1}", resultExpression, resultExpression.Compile().Invoke(3));

            Expression<Func<int,int,int, int>> sourceExp1 = (a,b,c) => a + (b + 1) + (c - 1);
            var replaceDictionary = new Dictionary<string, int>()
            {
                {"a", 3},
                {"b", 2},
                {"c", 4}
            };
            var resultExpression1 = new ReplaceVisitor(replaceDictionary).VisitAndConvert(sourceExp1, "");
            Console.WriteLine($"{0} : {1}", resultExpression1, resultExpression1.Compile().Invoke(5,6,7));

            Console.ReadLine();
        }
    }
}
