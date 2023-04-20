using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        if (type.IsEnum)
        {
            return GenerateEnumDeclaration(type);
        }

        if (type.IsSubclassOf(typeof(MulticastDelegate)) || type.IsSubclassOf(typeof(Delegate)))
        {
            return GenerateDelegateDeclaration(type);
        }

        var declaration = new TypeDeclaration();
        TypeUtility.WriteCustomAttributes(this, declaration, type);
        TypeUtility.WriteVisibilityKeyword(declaration, type);
        TypeUtility.WriteModifiers(declaration, type);
        WriteTypeKind(declaration, type);
        TypeUtility.WriteTypeName(declaration, type, new TypeWriteOptions {WriteAlias = false, WriteNamespace = false});

        Type[] baseTypes = type.HasBaseType() ? new[] {type.BaseType!} : Array.Empty<Type>();
        baseTypes = baseTypes.Concat(type.GetDirectInterfaces()).ToArray();
        
        if (baseTypes.Length > 0)
        {
            declaration.AddChild(Operators.Colon.With(o => o.LeadingWhitespace = o.TrailingWhitespace = " "));
        }

        for (var index = 0; index < baseTypes.Length; index++)
        {
            Type baseType = baseTypes[index];
            TypeUtility.WriteTypeName(declaration, baseType, new TypeWriteOptions());

            if (index < baseTypes.Length - 1)
            {
                declaration.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = " "));
            }
        }

        return declaration;
    }

    private static void WriteBaseType(SyntaxNode declaration, Type type)
    {
        if (type.HasBaseType())
        {
            TypeUtility.WriteTypeName(declaration, type.BaseType!, new TypeWriteOptions {WriteNamespace = true});
        }
    }

    private static void WriteInterfaces(SyntaxNode declaration, Type type)
    {
        Type[] interfaces = type.GetInterfaces();
        if (interfaces.Length == 0)
        {
            return;
        }

        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = " ");

        if (type.HasBaseType())
        {
            declaration.AddChild(comma);
        }

        var options = new TypeWriteOptions {WriteNamespace = true};
        for (var index = 0; index < interfaces.Length; index++)
        {
            Type @interface = interfaces[index];
            TypeUtility.WriteTypeName(declaration, @interface, options);

            if (index < interfaces.Length - 1)
            {
                declaration.AddChild(comma);
            }
        }
    }

    private static void WriteTypeKind(SyntaxNode declaration, Type type)
    {
        if (type.IsInterface)
        {
            declaration.AddChild(Keywords.InterfaceKeyword);
        }
        else if (type.IsValueType)
        {
            declaration.AddChild(Keywords.StructKeyword);
        }
        else
        {
            declaration.AddChild(Keywords.ClassKeyword);
        }
    }
}
