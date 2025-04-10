﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;
using X10D.Linq;

namespace SyntaxGenDotNet.CSharp.Utilities;

/// <summary>
///     Provides utility methods for working with types in the C# language.
/// </summary>
public static class TypeUtility
{
    private static readonly List<Type> TupleTypes = new()
    {
        typeof(ValueTuple<,>),
        typeof(ValueTuple<,,>),
        typeof(ValueTuple<,,,>),
        typeof(ValueTuple<,,,,>),
        typeof(ValueTuple<,,,,,>),
        typeof(ValueTuple<,,,,,,>),
        typeof(ValueTuple<,,,,,,,>),
    };

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
    public static void WriteAlias(SyntaxNode target, Type type, TypeWriteOptions? options = null)
    {
        if (TryResolveElementType(target, type, options))
        {
            return;
        }

        if (type.IsGenericType && TupleTypes.Contains(type.GetGenericTypeDefinition()))
        {
            WriteTuple(target, type, options);
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

        Type[] genericArguments = type.GetGenericArguments();
        if (type.DeclaringType?.GetGenericArguments().Select(t => t.FullName)
                .SequenceEqual(genericArguments.Select(t => t.FullName)) == true)
        {
            return;
        }

        WriteGenericArguments(target, genericArguments);
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
            WriteParameterVariance(target, genericArgument);
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
            int index = name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal);
            if (index >= 0)
            {
                name = name[..index];
            }
        }

        if (options.Value.TrimAttributeSuffix && name != "Attribute" && name.EndsWith("Attribute", StringComparison.Ordinal))
        {
            name = name[..^9];
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
    ///     Writes the generic parameter constraints for the specified generic arguments to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the constraints.</param>
    /// <param name="genericArguments">The generic arguments whose constraints to write.</param>
    public static void WriteParameterConstraints(SyntaxNode target, Type[] genericArguments)
    {
        genericArguments = genericArguments.Where(a => a.IsGenericConstrained()).ToArray();
        if (genericArguments.Length == 0)
        {
            return;
        }

        SyntaxNode colon = Operators.Colon.With(o => o.Whitespace = WhitespaceTrivia.Space);

        for (var index = 0; index < genericArguments.Length; index++)
        {
            Type genericArgument = genericArguments[index];
            target.AddChild(Keywords.WhereKeyword.With(o =>
                o.LeadingWhitespace = genericArguments.Length == 1 ? WhitespaceTrivia.Space : WhitespaceTrivia.Indent));
            WriteAlias(target, genericArgument);
            target.AddChild(colon);
            WriteConstraintTokens(target, genericArgument);
        }
    }

    private static void WriteConstraintTokens(SyntaxNode target, Type genericArgument)
    {
        if (!genericArgument.IsGenericConstrained())
        {
            return;
        }

        var wroteConstraint = false;
        GenericParameterAttributes attributes = genericArgument.GenericParameterAttributes;
        if ((attributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
        {
            target.AddChild(Keywords.ClassKeyword);
            wroteConstraint = true;
        }
        else if ((attributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
        {
            target.AddChild(Keywords.StructKeyword);
            wroteConstraint = true;
        }

        Type[] constraints = genericArgument.GetGenericParameterConstraints().Except(typeof(ValueType)).ToArray();
        for (var index = 0; index < constraints.Length; index++)
        {
            if (wroteConstraint || index > 0)
            {
                target.AddChild(Operators.Comma);
            }

            Type constraint = constraints[index];
            WriteAlias(target, constraint);
            wroteConstraint = true;
        }

        if ((attributes & GenericParameterAttributes.DefaultConstructorConstraint) == 0)
        {
            return;
        }

        if ((attributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
        {
            // struct constraint implies default constructor constraint
            return;
        }

        if (wroteConstraint)
        {
            target.AddChild(Operators.Comma);
        }

        target.AddChild(Keywords.NewKeyword);
        target.AddChild(Operators.OpenParenthesis);
        target.AddChild(Operators.CloseParenthesis);
    }

    private static bool TryResolveElementType(SyntaxNode target, Type type, TypeWriteOptions? options)
    {
        if (type.IsByRef)
        {
            if (options is {WriteRef: true})
            {
                target.AddChild(Keywords.RefKeyword);
            }

            WriteAlias(target, type.GetElementType()!, options);
            return true;
        }

        if (type.IsPointer)
        {
            WriteAlias(target, type.GetElementType()!, options);
            target.AddChild(Operators.Asterisk);
            return true;
        }

        if (type.IsArray)
        {
            WriteAlias(target, type.GetElementType()!, options);
            target.AddChild(Operators.OpenBracket);
            target.AddChild(Operators.CloseBracket);
            return true;
        }

        return false;
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

    private static void WriteTuple(SyntaxNode target, Type type, TypeWriteOptions? options)
    {
        var openParenthesis = new OperatorToken(Operators.OpenParenthesis.Text, false);
        target.AddChild(openParenthesis);

        Type[] genericArguments = type.GetGenericArguments();
        for (var index = 0; index < genericArguments.Length; index++)
        {
            Type genericArgument = genericArguments[index];
            WriteAlias(target, genericArgument, options);

            if (index < genericArguments.Length - 1)
            {
                target.AddChild(Operators.Comma);
            }
        }

        target.AddChild(Operators.CloseParenthesis);
    }
}
