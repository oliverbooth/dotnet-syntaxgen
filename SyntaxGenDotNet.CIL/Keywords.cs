using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

internal static class Keywords
{
    /// <summary>
    ///     The <c>abstract</c> keyword.
    /// </summary>
    public static readonly KeywordToken AbstractKeyword = new("abstract");

    /// <summary>
    ///     The <c>ansi</c> keyword.
    /// </summary>
    public static readonly KeywordToken AnsiKeyword = new("ansi");

    /// <summary>
    ///     The <c>assembly</c> keyword.
    /// </summary>
    public static readonly KeywordToken AssemblyKeyword = new("assembly");

    /// <summary>
    ///     The <c>auto</c> keyword.
    /// </summary>
    public static readonly KeywordToken AutoKeyword = new("auto");

    /// <summary>
    ///     The <c>autochar</c> keyword.
    /// </summary>
    public static readonly KeywordToken AutoCharKeyword = new("autochar");

    /// <summary>
    ///     The <c>beforefieldinit</c> keyword.
    /// </summary>
    public static readonly KeywordToken BeforeFieldInitKeyword = new("beforefieldinit");

    /// <summary>
    ///     The <c>.class</c> keyword.
    /// </summary>
    public static readonly KeywordToken ClassDeclaration = new(".class");

    /// <summary>
    ///     The <c>class</c> keyword.
    /// </summary>
    public static readonly KeywordToken ClassKeyword = new("class");

    /// <summary>
    ///     The <c>explicit</c> keyword.
    /// </summary>
    public static readonly KeywordToken ExplicitKeyword = new("explicit");

    /// <summary>
    ///     The <c>extends</c> keyword.
    /// </summary>
    public static readonly KeywordToken ExtendsKeyword = new("extends");

    /// <summary>
    ///     The <c>false</c> keyword.
    /// </summary>
    public static readonly KeywordToken FalseKeyword = new("false");

    /// <summary>
    ///     The <c>famandassem</c> keyword.
    /// </summary>
    public static readonly KeywordToken FamAndAssemKeyword = new("famandassem");

    /// <summary>
    ///     The <c>famorassem</c> keyword.
    /// </summary>
    public static readonly KeywordToken FamOrAssemKeyword = new("famorassem");

    /// <summary>
    ///     The <c>family</c> keyword.
    /// </summary>
    public static readonly KeywordToken FamilyKeyword = new("family");

    /// <summary>
    ///     The <c>.field</c> keyword.
    /// </summary>
    public static readonly KeywordToken FieldDeclaration = new(".field");

    /// <summary>
    ///     The <c>hassecurity</c> keyword.
    /// </summary>
    public static readonly KeywordToken HasSecurityKeyword = new("hassecurity");

    /// <summary>
    ///     The <c>initonly</c> keyword.
    /// </summary>
    public static readonly KeywordToken InitOnlyKeyword = new("initonly");

    /// <summary>
    ///     The <c>interface</c> keyword.
    /// </summary>
    public static readonly KeywordToken InterfaceKeyword = new("interface");

    /// <summary>
    ///     The <c>implements</c> keyword.
    /// </summary>
    public static readonly KeywordToken ImplementsKeyword = new("implements");

    /// <summary>
    ///     The <c>import</c> keyword.
    /// </summary>
    public static readonly KeywordToken ImportKeyword = new("import");

    /// <summary>
    ///     The <c>literal</c> keyword.
    /// </summary>
    public static readonly KeywordToken LiteralKeyword = new("literal");

    /// <summary>
    ///     The <c>nested</c> keyword.
    /// </summary>
    public static readonly KeywordToken NestedKeyword = new("nested");

    /// <summary>
    ///     The <c>null</c> keyword.
    /// </summary>
    public static readonly KeywordToken NullKeyword = new("null");

    /// <summary>
    ///     The <c>private</c> keyword.
    /// </summary>
    public static readonly KeywordToken PrivateKeyword = new("private");

    /// <summary>
    ///     The <c>public</c> keyword.
    /// </summary>
    public static readonly KeywordToken PublicKeyword = new("public");

    /// <summary>
    ///     The <c>rtspecialname</c> keyword.
    /// </summary>
    public static readonly KeywordToken RTSpecialNameKeyword = new("rtspecialname");

    /// <summary>
    ///     The <c>sealed</c> keyword.
    /// </summary>
    public static readonly KeywordToken SealedKeyword = new("sealed");

    /// <summary>
    ///     The <c>sequential</c> keyword.
    /// </summary>
    public static readonly KeywordToken SequentialKeyword = new("sequential");

    /// <summary>
    ///     The <c>serializable</c> keyword.
    /// </summary>
    public static readonly KeywordToken SerializableKeyword = new("serializable");

    /// <summary>
    ///     The <c>specialname</c> keyword.
    /// </summary>
    public static readonly KeywordToken SpecialNameKeyword = new("specialname");

    /// <summary>
    ///     The <c>static</c> keyword.
    /// </summary>
    public static readonly KeywordToken StaticKeyword = new("static");

    /// <summary>
    ///     The <c>true</c> keyword.
    /// </summary>
    public static readonly KeywordToken TrueKeyword = new("true");

    /// <summary>
    ///     The <c>unicode</c> keyword.
    /// </summary>
    public static readonly KeywordToken UnicodeKeyword = new("unicode");

    /// <summary>
    ///     The <c>valuetype</c> keyword.
    /// </summary>
    public static readonly KeywordToken ValueTypeKeyword = new("valuetype");

    /// <summary>
    ///     The <c>windowsruntime</c> keyword.
    /// </summary>
    public static readonly KeywordToken WindowsRuntimeKeyword = new("windowsruntime");
}
