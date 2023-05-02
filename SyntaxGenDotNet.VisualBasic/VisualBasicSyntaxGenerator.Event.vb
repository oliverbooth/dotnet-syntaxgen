Imports System.Reflection
Imports SyntaxGenDotNet.Extensions
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens
Imports SyntaxGenDotNet.VisualBasic.Utilities

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateEventDeclaration(eventInfo As EventInfo) As EventDeclaration
        Dim declaration As New EventDeclaration()
        WriteCustomAttributes(Me, declaration, eventInfo)

        Dim mostAccessibleMethod As MethodInfo = eventInfo.GetMostAccessibleMethod()
        WriteVisibilityModifier(declaration, mostAccessibleMethod)
        declaration.AddChild(CustomKeyword)
        declaration.AddChild(EventKeyword)

        declaration.AddChild(New IdentifierToken(eventInfo.Name))
        declaration.AddChild(AsKeyword)
        WriteTypeName(declaration, eventInfo.EventHandlerType)
        Return declaration
    End Function
End Class
