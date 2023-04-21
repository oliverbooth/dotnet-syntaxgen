using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL.Utilities;

internal static class TypeUtility
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
    ///     Writes the attributes for the specified type to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the attributes.</param>
    /// <param name="type">The type for which to write the attributes.</param>
    public static void WriteTypeAttributes(SyntaxNode node, Type type)
    {
        WriteClassSemanticsAttributes(node, type);
        WriteVisibilityAttribute(node, type);
        WriteSpecialSemanticsAttributes(node, type);
        WriteLayoutAttributes(node, type);
        WriteCharSetAttributes(node, type);
        WriteImplementationAttributes(node, type);
        WriteAdditionalAttributes(node, type);
    }

    /// <summary>
    ///     Writes the type name to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the type name.</param>
    /// <param name="type">The type for which to write the name.</param>
    public static void WriteTypeName(SyntaxNode node, Type type)
    {
        WriteTypeName(node, type, new TypeWriteOptions {WriteAlias = true, WriteNamespace = true, WriteKindPrefix = true});
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
            node.AddChild(new TypeIdentifierToken(type.Name));
            return;
        }

        if (options.WriteAlias && TryGetLanguageAliasToken(type, out KeywordToken? alias))
        {
            node.AddChild(alias);
            return;
        }

        if (!options.WriteNamespace)
        {
            if (options.WriteKindPrefix && type is {IsValueType: false, IsGenericType: true})
            {
                node.AddChild(Keywords.ClassKeyword);
            }

            node.AddChild(new TypeIdentifierToken(type.Name));
            return;
        }

        WriteNamespacedTypeName(node, type, options.WriteKindPrefix);

        SyntaxNode last = node.Children[^1];
        if (last is OperatorToken)
        {
            last.TrailingWhitespace = WhitespaceTrivia.Space;
        }
    }


    private static void WriteAdditionalAttributes(SyntaxNode node, Type type)
    {
        if ((type.Attributes & TypeAttributes.BeforeFieldInit) != 0)
        {
            node.AddChild(Keywords.BeforeFieldInitKeyword);
        }

        if ((type.Attributes & TypeAttributes.RTSpecialName) != 0)
        {
            node.AddChild(Keywords.RTSpecialNameKeyword);
        }

        if ((type.Attributes & TypeAttributes.HasSecurity) != 0)
        {
            node.AddChild(Keywords.HasSecurityKeyword);
        }
    }

    private static void WriteCharSetAttributes(SyntaxNode node, Type type)
    {
        const TypeAttributes mask = TypeAttributes.StringFormatMask;
        TypeAttributes attributes = type.Attributes & mask;

        switch (attributes)
        {
            case TypeAttributes.AutoClass:
                node.AddChild(Keywords.AutoCharKeyword);
                break;

            case TypeAttributes.AnsiClass:
                node.AddChild(Keywords.AnsiKeyword);
                break;

            case TypeAttributes.UnicodeClass:
                node.AddChild(Keywords.UnicodeKeyword);
                break;
        }
    }

    private static void WriteClassSemanticsAttributes(SyntaxNode node, Type type)
    {
        if ((type.Attributes & TypeAttributes.Interface) != 0)
        {
            node.AddChild(Keywords.InterfaceKeyword);
        }
    }

    private static void WriteGenericArguments(SyntaxNode node, Type type)
    {
        node.AddChild(Operators.OpenChevron);
        Type[] genericArguments = type.GetGenericArguments();
        for (var index = 0; index < genericArguments.Length; index++)
        {
            Type genericArgument = genericArguments[index];
            WriteParameterVariance(node, genericArgument);
            WriteTypeName(node, genericArgument);

            if (index < genericArguments.Length - 1)
            {
                node.AddChild(Operators.Comma);
            }
        }

        node.AddChild(Operators.CloseChevron);
    }


    private static void WriteImplementationAttributes(SyntaxNode node, Type type)
    {
        if ((type.Attributes & TypeAttributes.Import) != 0)
        {
            node.AddChild(Keywords.ImportKeyword);
        }

        if ((type.Attributes & TypeAttributes.Serializable) != 0)
        {
            node.AddChild(Keywords.SerializableKeyword);
        }

        if ((type.Attributes & TypeAttributes.WindowsRuntime) != 0)
        {
            node.AddChild(Keywords.WindowsRuntimeKeyword);
        }
    }

    private static void WriteLayoutAttributes(SyntaxNode node, Type type)
    {
        if (type.IsExplicitLayout)
        {
            node.AddChild(Keywords.ExplicitKeyword);
        }
        else if (type.IsAutoLayout)
        {
            node.AddChild(Keywords.AutoKeyword);
        }
        else
        {
            node.AddChild(Keywords.SequentialKeyword);
        }
    }

    private static void WriteNamespacedTypeName(SyntaxNode node, Type type, bool writeKindPrefix)
    {
        if (type.IsArray)
        {
            WriteTypeName(node, type.GetElementType()!);
            node.AddChild(Operators.OpenBracket);
            node.AddChild(Operators.CloseBracket);
            return;
        }

        if (writeKindPrefix)
        {
            if (type.IsValueType)
            {
                node.AddChild(Keywords.ValueTypeKeyword);
            }
            else if (type.IsGenericType)
            {
                node.AddChild(Keywords.ClassKeyword);
            }
        }

        string fullName = type.Name;
        if (type.Namespace is not null)
        {
            fullName = type.Namespace + ILOperators.NamespaceSeparator + fullName;
        }

        string[] namespaces = fullName.Split(ILOperators.NamespaceSeparator.Text);
        for (var index = 0; index < namespaces.Length; index++)
        {
            node.AddChild(new TypeIdentifierToken(namespaces[index]));

            if (index < namespaces.Length - 1)
            {
                node.AddChild(ILOperators.NamespaceSeparator);
            }
        }

        if (type.IsGenericType)
        {
            WriteGenericArguments(node, type);
        }
    }

    private static void WriteParameterVariance(SyntaxNode node, Type genericArgument)
    {
        if (!genericArgument.IsGenericParameter)
        {
            return;
        }

        const GenericParameterAttributes mask = GenericParameterAttributes.VarianceMask;
        var attributes = genericArgument.GenericParameterAttributes;

        switch (attributes & mask)
        {
            case GenericParameterAttributes.Contravariant:
                node.AddChild(Operators.Contravariant);
                break;

            case GenericParameterAttributes.Covariant:
                node.AddChild(Operators.Covariant);
                break;
        }
    }

    private static void WriteSpecialSemanticsAttributes(SyntaxNode node, Type type)
    {
        if (type.IsAbstract)
        {
            node.AddChild(Keywords.AbstractKeyword);
        }

        if (type.IsSealed)
        {
            node.AddChild(Keywords.SealedKeyword);
        }

        if (type.IsSpecialName)
        {
            node.AddChild(Keywords.SpecialNameKeyword);
        }

        if ((type.Attributes & TypeAttributes.RTSpecialName) != 0)
        {
            node.AddChild(Keywords.RTSpecialNameKeyword);
        }
    }

    private static void WriteVisibilityAttribute(SyntaxNode node, Type type)
    {
        const TypeAttributes mask = TypeAttributes.VisibilityMask;
        TypeAttributes attributes = type.Attributes & mask;

        switch (attributes)
        {
            case TypeAttributes.Public:
                node.AddChild(Keywords.PublicKeyword);
                break;

            case TypeAttributes.NestedPublic:
                node.AddChild(Keywords.NestedKeyword);
                node.AddChild(Keywords.PublicKeyword);
                break;

            case TypeAttributes.NestedPrivate:
                node.AddChild(Keywords.NestedKeyword);
                node.AddChild(Keywords.PrivateKeyword);
                break;

            case TypeAttributes.NestedFamily:
                node.AddChild(Keywords.NestedKeyword);
                node.AddChild(Keywords.FamilyKeyword);
                break;

            case TypeAttributes.NestedAssembly:
                node.AddChild(Keywords.NestedKeyword);
                node.AddChild(Keywords.AssemblyKeyword);
                break;

            case TypeAttributes.NestedFamANDAssem:
                node.AddChild(Keywords.NestedKeyword);
                node.AddChild(Keywords.FamAndAssemKeyword);
                break;

            case TypeAttributes.NestedFamORAssem:
                node.AddChild(Keywords.NestedKeyword);
                node.AddChild(Keywords.FamOrAssemKeyword);
                break;

            case TypeAttributes.NotPublic:
                node.AddChild(Keywords.PrivateKeyword);
                break;
        }
    }
}
