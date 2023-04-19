namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a boolean literal syntax node.
/// </summary>
public class BooleanLiteralToken : LiteralToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BooleanLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    public BooleanLiteralToken(bool literalValue) : this(literalValue ? "true" : "false")
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BooleanLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected BooleanLiteralToken(string literalValue) : base(literalValue)
    {
    }
}
