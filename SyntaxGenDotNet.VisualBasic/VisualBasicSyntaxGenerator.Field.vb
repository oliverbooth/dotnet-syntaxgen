Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Function GenerateFieldDeclaration(fieldInfo As FieldInfo) As FieldDeclaration _
        Implements ISyntaxGenerator.GenerateFieldDeclaration
        Dim fieldDeclaration As New FieldDeclaration()

        WriteCustomAttributes(fieldDeclaration, fieldInfo)
        FieldUtility.WriteVisibilityKeyword(fieldDeclaration, fieldInfo)
        WriteModifiers(fieldDeclaration, fieldInfo)

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
