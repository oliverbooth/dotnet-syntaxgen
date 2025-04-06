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
    public override EventDeclaration GenerateEventDeclaration(EventInfo eventInfo)
    {
        var declaration = new EventDeclaration();
        MethodInfo mostAccessibleMethod = eventInfo.GetMostAccessibleMethod();

        ModifierUtility.WriteVisibilityModifier(declaration, mostAccessibleMethod);
        declaration.AddChild(Operators.Colon.With(o => o.TrailingWhitespace = WhitespaceTrivia.Indent));
        ModifierUtility.WriteAllModifiers(declaration, mostAccessibleMethod);
        declaration.AddChild(Keywords.EventKeyword);
        TypeUtility.WriteAlias(declaration, eventInfo.EventHandlerType!);
        if (!eventInfo.EventHandlerType!.IsValueType)
        {
            declaration.AddChild(Operators.GcTrackedPointer);
        }

        declaration.AddChild(new IdentifierToken(eventInfo.Name));
        declaration.AddChild(Operators.Semicolon);
        return declaration;
    }
}
