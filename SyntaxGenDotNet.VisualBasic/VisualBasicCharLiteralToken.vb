Imports SyntaxGenDotNet.Syntax.Tokens

''' <summary>
'''     Represents a Visual Basic character literal token.
''' </summary>
Friend NotInheritable Class VisualBasicCharLiteralToken
    Inherits CharLiteralToken

    ''' <summary>
    '''     Initializes a new instance of the <see cref="VisualBasicCharLiteralToken" /> class.
    ''' </summary>
    ''' <param name="literalValue">The literal value.</param>
    Public Sub New(literalValue As Char)
        MyBase.New($"""{literalValue}""c")
    End Sub
End Class