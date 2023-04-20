using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo)
    {
        var fieldDeclaration = new FieldDeclaration();
        fieldDeclaration.AddChild(Keywords.FieldDeclaration);

        FieldUtility.WriteVisibilityKeyword(fieldDeclaration, fieldInfo);
        FieldUtility.WriteModifiers(fieldDeclaration, fieldInfo);
        TypeUtility.WriteTypeName(fieldDeclaration, fieldInfo.FieldType);
        fieldDeclaration.AddChild(new IdentifierToken(fieldInfo.Name));

        if (fieldInfo.IsLiteral)
        {
            fieldDeclaration.AddChild(Operators.Assignment);
            object? value = fieldInfo.GetRawConstantValue();

            TypeUtility.WriteTypeName(fieldDeclaration, fieldInfo.FieldType);
            fieldDeclaration.AddChild(Operators.OpenParenthesis);
            fieldDeclaration.AddChild(TokenUtility.CreateLiteralToken(value));
            fieldDeclaration.AddChild(Operators.CloseParenthesis);

            if (fieldInfo.FieldType == typeof(char))
            {
                // chars are represented as integers in CIL,
                // so a comment is added to show the actual character value.
                // most decompilers do this, so for consistency, it's done here too.
                fieldDeclaration.AddChild(new CommentToken($"'{value}'"));
            }
        }

        return fieldDeclaration;
    }
}
