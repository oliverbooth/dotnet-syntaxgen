using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="MethodImplAttribute" />.
/// </summary>
public sealed class MethodImplAttributeExpressionWriter : AttributeExpressionWriter<MethodImplAttribute>
{
    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<MethodImplAttribute> attributes)
    {
        if (declaringMember is not MethodInfo method)
        {
            return ArraySegment<Expression>.Empty;
        }

        Expression? argument = null;
        MethodImplAttributes flags = method.MethodImplementationFlags;

        foreach (MethodImplOptions option in Enum.GetValues<MethodImplOptions>())
        {
            if ((flags & ((MethodImplAttributes)option)) == 0)
            {
                continue;
            }

            UnaryExpression optionExpression = Expression.Convert(Expression.Constant(option), typeof(int));

            if (argument is null)
            {
                argument = optionExpression;
            }
            else
            {
                argument = Expression.Or(argument, optionExpression);
            }
        }

        if (argument is null)
        {
            return ArraySegment<Expression>.Empty;
        }

        UnaryExpression constructorArgument = Expression.Convert(argument, typeof(MethodImplOptions));
        var constructor = AttributeType.GetConstructor(new[] {typeof(MethodImplOptions)})!;
        NewExpression newExpression = Expression.New(constructor, constructorArgument);
        return Expression.MemberInit(newExpression).AsEnumerableValue();
    }
}
