using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

internal static class Keywords
{
    /// <summary>
    ///     The <c>abstract</c> keyword.
    /// </summary>
    public static readonly KeywordToken AbstractKeyword = new("abstract");

    /// <summary>
    ///     The <c>const</c> keyword.
    /// </summary>
    public static readonly KeywordToken ConstKeyword = new("const");

    /// <summary>
    ///     The <c>class</c> keyword.
    /// </summary>
    public static readonly KeywordToken ClassKeyword = new("class");

    /// <summary>
    ///     The <c>enum</c> keyword.
    /// </summary>
    public static readonly KeywordToken EnumKeyword = new("enum");

    /// <summary>
    ///     The <c>delegate</c> keyword.
    /// </summary>
    public static readonly KeywordToken DelegateKeyword = new("delegate");

    /// <summary>
    ///     The <c>false</c> keyword.
    /// </summary>
    public static readonly KeywordToken FalseKeyword = new("false");

    /// <summary>
    ///     The <c>in</c> keyword.
    /// </summary>
    public static readonly KeywordToken InKeyword = new("in");

    /// <summary>
    ///     The <c>interface</c> keyword.
    /// </summary>
    public static readonly KeywordToken InterfaceKeyword = new("interface");

    /// <summary>
    ///     The <c>internal</c> keyword.
    /// </summary>
    public static readonly KeywordToken InternalKeyword = new("internal");

    /// <summary>
    ///     The <c>null</c> keyword.
    /// </summary>
    public static readonly KeywordToken NullKeyword = new("null");

    /// <summary>
    ///     The <c>out</c> keyword.
    /// </summary>
    public static readonly KeywordToken OutKeyword = new("out");

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
    ///     The <c>readonly</c> keyword.
    /// </summary>
    public static readonly KeywordToken ReadOnlyKeyword = new("readonly");

    /// <summary>
    ///     The <c>ref</c> keyword.
    /// </summary>
    public static readonly KeywordToken RefKeyword = new("ref");

    /// <summary>
    ///     The <c>sealed</c> keyword.
    /// </summary>
    public static readonly KeywordToken SealedKeyword = new("sealed");

    /// <summary>
    ///     The <c>static</c> keyword.
    /// </summary>
    public static readonly KeywordToken StaticKeyword = new("static");

    /// <summary>
    ///     The <c>struct</c> keyword.
    /// </summary>
    public static readonly KeywordToken StructKeyword = new("struct");

    /// <summary>
    ///     The <c>true</c> keyword.
    /// </summary>
    public static readonly KeywordToken TrueKeyword = new("true");
}
