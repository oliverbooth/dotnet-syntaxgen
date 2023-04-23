Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Friend Module Operators
    ''' <summary>
    '''     The <c>=</c> operator.
    ''' </summary>
    Public ReadOnly Property Assignment As OperatorToken
        Get
            Return New OperatorToken("=", False) With {
                .TrailingWhitespace = WhitespaceTrivia.Space
                }
        End Get
    End Property

    ''' <summary>
    '''     The <c>&gt;</c> operator.
    ''' </summary>
    Public ReadOnly Property CloseChevron As OperatorToken
        Get
            Return New OperatorToken(">")

            ''' <summary>
        End Get
    End Property

    '''     The <c>)</c> operator.
    ''' </summary>
    Public ReadOnly Property CloseParenthesis As OperatorToken
        Get
            Return New OperatorToken(")")

            ''' <summary>
        End Get
    End Property

    '''     The <c>:</c> operator.
    ''' </summary>
    Public ReadOnly Property Colon As OperatorToken
        Get
            Return New OperatorToken(":") With {
                .LeadingWhitespace = WhitespaceTrivia.Space
                }
        End Get
    End Property

    ''' <summary>
    '''     The <c>,</c> operator.
    ''' </summary>
    Public ReadOnly Property Comma As OperatorToken
        Get
            Return New OperatorToken(",") With {
                .TrailingWhitespace = WhitespaceTrivia.Space
                }
        End Get
    End Property

    ''' <summary>
    '''     The <c>.</c> operator.
    ''' </summary>
    Public ReadOnly Property Dot As OperatorToken
        Get
            Return New OperatorToken(".")
        End Get
    End Property

    ''' <summary>
    '''     The <c>&lt;</c> operator.
    ''' </summary>
    Public ReadOnly Property OpenChevron As OperatorToken
        Get
            Return New OperatorToken("<")
        End Get
    End Property

    ''' <summary>
    '''     The <c>(</c> operator.
    ''' </summary>
    Public ReadOnly Property OpenParenthesis As OperatorToken
        Get
            Return New OperatorToken("(")
        End Get
    End Property
End Module