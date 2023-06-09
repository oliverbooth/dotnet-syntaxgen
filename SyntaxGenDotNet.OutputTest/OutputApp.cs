using SyntaxGenDotNet.CIL;
using SyntaxGenDotNet.CppCLI;
using SyntaxGenDotNet.CSharp;
using SyntaxGenDotNet.FSharp;
using SyntaxGenDotNet.Syntax;
using SyntaxGenDotNet.Syntax.Tokens;
using SyntaxGenDotNet.VisualBasic;

namespace SyntaxGenDotNet.OutputTest;

internal static class OutputApp
{
    public static void Run()
    {
        var generators = new SyntaxGenerator[]
        {
            new CSharpSyntaxGenerator(), new VisualBasicSyntaxGenerator(), new CilSyntaxGenerator(),
            new CppCliSyntaxGenerator(), new FSharpSyntaxGenerator()
        };

        var member = typeof(MyClass).GetMethod(nameof(MyClass.MethodWithImplOptions))!;
        foreach (SyntaxGenerator generator in generators)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int remainingSpace = Console.WindowWidth - (generator.LanguageName.Length + 6);
            var hyphens = new string('-', remainingSpace);
            Console.WriteLine($@"--- {generator.LanguageName} {hyphens}");
            Console.ResetColor();

            var declaration = generator.GenerateDeclaration(member);
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
