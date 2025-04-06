using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CppCLI.Utilities;

internal static partial class ModifierUtility
{
    /// <summary>
    ///     Writes all modifiers for the specified <see cref="FieldInfo" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
    /// <param name="field">The <see cref="FieldInfo" /> whose modifiers to write.</param>
    public static void WriteAllModifiers(SyntaxNode target, FieldInfo field)
    {
        WriteVisibilityModifier(target, field);
    }

    /// <summary>
    ///     Writes the initialization modifiers for the specified <see cref="FieldInfo" /> to the specified
    ///     <see cref="SyntaxNode" />.
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the initialization modifiers will be written.</param>
    /// <param name="field">The <see cref="FieldInfo" /> whose modifiers to write.</param>
    public static void WriteInitializationModifiers(SyntaxNode target, FieldInfo field)
    {
        if (field.IsStatic)
        {
            target.AddChild(Keywords.StaticKeyword);
        }

        if (field.IsInitOnly)
        {
            target.AddChild(Keywords.InitOnlyKeyword);
        }
    }

    /// <summary>
    ///     Writes the visibility modifier for the specified <see cref="FieldInfo" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
    /// <param name="field">The <see cref="FieldInfo" /> whose visibility to write.</param>
    public static void WriteVisibilityModifier(SyntaxNode target, FieldInfo field)
    {
        WriteVisibilityModifier(target, field.Attributes);
    }

    private static void WriteVisibilityModifier(SyntaxNode target, FieldAttributes attributes)
    {
        switch (attributes & FieldAttributes.FieldAccessMask)
        {
            case FieldAttributes.Public:
                target.AddChild(Keywords.PublicKeyword);
                break;

            case FieldAttributes.FamANDAssem:
                target.AddChild(Keywords.PrivateKeyword);
                target.AddChild(Keywords.ProtectedKeyword);
                break;

            case FieldAttributes.FamORAssem:
                target.AddChild(Keywords.ProtectedKeyword);
                target.AddChild(Keywords.InternalKeyword);
                break;

            case FieldAttributes.Assembly:
                target.AddChild(Keywords.InternalKeyword);
                break;

            case FieldAttributes.Family:
                target.AddChild(Keywords.ProtectedKeyword);
                break;

            case FieldAttributes.Private:
                target.AddChild(Keywords.PrivateKeyword);
                break;
        }
    }
}
