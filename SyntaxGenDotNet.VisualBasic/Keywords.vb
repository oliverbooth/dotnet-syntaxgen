Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Public Module Keywords
    ''' <summary>
    '''     The <c>As</c> keyword.
    ''' </summary>
    Public ReadOnly AsKeyword As New KeywordToken("As") With {
        .LeadingWhitespace = WhitespaceTrivia.Space
        }

    ''' <summary>
    '''     The <c>Const</c> keyword.
    ''' </summary>
    Public ReadOnly ConstKeyword As New KeywordToken("Const")

    ''' <summary>
    '''     The <c>False</c> keyword.
    ''' </summary>
    Public ReadOnly FalseKeyword As New KeywordToken("False")

    ''' <summary>
    '''     The <c>Friend</c> keyword.
    ''' </summary>
    Public ReadOnly FriendKeyword As New KeywordToken("Friend")

    ''' <summary>
    '''     The <c>Nothing</c> keyword.
    ''' </summary>
    Public ReadOnly NothingKeyword As New KeywordToken("Nothing")

    ''' <summary>
    '''     The <c>Of</c> keyword.
    ''' </summary>
    Public ReadOnly OfKeyword As New KeywordToken("Of")

    ''' <summary>
    '''     The <c>Private</c> keyword.
    ''' </summary>
    Public ReadOnly PrivateKeyword As New KeywordToken("Private")

    ''' <summary>
    '''     The <c>Protected</c> keyword.
    ''' </summary>
    Public ReadOnly ProtectedKeyword As New KeywordToken("Protected")

    ''' <summary>
    '''     The <c>Public</c> keyword.
    ''' </summary>
    Public ReadOnly PublicKeyword As New KeywordToken("Public")

    ''' <summary>
    '''     The <c>True</c> keyword.
    ''' </summary>
    Public ReadOnly TrueKeyword As New KeywordToken("True")
End Module
