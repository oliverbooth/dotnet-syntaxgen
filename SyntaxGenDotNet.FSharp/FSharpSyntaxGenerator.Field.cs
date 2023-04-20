﻿using System.Reflection;
using SyntaxGenDotNet.Syntax.Declaration;
using SyntaxGenDotNet.Syntax.Tokens;

namespace SyntaxGenDotNet.FSharp;

public partial class FSharpSyntaxGenerator
{
    /// <inheritdoc />
    public FieldDeclaration GenerateFieldDeclaration(FieldInfo fieldInfo)
    {
        var fieldDeclaration = new FieldDeclaration();

        FieldUtility.WriteModifiers(fieldDeclaration, fieldInfo);
        FieldUtility.WriteVisibilityKeyword(fieldDeclaration, fieldInfo);

        fieldDeclaration.AddChild(new IdentifierToken(fieldInfo.Name));
        fieldDeclaration.AddChild(Operators.Colon);
        TypeUtility.WriteTypeName(fieldDeclaration, fieldInfo.FieldType);

        return fieldDeclaration;
    }
}