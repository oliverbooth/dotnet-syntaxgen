
Imports System.Reflection
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc/>
    Public Function GenerateDelegateDeclaration(delegateType As Type) As TypeDeclaration _
        Implements ISyntaxGenerator.GenerateDelegateDeclaration
        Trace.Assert(delegateType.IsSubclassOf(GetType(MulticastDelegate)) Or delegateType.IsSubclassOf(GetType([Delegate])),
                     "The specified type is not a delegate.")

        Dim invokeMethod As MethodInfo = delegateType.GetMethod("Invoke")

        Dim delegateDeclaration As New TypeDeclaration()
        WriteTypeVisibilityKeyword(delegateDeclaration, delegateType)
        delegateDeclaration.AddChild(DelegateKeyword)

        If invokeMethod.ReturnType = GetType(Void) Then
            delegateDeclaration.AddChild(SubKeyword)
        Else
            delegateDeclaration.AddChild(FunctionKeyword)
        End If

        Dim name As String = delegateType.Name
        If delegateType.IsGenericType Then
            name = name.Substring(0, name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal))
        End If

        delegateDeclaration.AddChild(New TypeIdentifierToken(name))
        WriteGenericArguments(delegateDeclaration, delegateType)
        delegateDeclaration.AddChild(OpenParenthesis)

        Dim parameters As ParameterInfo() = invokeMethod.GetParameters()
        For index = 0 To parameters.Length - 1
            Dim parameter As ParameterInfo = parameters(index)

            If parameter.Name IsNot Nothing Then
                delegateDeclaration.AddChild(New IdentifierToken(parameter.Name))
            End If

            delegateDeclaration.AddChild(AsKeyword)
            WriteTypeName(delegateDeclaration, parameter.ParameterType)

            If index < parameters.Length - 1 Then
                delegateDeclaration.AddChild(Comma.With(Sub(o) o.TrailingWhitespace = " "))
            End If
        Next

        delegateDeclaration.AddChild(CloseParenthesis)

        If invokeMethod.ReturnType <> GetType(Void) Then
            delegateDeclaration.AddChild(AsKeyword)
            WriteTypeName(delegateDeclaration, invokeMethod.ReturnType)
        End If

        Return delegateDeclaration
    End Function
End Class
