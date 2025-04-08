using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CppCLI.Utilities;

public static partial class ModifierUtility
{
    /// <summary>
    ///     Writes the inheritance modifiers for the specified <see cref="Type" /> to the specified
    ///     <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the inheritance modifiers will be written.</param>
    /// <param name="type">The <see cref="Type" /> whose modifiers to write.</param>
    public static void WriteInheritanceModifiers(SyntaxNode target, Type type)
    {
        if (type.IsInterface || type.IsValueType)
        {
            return;
        }

        WriteInheritanceModifiers(target, type.Attributes);
    }

    /// <summary>
    ///     Writes the visibility modifier for the specified <see cref="Type" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
    /// <param name="type">The <see cref="Type" /> whose visibility to write.</param>
    public static void WriteVisibilityModifier(SyntaxNode target, Type type)
    {
        WriteVisibilityModifier(target, type.Attributes);
    }

    private static void WriteInheritanceModifiers(SyntaxNode target, TypeAttributes attributes)
    {
        if ((attributes & TypeAttributes.Interface) != 0)
        {
            return;
        }

        if ((attributes & TypeAttributes.Abstract) != 0)
        {
            target.AddChild(Keywords.AbstractKeyword);
        }

        if ((attributes & TypeAttributes.Sealed) != 0)
        {
            target.AddChild(Keywords.SealedKeyword);
        }
    }

    private static void WriteVisibilityModifier(SyntaxNode target, TypeAttributes attributes)
    {
        switch (attributes & TypeAttributes.VisibilityMask)
        {
            case TypeAttributes.Public:
            case TypeAttributes.NestedPublic:
                target.AddChild(Keywords.PublicKeyword);
                break;

            case TypeAttributes.NestedFamANDAssem:
                target.AddChild(Keywords.PrivateKeyword);
                target.AddChild(Keywords.ProtectedKeyword);
                break;

            case TypeAttributes.NestedFamORAssem:
                target.AddChild(Keywords.ProtectedKeyword);
                target.AddChild(Keywords.InternalKeyword);
                break;

            case TypeAttributes.NestedFamily:
                target.AddChild(Keywords.ProtectedKeyword);
                break;

            case TypeAttributes.NestedPrivate:
                target.AddChild(Keywords.PrivateKeyword);
                break;

            default:
                target.AddChild(Keywords.InternalKeyword);
                break;
        }
    }
}
