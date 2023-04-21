using System.Linq.Expressions;
using System.Reflection;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="AttributeUsageAttribute" />.
/// </summary>
public sealed class AttributeUsageAttributeExpressionWriter : AttributeExpressionWriter<AttributeUsageAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<AttributeUsageAttribute> attributes)
    {
        if (attributes.Count != 1)
        {
            return ArraySegment<Expression>.Empty;
        }

        AttributeUsageAttribute attribute = attributes.First();
        var allowMultipleProperty = AttributeType.GetProperty(nameof(AttributeUsageAttribute.AllowMultiple))!;
        var inheritedProperty = AttributeType.GetProperty(nameof(AttributeUsageAttribute.Inherited))!;

        Type[] constructorArgumentTypes = {typeof(AttributeTargets)};
        var constructor = AttributeType.GetConstructor(constructorArgumentTypes)!;
        var validOnExpression = Expression.Constant(attribute.ValidOn);
        var constructorExpression = Expression.New(constructor, validOnExpression);
        var bindings = new List<MemberBinding>();

        if (attribute.AllowMultiple)
        {
            var allowMultipleExpression = Expression.Constant(attribute.AllowMultiple);
            bindings.Add(Expression.Bind(allowMultipleProperty, allowMultipleExpression));
        }

        if (!attribute.Inherited)
        {
            var inheritedExpression = Expression.Constant(attribute.Inherited);
            bindings.Add(Expression.Bind(inheritedProperty, inheritedExpression));
        }

        return Expression.MemberInit(constructorExpression, bindings).AsEnumerableValue();
    }
}
