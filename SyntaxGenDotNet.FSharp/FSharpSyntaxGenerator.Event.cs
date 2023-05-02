using System.Reflection;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override EventDeclaration GenerateEventDeclaration(EventInfo eventInfo)
    {
        var declaration = new EventDeclaration();
        MethodInfo mostAccessibleMethod = eventInfo.GetMostAccessibleMethod();

        AttributeUtility.WriteCustomAttributes(this, declaration, eventInfo);
        ModifierUtility.WriteAllModifiers(declaration, mostAccessibleMethod);

        if (!mostAccessibleMethod.IsStatic)
        {
            declaration.AddChild(Keywords.ThisKeyword);
            declaration.AddChild(Operators.Dot);
        }

        declaration.AddChild(new IdentifierToken(eventInfo.Name));
        declaration.AddChild(Operators.Colon);
        TypeUtility.WriteAlias(declaration, eventInfo.EventHandlerType!);

        return declaration;
    }
}
