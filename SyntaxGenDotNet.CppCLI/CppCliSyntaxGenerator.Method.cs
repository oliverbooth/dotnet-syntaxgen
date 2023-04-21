using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        return new MethodDeclaration();
    }
}
