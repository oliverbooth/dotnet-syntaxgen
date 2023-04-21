using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

internal static class Keywords
{
    /// <summary>
    ///     The <c>abstract</c> keyword.
    /// </summary>
    public static readonly KeywordToken AbstractKeyword = new("abstract");

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
    ///     The <c>generic</c> keyword.
    /// </summary>
    public static readonly KeywordToken GenericKeyword = new("generic");

    /// <summary>
    ///     The <c>initonly</c> keyword.
    /// </summary>
    public static readonly KeywordToken InitOnlyKeyword = new("initonly");

    /// <summary>
    ///     The <c>internal</c> keyword.
    /// </summary>
    public static readonly KeywordToken InternalKeyword = new("internal");

    /// <summary>
    ///     The <c>null</c> keyword.
    /// </summary>
    public static readonly KeywordToken NullKeyword = new("null");

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
    ///     The <c>true</c> keyword.
    /// </summary>
    public static readonly KeywordToken TrueKeyword = new("true");

    /// <summary>
    ///     The <c>typename</c> keyword.
    /// </summary>
    public static readonly KeywordToken TypeNameKeyword = new("typename");

    /// <summary>
    ///     The <c>value</c> keyword.
    /// </summary>
    public static readonly KeywordToken ValueKeyword = new("value");
}
