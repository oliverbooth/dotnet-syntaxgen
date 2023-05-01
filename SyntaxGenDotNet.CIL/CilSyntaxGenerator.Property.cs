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
    public override PropertyDeclaration GeneratePropertyDeclaration(PropertyInfo propertyInfo)
    {
        var declaration = new PropertyDeclaration();

        declaration.AddChild(Keywords.PropertyDeclaration);
        declaration.AddChild(propertyInfo.IsStatic() ? Keywords.StaticKeyword : Keywords.InstanceKeyword);
        TypeUtility.WriteTypeName(declaration, propertyInfo.PropertyType);
        declaration.AddChild(new IdentifierToken(propertyInfo.Name));
        declaration.AddChild(Operators.OpenParenthesis);
        declaration.AddChild(Operators.CloseParenthesis);
        declaration.AddChild(Operators.OpenBrace.With(o => o.LeadingWhitespace = WhitespaceTrivia.NewLine));

        if (propertyInfo.GetMethod is { } getMethod)
        {
            declaration.AddChild(Keywords.GetDeclaration.With(o => o.LeadingWhitespace = WhitespaceTrivia.Indent));
            WritePropertyAccessor(declaration, propertyInfo.DeclaringType!, getMethod);
        }

        if (propertyInfo.SetMethod is { } setMethod)
        {
            declaration.AddChild(Keywords.SetDeclaration.With(o => o.LeadingWhitespace = WhitespaceTrivia.Indent));
            WritePropertyAccessor(declaration, propertyInfo.DeclaringType!, setMethod);
        }

        var closeBrace = new OperatorToken(Operators.CloseBrace.Text, false) {LeadingWhitespace = WhitespaceTrivia.NewLine};
        declaration.AddChild(closeBrace);
        return declaration;
    }

    private static void WritePropertyAccessor(SyntaxNode target, Type declaringType, MethodInfo accessor)
    {
        target.AddChild(accessor.IsStatic ? Keywords.StaticKeyword : Keywords.InstanceKeyword);
        TypeUtility.WriteTypeName(target, accessor.ReturnType);
        TypeUtility.WriteTypeName(target, declaringType, new TypeWriteOptions {WriteAlias = false, WriteKindPrefix = false});
        target.AddChild(Operators.ColonColon);
        target.AddChild(new IdentifierToken(accessor.Name));
        target.AddChild(Operators.OpenParenthesis);
        WriteParameters(target, accessor.GetParameters());
        target.AddChild(Operators.CloseParenthesis);
    }
}
