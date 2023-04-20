using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        if (type.IsEnum)
        {
            return GenerateEnumDeclaration(type);
        }

        var declaration = new TypeDeclaration();
        TypeUtility.WriteVisibilityKeyword(declaration, type);

        declaration.AddChild(type.IsValueType ? Keywords.ValueKeyword : Keywords.RefKeyword);
        declaration.AddChild(Keywords.ClassKeyword);
        declaration.AddChild(new TypeIdentifierToken(type.Name));

        TypeUtility.WriteModifiers(declaration, type);
        Type[] baseTypes = type.HasBaseType() ? new[] {type.BaseType!} : Array.Empty<Type>();
        baseTypes = baseTypes.Concat(type.GetDirectInterfaces()).ToArray();

        if (baseTypes.Length > 0)
        {
            declaration.AddChild(Operators.Colon.With(o => o.LeadingWhitespace = o.TrailingWhitespace = " "));
        }

        for (var index = 0; index < baseTypes.Length; index++)
        {
            Type baseType = baseTypes[index];
            TypeUtility.WriteTypeName(declaration, baseType, new TypeWriteOptions {WriteGcTrackedPointer = false});

            if (index < baseTypes.Length - 1)
            {
                declaration.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = " "));
            }
        }

        return declaration;
    }
}
