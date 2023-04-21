using System.Linq.Expressions;
using System.Reflection;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="SerializableAttribute" />.
/// </summary>
public sealed class SerializableAttributeExpressionWriter : AttributeExpressionWriter<SerializableAttribute>
{
    /// <inheritdoc />
    public override Expression CreateAttributeExpression(MemberInfo declaringMember, SerializableAttribute? attribute)
    {
        if (declaringMember is not Type type || (type.Attributes & TypeAttributes.Serializable) == 0)
        {
            return Expression.Empty();
        }

        return Expression.MemberInit(Expression.New(AttributeType));
    }
}
