Imports System.Data.Odbc

Public Class fcaripenjualan
    Dim pilih As String
    Dim kode As String
    Private Sub fcaripenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub
    Sub grid()
        GridColumn1.Caption = "id"
        GridColumn1.FieldName = "id"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_penjualan"
        GridColumn3.Caption = "Nama Pelanggan"
        GridColumn3.FieldName = "nama_pelanggan"
        GridColumn4.Caption = "Total Penjualan"
        GridColumn4.FieldName = "total_penjualan"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "##,#0"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT tb_penjualan.id, tb_penjualan.total_penjualan, tb_penjualan.tgl_penjualan, tb_pelanggan.nama_pelanggan FROM tb_penjualan JOIN tb_pelanggan ON tb_penjualan.pelanggan_id = tb_pelanggan.id WHERE DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT tb_penjualan.id, tb_penjualan.total_penjualan, tb_penjualan.tgl_penjualan, tb_pelanggan.nama_pelanggan FROM tb_penjualan JOIN tb_pelanggan ON tb_penjualan.pelanggan_id = tb_pelanggan.id WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
            End If
        Else
            sql = "SELECT tb_penjualan.id, tb_penjualan.total_penjualan, tb_penjualan.tgl_penjualan, tb_pelanggan.nama_pelanggan FROM tb_penjualan JOIN tb_pelanggan ON tb_penjualan.pelanggan_id = tb_pelanggan.id"
        End If


        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupjual = 1 Then
            freturjual.txtnonota.Text = Me.GridView1.GetFocusedRowCellValue("id")
        ElseIf tutupjual = 2 Then
            fpenjualan.txtgopenjualan.Text = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub

    Private Sub cbperiode_CheckedChanged(sender As Object, e As EventArgs) Handles cbperiode.CheckedChanged
        If cbperiode.Checked = True Then
            dtawal.Enabled = True
            dtakhir.Enabled = True
        Else
            dtawal.Enabled = False
            dtakhir.Enabled = False
        End If
    End Sub
End Class