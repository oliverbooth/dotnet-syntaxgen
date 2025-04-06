
Imports System.Linq.Expressions
Imports SyntaxGenDotNet.Syntax.Tokens

Namespace Utilities
    Public Module TokenUtility
        ''' <summary>
        '''     Creates a literal token from a value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>The literal token.</returns>
        Public Function CreateLiteralToken(value As Object) As SyntaxToken
            If TypeOf value Is ConstantExpression Then
                Return CreateLiteralToken(DirectCast(value, ConstantExpression).Value)
            ElseIf value Is Nothing Then
                Return NothingKeyword
            ElseIf TypeOf value Is Boolean Then
                Return If (DirectCast(value, Boolean), TrueKeyword, FalseKeyword)
            ElseIf TypeOf value Is String Then
                Return New StringLiteralToken(DirectCast(value, String))
            ElseIf TypeOf value Is Char Then
                Return New VisualBasicCharLiteralToken(DirectCast(value, Char))
            ElseIf TypeOf value Is Byte Then
                Return New IntegerLiteralToken(DirectCast(value, Byte))
            ElseIf TypeOf value Is SByte Then
                Return New IntegerLiteralToken(DirectCast(value, SByte))
            ElseIf TypeOf value Is Short Then
                Return New IntegerLiteralToken(DirectCast(value, Short))
            ElseIf TypeOf value Is UShort Then
                Return New IntegerLiteralToken(DirectCast(value, UShort))
            ElseIf TypeOf value Is Integer Then
                Return New IntegerLiteralToken(DirectCast(value, Integer))
            ElseIf TypeOf value Is UInteger Then
                Return New IntegerLiteralToken(DirectCast(value, UInteger))
            ElseIf TypeOf value Is Long Then
                Return New IntegerLiteralToken(DirectCast(value, Long))
            ElseIf TypeOf value Is ULong Then
                Return New IntegerLiteralToken(DirectCast(value, ULong))
            ElseIf TypeOf value Is Double Then
                Return New FloatingPointLiteralToken(DirectCast(value, Double))
            ElseIf TypeOf value Is Single Then
                Return New VisualBasicSingleLiteralToken(DirectCast(value, Single))
            Else
                Return New LiteralToken(value.ToString())
            End If
        End Function
    End Module
End Namespace
