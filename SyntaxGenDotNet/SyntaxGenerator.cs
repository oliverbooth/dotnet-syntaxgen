using System.Reflection;
using SyntaxGenDotNet.Attributes;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet;

/// <summary>
///     Represents a syntax generator.
/// </summary>
public abstract class SyntaxGenerator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SyntaxGenerator" /> class.
    /// </summary>
    protected SyntaxGenerator()
    {
        AttributeExpressionWriters.Add(new AttributeUsageAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new CLSCompliantAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new ExtensionAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new ObsoleteAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new PureAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new SerializableAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new StructLayoutAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new OSPlatformAttributeExpressionWriter());
    }

    /// <summary>
    ///     Gets a list of the attribute expression writers supported by this syntax generator.
    /// </summary>
    /// <value>A list of the attribute expression writers supported by this syntax generator.</value>
    public List<AttributeExpressionWriter> AttributeExpressionWriters { get; } = new();

    /// <summary>
    ///     Gets the name of the language for which this syntax generator generates syntax.
    /// </summary>
    /// <value>The name of the language.</value>
    public string LanguageName { get; protected set; } = string.Empty;

    /// <summary>
    ///     Generates the syntax for the specified member.
    /// </summary>
    /// <param name="member">The member for which to generate syntax.</param>
    /// <returns>The syntax for the specified member.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="member" /> is <see langword="null" />.</exception>
    public DeclarationSyntax GenerateDeclaration(MemberInfo member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        return member switch
        {
            Type type => GenerateTypeDeclaration(type),
            FieldInfo fieldInfo => GenerateFieldDeclaration(fieldInfo),
            MethodInfo methodInfo => GenerateMethodDeclaration(methodInfo),
            PropertyInfo propertyInfo => GeneratePropertyDeclaration(propertyInfo),
            ConstructorInfo constructorInfo => GenerateConstructorDeclaration(constructorInfo),
            // EventInfo eventInfo => GenerateEventDeclaration(eventInfo),
            _ => throw new NotSupportedException()
        };
    }

    /// <summary>
    ///     Generates the syntax for the specified constructor.
    /// </summary>
    /// <param name="constructorInfo">The constructor for which to generate syntax.</param>
    /// <returns>The syntax for the specified constructor.</returns>
    public abstract ConstructorDeclaration GenerateConstructorDeclaration(ConstructorInfo constructorInfo);

    /// <summary>
    ///     Generates the syntax for the specified field.
    /// </summary>
    /// <param name="fieldInfo">The field for which to generate syntax.</param>
    /// <returns>The syntax for the specified field.</returns>
    public abstract FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo);

    /// <summary>
    ///     Generates the syntax for the specified method.
    /// </summary>
    /// <param name="methodInfo">The method for which to generate syntax.</param>
    /// <returns>The syntax for the specified method.</returns>
    public abstract MethodDeclaration GenerateMethodDeclaration(MethodInfo methodInfo);

    /// <summary>
    ///     Generates the syntax for the specified property.
    /// </summary>
    /// <param name="propertyInfo">The property for which to generate syntax.</param>
    /// <returns>The syntax for the specified property.</returns>
    public abstract PropertyDeclaration GeneratePropertyDeclaration(PropertyInfo propertyInfo);

    /// <summary>
    ///     Generates the syntax for the specified type.
    /// </summary>
    /// <param name="type">The type for which to generate syntax.</param>
    /// <returns>The syntax for the specified type.</returns>
    public abstract TypeDeclaration GenerateTypeDeclaration(Type type);
}
