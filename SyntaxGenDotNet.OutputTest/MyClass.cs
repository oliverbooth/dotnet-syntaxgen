// ReSharper disable All

using System.Runtime.InteropServices;
using JetBrains.Annotations;

#pragma warning disable CS0414
#pragma warning disable CS0649
#pragma warning disable CS0169
#pragma warning disable CS0067

namespace SyntaxGenDotNet.OutputTest;

internal abstract class MyClass : Attribute, IComparable<MyClass>, IComparable, ICloneable
{
    public int PublicInstanceField;
    protected int ProtectedInstanceField;
    private int PrivateInstanceField;
    internal int InternalInstanceField;
    protected internal int ProtectedInternalInstanceField;
    private protected int PrivateProtectedInstanceField;

    public int? PublicInstanceNullableField;
    protected int? ProtectedInstanceNullableField;
    private int? PrivateInstanceNullableField;
    internal int? InternalInstanceNullableField;
    protected internal int? ProtectedInternalInstanceNullableField;
    private protected int? PrivateProtectedInstanceNullableField;

    public readonly int PublicInstanceReadOnlyField = 42;
    protected readonly int ProtectedInstanceReadOnlyField = 42;
    private readonly int PrivateInstanceReadOnlyField = 42;
    internal readonly int InternalInstanceReadOnlyField = 42;
    protected internal readonly int ProtectedInternalInstanceReadOnlyField = 42;
    private protected readonly int PrivateProtectedInstanceReadOnlyField = 42;

    public static int PublicStaticField;
    protected static int ProtectedStaticField;
    private static int PrivateStaticField;
    internal static int InternalStaticField;
    protected internal static int ProtectedInternalStaticField;
    private protected static int PrivateProtectedStaticField;

    public static readonly int PublicStaticReadOnlyField = 42;
    protected static readonly int ProtectedStaticReadOnlyField = 42;
    private static readonly int PrivateStaticReadOnlyField = 42;
    internal static readonly int InternalStaticReadOnlyField = 42;
    protected internal static readonly int ProtectedInternalStaticReadOnlyField = 42;
    private protected static readonly int PrivateProtectedStaticReadOnlyField = 42;

    public static readonly List<int> PublicStaticReadOnlyListField = new() {1, 2, 3};
    protected static readonly List<int> ProtectedStaticReadOnlyListField = new() {1, 2, 3};
    private static readonly List<int> PrivateStaticReadOnlyListField = new() {1, 2, 3};
    internal static readonly List<int> InternalStaticReadOnlyListField = new() {1, 2, 3};
    protected internal static readonly List<int> ProtectedInternalStaticReadOnlyListField = new() {1, 2, 3};
    private protected static readonly List<int> PrivateProtectedStaticReadOnlyListField = new() {1, 2, 3};

    public static readonly int[] PublicStaticReadOnlyArrayField = new[] {1, 2, 3};
    protected static readonly int[] ProtectedStaticReadOnlyArrayField = new[] {1, 2, 3};
    private static readonly int[] PrivateStaticReadOnlyArrayField = new[] {1, 2, 3};
    internal static readonly int[] InternalStaticReadOnlyArrayField = new[] {1, 2, 3};
    protected internal static readonly int[] ProtectedInternalStaticReadOnlyArrayField = new[] {1, 2, 3};
    private protected static readonly int[] PrivateProtectedStaticReadOnlyArrayField = new[] {1, 2, 3};

    public const int PublicConstantInteger = 42;
    protected const int ProtectedConstantInteger = 42;
    private const int PrivateConstantInteger = 42;
    internal const int InternalConstantInteger = 42;
    protected internal const int ProtectedInternalConstantInteger = 42;
    private protected const int PrivateProtectedConstantInteger = 42;

    [CLSCompliant(false)]
    public const uint PublicConstantUnsignedInteger = 42;

    public const string PublicConstantString = "42";
    protected const string ProtectedConstantString = "42";
    private const string PrivateConstantString = "42";
    internal const string InternalConstantString = "42";
    protected internal const string ProtectedInternalConstantString = "42";
    private protected const string PrivateProtectedConstantString = "42";

    public const char PublicConstantChar = '4';
    protected const char ProtectedConstantChar = '4';
    private const char PrivateConstantChar = '4';
    internal const char InternalConstantChar = '4';
    protected internal const char ProtectedInternalConstantChar = '4';
    private protected const char PrivateProtectedConstantChar = '4';

    public const bool PublicConstantBoolean = true;
    protected const bool ProtectedConstantBoolean = true;
    private const bool PrivateConstantBoolean = true;
    internal const bool InternalConstantBoolean = true;
    protected internal const bool ProtectedInternalConstantBoolean = true;
    private protected const bool PrivateProtectedConstantBoolean = true;

    public const float PublicConstantFloat = 42.0f;
    protected const float ProtectedConstantFloat = 42.0f;
    private const float PrivateConstantFloat = 42.0f;
    internal const float InternalConstantFloat = 42.0f;
    protected internal const float ProtectedInternalConstantFloat = 42.0f;
    private protected const float PrivateProtectedConstantFloat = 42.0f;

    public const double PublicConstantDouble = 42.0;
    protected const double ProtectedConstantDouble = 42.0;
    private const double PrivateConstantDouble = 42.0;
    internal const double InternalConstantDouble = 42.0;
    protected internal const double ProtectedInternalConstantDouble = 42.0;
    private protected const double PrivateProtectedConstantDouble = 42.0;

    public int PublicInstanceProperty { get; set; }

    protected int ProtectedInstanceProperty { get; set; }

    private int PrivateInstanceProperty { get; set; }

    internal int InternalInstanceProperty { get; set; }

    protected internal int ProtectedInternalInstanceProperty { get; set; }

    private protected int PrivateProtectedInstanceProperty { get; set; }

    public static int PublicStaticProperty { get; set; }

    protected static int ProtectedStaticProperty { get; set; }

    private static int PrivateStaticProperty { get; set; }

    internal static int InternalStaticProperty { get; set; }

    protected internal static int ProtectedInternalStaticProperty { get; set; }

    private protected static int PrivateProtectedStaticProperty { get; set; }

    public int this[int index]
    {
        get => 0;
    }

    public int this[int index1, int index2]
    {
        get => 0;
    }

    public int this[int index1, int index2, int index3]
    {
        get => 0;
    }

    public static int PublicStaticMethod()
    {
        return 0;
    }

    protected static int ProtectedStaticMethod()
    {
        return 0;
    }

    private static int PrivateStaticMethod()
    {
        return 0;
    }

    internal static int InternalStaticMethod()
    {
        return 0;
    }

    protected internal static int ProtectedInternalStaticMethod()
    {
        return 0;
    }

    private protected static int PrivateProtectedStaticMethod()
    {
        return 0;
    }

    public int PublicInstanceMethod()
    {
        return 0;
    }

    protected int ProtectedInstanceMethod()
    {
        return 0;
    }

    private int PrivateInstanceMethod()
    {
        return 0;
    }

    internal int InternalInstanceMethod()
    {
        return 0;
    }

    protected internal int ProtectedInternalInstanceMethod()
    {
        return 0;
    }

    private protected int PrivateProtectedInstanceMethod()
    {
        return 0;
    }

    public T PublicInstanceGenericMethod<T>()
    {
        return default!;
    }

    protected T ProtectedInstanceGenericMethod<T>()
    {
        return default!;
    }

    private T PrivateInstanceGenericMethod<T>()
    {
        return default!;
    }

    internal T InternalInstanceGenericMethod<T>()
    {
        return default!;
    }

    protected internal T ProtectedInternalInstanceGenericMethod<T>()
    {
        return default!;
    }

    private protected T PrivateProtectedInstanceGenericMethod<T>()
    {
        return default!;
    }

    public static T PublicStaticGenericMethod<T>()
    {
        return default!;
    }

    protected static T ProtectedStaticGenericMethod<T>()
    {
        return default!;
    }

    private static T PrivateStaticGenericMethod<T>()
    {
        return default!;
    }

    internal static T InternalStaticGenericMethod<T>()
    {
        return default!;
    }

    protected internal static T ProtectedInternalStaticGenericMethod<T>()
    {
        return default!;
    }

    private protected static T PrivateProtectedStaticGenericMethod<T>()
    {
        return default!;
    }

    public static int PublicStaticMethod(int x)
    {
        return 0;
    }

    protected static int ProtectedStaticMethod(int x)
    {
        return 0;
    }

    private static int PrivateStaticMethod(int x)
    {
        return 0;
    }

    internal static int InternalStaticMethod(int x)
    {
        return 0;
    }

    protected internal static int ProtectedInternalStaticMethod(int x)
    {
        return 0;
    }

    private protected static int PrivateProtectedStaticMethod(int x)
    {
        return 0;
    }

    public static event EventHandler PublicStaticEvent = null!;

    protected static event EventHandler ProtectedStaticEvent = null!;

    private static event EventHandler PrivateStaticEvent = null!;

    internal static event EventHandler InternalStaticEvent = null!;

    protected internal static event EventHandler ProtectedInternalStaticEvent = null!;

    private protected static event EventHandler PrivateProtectedStaticEvent = null!;

    public event EventHandler PublicInstanceEvent = null!;

    protected event EventHandler ProtectedInstanceEvent = null!;

    private event EventHandler PrivateInstanceEvent = null!;

    internal event EventHandler InternalInstanceEvent = null!;

    protected internal event EventHandler ProtectedInternalInstanceEvent = null!;

    private protected event EventHandler PrivateProtectedInstanceEvent = null!;

    public object Clone()
    {
        throw new NotImplementedException();
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(MyClass? other)
    {
        throw new NotImplementedException();
    }

    public unsafe void Foo(ref int[] x)
    {
    }
}
