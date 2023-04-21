using System.Linq.Expressions;
using System.Reflection;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents a writer for an attribute expression.
/// </summary>
public abstract class AttributeExpressionWriter
{
    /// <summary>
    ///     Gets the type of the attribute.
    /// </summary>
    /// <value>The type of the attribute.</value>
    public abstract Type AttributeType { get; }

    /// <summary>
    ///     Creates the attribute expression for the specified attribute.
    /// </summary>
    /// <param name="declaringMember">The type that declares the attribute.</param>
    /// <param name="attributes">An enumerable collection of matching attributes.</param>
    /// <returns>An read-only collection of attribute expressions which correspond <paramref name="attributes" />.</returns>
    public abstract IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<Attribute> attributes);
}

/// <summary>
///     Represents a writer for an attribute expression.
/// </summary>
public abstract class AttributeExpressionWriter<T> : AttributeExpressionWriter
    where T : Attribute
{
    /// <inheritdoc />
    public override Type AttributeType { get; } = typeof(T);

    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<Attribute> attributes)
    {
        return CreateAttributeExpressions(declaringMember, attributes.OfType<T>().ToArray());
    }

    /// <summary>
    ///     Creates the attribute expression for the specified attribute.
    /// </summary>
    /// <param name="declaringMember">The type that declares the attribute.</param>
    /// <param name="attributes">An enumerable collection of matching attributes.</param>
    /// <returns>An read-only collection of attribute expressions which correspond <paramref name="attributes" />.</returns>
    public abstract IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<T> attributes);
}
