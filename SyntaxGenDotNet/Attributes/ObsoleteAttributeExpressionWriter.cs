using System.Linq.Expressions;
using System.Reflection;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="ObsoleteAttribute" />.
/// </summary>
public sealed class ObsoleteAttributeExpressionWriter : AttributeExpressionWriter<ObsoleteAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<ObsoleteAttribute> attributes)
    {
        if (attributes.Count == 0)
        {
            return ArraySegment<Expression>.Empty;
        }

        var arguments = new List<Expression>();
        var bindings = new List<MemberAssignment>();
        var types = new List<Type>();

        ObsoleteAttribute attribute = attributes.First();
        BindConstructorArguments(attribute, types, arguments);
        BindProperties(attribute, bindings);

        var constructor = attribute.GetType().GetConstructor((BindingFlags)(-1), types.ToArray())!;
        var newExpression = Expression.New(constructor, arguments);
        return Expression.MemberInit(newExpression, bindings).AsEnumerableValue();
    }

    private static void BindConstructorArguments(ObsoleteAttribute attribute,
        ICollection<Type> argumentTypes,
        ICollection<Expression> arguments)
    {
        if (attribute.Message is { } message)
        {
            argumentTypes.Add(typeof(string));
            arguments.Add(Expression.Constant(message));
        }

        if (attribute.IsError)
        {
            argumentTypes.Add(typeof(bool));
            arguments.Add(Expression.Constant(true));
        }
    }

    private static void BindProperties(ObsoleteAttribute attribute, ICollection<MemberAssignment> bindings)
    {
        if (attribute.DiagnosticId is { } diagnosticId)
        {
            var diagnosticIdProperty = attribute.GetType().GetProperty(nameof(ObsoleteAttribute.DiagnosticId))!;
            bindings.Add(Expression.Bind(diagnosticIdProperty, Expression.Constant(diagnosticId)));
        }

        if (attribute.UrlFormat is { } urlFormat)
        {
            var urlFormatProperty = attribute.GetType().GetProperty(nameof(ObsoleteAttribute.UrlFormat))!;
            bindings.Add(Expression.Bind(urlFormatProperty, Expression.Constant(urlFormat)));
        }
    }
}
