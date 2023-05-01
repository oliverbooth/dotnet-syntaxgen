using System.Diagnostics.CodeAnalysis;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

[SuppressMessage("ReSharper", "CommentTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
internal static class Keywords
{
    /// <summary>
    ///     The <c>abstract</c> keyword.
    /// </summary>
    public static KeywordToken AbstractKeyword
    {
        get => new("abstract");
    }

    /// <summary>
    ///     The <c>class</c> keyword.
    /// </summary>
    public static KeywordToken ClassKeyword
    {
        get => new("class");
    }

    /// <summary>
    ///     The <c>delegate</c> keyword.
    /// </summary>
    public static KeywordToken DelegateKeyword
    {
        get => new("delegate");
    }

    /// <summary>
    ///     The <c>false</c> keyword.
    /// </summary>
    public static KeywordToken FalseKeyword
    {
        get => new("false");
    }

    /// <summary>
    ///     The <c>inherit</c> keyword.
    /// </summary>
    public static KeywordToken InheritKeyword
    {
        get => new("inherit") {LeadingWhitespace = WhitespaceTrivia.Indent};
    }

    /// <summary>
    ///     The <c>interface</c> keyword.
    /// </summary>
    public static KeywordToken InterfaceKeyword
    {
        get => new("interface");
    }

    /// <summary>
    ///     The <c>internal</c> keyword.
    /// </summary>
    public static KeywordToken InternalKeyword
    {
        get => new("internal");
    }

    /// <summary>
    ///     The <c>member</c> keyword.
    /// </summary>
    public static KeywordToken MemberKeyword
    {
        get => new("member");
    }

    /// <summary>
    ///     The <c>mutable</c> keyword.
    /// </summary>
    public static KeywordToken MutableKeyword
    {
        get => new("mutable");
    }

    /// <summary>
    ///     The <c>null</c> keyword.
    /// </summary>
    public static KeywordToken NullKeyword
    {
        get => new("null");
    }

    /// <summary>
    ///     The <c>of</c> keyword.
    /// </summary>
    public static KeywordToken OfKeyword
    {
        get => new("of");
    }

    /// <summary>
    ///     The <c>override</c> keyword.
    /// </summary>
    public static KeywordToken OverrideKeyword
    {
        get => new("override");
    }

    /// <summary>
    ///     The <c>private</c> keyword.
    /// </summary>
    public static KeywordToken PrivateKeyword
    {
        get => new("private");
    }

    /// <summary>
    ///     The <c>protected</c> keyword.
    /// </summary>
    public static KeywordToken ProtectedKeyword
    {
        get => new("protected");
    }

    /// <summary>
    ///     The <c>public</c> keyword.
    /// </summary>
    public static KeywordToken PublicKeyword
    {
        get => new("public");
    }

    /// <summary>
    ///     The <c>static</c> keyword.
    /// </summary>
    public static KeywordToken StaticKeyword
    {
        get => new("static");
    }

    /// <summary>
    ///     The <c>staticval</c> keyword.
    /// </summary>
    public static KeywordToken StaticValKeyword
    {
        get => new("staticval");
    }

    /// <summary>
    ///     The <c>struct</c> keyword.
    /// </summary>
    public static KeywordToken StructKeyword
    {
        get => new("struct");
    }

    /// <summary>
    ///     The <c>this</c> keyword.
    /// </summary>
    public static KeywordToken ThisKeyword
    {
        get => new("this");
    }

    /// <summary>
    ///     The <c>true</c> keyword.
    /// </summary>
    public static KeywordToken TrueKeyword
    {
        get => new("true");
    }

    /// <summary>
    ///     The <c>type</c> keyword.
    /// </summary>
    public static KeywordToken TypeKeyword
    {
        get => new("type");
    }

    /// <summary>
    ///     The <c>val</c> keyword.
    /// </summary>
    public static KeywordToken ValKeyword
    {
        get => new("val");
    }
}
