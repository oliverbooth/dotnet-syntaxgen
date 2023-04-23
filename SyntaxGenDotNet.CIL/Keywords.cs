using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

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
    ///     The <c>ansi</c> keyword.
    /// </summary>
    public static KeywordToken AnsiKeyword
    {
        get => new("ansi");
    }

    /// <summary>
    ///     The <c>assembly</c> keyword.
    /// </summary>
    public static KeywordToken AssemblyKeyword
    {
        get => new("assembly");
    }

    /// <summary>
    ///     The <c>auto</c> keyword.
    /// </summary>
    public static KeywordToken AutoKeyword
    {
        get => new("auto");
    }

    /// <summary>
    ///     The <c>autochar</c> keyword.
    /// </summary>
    public static KeywordToken AutoCharKeyword
    {
        get => new("autochar");
    }

    /// <summary>
    ///     The <c>beforefieldinit</c> keyword.
    /// </summary>
    public static KeywordToken BeforeFieldInitKeyword
    {
        get => new("beforefieldinit");
    }

    /// <summary>
    ///     The <c>.class</c> keyword.
    /// </summary>
    public static KeywordToken ClassDeclaration
    {
        get => new(".class");
    }

    /// <summary>
    ///     The <c>class</c> keyword.
    /// </summary>
    public static KeywordToken ClassKeyword
    {
        get => new("class");
    }

    /// <summary>
    ///     The <c>explicit</c> keyword.
    /// </summary>
    public static KeywordToken ExplicitKeyword
    {
        get => new("explicit");
    }

    /// <summary>
    ///     The <c>extends</c> keyword.
    /// </summary>
    public static KeywordToken ExtendsKeyword
    {
        get => new("extends");
    }

    /// <summary>
    ///     The <c>false</c> keyword.
    /// </summary>
    public static KeywordToken FalseKeyword
    {
        get => new("false");
    }

    /// <summary>
    ///     The <c>famandassem</c> keyword.
    /// </summary>
    public static KeywordToken FamAndAssemKeyword
    {
        get => new("famandassem");
    }

    /// <summary>
    ///     The <c>famorassem</c> keyword.
    /// </summary>
    public static KeywordToken FamOrAssemKeyword
    {
        get => new("famorassem");
    }

    /// <summary>
    ///     The <c>family</c> keyword.
    /// </summary>
    public static KeywordToken FamilyKeyword
    {
        get => new("family");
    }

    /// <summary>
    ///     The <c>.field</c> keyword.
    /// </summary>
    public static KeywordToken FieldDeclaration
    {
        get => new(".field");
    }

    /// <summary>
    ///     The <c>hassecurity</c> keyword.
    /// </summary>
    public static KeywordToken HasSecurityKeyword
    {
        get => new("hassecurity");
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
    ///     The <c>implements</c> keyword.
    /// </summary>
    public static KeywordToken ImplementsKeyword
    {
        get => new("implements");
    }

    /// <summary>
    ///     The <c>import</c> keyword.
    /// </summary>
    public static KeywordToken ImportKeyword
    {
        get => new("import");
    }

    /// <summary>
    ///     The <c>literal</c> keyword.
    /// </summary>
    public static KeywordToken LiteralKeyword
    {
        get => new("literal");
    }

    /// <summary>
    ///     The <c>nested</c> keyword.
    /// </summary>
    public static KeywordToken NestedKeyword
    {
        get => new("nested");
    }

    /// <summary>
    ///     The <c>null</c> keyword.
    /// </summary>
    public static KeywordToken NullKeyword
    {
        get => new("null");
    }

    /// <summary>
    ///     The <c>private</c> keyword.
    /// </summary>
    public static KeywordToken PrivateKeyword
    {
        get => new("private");
    }

    /// <summary>
    ///     The <c>public</c> keyword.
    /// </summary>
    public static KeywordToken PublicKeyword
    {
        get => new("public");
    }

    /// <summary>
    ///     The <c>rtspecialname</c> keyword.
    /// </summary>
    public static KeywordToken RTSpecialNameKeyword
    {
        get => new("rtspecialname");
    }

    /// <summary>
    ///     The <c>sealed</c> keyword.
    /// </summary>
    public static KeywordToken SealedKeyword
    {
        get => new("sealed");
    }

    /// <summary>
    ///     The <c>sequential</c> keyword.
    /// </summary>
    public static KeywordToken SequentialKeyword
    {
        get => new("sequential");
    }

    /// <summary>
    ///     The <c>serializable</c> keyword.
    /// </summary>
    public static KeywordToken SerializableKeyword
    {
        get => new("serializable");
    }

    /// <summary>
    ///     The <c>specialname</c> keyword.
    /// </summary>
    public static KeywordToken SpecialNameKeyword
    {
        get => new("specialname");
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
    ///     The <c>unicode</c> keyword.
    /// </summary>
    public static KeywordToken UnicodeKeyword
    {
        get => new("unicode");
    }

    /// <summary>
    ///     The <c>valuetype</c> keyword.
    /// </summary>
    public static KeywordToken ValueTypeKeyword
    {
        get => new("valuetype");
    }

    /// <summary>
    ///     The <c>windowsruntime</c> keyword.
    /// </summary>
    public static KeywordToken WindowsRuntimeKeyword
    {
        get => new("windowsruntime");
    }
}
