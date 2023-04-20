namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a comment token.
/// </summary>
public class CommentToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CommentToken" /> class.
    /// </summary>
    /// <param name="comment">The comment.</param>
    public CommentToken(string? comment)
    {
        Text = $"// {comment}";
        LeadingWhitespace = WhitespaceTrivia.Space;
    }

    /// <inheritdoc />
    public override string Text { get; }
}
