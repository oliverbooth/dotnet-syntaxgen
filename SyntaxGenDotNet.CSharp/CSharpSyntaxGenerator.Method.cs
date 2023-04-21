using System.Reflection;
using SyntaxGenDotNet.CSharp.Utilities;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

public partial class CSharpSyntaxGenerator
{
    private static void WriteParameter(SyntaxNode target, ParameterInfo parameter)
    {
        ModifierUtility.WritePassByModifier(target, parameter);
        TypeUtility.WriteAlias(target, parameter.ParameterType);

        if (parameter.Name is null)
        {
            return;
        }

        target.AddChild(new IdentifierToken(parameter.Name));

        if (!parameter.IsOptional)
        {
            return;
        }

        target.AddChild(Operators.Assignment);
        target.AddChild(TokenUtility.CreateLiteralToken(parameter.DefaultValue));
    }

    private static void WriteParameters(SyntaxNode target, IReadOnlyList<ParameterInfo> parameters)
    {
        SyntaxNode comma = parameters.Count switch
        {
            // With method returns a newly-allocated clone. but there's no use allocating
            // if only one parameter is present, as no comma will be necessary.
            > 1 => Operators.Comma.With(o => o.TrailingWhitespace = WhitespaceTrivia.Space),
            _ => null!
        };

        for (var index = 0; index < parameters.Count; index++)
        {
            ParameterInfo parameter = parameters[index];
            WriteParameter(target, parameter);

            if (index < parameters.Count - 1)
            {
                target.AddChild(comma);
            }
        }
    }
}
