Imports System
Imports System.Linq
Imports System.Reflection
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.CompilerServices

Namespace Education.Data

    Public Enum EducationType
        <Display(Name:="Not Stated")>
        NoInfo = 0
        <Display(Name:="High School")>
        School = 1
        <Display(Name:="College")>
        College = 2
        <Display(Name:="University Degree")>
        UniversityDegree = 3
        <Display(Name:="PhD")>
        PhD = 4
    End Enum

    Public Class EducationDegree

        Public Property Value As EducationType

        Public Property DisplayName As String
    End Class

    Public Module Extensions

        <Extension()>
        Public Function GetAttribute(Of TAttribute As Attribute)(ByVal enumValue As [Enum]) As TAttribute
            Return enumValue.[GetType]().GetMember(enumValue.ToString()).First().GetCustomAttribute(Of TAttribute)()
        End Function
    End Module
End Namespace
