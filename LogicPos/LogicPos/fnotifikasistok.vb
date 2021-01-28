Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fnotifikasistok
    Private Sub fnotifikasistok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksi()
        Call grid_stok()
    End Sub

    Sub grid_stok()
        Call koneksi()
        sql = "SELECT kode_stok, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.jenis_barang, tb_barang.satuan_barang, tb_stok.jumlah_stok, tb_gudang.nama_gudang FROM tb_stok JOIN tb_barang ON tb_barang.id = tb_stok.barang_id JOIN tb_gudang ON tb_gudang.id = tb_stok.gudang_id WHERE tb_stok.jumlah_stok < 20"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridColumn1.Caption = "Kode Stok"
        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Width = 10

        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Width = 10

        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Width = 30

        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"
        GridColumn4.Width = 5

        GridColumn5.Caption = "Satuan"
        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Width = 5

        GridColumn6.Caption = "Jumlah Stok"
        GridColumn6.FieldName = "jumlah_stok"
        GridColumn6.Width = 5
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"

        GridColumn7.Caption = "Gudang"
        GridColumn7.FieldName = "nama_gudang"
        GridColumn7.Width = 10

        GridControl1.Visible = True
    End Sub

    Private Sub fnotifikasistok_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call grid_stok()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Call grid_stok()
    End Sub
End Class