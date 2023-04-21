using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CSharp;

internal static class FieldUtility
{
    /// <summary>
    ///     Writes the modifiers for a field declaration.
    /// </summary>
    /// <param name="declaration">The declaration to write to.</param>
    /// <param name="fieldInfo">The field whose modifiers to write.</param>
    public static void WriteModifiers(SyntaxNode declaration, FieldInfo fieldInfo)
    {
        if (fieldInfo.IsLiteral)
        {
            declaration.AddChild(Keywords.ConstKeyword);
            return;
        }

        if (fieldInfo.IsStatic)
        {
            declaration.AddChild(Keywords.StaticKeyword);
        }

        if (fieldInfo.IsInitOnly)
        {
            declaration.AddChild(Keywords.ReadOnlyKeyword);
        }
    }
}
