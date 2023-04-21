using System.Runtime.InteropServices;

namespace SyntaxGenDotNet.OutputTest;

[Serializable]
[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
internal struct MyStruct : IEquatable<MyStruct>
{
    [FieldOffset(0)] public int MyInt;
    [FieldOffset(1)] public MyEnum MyEnum;
    [FieldOffset(2)] public bool MyBool;

    public bool Equals(MyStruct other)
    {
        throw new NotImplementedException();
    }
}
