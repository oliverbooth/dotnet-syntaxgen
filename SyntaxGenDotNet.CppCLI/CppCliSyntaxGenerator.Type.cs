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
        TypeUtility.WriteGenericArguments(declaration, type);
        ModifierUtility.WriteVisibilityModifier(declaration, type);

        if (type.IsDelegate())
        {
            WriteDelegateDeclaration(declaration, type);
            return declaration;
        }

        WriteTypeKind(declaration, type);
        declaration.AddChild(Keywords.ClassKeyword);
        WriteDeclaringType(declaration, type);
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

        var options = new TypeWriteOptions {WriteGenericTypeName = false, WriteGcTrackedPointer = false};
        for (var index = 0; index < baseTypes.Length; index++)
        {
            Type baseType = baseTypes[index];
            TypeUtility.WriteAlias(declaration, baseType, options);

            if (index < baseTypes.Length - 1)
            {
                declaration.AddChild(Operators.Comma);
            }
        }

        return declaration;
    }

    private static void WriteDeclaringType(SyntaxNode target, Type type)
    {
        if (!type.IsNested)
        {
            return;
        }

        Type? declaringType = type.DeclaringType;
        var options = new TypeWriteOptions
        {
            WriteAlias = false, WriteNamespace = false, TrimAttributeSuffix = false, WriteGenericTypeName = false
        };

        while (declaringType is not null)
        {
            TypeUtility.WriteAlias(target, declaringType, options);
            target.AddChild(Operators.ColonColon);
            declaringType = declaringType.DeclaringType;
        }
    }

    private static void WriteDelegateDeclaration(SyntaxNode target, Type delegateType)
    {
        MethodInfo invokeMethod = delegateType.GetMethod("Invoke")!;
        TypeUtility.WriteAlias(target, invokeMethod.ReturnType);
        WriteDeclaringType(target, delegateType);
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
