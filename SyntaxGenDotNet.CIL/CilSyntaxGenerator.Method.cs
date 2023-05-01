using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using SyntaxGenDotNet.CIL.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo)
    {
        var declaration = new MethodDeclaration();
        declaration.AddChild(Keywords.MethodDeclaration);
        MethodUtility.WriteAllAttributes(declaration, methodInfo);
        WriteTypeName(declaration, methodInfo, methodInfo.ReturnType);
        declaration.AddChild(new IdentifierToken(methodInfo.Name));
        TypeUtility.WriteGenericArguments(declaration, methodInfo.GetGenericArguments());
        declaration.AddChild(Operators.OpenParenthesis);
        WriteParameters(declaration, methodInfo.GetParameters());
        declaration.AddChild(Operators.CloseParenthesis.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
        MethodUtility.WriteImplementationFlags(declaration, methodInfo);

        declaration.Children[^1].TrailingWhitespace = WhitespaceTrivia.None;
        return declaration;
    }

    private static bool TryGetGenericIdentifier(MethodBase method, Type type, [NotNullWhen(true)] out TypeIdentifierToken? token)
    {
        if (method is ConstructorInfo)
        {
            token = null;
            return false;
        }
        
        Type[] arguments = method.GetGenericArguments();
        int index = Array.IndexOf(arguments, type);
        if (index >= 0)
        {
            token = new TypeIdentifierToken($"!!{index}");
            return true;
        }

        Type? declaringType = method.DeclaringType;
        while (declaringType is not null)
        {
            arguments = declaringType.GetGenericArguments();
            index = Array.IndexOf(arguments, type);
            if (index >= 0)
            {
                token = new TypeIdentifierToken($"!{index}");
                return true;
            }

            declaringType = declaringType.DeclaringType;
        }

        token = null;
        return false;
    }

    private static void WriteParameter(SyntaxNode target, ParameterInfo parameter)
    {
        if (parameter.Name is null)
        {
            return;
        }

        WriteTypeName(target, (MethodBase)parameter.Member, parameter.ParameterType);
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

    private static void WriteTypeName(SyntaxNode target, MethodBase method, Type type)
    {
        if (TryGetGenericIdentifier(method, type, out TypeIdentifierToken? token))
        {
            target.AddChild(token);
        }
        else
        {
            TypeUtility.WriteTypeName(target, type);
        }
    }
}
