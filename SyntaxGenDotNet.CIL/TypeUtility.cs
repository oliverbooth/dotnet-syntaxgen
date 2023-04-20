using System.Diagnostics.CodeAnalysis;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

internal sealed class TypeUtility
{
    private static readonly Dictionary<Type, KeywordToken> LanguageAliases = new()
    {
        [typeof(bool)] = new KeywordToken("bool"),
        [typeof(char)] = new KeywordToken("char"),
        [typeof(int)] = new KeywordToken("int32"),
        [typeof(long)] = new KeywordToken("int64"),
        [typeof(float)] = new KeywordToken("float32"),
        [typeof(double)] = new KeywordToken("float64"),
        [typeof(sbyte)] = new KeywordToken("int8"),
        [typeof(byte)] = new KeywordToken("uint8"),
        [typeof(short)] = new KeywordToken("int16"),
        [typeof(ushort)] = new KeywordToken("uint16"),
        [typeof(uint)] = new KeywordToken("uint32"),
        [typeof(ulong)] = new KeywordToken("uint64"),
        [typeof(void)] = new KeywordToken("void"),
        [typeof(object)] = new KeywordToken("object"),
        [typeof(string)] = new KeywordToken("string")
    };

    /// <summary>
    ///     Attempts to get the language alias for the specified type.
    /// </summary>
    /// <param name="type">The type for which to get the alias.</param>
    /// <param name="alias">
    ///     When this method returns, contains the alias for the specified type if the type has an alias; otherwise,
    ///     <see langword="null" />.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if the specified type has an alias; otherwise, <see langword="false" />.
    /// </returns>
    public static bool TryGetLanguageAliasToken(Type type, [NotNullWhen(true)] out KeywordToken? alias)
    {
        return LanguageAliases.TryGetValue(type, out alias);
    }

    /// <summary>
    ///     Writes the type name to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the type name.</param>
    /// <param name="type">The type for which to write the name.</param>
    public static void WriteTypeName(SyntaxNode node, Type type)
    {
        if (type.IsGenericParameter)
        {
            node.AddChild(new IdentifierToken(type.Name));
            return;
        }

        if (TryGetLanguageAliasToken(type, out KeywordToken? alias))
        {
            node.AddChild(alias);
            return;
        }

        if (!type.IsGenericType)
        {
            node.AddChild(new TypeIdentifierToken(type.Name));
            return;
        }

        var genericType = type.GetGenericTypeDefinition();
        if (TryGetLanguageAliasToken(genericType, out alias))
        {
            node.AddChild(alias);
            return;
        }

        node.AddChild(new TypeIdentifierToken(type.Name));
    }
}
