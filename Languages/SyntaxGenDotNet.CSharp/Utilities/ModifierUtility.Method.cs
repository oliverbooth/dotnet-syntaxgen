using System.Reflection;
using System.Runtime.CompilerServices;
using SyntaxGenDotNet.Syntax;
using X10D.Reflection;

namespace SyntaxGenDotNet.CSharp.Utilities;

public static partial class ModifierUtility
{
    /// <summary>
    ///     Writes all modifiers for the specified <see cref="MethodBase" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the modifiers will be written.</param>
    /// <param name="method">The <see cref="MethodBase" /> whose modifiers to write.</param>
    public static void WriteAllModifiers(SyntaxNode target, MethodInfo method)
    {
        WriteVisibilityModifier(target, method);
        WriteInheritanceModifiers(target, method);
        WriteValueTypeModifiers(target, method);
    }

    /// <summary>
    ///     Writes the inheritance modifiers for the specified <see cref="MethodBase" /> to the specified
    ///     <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the inheritance modifiers will be written.</param>
    /// <param name="method">The <see cref="MethodBase" /> whose modifiers to write.</param>
    public static void WriteInheritanceModifiers(SyntaxNode target, MethodInfo method)
    {
        MethodAttributes attributes = method.Attributes;
        bool isAbstract = (attributes & MethodAttributes.Abstract) != 0;
        bool isFinal = (attributes & MethodAttributes.Final) != 0;
        bool isNewSlot = (attributes & MethodAttributes.NewSlot) != 0;

        if (isAbstract)
        {
            target.AddChild(Keywords.AbstractKeyword);
        }
        else if (!isFinal && isNewSlot)
        {
            target.AddChild(Keywords.VirtualKeyword);
        }
        else if (isFinal && !isNewSlot)
        {
            target.AddChild(Keywords.SealedKeyword);
        }
        else if (method.DeclaringType != method.GetBaseDefinition().DeclaringType)
        {
            target.AddChild(Keywords.OverrideKeyword);
        }
    }

    /// <summary>
    ///     Writes the visibility modifier for the specified <see cref="MethodBase" /> to the specified <see cref="SyntaxNode" />. 
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the visibility modifier will be written.</param>
    /// <param name="method">The <see cref="MethodBase" /> whose visibility to write.</param>
    public static void WriteVisibilityModifier(SyntaxNode target, MethodBase method)
    {
        WriteVisibilityModifier(target, method.Attributes);
    }

    private static void WriteValueTypeModifiers(SyntaxNode target, MethodBase method)
    {
        if (method.HasCustomAttribute<IsReadOnlyAttribute>())
        {
            target.AddChild(Keywords.ReadOnlyKeyword);
        }
    }

    private static void WriteVisibilityModifier(SyntaxNode target, MethodAttributes attributes)
    {
        switch (attributes & MethodAttributes.MemberAccessMask)
        {
            case MethodAttributes.Public:
                target.AddChild(Keywords.PublicKeyword);
                break;

            case MethodAttributes.FamANDAssem:
                target.AddChild(Keywords.PrivateKeyword);
                target.AddChild(Keywords.ProtectedKeyword);
                break;

            case MethodAttributes.FamORAssem:
                target.AddChild(Keywords.ProtectedKeyword);
                target.AddChild(Keywords.InternalKeyword);
                break;

            case MethodAttributes.Assembly:
                target.AddChild(Keywords.InternalKeyword);
                break;

            case MethodAttributes.Family:
                target.AddChild(Keywords.ProtectedKeyword);
                break;

            case MethodAttributes.Private:
                target.AddChild(Keywords.PrivateKeyword);
                break;
        }
    }
}
