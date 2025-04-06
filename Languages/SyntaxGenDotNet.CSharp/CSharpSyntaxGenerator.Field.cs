using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo)
    {
        var declaration = new FieldDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, fieldInfo);
        ModifierUtility.WriteAllModifiers(declaration, fieldInfo);
        TypeUtility.WriteAlias(declaration, fieldInfo.FieldType);

        declaration.AddChild(new IdentifierToken(fieldInfo.Name));

        if (fieldInfo.IsLiteral)
        {
            declaration.AddChild(Operators.Assignment);
            object? value = fieldInfo.GetRawConstantValue();
            declaration.AddChild(TokenUtility.CreateLiteralToken(value));
        }

        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }
}
