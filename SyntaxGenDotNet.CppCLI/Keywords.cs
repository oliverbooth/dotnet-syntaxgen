using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

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
    ///     The <c>enum</c> keyword.
    /// </summary>
    public static KeywordToken EnumKeyword
    {
        get => new("enum");
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
    ///     The <c>generic</c> keyword.
    /// </summary>
    public static KeywordToken GenericKeyword
    {
        get => new("generic");
    }

    /// <summary>
    ///     The <c>initonly</c> keyword.
    /// </summary>
    public static KeywordToken InitOnlyKeyword
    {
        get => new("initonly");
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
    ///     The <c>null</c> keyword.
    /// </summary>
    public static KeywordToken NullKeyword
    {
        get => new("null");
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
    ///     The <c>readonly</c> keyword.
    /// </summary>
    public static KeywordToken RefKeyword
    {
        get => new("ref");
    }

    /// <summary>
    ///     The <c>sealed</c> keyword.
    /// </summary>
    public static KeywordToken SealedKeyword
    {
        get => new("sealed");
    }

    /// <summary>
    ///     The <c>static</c> keyword.
    /// </summary>
    public static KeywordToken StaticKeyword
    {
        get => new("static");
    }

    /// <summary>
    ///     The <c>true</c> keyword.
    /// </summary>
    public static KeywordToken TrueKeyword
    {
        get => new("true");
    }

    /// <summary>
    ///     The <c>typename</c> keyword.
    /// </summary>
    public static KeywordToken TypeNameKeyword
    {
        get => new("typename");
    }

    /// <summary>
    ///     The <c>value</c> keyword.
    /// </summary>
    public static KeywordToken ValueKeyword
    {
        get => new("value");
    }

    /// <summary>
    ///     The <c>virtual</c> keyword.
    /// </summary>
    public static KeywordToken VirtualKeyword
    {
        get => new("virtual");
    }
}
