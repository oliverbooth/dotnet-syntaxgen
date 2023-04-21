using System.Reflection;
using SyntaxGenDotNet.CppCLI.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo)
    {
        var declaration = new FieldDeclaration();
        ModifierUtility.WriteVisibilityModifier(declaration, fieldInfo);
        declaration.AddChild(Operators.Colon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
        ModifierUtility.WriteInitializationModifiers(declaration, fieldInfo);
        TypeUtility.WriteAlias(declaration, fieldInfo.FieldType);
        declaration.AddChild(new IdentifierToken(fieldInfo.Name));

        if (fieldInfo.IsLiteral)
        {
            declaration.AddChild(Operators.Assignment);
            object? value = fieldInfo.GetRawConstantValue();
            declaration.AddChild(TokenUtility.CreateLiteralToken(value));
        }

        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }
}
