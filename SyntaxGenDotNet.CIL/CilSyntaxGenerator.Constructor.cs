using System.Reflection;
using SyntaxGenDotNet.CIL.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        var declaration = new ConstructorDeclaration();
        declaration.AddChild(Keywords.MethodDeclaration);
        MethodUtility.WriteAllAttributes(declaration, constructorInfo);
        declaration.AddChild(Keywords.ConstructorDeclaration);
        declaration.AddChild(Operators.OpenParenthesis);
        WriteParameters(declaration, constructorInfo.GetParameters());
        declaration.AddChild(Operators.CloseParenthesis.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
        MethodUtility.WriteImplementationFlags(declaration, constructorInfo);

        declaration.Children[^1].TrailingWhitespace = WhitespaceTrivia.None;
        return declaration;
    }
}
