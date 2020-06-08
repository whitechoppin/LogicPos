Imports System.Data.Odbc

Public Class fpreviewpiutang
    Private Sub fpreviewpiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
    End Sub

    Sub comboboxcustomer()
        Call koneksii()
        cmbcustomer.Items.Clear()
        cmbcustomer.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_pelanggan", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbcustomer.AutoCompleteCustomSource.Add(dr("kode_pelanggan"))
                cmbcustomer.Items.Add(dr("kode_pelanggan"))
            End While
        End If
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click

    End Sub

    Private Sub fpreviewpiutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click

    End Sub
End Class