using System.Reflection;
using SyntaxGenDotNet.CIL;
using SyntaxGenDotNet.CSharp;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;
using SyntaxGenDotNet.VisualBasic;

namespace SyntaxGenDotNet.OutputTest;

internal static class OutputApp
{
    public static void Run()
    {
        var field = typeof(MyClass).GetField("InternalConstantChar", (BindingFlags)(-1))!;
        ISyntaxGenerator[] generators = {new CSharpSyntaxGenerator(), new VisualBasicSyntaxGenerator(), new CilSyntaxGenerator()};

        foreach (ISyntaxGenerator generator in generators)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($@"----------- {generator.LanguageName} -----------");
            Console.ResetColor();

            var declaration = generator.GenerateFieldDeclaration(field);
            WritePlainTextSyntax(declaration);
            Console.WriteLine();
        }
    }

    private static void WritePlainTextSyntax(SyntaxNode node)
    {
        Console.Write(node.LeadingWhitespace);

        for (var index = 0; index < node.Children.Count; index++)
        {
            SyntaxNode child = node.Children[index];
            if (child.Children.Count > 0)
            {
                WritePlainTextSyntax(child);
                continue;
            }

            Console.Write(child.LeadingWhitespace);

            Console.ForegroundColor = child switch
            {
                KeywordToken => ConsoleColor.DarkBlue,
                TypeIdentifierToken => ConsoleColor.Cyan,
                NumericLiteralToken => ConsoleColor.Red,
                StringLiteralToken => ConsoleColor.DarkYellow,
                CharLiteralToken => ConsoleColor.DarkYellow,
                CommentToken => ConsoleColor.DarkGreen,
                _ => ConsoleColor.White
            };
            Console.Write(child.Text);
            Console.ResetColor();

            if (index < node.Children.Count - 1 && !node.Children[index + 1].StripTrailingWhitespace)
            {
                Console.Write(child.TrailingWhitespace);
            }
        }

        if (node.Children.Count > 0 && node.Children[0].Children.Count > 0)
        {
            Console.Write(node.TrailingWhitespace);
        }
    }
}
