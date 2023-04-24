using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using SyntaxGenDotNet.Syntax;

namespace SyntaxGenDotNet.CIL.Utilities;

internal static class MethodUtility
{
    /// <summary>
    ///     Writes all attributes for the specified <see cref="MethodInfo" /> to the specified <see cref="SyntaxNode" />.
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the attributes will be written.</param>
    /// <param name="method">The <see cref="MethodInfo" /> whose attributes to write.</param>
    public static void WriteAllAttributes(SyntaxNode target, MethodInfo method)
    {
        MethodAttributes attributes = method.Attributes;
        WriteVisibilityAttribute(target, attributes);
        WriteContractAttributes(target, attributes);
        WriteVTableLayoutAttributes(target, attributes);
        WriteImplementationAttributes(target, attributes);
        WriteInteropAttributes(target, attributes);
    }

    /// <summary>
    ///     Writes the implementation flags for the specified <see cref="MethodInfo" /> to the specified
    /// </summary>
    /// <param name="target">The <see cref="SyntaxNode" /> to which the flags will be written.</param>
    /// <param name="method">The <see cref="MethodInfo" /> whose flags to write.</param>
    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    [SuppressMessage("ReSharper", "NonConstantEqualityExpressionHasConstantResult")]
    public static void WriteImplementationFlags(SyntaxNode target, MethodInfo method)
    {
        MethodImplAttributes attributes = method.MethodImplementationFlags;
        WriteCodeTypeFlags(target, attributes);
        WriteManagedFlags(target, attributes);

        if ((attributes & MethodImplAttributes.ForwardRef) != 0)
        {
            target.AddChild(Keywords.ForwardRefKeyword);
        }

        if ((attributes & MethodImplAttributes.Synchronized) != 0)
        {
            target.AddChild(Keywords.SynchronizedKeyword);
        }

        WriteInliningFlags(target, attributes);
        WriteOptimizationFlags(target, attributes);
    }

    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    private static void WriteCodeTypeFlags(SyntaxNode target, MethodImplAttributes attributes)
    {
        switch (attributes & MethodImplAttributes.CodeTypeMask)
        {
            case MethodImplAttributes.IL:
                target.AddChild(Keywords.CilKeyword);
                break;

            case MethodImplAttributes.Native:
                target.AddChild(Keywords.NativeKeyword);
                break;

            case MethodImplAttributes.Runtime:
                target.AddChild(Keywords.RuntimeKeyword);
                break;
        }
    }

    private static void WriteContractAttributes(SyntaxNode target, MethodAttributes attributes)
    {
        if ((attributes & MethodAttributes.Static) != 0)
        {
            target.AddChild(Keywords.StaticKeyword);
        }

        if ((attributes & MethodAttributes.Final) != 0)
        {
            target.AddChild(Keywords.FinalKeyword);
        }

        if ((attributes & MethodAttributes.Virtual) != 0)
        {
            target.AddChild(Keywords.VirtualKeyword);
        }

        if ((attributes & MethodAttributes.HideBySig) != 0)
        {
            target.AddChild(Keywords.HideBySigKeyword);
        }
    }

    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    private static void WriteInliningFlags(SyntaxNode target, MethodImplAttributes attributes)
    {
        if ((attributes & MethodImplAttributes.NoInlining) != 0)
        {
            target.AddChild(Keywords.NoInliningKeyword);
        }
    }

    private static void WriteInteropAttributes(SyntaxNode target, MethodAttributes attributes)
    {
        if ((attributes & MethodAttributes.PinvokeImpl) != 0)
        {
            target.AddChild(Keywords.PInvokeImplKeyword);
        }

        if ((attributes & MethodAttributes.UnmanagedExport) != 0)
        {
            target.AddChild(Keywords.UnmanagedKeyword);
        }

        if ((attributes & MethodAttributes.RTSpecialName) != 0)
        {
            target.AddChild(Keywords.RTSpecialNameKeyword);
        }
    }

    private static void WriteImplementationAttributes(SyntaxNode target, MethodAttributes attributes)
    {
        if ((attributes & MethodAttributes.Abstract) != 0)
        {
            target.AddChild(Keywords.AbstractKeyword);
        }

        if ((attributes & MethodAttributes.SpecialName) != 0)
        {
            target.AddChild(Keywords.SpecialNameKeyword);
        }

        if ((attributes & MethodAttributes.Static) != 0)
        {
            target.AddChild(Keywords.StaticKeyword);
        }
        else
        {
            target.AddChild(Keywords.InstanceKeyword);
        }
    }

    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    private static void WriteManagedFlags(SyntaxNode target, MethodImplAttributes attributes)
    {
        switch (attributes & MethodImplAttributes.ManagedMask)
        {
            case MethodImplAttributes.Managed:
                target.AddChild(Keywords.ManagedKeyword);
                break;

            case MethodImplAttributes.Unmanaged:
                target.AddChild(Keywords.UnmanagedKeyword);
                break;
        }
    }

    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    private static void WriteOptimizationFlags(SyntaxNode target, MethodImplAttributes attributes)
    {
        if ((attributes & MethodImplAttributes.NoOptimization) != 0)
        {
            target.AddChild(Keywords.NoOptimizationKeyword);
        }
    }

    private static void WriteVisibilityAttribute(SyntaxNode target, MethodAttributes attributes)
    {
        switch (attributes & MethodAttributes.MemberAccessMask)
        {
            case MethodAttributes.Public:
                target.AddChild(Keywords.PublicKeyword);
                break;

            case MethodAttributes.Family:
                target.AddChild(Keywords.FamilyKeyword);
                break;

            case MethodAttributes.Assembly:
                target.AddChild(Keywords.AssemblyKeyword);
                break;

            case MethodAttributes.FamANDAssem:
                target.AddChild(Keywords.FamAndAssemKeyword);
                break;

            case MethodAttributes.FamORAssem:
                target.AddChild(Keywords.FamOrAssemKeyword);
                break;

            case MethodAttributes.Private:
                target.AddChild(Keywords.PrivateKeyword);
                break;
        }
    }

    private static void WriteVTableLayoutAttributes(SyntaxNode target, MethodAttributes attributes)
    {
        if ((attributes & MethodAttributes.NewSlot) != 0)
        {
            target.AddChild(Keywords.NewSlotKeyword);
        }
    }
}
