Imports System.Reflection
Imports SyntaxGenDotNet.Syntax

Namespace Utilities
    Friend Partial Module ModifierUtility
        ''' <summary>
        '''     Writes the pass-by modifiers for the specified <see cref="ParameterInfo" /> to the specified
        '''     <see cref="SyntaxNode" />. 
        ''' </summary>
        ''' <param name="target">The <see cref="SyntaxNode" /> to which the pass-by modifiers will be written.</param>
        ''' <param name="parameter">The <see cref="ParameterInfo" /> whose modifiers to write.</param>
        Public Sub WritePassByModifier(target As SyntaxNode, parameter As ParameterInfo)
            If parameter.IsOptional Then
                target.AddChild(OptionalKeyword)
            End If

            If parameter.IsOut Or parameter.ParameterType.IsByRef Then
                target.AddChild(ByRefKeyword)
            Else
                target.AddChild(ByValKeyword)
            End If
        End Sub
    End Module
End Namespace
