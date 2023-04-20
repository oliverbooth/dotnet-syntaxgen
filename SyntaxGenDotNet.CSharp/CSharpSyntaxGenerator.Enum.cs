using System.Diagnostics;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateEnumDeclaration(Type enumType)
    {
        Trace.Assert(enumType.IsEnum, "The specified type is not an enum.");

        var enumDeclaration = new TypeDeclaration();
        TypeUtility.WriteVisibilityKeyword(enumDeclaration, enumType);
        enumDeclaration.AddChild(Keywords.EnumKeyword);
        enumDeclaration.AddChild(new TypeIdentifierToken(enumType.Name));

        Type enumUnderlyingType = enumType.GetEnumUnderlyingType();
        if (enumUnderlyingType != typeof(int))
        {
            enumDeclaration.AddChild(Operators.Colon.With(o => o.TrailingWhitespace = o.LeadingWhitespace = " "));
            TypeUtility.WriteTypeName(enumDeclaration, enumUnderlyingType);
        }

        return enumDeclaration;
    }
}
