using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="ExtensionAttribute" />.
/// </summary>
public sealed class ExtensionAttributeExpressionWriter : AttributeExpressionWriter<ExtensionAttribute>
{
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<ExtensionAttribute> attributes)
    {
        if (attributes.Count == 0)
        {
            return ArraySegment<Expression>.Empty;
        }

        var constructor = AttributeType.GetConstructor(Type.EmptyTypes)!;
        return Expression.MemberInit(Expression.New(constructor)).AsEnumerableValue();
    }
}
