using System.Diagnostics.CodeAnalysis;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp.Utilities;

internal static class TypeUtility
{
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
    ///     Writes the fully resolved name of the specified type to the specified node, or the alias if the type has one.
    /// </summary>
    /// <param name="target">The node to which to write the type name.</param>
    /// <param name="type">The type whose name to write.</param>
    /// <param name="options">The options to use when writing the type name.</param>
    public static void WriteAlias(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        while (type.IsByRef)
        {
            type = type.GetElementType()!;
        }

        if (type.IsArray)
        {
            WriteAlias(target, type.GetElementType()!, options);
            target.AddChild(Operators.OpenBracket);
            target.AddChild(Operators.CloseBracket);
            return;
        }

        if (type.IsGenericParameter)
        {
            target.AddChild(new TypeIdentifierToken($"'{type.Name}"));
            return;
        }

        if (TryGetLanguageAliasToken(type, out KeywordToken? alias))
        {
            target.AddChild(alias);
            return;
        }

        options ??= new TypeWriteOptions();
        WriteNamespace(target, type, options);
        WriteName(target, type, options);
    }

    /// <summary>
    ///     Writes the generic arguments for the specified type to the specified node.
    /// </summary>
    /// <param name="target">The target node to which to write the generic arguments.</param>
    /// <param name="type">The type whose generic arguments to write.</param>
    public static void WriteGenericArguments(SyntaxNode target, Type type)
    {
        if (!type.IsGenericType)
        {
            return;
        }

        WriteGenericArguments(target, type.GetGenericArguments());
    }

    /// <summary>
    ///     Writes the generic arguments to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the generic arguments.</param>
    /// <param name="genericArguments">The generic arguments to write.</param>
    public static void WriteGenericArguments(SyntaxNode target, IReadOnlyList<Type> genericArguments)
    {
        if (genericArguments.Count == 0)
        {
            return;
        }

        target.AddChild(Operators.OpenChevron);

        for (var index = 0; index < genericArguments.Count; index++)
        {
            Type genericArgument = genericArguments[index];
            WriteAlias(target, genericArgument);

            if (index < genericArguments.Count - 1)
            {
                target.AddChild(Operators.Comma);
            }
        }

        target.AddChild(Operators.CloseChevron);
    }

    /// <summary>
    ///     Writes the name of the specified type to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the name.</param>
    /// <param name="type">The type whose name to write.</param>
    /// <param name="options">The options for writing the name.</param>
    public static void WriteName(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        options ??= new TypeWriteOptions();

        string name = type.Name;
        if (type.IsGenericType)
        {
            name = name[..name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)];
        }

        if (options.Value.TrimAttributeSuffix && name.EndsWith(nameof(Attribute), StringComparison.Ordinal))
        {
            name = name[..^nameof(Attribute).Length];
        }

        target.AddChild(new TypeIdentifierToken(name));
    }

    /// <summary>
    ///     Writes the namespace for the specified type to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the namespace.</param>
    /// <param name="type">The type whose namespace to write.</param>
    /// <param name="options">The options for writing the namespace.</param>
    public static void WriteNamespace(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        options ??= new TypeWriteOptions();

        if (!options.Value.WriteNamespace || type.IsGenericParameter)
        {
            return;
        }

        WriteNamespace(target, type.Namespace);

        if (type.Namespace is not null)
        {
            target.AddChild(Operators.Dot);
        }
    }

    /// <summary>
    ///     Writes the specified namespace name the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the namespace.</param>
    /// <param name="namespaceName">The name of the namespace to write.</param>
    public static void WriteNamespace(SyntaxNode target, string? namespaceName)
    {
        if (string.IsNullOrWhiteSpace(namespaceName))
        {
            return;
        }

        string[] parts = namespaceName.Split('.');
        for (var index = 0; index < parts.Length; index++)
        {
            target.AddChild(new TypeIdentifierToken(parts[index]));

            if (index < parts.Length - 1)
            {
                target.AddChild(Operators.Dot);
            }
        }
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
