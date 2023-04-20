using System.Diagnostics;
using SyntaxGenDotNet.Syntax.Declaration;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public TypeDeclaration GenerateDelegateDeclaration(Type delegateType)
    {
        Trace.Assert(delegateType.IsSubclassOf(typeof(MulticastDelegate)) || delegateType.IsSubclassOf(typeof(Delegate)),
            "The specified type is not a delegate.");

        var delegateDeclaration = new TypeDeclaration();
        return delegateDeclaration;
    }
}
