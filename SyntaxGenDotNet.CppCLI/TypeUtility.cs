using System.Diagnostics.CodeAnalysis;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

internal sealed class TypeUtility
{
    private static readonly Dictionary<Type, KeywordToken> LanguageAliases = new()
    {
        [typeof(byte)] = new KeywordToken("unsigned char"),
        [typeof(sbyte)] = new KeywordToken("signed char"),
        [typeof(short)] = new KeywordToken("short"),
        [typeof(ushort)] = new KeywordToken("unsigned short"),
        [typeof(int)] = new KeywordToken("int"),
        [typeof(uint)] = new KeywordToken("unsigned int"),
        [typeof(long)] = new KeywordToken("long long"),
        [typeof(ulong)] = new KeywordToken("unsigned long long"),
        [typeof(float)] = new KeywordToken("float"),
        [typeof(double)] = new KeywordToken("double"),
        [typeof(bool)] = new KeywordToken("bool"),
        [typeof(char)] = new KeywordToken("wchar_t"),
        [typeof(void)] = new KeywordToken("void")
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
        if (!TryGetLanguageAliasToken(genericType, out alias))
        {
            node.AddChild(new TypeIdentifierToken(type.Name));
            return;
        }

        node.AddChild(alias);
        node.AddChild(Operators.OpenChevron);
        Type[] genericArguments = type.GetGenericArguments();
        for (var index = 0; index < genericArguments.Length; index++)
        {
            if (index > 0)
            {
                node.AddChild(Operators.Comma);
            }

            WriteTypeName(node, genericArguments[index]);
        }

        node.AddChild(Operators.CloseChevron);
    }
}
