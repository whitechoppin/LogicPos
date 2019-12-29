Imports System.Data.Odbc

Public Class fcaripenjualan
    Dim pilih As String
    Dim kode As String
    Private Sub fcaripenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_penjualan"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_penjualan"
        GridColumn3.Caption = "Nama Pelanggan"
        GridColumn3.FieldName = "nama_pelanggan"
        GridColumn4.Caption = "Total Penjualan"
        GridColumn4.FieldName = "total_penjualan"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "Rp ##,#0"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT tb_penjualan.kode_penjualan, tb_penjualan.total_penjualan, tb_penjualan.tgl_penjualan FROM tb_penjualan"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupjual = 1 Then
            freturjual.txtnonota.Text = Me.GridView1.GetFocusedRowCellValue("kode_penjualan")
        End If
        Me.Hide()
    End Sub
End Class