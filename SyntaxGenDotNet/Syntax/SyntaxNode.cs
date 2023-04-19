using System.Text;

namespace SyntaxGenDotNet.Syntax;

/// <summary>
///     Represents a syntax node.
/// </summary>
public class SyntaxNode
{
    /// <summary>
    ///     Gets or sets the children of the syntax node.
    /// </summary>
    /// <value>The children of the syntax node.</value>
    public virtual IReadOnlyList<SyntaxNode> Children { get; set; } = ArraySegment<SyntaxNode>.Empty;

    /// <summary>
    ///     Gets or sets the leading whitespace.
    /// </summary>
    /// <value>The leading whitespace.</value>
    public virtual WhitespaceTrivia LeadingWhitespace { get; set; } = WhitespaceTrivia.None;

    /// <summary>
    ///     Gets or sets the parent syntax node.
    /// </summary>
    /// <value>The parent syntax node.</value>
    public virtual SyntaxNode? Parent { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to strip trailing whitespace from the preceding syntax node.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> if trailing whitespace should be stripped from the preceding syntax node; otherwise,
    ///     <see langword="false" />.
    /// </value>
    public virtual bool StripTrailingWhitespace { get; set; }

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
    public virtual WhitespaceTrivia TrailingWhitespace { get; set; } = WhitespaceTrivia.Space;

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

        if (child.Parent is not null)
        {
            throw new InvalidOperationException("The child already has a parent.");
        }

        if (child == this)
        {
            throw new InvalidOperationException("The child is the syntax node.");
        }

        if (child.IsDescendantOf(this))
        {
            throw new InvalidOperationException("The child is a descendant of the syntax node.");
        }

        if (Children.Contains(child))
        {
            return;
        }

        child.Parent = this;
        Children = Children.Append(child).ToArray();
    }

    /// <summary>
    ///     Returns a value indicating whether the syntax node is a descendant of the specified syntax node.
    /// </summary>
    /// <param name="syntaxNode">The syntax node.</param>
    /// <returns>
    ///     <see langword="true" /> if the syntax node is a descendant of the specified syntax node; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="syntaxNode" /> is <see langword="null" />.</exception>
    public bool IsDescendantOf(SyntaxNode syntaxNode)
    {
        if (syntaxNode is null)
        {
            throw new ArgumentNullException(nameof(syntaxNode));
        }

        if (Parent is null)
        {
            return false;
        }

        if (Parent == syntaxNode)
        {
            return true;
        }

        return Parent.IsDescendantOf(syntaxNode);
    }

    /// <summary>
    ///     Returns a string representation of the syntax node.
    /// </summary>
    /// <returns>A string representation of the syntax node.</returns>
    public override string ToString()
    {
        return Text;
    }
}
