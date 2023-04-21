using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        var declaration = new TypeDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, type);

        if (type.IsEnum)
        {
            WriteEnumDeclaration(declaration, type);
        }
        else if (type.IsSubclassOf(typeof(MulticastDelegate)) || type.IsSubclassOf(typeof(Delegate)))
        {
            WriteDelegateDeclaration(declaration, type);
        }
        else
        {
            WriteClassDeclaration(declaration, type);
        }

        return declaration;
    }

    private static void WriteClassDeclaration(TypeDeclaration declaration, Type type)
    {
        ModifierUtility.WriteAllModifiers(declaration, type);
        WriteTypeKind(declaration, type);
        TypeUtility.WriteName(declaration, type);

        Type[] baseTypes = type.HasBaseType() ? new[] {type.BaseType!} : Array.Empty<Type>();
        baseTypes = baseTypes.Concat(type.GetDirectInterfaces()).ToArray();

        if (baseTypes.Length > 0)
        {
            declaration.AddChild(Operators.Colon);
        }

        for (var index = 0; index < baseTypes.Length; index++)
        {
            Type baseType = baseTypes[index];
            TypeUtility.WriteAlias(declaration, baseType);

            if (index < baseTypes.Length - 1)
            {
                declaration.AddChild(Operators.Comma);
            }
        }
    }

    private static void WriteDelegateDeclaration(SyntaxNode target, Type delegateType)
    {
        MethodInfo invokeMethod = delegateType.GetMethod("Invoke")!;
        ModifierUtility.WriteVisibilityModifier(target, delegateType);
        target.AddChild(Keywords.DelegateKeyword);
        TypeUtility.WriteAlias(target, invokeMethod.ReturnType);
        TypeUtility.WriteName(target, delegateType);

        if (delegateType.IsGenericType)
        {
            TypeUtility.WriteGenericArguments(target, delegateType);
        }

        target.AddChild(Operators.OpenParenthesis);
        WriteParameters(target, invokeMethod.GetParameters());
        target.AddChild(Operators.CloseParenthesis);
        target.AddChild(Operators.Semicolon);
    }

    private static void WriteEnumDeclaration(SyntaxNode target, Type enumType)
    {
        ModifierUtility.WriteVisibilityModifier(target, enumType);
        target.AddChild(Keywords.EnumKeyword);

        TypeUtility.WriteName(target, enumType);

        Type enumUnderlyingType = enumType.GetEnumUnderlyingType();
        if (enumUnderlyingType == typeof(int))
        {
            return;
        }

        target.AddChild(Operators.Colon);
        TypeUtility.WriteAlias(target, enumUnderlyingType);
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
