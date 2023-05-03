using SyntaxGenDotNet.CIL.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CIL;

public sealed partial class CilSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        var declaration = new TypeDeclaration();
        declaration.AddChild(Keywords.ClassDeclaration);

        var options = new TypeWriteOptions {WriteAlias = false, WriteNamespace = false};
        TypeUtility.WriteTypeAttributes(declaration, type);
        WriteDeclaringType(declaration, type);
        TypeUtility.WriteName(declaration, type);
        TypeUtility.WriteGenericArguments(declaration, type);

        declaration.Children[^1].TrailingWhitespace = WhitespaceTrivia.Space;

        if (type.BaseType is not null)
        {
            declaration.AddChild(Keywords.ExtendsKeyword);
            TypeUtility.WriteTypeName(declaration, type.BaseType, options with {WriteNamespace = true});
        }

        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = " ");

        if (type.GetInterfaces() is not {Length: > 0} interfaces)
        {
            declaration.Children[^1].TrailingWhitespace = WhitespaceTrivia.None;
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

        declaration.Children[^1].TrailingWhitespace = WhitespaceTrivia.None;
        return declaration;
    }

    private static void WriteDeclaringType(SyntaxNode target, Type type)
    {
        if (!type.IsNested)
        {
            return;
        }

        Type? declaringType = type.DeclaringType;
        var options = new TypeWriteOptions {WriteAlias = false, WriteNamespace = false, WriteKindPrefix = false};

        while (declaringType is not null)
        {
            TypeUtility.WriteAlias(target, declaringType, options);
            target.AddChild(Operators.Slash);
            declaringType = declaringType.DeclaringType;
        }
    }
}
