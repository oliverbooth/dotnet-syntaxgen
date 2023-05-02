using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        var declaration = new ConstructorDeclaration();

        AttributeUtility.WriteCustomAttributes(this, declaration, constructorInfo);
        ModifierUtility.WriteVisibilityModifier(declaration, constructorInfo);
        if (constructorInfo.IsStatic)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        TypeUtility.WriteName(declaration, constructorInfo.DeclaringType!);
        declaration.AddChild(Operators.OpenParenthesis);
        WriteParameters(declaration, constructorInfo);
        declaration.AddChild(Operators.CloseParenthesis);
        declaration.AddChild(Operators.Semicolon);

        return declaration;
    }
}
