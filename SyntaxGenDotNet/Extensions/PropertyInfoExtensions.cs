using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SyntaxGenDotNet.Extensions;

/// <summary>
///     Extensions for <see cref="PropertyInfo" />.
/// </summary>
public static class PropertyInfoExtensions
{
    private static readonly MethodAttributes[] AttributeOrder =
    {
        MethodAttributes.Public, MethodAttributes.FamORAssem, MethodAttributes.Family, MethodAttributes.Assembly,
        MethodAttributes.FamANDAssem, MethodAttributes.Private
    };

    /// <summary>
    ///     Gets the most accessible method for the specified <paramref name="propertyInfo" />.
    /// </summary>
    /// <param name="propertyInfo">The property info.</param>
    /// <returns>The most accessible method for the specified <paramref name="propertyInfo" />.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="propertyInfo" /> is <see langword="null" />.</exception>
    /// <exception cref="InvalidOperationException">The property info has no accessible methods.</exception>
    public static MethodInfo GetMostAccessibleMethod(this PropertyInfo propertyInfo)
    {
        if (propertyInfo is null)
        {
            throw new ArgumentNullException(nameof(propertyInfo));
        }

        if (TryGetSingleAccessor(propertyInfo, out var methodInfo))
        {
            return methodInfo;
        }

        MethodInfo? getMethod = propertyInfo.GetMethod;
        MethodInfo? setMethod = propertyInfo.SetMethod;

        for (var index = 0; index < AttributeOrder.Length; index++)
        {
            MethodAttributes current = AttributeOrder[index];
            if ((getMethod?.Attributes & current) == current)
            {
                return getMethod!;
            }

            if ((setMethod?.Attributes & current) == current)
            {
                return setMethod!;
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    ///     Returns a value indicating whether the specified property is <c>static</c> (<c>Shared</c> in Visual Basic).
    /// </summary>
    /// <param name="property">The property to check.</param>
    /// <returns>
    ///     <see langword="true" /> if the specified property is <c>static</c> (<c>Shared</c> in Visual Basic); otherwise,
    ///     <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="property" /> is <see langword="null" />.</exception>
    /// <remarks>
    ///     A property is determined as <see langword="static" /> if either the get or set method has the
    ///     <see cref="MethodAttributes.Static" /> attribute.
    /// </remarks>
    public static bool IsStatic(this PropertyInfo property)
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        return (property.GetMethod?.Attributes & MethodAttributes.Static) == MethodAttributes.Static ||
               (property.SetMethod?.Attributes & MethodAttributes.Static) == MethodAttributes.Static;
    }

    private static bool TryGetSingleAccessor(PropertyInfo propertyInfo, [NotNullWhen(true)] out MethodInfo? methodInfo)
    {
        MethodInfo? getMethod = propertyInfo.GetMethod;
        MethodInfo? setMethod = propertyInfo.SetMethod;

        if (getMethod is null)
        {
            methodInfo = setMethod ?? throw new InvalidOperationException();
            return true;
        }

        if (setMethod is null)
        {
            methodInfo = getMethod;
            return true;
        }

        methodInfo = null;
        return false;
    }
}
