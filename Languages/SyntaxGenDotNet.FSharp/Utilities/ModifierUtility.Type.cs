using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.FSharp.Utilities;

internal static partial class ModifierUtility
{
    /// <summary>
    ///     Writes all modifiers for the specified <see cref="Type" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
    /// <param name="type">The <see cref="Type" /> whose modifiers to write.</param>
    public static void WriteAllModifiers(SyntaxNode target, Type type)
    {
        target.AddChild(Keywords.TypeKeyword);
        WriteVisibilityModifier(target, type);
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

    private static void WriteVisibilityModifier(SyntaxNode target, TypeAttributes attributes)
    {
        switch (attributes & TypeAttributes.VisibilityMask)
        {
            case TypeAttributes.Public:
            case TypeAttributes.NestedPublic:
                // do nothing. public is the default
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
