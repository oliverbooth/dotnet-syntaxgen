namespace SyntaxGenDotNet.FSharp;

/// <summary>
///     Represents a syntax generator for the F# language.
/// </summary>
public partial class FSharpSyntaxGenerator : ISyntaxGenerator
{
    /// <inheritdoc />
    public string LanguageName { get; } = "F#";
}
