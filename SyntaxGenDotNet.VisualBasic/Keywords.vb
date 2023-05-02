Imports SyntaxGenDotNet.Syntax
Imports SyntaxGenDotNet.Syntax.Tokens

Friend Module Keywords
    ''' <summary>
    '''     The <c>As</c> keyword.
    ''' </summary>
    Public ReadOnly Property AsKeyword As KeywordToken
        Get
            Return New KeywordToken("As") With {
                .LeadingWhitespace = WhitespaceTrivia.Space
                }
        End Get
    End Property

    ''' <summary>
    '''     The <c>ByRef</c> keyword.
    ''' </summary>
    Public ReadOnly Property ByRefKeyword As KeywordToken
        Get
            Return New KeywordToken("ByRef")
        End Get
    End Property

    ''' <summary>
    '''     The <c>ByVal</c> keyword.
    ''' </summary>
    Public ReadOnly Property ByValKeyword As KeywordToken
        Get
            Return New KeywordToken("ByVal")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Class</c> keyword.
    ''' </summary>
    Public ReadOnly Property ClassKeyword As KeywordToken
        Get
            Return New KeywordToken("Class")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Const</c> keyword.
    ''' </summary>
    Public ReadOnly Property ConstKeyword As KeywordToken
        Get
            Return New KeywordToken("Const")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Custom</c> keyword.
    ''' </summary>
    Public ReadOnly Property CustomKeyword As KeywordToken
        Get
            Return New KeywordToken("Custom")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Delegate</c> keyword.
    ''' </summary>
    Public ReadOnly Property DelegateKeyword As KeywordToken
        Get
            Return New KeywordToken("Delegate")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Enum</c> keyword.
    ''' </summary>
    Public ReadOnly Property EnumKeyword As KeywordToken
        Get
            Return New KeywordToken("Enum")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Event</c> keyword.
    ''' </summary>
    Public ReadOnly Property EventKeyword As KeywordToken
        Get
            Return New KeywordToken("Event")
        End Get
    End Property

    ''' <summary>
    '''     The <c>False</c> keyword.
    ''' </summary>
    Public ReadOnly Property FalseKeyword As KeywordToken
        Get
            Return New KeywordToken("False")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Function</c> keyword.
    ''' </summary>
    Public ReadOnly Property FunctionKeyword As KeywordToken
        Get
            Return New KeywordToken("Function")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Friend</c> keyword.
    ''' </summary>
    Public ReadOnly Property FriendKeyword As KeywordToken
        Get
            Return New KeywordToken("Friend")
        End Get
    End Property

    ''' <summary>
    '''     The <c>In</c> keyword.
    ''' </summary>
    Public ReadOnly Property InKeyword As KeywordToken
        Get
            Return New KeywordToken("In")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Interface</c> keyword.
    ''' </summary>
    Public ReadOnly Property InterfaceKeyword As KeywordToken
        Get
            Return New KeywordToken("Interface")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Inherits</c> keyword.
    ''' </summary>
    Public ReadOnly Property InheritsKeyword As KeywordToken
        Get
            Return New KeywordToken("Inherits")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Implements</c> keyword.
    ''' </summary>
    Public ReadOnly Property ImplementsKeyword As KeywordToken
        Get
            Return New KeywordToken("Implements")
        End Get
    End Property

    ''' <summary>
    '''     The <c>MustInherit</c> keyword.
    ''' </summary>
    Public ReadOnly Property MustInheritKeyword As KeywordToken
        Get
            Return New KeywordToken("MustInherit")
        End Get
    End Property

    ''' <summary>
    '''     The <c>MustOverride</c> keyword.
    ''' </summary>
    Public ReadOnly Property MustOverrideKeyword As KeywordToken
        Get
            Return New KeywordToken("MustOverride")
        End Get
    End Property

    ''' <summary>
    '''     The <c>New</c> keyword.
    ''' </summary>
    Public ReadOnly Property NewKeyword As KeywordToken
        Get
            Return New KeywordToken("New")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Nothing</c> keyword.
    ''' </summary>
    Public ReadOnly Property NothingKeyword As KeywordToken
        Get
            Return New KeywordToken("Nothing")
        End Get
    End Property

    ''' <summary>
    '''     The <c>NotInheritable</c> keyword.
    ''' </summary>
    Public ReadOnly Property NotInheritableKeyword As KeywordToken
        Get
            Return New KeywordToken("NotInheritable")
        End Get
    End Property

    ''' <summary>
    '''     The <c>NotOverridable</c> keyword.
    ''' </summary>
    Public ReadOnly Property NotOverridableKeyword As KeywordToken
        Get
            Return New KeywordToken("NotOverridable")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Of</c> keyword.
    ''' </summary>
    Public ReadOnly Property OfKeyword As KeywordToken
        Get
            Return New KeywordToken("Of")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Optional</c> keyword.
    ''' </summary>
    Public ReadOnly Property OptionalKeyword As KeywordToken
        Get
            Return New KeywordToken("Optional")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Or</c> keyword.
    ''' </summary>
    Public ReadOnly Property OrKeyword As KeywordToken
        Get
            Return New KeywordToken("Or") With{.Whitespace = WhitespaceTrivia.Space}
        End Get
    End Property


    ''' <summary>
    '''     The <c>Out</c> keyword.
    ''' </summary>
    Public ReadOnly Property OutKeyword As KeywordToken
        Get
            Return New KeywordToken("Out")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Overridable</c> keyword.
    ''' </summary>
    Public ReadOnly Property OverridableKeyword As KeywordToken
        Get
            Return New KeywordToken("Overridable")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Overrides</c> keyword.
    ''' </summary>
    Public ReadOnly Property OverridesKeyword As KeywordToken
        Get
            Return New KeywordToken("Overrides")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Private</c> keyword.
    ''' </summary>
    Public ReadOnly Property PrivateKeyword As KeywordToken
        Get
            Return New KeywordToken("Private")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Property</c> keyword.
    ''' </summary>
    Public ReadOnly Property PropertyKeyword As KeywordToken
        Get
            Return New KeywordToken("Property")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Protected</c> keyword.
    ''' </summary>
    Public ReadOnly Property ProtectedKeyword As KeywordToken
        Get
            Return New KeywordToken("Protected")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Public</c> keyword.
    ''' </summary>
    Public ReadOnly Property PublicKeyword As KeywordToken
        Get
            Return New KeywordToken("Public")
        End Get
    End Property

    ''' <summary>
    '''     The <c>ReadOnly</c> keyword.
    ''' </summary>
    Public ReadOnly Property ReadOnlyKeyword As KeywordToken
        Get
            Return New KeywordToken("ReadOnly")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Shared</c> keyword.
    ''' </summary>
    Public ReadOnly Property SharedKeyword As KeywordToken
        Get
            Return New KeywordToken("Shared")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Structure</c> keyword.
    ''' </summary>
    Public ReadOnly Property StructureKeyword As KeywordToken
        Get
            Return New KeywordToken("Structure")
        End Get
    End Property

    ''' <summary>
    '''     The <c>Sub</c> keyword.
    ''' </summary>
    Public ReadOnly Property SubKeyword As KeywordToken
        Get
            Return New KeywordToken("Sub")
        End Get
    End Property

    ''' <summary>
    '''     The <c>True</c> keyword.
    ''' </summary>
    Public ReadOnly Property TrueKeyword As KeywordToken
        Get
            Return New KeywordToken("True")
        End Get
    End Property

    ''' <summary>
    '''     The <c>WriteOnly</c> keyword.
    ''' </summary>
    Public ReadOnly Property WriteOnlyKeyword As KeywordToken
        Get
            Return New KeywordToken("WriteOnly")
        End Get
    End Property
End Module
