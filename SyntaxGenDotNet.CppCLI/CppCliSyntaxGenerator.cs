namespace SyntaxGenDotNet.CppCLI;

/// <summary>
///     Represents a syntax generator for the C++/CLI language.
/// </summary>
public sealed partial class CppCliSyntaxGenerator : SyntaxGenerator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CppCliSyntaxGenerator" /> class.
    /// </summary>
    public CppCliSyntaxGenerator()
    {
        LanguageName = "C++/CLI";
    }
}
