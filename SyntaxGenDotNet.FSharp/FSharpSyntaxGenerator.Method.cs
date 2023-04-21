using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        return new MethodDeclaration();
    }
}
