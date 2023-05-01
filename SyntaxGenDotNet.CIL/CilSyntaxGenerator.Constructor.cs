using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        return new ConstructorDeclaration();
    }
}
