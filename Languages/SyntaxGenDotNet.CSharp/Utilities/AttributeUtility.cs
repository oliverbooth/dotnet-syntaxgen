using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using SyntaxGenDotNet.Attributes;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp.Utilities;

/// <summary>
///     Provides utility methods for working with attributes in the C# language.
/// </summary>
public static class AttributeUtility
{
    /// <summary>
    ///     Writes a custom attribute to the specified node.
    /// </summary>
    /// <param name="target">The node to which to write the attribute.</param>
    /// <param name="attributeExpression">The attribute expression to write.</param>
    public static void WriteCustomAttribute(SyntaxNode target, MemberInitExpression attributeExpression)
    {
        var options = new TypeWriteOptions {TrimAttributeSuffix = true, WriteNamespace = true, WriteAlias = false};

        // explicit creation of the open bracket token to avoid the trailing whitespace
        // of the previous token being trimmed, as Operators.OpenBracket defaults to trimming.
        var openBracket = new OperatorToken(Operators.OpenBracket.Text, false);

        target.AddChild(openBracket);
        TypeUtility.WriteAlias(target, attributeExpression.Type, options);

        ReadOnlyCollection<Expression> arguments = attributeExpression.NewExpression.Arguments;
        bool hasArguments = arguments.Count > 0;
        bool hasBindings = attributeExpression.Bindings.Count > 0;

        if (hasArguments || hasBindings)
        {
            target.AddChild(Operators.OpenParenthesis);

            if (hasArguments)
            {
                WriteArguments(target, arguments);
            }

            if (hasBindings)
            {
                WriteBindings(hasArguments, target, attributeExpression, options);
            }

            target.AddChild(Operators.CloseParenthesis);
        }

        target.AddChild(Operators.CloseBracket.With(o => o.TrailingWhitespace = WhitespaceTrivia.NewLine));
    }

    /// <summary>
    ///     Writes all known supported custom attribute to the specified node.
    /// </summary>
    /// <param name="generator">The syntax generator.</param>
    /// <param name="target">The node to which to write the attributes.</param>
    /// <param name="member">The member whose custom attributes to write.</param>
    public static void WriteCustomAttributes(SyntaxGenerator generator, SyntaxNode target, MemberInfo member)
    {
        foreach (AttributeExpressionWriter writer in generator.AttributeExpressionWriters)
        {
            Type attributeType = writer.AttributeType;
            Attribute[] attributes = member.GetCustomAttributes(attributeType, false).OfType<Attribute>().ToArray();
            IEnumerable<Expression> expressions = writer.CreateAttributeExpressions(member, attributes);

            foreach (Expression expression in expressions)
            {
                if (expression is MemberInitExpression memberInitExpression)
                {
                    WriteCustomAttribute(target, memberInitExpression);
                }
            }
        }
    }

    private static void WriteArguments(SyntaxNode target, IReadOnlyList<Expression> arguments)
    {
        for (var index = 0; index < arguments.Count; index++)
        {
            Expression argument = arguments[index];
            WriteResolvedExpression(target, argument);
        }
    }

    private static void WriteBindings(bool writeComma,
        SyntaxNode target,
        MemberInitExpression expression,
        TypeWriteOptions options)
    {
        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = " ");
        ReadOnlyCollection<MemberBinding> bindings = expression.Bindings;
        options.WriteNamespace = false;

        for (var index = 0; index < bindings.Count; index++)
        {
            MemberBinding binding = bindings[index];
            if (binding is not MemberAssignment assignment)
            {
                continue;
            }

            if (index > 0 || writeComma)
            {
                target.AddChild(comma);
            }

            target.AddChild(new IdentifierToken(binding.Member.Name));
            target.AddChild(Operators.Assignment);
            if (assignment.Expression.Type.IsEnum)
            {
                TypeUtility.WriteAlias(target, assignment.Expression.Type, options);
                target.AddChild(Operators.Dot);
            }

            target.AddChild(TokenUtility.CreateLiteralToken(assignment.Expression));
        }
    }

    private static void WriteResolvedExpression(SyntaxNode target, Expression expression)
    {
        if (expression is UnaryExpression {NodeType: ExpressionType.Convert} unary)
        {
            WriteResolvedExpression(target, unary.Operand);
            return;
        }

        if (expression is BinaryExpression binary)
        {
            WriteResolvedExpression(target, binary.Left);
            target.AddChild(Operators.Or);
            WriteResolvedExpression(target, binary.Right);
            return;
        }

        if (expression.Type.IsEnum)
        {
            TypeUtility.WriteName(target, expression.Type);
            target.AddChild(Operators.Dot);
        }

        target.AddChild(TokenUtility.CreateLiteralToken(expression));
    }
}
