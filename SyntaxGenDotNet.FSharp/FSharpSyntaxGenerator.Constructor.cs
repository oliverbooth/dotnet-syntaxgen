using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        return new ConstructorDeclaration();
    }
}
