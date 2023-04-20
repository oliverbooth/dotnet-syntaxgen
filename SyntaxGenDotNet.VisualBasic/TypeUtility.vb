Imports System.Diagnostics.CodeAnalysis
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
    '''     Writes the type name to the specified node.
    ''' </summary>
    ''' <param name="node">The node to which to write the type name.</param>
    ''' <param name="type">The type for which to write the name.</param>
    ''' <param name="trimAttributeSuffix">
    '''     <see langword="true" /> to trim the "Attribute" suffix from the type name; otherwise,
    '''     <see langword="false" />. The default is <see langword="false" />.
    ''' </param>
    Public Sub WriteTypeName(node As SyntaxNode, type As Type, Optional trimAttributeSuffix As Boolean = False)
        If type.IsGenericParameter Then
            node.AddChild(New IdentifierToken(type.Name))
            Return
        End If

        Dim languageAlias As KeywordToken = Nothing
        If TryGetLanguageAliasToken(type, languageAlias) Then
            node.AddChild(languageAlias)
            Return
        End If

        WriteNamespacedTypeName(node, type, trimAttributeSuffix)
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

    Private Sub WriteGenericArguments(node As SyntaxNode, type As Type)
        node.AddChild(OpenParenthesis)
        node.AddChild(OfKeyword)
        Dim genericArguments As Type() = type.GetGenericArguments()
        For index = 0 To genericArguments.Length - 1
            If index > 0 Then
                node.AddChild(Comma)
            End If

            WriteTypeName(node, genericArguments(index))
        Next

        node.AddChild(CloseParenthesis)
    End Sub
End Module
