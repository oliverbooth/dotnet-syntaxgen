using System.Reflection;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override PropertyDeclaration GeneratePropertyDeclaration(PropertyInfo propertyInfo)
    {
        var declaration = new PropertyDeclaration();
        MethodInfo mostAccessibleMethod = propertyInfo.GetMostAccessibleMethod();

        AttributeUtility.WriteCustomAttributes(this, declaration, mostAccessibleMethod);
        ModifierUtility.WriteAllModifiers(declaration, mostAccessibleMethod);

        if (!mostAccessibleMethod.IsStatic)
        {
            declaration.AddChild(Keywords.ThisKeyword);
            declaration.AddChild(Operators.Dot);
        }

        declaration.AddChild(new IdentifierToken(propertyInfo.Name));
        declaration.AddChild(Operators.Colon);
        TypeUtility.WriteAlias(declaration, propertyInfo.PropertyType);

        return declaration;
    }
}
