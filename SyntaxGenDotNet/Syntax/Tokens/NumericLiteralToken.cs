using System.Globalization;

namespace SyntaxGenDotNet.Syntax.Tokens;

/// <summary>
///     Represents a numeric literal syntax node.
/// </summary>
public abstract class NumericLiteralToken : LiteralToken
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected NumericLiteralToken(int literalValue) : this(literalValue.ToString(CultureInfo.InvariantCulture))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected NumericLiteralToken(long literalValue) : this(literalValue.ToString(CultureInfo.InvariantCulture))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected NumericLiteralToken(ulong literalValue) : this(literalValue.ToString(CultureInfo.InvariantCulture))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected NumericLiteralToken(float literalValue) : this(literalValue.ToString(CultureInfo.InvariantCulture))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected NumericLiteralToken(double literalValue) : this(literalValue.ToString(CultureInfo.InvariantCulture))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    protected NumericLiteralToken(decimal literalValue) : this(literalValue.ToString(CultureInfo.InvariantCulture))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NumericLiteralToken" /> class.
    /// </summary>
    /// <param name="literalValue">The literal value.</param>
    /// <exception cref="ArgumentNullException"><paramref name="literalValue" /> is <see langword="null" />.</exception>
    protected NumericLiteralToken(string literalValue) : base(literalValue)
    {
    }
}
