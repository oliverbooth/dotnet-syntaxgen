using System.Reflection;
using SyntaxGenDotNet.CIL.Utilities;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override EventDeclaration GenerateEventDeclaration(EventInfo eventInfo)
    {
        var declaration = new EventDeclaration();

        declaration.AddChild(Keywords.EventDeclaration);
        declaration.AddChild(eventInfo.IsStatic() ? Keywords.StaticKeyword : Keywords.InstanceKeyword);
        TypeUtility.WriteTypeName(declaration, eventInfo.EventHandlerType!);
        declaration.AddChild(new IdentifierToken(eventInfo.Name));
        declaration.AddChild(Operators.OpenBrace.With(o => o.LeadingWhitespace = WhitespaceTrivia.NewLine));

        if (eventInfo.AddMethod is { } addMethod)
        {
            declaration.AddChild(Keywords.AddOnDeclaration.With(o => o.LeadingWhitespace = WhitespaceTrivia.Indent));
            WriteEventAccessor(declaration, eventInfo.DeclaringType!, addMethod);
        }

        if (eventInfo.RemoveMethod is { } removeMethod)
        {
            declaration.AddChild(Keywords.RemoveOnDeclaration.With(o => o.LeadingWhitespace = WhitespaceTrivia.Indent));
            WriteEventAccessor(declaration, eventInfo.DeclaringType!, removeMethod);
        }

        var closeBrace = new OperatorToken(Operators.CloseBrace.Text, false) {LeadingWhitespace = WhitespaceTrivia.NewLine};
        declaration.AddChild(closeBrace);
        return declaration;
    }

    private static void WriteEventAccessor(SyntaxNode target, Type declaringType, MethodInfo accessor)
    {
        target.AddChild(accessor.IsStatic ? Keywords.StaticKeyword : Keywords.InstanceKeyword);
        TypeUtility.WriteTypeName(target, accessor.ReturnType);
        TypeUtility.WriteTypeName(target, declaringType, new TypeWriteOptions {WriteAlias = false, WriteKindPrefix = false});
        target.AddChild(Operators.ColonColon);
        target.AddChild(new IdentifierToken(accessor.Name));
        target.AddChild(Operators.OpenParenthesis);
        ParameterInfo[] parameters = accessor.GetParameters();

        for (var index = 0; index < parameters.Length; index++)
        {
            ParameterInfo parameter = parameters[index];
            target.AddChild(parameter.ParameterType.IsValueType ? Keywords.ValueTypeKeyword : Keywords.ClassKeyword);
            TypeUtility.WriteTypeName(target, parameter.ParameterType);

            if (index < parameters.Length - 1)
            {
                target.AddChild(Operators.Comma);
            }
        }

        target.AddChild(Operators.CloseParenthesis);
    }
}
