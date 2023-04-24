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
        else if (type.IsDelegate())
        {
            WriteDelegateDeclaration(declaration, type);
        }
        else
        {
            WriteClassDeclaration(declaration, type);
        }

        return declaration;
    }

    private static void WriteClassDeclaration(SyntaxNode target, Type type)
    {
        ModifierUtility.WriteAllModifiers(target, type);
        WriteTypeKind(target, type);
        TypeUtility.WriteName(target, type);
        TypeUtility.WriteGenericArguments(target, type);

        Type[] baseTypes = type.HasBaseType() ? new[] {type.BaseType!} : Array.Empty<Type>();
        baseTypes = baseTypes.Concat(type.GetDirectInterfaces()).ToArray();

        if (baseTypes.Length > 0)
        {
            target.AddChild(Operators.Colon);
        }

        for (var index = 0; index < baseTypes.Length; index++)
        {
            Type baseType = baseTypes[index];
            TypeUtility.WriteAlias(target, baseType);

            if (index < baseTypes.Length - 1)
            {
                target.AddChild(Operators.Comma);
            }
        }

        TypeUtility.WriteParameterConstraints(target, type.GetGenericArguments());
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
        WriteParameters(target, invokeMethod);
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

    private static void WriteTypeKind(SyntaxNode target, Type type)
    {
        if (type.IsInterface)
        {
            target.AddChild(Keywords.InterfaceKeyword);
        }
        else if (type.IsValueType)
        {
            target.AddChild(Keywords.StructKeyword);
        }
        else
        {
            target.AddChild(Keywords.ClassKeyword);
        }
    }
}
