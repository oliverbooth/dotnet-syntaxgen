using System.Linq.Expressions;
using System.Reflection;

namespace SyntaxGenDotNet;

/// <summary>
///     Represents an expression writer for <see cref="SerializableAttribute" />.
/// </summary>
public sealed class SerializableAttributeExpressionWriter : AttributeExpressionWriter<SerializableAttribute>
{
    /// <inheritdoc />
    public override Expression CreateAttributeExpression(Type declaringType, SerializableAttribute? attribute)
    {
        if ((declaringType.Attributes & TypeAttributes.Serializable) != 0)
        {
            return Expression.MemberInit(Expression.New(AttributeType));
        }

        return Expression.Empty();
    }
}
