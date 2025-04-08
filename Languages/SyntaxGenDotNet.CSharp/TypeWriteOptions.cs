namespace SyntaxGenDotNet.CSharp;

/// <summary>
///     Provides options for writing a type.
/// </summary>
public struct TypeWriteOptions
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeWriteOptions" /> struct.
    /// </summary>
    public TypeWriteOptions()
    {
    }

    /// <summary>
    ///     Gets or sets a value indicating whether to append the namespace delimiter to the type name.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if the namespace delimiter should be appended to the type name; otherwise,
    ///     <see langword="false" />.
    /// </value>
    public bool AppendNamespaceDelimiter { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to trim the "Attribute" suffix from the type name.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if the "Attribute" suffix should be trimmed from the type name; otherwise,
    ///     <see langword="false" />.
    /// </value>
    public bool TrimAttributeSuffix { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether to write the alias for the type.
    /// </summary>
    /// <value><see langword="true" /> if the alias for the type should be written; otherwise, <see langword="false" />.</value>
    public bool WriteAlias { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to write the generic arguments for the type.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if the generic arguments for the type should be written; otherwise, <see langword="false" />.
    /// </value>
    public bool WriteGenericArguments { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to write the full namespace for the type.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if the full namespace for the type should be written; otherwise, <see langword="false" />.
    /// </value>
    public bool WriteNamespace { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to write the <c>ref</c> modifier for the type.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if the <c>ref</c> modifier for the type should be written; otherwise,
    ///     <see langword="false" />.
    /// </value>
    public bool WriteRef { get; set; } = true;
}
