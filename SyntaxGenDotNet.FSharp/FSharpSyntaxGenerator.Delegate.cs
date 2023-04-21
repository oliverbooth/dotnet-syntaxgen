using System.Diagnostics;
using System.Reflection;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateDelegateDeclaration(Type delegateType)
    {
        Trace.Assert(delegateType.IsSubclassOf(typeof(MulticastDelegate)) || delegateType.IsSubclassOf(typeof(Delegate)),
            "The specified type is not a delegate.");

        var declaration = new TypeDeclaration();
        declaration.AddChild(Keywords.TypeKeyword);
        TypeUtility.WriteVisibilityKeyword(declaration, delegateType);
        TypeUtility.WriteTypeName(declaration, delegateType, new TypeWriteOptions {WriteNamespace = false, WriteAlias = false});

        declaration.AddChild(Operators.Assignment.With(o => o.LeadingWhitespace = WhitespaceTrivia.None));
        declaration.AddChild(Keywords.DelegateKeyword);
        declaration.AddChild(Keywords.OfKeyword);

        MethodInfo invokeMethod = delegateType.GetMethod("Invoke")!;
        ParameterInfo[] parameters = invokeMethod.GetParameters();
        for (var index = 0; index < parameters.Length; index++)
        {
            ParameterInfo parameter = parameters[index];
            TypeUtility.WriteTypeName(declaration, parameter.ParameterType);

            if (index < parameters.Length - 1)
            {
                declaration.AddChild(Operators.Asterisk);
            }
        }

        declaration.AddChild(Operators.Arrow);
        TypeUtility.WriteTypeName(declaration, invokeMethod.ReturnType);
        return declaration;
    }
}
