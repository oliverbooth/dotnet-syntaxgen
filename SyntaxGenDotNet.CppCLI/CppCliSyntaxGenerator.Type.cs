using System.Reflection;
using SyntaxGenDotNet.CppCLI.Utilities;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CppCLI;

public sealed partial class CppCliSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        var declaration = new TypeDeclaration();
        ModifierUtility.WriteVisibilityModifier(declaration, type);

        if (type.IsDelegate())
        {
            WriteDelegateDeclaration(declaration, type);
            return declaration;
        }

        WriteTypeKind(declaration, type);
        declaration.AddChild(Keywords.ClassKeyword);
        TypeUtility.WriteName(declaration, type);

        if (type.IsEnum)
        {
            WriteEnumDeclaration(declaration, type);
            return declaration;
        }
        
        ModifierUtility.WriteInheritanceModifiers(declaration, type);

        Type[] baseTypes = type.HasBaseType() ? new[] {type.BaseType!} : Array.Empty<Type>();
        baseTypes = baseTypes.Concat(type.GetDirectInterfaces()).ToArray();

        if (baseTypes.Length > 0)
        {
            declaration.AddChild(Operators.Colon.With(o => o.LeadingWhitespace = o.TrailingWhitespace = " "));
        }

        for (var index = 0; index < baseTypes.Length; index++)
        {
            Type baseType = baseTypes[index];
            TypeUtility.WriteName(declaration, baseType, new TypeWriteOptions {WriteGcTrackedPointer = false});

            if (index < baseTypes.Length - 1)
            {
                declaration.AddChild(Operators.Comma.With(o => o.TrailingWhitespace = " "));
            }
        }

        return declaration;
    }

    private static void WriteDelegateDeclaration(SyntaxNode target, Type delegateType)
    {
        MethodInfo invokeMethod = delegateType.GetMethod("Invoke")!;
        TypeUtility.WriteAlias(target, invokeMethod.ReturnType);
        TypeUtility.WriteName(target, delegateType);
        target.AddChild(Operators.OpenParenthesis);
        WriteParameters(target, invokeMethod.GetParameters());
        target.AddChild(Operators.CloseParenthesis);
        target.AddChild(Operators.Semicolon);
    }

    private static void WriteEnumDeclaration(SyntaxNode target, Type enumType)
    {
        Type enumUnderlyingType = enumType.GetEnumUnderlyingType();
        if (enumUnderlyingType == typeof(int))
        {
            return;
        }

        target.AddChild(Operators.Colon);
        TypeUtility.WriteAlias(target, enumUnderlyingType);
    }

    private static void WriteTypeKind(SyntaxNode target, Type type)
    {
        if (type.IsInterface)
        {
            target.AddChild(Keywords.InterfaceKeyword);
        }
        else if (type.IsEnum)
        {
            target.AddChild(Keywords.EnumKeyword);
        }
        else if (type.IsValueType)
        {
            target.AddChild(Keywords.ValueKeyword);
        }
        else
        {
            target.AddChild(Keywords.RefKeyword);
        }
    }
}
