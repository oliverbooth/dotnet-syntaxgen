using System.Reflection;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

internal static class FieldUtility
{
    /// <summary>
    ///     Writes the custom attributes for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose custom attributes to write.</param>
    public static void WriteCustomAttributes(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        IEnumerable<Attribute> customAttributes = fieldInfo.GetCustomAttributes().Where(a => a.GetType().IsPublic);
        foreach (Attribute attribute in customAttributes)
        {
            declaration.AddChild(Operators.OpenBracket);
            declaration.AddChild(Operators.OpenChevron);
            TypeUtility.WriteTypeName(declaration, attribute.GetType());
            declaration.AddChild(Operators.OpenParenthesis);

            Type attributeType = attribute.GetType();
            ConstructorInfo constructor = attributeType.GetConstructors()[0];

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
            declaration.AddChild(Operators.CloseChevron);
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
        declaration.AddChild(fieldInfo.IsStatic ? Keywords.StaticValKeyword : Keywords.ValKeyword);

        if (fieldInfo is {IsInitOnly: false, IsLiteral: false})
        {
            declaration.AddChild(Keywords.MutableKeyword);
        }
    }

    /// <summary>
    ///     Writes the visibility keyword for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose visibility to write.</param>
    public static void WriteVisibilityKeyword(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        const FieldAttributes mask = FieldAttributes.FieldAccessMask;

        switch (fieldInfo.Attributes & mask)
        {
            // public is default inferred, so we don't need to write it

            case FieldAttributes.Private:
                declaration.AddChild(Keywords.PrivateKeyword);
                break;

            case FieldAttributes.FamANDAssem:
                declaration.AddChild(Keywords.PrivateKeyword);
                declaration.AddChild(Keywords.ProtectedKeyword);
                break;

            case FieldAttributes.FamORAssem:
                declaration.AddChild(Keywords.ProtectedKeyword);
                declaration.AddChild(Keywords.InternalKeyword);
                break;

            case FieldAttributes.Assembly:
                declaration.AddChild(Keywords.InternalKeyword);
                break;

            case FieldAttributes.Family:
                declaration.AddChild(Keywords.ProtectedKeyword);
                break;
        }
    }
}
