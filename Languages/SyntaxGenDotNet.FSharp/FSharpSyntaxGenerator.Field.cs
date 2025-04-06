using System.Reflection;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo)
    {
        var declaration = new FieldDeclaration();

        AttributeUtility.WriteCustomAttributes(this, declaration, fieldInfo);
        ModifierUtility.WriteAllModifiers(declaration, fieldInfo);
        declaration.AddChild(new IdentifierToken(fieldInfo.Name));
        declaration.AddChild(Operators.Colon);
        TypeUtility.WriteTypeName(declaration, fieldInfo.FieldType);

        return declaration;
    }
}
