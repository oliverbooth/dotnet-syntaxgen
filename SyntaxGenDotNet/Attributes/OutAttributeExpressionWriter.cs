using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="OutAttribute" />.
/// </summary>
public sealed class OutAttributeExpressionWriter : AttributeExpressionWriter<OutAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<OutAttribute> attributes)
    {
        if (attributes.Count != 1)
        {
            return ArraySegment<Expression>.Empty;
        }

        var constructor = AttributeType.GetConstructor(Type.EmptyTypes)!;
        return Expression.MemberInit(Expression.New(constructor)).AsEnumerableValue();
    }
}
