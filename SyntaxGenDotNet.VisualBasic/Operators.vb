Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Friend Module Operators
    ''' <summary>
    '''     The <c>=</c> operator.
    ''' </summary>
    Public ReadOnly Assignment As New OperatorToken("=", False) With {
        .TrailingWhitespace = WhitespaceTrivia.Space
        }

    ''' <summary>
    '''     The <c>)</c> operator.
    ''' </summary>
    Public ReadOnly CloseParenthesis As New OperatorToken(")")

    ''' <summary>
    '''     The <c>,</c> operator.
    ''' </summary>
    Public ReadOnly Comma As New OperatorToken(",")

    ''' <summary>
    '''     The <c>.</c> operator.
    ''' </summary>
    Public ReadOnly Dot As New OperatorToken(".")

    ''' <summary>
    '''     The <c>(</c> operator.
    ''' </summary>
    Public ReadOnly OpenParenthesis As New OperatorToken("(")
End Module