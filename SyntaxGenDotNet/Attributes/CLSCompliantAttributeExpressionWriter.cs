using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="CLSCompliantAttribute" />.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class CLSCompliantAttributeExpressionWriter : AttributeExpressionWriter<CLSCompliantAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<CLSCompliantAttribute> attributes)
    {
        if (attributes.Count != 1)
        {
            return ArraySegment<Expression>.Empty;
        }

        CLSCompliantAttribute attribute = attributes.First();
        Type[] constructorArgumentTypes = {typeof(bool)};
        var constructor = AttributeType.GetConstructor(constructorArgumentTypes)!;
        var isCompliantExpression = Expression.Constant(attribute.IsCompliant);
        return Expression.MemberInit(Expression.New(constructor, isCompliantExpression)).AsEnumerableValue();
    }
}
