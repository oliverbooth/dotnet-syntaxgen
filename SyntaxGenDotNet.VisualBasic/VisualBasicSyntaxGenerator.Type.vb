Imports SyntaxGenDotNet.Extensions
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc />
    Public Overrides Function GenerateTypeDeclaration(type As Type) As TypeDeclaration
        Dim declaration As New TypeDeclaration()

        WriteCustomTypeAttributes(Me, declaration, type)
        WriteTypeVisibilityKeyword(declaration, type)
        WriteTypeModifiers(declaration, type)
        WriteTypeKind(declaration, type)
        declaration.AddChild(new TypeIdentifierToken(type.Name))
        WriteBaseType(declaration, type)
        WriteInterfaces(declaration, type)

        Return declaration
    End Function

    Private Shared Sub WriteBaseType(declaration As SyntaxNode, type As Type)
        If Not type.HasBaseType() Then
            Return
        End If

        declaration.AddChild(InheritsKeyword.With(Sub(k) k.LeadingWhitespace = WhitespaceTrivia.Indent))
        WriteTypeName(declaration, type.BaseType, New TypeWriteOptions() With { .WriteAlias = False, .WriteNamespace = True })
    End Sub

    Private Shared Sub WriteInterfaces(declaration As SyntaxNode, type As Type)
        Dim interfaces As Type() = type.GetDirectInterfaces()
        If interfaces.Length = 0 Then
            Return
        End If

        Dim comma As SyntaxToken = Operators.Comma.With(Sub(c) c.TrailingWhitespace = " ")
        declaration.AddChild(ImplementsKeyword.With(Sub(k) k.LeadingWhitespace = WhitespaceTrivia.Indent))

        Dim options As New TypeWriteOptions() With { .WriteAlias = False, .WriteNamespace = True }

        For index = 0 To interfaces.Length - 1
            Dim interfaceType As Type = interfaces(index)
            WriteTypeName(declaration, interfaceType, options)

            If index < interfaces.Length - 1 Then
                declaration.AddChild(comma)
            End If
        Next
    End Sub

    Private Shared Sub WriteTypeKind(declaration As SyntaxNode, type As Type)
        If type.IsInterface Then
            declaration.AddChild(InterfaceKeyword)
        ElseIf type.IsEnum Then
            declaration.AddChild(EnumKeyword)
        ElseIf type.IsValueType Then
            declaration.AddChild(StructureKeyword)
        Else
            declaration.AddChild(ClassKeyword)
        End If
    End Sub
End Class
