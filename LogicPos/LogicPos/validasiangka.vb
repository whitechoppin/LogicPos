Module validasiangka
    Function ValidAngka(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim strValid As String = "0123456789"
        If Strings.InStr(strValid, e.KeyChar) = 0 And Not (e.KeyChar = Strings.Chr(Keys.Back)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function Validdesimal(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim strValid As String = "0123456789,."
        If Strings.InStr(strValid, e.KeyChar) = 0 And Not (e.KeyChar = Strings.Chr(Keys.Back)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function ValidAngkaHuruf(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim strValid As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789"
        If Strings.InStr(strValid, e.KeyChar) = 0 And Not (e.KeyChar = Strings.Chr(Keys.Back)) Then
            Return True
        Else
            Return False
        End If
    End Function

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

    'pastekan di keypress
    'e.Handled = Not (Char.IsDigit(e.KeyChar) OR e.KeyChar=".")
End Module
