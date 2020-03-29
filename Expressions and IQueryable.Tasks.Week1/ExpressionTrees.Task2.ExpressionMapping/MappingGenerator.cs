using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTrees.Task2.ExpressionMapping
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);
            Dictionary<string, PropertyInfo> outProperties = destinationType.GetProperties().ToDictionary(p => p.Name);
            var sourceParam = Expression.Parameter(typeof(TSource));
            var destinationConstructor = destinationType.GetConstructor(Type.EmptyTypes);

            var sourceInstance = Expression.Variable(sourceType);
            var destinationInstance = Expression.Variable(destinationType);

            var expressions = new List<Expression>
            {
                Expression.Assign(sourceInstance, Expression.Convert(sourceParam, sourceType)),
                Expression.Assign(destinationInstance, Expression.New(destinationConstructor))
            };

            var sourceProperties = sourceType.GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                if (!outProperties.TryGetValue(sourceProperty.Name, out var outProperty)) continue;

                var sourceValue = Expression.Property(sourceInstance, sourceProperty);
                var destinationValue = Expression.Property(destinationInstance, outProperty);

                expressions.Add(Expression.Assign(destinationValue, sourceValue));
            }

            expressions.Add(destinationInstance);

            var body = Expression.Block(new[] { sourceInstance, destinationInstance }, expressions);
            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }
}
