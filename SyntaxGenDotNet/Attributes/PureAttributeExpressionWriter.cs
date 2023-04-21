using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="PureAttribute" />.
/// </summary>
public sealed class PureAttributeExpressionWriter : AttributeExpressionWriter<PureAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<PureAttribute> attributes)
    {
        if (attributes.Count != 1)
        {
            return ArraySegment<Expression>.Empty;
        }

        var constructor = AttributeType.GetConstructor(Type.EmptyTypes)!;
        return Expression.MemberInit(Expression.New(constructor)).AsEnumerableValue();
    }
}
