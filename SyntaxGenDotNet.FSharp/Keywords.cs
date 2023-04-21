using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

internal static class Keywords
{
    /// <summary>
    ///     The <c>class</c> keyword.
    /// </summary>
    public static readonly KeywordToken ClassKeyword = new("class");

    /// <summary>
    ///     The <c>delegate</c> keyword.
    /// </summary>
    public static readonly KeywordToken DelegateKeyword = new("delegate");

    /// <summary>
    ///     The <c>false</c> keyword.
    /// </summary>
    public static readonly KeywordToken FalseKeyword = new("false");

    /// <summary>
    ///     The <c>inherit</c> keyword.
    /// </summary>
    public static readonly KeywordToken InheritKeyword = new("inherit") {LeadingWhitespace = WhitespaceTrivia.Indent};

    /// <summary>
    ///     The <c>interface</c> keyword.
    /// </summary>
    public static readonly KeywordToken InterfaceKeyword = new("interface");

    /// <summary>
    ///     The <c>internal</c> keyword.
    /// </summary>
    public static readonly KeywordToken InternalKeyword = new("internal");

    /// <summary>
    ///     The <c>member</c> keyword.
    /// </summary>
    public static readonly KeywordToken MemberKeyword = new("member");

    /// <summary>
    ///     The <c>mutable</c> keyword.
    /// </summary>
    public static readonly KeywordToken MutableKeyword = new("mutable");

    /// <summary>
    ///     The <c>null</c> keyword.
    /// </summary>
    public static readonly KeywordToken NullKeyword = new("null");

    /// <summary>
    ///     The <c>of</c> keyword.
    /// </summary>
    public static readonly KeywordToken OfKeyword = new("of");

    /// <summary>
    ///     The <c>private</c> keyword.
    /// </summary>
    public static readonly KeywordToken PrivateKeyword = new("private");

    /// <summary>
    ///     The <c>protected</c> keyword.
    /// </summary>
    public static readonly KeywordToken ProtectedKeyword = new("protected");

    /// <summary>
    ///     The <c>public</c> keyword.
    /// </summary>
    public static readonly KeywordToken PublicKeyword = new("public");

    /// <summary>
    ///     The <c>static</c> keyword.
    /// </summary>
    public static readonly KeywordToken StaticKeyword = new("static");

    /// <summary>
    ///     The <c>staticval</c> keyword.
    /// </summary>
    public static readonly KeywordToken StaticValKeyword = new("staticval");

    /// <summary>
    ///     The <c>struct</c> keyword.
    /// </summary>
    public static readonly KeywordToken StructKeyword = new("struct");

    /// <summary>
    ///     The <c>true</c> keyword.
    /// </summary>
    public static readonly KeywordToken TrueKeyword = new("true");

    /// <summary>
    ///     The <c>type</c> keyword.
    /// </summary>
    public static readonly KeywordToken TypeKeyword = new("type");

    /// <summary>
    ///     The <c>val</c> keyword.
    /// </summary>
    public static readonly KeywordToken ValKeyword = new("val");
}
