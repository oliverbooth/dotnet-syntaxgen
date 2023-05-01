Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateConstructorDeclaration(constructorInfo As ConstructorInfo) As ConstructorDeclaration
        Return New ConstructorDeclaration()
    End Function
End Class
