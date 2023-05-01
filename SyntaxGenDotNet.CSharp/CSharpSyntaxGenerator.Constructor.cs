using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        return new ConstructorDeclaration();
    }
}
