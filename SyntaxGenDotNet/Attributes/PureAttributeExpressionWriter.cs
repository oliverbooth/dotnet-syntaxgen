using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="PureAttribute" />.
/// </summary>
public sealed class PureAttributeExpressionWriter : AttributeExpressionWriter<PureAttribute>
{
    /// <inheritdoc />
    public override Expression CreateAttributeExpression(MemberInfo declaringMember, PureAttribute? attribute)
    {
        if (attribute is null)
        {
            return Expression.Empty();
        }

        var constructor = AttributeType.GetConstructor(Type.EmptyTypes)!;
        return Expression.MemberInit(Expression.New(constructor));
    }
}
