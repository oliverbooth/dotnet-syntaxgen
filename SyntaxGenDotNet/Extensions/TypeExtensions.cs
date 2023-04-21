using System.Reflection;

namespace SyntaxGenDotNet.Extensions;

/// <summary>
///     Extension methods for <see cref="Type" />.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    ///     Gets the direct interfaces implemented by the specified type.
    /// </summary>
    /// <param name="type">The type for which to get the direct interfaces.</param>
    /// <returns>An array of the direct interfaces implemented by the specified type.</returns>
    public static Type[] GetDirectInterfaces(this Type type)
    {
        Type[] interfaces = type.GetInterfaces();
        if (type.BaseType is null || type.BaseType == typeof(object) || type.BaseType == typeof(ValueType))
        {
            return interfaces;
        }

        var directInterfaces = new HashSet<Type>(interfaces);
        Type[] baseInterfaces = GetDirectInterfaces(type.BaseType).ToArray();
        directInterfaces.ExceptWith(baseInterfaces);
        return directInterfaces.ToArray();
    }

    /// <summary>
    ///     Returns a value indicating whether the type has a base type that is not <see cref="object" /> or
    ///     <see cref="ValueType" />.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    ///     <see langword="true" /> if the type has a base type that is not <see cref="object" /> or <see cref="ValueType" />;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
    public static bool HasBaseType(this Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        return type.BaseType is not null && type.BaseType != typeof(object) && type.BaseType != typeof(ValueType);
    }

    /// <summary>
    ///     Returns a value indicating whether the type is a delegate.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><see langword="true" /> if the type is a delegate; otherwise, <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
    public static bool IsDelegate(this Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        return type.IsSubclassOf(typeof(MulticastDelegate)) || type.IsSubclassOf(typeof(Delegate));
    }

    /// <summary>
    ///     Returns a value indicating whether the type is a generic parameter that has constraints.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    ///     <see langword="true" /> if the type is a generic parameter that has constraints; otherwise, <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
    public static bool IsGenericConstrained(this Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (!type.IsGenericParameter)
        {
            return false;
        }

        return type.GenericParameterAttributes != GenericParameterAttributes.None ||
               type.GetGenericParameterConstraints().Length > 0;
    }
}
