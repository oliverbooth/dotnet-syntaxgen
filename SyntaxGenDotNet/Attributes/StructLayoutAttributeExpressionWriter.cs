using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using X10D.Core;

namespace SyntaxGenDotNet.Attributes;

/// <summary>
///     Represents an expression writer for <see cref="StructLayoutAttribute" />.
/// </summary>
public sealed class StructLayoutAttributeExpressionWriter : AttributeExpressionWriter<StructLayoutAttribute>
{
    /// <inheritdoc />
    public override IEnumerable<Expression> CreateAttributeExpressions(MemberInfo declaringMember,
        IReadOnlyCollection<StructLayoutAttribute> attribute)
    {
        if (declaringMember is not Type type)
        {
            return ArraySegment<Expression>.Empty;
        }

        TypeAttributes mask = (type.Attributes & TypeAttributes.LayoutMask);
        bool hasNonAnsiCharset = (type.Attributes & TypeAttributes.StringFormatMask) != TypeAttributes.AnsiClass;
        bool writeStructLayout = mask is not (TypeAttributes.AutoLayout or TypeAttributes.SequentialLayout) || hasNonAnsiCharset;

        if (!writeStructLayout)
        {
            return ArraySegment<Expression>.Empty;
        }

        var arguments = new List<Expression>();
        var constructor = AttributeType.GetConstructor(new[] {typeof(LayoutKind)})!;
        ConstantExpression structLayout = mask switch
        {
            TypeAttributes.AutoLayout => Expression.Constant(LayoutKind.Auto),
            TypeAttributes.SequentialLayout => Expression.Constant(LayoutKind.Sequential),
            _ => Expression.Constant(LayoutKind.Explicit)
        };

        arguments.Add(structLayout);
        NewExpression newExpression = Expression.New(constructor, arguments);

        if (!hasNonAnsiCharset)
        {
            return Expression.MemberInit(newExpression).AsEnumerableValue();
        }

        var charSet = (type.Attributes & TypeAttributes.StringFormatMask) switch
        {
            TypeAttributes.UnicodeClass => CharSet.Unicode,
            TypeAttributes.AutoClass => CharSet.Auto,
            _ => CharSet.Ansi
        };

        var charSetExpression = Expression.Constant(charSet);
        var charSetField = AttributeType.GetField(nameof(StructLayoutAttribute.CharSet))!;
        MemberAssignment bind = Expression.Bind(charSetField, charSetExpression);
        return Expression.MemberInit(newExpression, bind).AsEnumerableValue();
    }
}
