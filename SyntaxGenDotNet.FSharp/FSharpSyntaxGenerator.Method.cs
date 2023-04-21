using System.Reflection;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        var declaration = new MethodDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, methodInfo);
        ModifierUtility.WriteAllModifiers(declaration, methodInfo);

        declaration.AddChild(new IdentifierToken(methodInfo.Name));
        declaration.AddChild(Operators.Colon);
        WriteParameters(declaration, methodInfo.GetParameters());
        TypeUtility.WriteAlias(declaration, methodInfo.ReturnType);

        return declaration;
    }

    private static void WriteParameters(SyntaxNode target, IReadOnlyList<ParameterInfo> parameters)
    {
        for (var index = 0; index < parameters.Count; index++)
        {
            TypeUtility.WriteAlias(target, parameters[index].ParameterType);

            if (index < parameters.Count - 1)
            {
                target.AddChild(Operators.Asterisk);
            }
        }

        if (parameters.Count > 0)
        {
            target.AddChild(Operators.Arrow.With(o => o.LeadingWhitespace = WhitespaceTrivia.None));
        }
    }
}
