using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

internal static class Operators
{
    /// <summary>
    ///     The <c>&</c> operator.
    /// </summary>
    public static OperatorToken Ampersand
    {
        get => new("&") {TrailingWhitespace = WhitespaceTrivia.Space};
    }

    /// <summary>
    ///     The <c>=</c> operator.
    /// </summary>
    public static OperatorToken Assignment
    {
        get =>
            new("=", false) {LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space};
    }

    /// <summary>
    ///     The <c>=</c> operator.
    /// </summary>
    public static OperatorToken Asterisk
    {
        get => new("*") {TrailingWhitespace = WhitespaceTrivia.Space};
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
        get => new(":");
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
    ///     The <c>^</c> operator.
    /// </summary>
    public static OperatorToken GcTrackedPointer
    {
        get =>
            new("^") {LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space};
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
    ///     The <c>;</c> operator.
    /// </summary>
    public static OperatorToken Semicolon
    {
        get => new(";");
    }
}
