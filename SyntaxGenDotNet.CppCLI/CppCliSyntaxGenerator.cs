using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CppCLI;

/// <summary>
///     Represents a syntax generator for the C++/CLI language.
/// </summary>
public sealed partial class CppCliSyntaxGenerator : ISyntaxGenerator
{
    /// <inheritdoc />
    public string LanguageName { get; } = "C++/CLI";
}
