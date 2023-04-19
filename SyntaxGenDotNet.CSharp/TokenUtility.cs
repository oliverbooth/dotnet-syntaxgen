using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.CSharp;

internal sealed class TokenUtility
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
            char charValue => new CharLiteralToken(charValue),
            int intValue => new IntegerLiteralToken(intValue),
            long longValue => new IntegerLiteralToken(longValue),
            float floatValue => new CSharpSingleLiteralToken(floatValue),
            double doubleValue => new FloatingPointLiteralToken(doubleValue),
            _ => new LiteralToken(value.ToString()!)
        };
    }
}
