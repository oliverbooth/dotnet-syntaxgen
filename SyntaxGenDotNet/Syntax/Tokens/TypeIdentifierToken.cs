namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a type identifier token.
/// </summary>
public sealed class TypeIdentifierToken : IdentifierToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeIdentifierToken" /> class.
    /// </summary>
    /// <param name="identifier">The identifier.</param>
    public TypeIdentifierToken(string identifier) : base(identifier)
    {
    }
}
