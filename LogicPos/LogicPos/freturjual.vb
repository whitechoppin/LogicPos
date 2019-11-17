Imports System.Data.Odbc
Imports DevExpress.Utils
Public Class freturjual
    Dim tabel1, tabel2 As DataTable
    'variabel dalam penjualan
    Dim jenis, satuan, kodepenjualan As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    'Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran As String
    'Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    'Dim viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double

    'variabel edit penjualan
    'Dim countingbarang As Integer
    Private Sub freturjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        'kodepenjualan = currentnumber()
        'Call inisialisasi(kodepenjualan)
        Call awalbaru()
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With
        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With
    End Sub
    Sub awalbaru()
        'header
        txtnonota.Clear()
        txtnonota.Enabled = True
        btncarinota.Enabled = True

        txtcustomer.Enabled = False
        txtcustomer.Clear()

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        'buat tabel
        Call tabel_utama()
        Call tabel_retur()
    End Sub
    Sub tabel_utama()
        tabel1 = New DataTable

        With tabel1
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))

        End With

        GridControl1.DataSource = tabel1

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "kode_stok"
        GridColumn2.Caption = "Kode Stok"
        GridColumn2.Width = 20

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 70

        GridColumn4.FieldName = "banyak"
        GridColumn4.Caption = "banyak"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        GridColumn4.Width = 5

        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn5.Width = 10

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 10

        GridColumn7.FieldName = "harga_satuan"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 20

        GridColumn8.FieldName = "diskon_persen"
        GridColumn8.Caption = "Diskon %"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 20

        GridColumn9.FieldName = "diskon_nominal"
        GridColumn9.Caption = "Diskon Nominal"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "harga_diskon"
        GridColumn10.Caption = "Harga Diskon"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

        GridColumn11.FieldName = "subtotal"
        GridColumn11.Caption = "Subtotal"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:n0}"
        GridColumn11.Width = 30

        GridColumn12.FieldName = "laba"
        GridColumn12.Caption = "Laba"
        GridColumn12.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn12.DisplayFormat.FormatString = "{0:n0}"
        GridColumn12.Width = 20
        GridColumn12.Visible = False

        GridColumn13.FieldName = "modal_barang"
        GridColumn13.Caption = "Modal Barang"
        GridColumn13.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn13.DisplayFormat.FormatString = "{0:n0}"
        GridColumn13.Width = 20
        GridColumn13.Visible = False

    End Sub
    Sub tabel_retur()
        tabel2 = New DataTable

        With tabel2
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))

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

        GridColumn19.FieldName = "jenis_barang"
        GridColumn19.Caption = "Jenis Barang"
        GridColumn19.Width = 10

        GridColumn20.FieldName = "harga_satuan"
        GridColumn20.Caption = "Harga Satuan"
        GridColumn20.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn20.DisplayFormat.FormatString = "{0:n0}"
        GridColumn20.Width = 20

        GridColumn21.FieldName = "diskon_persen"
        GridColumn21.Caption = "Diskon %"
        GridColumn21.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn21.DisplayFormat.FormatString = "{0:n0}"
        GridColumn21.Width = 20

        GridColumn22.FieldName = "diskon_nominal"
        GridColumn22.Caption = "Diskon Nominal"
        GridColumn22.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn22.DisplayFormat.FormatString = "{0:n0}"
        GridColumn22.Width = 30

        GridColumn23.FieldName = "harga_diskon"
        GridColumn23.Caption = "Harga Diskon"
        GridColumn23.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn23.DisplayFormat.FormatString = "{0:n0}"
        GridColumn23.Width = 30

        GridColumn24.FieldName = "subtotal"
        GridColumn24.Caption = "Subtotal"
        GridColumn24.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn24.DisplayFormat.FormatString = "{0:n0}"
        GridColumn24.Width = 30

        GridColumn25.FieldName = "laba"
        GridColumn25.Caption = "Laba"
        GridColumn25.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn25.DisplayFormat.FormatString = "{0:n0}"
        GridColumn25.Width = 20
        GridColumn25.Visible = False

        GridColumn26.FieldName = "modal_barang"
        GridColumn26.Caption = "Modal Barang"
        GridColumn26.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn26.DisplayFormat.FormatString = "{0:n0}"
        GridColumn26.Width = 20
        GridColumn26.Visible = False
    End Sub
    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtcustomer.Clear()
    End Sub
    Sub cari_nota()
        Call koneksii()
        sql = "Select * From tb_penjualan Join tb_pelanggan On tb_pelanggan.kode_pelanggan=tb_penjualan.kode_pelanggan where tb_penjualan.kode_penjualan = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            'jika ditemukan
            txtcustomer.Text = dr("nama_pelanggan")

            sql = "SELECT * FROM tb_penjualan_detail WHERE kode_penjualan='" & txtnonota.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            While dr.Read
                tabel1.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("harga_jual"), dr("diskon"), 0, dr("harga_diskon"), dr("subtotal"), dr("keuntungan"), dr("modal"))
                GridControl1.RefreshDataSource()
            End While
        Else
            'jika tidak ditemukan
            txtcustomer.Text = ""
        End If

    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call cari_nota()
    End Sub
End Class