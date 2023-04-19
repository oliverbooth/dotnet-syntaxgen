namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a keyword syntax node.
/// </summary>
public class KeywordToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="KeywordToken" /> class.
    /// </summary>
    /// <param name="keyword">The keyword.</param>
    /// <exception cref="ArgumentNullException"><paramref name="keyword" /> is <see langword="null" />.</exception>
    public KeywordToken(string keyword)
    {
        Text = keyword ?? throw new ArgumentNullException(nameof(keyword));
    }

    /// <summary>
    ///     Gets the keyword.
    /// </summary>
    /// <value>The keyword.</value>
    public override string Text { get; }
}
