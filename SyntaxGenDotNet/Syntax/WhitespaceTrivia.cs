namespace SyntaxGenDotNet.Syntax;

/// <summary>
///     Represents a whitespace trivia.
/// </summary>
public readonly struct WhitespaceTrivia
{
    /// <summary>
    ///     Represents no whitespace.
    /// </summary>
    public static readonly WhitespaceTrivia None = new(string.Empty);

    /// <summary>
    ///     Represents a single space.
    /// </summary>
    public static readonly WhitespaceTrivia Space = new(" ");

    /// <summary>
    ///     Represents indentation; that is, a new line followed by four spaces.
    /// </summary>
    public static readonly WhitespaceTrivia Indent = new(Environment.NewLine + "    ");

    /// <summary>
    ///     Represents a new line.
    /// </summary> 
    public static readonly WhitespaceTrivia NewLine = new(Environment.NewLine);

    private readonly string _value = string.Empty;

    private WhitespaceTrivia(string value)
    {
        _value = value;
    }

    /// <summary>
    ///     Implicitly converts a <see cref="string" /> to a <see cref="WhitespaceTrivia" />.
    /// </summary>
    /// <param name="value">The whitespace string to convert.</param>
    /// <returns>The whitespace trivia.</returns>
    public static implicit operator WhitespaceTrivia(string value)
    {
        return new WhitespaceTrivia(value);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _value;
    }
}
