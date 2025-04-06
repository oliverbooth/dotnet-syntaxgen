Imports SyntaxGenDotNet.Syntax.Tokens

''' <summary>
'''     Represents a Visual Basic <see cref="Single" /> literal token.
''' </summary>
Friend NotInheritable Class VisualBasicSingleLiteralToken
    Inherits FloatingPointLiteralToken

    ''' <summary>
    '''     Initializes a new instance of the <see cref="VisualBasicSingleLiteralToken" /> class.
    ''' </summary>
    ''' <param name="literalValue">The value of the literal.</param>
    Public Sub New(literalValue As Single)
        MyBase.New($"{literalValue}F")
    End Sub
End Class
