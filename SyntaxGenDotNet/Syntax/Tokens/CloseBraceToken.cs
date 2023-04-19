namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a close brace token.
/// </summary>
public sealed class CloseBraceToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CloseBraceToken" /> class.
    /// </summary>
    public CloseBraceToken()
    {
    }

    /// <inheritdoc />
    public override string Text { get; } = "}";
}
