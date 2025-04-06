using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

/// <summary>
///     Represents a CIL character literal token.
/// </summary>
internal sealed class CilCharLiteralToken : NumericLiteralToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CilCharLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    public CilCharLiteralToken(char literalValue) : base($"0x{(int)literalValue:X4}")
    {
    }
}
