using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CppCLI;

internal static class FieldUtility
{
    /// <summary>
    ///     Writes the modifiers for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose modifiers to write.</param>
    public static void WriteModifiers(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        if (fieldInfo.IsStatic)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        if (fieldInfo.IsInitOnly)
        {
            declaration.AddChild(Keywords.InitOnlyKeyword);
        }
    }

    /// <summary>
    ///     Writes the visibility keyword for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose visibility to write.</param>
    public static void WriteVisibilityKeyword(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        const FieldAttributes mask = FieldAttributes.FieldAccessMask;

        switch (fieldInfo.Attributes & mask)
        {
            case FieldAttributes.Public:
                declaration.AddChild(Keywords.PublicKeyword);
                break;

            case FieldAttributes.Private:
                declaration.AddChild(Keywords.PrivateKeyword);
                break;

            case FieldAttributes.FamANDAssem:
                declaration.AddChild(Keywords.PrivateKeyword);
                declaration.AddChild(Keywords.ProtectedKeyword);
                break;

            case FieldAttributes.FamORAssem:
                declaration.AddChild(Keywords.ProtectedKeyword);
                declaration.AddChild(Keywords.InternalKeyword);
                break;

            case FieldAttributes.Assembly:
                declaration.AddChild(Keywords.InternalKeyword);
                break;

            case FieldAttributes.Family:
                declaration.AddChild(Keywords.ProtectedKeyword);
                break;
        }
    }
}
