Imports System.Collections.ObjectModel
Imports System.Linq.Expressions
Imports System.Reflection
Imports SyntaxGenDotNet.Attributes
Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Namespace Utilities
    Friend Module AttributeUtility
        ''' <summary>
        '''     Writes a custom attribute to the specified node.
        ''' </summary>
        ''' <param name="target">The node to which to write the attribute.</param>
        ''' <param name="attributeExpression">The attribute expression to write.</param>
        Public Sub WriteCustomAttribute(target As SyntaxNode, attributeExpression As MemberInitExpression)
            Dim options As New TypeWriteOptions With {
                    .WriteAlias = False,
                    .WriteNamespace = True,
                    .TrimAttributeSuffix = True
                    }

            ' explicit creation of the open bracket token to avoid the trailing whitespace
            ' of the previous token being trimmed, as Operators.OpenBracket defaults to trimming.
            Dim openBracket As New OperatorToken(OpenChevron.Text, False)

            target.AddChild(openBracket)
            WriteAlias(target, attributeExpression.Type, options)

            Dim arguments As ReadOnlyCollection(Of Expression) = attributeExpression.NewExpression.Arguments
            Dim hasArguments As Boolean = arguments.Count > 0
            Dim hasBindings As Boolean = attributeExpression.Bindings.Count > 0
            Dim writeParentheses As Boolean = hasArguments Or hasBindings

            If writeParentheses Then
                target.AddChild(OpenParenthesis)
            End If

            If hasArguments Then
                WriteArguments(target, arguments)
            End If

            If hasBindings Then
                WriteBindings(hasArguments, target, attributeExpression, options)
            End If

            If writeParentheses Then
                target.AddChild(CloseParenthesis)
            End If

            target.AddChild(CloseChevron.With(Sub(o) o.TrailingWhitespace = WhitespaceTrivia.NewLine))
        End Sub

        ''' <summary>
        '''     Writes all known supported custom attribute to the specified node.
        ''' </summary>
        ''' <param name="generator">The syntax generator.</param>
        ''' <param name="target">The node to which to write the attributes.</param>
        ''' <param name="member">The member whose custom attributes to write.</param>
        Public Sub WriteCustomAttributes(generator As SyntaxGenerator, target As SyntaxNode, member As MemberInfo)
            For Each writer As AttributeExpressionWriter In generator.AttributeExpressionWriters
                Dim attributeType As Type = writer.AttributeType
                Dim attributes As Attribute() = member.GetCustomAttributes(attributeType, False).OfType (Of Attribute)().ToArray()
                Dim expressions As IEnumerable(Of Expression) = writer.CreateAttributeExpressions(member, attributes)

                For Each expression As Expression In expressions
                    If expression.NodeType <> ExpressionType.MemberInit Then
                        Continue For
                    End If

                    Dim memberInitExpression = DirectCast(expression, MemberInitExpression)
                    WriteCustomAttribute(target, memberInitExpression)
                Next
            Next
        End Sub

        Private Sub WriteArguments(target As SyntaxNode, arguments As ReadOnlyCollection(Of Expression))
            For index = 0 To arguments.Count - 1
                Dim argument As Expression = arguments(index)

                If argument.Type.IsEnum Then
                    WriteName(target, argument.Type)
                    target.AddChild(Dot)
                End If

                target.AddChild(CreateLiteralToken(argument))
            Next
        End Sub

        Private Sub WriteBindings(writeComma As Boolean,
                                  target As SyntaxNode,
                                  expression As MemberInitExpression,
                                  options as TypeWriteOptions)
            Dim comma As SyntaxNode = Operators.Comma.With(Sub(o) o.TrailingWhitespace = WhitespaceTrivia.Space)
            Dim bindings As ReadOnlyCollection(Of MemberBinding) = expression.Bindings
            options.WriteNamespace = False

            For index = 0 To bindings.Count - 1
                Dim binding As MemberBinding = bindings(index)
                If binding.BindingType <> MemberBindingType.Assignment Then
                    Continue For
                End If

                Dim assignment = DirectCast(binding, MemberAssignment)

                If index > 0 Or writeComma Then
                    target.AddChild(comma)
                End If

                target.AddChild(New IdentifierToken(binding.Member.Name))
                target.AddChild(Colon)
                target.AddChild(Operators.Assignment)

                If assignment.Expression.Type.IsEnum Then
                    WriteName(target, assignment.Expression.Type, options)
                    target.AddChild(Dot)
                End If

                target.AddChild(CreateLiteralToken(assignment.Expression))
            Next
        End Sub
    End Module
End NameSpace