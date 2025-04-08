Imports System.Reflection
Imports SyntaxGenDotNet.Syntax

Namespace Utilities
    Public Partial Module ModifierUtility
        ''' <summary>
        '''     Writes all modifiers for the specified <see cref="FieldInfo" /> to the specified <see cref="SyntaxNode" />. 
        ''' </summary>
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
        ''' <param name="field">The <see cref="FieldInfo" /> whose modifiers to write.</param>
        Public Sub WriteAllModifiers(target As SyntaxNode, field As FieldInfo)
            WriteVisibilityModifier(target, field)

            If field.IsLiteral Then
                target.AddChild(ConstKeyword)
                Return
            End If

            If field.IsStatic Then
                target.AddChild(SharedKeyword)
            End If

            If field.IsInitOnly Then
                target.AddChild(ReadOnlyKeyword)
            End If
        End Sub

        ''' <summary>
        '''     Writes the visibility modifier for the specified <see cref="FieldInfo" /> to the specified <see cref="SyntaxNode" />.
        ''' </summary> 
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
        ''' <param name="field">The <see cref="FieldInfo" /> whose visibility to write.</param>
        Public Sub WriteVisibilityModifier(target As SyntaxNode, field As FieldInfo)
            WriteVisibilityModifier(target, field.Attributes)
        End Sub

        Private Sub WriteVisibilityModifier(type As SyntaxNode, attributes As FieldAttributes)
            Select Case attributes And FieldAttributes.FieldAccessMask
                Case FieldAttributes.Public
                    type.AddChild(PublicKeyword)

                Case FieldAttributes.FamANDAssem
                    type.AddChild(PrivateKeyword)
                    type.AddChild(ProtectedKeyword)

                Case FieldAttributes.FamORAssem
                    type.AddChild(ProtectedKeyword)
                    type.AddChild(FriendKeyword)

                Case FieldAttributes.Assembly
                    type.AddChild(FriendKeyword)

                Case FieldAttributes.Family
                    type.AddChild(ProtectedKeyword)

                Case FieldAttributes.Private
                    type.AddChild(PrivateKeyword)
            End Select
        End Sub
    End Module
End Namespace
