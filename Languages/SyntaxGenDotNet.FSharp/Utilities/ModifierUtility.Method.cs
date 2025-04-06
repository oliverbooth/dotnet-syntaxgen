using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.FSharp.Utilities;

internal static partial class ModifierUtility
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
        target.AddChild(Keywords.MemberKeyword);
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
            // TODO write virtual differently
            // target.AddChild(Keywords.VirtualKeyword);
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

    private static void WriteVisibilityModifier(SyntaxNode target, MethodAttributes attributes)
    {
        switch (attributes & MethodAttributes.MemberAccessMask)
        {
            case MethodAttributes.Public:
                // do nothing. public is the default.
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

        if ((attributes & MethodAttributes.Static) != 0)
        {
            target.AddChild(Keywords.StaticKeyword);
        }
    }
}
