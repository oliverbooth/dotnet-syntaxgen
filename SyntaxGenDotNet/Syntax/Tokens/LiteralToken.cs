namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a literal syntax node.
/// </summary>
public class LiteralToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    /// <exception cref="ArgumentNullException"><paramref name="literalValue" /> is <see langword="null" />.</exception>
    public LiteralToken(string literalValue)
    {
        Text = literalValue ?? throw new ArgumentNullException(nameof(literalValue));
        TrailingWhitespace = WhitespaceTrivia.None;
    }

    /// <summary>
    ///     Gets the literal value.
    /// </summary>
    /// <value>The literal value.</value>
    public override string Text { get; }
}
