''' <summary>
'''     Provides options for writing a type.
''' </summary>
Friend Structure TypeWriteOptions
    Public Shared ReadOnly DefaultOptions As New TypeWriteOptions() With {
        .TrimAttributeSuffix = False,
        .WriteAlias = True,
        .WriteNamespace = True
        }

    ''' <summary>
    '''     Gets or sets a value indicating whether to trim the "Attribute" suffix from the type name.
    ''' </summary>
    ''' <value>
    '''     <see langword="true" /> to trim the "Attribute" suffix from the type name; otherwise,
    '''     <see langword="false" />.
    ''' </value>
    Public Property TrimAttributeSuffix As Boolean

    ''' <summary>
    '''     Gets or sets a value indicating whether to write the alias for the type.
    ''' </summary>
    ''' <value><see langword="true" /> to write the alias for the type; otherwise, <see langword="false" />.</value>
    Public Property WriteAlias As Boolean

    ''' <summary>
    '''     Gets or sets a value indicating whether to write the full namespace for the type.
    ''' </summary>
    ''' <value><see langword="true" /> to write the full namespace for the type; otherwise, <see langword="false" />.</value>
    Public Property WriteNamespace As Boolean
End Structure