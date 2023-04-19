namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a literal syntax node for a floating-point number.
/// </summary>
public class FloatingPointLiteralToken : NumericLiteralToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FloatingPointLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    public FloatingPointLiteralToken(double literalValue) : base(literalValue)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FloatingPointLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected FloatingPointLiteralToken(string literalValue) : base(literalValue)
    {
    }
}
