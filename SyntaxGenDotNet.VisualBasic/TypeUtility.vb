Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Friend Module TypeUtility
    Friend ReadOnly RecognizedAttributes As New List(Of Type)() From {GetType(CLSCompliantAttribute)}

    Private ReadOnly LanguageAliases As New Dictionary(Of Type, KeywordToken) From {
        {GetType(Byte), New KeywordToken("Byte")},
        {GetType(SByte), New KeywordToken("SByte")},
        {GetType(Short), New KeywordToken("Short")},
        {GetType(UShort), New KeywordToken("UShort")},
        {GetType(Integer), New KeywordToken("Integer")},
        {GetType(UInteger), New KeywordToken("UInteger")},
        {GetType(Long), New KeywordToken("Long")},
        {GetType(ULong), New KeywordToken("ULong")},
        {GetType(Single), New KeywordToken("Single")},
        {GetType(Double), New KeywordToken("Double")},
        {GetType(Decimal), New KeywordToken("Decimal")},
        {GetType(String), New KeywordToken("String")},
        {GetType(Boolean), New KeywordToken("Boolean")},
        {GetType(Char), New KeywordToken("Char")},
        {GetType(IntPtr), New KeywordToken("IntPtr")},
        {GetType(UIntPtr), New KeywordToken("UIntPtr")},
        {GetType(Object), New KeywordToken("Object")}
        }

    ''' <summary>
    '''     Attempts to get the language alias for the specified type.
    ''' </summary>
    ''' <param name="type">The type for which to get the alias.</param>
    ''' <param name="languageAlias">
    '''     When this method returns, contains the alias for the specified type if the type has an alias; otherwise,
    '''     <see langword="null" />.
    ''' </param>
    ''' <returns>
    '''     <see langword="true" /> if the specified type has an alias; otherwise, <see langword="false" />.
    ''' </returns>
    Public Function TryGetLanguageAliasToken(type As Type, <NotNullWhen(True)> ByRef languageAlias As KeywordToken) As Boolean
        languageAlias = Nothing
        Return LanguageAliases.TryGetValue(type, languageAlias)
    End Function

    ''' <summary>
    '''     Writes the generic arguments for the specified type to the specified node.
    ''' </summary>
    ''' <param name="node">The node to which to write the generic arguments.</param>
    ''' <param name="type">The type for which to write the generic arguments.</param>
    Public Sub WriteGenericArguments(node As SyntaxNode, type As Type)
        If Not type.IsGenericType Then
            Return
        End If

        node.AddChild(OpenParenthesis)
        node.AddChild(OfKeyword)
        Dim genericArguments As Type() = type.GetGenericArguments()
        For index = 0 To genericArguments.Length - 1
            Dim genericArgument As Type = genericArguments(index)
            WriteParameterVariance(node, genericArgument)
            WriteTypeName(node, genericArgument)

            If index < genericArguments.Length - 1 Then
                node.AddChild(Comma.With(Sub(o) o.TrailingWhitespace = " "))
            End If
        Next

        node.AddChild(CloseParenthesis)
    End Sub

    ''' <summary>
    '''     Writes the modifiers for the specified type to the specified node.
    ''' </summary>
    ''' <param name="node">The node to which to write the modifiers.</param>
    ''' <param name="type">The type for which to write the modifiers.</param>
    Public Sub WriteTypeModifiers(node As SyntaxNode, type As Type)
        If type.IsInterface Or type.IsValueType Then
            Return
        End If

        If type.IsAbstract And Not type.IsSealed Then
            node.AddChild(MustInheritKeyword)
        ElseIf type.IsSealed And Not type.IsAbstract Then
            node.AddChild(NotInheritableKeyword)
        End If
    End Sub

    ''' <summary>
    '''     Writes the type name to the specified node.
    ''' </summary>
    ''' <param name="node">The node to which to write the type name.</param>
    ''' <param name="type">The type for which to write the name.</param>
    Public Sub WriteTypeName(node As SyntaxNode, type As Type)
        WriteTypeName(node, type, TypeWriteOptions.DefaultOptions)
    End Sub

    ''' <summary>
    '''     Writes the type name to the specified node.
    ''' </summary>
    ''' <param name="node">The node to which to write the type name.</param>
    ''' <param name="type">The type for which to write the name.</param>
    ''' <param name="options">The options for writing the type name.</param>
    Public Sub WriteTypeName(node As SyntaxNode, type As Type, options As TypeWriteOptions)
        Dim typeName As String = type.FullName
        If typeName Is Nothing Or Not options.WriteNamespace Then
            typeName = type.Name
        End If

        If type.IsGenericParameter Then
            node.AddChild(New TypeIdentifierToken(typeName))
            Return
        End If

        Dim languageAlias As KeywordToken = Nothing
        If options.WriteAlias And TryGetLanguageAliasToken(type, languageAlias) Then
            node.AddChild(languageAlias)
            Return
        End If

        If Not options.WriteNamespace Then
            If type.IsGenericType Then
                typeName = type.Name.Substring(0, typeName.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal))
            End If

            node.AddChild(New TypeIdentifierToken(typeName))
            Return
        End If

        WriteNamespacedTypeName(node, type, options.TrimAttributeSuffix)
    End Sub

    ''' <summary>
    '''     Writes the visibility keyword for a type declaration.
    ''' </summary>
    ''' <param name="declaration">The declaration to write to.</param>
    ''' <param name="type">The type whose visibility to write.</param>
    Public Sub WriteTypeVisibilityKeyword(declaration As SyntaxNode, type As Type)
        Const mask = TypeAttributes.VisibilityMask

        Select Case type.Attributes And mask
            Case TypeAttributes.Public, TypeAttributes.NestedPublic
                declaration.AddChild(PublicKeyword)

            Case TypeAttributes.NestedPrivate
                declaration.AddChild(PrivateKeyword)

            Case TypeAttributes.NestedFamANDAssem
                declaration.AddChild(PrivateKeyword)
                declaration.AddChild(ProtectedKeyword)

            Case TypeAttributes.NestedFamORAssem
                declaration.AddChild(ProtectedKeyword)
                declaration.AddChild(FriendKeyword)

            Case TypeAttributes.NestedFamily
                declaration.AddChild(ProtectedKeyword)

            Case Else
                declaration.AddChild(FriendKeyword)
        End Select
    End Sub

    Private Sub WriteNamespacedTypeName(node As SyntaxNode, type As Type, Optional trimAttributeSuffix As Boolean = False)
        If type.IsArray Then
            WriteTypeName(node, type.GetElementType())

            Dim arrayRank As Integer = type.GetArrayRank()
            For index = 0 To arrayRank - 1
                node.AddChild(OpenParenthesis)
                node.AddChild(CloseParenthesis)
            Next

            Return
        End If

        Dim fullName As String = type.FullName
        If fullName Is Nothing Then
            fullName = type.Name
        End If

        If type.IsGenericType Then
            Dim index As Integer = fullName.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal)
            fullName = fullName.Substring(0, index)
        End If

        WriteFullName(node, fullName, trimAttributeSuffix)

        If type.IsGenericType Then
            WriteGenericArguments(node, type)
        End If
    End Sub

    Private Sub WriteFullName(node As SyntaxNode, fullName As String, trimAttributeSuffix As Boolean)
        Dim namespaces As String() = fullName.Split(ILOperators.NamespaceSeparator.Text)
        For index = 0 To namespaces.Length - 1
            Dim name As String = namespaces(index)
            If trimAttributeSuffix And name.EndsWith("Attribute", StringComparison.Ordinal) Then
                name = name.Substring(0, name.Length - 9)
            End If
            node.AddChild(New TypeIdentifierToken(name))

            If index < namespaces.Length - 1 Then
                node.AddChild(Dot)
            End If
        Next
    End Sub

    Private Sub WriteParameterVariance(node As SyntaxNode, type As Type)
        If Not type.IsGenericParameter Then
            Return
        End If

        Const mask = GenericParameterAttributes.VarianceMask
        Dim attributes = type.GenericParameterAttributes

        Select Case attributes And mask
            Case GenericParameterAttributes.Contravariant
                node.AddChild(InKeyword)

            Case GenericParameterAttributes.Covariant
                node.AddChild(OutKeyword)
        End Select
    End Sub
End Module
