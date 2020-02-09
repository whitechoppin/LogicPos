Imports System.Data.Odbc

Public Class fjatuhtempopenjualan
    Private Sub fjatuhtempopenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call tabel_penjualan()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
            .Columns("bayar_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_penjualan", "{0:n0}")
            .Columns("sisa_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_penjualan", "{0:n0}")

        End With
    End Sub

    Sub grid_penjualan()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_penjualan"

        GridColumn2.Caption = "Pelanggan"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "Tanggal Penjualan"
        GridColumn3.FieldName = "tgl_penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_penjualan"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total Penjualan"
        GridColumn5.FieldName = "total_penjualan"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

        GridColumn6.Caption = "Bayar"
        GridColumn6.FieldName = "bayar_penjualan"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Sisa"
        GridColumn7.FieldName = "sisa_penjualan"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

    End Sub
    Sub tabel_penjualan()
        Call koneksii()

        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan = tb_penjualan.kode_pelanggan WHERE tb_penjualan.lunas_penjualan = 0 AND tb_penjualan.tgl_jatuhtempo_penjualan < now()"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid_penjualan()
            cnn.Close()
        End Using
    End Sub
End Class