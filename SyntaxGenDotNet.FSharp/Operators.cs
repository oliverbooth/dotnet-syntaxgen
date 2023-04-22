﻿using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

internal static class Operators
{
    /// <summary>
    ///     The <c>-&gt;</c> operator.
    /// </summary>
    public static readonly OperatorToken Arrow = new("->", false) {TrailingWhitespace = WhitespaceTrivia.Space};

    /// <summary>
    ///     The <c>=</c> operator.
    /// </summary>
    public static readonly OperatorToken Assignment = new("=", false) {Whitespace = WhitespaceTrivia.Space};

    /// <summary>
    ///     The <c>*</c> operator.
    /// </summary>
    public static readonly OperatorToken Asterisk = new("*", false)
    {
        LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space
    };

    /// <summary>
    ///     The <c>]</c> operator.
    /// </summary>
    public static readonly OperatorToken CloseBracket = new("]");

    /// <summary>
    ///     The <c>&gt;</c> operator.
    /// </summary>
    public static readonly OperatorToken CloseChevron = new(">");

    /// <summary>
    ///     The <c>)</c> operator.
    /// </summary>
    public static readonly OperatorToken CloseParenthesis = new(")");

    /// <summary>
    ///     The <c>:</c> operator.
    /// </summary>
    public static readonly OperatorToken Colon = new(":", false)
    {
        LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space
    };

    /// <summary>
    ///     The <c>,</c> operator.
    /// </summary>
    public static readonly OperatorToken Comma = new(",");

    /// <summary>
    ///     The <c>.</c> operator.
    /// </summary>
    public static readonly OperatorToken Dot = new(".");

    /// <summary>
    ///     The <c>[</c> operator.
    /// </summary>
    public static readonly OperatorToken OpenBracket = new("[");

    /// <summary>
    ///     The <c>&lt;</c> operator.
    /// </summary>
    public static readonly OperatorToken OpenChevron = new("<");

    /// <summary>
    ///     The <c>(</c> operator.
    /// </summary>
    public static readonly OperatorToken OpenParenthesis = new("(");
}
