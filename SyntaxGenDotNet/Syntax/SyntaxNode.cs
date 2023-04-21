using System.Text;

namespace SyntaxGenDotNet.Syntax;

/// <summary>
///     Represents a syntax node.
/// </summary>
public class SyntaxNode : ICloneable
{
    /// <summary>
    ///     Gets or sets the children of the syntax node.
    /// </summary>
    /// <value>The children of the syntax node.</value>
    public IReadOnlyList<SyntaxNode> Children { get; set; } = ArraySegment<SyntaxNode>.Empty;

    /// <summary>
    ///     Gets or sets the leading whitespace.
    /// </summary>
    /// <value>The leading whitespace.</value>
    public WhitespaceTrivia LeadingWhitespace { get; set; } = WhitespaceTrivia.None;

    /// <summary>
    ///     Gets or sets a value indicating whether to strip trailing whitespace from the preceding syntax node.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if trailing whitespace should be stripped from the preceding syntax node; otherwise,
    ///     <see langword="false" />.
    /// </value>
    public bool StripTrailingWhitespace { get; set; }

    /// <summary>
    ///     Gets or sets the textual representation of the syntax node.
    /// </summary>
    /// <value>The textual representation of the syntax node.</value>
    public virtual string Text
    {
        get
        {
            var builder = new StringBuilder();
            builder.Append(LeadingWhitespace);

            for (var index = 0; index < Children.Count; index++)
            {
                var node = Children[index];
                builder.Append(node);

                if (index < Children.Count - 1 && !Children[index + 1].StripTrailingWhitespace)
                {
                    builder.Append(node.TrailingWhitespace);
                }
            }

            return builder.ToString();
        }
    }

    /// <summary>
    ///     Gets or sets the trailing whitespace.
    /// </summary>
    /// <value>The trailing whitespace.</value>
    public WhitespaceTrivia TrailingWhitespace { get; set; } = WhitespaceTrivia.Space;

    /// <summary>
    ///     Sets the leading and trailing whitespace simultaneously.
    /// </summary>
    /// <value>The new leading and trailing whitespace value.</value>
    public WhitespaceTrivia Whitespace
    {
        set
        {
            LeadingWhitespace = value;
            TrailingWhitespace = value;
        }
    }

    /// <summary>
    ///     Adds a child syntax node to the syntax node.
    /// </summary>
    /// <param name="child">The child syntax node to add.</param>
    /// <exception cref="ArgumentNullException"><paramref name="child" /> is <see langword="null" />.</exception>
    /// <exception cref="InvalidOperationException"><paramref name="child" /> already has a parent.</exception>
    /// <exception cref="InvalidOperationException"><paramref name="child" /> is already a child of the syntax node.</exception>
    /// <exception cref="InvalidOperationException"><paramref name="child" /> is a descendant of the syntax node.</exception>
    public void AddChild(SyntaxNode child)
    {
        if (child is null)
        {
            throw new ArgumentNullException(nameof(child));
        }

        if (child == this)
        {
            throw new InvalidOperationException("The child is the syntax node.");
        }

        Children = Children.Append(child).ToArray();
    }

    /// <inheritdoc />
    public virtual object Clone()
    {
        var clone = (SyntaxNode)MemberwiseClone();
        clone.Children = Children.Select(c => (SyntaxNode)c.Clone()).ToArray();
        return clone;
    }

    /// <summary>
    ///     Returns a string representation of the syntax node.
    /// </summary>
    /// <returns>A string representation of the syntax node.</returns>
    public override string ToString()
    {
        return Text;
    }

    /// <summary>
    ///     Creates a clone of the syntax node with the specified modification.
    /// </summary>
    /// <param name="modification">The modification to apply to the clone.</param>
    /// <returns>A clone of the syntax node with the specified modification.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="modification" /> is <see langword="null" />.</exception>
    public virtual SyntaxNode With(Action<SyntaxNode> modification)
    {
        if (modification is null)
        {
            throw new ArgumentNullException(nameof(modification));
        }

        var clone = (SyntaxNode)Clone();
        modification(clone);
        return clone;
    }
}
