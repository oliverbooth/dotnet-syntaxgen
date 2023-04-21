using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CSharp.Utilities;

internal sealed partial class ModifierUtility
{
    /// <summary>
    ///     Writes the visibility modifier for the specified <see cref="FieldInfo" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
    /// <param name="field">The <see cref="FieldInfo" /> whose visibility to write.</param>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="target" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="field" /> is <see langword="null" />.</para>
    /// </exception>
    public static void WriteVisibilityModifier(SyntaxNode target, FieldInfo field)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (field is null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        WriteVisibilityModifier(target, field.Attributes);
    }

    /// <summary>
    ///     Writes the visibility modifier for the specified <see cref="FieldAttributes" /> to the specified
    ///     <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
    /// <param name="attributes">The <see cref="MethodAttributes" /> to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="target" /> is <see langword="null" />.</exception>
    public static void WriteVisibilityModifier(SyntaxNode target, FieldAttributes attributes)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        attributes &= FieldAttributes.FieldAccessMask;

        switch (attributes)
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
