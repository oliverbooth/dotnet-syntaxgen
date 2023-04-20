using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        return new TypeDeclaration();
    }
}
