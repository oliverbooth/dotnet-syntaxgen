using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

internal static class Operators
{
    /// <summary>
    ///     The <c>-&gt;</c> operator.
    /// </summary>
    public static readonly OperatorToken Arrow = new("->") {Whitespace = WhitespaceTrivia.Space};

    /// <summary>
    ///     The <c>=</c> operator.
    /// </summary>
    public static OperatorToken Assignment
    {
        get => new("=") {Whitespace = WhitespaceTrivia.Space};
    }

    /// <summary>
    ///     The <c>*</c> operator.
    /// </summary>
    public static OperatorToken Asterisk
    {
        get => new("*") {Whitespace = WhitespaceTrivia.Space};
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
    ///     The <c>:</c> operator.
    /// </summary>
    public static OperatorToken Colon
    {
        get =>
            new(":", false) {LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space};
    }

    /// <summary>
    ///     The <c>,</c> operator.
    /// </summary>
    public static OperatorToken Comma
    {
        get => new(",");
    }

    /// <summary>
    ///     The <c>.</c> operator.
    /// </summary>
    public static OperatorToken Dot
    {
        get => new(".");
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
}
