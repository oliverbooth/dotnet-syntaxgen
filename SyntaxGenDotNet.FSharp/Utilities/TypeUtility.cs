using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp.Utilities;

internal sealed class TypeUtility
{
    internal static readonly List<Type> RecognizedAttributes = new() {typeof(CLSCompliantAttribute)};
    private static readonly Dictionary<Type, KeywordToken> LanguageAliases = new()
    {
        [typeof(bool)] = new KeywordToken("bool"),
        [typeof(char)] = new KeywordToken("char"),
        [typeof(int)] = new KeywordToken("int"),
        [typeof(long)] = new KeywordToken("int64"),
        [typeof(float)] = new KeywordToken("single"),
        [typeof(double)] = new KeywordToken("float64"),
        [typeof(sbyte)] = new KeywordToken("sbyte"),
        [typeof(byte)] = new KeywordToken("byte"),
        [typeof(short)] = new KeywordToken("int16"),
        [typeof(ushort)] = new KeywordToken("uint16"),
        [typeof(uint)] = new KeywordToken("uint"),
        [typeof(ulong)] = new KeywordToken("uint64"),
        [typeof(void)] = new KeywordToken("unit"),
        [typeof(object)] = new KeywordToken("obj"),
        [typeof(string)] = new KeywordToken("string"),
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
    ///     Writes the generic arguments for the specified type to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the generic arguments.</param>
    /// <param name="type">The type for which to write the generic arguments.</param>
    public static void WriteGenericArguments(SyntaxNode node, Type type)
    {
        Type[] genericArguments = type.GetGenericArguments();
        if (genericArguments.Length == 0)
        {
            return;
        }

        node.AddChild(Operators.OpenChevron);
        for (var index = 0; index < genericArguments.Length; index++)
        {
            if (index > 0)
            {
                node.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
            }

            WriteTypeName(node, genericArguments[index]);
        }

        node.AddChild(Operators.CloseChevron);
    }

    /// <summary>
    ///     Writes the type name to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the type name.</param>
    /// <param name="type">The type for which to write the name.</param>
    public static void WriteTypeName(SyntaxNode node, Type type)
    {
        WriteTypeName(node, type, new TypeWriteOptions());
    }

    /// <summary>
    ///     Writes the type name to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the type name.</param>
    /// <param name="type">The type for which to write the name.</param>
    /// <param name="options">The options for writing the type name.</param>
    public static void WriteTypeName(SyntaxNode node, Type type, TypeWriteOptions options)
    {
        if (type.IsGenericParameter)
        {
            node.AddChild(new TypeIdentifierToken($"'{type.Name}"));
            return;
        }

        if (TryGetLanguageAliasToken(type, out KeywordToken? alias))
        {
            node.AddChild(alias);
            return;
        }

        WriteNamespacedTypeName(node, type, options);
        SyntaxNode last = node.Children[^1];
        if (last is OperatorToken)
        {
            last.TrailingWhitespace = WhitespaceTrivia.Space;
        }
    }

    /// <summary>
    ///     Writes the visibility keyword for a type declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="type">The type whose visibility to write.</param>
    public static void WriteVisibilityKeyword(SyntaxNode declaration, Type type)
    {
        const TypeAttributes mask = TypeAttributes.VisibilityMask;

        switch (type.Attributes & mask)
        {
            case TypeAttributes.Public:
            case TypeAttributes.NestedPublic:
                // do nothing. public is the default.
                break;

            case TypeAttributes.NestedPrivate:
                declaration.AddChild(Keywords.PrivateKeyword);
                break;

            case TypeAttributes.NestedFamANDAssem:
                declaration.AddChild(Keywords.PrivateKeyword);
                declaration.AddChild(Keywords.ProtectedKeyword);
                break;

            case TypeAttributes.NestedFamORAssem:
                declaration.AddChild(Keywords.ProtectedKeyword);
                declaration.AddChild(Keywords.InternalKeyword);
                break;

            case TypeAttributes.NestedFamily:
                declaration.AddChild(Keywords.ProtectedKeyword);
                break;

            default:
                declaration.AddChild(Keywords.InternalKeyword);
                break;
        }
    }

    private static void WriteNamespacedTypeName(SyntaxNode node, Type type, TypeWriteOptions options)
    {
        if (type.IsArray)
        {
            WriteTypeName(node, type.GetElementType()!);
            node.AddChild(Operators.OpenBracket);
            node.AddChild(Operators.CloseBracket);
            return;
        }

        string fullName = options.WriteNamespace ? type.FullName ?? type.Name : type.Name;
        if (type.IsGenericType)
        {
            fullName = fullName[..fullName.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)];
        }

        WriteFullName(node, fullName, options.TrimAttributeSuffix);

        if (type.IsGenericType)
        {
            WriteGenericArguments(node, type);
        }
    }

    private static void WriteFullName(SyntaxNode node, string fullName, bool trimAttributeSuffix)
    {
        string[] namespaces = fullName.Split(ILOperators.NamespaceSeparator.Text);
        for (var index = 0; index < namespaces.Length; index++)
        {
            string name = namespaces[index];
            if (trimAttributeSuffix && name.EndsWith("Attribute", StringComparison.Ordinal))
            {
                name = name[..^9];
            }

            node.AddChild(new TypeIdentifierToken(name));

            if (index < namespaces.Length - 1)
            {
                node.AddChild(Operators.Dot);
            }
        }
    }
}
