using System.Reflection;
using SyntaxGenDotNet.CppCLI.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        var declaration = new MethodDeclaration();
        TypeUtility.WriteGenericArguments(declaration, methodInfo.GetGenericArguments());
        ModifierUtility.WriteVisibilityModifier(declaration, methodInfo);
        declaration.AddChild(Operators.Colon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
        WriteMethodTypeSignature(declaration, methodInfo);
        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }

    private static void WriteMethodTypeSignature(SyntaxNode target, MethodInfo methodInfo)
    {
        TypeUtility.WriteAlias(target, methodInfo.ReturnType);
        target.AddChild(new IdentifierToken(methodInfo.Name));
        target.AddChild(Operators.OpenParenthesis);
        WriteParameters(target, methodInfo.GetParameters());
        target.AddChild(Operators.CloseParenthesis);
    }

    private static void WriteParameter(SyntaxNode target, ParameterInfo parameter)
    {
        if (parameter.Name is null)
        {
            return;
        }

        TypeUtility.WriteAlias(target, parameter.ParameterType);
        target.Children[^1].TrailingWhitespace = WhitespaceTrivia.Space;
        target.AddChild(new IdentifierToken(parameter.Name));

        if (!parameter.IsOptional)
        {
            return;
        }

        target.AddChild(Operators.Assignment);
        target.AddChild(TokenUtility.CreateLiteralToken(parameter.DefaultValue));
    }

    private static void WriteParameters(SyntaxNode target, IReadOnlyList<ParameterInfo> parameters)
    {
        SyntaxNode comma = parameters.Count switch
        {
            // With method returns a newly-allocated clone. but there's no use allocating
            // if only one parameter is present, as no comma will be necessary.
            > 1 => Operators.Comma.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space),
            _ => null!
        };

        for (var index = 0; index < parameters.Count; index++)
        {
            WriteParameter(target, parameters[index]);

            if (index < parameters.Count - 1)
            {
                target.AddChild(comma);
            }
        }
    }
}
