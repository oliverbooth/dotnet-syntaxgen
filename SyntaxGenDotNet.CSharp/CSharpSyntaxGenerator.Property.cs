using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    private const MethodAttributes MethodAccessMask = MethodAttributes.MemberAccessMask;

    /// <inheritdoc />
    public override PropertyDeclaration GeneratePropertyDeclaration(PropertyInfo propertyInfo)
    {
        var declaration = new PropertyDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, propertyInfo);
        MethodInfo mostAccessibleMethod = propertyInfo.GetMostAccessibleMethod();
        MethodAttributes mostAccessibleAccess = (mostAccessibleMethod.Attributes & MethodAccessMask);

        ModifierUtility.WriteAllModifiers(declaration, mostAccessibleMethod);

        if ((mostAccessibleMethod.Attributes & MethodAttributes.Static) != 0)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        TypeUtility.WriteAlias(declaration, propertyInfo.PropertyType);
        declaration.AddChild(new IdentifierToken(propertyInfo.Name));
        declaration.AddChild(Operators.OpenBrace);

        if (propertyInfo.GetMethod is { } getMethod)
        {
            WritePropertyAccessor(declaration, getMethod, mostAccessibleAccess, Keywords.GetKeyword);
        }

        if (propertyInfo.SetMethod is { } setMethod)
        {
            WritePropertyAccessor(declaration, setMethod, mostAccessibleAccess, Keywords.SetKeyword);
        }

        declaration.AddChild(Operators.CloseBrace);
        return declaration;
    }

    private static void WritePropertyAccessor(SyntaxNode target,
        MethodBase accessorMethod,
        MethodAttributes mostAccessibleAccess,
        SyntaxNode accessorKeyword)
    {
        if ((accessorMethod.Attributes & MethodAccessMask) != mostAccessibleAccess)
        {
            ModifierUtility.WriteVisibilityModifier(target, accessorMethod);
        }

        target.AddChild(accessorKeyword);
        target.AddChild(Operators.Semicolon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space));
    }
}
