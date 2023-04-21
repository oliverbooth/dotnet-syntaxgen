using System.Linq.Expressions;
using System.Reflection;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="SerializableAttribute" />.
/// </summary>
public sealed class SerializableAttributeExpressionWriter : AttributeExpressionWriter<SerializableAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<SerializableAttribute> attributes)
    {
        if (declaringMember is not Type type || (type.Attributes & TypeAttributes.Serializable) == 0)
        {
            return ArraySegment<Expression>.Empty;
        }

        return Expression.MemberInit(Expression.New(AttributeType)).AsEnumerableValue();
    }
}
