using System.Reflection;
using SyntaxGenDotNet.Extensions;
using SyntaxGenDotNet.FSharp.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public override TypeDeclaration GenerateTypeDeclaration(Type type)
    {
        var declaration = new TypeDeclaration();
        AttributeUtility.WriteCustomAttributes(this, declaration, type);
        ModifierUtility.WriteAllModifiers(declaration, type);
        TypeUtility.WriteName(declaration, type, new TypeWriteOptions {WriteAlias = false, WriteNamespace = false});
        if (!type.IsEnum && !type.IsDelegate())
        {
            TypeUtility.WriteGenericArguments(declaration, type);
        }

        declaration.AddChild(Operators.Assignment);

        if (type.IsEnum)
        {
            return declaration;
        }

        WriteKind(declaration, type);

        if (type.IsDelegate())
        {
            WriteDelegateDeclaration(declaration, type);
            return declaration;
        }

        WriteBaseType(declaration, type);
        WriteInterfaces(declaration, type);
        return declaration;
    }

    private static void WriteBaseType(SyntaxNode target, Type type)
    {
        if (!type.HasBaseType())
        {
            return;
        }

        target.AddChild(Keywords.InheritKeyword);
        TypeUtility.WriteTypeName(target, type.BaseType!);
    }

    private static void WriteDelegateDeclaration(SyntaxNode target, Type type)
    {
        MethodInfo invokeMethod = type.GetMethod("Invoke")!;
        ParameterInfo[] parameters = invokeMethod.GetParameters();
        for (var index = 0; index < parameters.Length; index++)
        {
            ParameterInfo parameter = parameters[index];
            TypeUtility.WriteTypeName(target, parameter.ParameterType);

            if (index < parameters.Length - 1)
            {
                target.AddChild(Operators.Asterisk);
            }
        }

        target.AddChild(Operators.Arrow);
        TypeUtility.WriteTypeName(target, invokeMethod.ReturnType);
    }

    private static void WriteInterfaces(TypeDeclaration target, Type type)
    {
        Type[] interfaces = type.GetDirectInterfaces();
        if (interfaces.Length == 0)
        {
            return;
        }

        SyntaxNode interfaceKeyword = Keywords.InterfaceKeyword.With(k => k.LeadingWhitespace = WhitespaceTrivia.Indent);
        foreach (Type interfaceType in interfaces)
        {
            target.AddChild(interfaceKeyword);
            TypeUtility.WriteTypeName(target, interfaceType);
        }
    }

    private static void WriteKind(SyntaxNode target, Type type)
    {
        if (type.IsInterface)
        {
            target.AddChild(Keywords.InterfaceKeyword);
        }
        else if (type.IsValueType)
        {
            target.AddChild(Keywords.StructKeyword);
        }
        else if (type.IsDelegate())
        {
            target.AddChild(Keywords.DelegateKeyword);
            target.AddChild(Keywords.OfKeyword);
        }
        else
        {
            target.AddChild(Keywords.ClassKeyword);
        }
    }
}
