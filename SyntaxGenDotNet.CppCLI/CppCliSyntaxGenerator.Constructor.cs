using System.Reflection;
using SyntaxGenDotNet.CppCLI.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        var declaration = new ConstructorDeclaration();

        ModifierUtility.WriteVisibilityModifier(declaration, constructorInfo);
        declaration.AddChild(Operators.Colon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Indent));
        if (constructorInfo.IsStatic)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        TypeUtility.WriteName(declaration, constructorInfo.DeclaringType!);
        declaration.AddChild(Operators.OpenParenthesis);
        WriteParameters(declaration, constructorInfo.GetParameters());
        declaration.AddChild(Operators.CloseParenthesis);
        declaration.AddChild(Operators.Semicolon);

        return declaration;
    }
}
