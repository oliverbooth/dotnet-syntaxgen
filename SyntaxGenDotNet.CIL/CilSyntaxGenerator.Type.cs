using System.Diagnostics;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateDelegateDeclaration(Type delegateType)
    {
        Trace.Assert(delegateType.IsSubclassOf(typeof(MulticastDelegate)) || delegateType.IsSubclassOf(typeof(Delegate)),
            "The specified type is not a delegate.");

        return GenerateTypeDeclaration(delegateType);
    }

    /// <inheritdoc />
    public override TypeDeclaration GenerateEnumDeclaration(Type enumType)
    {
        Trace.Assert(enumType.IsEnum, "The specified type is not an enum.");

        return GenerateTypeDeclaration(enumType);
    }

    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        var declaration = new TypeDeclaration();
        declaration.AddChild(Keywords.DotClassKeyword);

        var options = new TypeWriteOptions {WriteAlias = false, WriteNamespace = false, WriteKindPrefix = false};
        TypeUtility.WriteTypeAttributes(declaration, type);
        TypeUtility.WriteTypeName(declaration, type, options);

        if (type.BaseType is not null)
        {
            declaration.AddChild(Keywords.ExtendsKeyword);
            TypeUtility.WriteTypeName(declaration, type.BaseType, options with {WriteNamespace = true});
        }

        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = " ");

        if (type.GetInterfaces() is not {Length: > 0} interfaces)
        {
            return declaration;
        }

        declaration.AddChild(Keywords.ImplementsKeyword);
        for (var index = 0; index < interfaces.Length; index++)
        {
            Type interfaceType = interfaces[index];
            TypeUtility.WriteTypeName(declaration, interfaceType, options with {WriteNamespace = true});

            if (index < interfaces.Length - 1)
            {
                declaration.AddChild(comma);
            }
        }

        return declaration;
    }
}
