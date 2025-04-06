Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.VisualBasic.Utilities

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateConstructorDeclaration(constructorInfo As ConstructorInfo) As ConstructorDeclaration
        Dim declaration As New ConstructorDeclaration()

        WriteCustomAttributes(Me, declaration, constructorInfo)
        WriteVisibilityModifier(declaration, constructorInfo)
        If constructorInfo.IsStatic Then
            declaration.AddChild(SharedKeyword)
        End If

        declaration.AddChild(SubKeyword)
        declaration.AddChild(NewKeyword)
        declaration.AddChild(OpenParenthesis)
        WriteParameters(declaration, constructorInfo.GetParameters())
        declaration.AddChild(CloseParenthesis)

        Return declaration
    End Function
End Class
