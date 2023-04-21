﻿using System.Linq.Expressions;
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
    /// <param name="attribute">The attribute. This may be <see langword="null" /> if the attribute is not custom.</param>
    /// <returns>The attribute expression.</returns>
    public abstract Expression CreateAttributeExpression(MemberInfo declaringMember, Attribute? attribute);
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
    public override Expression CreateAttributeExpression(MemberInfo declaringMember, Attribute? attribute)
    {
        return CreateAttributeExpression(declaringMember, (T?)attribute);
    }

    /// <summary>
    ///     Creates the attribute expression for the specified attribute.
    /// </summary>
    /// <param name="declaringMember">The type that declares the attribute.</param>
    /// <param name="attribute">The attribute. This may be <see langword="null" /> if the attribute is not custom.</param>
    /// <returns>The attribute expression.</returns>
    public abstract Expression CreateAttributeExpression(MemberInfo declaringMember, T? attribute);
}
