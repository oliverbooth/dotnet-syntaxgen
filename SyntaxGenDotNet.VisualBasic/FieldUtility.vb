Imports System.Reflection
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Friend Module FieldUtility
    ''' <summary>
    '''     Writes the custom attributes for a field declaration.
    ''' </summary>
    ''' <param name="declaration">The declaration to write to.</param>
    ''' <param name="fieldInfo">The field whose custom attributes to write.</param>
    Public Sub WriteCustomFieldAttributes(declaration as SyntaxNode, fieldInfo As FieldInfo)
    End Sub

    ''' <summary>
    '''     Writes the modifiers for a field declaration.
    ''' </summary>
    ''' <param name="declaration">The declaration to write to.</param>
    ''' <param name="fieldInfo">The field whose modifiers to write.</param>
    Public Sub WriteFieldModifiers(declaration As SyntaxNode, fieldInfo As FieldInfo)
        If fieldInfo.IsLiteral Then
            declaration.AddChild(ConstKeyword)
            Return
        End If

        If fieldInfo.IsStatic Then
            declaration.AddChild(SharedKeyword)
        End If

        If fieldInfo.IsInitOnly Then
            declaration.AddChild(ReadOnlyKeyword)
        End If
    End Sub

    ''' <summary>
    '''     Writes the visibility keyword for a field declaration.
    ''' </summary>
    ''' <param name="declaration">The declaration to write to.</param>
    ''' <param name="fieldInfo">The field whose visibility to write.</param>
    Public Sub WriteFieldVisibilityKeyword(declaration As SyntaxNode, fieldInfo As FieldInfo)
        Const mask = FieldAttributes.FieldAccessMask

        Select Case fieldInfo.Attributes And mask
            Case FieldAttributes.Public
                declaration.AddChild(PublicKeyword)

            Case FieldAttributes.Private
                declaration.AddChild(PrivateKeyword)

            Case FieldAttributes.FamANDAssem
                declaration.AddChild(PrivateKeyword)
                declaration.AddChild(ProtectedKeyword)

            Case FieldAttributes.FamORAssem
                declaration.AddChild(ProtectedKeyword)
                declaration.AddChild(FriendKeyword)

            Case FieldAttributes.Assembly
                declaration.AddChild(FriendKeyword)

            Case FieldAttributes.Family
                declaration.AddChild(ProtectedKeyword)
        End Select
    End Sub
End Module
