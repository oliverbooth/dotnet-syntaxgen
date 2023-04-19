namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a character literal token.
/// </summary>
public class CharLiteralToken : LiteralToken
{
    /// <summary>
    ///     Initializes a new instance of <see cref="CharLiteralToken" />.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    public CharLiteralToken(char literalValue) : this($"'{literalValue}'")
    {
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="CharLiteralToken" />.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected CharLiteralToken(string literalValue) : base(literalValue)
    {
    }
}
