using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SyntaxGenDotNet;

/// <summary>
///     Represents an expression writer for <see cref="StructLayoutAttribute" />.
/// </summary>
public sealed class StructLayoutAttributeExpressionWriter : AttributeExpressionWriter<StructLayoutAttribute>
{
    /// <inheritdoc />
    public override Expression CreateAttributeExpression(Type declaringType, StructLayoutAttribute? attribute)
    {
        TypeAttributes mask = (declaringType.Attributes & TypeAttributes.LayoutMask);
        bool hasNonAnsiCharset = (declaringType.Attributes & TypeAttributes.StringFormatMask) != TypeAttributes.AnsiClass;
        bool writeStructLayout = mask is not (TypeAttributes.AutoLayout or TypeAttributes.SequentialLayout) || hasNonAnsiCharset;

        if (!writeStructLayout)
        {
            return Expression.Empty();
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
            return Expression.MemberInit(newExpression);
        }

        var charSet = (declaringType.Attributes & TypeAttributes.StringFormatMask) switch
        {
            TypeAttributes.UnicodeClass => CharSet.Unicode,
            TypeAttributes.AutoClass => CharSet.Auto,
            _ => CharSet.Ansi
        };

        var charSetExpression = Expression.Constant(charSet);
        var charSetField = AttributeType.GetField(nameof(StructLayoutAttribute.CharSet))!;
        MemberAssignment bind = Expression.Bind(charSetField, charSetExpression);
        return Expression.MemberInit(newExpression, bind);
    }
}
