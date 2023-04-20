Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Friend Module Keywords
    ''' <summary>
    '''     The <c>As</c> keyword.
    ''' </summary>
    Public ReadOnly AsKeyword As New KeywordToken("As") With {
        .LeadingWhitespace = WhitespaceTrivia.Space
        }

    ''' <summary>
    '''     The <c>Class</c> keyword.
    ''' </summary>
    Public ReadOnly ClassKeyword As New KeywordToken("Class")

    ''' <summary>
    '''     The <c>Const</c> keyword.
    ''' </summary>
    Public ReadOnly ConstKeyword As New KeywordToken("Const")

    ''' <summary>
    '''     The <c>Delegate</c> keyword.
    ''' </summary>
    Public ReadOnly DelegateKeyword As New KeywordToken("Delegate")

    ''' <summary>
    '''     The <c>Enum</c> keyword.
    ''' </summary>
    Public ReadOnly EnumKeyword As New KeywordToken("Enum")

    ''' <summary>
    '''     The <c>False</c> keyword.
    ''' </summary>
    Public ReadOnly FalseKeyword As New KeywordToken("False")

    ''' <summary>
    '''     The <c>Function</c> keyword.
    ''' </summary>
    Public ReadOnly FunctionKeyword As New KeywordToken("Function")

    ''' <summary>
    '''     The <c>Friend</c> keyword.
    ''' </summary>
    Public ReadOnly FriendKeyword As New KeywordToken("Friend")

    ''' <summary>
    '''     The <c>In</c> keyword.
    ''' </summary>
    Public ReadOnly InKeyword As New KeywordToken("In")

    ''' <summary>
    '''     The <c>Interface</c> keyword.
    ''' </summary>
    Public ReadOnly InterfaceKeyword As New KeywordToken("Interface")

    ''' <summary>
    '''     The <c>Inherits</c> keyword.
    ''' </summary>
    Public ReadOnly InheritsKeyword As New KeywordToken("Inherits")

    ''' <summary>
    '''     The <c>Implements</c> keyword.
    ''' </summary>
    Public ReadOnly ImplementsKeyword As New KeywordToken("Implements")

    ''' <summary>
    '''     The <c>MustInherit</c> keyword.
    ''' </summary>
    Public ReadOnly MustInheritKeyword As New KeywordToken("MustInherit")

    ''' <summary>
    '''     The <c>MustOverride</c> keyword.
    ''' </summary>
    Public ReadOnly MustOverrideKeyword As New KeywordToken("MustOverride")

    ''' <summary>
    '''     The <c>Nothing</c> keyword.
    ''' </summary>
    Public ReadOnly NothingKeyword As New KeywordToken("Nothing")

    ''' <summary>
    '''     The <c>NotInheritable</c> keyword.
    ''' </summary>
    Public ReadOnly NotInheritableKeyword As New KeywordToken("NotInheritable")

    ''' <summary>
    '''     The <c>NotOverridable</c> keyword.
    ''' </summary>
    Public ReadOnly NotOverridableKeyword As New KeywordToken("NotOverridable")

    ''' <summary>
    '''     The <c>Of</c> keyword.
    ''' </summary>
    Public ReadOnly OfKeyword As New KeywordToken("Of")

    ''' <summary>
    '''     The <c>Out</c> keyword.
    ''' </summary>
    Public ReadOnly OutKeyword As New KeywordToken("Out")

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
    '''     The <c>ReadOnly</c> keyword.
    ''' </summary>
    Public ReadOnly ReadOnlyKeyword As New KeywordToken("ReadOnly")

    ''' <summary>
    '''     The <c>Shared</c> keyword.
    ''' </summary>
    Public ReadOnly SharedKeyword As New KeywordToken("Shared")

    ''' <summary>
    '''     The <c>Structure</c> keyword.
    ''' </summary>
    Public ReadOnly StructureKeyword As New KeywordToken("Structure")

    ''' <summary>
    '''     The <c>Sub</c> keyword.
    ''' </summary>
    Public ReadOnly SubKeyword As New KeywordToken("Sub")

    ''' <summary>
    '''     The <c>True</c> keyword.
    ''' </summary>
    Public ReadOnly TrueKeyword As New KeywordToken("True")
End Module
