using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="CLSCompliantAttribute" />.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class CLSCompliantAttributeExpressionWriter : AttributeExpressionWriter<CLSCompliantAttribute>
{
    /// <inheritdoc />
    public override Expression CreateAttributeExpression(Type declaringType, CLSCompliantAttribute? attribute)
    {
        if (attribute is null)
        {
            return Expression.Empty();
        }

        Type[] constructorArgumentTypes = {typeof(bool)};
        var constructor = AttributeType.GetConstructor(constructorArgumentTypes)!;
        var isCompliantExpression = Expression.Constant(attribute.IsCompliant);
        return Expression.New(constructor, isCompliantExpression);
    }
}
