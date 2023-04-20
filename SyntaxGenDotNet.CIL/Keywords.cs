using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

internal static class Keywords
{
    /// <summary>
    ///     The <c>assembly</c> keyword.
    /// </summary>
    public static readonly KeywordToken AssemblyKeyword = new("assembly");

    /// <summary>
    ///     The <c>class</c> keyword.
    /// </summary>
    public static readonly KeywordToken ClassKeyword = new("class");

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
    ///     The <c>literal</c> keyword.
    /// </summary>
    public static readonly KeywordToken LiteralKeyword = new("literal");

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
    ///     The <c>initonly</c> keyword.
    /// </summary>
    public static readonly KeywordToken InitOnlyKeyword = new("initonly");

    /// <summary>
    ///     The <c>static</c> keyword.
    /// </summary>
    public static readonly KeywordToken StaticKeyword = new("static");

    /// <summary>
    ///     The <c>true</c> keyword.
    /// </summary>
    public static readonly KeywordToken TrueKeyword = new("true");

    /// <summary>
    ///     The <c>valuetype</c> keyword.
    /// </summary>
    public static readonly KeywordToken ValueTypeKeyword = new("valuetype");
}
