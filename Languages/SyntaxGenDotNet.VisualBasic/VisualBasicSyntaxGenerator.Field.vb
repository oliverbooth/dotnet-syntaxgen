Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens
Imports SyntaxGenDotNet.VisualBasic.Utilities

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateFieldDeclaration(fieldInfo As FieldInfo) As FieldDeclaration
        Dim declaration As New FieldDeclaration()

        WriteCustomAttributes(Me, declaration, fieldInfo)
        WriteAllModifiers(declaration, fieldInfo)

        declaration.AddChild(new IdentifierToken(fieldInfo.Name))
        declaration.AddChild(AsKeyword)
        WriteAlias(declaration, fieldInfo.FieldType)

        If fieldInfo.IsLiteral Then
            declaration.AddChild(Assignment)
            Dim value = fieldInfo.GetRawConstantValue()
            declaration.AddChild(CreateLiteralToken(value))
        End If

        Return declaration
    End Function
End Class
