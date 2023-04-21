Imports System.Reflection
Imports SyntaxGenDotNet.Extensions
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Declaration
Imports SyntaxGenDotNet.Syntax.Tokens
Imports SyntaxGenDotNet.VisualBasic.Extensions
Imports SyntaxGenDotNet.VisualBasic.Utilities

Public Partial Class VisualBasicSyntaxGenerator
    ''' <inheritdoc />
    Public Overrides Function GenerateTypeDeclaration(type As Type) As TypeDeclaration
        Dim declaration As New TypeDeclaration()
        WriteCustomAttributes(Me, declaration, type)

        If type.IsEnum Then
            WriteEnumDeclaration(declaration, type)
        ElseIf type.IsSubclassOf(GetType(MulticastDelegate)) Or type.IsSubclassOf(GetType([Delegate])) Then
            WriteDelegateDeclaration(declaration, type)
        Else
            WriteClassDeclaration(declaration, type)
        End If

        Return declaration
    End Function

    Private Shared Sub WriteClassDeclaration(target As SyntaxNode, type As Type)
        WriteAllModifiers(target, type)
        WriteTypeModifiers(target, type)
        WriteTypeKind(target, type)
        WriteName(target, type)
        WriteBaseType(target, type)
        WriteInterfaces(target, type)
    End Sub

    Private Shared Sub WriteDelegateDeclaration(target As SyntaxNode, delegateType As Type)
        Dim invokeMethod As MethodInfo = delegateType.GetMethod("Invoke")
        WriteVisibilityModifier(target, delegateType)
        target.AddChild(DelegateKeyword)

        target.AddChild(If(invokeMethod.IsSub(), SubKeyword, FunctionKeyword))

        Dim name As String = delegateType.Name
        If delegateType.IsGenericType Then
            name = name.Substring(0, name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal))
        End If

        target.AddChild(New TypeIdentifierToken(name))
        WriteGenericArguments(target, delegateType)
        target.AddChild(OpenParenthesis)

        Dim parameters As ParameterInfo() = invokeMethod.GetParameters()
        For index = 0 To parameters.Length - 1
            Dim parameter As ParameterInfo = parameters(index)

            If parameter.Name IsNot Nothing Then
                target.AddChild(New IdentifierToken(parameter.Name))
            End If

            target.AddChild(AsKeyword)
            WriteTypeName(target, parameter.ParameterType)

            If index < parameters.Length - 1 Then
                target.AddChild(Comma.With(Sub(o) o.TrailingWhitespace = " "))
            End If
        Next

        target.AddChild(CloseParenthesis)

        If invokeMethod.IsFunction() Then
            target.AddChild(AsKeyword)
            WriteTypeName(target, invokeMethod.ReturnType)
        End If
    End Sub

    Private Shared Sub WriteEnumDeclaration(target As SyntaxNode, enumType As Type)
        WriteVisibilityModifier(target, enumType)
        target.AddChild(EnumKeyword)
        target.AddChild(new TypeIdentifierToken(enumType.Name))

        Dim enumUnderlyingType As Type = enumType.GetEnumUnderlyingType()
        If enumUnderlyingType <> GetType(Integer) Then
            target.AddChild(AsKeyword.With(Sub(k) k.LeadingWhitespace = WhitespaceTrivia.None))
            WriteTypeName(target, enumUnderlyingType)
        End If
    End Sub

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
