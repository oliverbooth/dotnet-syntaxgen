using System.Linq.Expressions;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="AttributeUsageAttribute" />.
/// </summary>
public sealed class AttributeUsageAttributeExpressionWriter : AttributeExpressionWriter<AttributeUsageAttribute>
{
    /// <inheritdoc />
    public override Expression CreateAttributeExpression(Type declaringType, AttributeUsageAttribute? attribute)
    {
        if (attribute is null)
        {
            return Expression.Empty();
        }

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

        return Expression.MemberInit(constructorExpression, bindings);
    }
}
