using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CIL.Utilities;

/// <summary>
///     Provides utility methods for working with tokens in the CIL language.
/// </summary>
public static class TokenUtility
{
    /// <summary>
    ///     Creates a literal token from a value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The literal token.</returns>
    public static SyntaxToken CreateLiteralToken(object? value)
    {
        return value switch
        {
            null => Keywords.NullKeyword,
            bool boolValue => boolValue ? Keywords.TrueKeyword : Keywords.FalseKeyword,
            string stringValue => new StringLiteralToken(stringValue),
            char charValue => new CilCharLiteralToken(charValue),
            sbyte sbyteValue => new IntegerLiteralToken(sbyteValue),
            byte byteValue => new IntegerLiteralToken(byteValue),
            short shortValue => new IntegerLiteralToken(shortValue),
            ushort ushortValue => new IntegerLiteralToken(ushortValue),
            int intValue => new IntegerLiteralToken(intValue),
            uint uintValue => new IntegerLiteralToken(uintValue),
            long longValue => new IntegerLiteralToken(longValue),
            ulong ulongValue => new IntegerLiteralToken(ulongValue),
            float floatValue => new FloatingPointLiteralToken(floatValue),
            double doubleValue => new FloatingPointLiteralToken(doubleValue),
            _ => new LiteralToken(value.ToString()!)
        };
    }
}
