Imports System.Reflection
Imports SyntaxGenDotNet.Syntax

Namespace Utilities
    Partial Public Module ModifierUtility
        ''' <summary>
        '''     Writes all modifiers for the specified <see cref="Type" /> to the specified <see cref="SyntaxNode" />.
        ''' </summary> 
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
        ''' <param name="type">The <see cref="Type" /> whose modifiers to write.</param>
        Public Sub WriteAllModifiers(target As SyntaxNode, type As Type)
            WriteVisibilityModifier(target, type)
            WriteInheritanceModifiers(target, type)
        End Sub

        ''' <summary>
        '''     Writes the inheritance modifiers for the specified <see cref="Type" /> to the specified
        '''     <see cref="SyntaxNode" />. 
        ''' </summary>
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the inheritance modifiers will be written.</param>
        ''' <param name="type">The <see cref="Type" /> whose modifiers to write.</param>
        Public Sub WriteInheritanceModifiers(target As SyntaxNode, type As Type)
            If type.IsInterface Or type.IsValueType Then
                Return
            End If

            WriteInheritanceModifiers(target, type.Attributes)
        End Sub

        ''' <summary>
        '''     Writes the visibility modifier for the specified <see cref="Type" /> to the specified <see cref="SyntaxNode" />.
        ''' </summary> 
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
        ''' <param name="type">The <see cref="Type" /> whose visibility to write.</param>
        Public Sub WriteVisibilityModifier(target As SyntaxNode, type As Type)
            WriteVisibilityModifier(target, type.Attributes)
        End Sub

        Private Sub WriteInheritanceModifiers(target As SyntaxNode, attributes As TypeAttributes)
            If (attributes And TypeAttributes.Interface) <> 0 Then
                Return
            End If

            If (attributes And TypeAttributes.Abstract) <> 0 AndAlso (attributes And TypeAttributes.Sealed) = 0 Then
                target.AddChild(MustInheritKeyword)
            ElseIf (attributes And TypeAttributes.Abstract) = 0 AndAlso (attributes And TypeAttributes.Sealed) <> 0 Then
                target.AddChild(NotInheritableKeyword)
            End If
        End Sub

        Private Sub WriteVisibilityModifier(target As SyntaxNode, attributes As TypeAttributes)
            Select Case attributes And TypeAttributes.VisibilityMask
                Case TypeAttributes.Public, TypeAttributes.NestedPublic
                    target.AddChild(PublicKeyword)

                Case TypeAttributes.NestedFamANDAssem
                    target.AddChild(PrivateKeyword)
                    target.AddChild(ProtectedKeyword)

                Case TypeAttributes.NestedFamORAssem
                    target.AddChild(ProtectedKeyword)
                    target.AddChild(FriendKeyword)

                Case TypeAttributes.NestedFamily
                    target.AddChild(ProtectedKeyword)

                Case TypeAttributes.NestedPrivate
                    target.AddChild(PrivateKeyword)

                Case Else
                    target.AddChild(FriendKeyword)
            End Select
        End Sub
    End Module
End Namespace
