using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.FSharp.Utilities;

internal static partial class ModifierUtility
{
    /// <summary>
    ///     Writes all modifiers for the specified <see cref="FieldInfo" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
    /// <param name="field">The <see cref="FieldInfo" /> whose modifiers to write.</param>
    public static void WriteAllModifiers(SyntaxNode target, FieldInfo field)
    {
        target.AddChild(field.IsStatic ? Keywords.StaticValKeyword : Keywords.ValKeyword);

        if (field is {IsInitOnly: false, IsLiteral: false})
        {
            target.AddChild(Keywords.MutableKeyword);
        }

        WriteVisibilityModifier(target, field);
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
                // do nothing. public is the default
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
