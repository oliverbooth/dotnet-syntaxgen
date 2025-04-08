Imports System.Reflection
Imports SyntaxGenDotNet.Syntax

Namespace Utilities
    Partial Public Module ModifierUtility
        ''' <summary>
        '''     Writes all modifiers for the specified <see cref="MethodBase" /> to the specified <see cref="SyntaxNode" />. 
        ''' </summary>
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
        ''' <param name="method">The <see cref="MethodBase" /> whose modifiers to write.</param>
        Public Sub WriteAllModifiers(target As SyntaxNode, method As MethodInfo)
            WriteVisibilityModifier(target, method)
            WriteInheritanceModifiers(target, method)
        End Sub

        ''' <summary>
        '''     Writes the inheritance modifiers for the specified <see cref="MethodBase" /> to the specified
        '''     <see cref="SyntaxNode" />. 
        ''' </summary>
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the inheritance modifiers will be written.</param>
        ''' <param name="method">The <see cref="MethodBase" /> whose modifiers to write.</param>
        Public Sub WriteInheritanceModifiers(target As SyntaxNode, method As MethodInfo)
            Dim attributes As MethodAttributes = method.Attributes
            Dim isAbstract As Boolean = (attributes And MethodAttributes.Abstract) <> 0
            Dim isFinal As Boolean = (attributes And MethodAttributes.Final) <> 0
            Dim isNewSlot As Boolean = (attributes And MethodAttributes.NewSlot) <> 0

            If isAbstract Then
                target.AddChild(MustOverrideKeyword)
            ElseIf Not isFinal AndAlso isNewSlot Then
                target.AddChild(OverridableKeyword)
            ElseIf isFinal AndAlso Not isNewSlot Then
                target.AddChild(NotOverridableKeyword)
            ElseIf method.DeclaringType <> method.GetBaseDefinition().DeclaringType Then
                target.AddChild(OverridesKeyword)
            End If
        End Sub

        ''' <summary>
        '''     Writes the visibility modifier for the specified <see cref="MethodBase" /> to the specified <see cref="SyntaxNode" />.
        ''' </summary> 
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
        ''' <param name="method">The <see cref="MethodBase" /> whose visibility to write.</param>
        Public Sub WriteVisibilityModifier(target As SyntaxNode, method As MethodBase)
            WriteVisibilityModifier(target, method.Attributes)
        End Sub

        Private Sub WriteVisibilityModifier(type As SyntaxNode, attributes As MethodAttributes)
            Select Case attributes And MethodAttributes.MemberAccessMask
                Case MethodAttributes.Public
                    type.AddChild(PublicKeyword)

                Case MethodAttributes.FamANDAssem
                    type.AddChild(PrivateKeyword)
                    type.AddChild(ProtectedKeyword)

                Case MethodAttributes.FamORAssem
                    type.AddChild(ProtectedKeyword)
                    type.AddChild(FriendKeyword)

                Case MethodAttributes.Assembly
                    type.AddChild(FriendKeyword)

                Case MethodAttributes.Family
                    type.AddChild(ProtectedKeyword)

                Case MethodAttributes.Private
                    type.AddChild(PrivateKeyword)
            End Select

            If (attributes And MethodAttributes.Static) <> 0 Then
                type.AddChild(SharedKeyword)
            End If
        End Sub
    End Module
End Namespace
