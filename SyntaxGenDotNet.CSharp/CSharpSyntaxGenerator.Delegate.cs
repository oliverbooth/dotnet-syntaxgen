using System.Diagnostics;
using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateDelegateDeclaration(Type delegateType)
    {
        Trace.Assert(delegateType.IsSubclassOf(typeof(MulticastDelegate)) || delegateType.IsSubclassOf(typeof(Delegate)),
            "The specified type is not a delegate.");

        MethodInfo invokeMethod = delegateType.GetMethod("Invoke")!;
        var delegateDeclaration = new TypeDeclaration();
        TypeUtility.WriteVisibilityKeyword(delegateDeclaration, delegateType);
        delegateDeclaration.AddChild(Keywords.DelegateKeyword);
        TypeUtility.WriteTypeName(delegateDeclaration, invokeMethod.ReturnType);

        string name = delegateType.Name;
        if (delegateType.IsGenericType)
        {
            name = name[..name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)];
        }

        delegateDeclaration.AddChild(new TypeIdentifierToken(name));
        TypeUtility.WriteGenericArguments(delegateDeclaration, delegateType);
        delegateDeclaration.AddChild(Operators.OpenParenthesis);

        ParameterInfo[] parameters = invokeMethod.GetParameters();
        for (var index = 0; index < parameters.Length; index++)
        {
            ParameterInfo parameter = parameters[index];
            TypeUtility.WriteTypeName(delegateDeclaration, parameter.ParameterType);

            if (parameter.Name is not null)
            {
                delegateDeclaration.AddChild(new IdentifierToken(parameter.Name));
            }

            if (index < parameters.Length - 1)
            {
                delegateDeclaration.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = " "));
            }
        }

        delegateDeclaration.AddChild(Operators.CloseParenthesis);
        delegateDeclaration.AddChild(Operators.Semicolon);
        return delegateDeclaration;
    }
}
