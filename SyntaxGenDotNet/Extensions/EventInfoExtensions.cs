using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SyntaxGenDotNet.Extensions;

/// <summary>
///     Extensions for <see cref="EventInfo" />.
/// </summary>
public static class EventInfoExtensions
{
    private static readonly MethodAttributes[] AttributeOrder =
    {
        MethodAttributes.Public, MethodAttributes.FamORAssem, MethodAttributes.Family, MethodAttributes.Assembly,
        MethodAttributes.FamANDAssem, MethodAttributes.Private
    };

    /// <summary>
    ///     Gets the most accessible method for the specified <paramref name="eventInfo" />.
    /// </summary>
    /// <param name="eventInfo">The event info.</param>
    /// <returns>The most accessible method for the specified <paramref name="eventInfo" />.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="eventInfo" /> is <see langword="null" />.</exception>
    /// <exception cref="InvalidOperationException">The event info has no accessible methods.</exception>
    public static MethodInfo GetMostAccessibleMethod(this EventInfo eventInfo)
    {
        if (eventInfo is null)
        {
            throw new ArgumentNullException(nameof(eventInfo));
        }

        if (TryGetSingleAccessor(eventInfo, out var methodInfo))
        {
            return methodInfo;
        }

        MethodInfo? addMethod = eventInfo.AddMethod;
        MethodInfo? removeMethod = eventInfo.RemoveMethod;

        for (var index = 0; index < AttributeOrder.Length; index++)
        {
            MethodAttributes current = AttributeOrder[index];
            if ((addMethod?.Attributes & current) == current)
            {
                return addMethod!;
            }

            if ((removeMethod?.Attributes & current) == current)
            {
                return removeMethod!;
            }
        }

        throw new InvalidOperationException();
    }

    /// <summary>
    ///     Returns a value indicating whether the specified event is <c>static</c> (<c>Shared</c> in Visual Basic).
    /// </summary>
    /// <param name="eventInfo">The event to check.</param>
    /// <returns>
    ///     <see langword="true" /> if the specified event is <c>static</c> (<c>Shared</c> in Visual Basic); otherwise,
    ///     <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="eventInfo" /> is <see langword="null" />.</exception>
    /// <remarks>
    ///     A event is determined as <see langword="static" /> if either the add or remove method has the
    ///     <see cref="MethodAttributes.Static" /> attribute.
    /// </remarks>
    public static bool IsStatic(this EventInfo eventInfo)
    {
        if (eventInfo is null)
        {
            throw new ArgumentNullException(nameof(eventInfo));
        }

        return (eventInfo.AddMethod?.Attributes & MethodAttributes.Static) == MethodAttributes.Static ||
               (eventInfo.RemoveMethod?.Attributes & MethodAttributes.Static) == MethodAttributes.Static;
    }

    private static bool TryGetSingleAccessor(EventInfo eventInfo, [NotNullWhen(true)] out MethodInfo? methodInfo)
    {
        MethodInfo? addMethod = eventInfo.AddMethod;
        MethodInfo? removeMethod = eventInfo.RemoveMethod;

        if (addMethod is null)
        {
            methodInfo = removeMethod ?? throw new InvalidOperationException();
            return true;
        }

        if (removeMethod is null)
        {
            methodInfo = addMethod;
            return true;
        }

        methodInfo = null;
        return false;
    }
}
