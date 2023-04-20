﻿using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

internal static class Operators
{
    /// <summary>
    ///     The <c>=</c> operator.
    /// </summary>
    public static readonly OperatorToken Assignment = new("=", false)
    {
        LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space
    };

    /// <summary>
    ///     The <c>&gt;</c> operator.
    /// </summary>
    public static readonly OperatorToken CloseChevron = new(">");

    /// <summary>
    ///     The <c>:</c> operator.
    /// </summary>
    public static readonly OperatorToken Colon = new(":");

    /// <summary>
    ///     The <c>::</c> operator.
    /// </summary>
    public static readonly OperatorToken ColonColon = new("::");

    /// <summary>
    ///     The <c>,</c> operator.
    /// </summary>
    public static readonly OperatorToken Comma = new(",");

    /// <summary>
    ///     The <c>^</c> operator.
    /// </summary>
    public static readonly OperatorToken GcTrackedPointer = new("^")
    {
        LeadingWhitespace = WhitespaceTrivia.Space, TrailingWhitespace = WhitespaceTrivia.Space
    };

    /// <summary>
    ///     The <c>&lt;</c> operator.
    /// </summary>
    public static readonly OperatorToken OpenChevron = new("<");

    /// <summary>
    ///     The <c>;</c> operator.
    /// </summary>
    public static readonly OperatorToken Semicolon = new(";");
}
