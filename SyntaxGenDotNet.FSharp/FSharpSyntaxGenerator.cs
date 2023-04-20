namespace SyntaxGenDotNet.FSharp;

/// <summary>
///     Represents a syntax generator for the F# language.
/// </summary>
public partial class FSharpSyntaxGenerator : SyntaxGenerator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FSharpSyntaxGenerator" /> class.
    /// </summary>
    public FSharpSyntaxGenerator()
    {
        LanguageName = "F#";
    }
}
