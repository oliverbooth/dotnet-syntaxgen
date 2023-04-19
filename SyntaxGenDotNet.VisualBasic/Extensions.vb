Imports System.Runtime.CompilerServices

Public Module Extensions
    <Extension>
    Public Function Switch (Of T, TResult)(value as T) As SwitchCaseSelector(Of T, TResult)
        Return New SwitchCaseSelector(Of T, TResult)(value)
    End Function

    Public Class SwitchCaseSelector (Of T, TResult)
        Private ReadOnly _value As T

        Public Sub New(value As T)
            _value = value
        End Sub

        Public Function [Case] (Of TCase)(condition As Func(Of T, Boolean), result As Func(Of T, TCase)) _
            As SwitchCaseSelector(Of T, TResult)
            If condition(_value) Then
                Return New SwitchCaseSelector(Of T, TResult)(_value)
            Else
                Return Me
            End If
        End Function

        Public Function [Case] (Of TCase As T)(result As Func(Of TCase, TResult)) As SwitchCaseSelector(Of T, TResult)
            Return New SwitchCaseSelector(Of T, TResult)(_value)
        End Function

        Public Function [Default](result As Func(Of T, TResult)) As TResult
            Return result(_value)
        End Function
    End Class
End Module
