Imports System.Data.Odbc
Public Class fcaripelanggan
    Dim pilih As String
    Dim kode As String
    Private Sub fcaricust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub
    Sub tabel()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_pelanggan"

        GridColumn2.Caption = "Nama Pelanggan"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "id"
        GridColumn3.FieldName = "id"
        GridColumn3.Visible = False

        GridControl1.Visible = True
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcus = 1 Then
            fpricelist.cmbpelanggan.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        ElseIf tutupcus = 2 Then
            fpenjualan.cmbpelanggan.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        ElseIf tutupcus = 3 Then
            fbarangkeluar.cmbpelanggan.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        ElseIf tutupcus = 4 Then
            flaporanpricelist.cmbpelanggan.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class