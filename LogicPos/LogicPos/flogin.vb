Imports System.Data.Odbc
Public Class flogin
    Sub login()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtusername.Text + "' AND password_user= '" + txtpassword.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            MsgBox("Username atau Password ada yang salah !", MsgBoxStyle.Exclamation, "Error Login")
        Else
            MsgBox("Selamat Datang " & txtusername.Text & " ! ", MsgBoxStyle.Information, "Successfull Login")
            'Call offform()
            'sql = "SELECT * FROM tb_detailuser WHERE username = '" + txtusername.Text + "'"
            'cmmd = New OdbcCommand(sql, cnn)
            'dr = cmmd.ExecuteReader()
            'While dr.Read
            '    Dim a As Integer
            '    a = CInt(dr("idhead"))
            '    'MsgBox(dr("idhead").ToString)
            '    fmenu.MenuUtama.Items.Item(a).Visible = True
            'End While

            'sql = "SELECT * FROM tb_detailuser WHERE username = '" + txtusername.Text + "'"
            'cmmd = New OdbcCommand(sql, cnn)
            'dr = cmmd.ExecuteReader()
            'While dr.Read
            '    sql = "SELECT * FROM tb_detailuser WHERE username = '" + txtusername.Text + "' and idhead= 0 "
            '    cmmd = New OdbcCommand(sql, cnn)
            '    dr = cmmd.ExecuteReader()
            '    While dr.Read
            '        Dim b As Integer
            '        b = CInt(dr("idchild"))
            '        If b = 9 Then
            '            On Error Resume Next
            '        Else
            '            'MsgBox(dr("idchild").ToString)
            '            fmenu.mastermenu.DropDownItems.Item(b).Visible = True
            '            On Error Resume Next
            '        End If
            '    End While


            '    sql = "SELECT * FROM tb_detailuser WHERE username = '" + txtusername.Text + "' and idhead= 2 "
            '    cmmd = New OdbcCommand(sql, cnn)
            '    dr = cmmd.ExecuteReader()
            '    While dr.Read
            '        Dim c As Integer
            '        c = dr("idchild")
            '        If c = 9 Then
            '            On Error Resume Next
            '        Else
            '            fmenu.AdministrasiMenu.DropDownItems.Item(c).Visible = True
            '            On Error Resume Next
            '        End If
            '    End While

            '    sql = "SELECT * FROM tb_detailuser WHERE username = '" + txtusername.Text + "' and idhead= 3 "
            '    cmmd = New OdbcCommand(sql, cnn)
            '    dr = cmmd.ExecuteReader()
            '    While dr.Read
            '        Dim d As Integer
            '        d = dr("idchild")
            '        If d = 9 Then
            '            On Error Resume Next
            '        Else
            '            fmenu.transaksimenu.DropDownItems.Item(d).Visible = True
            '            On Error Resume Next
            '        End If
            '    End While

            '    sql = "SELECT * FROM tb_detailuser WHERE username = '" + txtusername.Text + "' and idhead=4 "
            '    cmmd = New OdbcCommand(sql, cnn)
            '    dr = cmmd.ExecuteReader()

            '    While dr.Read
            '        Dim e As Integer
            '        e = dr("idchild")
            '        If e = 9 Then
            '            On Error Resume Next
            '        Else
            '            fmenu.laporanmenu.DropDownItems.Item(e).Visible = True
            '            On Error Resume Next
            '        End If
            '    End While
            'End While

            fmenu.statususer.Text = txtusername.Text.ToUpper
            fmenu.Show()
        End If
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If txtusername.Text = "" Then
            MessageBox.Show("User masih kosong", "username", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            txtusername.Focus()
            Exit Sub
        End If
        If txtpassword.Text = "" Then
            MessageBox.Show("Password masih kosong", "password", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            txtpassword.Focus()
        Else
        End If
        Call login()
    End Sub


    Private Sub flogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = False
        txtpassword.PasswordChar = "•"
    End Sub
End Class