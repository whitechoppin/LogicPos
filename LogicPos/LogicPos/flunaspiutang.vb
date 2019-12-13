Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class flunaspiutang
    Public tabel1, tabel2 As DataTable

    Private Sub flunaspiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("Bayar").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Bayar", "{0:n0}")
        End With
    End Sub

    Sub tabel_utama()
        tabel1 = New DataTable

        With tabel1
            .Columns.Add("kode_penjualan")
            .Columns.Add("kode_costumer")
            .Columns.Add("kode_gudang")
            .Columns.Add("kode_user")
            .Columns.Add("tgl_penjualan")
            .Columns.Add("tgl_jatuhtempo_penjualan")
            .Columns.Add("diskon_penjualan", GetType(Double))
            .Columns.Add("pajak_penjualan", GetType(Double))
            .Columns.Add("ongkir_penjualan", GetType(Double))
            .Columns.Add("total_penjualan", GetType(Double))
            .Columns.Add("bayar_penjualan", GetType(Double))
            .Columns.Add("sisa_penjualan", GetType(Double))
        End With

        GridControl1.DataSource = tabel1

        GridColumn1.FieldName = "kode_penjualan"
        GridColumn1.Caption = "Kode Penjualan"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "kode_costumer"
        GridColumn2.Caption = "Kode Costumer"
        GridColumn2.Width = 20

        GridColumn3.FieldName = "kode_gudang"
        GridColumn3.Caption = "Kode Gudang"
        GridColumn3.Width = 20

        GridColumn4.FieldName = "kode_user"
        GridColumn4.Caption = "Kode User"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "tgl_penjualan"
        GridColumn5.Caption = "Tgl Penjualan"
        GridColumn5.Width = 20

        GridColumn6.FieldName = "tgl_jatuhtempo_penjualan"
        GridColumn6.Caption = "Tgl Jatuh Tempo"
        GridColumn6.Width = 20

        GridColumn7.FieldName = "diskon_penjualan"
        GridColumn7.Caption = "Diskon"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 20

        GridColumn8.FieldName = "pajak_penjualan"
        GridColumn8.Caption = "Pajak"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 20

        GridColumn9.FieldName = "ongkir_penjualan"
        GridColumn9.Caption = "Ongkir"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "total_penjualan"
        GridColumn10.Caption = "Total"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

        GridColumn11.FieldName = "bayar_penjualan"
        GridColumn11.Caption = "Bayar"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:n0}"
        GridColumn11.Width = 30

        GridColumn12.FieldName = "sisa_penjualan"
        GridColumn12.Caption = "Sisa"
        GridColumn12.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn12.DisplayFormat.FormatString = "{0:n0}"
        GridColumn12.Width = 30

    End Sub
    Sub tabel_lunas()
        tabel2 = New DataTable

        With tabel2
            .Columns.Add("kode_lunas")
            .Columns.Add("kode_penjualan")
            .Columns.Add("tanggal_transaksi")
            .Columns.Add("kode_user")
            .Columns.Add("kode_kas")
            .Columns.Add("bayar_kas", GetType(Double))

        End With

        GridControl2.DataSource = tabel2

        GridColumn14.FieldName = "kode_barang"
        GridColumn14.Caption = "Kode Barang"
        GridColumn14.Width = 20

        GridColumn15.FieldName = "kode_stok"
        GridColumn15.Caption = "Kode Stok"
        GridColumn15.Width = 20

        GridColumn16.FieldName = "nama_barang"
        GridColumn16.Caption = "Nama Barang"
        GridColumn16.Width = 70

        GridColumn17.FieldName = "banyak"
        GridColumn17.Caption = "banyak"
        GridColumn17.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn17.DisplayFormat.FormatString = "{0:n0}"
        GridColumn17.Width = 5

        GridColumn18.FieldName = "satuan_barang"
        GridColumn18.Caption = "Satuan Barang"
        GridColumn18.Width = 10

    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtcustomer.Clear()
    End Sub
    Sub loadingpenjualan(lihat As String)
        Call tabel_utama()
        Call tabel_lunas()
        sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_penjualan"), dr("kode_pelanggan"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")))
            'tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), dr("diskon"), 0, dr("harga_diskon"), dr("subtotal"), 0, 0)
            GridControl1.RefreshDataSource()
        End While

    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click

    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click

    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick

    End Sub
End Class