using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override EventDeclaration GenerateEventDeclaration(EventInfo eventInfo)
    {
        var declaration = new EventDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, eventInfo);
        MethodInfo mostAccessibleMethod = eventInfo.GetMostAccessibleMethod();

        ModifierUtility.WriteVisibilityModifier(declaration, mostAccessibleMethod);
        if (mostAccessibleMethod.IsStatic)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        declaration.AddChild(Keywords.EventKeyword);
        TypeUtility.WriteAlias(declaration, eventInfo.EventHandlerType!);
        declaration.AddChild(new IdentifierToken(eventInfo.Name));
        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }
}
