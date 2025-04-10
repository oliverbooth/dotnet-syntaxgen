﻿using System.Reflection;
using System.Runtime.CompilerServices;
using SyntaxGenDotNet.Syntax;
using X10D.Reflection;

namespace SyntaxGenDotNet.CSharp.Utilities;

public static partial class ModifierUtility
{
    /// <summary>
    ///     Writes all modifiers for the specified <see cref="Type" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
    /// <param name="type">The <see cref="Type" /> whose modifiers to write.</param>
    public static void WriteAllModifiers(SyntaxNode target, Type type)
    {
        WriteVisibilityModifier(target, type);
        WriteInheritanceModifiers(target, type);
        WriteValueTypeModifiers(target, type);
    }

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
    ///     Writes the inheritance modifiers for the specified type to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the modifiers.</param>
    /// <param name="type">The type for which to write the modifiers.</param>
    public static void WriteValueTypeModifiers(SyntaxNode target, Type type)
    {
        if (!type.IsValueType)
        {
            return;
        }

        if (type.HasCustomAttribute<IsReadOnlyAttribute>())
        {
            target.AddChild(Keywords.ReadOnlyKeyword);
        }

        if (type.IsByRefLike)
        {
            target.AddChild(Keywords.RefKeyword);
        }
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

        if ((attributes & TypeAttributes.Abstract) != 0 && (attributes & TypeAttributes.Sealed) != 0)
        {
            target.AddChild(Keywords.StaticKeyword);
        }
        else if ((attributes & TypeAttributes.Abstract) != 0)
        {
            target.AddChild(Keywords.AbstractKeyword);
        }
        else if ((attributes & TypeAttributes.Sealed) != 0)
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
