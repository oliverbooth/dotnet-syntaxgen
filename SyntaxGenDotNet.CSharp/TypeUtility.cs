using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using SyntaxGenDotNet.Attributes;
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
    ///     Writes the fully resolved name of the specified type to the specified node, or the alias if the type has one.
    /// </summary>
    /// <param name="target">The node to which to write the type name.</param>
    /// <param name="type">The type whose name to write.</param>
    /// <param name="options">The options to use when writing the type name.</param>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="target" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="type" /> is <see langword="null" />.</para>
    /// </exception>
    public static void WriteAlias(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (type.IsByRef)
        {
            target.AddChild(Keywords.RefKeyword);
            WriteAlias(target, type.GetElementType()!, options);
            return;
        }

        if (type.IsPointer)
        {
            WriteAlias(target, type.GetElementType()!, options);
            target.AddChild(Operators.Asterisk);
            return;
        }

        if (type.IsArray)
        {
            WriteAlias(target, type.GetElementType()!, options);
            target.AddChild(Operators.OpenBracket);
            target.AddChild(Operators.CloseBracket);
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

        if (options.Value.WriteGenericArguments)
        {
            WriteGenericArguments(target, type);
        }
    }

    /// <summary>
    ///     Writes a custom attribute to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the attribute.</param>
    /// <param name="attributeExpression">The attribute expression to write.</param>
    public static void WriteCustomAttribute(SyntaxNode target, MemberInitExpression attributeExpression)
    {
        var options = new TypeWriteOptions {TrimAttributeSuffix = true, WriteNamespace = true, WriteAlias = false};

        // explicit creation of the open bracket token to avoid the trailing whitespace
        // of the previous token being trimmed, as Operators.OpenBracket defaults to trimming.
        var openBracket = new OperatorToken(Operators.OpenBracket.Text, false);

        target.AddChild(openBracket);
        WriteAlias(target, attributeExpression.Type, options);

        ReadOnlyCollection<Expression> arguments = attributeExpression.NewExpression.Arguments;
        if (arguments.Count > 0)
        {
            target.AddChild(Operators.OpenParenthesis);

            for (var index = 0; index < arguments.Count; index++)
            {
                Expression argument = arguments[index];

                if (argument.Type.IsEnum)
                {
                    WriteName(target, argument.Type);
                    target.AddChild(Operators.Dot);
                }

                target.AddChild(TokenUtility.CreateLiteralToken(argument));
            }

            if (attributeExpression.Bindings.Count > 0)
            {
                WriteBindings(target, attributeExpression, options);
            }

            target.AddChild(Operators.CloseParenthesis);
        }

        target.AddChild(Operators.CloseBracket.With(o => o.TrailingWhitespace = WhitespaceTrivia.NewLine));
    }

    /// <summary>
    ///     Writes all known supported custom attribute to the specified node.
    /// </summary>
    /// <param name="generator">The syntax generator.</param>
    /// <param name="target">The node to which to write the attributes.</param>
    /// <param name="type">The type for which to write the attributes.</param>
    public static void WriteCustomAttributes(SyntaxGenerator generator, TypeDeclaration target, Type type)
    {
        foreach (AttributeExpressionWriter writer in generator.AttributeExpressionWriters)
        {
            Type attributeType = writer.AttributeType;
            Attribute? attribute = type.GetCustomAttribute(attributeType, false);
            Expression attributeExpression = writer.CreateAttributeExpression(type, attribute);

            if (attributeExpression is MemberInitExpression memberInitExpression)
            {
                WriteCustomAttribute(target, memberInitExpression);
            }
        }
    }

    /// <summary>
    ///     Writes the generic arguments for the specified type to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the generic arguments.</param>
    /// <param name="type">The type for which to write the generic arguments.</param>
    public static void WriteGenericArguments(SyntaxNode target, Type type)
    {
        if (!type.IsGenericType)
        {
            return;
        }

        target.AddChild(Operators.OpenChevron);
        Type[] genericArguments = type.GetGenericArguments();
        for (var index = 0; index < genericArguments.Length; index++)
        {
            Type genericArgument = genericArguments[index];
            WriteParameterVariance(target, genericArgument);
            WriteAlias(target, genericArgument);

            if (index < genericArguments.Length - 1)
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
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="target" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="type" /> is <see langword="null" />.</para>
    /// </exception>
    public static void WriteName(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        options ??= new TypeWriteOptions();

        string name = type.Name;
        if (type.IsGenericType)
        {
            name = name.AsSpan()[..name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)].ToString();
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
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="target" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="type" /> is <see langword="null" />.</para>
    /// </exception>
    public static void WriteNamespace(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        options ??= new TypeWriteOptions();

        if (!options.Value.WriteNamespace)
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
    /// <exception cref="ArgumentNullException"><paramref name="target" /> is <see langword="null" />.</exception>
    public static void WriteNamespace(SyntaxNode target, string? namespaceName)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

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

    private static void WriteBindings(SyntaxNode target, MemberInitExpression memberInitExpression, TypeWriteOptions options)
    {
        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = " ");
        ReadOnlyCollection<MemberBinding> bindings = memberInitExpression.Bindings;
        for (var index = 0; index < bindings.Count; index++)
        {
            target.AddChild(comma);

            MemberBinding binding = bindings[index];
            if (binding is not MemberAssignment assignment)
            {
                continue;
            }

            target.AddChild(new IdentifierToken(binding.Member.Name));
            target.AddChild(Operators.Assignment);
            if (assignment.Expression.Type.IsEnum)
            {
                WriteAlias(target, assignment.Expression.Type, options with {WriteNamespace = false});
                target.AddChild(Operators.Dot);
            }

            target.AddChild(TokenUtility.CreateLiteralToken((ConstantExpression)assignment.Expression));
        }
    }

    private static void WriteParameterVariance(SyntaxNode target, Type genericArgument)
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
                target.AddChild(Keywords.InKeyword);
                break;

            case GenericParameterAttributes.Covariant:
                target.AddChild(Keywords.OutKeyword);
                break;
        }
    }
}
