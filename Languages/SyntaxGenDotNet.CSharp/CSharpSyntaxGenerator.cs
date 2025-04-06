using SyntaxGenDotNet.Attributes;

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

        // in C#, extension methods are written as static methods with the first parameter
        // containing the "this" modifier, so we don't need to write the attribute explicitly.
        AttributeExpressionWriters.RemoveAll(w => w is ExtensionAttributeExpressionWriter);
    }
}
