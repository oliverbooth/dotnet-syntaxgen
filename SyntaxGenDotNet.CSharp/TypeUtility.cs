﻿using System.Diagnostics.CodeAnalysis;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

internal sealed class TypeUtility
{
    private static readonly Dictionary<Type, KeywordToken> LanguageAliases = new()
    {
        [typeof(byte)] = new KeywordToken("byte"),
        [typeof(sbyte)] = new KeywordToken("sbyte"),
        [typeof(short)] = new KeywordToken("short"),
        [typeof(ushort)] = new KeywordToken("ushort"),
        [typeof(int)] = new KeywordToken("int"),
        [typeof(uint)] = new KeywordToken("uint"),
        [typeof(long)] = new KeywordToken("long"),
        [typeof(ulong)] = new KeywordToken("ulong"),
        [typeof(float)] = new KeywordToken("float"),
        [typeof(double)] = new KeywordToken("double"),
        [typeof(decimal)] = new KeywordToken("decimal"),
        [typeof(string)] = new KeywordToken("string"),
        [typeof(bool)] = new KeywordToken("bool"),
        [typeof(char)] = new KeywordToken("char"),
        [typeof(void)] = new KeywordToken("void"),
        [typeof(nint)] = new KeywordToken("nint"),
        [typeof(nuint)] = new KeywordToken("nuint"),
        [typeof(object)] = new KeywordToken("object")
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
    /// <param name="trimAttributeSuffix">
    ///     <see langword="true" /> to trim the "Attribute" suffix from the type name; otherwise, <see langword="false" />. The
    ///     default is <see langword="false" />.
    /// </param>
    public static void WriteTypeName(SyntaxNode node, Type type, bool trimAttributeSuffix = false)
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

        WriteNamespacedTypeName(node, type, trimAttributeSuffix);
        SyntaxNode last = node.Children[^1];
        if (last is OperatorToken)
        {
            last.TrailingWhitespace = WhitespaceTrivia.Space;
        }
    }

    private static void WriteNamespacedTypeName(SyntaxNode node, Type type, bool trimAttributeSuffix)
    {
        if (type.IsArray)
        {
            WriteTypeName(node, type.GetElementType()!);
            node.AddChild(Operators.OpenBracket);
            node.AddChild(Operators.CloseBracket);
            return;
        }

        string fullName = type.FullName ?? type.Name;
        if (type.IsGenericType)
        {
            fullName = fullName[..fullName.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)];
        }

        WriteFullName(node, fullName, trimAttributeSuffix);

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

    private static void WriteGenericArguments(SyntaxNode node, Type type)
    {
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
