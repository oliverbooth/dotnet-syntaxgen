Imports System.Reflection
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens
Imports SyntaxGenDotNet.VisualBasic.Extensions
Imports SyntaxGenDotNet.VisualBasic.Utilities

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Overrides Function GenerateMethodDeclaration(methodInfo As MethodInfo) As MethodDeclaration
        Dim declaration As New MethodDeclaration()
        WriteCustomAttributes(Me, declaration, methodInfo)
        WriteAllModifiers(declaration, methodInfo)
        WriteMethodTypeSignature(declaration, methodInfo, True)
        Return declaration
    End Function

    Private Sub WriteMethodTypeSignature(target As SyntaxNode, methodInfo As MethodInfo,
                                         Optional writeGenericArguments As Boolean = False)
        target.AddChild(If(methodInfo.IsSub(), SubKeyword, FunctionKeyword))
        target.AddChild(New IdentifierToken(methodInfo.Name))

        If writeGenericArguments Then
            Dim genericArguments As Type() = methodInfo.GetGenericArguments()
            TypeUtility.WriteGenericArguments(target, genericArguments)
        End If

        target.AddChild(OpenParenthesis)
        WriteParameters(target, methodInfo.GetParameters())
        target.AddChild(CloseParenthesis)

        If methodInfo.IsFunction() Then
            target.AddChild(AsKeyword)
            WriteAlias(target, methodInfo.ReturnType)
        End If
    End Sub

    Private Shared Sub WriteParameter(target As SyntaxNode, parameter As ParameterInfo)
        If parameter.Name Is Nothing Then
            Return
        End If

        WritePassByModifier(target, parameter)
        target.AddChild(New IdentifierToken(parameter.Name))
        target.AddChild(AsKeyword)
        WriteAlias(target, parameter.ParameterType)

        If Not parameter.IsOptional Then
            Return
        End If

        target.AddChild(Assignment)
        target.AddChild(CreateLiteralToken(parameter.DefaultValue))
    End Sub

    Private Sub WriteParameters(target As SyntaxNode, parameters As IReadOnlyList(Of ParameterInfo))
        Dim comma As SyntaxNode = If(parameters.Count > 1,
                                     Operators.Comma.With(Sub(o) o.TrailingWhitespace = WhitespaceTrivia.Space),
                                     Nothing)

        For index = 0 To parameters.Count - 1
            WriteParameter(target, parameters(index))

            If index < parameters.Count - 1 Then
                target.AddChild(comma)
            End If
        Next
    End Sub
End Class
