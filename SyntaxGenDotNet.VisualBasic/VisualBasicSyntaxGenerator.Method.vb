Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateMethodDeclaration(methodInfo As MethodInfo) As MethodDeclaration
        Return New MethodDeclaration()
    End Function
End Class
