namespace SyntaxGenDotNet.CSharp;

/// <summary>
///     Represents a syntax generator for the C# language.
/// </summary>
public partial class CSharpSyntaxGenerator : SyntaxGenerator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CSharpSyntaxGenerator" /> class.
    /// </summary>
    public CSharpSyntaxGenerator()
    {
        LanguageName = "C#";
    }
}
