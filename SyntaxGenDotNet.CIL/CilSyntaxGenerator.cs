namespace SyntaxGenDotNet.CIL;

/// <summary>
///     Represents a syntax generator for CIL (Common Intermediate Language).
/// </summary>
public sealed partial class CilSyntaxGenerator : SyntaxGenerator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CilSyntaxGenerator" /> class.
    /// </summary>
    public CilSyntaxGenerator()
    {
        LanguageName = "CIL";
    }
}
