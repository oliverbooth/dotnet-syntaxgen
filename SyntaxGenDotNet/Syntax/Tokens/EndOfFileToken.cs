namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents an EOF token.
/// </summary>
public sealed class EndOfFileToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EndOfFileToken" /> class.
    /// </summary>
    public EndOfFileToken()
    {
        TrailingWhitespace = WhitespaceTrivia.None;
    }

    /// <inheritdoc />
    public override string Text { get; } = string.Empty;
}
