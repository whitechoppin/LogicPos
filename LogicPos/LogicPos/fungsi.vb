Imports System.Data.Odbc

Module fungsi
    Dim cpuid As String = flogin.CPUIDPOS

    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        Dim allowChrs() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next

        Return sResult
    End Function

    Public Function GetRandomString(ByVal iLength As Integer) As String
        Dim sResult As String = ""
        Dim rdm As New Random()

        For i As Integer = 1 To iLength
            sResult &= ChrW(rdm.Next(32, 126))
        Next

        Return sResult
    End Function

    Public Function GetPrinterName(ByVal nomor As Integer) As String
        Dim sResult As String = ""
        Call koneksii()
        sql = "SELECT * FROM tb_printer WHERE nomor='" & nomor & "' AND computer_id='" & cpuid & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        If dr.HasRows Then
            sResult = dr("nama_printer")
        Else
            sResult = ""
        End If

        Return sResult
    End Function
End Module
