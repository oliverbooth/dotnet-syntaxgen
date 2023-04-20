Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateFieldDeclaration(fieldInfo As FieldInfo) As FieldDeclaration
        Dim fieldDeclaration As New FieldDeclaration()

        WriteCustomAttributes(fieldDeclaration, fieldInfo)
        WriteFieldVisibilityKeyword(fieldDeclaration, fieldInfo)
        WriteFieldModifiers(fieldDeclaration, fieldInfo)

        fieldDeclaration.AddChild(new IdentifierToken(fieldInfo.Name))
        fieldDeclaration.AddChild(AsKeyword)
        WriteTypeName(fieldDeclaration, fieldInfo.FieldType)

        If fieldInfo.IsLiteral Then
            fieldDeclaration.AddChild(Assignment)
            Dim value = fieldInfo.GetRawConstantValue()
            fieldDeclaration.AddChild(CreateLiteralToken(value))
        End If

        Return fieldDeclaration
    End Function
End Class
