using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet;

/// <summary>
///     Predefined IL operators.
/// </summary>
public static class ILOperators
{
    /// <summary>
    ///     The IL operator which separates the type name and its generic argument count.
    /// </summary>
    /// <value><c>`</c></value>
    public static readonly OperatorToken GenericMarker = new("`");

    /// <summary>
    ///     The IL operator which separates namespaces.
    /// </summary>
    /// <value><c>.</c></value>
    public static readonly OperatorToken NamespaceSeparator = new(".");
}
