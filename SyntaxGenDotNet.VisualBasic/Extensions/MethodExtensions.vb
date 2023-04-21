Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace Extensions
    Friend Module MethodExtensions
        <Extension>
        Public Function IsFunction(method As MethodInfo) As Boolean
            Return method.ReturnType <> GetType(Void)
        End Function

        <Extension>
        Public Function IsSub(method As MethodInfo) As Boolean
            Return method.ReturnType = GetType(Void)
        End Function
    End Module
End NameSpace
