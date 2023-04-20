Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Function GenerateEnumDeclaration(enumType As Type) As TypeDeclaration _
        Implements ISyntaxGenerator.GenerateEnumDeclaration
        Trace.Assert(enumType.IsEnum, "The specified type is not an enum.")

        Dim enumDeclaration As New TypeDeclaration()

        WriteTypeVisibilityKeyword(enumDeclaration, enumType)
        enumDeclaration.AddChild(EnumKeyword)
        enumDeclaration.AddChild(new TypeIdentifierToken(enumType.Name))

        Dim enumUnderlyingType As Type = enumType.GetEnumUnderlyingType()
        If enumUnderlyingType <> GetType(Integer) Then
            enumDeclaration.AddChild(AsKeyword.With(Sub(k) k.LeadingWhitespace = WhitespaceTrivia.None))
            WriteTypeName(enumDeclaration, enumUnderlyingType)
        End If

        Return enumDeclaration
    End Function
End Class
