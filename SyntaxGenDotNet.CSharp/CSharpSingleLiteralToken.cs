using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

/// <summary>
///     Represents a C# <see cref="float" /> literal token.
/// </summary>
internal sealed class CSharpSingleLiteralToken : FloatingPointLiteralToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CSharpSingleLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    public CSharpSingleLiteralToken(float literalValue) : base($"{literalValue}f")
    {
    }
}
