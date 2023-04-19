Imports System.Reflection
Imports SyntaxGenDotNet.Syntax

Friend Module FieldUtility
    Public Sub WriteModifiers(declaration As SyntaxNode, fieldInfo As FieldInfo)
        If fieldInfo.IsLiteral Then
            declaration.AddChild(ConstKeyword)
        End If
    End Sub

    Public Sub WriteVisibilityKeyword(declaration As SyntaxNode, fieldInfo As FieldInfo)
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
