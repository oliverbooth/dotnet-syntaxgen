namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents an open brace token.
/// </summary>
public sealed class OpenBraceToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OpenBraceToken" /> class.
    /// </summary>
    public OpenBraceToken()
    {
    }

    /// <inheritdoc />
    public override string Text { get; } = "{";
}