using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        return new MethodDeclaration();
    }
}
