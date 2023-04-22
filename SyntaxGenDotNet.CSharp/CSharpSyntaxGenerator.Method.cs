using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        var declaration = new MethodDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, methodInfo);
        ModifierUtility.WriteAllModifiers(declaration, methodInfo);
        WriteMethodTypeSignature(declaration, methodInfo, true);
        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }

    private static void WriteMethodTypeSignature(SyntaxNode target, MethodInfo methodInfo, bool writeGenericArguments = false)
    {
        TypeUtility.WriteAlias(target, methodInfo.ReturnType);
        target.Children[^1].TrailingWhitespace = WhitespaceTrivia.Space;
        target.AddChild(new IdentifierToken(methodInfo.Name));

        if (writeGenericArguments)
        {
            Type[] genericArguments = methodInfo.GetGenericArguments();
            TypeUtility.WriteGenericArguments(target, genericArguments);
        }

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

        ModifierUtility.WritePassByModifier(target, parameter);
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
