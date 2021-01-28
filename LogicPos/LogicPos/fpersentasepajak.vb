Imports System.Data.Odbc

Public Class fpersentasepajak
    Dim pajakawal, pajakakhir As Integer
    Private Sub fpersentasepajak_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Private Sub fpersentasepajak_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub awal()
        Call koneksi()
        sql = "SELECT * FROM tb_pajak LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            pajakawal = dr("pajak")
            txtpersen.Text = pajakawal
        Else
            txtpersen.Text = "0"
        End If
    End Sub

    Sub simpan()
        Call koneksi()

        sql = "SELECT * FROM tb_pajak WHERE pajak='" & pajakawal & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "UPDATE tb_pajak SET pajak='" & pajakakhir & "' WHERE pajak='" & pajakawal & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "INSERT INTO tb_pajak (pajak) VALUES ('" & pajakakhir & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Pengaturan Pajak Di Simpan")
    End Sub

    Private Sub txtpersen_TextChanged(sender As Object, e As EventArgs) Handles txtpersen.TextChanged
        If txtpersen.Text = "" Then
            txtpersen.Text = 0
        Else
            pajakakhir = txtpersen.Text
            txtpersen.Text = Format(pajakakhir, "##")
            txtpersen.SelectionStart = Len(txtpersen.Text)
        End If
    End Sub

    Private Sub txtpersen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpersen.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call simpan()
    End Sub
End Class