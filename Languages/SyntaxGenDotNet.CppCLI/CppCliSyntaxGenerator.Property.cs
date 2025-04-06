using System.Reflection;
using SyntaxGenDotNet.CppCLI.Utilities;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override PropertyDeclaration GeneratePropertyDeclaration(PropertyInfo propertyInfo)
    {
        var declaration = new PropertyDeclaration();
        MethodInfo mostAccessibleMethod = propertyInfo.GetMostAccessibleMethod();

        AttributeUtility.WriteCustomAttributes(this, declaration, propertyInfo);
        ModifierUtility.WriteVisibilityModifier(declaration, mostAccessibleMethod);
        declaration.AddChild(Operators.Colon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Indent));
        ModifierUtility.WriteAllModifiers(declaration, mostAccessibleMethod);
        declaration.AddChild(Keywords.PropertyKeyword);
        TypeUtility.WriteAlias(declaration, propertyInfo.PropertyType);
        declaration.AddChild(new IdentifierToken(propertyInfo.Name));
        declaration.AddChild(Operators.OpenBrace);

        if (propertyInfo.GetMethod is { } getMethod)
        {
            WritePropertyAccessor(declaration, getMethod);
        }

        if (propertyInfo.SetMethod is { } setMethod)
        {
            WritePropertyAccessor(declaration, setMethod);
        }

        declaration.AddChild(Operators.CloseBrace);
        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }

    private void WritePropertyAccessor(SyntaxNode target, MethodInfo accessor)
    {
        TypeUtility.WriteAlias(target, accessor.ReturnType);
        target.AddChild(new IdentifierToken(accessor.Name[..accessor.Name.IndexOf('_')]));
        target.AddChild(Operators.OpenParenthesis);
        WriteParameters(target, accessor.GetParameters());
        target.AddChild(Operators.CloseParenthesis);
        target.AddChild(Operators.Semicolon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
    }
}
