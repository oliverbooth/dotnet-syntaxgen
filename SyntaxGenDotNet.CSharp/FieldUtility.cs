﻿using System.Reflection;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

internal static class FieldUtility
{
    /// <summary>
    ///     Writes the custom attributes for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose custom attributes to write.</param>
    public static void WriteCustomAttributes(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        IEnumerable<Attribute> customAttributes = fieldInfo.GetCustomAttributes()
            .Where(a => a.GetType().IsPublic && TypeUtility.RecognizedAttributes.Contains(a.GetType()));

        foreach (Attribute attribute in customAttributes)
        {
            declaration.AddChild(Operators.OpenBracket);
            TypeUtility.WriteAlias(declaration, attribute.GetType(), new TypeWriteOptions {TrimAttributeSuffix = true});
            declaration.AddChild(Operators.OpenParenthesis);

            ConstructorInfo constructor = attribute.GetType().GetConstructors()[0];

            object?[] arguments = constructor.GetParameters()
                .Select(parameter => AttributeUtility.GetAttributeParameter(attribute, parameter))
                .ToArray();

            for (var index = 0; index < arguments.Length; index++)
            {
                SyntaxToken token = TokenUtility.CreateLiteralToken(arguments[index]);
                declaration.AddChild(token);

                if (index < arguments.Length - 1)
                {
                    declaration.AddChild(Operators.Comma);
                }
            }

            declaration.AddChild(Operators.CloseParenthesis);
            declaration.AddChild(Operators.CloseBracket.With(o => o.TrailingWhitespace = WhitespaceTrivia.NewLine));
        }
    }

    /// <summary>
    ///     Writes the modifiers for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose modifiers to write.</param>
    public static void WriteModifiers(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        if (fieldInfo.IsLiteral)
        {
            declaration.AddChild(Keywords.ConstKeyword);
            return;
        }

        if (fieldInfo.IsStatic)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        if (fieldInfo.IsInitOnly)
        {
            declaration.AddChild(Keywords.ReadOnlyKeyword);
        }
    }
}
