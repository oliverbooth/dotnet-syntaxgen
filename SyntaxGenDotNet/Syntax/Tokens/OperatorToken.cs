namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents an operator syntax node.
/// </summary>
public sealed class OperatorToken : SyntaxToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OperatorToken" /> class.
    /// </summary>
    /// <param name="operatorValue">The operator.</param>
    /// <param name="stripTrailingWhitespace">
    ///     A value indicating whether to strip trailing whitespace from the preceding syntax node.
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="operatorValue" /> is <see langword="null" />.</exception>
    public OperatorToken(string operatorValue, bool stripTrailingWhitespace = true)
    {
        Text = operatorValue ?? throw new ArgumentNullException(nameof(operatorValue));
        TrailingWhitespace = WhitespaceTrivia.None;
        StripTrailingWhitespace = stripTrailingWhitespace;
    }

    /// <summary>
    ///     Gets the operator.
    /// </summary>
    /// <value>The operator.</value>
    public override string Text { get; }
}
