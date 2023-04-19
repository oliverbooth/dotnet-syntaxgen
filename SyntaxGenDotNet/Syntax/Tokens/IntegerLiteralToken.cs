namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a literal syntax node for an integer.
/// </summary>
public class IntegerLiteralToken : NumericLiteralToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FloatingPointLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    public IntegerLiteralToken(long literalValue) : base(literalValue)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FloatingPointLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected IntegerLiteralToken(string literalValue) : base(literalValue)
    {
    }
}
