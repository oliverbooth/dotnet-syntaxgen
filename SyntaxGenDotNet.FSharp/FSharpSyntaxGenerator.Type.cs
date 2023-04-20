using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
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

        declaration.AddChild(Keywords.TypeKeyword);
        TypeUtility.WriteVisibilityKeyword(declaration, type);
        TypeUtility.WriteTypeName(declaration, type, new TypeWriteOptions {WriteNamespace = false, WriteAlias = false});
        declaration.AddChild(Operators.Assignment.With(o => o.LeadingWhitespace = WhitespaceTrivia.None));
        declaration.AddChild(type.IsValueType ? Keywords.StructKeyword : Keywords.ClassKeyword);

        if (type.HasBaseType())
        {
            declaration.AddChild(Keywords.InheritKeyword);
            TypeUtility.WriteTypeName(declaration, type.BaseType!);
        }

        Type[] interfaces = type.GetDirectInterfaces();

        if (interfaces.Length > 0)
        {
            SyntaxNode interfaceKeyword = Keywords.InterfaceKeyword.With(k => k.LeadingWhitespace = WhitespaceTrivia.Indent);
            foreach (Type interfaceType in interfaces)
            {
                declaration.AddChild(interfaceKeyword);
                TypeUtility.WriteTypeName(declaration, interfaceType);
            }
        }

        return declaration;
    }
}
