Imports System.Data.Odbc
Imports DevExpress.Utils
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Drawing.Drawing2D
Imports ZXing

Public Class fpenjualan
    Public namaform As String = "transaksi-penjualan"

    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tinggi As Integer
    Public tabel, tabelsementara As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Dim lunasstatus As Integer = 0

    Dim dateterm, datetermnow As Date
    'variabel penjualan detail
    Dim idpenjualan, idgudang, iduser, idpelanggan As String
    Public jenis, satuan As String
    Dim idbarang, idstok As Integer

    Dim diskonpersennilai, diskonnominalnilai As Double
    Dim term, banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalbarang, selisihkategori, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorpelanggan, nomorsales, nomorgudang, viewketerangan, viewnamaexpedisi, viewalamatexpedisi, viewpembayaran, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewterm As Integer
    Dim viewtglpenjualan, viewtgljatuhtempo, viewtglcreated, viewtglupdated As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    Dim rpt_faktur As New ReportDocument

    'variabel edit penjualan
    Dim countingbarang As Integer


    '==== autosize form ====
    Dim CuRWidth As Integer = Me.Width
    Dim CuRHeight As Integer = Me.Height

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim RatioHeight As Double = (Me.Height - CuRHeight) / CuRHeight
        Dim RatioWidth As Double = (Me.Width - CuRWidth) / CuRWidth

        For Each ctrl As Control In Controls
            ctrl.Width += ctrl.Width * RatioWidth
            ctrl.Left += ctrl.Left * RatioWidth
            ctrl.Top += ctrl.Top * RatioHeight
            ctrl.Height += ctrl.Height * RatioHeight
        Next

        CuRHeight = Me.Height
        CuRWidth = Me.Width
    End Sub

    '=======================

    Private Sub fpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idpenjualan = currentnumber()
        Call inisialisasi(idpenjualan)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With

        Select Case kodeakses
            Case 1
                tambahstatus = True
                editstatus = False
                printstatus = False
            Case 3
                tambahstatus = False
                editstatus = True
                printstatus = False
            Case 5
                tambahstatus = False
                editstatus = False
                printstatus = True
            Case 4
                tambahstatus = True
                editstatus = True
                printstatus = False
            Case 6
                tambahstatus = True
                editstatus = False
                printstatus = True
            Case 8
                tambahstatus = False
                editstatus = True
                printstatus = True
            Case 9
                tambahstatus = True
                editstatus = True
                printstatus = True
        End Select

        Call historysave("Membuka Transaksi Penjualan", "N/A", namaform)
    End Sub

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_penjualan ORDER BY id DESC LIMIT 1;"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Return dr.Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        End Try
        Return pesan
    End Function
    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT id FROM tb_penjualan WHERE date_created < (SELECT date_created FROM tb_penjualan WHERE id = '" & previousnumber & "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Call inisialisasi(dr.Item(0).ToString)
                hitnumber = 0
            Else
                If hitnumber <= 2 Then
                    Call inisialisasi(previousnumber)
                    hitnumber = hitnumber + 1
                Else
                    MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
                End If
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT id FROM tb_penjualan WHERE date_created > (SELECT date_created FROM tb_penjualan WHERE id = '" & nextingnumber & "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Call inisialisasi(dr.Item(0).ToString)
                hitnumber = 0
            Else
                If hitnumber <= 2 Then
                    Call inisialisasi(nextingnumber)
                    hitnumber = hitnumber + 1
                Else
                    MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
                End If
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        End Try
    End Sub
    Sub previewpenjualan(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan_detail WHERE penjualan_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), Val(dr("qty")), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")), dr("barang_id"), dr("stok_id"))
            tabelsementara.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), Val(dr("qty")), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()
    End Sub
    Sub comboboxpelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbpelanggan.DataSource = ds.Tables(0)
        cmbpelanggan.ValueMember = "id"
        cmbpelanggan.DisplayMember = "kode_pelanggan"
    End Sub
    Sub comboboxgudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbgudang.DataSource = ds.Tables(0)
        cmbgudang.ValueMember = "id"
        cmbgudang.DisplayMember = "kode_gudang"
    End Sub
    Sub comboboxuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsales.DataSource = ds.Tables(0)
        cmbsales.ValueMember = "id"
        cmbsales.DisplayMember = "kode_user"
    End Sub
    Sub comboboxpembayaran()
        Call koneksii()
        sql = "SELECT * FROM tb_kas ORDER BY kode_kas DESC"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbpembayaran.DataSource = ds.Tables(0)
        cmbpembayaran.ValueMember = "id"
        cmbpembayaran.DisplayMember = "kode_kas"
    End Sub
    Sub awalbaru()
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = False
        btnsimpan.Enabled = True
        btnprint.Enabled = False
        btnedit.Enabled = False
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgopenjualan.Enabled = False
        btncaripenjualan.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxpelanggan()
        Call comboboxuser()
        Call comboboxgudang()
        Call comboboxpembayaran()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbpelanggan.Enabled = True
        cmbpelanggan.SelectedIndex = -1
        cmbpelanggan.Focus()
        btncaricustomer.Enabled = True

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True

        cblunas.Checked = False
        cbvoid.Checked = False
        cbprinted.Checked = False
        cbposted.Checked = False

        dtpenjualan.Enabled = True
        dtpenjualan.Value = Date.Now

        txtterm.Clear()
        txtterm.Text = 0
        txtterm.Enabled = True

        dtjatuhtempo.Enabled = False
        dtjatuhtempo.Value = dtpenjualan.Value

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = True

        txtharga.Clear()
        txtharga.Text = 0

        lblsatuan.Text = "satuan"
        lblsatuanjual.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        txtnamaexpedisi.Enabled = True
        txtnamaexpedisi.Clear()

        txtalamatexpedisi.Enabled = True
        txtalamatexpedisi.Clear()

        cbongkir.Enabled = True
        cbppn.Enabled = True
        cbdiskon.Enabled = True

        cbongkir.Checked = False
        cbppn.Checked = False
        cbdiskon.Checked = False

        txtongkir.Enabled = False
        txtppnpersen.Enabled = False
        txtppnnominal.Enabled = False
        txtdiskonpersen.Enabled = False
        txtdiskonnominal.Enabled = False

        txtdiskonpersen.Clear()
        txtdiskonpersen.Text = 0
        txtdiskonnominal.Clear()
        txtdiskonnominal.Text = 0
        txtppnpersen.Clear()
        txtppnpersen.Text = 0
        txtppnnominal.Clear()
        txtppnnominal.Text = 0
        txtongkir.Clear()
        txtongkir.Text = 0
        txttotal.Clear()
        txttotal.Text = 0

        cmbpembayaran.SelectedIndex = -1
        cmbpembayaran.Enabled = True
        btncarikas.Enabled = True

        btnbayarfull.Enabled = True
        btnbayarfull.Text = "Bayar"

        txtbayar.Enabled = True
        txtbayar.Clear()
        txtbayar.Text = 0
        txtsisa.Clear()
        txtsisa.Text = 0

        dtcreated.Value = Now
        dtupdated.Value = Now

        'buat tabel
        Call tabel_utama()

    End Sub
    Sub inisialisasi(nomorkode As Integer)
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = True
        btnsimpan.Enabled = False
        btnprint.Enabled = True
        btnedit.Enabled = True
        btnbatal.Enabled = False

        'button navigations
        btnprev.Enabled = True
        btngo.Enabled = True
        txtgopenjualan.Enabled = True
        btncaripenjualan.Enabled = True
        btnnext.Enabled = True

        rbfaktur.Checked = False
        rbstruk.Checked = True

        'isi combo box
        Call comboboxpelanggan()
        Call comboboxuser()
        Call comboboxgudang()
        Call comboboxpembayaran()

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbpelanggan.Enabled = False
        btncaricustomer.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False

        dtpenjualan.Enabled = False
        dtpenjualan.Value = Date.Now

        txtterm.Clear()
        txtterm.Text = 0
        txtterm.Enabled = False

        dtjatuhtempo.Enabled = False
        dtjatuhtempo.Value = dtpenjualan.Value

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = False
        btncaribarang.Enabled = False

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = False

        txtharga.Clear()
        txtharga.Text = 0

        lblsatuan.Text = "satuan"
        lblsatuanjual.Text = "satuan"

        btntambah.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        txtnamaexpedisi.Enabled = False
        txtnamaexpedisi.Clear()

        txtalamatexpedisi.Enabled = False
        txtalamatexpedisi.Clear()

        cbongkir.Enabled = False
        cbppn.Enabled = False
        cbdiskon.Enabled = False

        cbongkir.Checked = False
        cbppn.Checked = False
        cbdiskon.Checked = False

        txtongkir.Enabled = False
        txtppnpersen.Enabled = False
        txtppnnominal.Enabled = False
        txtdiskonpersen.Enabled = False
        txtdiskonnominal.Enabled = False

        txtdiskonpersen.Clear()
        txtdiskonpersen.Text = 0
        txtdiskonnominal.Clear()
        txtdiskonnominal.Text = 0
        txtppnpersen.Clear()
        txtppnpersen.Text = 0
        txtppnnominal.Clear()
        txtppnnominal.Text = 0
        txtongkir.Clear()
        txtongkir.Text = 0

        txttotal.Clear()
        txttotal.Text = 0

        cmbpembayaran.Enabled = False
        btncarikas.Enabled = False

        btnbayarfull.Enabled = False
        txtbayar.Clear()
        txtbayar.Text = 0
        txtbayar.Enabled = False
        txtsisa.Clear()
        txtsisa.Text = 0

        If nomorkode > 0 Then
            Call koneksii()
            sql = "SELECT * FROM tb_penjualan WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorpelanggan = dr("pelanggan_id")
                nomorsales = dr("user_id")
                nomorgudang = dr("gudang_id")

                statuslunas = dr("lunas_penjualan")
                statusvoid = dr("void_penjualan")
                statusprint = dr("print_penjualan")
                statusposted = dr("posted_penjualan")

                viewtglpenjualan = dr("tgl_penjualan")
                viewterm = dr("term_penjualan")
                viewtgljatuhtempo = dr("tgl_jatuhtempo_penjualan")

                viewtglcreated = dr("date_created")
                viewtglupdated = dr("last_updated")

                viewketerangan = dr("keterangan_penjualan")

                viewnamaexpedisi = dr("nama_expedisi")
                viewalamatexpedisi = dr("alamat_expedisi")

                viewpembayaran = dr("metode_pembayaran")

                nilaidiskon = dr("diskon_penjualan")
                nilaippn = dr("pajak_penjualan")
                nilaiongkir = dr("ongkir_penjualan")
                nilaibayar = dr("bayar_penjualan")

                txtnonota.Text = nomornota
                cmbpelanggan.SelectedValue = nomorpelanggan
                cmbsales.SelectedValue = nomorsales
                cmbgudang.SelectedValue = nomorgudang

                cblunas.Checked = statuslunas
                cbvoid.Checked = statusvoid
                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dtpenjualan.Value = viewtglpenjualan
                txtterm.Text = viewterm
                dtjatuhtempo.Value = viewtgljatuhtempo

                dtcreated.Value = viewtglcreated
                dtupdated.Value = viewtglupdated

                'isi tabel view pembelian

                Call previewpenjualan(nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan
                txtnamaexpedisi.Text = viewnamaexpedisi
                txtalamatexpedisi.Text = viewalamatexpedisi

                If nilaidiskon <> 0 Then
                    cbdiskon.Checked = True
                    txtdiskonpersen.Text = nilaidiskon
                Else
                    cbdiskon.Checked = False
                    txtdiskonpersen.Text = 0
                End If

                If nilaippn <> 0 Then
                    cbppn.Checked = True
                    txtppnpersen.Text = nilaippn
                Else
                    cbppn.Checked = False
                    txtppnpersen.Text = 0
                End If

                If nilaiongkir <> 0 Then
                    cbongkir.Checked = True
                    txtongkir.Text = nilaiongkir
                Else
                    cbongkir.Checked = False
                    txtongkir.Text = 0
                End If

                txtongkir.Enabled = False
                txtppnpersen.Enabled = False
                txtdiskonpersen.Enabled = False

                cmbpembayaran.Text = viewpembayaran
                txtbayar.Text = nilaibayar
            End If
        Else
            txtnonota.Clear()
            cmbpelanggan.SelectedIndex = -1
            cmbsales.SelectedIndex = -1
            cmbgudang.SelectedIndex = -1
            cblunas.Checked = False
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            dtpenjualan.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            txtketerangan.Text = ""

            txtnamaexpedisi.Text = ""
            txtalamatexpedisi.Text = ""

            cbdiskon.Checked = False
            txtdiskonpersen.Text = 0

            cbppn.Checked = False
            txtppnpersen.Text = 0

            cbongkir.Checked = False
            txtongkir.Text = 0

            txtongkir.Enabled = False
            txtppnpersen.Enabled = False
            txtdiskonpersen.Enabled = False

            cmbpembayaran.SelectedIndex = -1
        End If

    End Sub
    Sub awaledit()
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = False
        btnsimpan.Enabled = False
        btnprint.Enabled = False
        btnedit.Enabled = True
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgopenjualan.Enabled = False
        btncaripenjualan.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Enabled = False

        cmbpelanggan.Enabled = True
        cmbpelanggan.Focus()
        btncaricustomer.Enabled = True

        cmbsales.Enabled = True
        cmbgudang.Enabled = True
        btncarigudang.Enabled = True

        dtpenjualan.Enabled = True
        'dtpenjualan.Value = Date.Now

        txtterm.Enabled = True

        dtjatuhtempo.Enabled = False
        'dtjatuhtempo.Value = Date.Now

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = True

        txtharga.Clear()
        txtharga.Text = 0

        lblsatuan.Text = "satuan"
        lblsatuanjual.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        'txtketerangan.Clear()
        txtnamaexpedisi.Enabled = True
        txtalamatexpedisi.Enabled = True

        cbongkir.Enabled = True
        cbppn.Enabled = True
        cbdiskon.Enabled = True

        txtongkir.Enabled = False
        txtppnpersen.Enabled = False
        txtppnnominal.Enabled = False
        txtdiskonpersen.Enabled = False
        txtdiskonnominal.Enabled = False

        btnbayarfull.Enabled = True
        'cmbpembayaran.SelectedIndex = -1
        cmbpembayaran.Enabled = True
        btncarikas.Enabled = True
        txtbayar.Enabled = True
    End Sub

    Sub tabel_utama()
        tabel = New DataTable
        With tabel
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        tabelsementara = New DataTable
        With tabelsementara
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 5

        GridColumn2.FieldName = "kode_stok"
        GridColumn2.Caption = "Kode Stok"
        GridColumn2.Width = 5

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 35

        GridColumn4.FieldName = "qty"
        GridColumn4.Caption = "Qty"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        GridColumn4.Width = 5

        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn5.Width = 5

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 5

        GridColumn7.FieldName = "harga_satuan"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 10

        GridColumn8.FieldName = "diskon_persen"
        GridColumn8.Caption = "Diskon %"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n3}"
        GridColumn8.Width = 5

        GridColumn9.FieldName = "diskon_nominal"
        GridColumn9.Caption = "Diskon Nominal"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 10

        GridColumn10.FieldName = "harga_diskon"
        GridColumn10.Caption = "Harga Diskon"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 15

        GridColumn11.FieldName = "subtotal"
        GridColumn11.Caption = "Subtotal"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:n0}"
        GridColumn11.Width = 15

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

        GridColumn14.FieldName = "barang_id"
        GridColumn14.Caption = "Barang id"
        GridColumn14.Width = 15
        GridColumn14.Visible = False

        GridColumn15.FieldName = "stok_id"
        GridColumn15.Caption = "stok id"
        GridColumn15.Width = 15
        GridColumn15.Visible = False
    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodestok.Clear()
        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyak.Clear()
        txtharga.Text = 0
        txtkodestok.Focus()
    End Sub

    Sub caripelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbpelanggan.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idpelanggan = dr("id")
            txtpelanggan.Text = dr("nama_pelanggan")
            txtalamat.Text = dr("alamat_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
        Else

            txtpelanggan.Text = ""
            txtalamat.Text = ""
            txttelp.Text = ""
        End If
    End Sub

    Sub carigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang ='" & cmbgudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idgudang = dr("id")
            txtgudang.Text = dr("nama_gudang")
        Else
            txtgudang.Text = ""
        End If
    End Sub

    Sub carisales()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = dr("id")
        End If
    End Sub

    Sub caripembayaran()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas ='" & cmbpembayaran.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtrekening.Text = dr("nama_kas")
        Else
            txtrekening.Text = ""
        End If
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call carisales()
    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call carisales()
    End Sub

    Private Sub cmbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.SelectedIndexChanged
        txtkodestok.Clear()
        Call caripelanggan()
    End Sub

    Private Sub cmbcustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.TextChanged
        txtkodestok.Clear()
        Call caripelanggan()
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        txtkodestok.Clear()
        Call carigudang()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        txtkodestok.Clear()
        Call carigudang()
    End Sub

    Private Sub cmbpembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpembayaran.SelectedIndexChanged
        Call caripembayaran()
    End Sub

    Private Sub cmbpembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmbpembayaran.TextChanged
        Call caripembayaran()
    End Sub
    Private Sub ritehargasatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritehargasatuan.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub ritediskonpersen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritediskonpersen.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub ritediskonnominal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritediskonnominal.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Sub caristok()
        Call koneksii()
        sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang, tb_price_group.harga_jual, tb_barang.modal_barang FROM tb_stok JOIN tb_barang ON tb_stok.barang_id = tb_barang.id JOIN tb_price_group ON tb_barang.id = tb_price_group.barang_id WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.pelanggan_id ='" & idpelanggan & "' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idstok = Val(dr("idstok"))
            idbarang = Val(dr("idbarang"))

            txtkodebarang.Text = dr("kode_barang")
            txtnamabarang.Text = dr("nama_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            lblsatuanjual.Text = satuan
            jenis = dr("jenis_barang")
            txtharga.Text = Format(dr("harga_jual"), "##,##0")
            txtharga.SelectionStart = Len(txtharga.Text)
            modalbarang = Val(dr("modal_barang"))
        Else
            Call koneksii()
            sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang, tb_price_group.harga_jual, tb_barang.modal_barang FROM tb_stok JOIN tb_barang ON tb_stok.barang_id = tb_barang.id JOIN tb_price_group ON tb_barang.id = tb_price_group.barang_id WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.pelanggan_id ='1' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                idstok = Val(dr("idstok"))
                idbarang = Val(dr("idbarang"))

                txtkodebarang.Text = dr("kode_barang")
                txtnamabarang.Text = dr("nama_barang")
                satuan = dr("satuan_barang")
                lblsatuan.Text = satuan
                lblsatuanjual.Text = satuan
                jenis = dr("jenis_barang")
                txtharga.Text = Format(dr("harga_jual"), "##,##0")
                txtharga.SelectionStart = Len(txtharga.Text)
                modalbarang = Val(dr("modal_barang"))
            Else
                Call koneksii()
                sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang, tb_barang.modal_barang, tb_kategori_barang.selisih_kategori FROM tb_stok JOIN tb_barang ON tb_barang.id = tb_stok.barang_id JOIN tb_kategori_barang ON tb_barang.kategori_barang_id = tb_kategori_barang.id WHERE kode_stok = '" & txtkodestok.Text & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    selisihkategori = Val(dr("selisih_kategori"))
                    modalbarang = Val(dr("modal_barang"))

                    idstok = Val(dr("idstok"))
                    idbarang = Val(dr("idbarang"))

                    txtkodebarang.Text = dr("kode_barang")
                    txtnamabarang.Text = dr("nama_barang")
                    satuan = dr("satuan_barang")
                    lblsatuan.Text = satuan
                    lblsatuanjual.Text = satuan
                    jenis = dr("jenis_barang")

                    If selisihkategori.Equals(0) Then
                        txtharga.Text = Format(Val(dr("modal_barang")) + selisihkategori, "##,##0")
                        txtharga.SelectionStart = Len(txtharga.Text)

                        MsgBox("Harga Merupakan Modal Karena Selisih Kategori Rp.0 dan Tidak ada Data Pricelist !!!")
                    Else
                        txtharga.Text = Format(Val(dr("modal_barang")) + selisihkategori, "##,##0")
                        txtharga.SelectionStart = Len(txtharga.Text)
                    End If
                Else
                    idstok = 0
                    idbarang = 0

                    txtkodebarang.Text = ""
                    txtnamabarang.Text = ""
                    satuan = "satuan"
                    lblsatuan.Text = satuan
                    lblsatuanjual.Text = satuan
                    jenis = ""
                    txtharga.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then
            If txtpelanggan.Text IsNot "" Then
                If txtgudang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        If txtrekening.Text IsNot "" Then
                            Call proses()
                        Else
                            MsgBox("Isi Pembayaran")
                        End If
                    Else
                        MsgBox("Isi Sales")
                    End If
                Else
                    MsgBox("Isi Gudang")
                End If
            Else
                MsgBox("Isi Customer")
            End If
        Else
            MsgBox("Keranjang Masih Kosong")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            If cbvoid.Checked = False Then

                If cekcetakan(txtnonota.Text, namaform).Equals(True) Then
                    statusizincetak = False
                    passwordid = 7
                    fpassword.kodetabel = txtnonota.Text
                    fpassword.ShowDialog()
                    If statusizincetak.Equals(True) Then
                        Call koneksii()
                        If rbstruk.Checked Then
                            Call PrintTransaksi()

                            sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE id = '" & txtnonota.Text & "' "
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            cbprinted.Checked = True
                        Else
                            If rbfaktur.Checked Then
                                Call cetak_faktur()

                                sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE id = '" & txtnonota.Text & "' "
                                cmmd = New OdbcCommand(sql, cnn)
                                dr = cmmd.ExecuteReader()

                                cbprinted.Checked = True
                            Else
                                If rbsurat.Checked Then
                                    Call cetak_surat()

                                    sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE id = '" & txtnonota.Text & "' "
                                    cmmd = New OdbcCommand(sql, cnn)
                                    dr = cmmd.ExecuteReader()

                                    cbprinted.Checked = True
                                End If
                            End If
                        End If
                    End If
                Else
                    Call koneksii()
                    If rbstruk.Checked Then
                        'Call cetak_struk()
                        Call PrintTransaksi()

                        sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE id = '" & txtnonota.Text & "' "
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        cbprinted.Checked = True
                    Else
                        If rbfaktur.Checked Then
                            Call cetak_faktur()

                            sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE id = '" & txtnonota.Text & "' "
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            cbprinted.Checked = True
                        Else
                            If rbsurat.Checked Then
                                Call cetak_surat()

                                sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE id = '" & txtnonota.Text & "' "
                                cmmd = New OdbcCommand(sql, cnn)
                                dr = cmmd.ExecuteReader()

                                cbprinted.Checked = True
                            End If
                        End If
                    End If
                End If

                'history user ==========
                Call historysave("Mencetak Data Penjualan Kode " + txtnonota.Text, txtnonota.Text, namaform)
                '========================
            Else
                MsgBox("Nota sudah void !")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub PrintTransaksi()
        Dim struk As String
        Call koneksii()
        sql = "SELECT * FROM tb_printer WHERE nomor='1'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            struk = dr("nama_printer")
        Else
            struk = ""
        End If
        tinggi = 0
        With Me.PrintDocument
            .PrinterSettings.PrinterName = struk
            .PrinterSettings.DefaultPageSettings.Landscape = False
            .Print()
        End With
    End Sub

    Private Function ResizeGambar(ByVal gmb As Image,
    ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = gmb.Width
            Dim originalHeight As Integer = gmb.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(gmb, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage
        'ambil data alamat
        Dim nama, alamat, telp, rekening As String
        Dim countnama, countalamat, counttelp, countrekening As Integer
        Dim fotostruk As Byte()
        Dim resized, barcode As Image
        Dim writer As New BarcodeWriter

        Call koneksii()
        sql = "SELECT * FROM tb_info_perusahaan LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            nama = dr("nama")
            alamat = dr("alamat")
            telp = dr("telepon")
            rekening = dr("rekening")
            fotostruk = dr("logo")
            resized = ResizeGambar(Image.FromStream(New IO.MemoryStream(fotostruk)), New Size(65, 65))
        Else
            nama = ""
            alamat = ""
            telp = ""
            rekening = ""
            resized = ResizeGambar(ImageList.Images(0), New Size(65, 65))
        End If
        '============================================================================================================

        countnama = nama.Length
        countalamat = alamat.Length
        counttelp = telp.Length
        countrekening = rekening.Length

        '============================================================================================================

        writer.Options.Height = 88
        writer.Options.Width = 88
        writer.Format = BarcodeFormat.QR_CODE
        barcode = writer.Write(txtnonota.Text)

        '============================================================================================================

        tinggi += 0
        e.Graphics.Flush()
        e.Graphics.DrawImage(New Bitmap(resized), 10, 0)

        tinggi += 0
        If countnama > 15 Then
            e.Graphics.DrawString(nama, New System.Drawing.Font("Arial", 10), Brushes.Red, 80, tinggi)
            tinggi += 17
        Else
            e.Graphics.DrawString(nama, New System.Drawing.Font("Arial", 15), Brushes.Red, 80, tinggi)
            tinggi += 25
        End If

        e.Graphics.DrawString(alamat, New System.Drawing.Font("Arial", 7), Brushes.Black, 80, tinggi)
        tinggi += 15
        e.Graphics.DrawString("Telp : " + telp, New System.Drawing.Font("Arial", 7), Brushes.Black, 80, tinggi)
        tinggi += 15
        e.Graphics.DrawString("Rek : " + rekening, New System.Drawing.Font("Arial", 7), Brushes.Black, 80, tinggi)

        tinggi += 30

        e.Graphics.DrawString("No.Nota", New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + txtnonota.Text, New System.Drawing.Font("Arial", 7), Brushes.Black, 60, tinggi)
        'tinggi += 20
        e.Graphics.DrawImage(barcode, 170, tinggi - 10)

        tinggi += 15
        e.Graphics.DrawString("Customer", New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + txtpelanggan.Text, New System.Drawing.Font("Arial", 7), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Tanggal", New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + Format(dtpenjualan.Value, "dd MMMM yyyy HH:mm:ss").ToString, New System.Drawing.Font("Arial", 7), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Kasir", New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + fmenu.kodeuser.Text, New System.Drawing.Font("Arial", 7), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Sales", New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + cmbsales.Text, New System.Drawing.Font("Arial", 7), Brushes.Black, 60, tinggi)

        tinggi += 5
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

        For itm As Integer = 0 To GridView1.RowCount - 1
            tinggi += 15
            e.Graphics.DrawString(GridView1.GetRowCellValue(itm, "nama_barang"), New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)

            tinggi += 15
            e.Graphics.DrawString(FormatNumber(GridView1.GetRowCellValue(itm, "qty").ToString, 0) + " " + GridView1.GetRowCellValue(itm, "satuan_barang") + " x " + FormatNumber(GridView1.GetRowCellValue(itm, "harga_diskon").ToString, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 16, tinggi)
            e.Graphics.DrawString(" = " + FormatNumber(GridView1.GetRowCellValue(itm, "subtotal").ToString, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 167, tinggi)
        Next

        tinggi += 5
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

        If cbdiskon.Checked = True Or cbppn.Checked = True Or cbongkir.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("Subtotal ", New System.Drawing.Font("Arial", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(totalbelanja, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)
        End If

        If cbdiskon.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("Diskon ", New System.Drawing.Font("Arial", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(txtdiskonnominal.Text, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)
        End If

        If cbppn.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("PPN ", New System.Drawing.Font("Arial", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(txtppnnominal.Text, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)
        End If

        If cbongkir.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("Ongkir ", New System.Drawing.Font("Arial", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(txtongkir.Text, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)
        End If

        tinggi += 15
        e.Graphics.DrawString("Grandtotal ", New System.Drawing.Font("Arial", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + FormatNumber(txttotal.Text, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)

        tinggi += 15
        e.Graphics.DrawString("________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 110, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Pembayaran ", New System.Drawing.Font("Arial", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + cmbpembayaran.Text, New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Bayar ", New System.Drawing.Font("Arial", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + FormatNumber(bayar, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Sisa ", New System.Drawing.Font("Arial", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + FormatNumber(sisa, 0), New System.Drawing.Font("Arial", 7), Brushes.Black, 175, tinggi)


        tinggi += 30
        e.Graphics.DrawString("Keterangan :", New System.Drawing.Font("Arial", 7), Brushes.Black, 8, tinggi)

        tinggi += 5
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

        tinggi += 15
        e.Graphics.DrawString(txtketerangan.Text, New System.Drawing.Font("Arial", 6), Brushes.Black, 8, tinggi)

        tinggi += 50
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)


        tinggi += 30
        e.Graphics.DrawString("Terima Kasih Sudah berbelanja di toko kami", New System.Drawing.Font("Arial", 6), Brushes.Black, 35, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Barang yang sudah di beli tidak dapat di kembalikan atau ", New System.Drawing.Font("Arial", 6), Brushes.Black, 2, tinggi)
        tinggi += 15
        e.Graphics.DrawString("di tukar dengan alasan apapun ", New System.Drawing.Font("Arial", 6), Brushes.Black, 60, tinggi)

        tinggi += 25
        e.Graphics.DrawString("Barang yang sudah di beli dianggap sudah di cek", New System.Drawing.Font("Arial", 6), Brushes.Black, 25, tinggi)

        tinggi += 40
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

    End Sub

    Public Sub cetak_faktur()
        'barcode
        Dim tabel_barcode As New DataTable
        Dim baris_barcode As DataRow

        Dim writer As New BarcodeWriter
        Dim barcode As Image
        Dim ms As MemoryStream = New MemoryStream

        With tabel_barcode
            .Columns.Add("kode_barcode")
            .Columns.Add("gambar_barcode", GetType(Byte()))
        End With

        baris_barcode = tabel_barcode.NewRow
        baris_barcode("kode_barcode") = txtnonota.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtnonota.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan")
            .Columns.Add("jenis")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("satuan") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis") = GridView1.GetRowCellValue(i, "jenis_barang")
            baris("harga_satuan") = GridView1.GetRowCellValue(i, "harga_satuan")
            baris("diskon_persen") = GridView1.GetRowCellValue(i, "diskon_persen")
            baris("harga_diskon") = GridView1.GetRowCellValue(i, "harga_diskon")
            baris("subtotal") = GridView1.GetRowCellValue(i, "subtotal")
            baris("diskon_nominal") = GridView1.GetRowCellValue(i, "diskon_nominal")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturpenjualan
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", idpenjualan)
        rpt_faktur.SetParameterValue("namakasir", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("pembeli", txtpelanggan.Text)
        rpt_faktur.SetParameterValue("jatem", Format(dtjatuhtempo.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("bayar", bayar)
        rpt_faktur.SetParameterValue("sisa", sisa)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("diskon", diskonnominal)
        rpt_faktur.SetParameterValue("grandtotal", grandtotal)
        rpt_faktur.SetParameterValue("ppn", ppnnominal)
        rpt_faktur.SetParameterValue("ongkir", ongkir)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpenjualan.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("subtotal", totalbelanja)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Public Sub cetak_surat()
        'barcode
        Dim tabel_barcode As New DataTable
        Dim baris_barcode As DataRow

        Dim writer As New BarcodeWriter
        Dim barcode As Image
        Dim ms As MemoryStream = New MemoryStream

        With tabel_barcode
            .Columns.Add("kode_barcode")
            .Columns.Add("gambar_barcode", GetType(Byte()))
        End With

        baris_barcode = tabel_barcode.NewRow
        baris_barcode("kode_barcode") = txtnonota.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtnonota.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan")
            .Columns.Add("jenis")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("satuan") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis") = GridView1.GetRowCellValue(i, "jenis_barang")
            baris("harga_satuan") = GridView1.GetRowCellValue(i, "harga_satuan")
            baris("diskon_persen") = GridView1.GetRowCellValue(i, "diskon_persen")
            baris("harga_diskon") = GridView1.GetRowCellValue(i, "harga_diskon")
            baris("subtotal") = GridView1.GetRowCellValue(i, "subtotal")
            baris("diskon_nominal") = GridView1.GetRowCellValue(i, "diskon_nominal")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturpenjualansurat
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", idpenjualan)
        rpt_faktur.SetParameterValue("namakasir", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("pembeli", txtpelanggan.Text)
        rpt_faktur.SetParameterValue("jatem", Format(dtjatuhtempo.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("bayar", bayar)
        rpt_faktur.SetParameterValue("sisa", sisa)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("diskon", diskonnominal)
        rpt_faktur.SetParameterValue("grandtotal", grandtotal)
        rpt_faktur.SetParameterValue("ppn", ppnnominal)
        rpt_faktur.SetParameterValue("ongkir", ongkir)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpenjualan.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("subtotal", totalbelanja)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("namaexpedisi", txtnamaexpedisi.Text)
        rpt_faktur.SetParameterValue("alamatexpedisi", txtalamatexpedisi.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub
    Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
        Dim faktur As String
        Call koneksii()
        sql = "SELECT * FROM tb_printer WHERE nomor='2'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            faktur = dr("nama_printer")
        Else
            faktur = ""
        End If

        Try
            Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
            Dim PkSize As New System.Drawing.Printing.PaperSize
            ObjPrinterSetting.PrinterName = faktur
            For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                If ObjPrinterSetting.PaperSizes.Item(i).PaperName = mPaperSize.Trim Then
                    PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                    Exit For
                End If
            Next

            If PkSize IsNot Nothing Then
                Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = rpt_faktur.PrintOptions
                myAppPrintOptions.PrinterName = faktur
                myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                rpt_faktur.PrintOptions.PaperOrientation = IIf(PaperOrientation = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)
            End If
            PkSize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        'cek ke piutang
        Dim statuspiutang As Boolean = False

        If cbvoid.Checked = False Then
            Call koneksii()
            sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE penjualan_id = '" & txtnonota.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                statuspiutang = True
            Else
                statuspiutang = False
            End If

            If statuspiutang = False Then

                If btnedit.Text.Equals("Edit") Then
                    btnedit.Text = "Update"
                    Call awaledit()

                ElseIf btnedit.Text.Equals("Update") Then
                    If GridView1.DataRowCount > 0 Then
                        If txtpelanggan.Text IsNot "" Then
                            If txtgudang.Text IsNot "" Then
                                If cmbsales.Text IsNot "" Then
                                    If txtrekening.Text IsNot "" Then
                                        'isi disini sub updatenya
                                        Call perbaharui(txtnonota.Text)
                                        'Call inisialisasi(txtnonota.Text)
                                    Else
                                        MsgBox("Isi Pembayaran")
                                    End If
                                Else
                                    MsgBox("Isi Sales")
                                End If
                            Else
                                MsgBox("Isi Gudang")
                            End If
                        Else
                            MsgBox("Isi Customer")
                        End If
                    Else
                        MsgBox("Keranjang Masih Kosong")
                    End If
                End If
            Else
                MsgBox("Nota sudah lunas!")
            End If
        Else
            MsgBox("Nota sudah void !")
        End If


    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(idpenjualan)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgopenjualan.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT id FROM tb_penjualan WHERE id = '" & txtgopenjualan.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgopenjualan.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncaripenjualan_Click(sender As Object, e As EventArgs) Handles btncaripenjualan.Click
        tutupjual = 2
        fcaripenjualan.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub


    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 2
        fcaripelanggan.ShowDialog()
    End Sub
    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        If txtgudang.Text = "" Then
            MsgBox("Isi Kode Gudang", MsgBoxStyle.Information, "Informasi")
        Else
            If txtpelanggan.Text = "" Then
                MsgBox("Isi Kode Pelanggan", MsgBoxStyle.Information, "Informasi")
            Else
                tutupcaristok = 1
                idgudangcari = idgudang
                fcaristok.ShowDialog()
            End If
        End If
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 2
        fcarigudang.ShowDialog()
    End Sub

    Private Sub dtpenjualan_ValueChanged(sender As Object, e As EventArgs) Handles dtpenjualan.ValueChanged
        dtjatuhtempo.MinDate = dtpenjualan.Value
        'selisih = DateDiff(DateInterval.Day, dtpenjualan.Value, dtjatuhtempo.Value)
        'txtterm.Text = selisih
        term = txtterm.Text
        dateterm = dtpenjualan.Value
        datetermnow = dateterm.AddDays(term)
        dtjatuhtempo.Value = datetermnow
    End Sub

    Private Sub dtjatuhtempo_ValueChanged(sender As Object, e As EventArgs) Handles dtjatuhtempo.ValueChanged
        'selisih = DateDiff(DateInterval.Day, dtpenjualan.Value, dtjatuhtempo.Value)
        'txtterm.Text = selisih
    End Sub

    Private Sub txtterm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtterm.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtterm_TextChanged(sender As Object, e As EventArgs) Handles txtterm.TextChanged
        If txtterm.Text = "" Then
            txtterm.Text = 0
        Else
            term = txtterm.Text
            txtterm.Text = Format(term, "##,##0")
            txtterm.SelectionStart = Len(txtterm.Text)

            If term = 0 Then
                dtjatuhtempo.Value = Date.Now
            Else
                dateterm = dtpenjualan.Value
                datetermnow = dateterm.AddDays(term)
                dtjatuhtempo.Value = datetermnow
            End If
        End If
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        If txtgudang.Text = "" Then
            MsgBox("Isi Kode Gudang", MsgBoxStyle.Information, "Informasi")
        Else
            If txtpelanggan.Text = "" Then
                MsgBox("Isi Kode Pelanggan", MsgBoxStyle.Information, "Informasi")
            Else
                Call caristok()
            End If
        End If
    End Sub

    Private Sub txtkodestok_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodestok.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
            BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If
    End Sub

    Sub tambah()
        Dim hargajuallangsung As Double
        Dim tbbanyak As Integer = 0
        Dim tbnilaipersen As Double = 0
        Dim tbnilainominal As Double = 0

        Dim lokasi As Integer = -1
        'Columns.Add("kode_barang")
        'Columns.Add("kode_stok")
        'Columns.Add("nama_barang")
        'Columns.Add("qty", GetType(Double))
        'Columns.Add("satuan_barang")
        'Columns.Add("jenis_barang")
        'Columns.Add("harga_satuan", GetType(Double))
        'Columns.Add("diskon_persen", GetType(Double))
        'Columns.Add("diskon_nominal", GetType(Double))
        'Columns.Add("harga_diskon", GetType(Double))
        'Columns.Add("subtotal", GetType(Double))
        'Columns.Add("laba", GetType(Double))
        'Columns.Add("modal_barang", GetType(Double))
        'Columns.Add("barang_id")
        'Columns.Add("stok_id")

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtharga.Text = "" Or txtbanyak.Text = "" Or banyak <= 0 Then
            MsgBox("Barang Kosong Atau Pricelist Group belum terisi", MsgBoxStyle.Information, "Informasi")
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.id = tb_stok.barang_id JOIN tb_price_group ON tb_barang.id = tb_price_group.barang_id WHERE tb_stok.id = '" & idstok & "' AND tb_price_group.pelanggan_id ='" & idpelanggan & "' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'cek stok mencukupi atau enggak
                    If dr("jumlah_stok") < banyak Then
                        MsgBox("Stok tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                    Else
                        tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * banyak, (Val(dr("harga_jual")) * banyak - Val(modalbarang) * banyak), modalbarang, idbarang, idstok)
                        Call reload_tabel()
                    End If
                Else
                    'kalau stok tidak ada karena tidak terdaftar di price group
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.id = tb_stok.barang_id JOIN tb_price_group ON tb_barang.id = tb_price_group.barang_id WHERE tb_stok.id = '" & idstok & "' AND tb_price_group.pelanggan_id = '1'  AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        If dr("jumlah_stok") < banyak Then
                            MsgBox("Stok tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * banyak, (Val(dr("harga_jual")) * banyak - Val(modalbarang) * banyak), modalbarang, idbarang, idstok)
                            Call reload_tabel()
                        End If
                    Else
                        'maka gunakan table kategori sebagai selisih harga modal dan harga jual
                        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.id = tb_stok.barang_id JOIN tb_kategori_barang ON tb_barang.kategori_barang_id = tb_kategori_barang.id WHERE tb_stok.id = '" & idstok & "' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        If dr.HasRows Then
                            If Val(dr("jumlah_stok")) < banyak Then
                                MsgBox("Stok tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                hargajuallangsung = Val(dr("modal_barang")) + Val(dr("selisih_kategori"))
                                tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("modal_barang")) + Val(dr("selisih_kategori")), "0", "0", hargajuallangsung, hargajuallangsung * banyak, (hargajuallangsung * banyak - Val(modalbarang) * banyak), modalbarang, idbarang, idstok)
                                Call reload_tabel()
                            End If
                        Else
                            MsgBox("Stok kosong", MsgBoxStyle.Information, "Informasi")
                        End If
                    End If
                End If

            Else 'kalau ada isi
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.id = tb_stok.barang_id JOIN tb_price_group ON tb_barang.id = tb_price_group.barang_id WHERE tb_stok.id = '" & idstok & "' AND tb_price_group.pelanggan_id = '" & idpelanggan & "' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                            lokasi = i
                        End If
                    Next

                    tbbanyak = Val(GridView1.GetRowCellValue(lokasi, "qty"))
                    tbnilaipersen = Val(GridView1.GetRowCellValue(lokasi, "diskon_persen"))
                    tbnilainominal = Val(GridView1.GetRowCellValue(lokasi, "diskon_nominal"))

                    If lokasi = -1 Then
                        'cek 1
                        'MsgBox("cek 1")

                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalbarang) * (banyak + tbbanyak))), modalbarang, idbarang, idstok)
                            Call reload_tabel()
                        End If
                    Else
                        'cek 2
                        'MsgBox("cek 2")

                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalbarang) * (banyak + tbbanyak))), modalbarang, idbarang, idstok)
                            Call reload_tabel()
                        End If
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.id = tb_stok.barang_id JOIN tb_price_group ON tb_barang.id = tb_price_group.barang_id WHERE tb_stok.id = '" & idstok & "' AND tb_price_group.pelanggan_id='1' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        For i As Integer = 0 To GridView1.RowCount - 1
                            If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                                lokasi = i
                            End If
                        Next

                        tbbanyak = Val(GridView1.GetRowCellValue(lokasi, "qty"))
                        tbnilaipersen = Val(GridView1.GetRowCellValue(lokasi, "diskon_persen"))
                        tbnilainominal = Val(GridView1.GetRowCellValue(lokasi, "diskon_nominal"))

                        If lokasi = -1 Then
                            'cek 3
                            'MsgBox("cek 3")

                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalbarang) * (banyak + tbbanyak))), modalbarang, idbarang, idstok)
                                Call reload_tabel()
                            End If
                        Else
                            'cek 4
                            'MsgBox("cek 4")

                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalbarang) * (banyak + tbbanyak))), modalbarang, idbarang, idstok)
                                Call reload_tabel()
                            End If
                        End If
                    Else
                        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.id = tb_stok.barang_id JOIN tb_kategori_barang ON tb_barang.kategori_barang_id = tb_kategori_barang.id WHERE tb_stok.id = '" & idstok & "' AND tb_stok.gudang_id ='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        If dr.HasRows Then

                            For i As Integer = 0 To GridView1.RowCount - 1
                                If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                                    lokasi = i
                                End If
                            Next

                            tbbanyak = Val(GridView1.GetRowCellValue(lokasi, "qty"))
                            tbnilaipersen = Val(GridView1.GetRowCellValue(lokasi, "diskon_persen"))
                            tbnilainominal = Val(GridView1.GetRowCellValue(lokasi, "diskon_nominal"))

                            hargajuallangsung = Val(dr("modal_barang")) + Val(dr("selisih_kategori"))

                            If lokasi = -1 Then
                                'cek 5
                                'MsgBox("cek 5")

                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, hargajuallangsung, tbnilaipersen, tbnilainominal, hargajuallangsung - tbnilainominal, (hargajuallangsung - tbnilainominal) * (banyak + tbbanyak), (Val(hargajuallangsung - tbnilainominal) * (banyak + tbbanyak) - (Val(modalbarang) * (banyak + tbbanyak))), modalbarang, idbarang, idstok)
                                    Call reload_tabel()
                                End If
                            Else
                                'cek 6
                                'MsgBox("cek 6")

                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                    tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, hargajuallangsung, tbnilaipersen, tbnilainominal, hargajuallangsung - tbnilainominal, (hargajuallangsung - tbnilainominal) * (banyak + tbbanyak), (Val(hargajuallangsung - tbnilainominal) * (banyak + tbbanyak) - (Val(modalbarang) * (banyak + tbbanyak))), modalbarang, idbarang, idstok)
                                    Call reload_tabel()
                                End If
                            End If
                        Else
                            MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "qty" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "qty", 1)
                Try
                    'GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
                    'GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 1 * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                    GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * 1)
                Catch ex As Exception
                    'error jika nulai qty=blank
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                End Try
            Else
                Try
                    'GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
                    'GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                    GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * e.Value)
                Catch ex As Exception
                    'error jika nulai qty=blank
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                End Try
            End If
        End If
        '==========================================
        If e.Column.FieldName = "harga_satuan" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "harga_satuan", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * 1)
                    GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", e.Value - GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * 1)
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "qty") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                    GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "qty"))
                Catch ex As Exception
                    'error jika nulai qty=blank
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                End Try
            End If
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", e.Value - GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "qty") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "qty"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        End If
        '==========================================
        If e.Column.FieldName = "diskon_persen" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "qty") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "qty"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        End If
        '==========================================
        If e.Column.FieldName = "diskon_nominal" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon_persen", Math.Round(e.Value / GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") * 100%, 3))
                GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "qty") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "qty"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        End If
        '==========================================
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub

    Private Sub GridView1_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles GridView1.RowUpdated
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub fpenjualan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub txtrekening_TextChanged(sender As Object, e As EventArgs) Handles txtrekening.TextChanged
        If txtrekening.Text.Equals("KREDIT") Then
            txtbayar.Text = 0
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column.FieldName = "harga_satuan" Then
            If (Val(e.Value) < Val(GridView1.GetRowCellValue(e.RowHandle, "modal_barang"))) Then
                MsgBox("Harga Dibawah Modal", MsgBoxStyle.Information, "Peringatan")
            End If
        ElseIf e.Column.FieldName = "diskon_persen" Then
            If (Val(e.Value) > 10) Then
                MsgBox("Persentase Lebih dari 10%", MsgBoxStyle.Information, "Peringatan")
            End If
        End If

    End Sub

    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub cbdiskon_CheckedChanged(sender As Object, e As EventArgs) Handles cbdiskon.CheckedChanged
        If cbdiskon.Checked = True Then
            txtdiskonpersen.Enabled = True
        Else
            txtdiskonpersen.Enabled = False
            txtdiskonpersen.Text = 0
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub cbppn_CheckedChanged(sender As Object, e As EventArgs) Handles cbppn.CheckedChanged
        If cbppn.Checked = True Then
            txtppnpersen.Enabled = True
        Else
            txtppnpersen.Enabled = False
            txtppnpersen.Text = 0
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub cbongkir_CheckedChanged(sender As Object, e As EventArgs) Handles cbongkir.CheckedChanged
        If cbongkir.Checked = True Then
            txtongkir.Enabled = True
        Else
            txtongkir.Enabled = False
            txtongkir.Text = 0
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub txtppn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtppnpersen.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtdiskonpersen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdiskonpersen.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtdiskonnominal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdiskonnominal.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtongkir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtongkir.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbayar.KeyPress
        e.Handled = ValidAngka(e)
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub txtppnnominal_TextChanged(sender As Object, e As EventArgs) Handles txtppnnominal.TextChanged
        If txtppnnominal.Text = "" Then
            txtppnnominal.Text = 0
        Else
            ppnnominal = txtppnnominal.Text
            txtppnnominal.Text = Format(ppnnominal, "##,##0")
            txtppnnominal.SelectionStart = Len(txtppnnominal.Text)
        End If
    End Sub

    Private Sub txtdiskonnominal_TextChanged(sender As Object, e As EventArgs) Handles txtdiskonnominal.TextChanged
        If txtdiskonnominal.Text = "" Then
            txtdiskonnominal.Text = 0
        Else
            diskonnominal = txtdiskonnominal.Text
            txtdiskonnominal.Text = Format(diskonnominal, "##,##0")
            txtdiskonnominal.SelectionStart = Len(txtdiskonnominal.Text)
        End If
    End Sub

    Private Sub txtppnpersen_TextChanged(sender As Object, e As EventArgs) Handles txtppnpersen.TextChanged
        If txtppnpersen.Text = "" Then
            txtppnpersen.Text = 0
        Else
            ppnpersen = txtppnpersen.Text
            txtppnpersen.Text = Format(ppnpersen, "##,##0")
            txtppnpersen.SelectionStart = Len(txtppnpersen.Text)
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub txtdiskonpersen_TextChanged(sender As Object, e As EventArgs) Handles txtdiskonpersen.TextChanged
        If txtdiskonpersen.Text = "" Then
            txtdiskonpersen.Text = 0
        Else
            diskonpersen = txtdiskonpersen.Text
            txtdiskonpersen.Text = Format(diskonpersen, "##,##0")
            txtdiskonpersen.SelectionStart = Len(txtdiskonpersen.Text)
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub txtongkir_TextChanged(sender As Object, e As EventArgs) Handles txtongkir.TextChanged
        If txtongkir.Text = "" Then
            txtongkir.Text = 0
        Else
            ongkir = txtongkir.Text
            txtongkir.Text = Format(ongkir, "##,##0")
            txtongkir.SelectionStart = Len(txtongkir.Text)
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub txttotal_TextChanged(sender As Object, e As EventArgs) Handles txttotal.TextChanged
        If txttotal.Text = "" Then
            txttotal.Text = 0
        Else
            txttotal.Text = Format(grandtotal, "##,##0")
            txttotal.SelectionStart = Len(txttotal.Text)
        End If
    End Sub

    Private Sub btncarikas_Click(sender As Object, e As EventArgs) Handles btncarikas.Click
        tutupkas = 1
        fcarikas.ShowDialog()
    End Sub

    Private Sub btnbayarfull_Click(sender As Object, e As EventArgs) Handles btnbayarfull.Click
        Dim bayarfull, sisafull As Double

        If btnbayarfull.Text.Equals("Bayar") Then
            bayarfull = totalbelanja
            sisa = 0
            bayar = bayarfull

            txtbayar.Text = bayar
            txtsisa.Text = sisa

            btnbayarfull.Text = "Sisa"
        ElseIf btnbayarfull.Text.Equals("Sisa") Then
            sisafull = totalbelanja
            bayar = 0
            sisa = sisafull

            txtbayar.Text = bayar
            txtsisa.Text = sisa

            btnbayarfull.Text = "Bayar"
        End If

    End Sub

    Private Sub txtbayar_TextChanged(sender As Object, e As EventArgs) Handles txtbayar.TextChanged
        If txtbayar.Text = "" Then
            txtbayar.Text = 0
        Else
            If cmbpembayaran.Text.Equals("KREDIT") Then
                bayar = 0
                txtbayar.Text = 0
            Else
                bayar = txtbayar.Text
                txtbayar.Text = Format(bayar, "##,##0")
                txtbayar.SelectionStart = Len(txtbayar.Text)
            End If
        End If
    End Sub

    Private Sub txtsisa_TextChanged(sender As Object, e As EventArgs) Handles txtsisa.TextChanged
        If txtsisa.Text = "" Then
            txtsisa.Text = 0
        Else
            txtsisa.Text = Format(sisa, "##,##0")
            txtsisa.SelectionStart = Len(txtsisa.Text)
        End If
    End Sub
    Sub simpan()
        Call koneksii()

        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            If bayar.Equals(grandtotal) Then
                lunasstatus = 1
            Else
                lunasstatus = 0
            End If

            sql = "INSERT INTO tb_penjualan(pelanggan_id, gudang_id, user_id, tgl_penjualan, tgl_jatuhtempo_penjualan, term_penjualan, lunas_penjualan, void_penjualan, print_penjualan, posted_penjualan, keterangan_penjualan, nama_expedisi, alamat_expedisi, diskon_penjualan, pajak_penjualan, ongkir_penjualan, total_penjualan, metode_pembayaran, rekening, bayar_penjualan, sisa_penjualan, created_by, updated_by, date_created, last_updated) VALUES('" & idpelanggan & "','" & idgudang & "','" & iduser & "' , '" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & term & "','" & lunasstatus & "','" & 0 & "','" & 0 & "','" & 1 & "','" & txtketerangan.Text & "','" & txtnamaexpedisi.Text & "','" & txtalamatexpedisi.Text & "','" & txtdiskonpersen.Text & "','" & txtppnpersen.Text & "','" & ongkir & "','" & grandtotal & "','" & cmbpembayaran.Text & "', '" & txtrekening.Text & "','" & bayar & "','" & sisa & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idpenjualan = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & Val(GridView1.GetRowCellValue(i, "qty")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                myCommand.ExecuteNonQuery()

                diskonpersennilai = GridView1.GetRowCellValue(i, "diskon_persen")
                diskonnominalnilai = GridView1.GetRowCellValue(i, "diskon_nominal")

                myCommand.CommandText = "INSERT INTO tb_penjualan_detail (penjualan_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & idpenjualan & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & Val(GridView1.GetRowCellValue(i, "qty")) & "','" & Val(GridView1.GetRowCellValue(i, "harga_satuan")) & "','" & diskonpersennilai & "','" & diskonnominalnilai & "' ,'" & Val(GridView1.GetRowCellValue(i, "subtotal")) & "','" & Val(GridView1.GetRowCellValue(i, "modal_barang")) & "','" & Val(GridView1.GetRowCellValue(i, "laba")) & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            kodepembayaran = cmbpembayaran.Text

            If kodepembayaran IsNot "" Then
                myCommand.CommandText = "INSERT INTO tb_transaksi_kas (kode_kas, kode_penjualan, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepembayaran & "','" & idpenjualan & "', 'AWAL','" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Nota Nomor " & idpenjualan & "','" & sisa & "', '" & bayar & "', '" & fmenu.kodeuser.Text & "', '" & fmenu.kodeuser.Text & "', now(), now())"
                myCommand.ExecuteNonQuery()
            End If

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            'history user ==========
            Call historysave("Menyimpan Data Penjualan Kode " & idpenjualan, idpenjualan, namaform)
            '========================
            Call inisialisasi(idpenjualan)

        Catch e As Exception
            Try
                myTrans.Rollback()
            Catch ex As OdbcException
                If Not myTrans.Connection Is Nothing Then
                    Console.WriteLine("An exception of type " + ex.GetType().ToString() +
                    " was encountered while attempting to roll back the transaction.")
                End If
            End Try

            Console.WriteLine("An exception of type " + e.GetType().ToString() +
            "was encountered while inserting the data.")
            Console.WriteLine("Neither record was written to database.")
            MsgBox("Transaksi Gagal Dilakukan", MsgBoxStyle.Information, "Sukses")
        End Try

    End Sub

    Sub perbaharui(nomornota As Integer)
        kodepembayaran = cmbpembayaran.Text

        'periksa di barang di stok dulu
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim stokdatabasesementara As Integer

        Dim kodestokdatabase As String
        Dim statusavailable As Boolean = True
        Dim idgudanglama As Integer

        'variabel transactional
        '=======================
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction
        '=======================

        'periksa stok
        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "qty")
                stokdatabase = dr("jumlah_stok")
                kodestokdatabase = dr("kode_stok")

                'mengambil selisih qty dari penjualan detail
                sql = "SELECT * FROM tb_penjualan_detail WHERE stok_id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND penjualan_id ='" & nomornota & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    stokdatabasesementara = dr("qty")
                Else
                    stokdatabasesementara = 0
                End If
                '=============================================

                If (stokdatabase + stokdatabasesementara) < stok Then
                    MsgBox("Stok dengan kode stok " & kodestokdatabase & " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " & GridView1.GetRowCellValue(i, "kode_stok") & " tidak ada.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            ' Start a local transaction
            myTrans = cnnx.BeginTransaction()
            myCommand.Connection = cnnx
            myCommand.Transaction = myTrans


            'cari nota  yang sebelumnya (kembalikan stok dulu) cek dulu disini
            Call koneksii()
            sql = "SELECT gudang_id FROM tb_penjualan WHERE id = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            idgudanglama = Val(cmmd.ExecuteScalar())

            Try
                'update kembali dari transaksi sebelumnya
                For i As Integer = 0 To tabelsementara.Rows.Count - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & tabelsementara.Rows(i).Item(3) & "' WHERE id = '" & tabelsementara.Rows(i).Item(14) & "' AND gudang_id ='" & idgudanglama & "'"
                    myCommand.ExecuteNonQuery()
                Next

                'hapus barang dari tabel keluar detail
                myCommand.CommandText = "DELETE FROM tb_penjualan_detail WHERE penjualan_id = '" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                'hapus panjar
                myCommand.CommandText = "DELETE FROM tb_transaksi_kas WHERE kode_penjualan = '" & nomornota & "' and jenis_kas ='AWAL'"
                myCommand.ExecuteNonQuery()

                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()

                    diskonpersennilai = GridView1.GetRowCellValue(i, "diskon_persen")
                    diskonnominalnilai = GridView1.GetRowCellValue(i, "diskon_nominal")

                    myCommand.CommandText = "INSERT INTO tb_penjualan_detail(penjualan_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & diskonpersennilai & "','" & diskonnominalnilai & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "modal_barang") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                If bayar.Equals(grandtotal) Then
                    lunasstatus = 1
                Else
                    lunasstatus = 0
                End If

                myCommand.CommandText = "UPDATE tb_penjualan SET pelanggan_id ='" & idpelanggan & "', gudang_id ='" & idgudang & "', user_id ='" & iduser & "' , tgl_penjualan ='" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "', tgl_jatuhtempo_penjualan ='" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "', term_penjualan='" & term & "', lunas_penjualan = '" & lunasstatus & "',keterangan_penjualan ='" & txtketerangan.Text & "',nama_expedisi ='" & txtnamaexpedisi.Text & "',alamat_expedisi ='" & txtalamatexpedisi.Text & "', diskon_penjualan ='" & txtdiskonpersen.Text & "', pajak_penjualan ='" & txtppnpersen.Text & "', ongkir_penjualan ='" & ongkir & "', total_penjualan ='" & grandtotal & "',metode_pembayaran ='" & cmbpembayaran.Text & "',rekening ='" & txtrekening.Text & "', bayar_penjualan ='" & bayar & "', sisa_penjualan ='" & sisa & "', updated_by ='" & fmenu.kodeuser.Text & "', last_updated = now() WHERE id ='" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                kodepembayaran = cmbpembayaran.Text

                If kodepembayaran IsNot "" Then
                    myCommand.CommandText = "INSERT INTO tb_transaksi_kas (kode_kas, kode_penjualan, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepembayaran & "','" & nomornota & "', 'AWAL', now(), 'Transaksi Nota Nomor " & nomornota & "','" & sisa & "', '" & bayar & "', '" & fmenu.kodeuser.Text & "', '" & fmenu.kodeuser.Text & "', now(), now())"
                    myCommand.ExecuteNonQuery()
                End If

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                'Dim mystring = ""
                'For Each ts As DataRow In tabelsementara.Rows
                '    mystring &= ts.Item(0)
                'Next

                'history user ==========
                Call historysave("Mengedit Data Penjualan Kode " & nomornota.ToString, nomornota, namaform)
                '========================
                MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
                Call inisialisasi(nomornota)
                btnedit.Text = "Edit"

            Catch e As Exception
                Try
                    myTrans.Rollback()
                Catch ex As OdbcException
                    If Not myTrans.Connection Is Nothing Then
                        Console.WriteLine("An exception of type " + ex.GetType().ToString() + " was encountered while attempting to roll back the transaction.")
                    End If
                End Try

                Console.WriteLine("An exception of type " + e.GetType().ToString() + "was encountered while inserting the data.")
                Console.WriteLine("Neither record was written to database.")

                MsgBox("Update Gagal", MsgBoxStyle.Information, "Sukses")
            End Try
        End If
    End Sub



    'Private Function CpuId() As String
    '    Dim computer As String = "."
    '    Dim wmi As Object = GetObject("winmgmts:" &
    '    "{impersonationLevel=impersonate}!\\" &
    '    computer & "\root\cimv2")
    '    Dim processors As Object = wmi.ExecQuery("Select * from " &
    '    "Win32_Processor")

    '    Dim cpu_ids As String = ""
    '    For Each cpu As Object In processors
    '        cpu_ids = cpu_ids & ", " & cpu.ProcessorId
    '    Next cpu
    '    If cpu_ids.Length > 0 Then cpu_ids =
    '    cpu_ids.Substring(2)

    '    Return cpu_ids
    'End Function

    'Sub printer()
    '    sql = "select * from tb_printer where cpuid='" & CpuId() & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    If dr.HasRows Then
    '        faktur = dr("faktur")
    '        struk = dr("struk")
    '    Else
    '        MsgBox("Printer Belum di Setting", vbInformation.Information, "Error....")
    '        MsgBox("Gagal Mencetak", vbInformation.Information, "Error....")
    '    End If
    'End Sub

    Sub proses()
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim statusavailable As Boolean = True

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id = '" & Val(GridView1.GetRowCellValue(i, "stok_id")) & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = Val(GridView1.GetRowCellValue(i, "qty"))
                stokdatabase = Val(dr("jumlah_stok"))
                If stokdatabase < stok Then
                    MsgBox("Stok dengan kode stok " + dr("kode_stok") + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " + GridView1.GetRowCellValue(i, "kode_stok").ToString + " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            Call simpan()
        End If
    End Sub

    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtbanyak_TextChanged(sender As Object, e As EventArgs) Handles txtbanyak.TextChanged
        If txtbanyak.Text = "" Or txtbanyak.Text = "0" Then
            txtbanyak.Text = 1
        Else
            banyak = txtbanyak.Text
            txtbanyak.Text = Format(banyak, "##,##0")
            txtbanyak.SelectionStart = Len(txtbanyak.Text)
        End If
    End Sub

    Private Sub UpdateTotalText()
        totalbelanja = GridView1.Columns("subtotal").SummaryItem.SummaryValue
        grandtotal = totalbelanja
        If cbdiskon.Checked = True And cbppn.Checked = False And cbongkir.Checked = False Then
            txtdiskonnominal.Text = totalbelanja * txtdiskonpersen.Text / 100
            grandtotal = totalbelanja - (totalbelanja * txtdiskonpersen.Text / 100)
        ElseIf cbppn.Checked = True And cbdiskon.Checked = False And cbongkir.Checked = False Then
            txtppnnominal.Text = totalbelanja * txtppnpersen.Text / 100
            grandtotal = totalbelanja + (totalbelanja * txtppnpersen.Text / 100)
        ElseIf cbppn.Checked = True And cbdiskon.Checked = True And cbongkir.Checked = False Then
            txtdiskonnominal.Text = totalbelanja * txtdiskonpersen.Text / 100
            txtppnnominal.Text = (totalbelanja - txtdiskonnominal.Text) * txtppnpersen.Text / 100
            grandtotal = totalbelanja - txtdiskonnominal.Text + txtppnnominal.Text
        ElseIf cbdiskon.Checked = True And cbppn.Checked = False And cbongkir.Checked = True Then
            txtdiskonnominal.Text = totalbelanja * txtdiskonpersen.Text / 100
            grandtotal = totalbelanja - (totalbelanja * txtdiskonpersen.Text / 100) + txtongkir.Text
        ElseIf cbppn.Checked = True And cbdiskon.Checked = False And cbongkir.Checked = True Then
            txtppnnominal.Text = totalbelanja * txtppnpersen.Text / 100
            grandtotal = totalbelanja + (totalbelanja * txtppnpersen.Text / 100) + txtongkir.Text
        ElseIf cbppn.Checked = True And cbdiskon.Checked = True And cbongkir.Checked = True Then
            txtdiskonnominal.Text = totalbelanja * txtdiskonpersen.Text / 100
            txtppnnominal.Text = (totalbelanja - txtdiskonnominal.Text) * txtppnpersen.Text / 100
            grandtotal = totalbelanja - txtdiskonnominal.Text + txtppnnominal.Text + txtongkir.Text
        ElseIf cbppn.Checked = False And cbdiskon.Checked = False And cbongkir.Checked = True Then
            grandtotal = totalbelanja + txtongkir.Text
        End If
        txttotal.Text = grandtotal
        sisa = grandtotal - txtbayar.Text
        txtsisa.Text = sisa
    End Sub
End Class