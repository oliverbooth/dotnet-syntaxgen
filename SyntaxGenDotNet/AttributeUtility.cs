using System.Reflection;

namespace SyntaxGenDotNet;

/// <summary>
///     Provides utility methods for attributes.
/// </summary>
public static class AttributeUtility
{
    /// <summary>
    ///     Gets the value of an attribute's parameter.
    /// </summary>
    /// <param name="attribute">The attribute.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="attribute" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="parameter" /> is <see langword="null" />.</para>
    /// </exception>
    public static object? GetAttributeParameter(Attribute attribute, ParameterInfo parameter)
    {
        if (attribute is null)
        {
            throw new ArgumentNullException(nameof(attribute));
        }

        if (parameter is null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        PropertyInfo[] properties = attribute.GetType().GetProperties();

        switch (properties.Length)
        {
            case 0:
                return attribute;
            case 1:
                return properties[0].GetValue(attribute);
        }

        for (var index = 0; index < properties.Length; index++)
        {
            PropertyInfo property = properties[index];
            if (property.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase))
            {
                return property.GetValue(attribute);
            }
        }

        return attribute;
    }
}
