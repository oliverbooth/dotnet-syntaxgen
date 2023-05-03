using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

internal static class Operators
{
    /// <summary>
    ///     The <c>=</c> operator.
    /// </summary>
    public static OperatorToken Assignment
    {
        get => new("=", false) {LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space};
    }

    /// <summary>
    ///     The <c>}</c> operator.
    /// </summary>
    public static OperatorToken CloseBrace
    {
        get => new("}", false);
    }

    /// <summary>
    ///     The <c>]</c> operator.
    /// </summary>
    public static OperatorToken CloseBracket
    {
        get => new("]");
    }

    /// <summary>
    ///     The <c>&gt;</c> operator.
    /// </summary>
    public static OperatorToken CloseChevron
    {
        get => new(">");
    }

    /// <summary>
    ///     The <c>)</c> operator.
    /// </summary>
    public static OperatorToken CloseParenthesis
    {
        get => new(")");
    }

    /// <summary>
    ///     The <c>::</c> operator.
    /// </summary>
    public static OperatorToken ColonColon
    {
        get => new("::");
    }

    /// <summary>
    ///     The <c>,</c> operator.
    /// </summary>
    public static OperatorToken Comma
    {
        get => new(",") {TrailingWhitespace = WhitespaceTrivia.Space};
    }

    /// <summary>
    ///     The <c>-</c> operator.
    /// </summary>
    /// <remarks>
    ///     While this is technically an operator, it is treated as a keyword in CIL, so this field is of type
    ///     <see cref="KeywordToken" />.
    /// </remarks>
    public static KeywordToken Contravariant
    {
        get => new("-");
    }

    /// <summary>
    ///     The <c>+</c> operator.
    /// </summary>
    /// <remarks>
    ///     While this is technically an operator, it is treated as a keyword in CIL, so this field is of type
    ///     <see cref="KeywordToken" />.
    /// </remarks>
    public static KeywordToken Covariant
    {
        get => new("+");
    }

    /// <summary>
    ///     The <c>{</c> operator.
    /// </summary>
    public static OperatorToken OpenBrace
    {
        get => new("{", false);
    }

    /// <summary>
    ///     The <c>[</c> operator.
    /// </summary>
    public static OperatorToken OpenBracket
    {
        get => new("[");
    }

    /// <summary>
    ///     The <c>&lt;</c> operator.
    /// </summary>
    public static OperatorToken OpenChevron
    {
        get => new("<");
    }

    /// <summary>
    ///     The <c>(</c> operator.
    /// </summary>
    public static OperatorToken OpenParenthesis
    {
        get => new("(");
    }
    
    /// <summary>
    ///     The <c>/</c> operator.
    /// </summary>
    public static OperatorToken Slash
    {
        get => new("/");
    }
}
