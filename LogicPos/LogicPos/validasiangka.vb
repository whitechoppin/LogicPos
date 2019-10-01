Module validasiangka
    Function ValidAngka(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Dim strValid As String = "0123456789"
        If Strings.InStr(strValid, e.KeyChar) = 0 And Not (e.KeyChar = Strings.Chr(Keys.Back)) Then
            Return True
        Else
            Return False
        End If
    End Function

End Module
