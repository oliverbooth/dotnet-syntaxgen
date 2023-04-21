using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Versioning;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="PureAttribute" />.
/// </summary>
public sealed class OSPlatformAttributeExpressionWriter : AttributeExpressionWriter<OSPlatformAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<OSPlatformAttribute> attributes)
    {
        if (attributes.Count == 0)
        {
            return ArraySegment<Expression>.Empty;
        }

        var expressions = new List<Expression>();

        foreach (var attribute in attributes)
        {
            var constructor = attribute.GetType().GetConstructor((BindingFlags)(-1), new[] {typeof(string)})!;
            var platformExpression = Expression.Constant(attribute.PlatformName);
            expressions.Add(Expression.MemberInit(Expression.New(constructor, platformExpression)));
        }

        return expressions.AsReadOnly();
    }
}
