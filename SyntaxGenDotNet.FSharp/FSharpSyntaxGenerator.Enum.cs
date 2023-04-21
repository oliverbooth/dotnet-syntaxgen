using System.Diagnostics;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateEnumDeclaration(Type enumType)
    {
        Trace.Assert(enumType.IsEnum, "The specified type is not an enum.");

        var enumDeclaration = new TypeDeclaration();
        enumDeclaration.AddChild(Keywords.TypeKeyword);
        TypeUtility.WriteVisibilityKeyword(enumDeclaration, enumType);
        enumDeclaration.AddChild(new TypeIdentifierToken(enumType.Name));
        enumDeclaration.AddChild(Operators.Assignment);

        return enumDeclaration;
    }
}
