Imports System.Data.Odbc

Public Class falamat
    Dim alamatawal, alamatakhir As String
    Private Sub falamat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Private Sub falamat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub awal()
        Call koneksii()
        sql = "SELECT * FROM tb_alamat LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            alamatawal = dr("alamat")
            txtalamat.Text = alamatawal
        Else
            txtalamat.Text = ""
        End If
    End Sub

    Sub simpan()
        Call koneksii()
        alamatakhir = txtalamat.Text

        sql = "SELECT * FROM tb_alamat WHERE alamat='" & alamatawal & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "UPDATE tb_alamat SET alamat='" & alamatakhir & "' WHERE alamat='" & alamatawal & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "INSERT INTO tb_alamat (alamat) VALUES ('" & alamatakhir & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Pengaturan Alamat Di Simpan")
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call simpan()
    End Sub
End Class