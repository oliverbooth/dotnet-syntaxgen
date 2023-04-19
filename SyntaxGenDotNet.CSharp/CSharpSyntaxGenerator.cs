namespace SyntaxGenDotNet.CSharp;

/// <summary>
///     Represents a syntax generator for the C# language.
/// </summary>
public partial class CSharpSyntaxGenerator : ISyntaxGenerator
{
    /// <inheritdoc />
    public string LanguageName { get; } = "C#";
}
