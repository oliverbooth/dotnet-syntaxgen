using System.Reflection;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo)
    {
        var declaration = new ConstructorDeclaration();

        AttributeUtility.WriteCustomAttributes(this, declaration, constructorInfo);
        declaration.AddChild(Keywords.NewKeyword);
        TypeUtility.WriteName(declaration, constructorInfo.DeclaringType!, new TypeWriteOptions {WriteAlias = false});
        declaration.AddChild(constructorInfo.DeclaringType!.IsGenericType
            ? Operators.Colon
            : Operators.Colon.With(o => o.LeadingWhitespace = WhitespaceTrivia.None));

        ParameterInfo[] parameters = constructorInfo.GetParameters();
        if (parameters.Length == 0)
        {
            TypeUtility.WriteAlias(declaration, typeof(void));
            declaration.AddChild(Operators.Arrow);
        }
        else
        {
            WriteParameters(declaration, parameters);
        }

        TypeUtility.WriteAlias(declaration, constructorInfo.DeclaringType!);
        return declaration;
    }
}
