using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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
    ///     Writes the modifiers for the specified type to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the modifiers.</param>
    /// <param name="type">The type for which to write the modifiers.</param>
    public static void WriteModifiers(SyntaxNode node, Type type)
    {
        if (type.IsInterface || type.IsValueType)
        {
            return;
        }

        if (type.IsAbstract)
        {
            node.AddChild(Keywords.AbstractKeyword);
        }

        if (type.IsSealed)
        {
            node.AddChild(Keywords.SealedKeyword);
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
    /// <param name="options">The options to use when writing the type name.</param>
    public static void WriteTypeName(SyntaxNode node, Type type, TypeWriteOptions options)
    {
        if (type.IsGenericParameter)
        {
            node.AddChild(new TypeIdentifierToken(type.Name));
            return;
        }

        if (options.WriteAlias && TryGetLanguageAliasToken(type, out KeywordToken? alias))
        {
            node.AddChild(alias);
            return;
        }

        WriteNamespacedTypeName(node, type, options);
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
                declaration.AddChild(Keywords.PublicKeyword);
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
            WriteArrayType(node, type);
            return;
        }

        string typeName = type.Name;
        if (type.IsGenericType)
        {
            typeName = typeName[..typeName.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)];
        }

        string fullName = type.Namespace is null ? typeName : $"{type.Namespace}.{typeName}";

        if (options.WriteNamespace)
        {
            WriteFullName(node, fullName);
        }
        else
        {
            node.AddChild(new TypeIdentifierToken(typeName));
        }

        if (options.WriteGenericArguments && type.IsGenericType)
        {
            WriteGenericArguments(node, type);
        }

        if (!type.IsValueType && options.WriteGcTrackedPointer)
        {
            node.AddChild(Operators.GcTrackedPointer);
        }
    }

    private static void WriteArrayType(SyntaxNode node, Type type)
    {
        node.AddChild(new TypeIdentifierToken("cli"));
        node.AddChild(Operators.ColonColon);
        node.AddChild(new TypeIdentifierToken("array"));

        node.AddChild(Operators.OpenChevron);
        WriteTypeName(node, type.GetElementType()!);
        node.AddChild(Operators.CloseChevron);

        node.AddChild(Operators.GcTrackedPointer);
    }

    private static void WriteFullName(SyntaxNode node, string fullName)
    {
        string[] namespaces = fullName.Split(ILOperators.NamespaceSeparator.Text);
        for (var index = 0; index < namespaces.Length; index++)
        {
            node.AddChild(new TypeIdentifierToken(namespaces[index]));

            if (index < namespaces.Length - 1)
            {
                node.AddChild(Operators.ColonColon);
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

        node.AddChild(Operators.CloseChevron.With(o => o.TrailingWhitespace = " "));
    }
}
