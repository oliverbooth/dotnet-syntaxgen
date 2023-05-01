Imports System.Reflection
Imports SyntaxGenDotNet.Extensions
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens
Imports SyntaxGenDotNet.VisualBasic.Extensions
Imports SyntaxGenDotNet.VisualBasic.Utilities

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GeneratePropertyDeclaration(propertyInfo As PropertyInfo) As PropertyDeclaration
        Dim declaration As New PropertyDeclaration()
        WriteCustomAttributes(Me, declaration, propertyInfo)

        Dim mostAccessibleMethod As MethodInfo = propertyInfo.GetMostAccessibleMethod()
        WriteAllModifiers(declaration, mostAccessibleMethod)

        If propertyInfo.GetMethod IsNot Nothing AndAlso propertyInfo.SetMethod Is Nothing Then
            declaration.AddChild(ReadOnlyKeyword)
        ElseIf propertyInfo.SetMethod IsNot Nothing AndAlso propertyInfo.GetMethod Is Nothing Then
            declaration.AddChild(WriteOnlyKeyword)
        End If

        declaration.AddChild(PropertyKeyword)
        declaration.AddChild(New IdentifierToken(propertyInfo.Name))
        declaration.AddChild(AsKeyword)
        WriteAlias(declaration, propertyInfo.PropertyType)

        Return declaration
    End Function
End Class
