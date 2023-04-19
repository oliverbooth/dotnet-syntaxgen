using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo)
    {
        var fieldDeclaration = new FieldDeclaration();
        FieldUtility.WriteVisibilityKeyword(fieldDeclaration, fieldInfo);
        FieldUtility.WriteModifiers(fieldDeclaration, fieldInfo);
        TypeUtility.WriteTypeName(fieldDeclaration, fieldInfo.FieldType);

        fieldDeclaration.AddChild(new IdentifierToken(fieldInfo.Name));

        if (fieldInfo.IsLiteral)
        {
            fieldDeclaration.AddChild(Operators.Assignment);
            object? value = fieldInfo.GetRawConstantValue();
            fieldDeclaration.AddChild(TokenUtility.CreateLiteralToken(value));
        }

        fieldDeclaration.AddChild(Operators.Semicolon);
        return fieldDeclaration;
    }
}
