using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        return new ConstructorDeclaration();
    }
}
