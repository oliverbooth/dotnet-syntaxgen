namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents an identifier syntax node.
/// </summary>
public class IdentifierToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="IdentifierToken" /> class.
    /// </summary>
    /// <param name="identifier">The identifier.</param>
    /// <exception cref="ArgumentNullException"><paramref name="identifier" /> is <see langword="null" />.</exception>
    public IdentifierToken(string identifier)
    {
        Text = identifier ?? throw new ArgumentNullException(nameof(identifier));
        TrailingWhitespace = WhitespaceTrivia.None;
    }

    /// <summary>
    ///     Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public override string Text { get; }
}
