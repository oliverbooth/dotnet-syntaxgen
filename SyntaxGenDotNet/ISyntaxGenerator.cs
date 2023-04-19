using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet;

/// <summary>
///     Represents a syntax generator.
/// </summary>
public interface ISyntaxGenerator
{
    /// <summary>
    ///     Gets the name of the language for which this syntax generator generates syntax.
    /// </summary>
    /// <value>The name of the language.</value>
    string LanguageName { get; }

    /// <summary>
    ///     Generates the syntax for the specified field.
    /// </summary>
    /// <param name="fieldInfo">The field for which to generate syntax.</param>
    /// <returns>The syntax for the specified field.</returns>
    FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo);
}
