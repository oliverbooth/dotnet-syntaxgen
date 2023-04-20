using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

internal sealed class TypeUtility
{
    internal static readonly List<Type> RecognizedAttributes = new() {typeof(CLSCompliantAttribute)};
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
    ///     Writes all known supported custom attribute to the specified node.
    /// </summary>
    /// <param name="generator">The syntax generator.</param>
    /// <param name="node">The node to which to write the attributes.</param>
    /// <param name="type">The type for which to write the attributes.</param>
    public static void WriteCustomAttributes(SyntaxGenerator generator, TypeDeclaration node, Type type)
    {
        foreach (AttributeExpressionWriter writer in generator.AttributeExpressionWriters)
        {
            var writerType = writer.GetType();
            if (!writerType.IsSubclassOf(typeof(AttributeExpressionWriter)))
            {
                continue;
            }

            Type attributeType = writer.AttributeType;
            Attribute? attribute = type.GetCustomAttribute(attributeType, false);
            Expression attributeExpression = writer.CreateAttributeExpression(type, attribute);

            if (attributeExpression is MemberInitExpression memberInitExpression)
            {
                WriteCustomAttribute(node, memberInitExpression);
            }
        }
    }

    /// <summary>
    ///     Writes a custom attribute to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the attribute.</param>
    /// <param name="attributeExpression">The attribute expression to write.</param>
    public static void WriteCustomAttribute(SyntaxNode node, MemberInitExpression attributeExpression)
    {
        var options = new TypeWriteOptions {TrimAttributeSuffix = true, WriteNamespace = true, WriteAlias = false};

        // explicit creation of the open bracket token to avoid the trailing whitespace
        // of the previous token being trimmed, as Operators.OpenBracket defaults to trimming.
        var openBracket = new OperatorToken("[", false);

        node.AddChild(openBracket);
        WriteTypeName(node, attributeExpression.Type, options);

        ReadOnlyCollection<Expression> arguments = attributeExpression.NewExpression.Arguments;
        if (arguments.Count > 0)
        {
            node.AddChild(Operators.OpenParenthesis);

            for (var index = 0; index < arguments.Count; index++)
            {
                if (arguments[index].Type.IsEnum)
                {
                    WriteTypeName(node, arguments[index].Type, options with {WriteNamespace = false});
                    node.AddChild(Operators.Dot);
                }

                node.AddChild(TokenUtility.CreateLiteralToken(arguments[index]));
            }

            if (attributeExpression.Bindings.Count > 0)
            {
                WriteBindings(node, attributeExpression, options);
            }

            node.AddChild(Operators.CloseParenthesis);
        }

        node.AddChild(Operators.CloseBracket.With(o => o.TrailingWhitespace = WhitespaceTrivia.NewLine));
    }

    /// <summary>
    ///     Writes the generic arguments for the specified type to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the generic arguments.</param>
    /// <param name="type">The type for which to write the generic arguments.</param>
    public static void WriteGenericArguments(SyntaxNode node, Type type)
    {
        if (!type.IsGenericType)
        {
            return;
        }

        node.AddChild(Operators.OpenChevron);
        Type[] genericArguments = type.GetGenericArguments();
        for (var index = 0; index < genericArguments.Length; index++)
        {
            Type genericArgument = genericArguments[index];
            WriteParameterVariance(node, genericArgument);
            WriteTypeName(node, genericArgument);

            if (index < genericArguments.Length - 1)
            {
                node.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = " "));
            }
        }

        node.AddChild(Operators.CloseChevron);
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

        if (type is {IsAbstract: true, IsSealed: true})
        {
            node.AddChild(Keywords.StaticKeyword);
        }
        else if (type.IsAbstract)
        {
            node.AddChild(Keywords.AbstractKeyword);
        }
        else if (type.IsSealed)
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
        WriteTypeName(node, type, new TypeWriteOptions {WriteAlias = true, WriteNamespace = true});
    }

    /// <summary>
    ///     Writes the type name to the specified node.
    /// </summary>
    /// <param name="node">The node to which to write the type name.</param>
    /// <param name="type">The type for which to write the name.</param>
    /// <param name="options">The options for writing the type name.</param>
    public static void WriteTypeName(SyntaxNode node, Type type, TypeWriteOptions options)
    {
        if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            WriteTypeName(node, type.GenericTypeArguments[0], options);
            node.AddChild(Operators.QuestionMark);
            return;
        }

        string typeName = type.Name;
        if (type.IsGenericParameter)
        {
            node.AddChild(new TypeIdentifierToken(typeName));
            return;
        }

        if (options.WriteAlias && TryGetLanguageAliasToken(type, out KeywordToken? alias))
        {
            node.AddChild(alias);
            return;
        }

        if (!options.WriteNamespace)
        {
            if (type.IsGenericType)
            {
                typeName = typeName[..typeName.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)];
            }

            node.AddChild(new TypeIdentifierToken(typeName));
            return;
        }

        WriteNamespacedTypeName(node, type, options.TrimAttributeSuffix);
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

    private static void WriteBindings(SyntaxNode node, MemberInitExpression memberInitExpression, TypeWriteOptions options)
    {
        node.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));

        ReadOnlyCollection<MemberBinding> bindings = memberInitExpression.Bindings;
        for (var index = 0; index < bindings.Count; index++)
        {
            var binding = bindings[index];
            if (binding is not MemberAssignment memberAssignment)
            {
                continue;
            }

            node.AddChild(new IdentifierToken(binding.Member.Name));
            node.AddChild(Operators.Assignment);
            if (memberAssignment.Expression.Type.IsEnum)
            {
                WriteTypeName(node, memberAssignment.Expression.Type, options with {WriteNamespace = false});
                node.AddChild(Operators.Dot);
            }

            node.AddChild(TokenUtility.CreateLiteralToken((ConstantExpression)memberAssignment.Expression));

            if (index < bindings.Count - 1)
            {
                node.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = " "));
            }
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
                node.AddChild(Keywords.InKeyword);
                break;

            case GenericParameterAttributes.Covariant:
                node.AddChild(Keywords.OutKeyword);
                break;
        }
    }
}
