Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fnotifikasistok
    Private Sub fnotifikasistok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call tabel_stok()

        'With GridView1
        '    .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
        '    'buat sum harga
        '    .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
        '    .Columns("bayar_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_penjualan", "{0:n0}")
        '    .Columns("sisa_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_penjualan", "{0:n0}")
        'End With
    End Sub

    Sub grid_stok()
        GridColumn1.Caption = "Kode Stok"
        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Width = 30

        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Width = 30

        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Width = 50

        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"
        GridColumn4.Width = 15

        GridColumn5.Caption = "Satuan"
        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Width = 15

        GridColumn6.Caption = "Jumlah Stok"
        GridColumn6.FieldName = "jumlah_stok"
        GridColumn6.Width = 10
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"

        GridControl1.Visible = True

    End Sub
    Sub tabel_stok()
        Call koneksii()
        'Using cnn As New OdbcConnection(strConn)
        sql = "SELECT kode_stok, tb_stok.kode_barang, nama_barang, jenis_barang, satuan_barang, tb_stok.jumlah_stok FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE tb_stok.jumlah_stok < 20"
        da = New OdbcDataAdapter(sql, cnn)
        'cnn.Open()
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid_stok()
        'cnn.Close()
        'End Using
    End Sub

    Private Sub fnotifikasistok_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_stok()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Call tabel_stok()

    End Sub
End Class