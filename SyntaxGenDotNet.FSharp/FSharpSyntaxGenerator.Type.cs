using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        return new TypeDeclaration();
    }
}
