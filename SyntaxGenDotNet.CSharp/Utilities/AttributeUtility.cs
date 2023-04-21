﻿using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using SyntaxGenDotNet.Attributes;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp.Utilities;

internal static class AttributeUtility
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
        if (arguments.Count > 0)
        {
            target.AddChild(Operators.OpenParenthesis);

            for (var index = 0; index < arguments.Count; index++)
            {
                Expression argument = arguments[index];

                if (argument.Type.IsEnum)
                {
                    TypeUtility.WriteName(target, argument.Type);
                    target.AddChild(Operators.Dot);
                }

                target.AddChild(TokenUtility.CreateLiteralToken(argument));
            }

            if (attributeExpression.Bindings.Count > 0)
            {
                WriteBindings(target, attributeExpression, options);
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

    private static void WriteBindings(SyntaxNode target, MemberInitExpression memberInitExpression, TypeWriteOptions options)
    {
        SyntaxNode comma = Operators.Comma.With(o => o.TrailingWhitespace = " ");
        ReadOnlyCollection<MemberBinding> bindings = memberInitExpression.Bindings;
        options.WriteNamespace = false;

        for (var index = 0; index < bindings.Count; index++)
        {
            MemberBinding binding = bindings[index];
            if (binding is not MemberAssignment assignment)
            {
                continue;
            }

            target.AddChild(comma);
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
}