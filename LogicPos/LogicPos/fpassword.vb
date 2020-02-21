Imports System.Data.Odbc

Public Class fpassword
    Private Sub fpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = False
        txtpassword.PasswordChar = "•"
    End Sub
    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE password_user= '" + txtpassword.Text + "' AND jabatan_user ='Owner' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            MsgBox("Password salah !", MsgBoxStyle.Exclamation, "Error Login")
        Else
            If passwordid = 1 Then
                fbarang.txthidden.Visible = False
            End If
            Me.Close()
        End If

    End Sub
End Class