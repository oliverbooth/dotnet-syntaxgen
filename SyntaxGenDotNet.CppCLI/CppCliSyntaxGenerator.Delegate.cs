using System.Diagnostics;
using System.Reflection;
using SyntaxGenDotNet.CppCLI.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateDelegateDeclaration(Type delegateType)
    {
        Trace.Assert(delegateType.IsSubclassOf(typeof(MulticastDelegate)) || delegateType.IsSubclassOf(typeof(Delegate)),
            "The specified type is not a delegate.");

        var declaration = new TypeDeclaration();

        WriteGenericArguments(declaration, delegateType);

        var options = new TypeWriteOptions
        {
            WriteAlias = false, WriteGenericArguments = false, WriteGcTrackedPointer = false, WriteNamespace = false,
        };

        MethodInfo invokeMethod = delegateType.GetMethod("Invoke")!;

        TypeUtility.WriteVisibilityKeyword(declaration, delegateType);
        declaration.AddChild(Keywords.DelegateKeyword);

        TypeUtility.WriteTypeName(declaration, invokeMethod.ReturnType);
        TypeUtility.WriteTypeName(declaration, delegateType, options);

        ParameterInfo[] parameters = invokeMethod.GetParameters();
        declaration.AddChild(Operators.OpenParenthesis);
        for (var index = 0; index < parameters.Length; index++)
        {
            ParameterInfo parameter = parameters[index];
            TypeUtility.WriteTypeName(declaration, parameter.ParameterType);
            if (parameter.Name != null)
            {
                declaration.AddChild(new IdentifierToken(parameter.Name));
            }

            if (index < parameters.Length - 1)
            {
                declaration.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
            }
        }

        declaration.AddChild(Operators.CloseParenthesis);
        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }

    private static void WriteGenericArguments(SyntaxNode declaration, Type delegateType)
    {
        Type[] genericTypeArguments = delegateType.GetGenericArguments();
        if (genericTypeArguments.Length <= 0)
        {
            return;
        }

        declaration.AddChild(Keywords.GenericKeyword);
        declaration.AddChild(Operators.OpenChevron);

        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space);
        for (var index = 0; index < genericTypeArguments.Length; index++)
        {
            declaration.AddChild(Keywords.TypeNameKeyword);
            declaration.AddChild(new TypeIdentifierToken(genericTypeArguments[index].Name));

            if (index < genericTypeArguments.Length - 1)
            {
                declaration.AddChild(comma);
            }
        }

        declaration.AddChild(Operators.CloseChevron.With(o => o.TrailingWhitespace = WhitespaceTrivia.NewLine));
    }
}
