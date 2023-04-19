namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a string literal token.
/// </summary>
public sealed class StringLiteralToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StringLiteralToken" /> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public StringLiteralToken(string text)
    {
        Text = $"\"{text}\"";
    }

    /// <summary>
    ///     Gets the text of the token.
    /// </summary>
    /// <value>The text of the token.</value>
    public override string Text { get; }
}
