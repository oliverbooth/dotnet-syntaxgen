using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet;

/// <summary>
///     Represents a syntax generator.
/// </summary>
public abstract class SyntaxGenerator
{
    /// <summary>
    ///     Gets the name of the language for which this syntax generator generates syntax.
    /// </summary>
    /// <value>The name of the language.</value>
    public string LanguageName { get; protected set; } = string.Empty;

    /// <summary>
    ///     Generates the syntax for the specified enum.
    /// </summary>
    /// <param name="enumType">The enum for which to generate syntax.</param>
    /// <returns>The syntax for the specified enum.</returns>
    public abstract TypeDeclaration GenerateEnumDeclaration(Type enumType);

    /// <summary>
    ///     Generates the syntax for the specified delegate.
    /// </summary>
    /// <param name="delegateType">The delegate for which to generate syntax.</param>
    /// <returns>The syntax for the specified delegate.</returns>
    public abstract TypeDeclaration GenerateDelegateDeclaration(Type delegateType);

    /// <summary>
    ///     Generates the syntax for the specified field.
    /// </summary>
    /// <param name="fieldInfo">The field for which to generate syntax.</param>
    /// <returns>The syntax for the specified field.</returns>
    public abstract FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo);

    /// <summary>
    ///     Generates the syntax for the specified type.
    /// </summary>
    /// <param name="type">The type for which to generate syntax.</param>
    /// <returns>The syntax for the specified type.</returns>
    public abstract TypeDeclaration GenerateTypeDeclaration(Type type);
}
