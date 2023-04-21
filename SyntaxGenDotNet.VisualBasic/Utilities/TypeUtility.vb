
Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Namespace Utilities
    Friend Module TypeUtility
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
        '''     Writes the fully resolved name of the specified type to the specified node, or the alias if the type has one.
        ''' </summary>
        ''' <param name="target">The node to which to write the type name.</param>
        ''' <param name="type">The type whose name to write.</param>
        ''' <param name="options">The options to use when writing the type name.</param>
        Public Sub WriteAlias(target As SyntaxNode, type As Type, Optional options As Nullable(Of TypeWriteOptions) = Nothing)
            If type.IsPointer Then
                WriteAlias(target, GetType(IntPtr), options)
                Return
            End If

            While type.IsByRef
                type = type.GetElementType()
            End While

            If type.IsArray Then
                WriteAlias(target, type.GetElementType(), options)
                target.AddChild(OpenParenthesis)
                target.AddChild(CloseParenthesis)
                Return
            End If

            Dim [alias] As KeywordToken
            If TryGetLanguageAliasToken(type, [alias]) Then
                target.AddChild([alias])
                Return
            End If

            options = If(options.HasValue, options, TypeWriteOptions.DefaultOptions)
            WriteNamespace(target, type, options)
            WriteName(target, type, options)

            If options.Value.WriteGenericArguments Then
                WriteGenericArguments(target, type)
            End If
        End Sub

        ''' <summary>
        '''     Writes the generic arguments for the specified type to the specified node.
        ''' </summary>
        ''' <param name="target">The target node to which to write the generic arguments.</param>
        ''' <param name="type">The type whose generic arguments to write.</param>
        Public Sub WriteGenericArguments(target As SyntaxNode, type As Type)
            If Not type.IsGenericType Then
                Return
            End If

            WriteGenericArguments(target, type.GetGenericArguments())
        End Sub

        ''' <summary>
        '''     Writes the generic arguments to the specified node.
        ''' </summary>
        ''' <param name="target">The node to which to write the generic arguments.</param>
        ''' <param name="genericArguments">The generic arguments to write.</param>
        Public Sub WriteGenericArguments(target As SyntaxNode, genericArguments As IReadOnlyList(Of Type))
            If genericArguments.Count = 0 Then
                Return
            End If

            target.AddChild(OpenParenthesis)
            target.AddChild(OfKeyword)

            For index = 0 To genericArguments.Count - 1
                Dim genericArgument As Type = genericArguments(index)
                WriteParameterVariance(target, genericArgument)
                WriteTypeName(target, genericArgument)

                If index < genericArguments.Count - 1 Then
                    target.AddChild(Comma)
                End If
            Next

            target.AddChild(CloseParenthesis)
        End Sub

        ''' <summary>
        '''     Writes the name of the specified type to the specified node.
        ''' </summary>
        ''' <param name="target">The node to which to write the name.</param>
        ''' <param name="type">The type whose name to write.</param>
        ''' <param name="options">The options for writing the name.</param>
        Public Sub WriteName(target As SyntaxNode, type As Type, Optional options As Nullable(Of TypeWriteOptions) = Nothing)
            options = If(options.HasValue, options, TypeWriteOptions.DefaultOptions)

            Dim name As String = type.Name
            If type.IsGenericType Then
                name = name.Substring(0, name.IndexOf(ILOperators.GenericMarker.Text, StringComparison.Ordinal))
            End If

            If options.Value.TrimAttributeSuffix AndAlso name.EndsWith(NameOf(Attribute), StringComparison.Ordinal) Then
                name = name.Substring(0, name.Length - NameOf(Attribute).Length)
            End If

            target.AddChild(New TypeIdentifierToken(name))
        End Sub

        ''' <summary>
        '''     Writes the namespace for the specified type to the specified node.
        ''' </summary>
        ''' <param name="target">The node to which to write the namespace.</param>
        ''' <param name="type">The type whose namespace to write.</param>
        ''' <param name="options">The options for writing the namespace.</param>
        Public Sub WriteNamespace(target As SyntaxNode, type As Type, Optional options As Nullable(Of TypeWriteOptions) = Nothing)
            options = If(options.HasValue, options, TypeWriteOptions.DefaultOptions)

            If Not options.Value.WriteNamespace Or type.IsGenericParameter Then
                Return
            End If

            WriteNamespace(target, type.Namespace)

            If type.Namespace IsNot Nothing Then
                target.AddChild(Dot)
            End If
        End Sub

        ''' <summary>
        '''     Writes the specified namespace name the specified node.
        ''' </summary>
        ''' <param name="target">The node to which to write the namespace.</param>
        ''' <param name="namespaceName">The name of the namespace to write.</param>
        Public Sub WriteNamespace(target As SyntaxNode, namespaceName As String)
            If String.IsNullOrEmpty(namespaceName) Then
                Return
            End If

            Dim parts As String() = namespaceName.Split("."c)
            For index = 0 To parts.Length - 1
                target.AddChild(New TypeIdentifierToken(parts(index)))

                If index < parts.Length - 1 Then
                    target.AddChild(Dot)
                End If
            Next
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
End NameSpace
