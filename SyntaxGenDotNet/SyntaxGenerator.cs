using System.Diagnostics;
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
        AttributeExpressionWriters.Add(new PureAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new SerializableAttributeExpressionWriter());
        AttributeExpressionWriters.Add(new StructLayoutAttributeExpressionWriter());
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
            Type type when type.IsSubclassOf(typeof(MulticastDelegate)) || type.IsSubclassOf(typeof(Delegate)) =>
                GenerateDelegateDeclaration(type),
            Type {IsEnum: true} type => GenerateEnumDeclaration(type),
            Type type => GenerateTypeDeclaration(type),
            FieldInfo fieldInfo => GenerateFieldDeclaration(fieldInfo),
            MethodInfo methodInfo => GenerateMethodDeclaration(methodInfo),
            // PropertyInfo propertyInfo => GeneratePropertyDeclaration(propertyInfo),
            // ConstructorInfo constructorInfo => GenerateConstructorDeclaration(constructorInfo),
            // EventInfo eventInfo => GenerateEventDeclaration(eventInfo),
            _ => throw new NotSupportedException()
        };
    }

    /// <summary>
    ///     Generates the syntax for the specified enum.
    /// </summary>
    /// <param name="enumType">The enum for which to generate syntax.</param>
    /// <returns>The syntax for the specified enum.</returns>
    public virtual TypeDeclaration GenerateEnumDeclaration(Type enumType)
    {
        Trace.Assert(enumType.IsEnum);
        return GenerateTypeDeclaration(enumType);
    }

    /// <summary>
    ///     Generates the syntax for the specified delegate.
    /// </summary>
    /// <param name="delegateType">The delegate for which to generate syntax.</param>
    /// <returns>The syntax for the specified delegate.</returns>
    public virtual TypeDeclaration GenerateDelegateDeclaration(Type delegateType)
    {
        Trace.Assert(delegateType.IsSubclassOf(typeof(MulticastDelegate)) || delegateType.IsSubclassOf(typeof(Delegate)));
        return GenerateTypeDeclaration(delegateType);
    }

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
    ///     Generates the syntax for the specified type.
    /// </summary>
    /// <param name="type">The type for which to generate syntax.</param>
    /// <returns>The syntax for the specified type.</returns>
    public abstract TypeDeclaration GenerateTypeDeclaration(Type type);
}
