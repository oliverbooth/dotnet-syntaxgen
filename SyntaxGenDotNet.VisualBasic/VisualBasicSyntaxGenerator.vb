''' <summary>
'''     Represents a syntax generator for the Visual Basic language.
''' </summary>
Public Partial Class VisualBasicSyntaxGenerator
    Implements ISyntaxGenerator

    ''' <inheritdoc/>
    Public ReadOnly Property LanguageName As String Implements ISyntaxGenerator.LanguageName
        Get
            Return "VB"
        End Get
    End Property
End Class
