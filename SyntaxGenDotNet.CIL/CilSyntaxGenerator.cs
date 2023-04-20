using System.Diagnostics;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CIL;

/// <summary>
///     Represents a syntax generator for CIL (Common Intermediate Language).
/// </summary>
public sealed partial class CilSyntaxGenerator : ISyntaxGenerator
{
    /// <inheritdoc />
    public string LanguageName { get; } = "IL";
}
