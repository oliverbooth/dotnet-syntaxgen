using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CSharp.Utilities;

internal sealed partial class ModifierUtility
{
    /// <summary>
    ///     Writes the pass-by modifiers for the specified <see cref="ParameterInfo" /> to the specified
    ///     <see cref="SyntaxNode" />.
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the pass-by modifiers will be written.</param>
    /// <param name="parameter">The <see cref="ParameterInfo" /> whose modifiers to write.</param>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="target" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="parameter" /> is <see langword="null" />.</para>
    /// </exception>
    public static void WritePassByModifier(SyntaxNode target, ParameterInfo parameter)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (parameter is null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        if (parameter.IsOut)
        {
            target.AddChild(Keywords.OutKeyword);
        }
        else if (parameter.IsIn)
        {
            target.AddChild(Keywords.OutKeyword);
        }
    }
}
