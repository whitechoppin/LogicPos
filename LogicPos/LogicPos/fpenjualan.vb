Imports System.Data.Odbc
Imports DevExpress.Utils
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports CrystalDecisions.Shared
Public Class fpenjualan
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tinggi As Integer
    Public tabel As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Dim lunasstatus As Integer = 0
    Public jenis, satuan, kodepenjualan, kodetransaksi, kodegudang As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    Dim rpt_faktur As New ReportDocument

    'variabel edit penjualan
    Dim countingbarang As Integer
    Private Sub fpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        hitnumber = 0
        kodepenjualan = currentnumber()
        Call inisialisasi(kodepenjualan)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
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
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_penjualan,3) FROM tb_penjualan WHERE DATE_FORMAT(MID(`kode_penjualan`, 3 , 6), ' %y ')+ MONTH(MID(`kode_penjualan`,3 , 6)) + DAY(MID(`kode_penjualan`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_penjualan,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "JL" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "JL" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "JL" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "JL" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
    Function currentnumber()
        Call koneksii()
        sql = "SELECT kode_penjualan FROM tb_penjualan ORDER BY kode_penjualan DESC LIMIT 1;"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Return dr.Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            'cnn.Close()
        End Try
        Return pesan
    End Function
    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT kode_penjualan FROM tb_penjualan WHERE date_created < (SELECT date_created FROM tb_penjualan WHERE kode_penjualan = '" + previousnumber + "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
        Finally
            'cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_penjualan FROM tb_penjualan WHERE date_created > (SELECT date_created FROM tb_penjualan WHERE kode_penjualan = '" + nextingnumber + "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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
        Finally
            'cnn.Close()
        End Try
    End Sub
    Sub previewpenjualan(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan_detail WHERE kode_penjualan ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")))
            GridControl1.RefreshDataSource()
        End While
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
    Sub comboboxgudang()
        Call koneksii()
        cmbgudang.Items.Clear()
        cmbgudang.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_gudang", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbgudang.AutoCompleteCustomSource.Add(dr("kode_gudang"))
                cmbgudang.Items.Add(dr("kode_gudang"))
            End While
        End If
    End Sub
    Sub comboboxuser()
        Call koneksii()
        cmbsales.Items.Clear()
        cmbsales.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_user", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsales.AutoCompleteCustomSource.Add(dr("kode_user"))
                cmbsales.Items.Add(dr("kode_user"))
            End While
        End If
    End Sub
    Sub comboboxpembayaran()
        Call koneksii()
        cmbpembayaran.Items.Clear()
        cmbpembayaran.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbpembayaran.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbpembayaran.Items.Add(dr("kode_kas"))
            End While
        End If
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
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbcustomer.Enabled = True
        cmbcustomer.SelectedIndex = 0
        cmbcustomer.Text = "00000000"
        cmbcustomer.Focus()
        btncaricustomer.Enabled = True

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = 0
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        cblunas.Checked = False
        cbvoid.Checked = False
        cbprinted.Checked = False
        cbposted.Checked = False

        dtpenjualan.Enabled = True
        dtpenjualan.Value = Date.Now

        dtjatuhtempo.Enabled = True
        dtjatuhtempo.Value = dtpenjualan.Value

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 0
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

        cmbpembayaran.SelectedIndex = 0
        cmbpembayaran.Enabled = True
        btncarikas.Enabled = True

        btnbayarfull.Enabled = True
        txtbayar.Enabled = True
        txtbayar.Clear()
        txtbayar.Text = 0
        txtsisa.Clear()
        txtsisa.Text = 0

        'isi combo box
        Call comboboxcustomer()
        Call comboboxuser()
        Call comboboxgudang()
        Call comboboxpembayaran()

        'buat tabel
        Call tabel_utama()

    End Sub
    Sub inisialisasi(nomorkode As String)

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
        btnnext.Enabled = True

        rbfaktur.Checked = False
        rbstruk.Checked = True

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbcustomer.Enabled = False
        btncaricustomer.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False
        txtgudang.Enabled = False

        dtpenjualan.Enabled = False
        dtpenjualan.Value = Date.Now

        dtjatuhtempo.Enabled = False
        dtjatuhtempo.Value = dtpenjualan.Value

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = False
        btncaribarang.Enabled = False

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 0
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
        txtrekening.Clear()

        btnbayarfull.Enabled = False
        txtbayar.Clear()
        txtbayar.Text = 0
        txtbayar.Enabled = False
        txtsisa.Clear()
        txtsisa.Text = 0

        'isi combo box
        Call comboboxcustomer()
        Call comboboxuser()
        Call comboboxgudang()
        Call comboboxpembayaran()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomornota = dr("kode_penjualan")
                    nomorcustomer = dr("kode_pelanggan")
                    nomorsales = dr("kode_user")
                    nomorgudang = dr("kode_gudang")

                    statuslunas = dr("lunas_penjualan")
                    statusvoid = dr("void_penjualan")
                    statusprint = dr("print_penjualan")
                    statusposted = dr("posted_penjualan")

                    viewtglpenjualan = dr("tgl_penjualan")
                    viewtgljatuhtempo = dr("tgl_jatuhtempo_penjualan")

                    viewketerangan = dr("keterangan_penjualan")
                    viewpembayaran = dr("metode_pembayaran")

                    nilaidiskon = dr("diskon_penjualan")
                    nilaippn = dr("pajak_penjualan")
                    nilaiongkir = dr("ongkir_penjualan")
                    nilaibayar = dr("bayar_penjualan")

                    txtnonota.Text = nomornota
                    cmbcustomer.Text = nomorcustomer
                    cmbsales.Text = nomorsales
                    cmbgudang.Text = nomorgudang
                    cblunas.Checked = statuslunas
                    cbvoid.Checked = statusvoid
                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    dtpenjualan.Value = viewtglpenjualan
                    dtjatuhtempo.Value = viewtgljatuhtempo

                    'isi tabel view pembelian

                    Call previewpenjualan(nomorkode)

                    'total tabel pembelian

                    txtketerangan.Text = viewketerangan

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

                    cnn.Close()
                End If
            End Using
        Else
            txtnonota.Clear()
            cmbcustomer.Text = ""
            cmbsales.Text = ""
            cmbgudang.Text = ""
            cblunas.Checked = False
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            dtpenjualan.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            txtketerangan.Text = ""


            cbdiskon.Checked = False
            txtdiskonpersen.Text = 0

            cbppn.Checked = False
            txtppnpersen.Text = 0

            cbongkir.Checked = False
            txtongkir.Text = 0

            txtongkir.Enabled = False
            txtppnpersen.Enabled = False
            txtdiskonpersen.Enabled = False

            cmbpembayaran.Text = ""
            txtbayar.Text = ""
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
        btnnext.Enabled = False

        'header
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbcustomer.Enabled = True
        'cmbcustomer.SelectedIndex = -1
        cmbcustomer.Focus()
        btncaricustomer.Enabled = True

        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        'cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtpenjualan.Enabled = True
        'dtpenjualan.Value = Date.Now

        dtjatuhtempo.Enabled = True
        'dtjatuhtempo.Value = Date.Now

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 0
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

        'txtrekening.Clear()

        'txtbayar.Clear()
        'txtbayar.Text = 0
        'txtsisa.Clear()
        'txtsisa.Text = 0

        'isi combo box
        Call comboboxcustomer()
        Call comboboxuser()
        Call comboboxgudang()
        Call comboboxpembayaran()

        'simpan di tabel sementara
        Call koneksii()

        'hapus di tabel jual sementara
        Call koneksii()
        sql = "DELETE FROM tb_penjualan_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'isi tabel sementara dengan data tabel detail
        sql = "INSERT INTO tb_penjualan_detail_sementara SELECT * FROM tb_penjualan_detail WHERE kode_penjualan ='" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

    End Sub

    Sub tabel_utama()
        tabel = New DataTable

        With tabel
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

        GridControl1.DataSource = tabel

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
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbcustomer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtcustomer.Text = dr("nama_pelanggan")
            txtalamat.Text = dr("alamat_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
        Else
            txtcustomer.Text = ""
            txtalamat.Text = ""
            txttelp.Text = ""
        End If
    End Sub

    Sub carigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbgudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtgudang.Text = dr("nama_gudang")
        Else
            txtgudang.Text = ""
        End If
    End Sub

    Sub caripembayaran()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbpembayaran.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtrekening.Text = dr("nama_kas")
        Else
            txtrekening.Text = ""
        End If
    End Sub

    Private Sub rbfaktur_CheckedChanged(sender As Object, e As EventArgs) Handles rbfaktur.CheckedChanged

    End Sub

    Private Sub rbstruk_CheckedChanged(sender As Object, e As EventArgs) Handles rbstruk.CheckedChanged

    End Sub
    Private Sub cmbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcustomer.SelectedIndexChanged
        Call caripelanggan()
    End Sub

    Private Sub cmbcustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbcustomer.TextChanged
        Call caripelanggan()
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        Call carigudang()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
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
    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan ='" & cmbcustomer.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamabarang.Text = dr("nama_barang")
            txtkodebarang.Text = dr("kode_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            lblsatuanjual.Text = satuan
            jenis = dr("jenis_barang")
            txtharga.Text = Format(dr("harga_jual"), "##,##0")
            txtharga.SelectionStart = Len(txtharga.Text)
            modalpenjualan = Val(dr("modal_barang"))
        Else
            sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                txtnamabarang.Text = dr("nama_barang")
                txtkodebarang.Text = dr("kode_barang")
                satuan = dr("satuan_barang")
                lblsatuan.Text = satuan
                lblsatuanjual.Text = satuan
                jenis = dr("jenis_barang")
                txtharga.Text = Format(dr("harga_jual"), "##,##0")
                txtharga.SelectionStart = Len(txtharga.Text)
                modalpenjualan = Val(dr("modal_barang"))
            Else
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_kategori_barang ON tb_barang.kategori_barang = tb_kategori_barang.kode_kategori WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    txtnamabarang.Text = dr("nama_barang")
                    txtkodebarang.Text = dr("kode_barang")
                    satuan = dr("satuan_barang")
                    lblsatuan.Text = satuan
                    lblsatuanjual.Text = satuan
                    jenis = dr("jenis_barang")
                    txtharga.Text = Format(Val(dr("modal_barang")) + Val(dr("selisih_kategori")), "##,##0")
                    txtharga.SelectionStart = Len(txtharga.Text)
                    modalpenjualan = Val(dr("modal_barang"))
                Else

                    txtnamabarang.Text = ""
                    txtkodebarang.Text = ""
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
            If txtcustomer.Text IsNot "" Then
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
                If rbstruk.Checked Then
                    'Call cetak_struk()
                    Call PrintTransaksi()

                    sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE kode_penjualan = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    cbprinted.Checked = True
                Else
                    If rbfaktur.Checked Then
                        Call cetak_faktur()

                        sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE kode_penjualan = '" & txtnonota.Text & "' "
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        cbprinted.Checked = True
                    Else
                        If rbsurat.Checked Then
                            Call cetak_surat()

                            sql = "UPDATE tb_penjualan SET print_penjualan = 1 WHERE kode_penjualan = '" & txtnonota.Text & "' "
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            cbprinted.Checked = True
                        End If
                    End If
                End If
            Else
                MsgBox("Nota sudah void !")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub cetak_struk()
        Dim struk As String
        Dim tabel_struk As New DataTable
        With tabel_struk
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
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
            baris = tabel_struk.NewRow
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("banyak") = GridView1.GetRowCellValue(i, "banyak")
            baris("satuan") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis") = GridView1.GetRowCellValue(i, "jenis_barang")
            baris("harga_satuan") = GridView1.GetRowCellValue(i, "harga_satuan")
            baris("diskon_persen") = GridView1.GetRowCellValue(i, "diskon_persen")
            baris("harga_diskon") = GridView1.GetRowCellValue(i, "harga_diskon")
            baris("subtotal") = GridView1.GetRowCellValue(i, "subtotal")
            baris("diskon_nominal") = GridView1.GetRowCellValue(i, "diskon_nominal")
            tabel_struk.Rows.Add(baris)
        Next

        Call koneksii()
        sql = "SELECT * FROM tb_printer WHERE nomor='1'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            struk = dr("nama_printer")
        Else
            struk = ""
        End If

        Dim rpt As ReportDocument
        rpt = New Struk_Penjualan
        rpt.SetDataSource(tabel_struk)
        rpt.SetParameterValue("nofaktur", txtnonota.Text)
        rpt.SetParameterValue("kasir", fmenu.statususer.Text)
        rpt.SetParameterValue("customer", txtcustomer.Text)
        rpt.SetParameterValue("tgl", dtpenjualan.Text)
        rpt.PrintOptions.PrinterName = struk
        rpt.PrintToPrinter(1, False, 0, 0)
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
        With Me.PrintDocument1
            .PrinterSettings.PrinterName = struk
            .PrinterSettings.DefaultPageSettings.Landscape = False
            .Print()
        End With
        'MsgBox("Transaksi Tersimpan!", MsgBoxStyle.Exclamation, "Berhasil")
    End Sub
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'ambil data alamat
        Dim alamat, telp, rekening As String
        Dim countalamat, counttelp, countrekening, center As Integer

        Call koneksii()
        sql = "SELECT * FROM tb_info_perusahaan LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            alamat = dr("alamat")
            telp = dr("telepon")
            rekening = dr("rekening")
        Else
            alamat = ""
            telp = ""
            rekening = ""
        End If
        '==================

        countalamat = alamat.Length
        counttelp = telp.Length
        countrekening = rekening.Length

        'Dim ps As New Printing.PaperSize("paper", 100, 100)

        'e.PageSettings.PaperSize = ps
        'e.PageSettings.PaperSize.Height = 100
        'e.PageSettings.PaperSize.Width = 100
        'e.PageSettings.Landscape = False

        tinggi += 0
        e.Graphics.DrawString("SJT", New System.Drawing.Font("Arial", 16), Brushes.Black, 108, tinggi)

        center = Convert.ToSingle((e.PageBounds.Width / 3.5 - e.Graphics.MeasureString(alamat, New System.Drawing.Font("verdana", 6)).Width) / 2)

        tinggi += 25
        e.Graphics.DrawString(alamat, New System.Drawing.Font("verdana", 6), Brushes.Black, center + 4, tinggi)
        tinggi += 15
        e.Graphics.DrawString("Makassar", New System.Drawing.Font("verdana", 6), Brushes.Black, 108, tinggi)
        tinggi += 15
        e.Graphics.DrawString("Telp 085 363 930 370", New System.Drawing.Font("verdana", 6), Brushes.Black, 80, tinggi)

        tinggi += 15
        e.Graphics.DrawString("No.Nota", New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + txtnonota.Text, New System.Drawing.Font("verdana", 6), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Customer", New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + txtcustomer.Text, New System.Drawing.Font("verdana", 6), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Tanggal", New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + dtpenjualan.Text, New System.Drawing.Font("verdana", 6), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Kasir", New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + fmenu.statususer.Text, New System.Drawing.Font("verdana", 6), Brushes.Black, 60, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Sales", New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)
        e.Graphics.DrawString(": " + cmbsales.Text, New System.Drawing.Font("verdana", 6), Brushes.Black, 60, tinggi)

        tinggi += 5
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

        For itm As Integer = 0 To GridView1.RowCount - 1
            tinggi += 15
            e.Graphics.DrawString(GridView1.GetRowCellValue(itm, "nama_barang"), New System.Drawing.Font("verdana", 7), Brushes.Black, 8, tinggi)

            tinggi += 15
            e.Graphics.DrawString(FormatNumber(GridView1.GetRowCellValue(itm, "banyak").ToString, 0) + " " + GridView1.GetRowCellValue(itm, "satuan_barang") + " x " + FormatNumber(GridView1.GetRowCellValue(itm, "harga_diskon").ToString, 0), New System.Drawing.Font("verdana", 7), Brushes.Black, 16, tinggi)
            e.Graphics.DrawString(" = " + FormatNumber(GridView1.GetRowCellValue(itm, "subtotal").ToString, 0), New System.Drawing.Font("verdana", 7), Brushes.Black, 167, tinggi)
        Next

        tinggi += 5
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

        If cbdiskon.Checked = True Or cbppn.Checked = True Or cbongkir.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("Subtotal ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(totalbelanja, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)
        End If

        If cbdiskon.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("Diskon ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(txtdiskonnominal.Text, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)
        End If

        If cbppn.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("PPN ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(txtppnnominal.Text, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)
        End If

        If cbongkir.Checked = True Then
            tinggi += 15
            e.Graphics.DrawString("Ongkir ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 115, tinggi)
            e.Graphics.DrawString(": " + FormatNumber(txtongkir.Text, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)
        End If

        tinggi += 15
        e.Graphics.DrawString("Grandtotal ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + FormatNumber(txttotal.Text, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)

        tinggi += 15
        e.Graphics.DrawString("______________________", New System.Drawing.Font("Verdana", 7), Brushes.Black, 110, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Bayar ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + FormatNumber(bayar, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Sisa ", New System.Drawing.Font("Verdana", 7), Brushes.Black, 110, tinggi)
        e.Graphics.DrawString(": " + FormatNumber(sisa, 0), New System.Drawing.Font("Verdana", 7), Brushes.Black, 175, tinggi)


        tinggi += 30
        e.Graphics.DrawString("Keterangan :", New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)

        tinggi += 5
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)

        tinggi += 15
        e.Graphics.DrawString(txtketerangan.Text, New System.Drawing.Font("verdana", 6), Brushes.Black, 8, tinggi)

        tinggi += 50
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)


        tinggi += 30
        e.Graphics.DrawString("Terima Kasih Sudah berbelanja di toko kami", New System.Drawing.Font("verdana", 6), Brushes.Black, 35, tinggi)

        tinggi += 15
        e.Graphics.DrawString("Barang yang sudah di beli tidak dapat di kembalikan atau ", New System.Drawing.Font("verdana", 6), Brushes.Black, 2, tinggi)
        tinggi += 15
        e.Graphics.DrawString("di tukar dengan alasan apapun ", New System.Drawing.Font("verdana", 6), Brushes.Black, 60, tinggi)

        tinggi += 25
        e.Graphics.DrawString("Barang yang sudah di beli dianggap sudah di cek", New System.Drawing.Font("verdana", 6), Brushes.Black, 25, tinggi)

        tinggi += 40
        e.Graphics.DrawString("___________________________________________", New System.Drawing.Font("Arial Black", 8), Brushes.Black, 2, tinggi)
    End Sub

    Public Sub cetak_faktur()
        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
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
            baris("banyak") = GridView1.GetRowCellValue(i, "banyak")
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
        rpt_faktur.SetDataSource(tabel_faktur)

        rpt_faktur.SetParameterValue("nofaktur", kodepenjualan)
        rpt_faktur.SetParameterValue("namakasir", fmenu.statususer.Text)
        rpt_faktur.SetParameterValue("pembeli", txtcustomer.Text)
        rpt_faktur.SetParameterValue("jatem", dtjatuhtempo.Text)
        rpt_faktur.SetParameterValue("bayar", bayar)
        rpt_faktur.SetParameterValue("sisa", sisa)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("diskon", diskonnominal)
        rpt_faktur.SetParameterValue("grandtotal", grandtotal)
        rpt_faktur.SetParameterValue("ppn", ppnnominal)
        rpt_faktur.SetParameterValue("ongkir", ongkir)
        rpt_faktur.SetParameterValue("tanggal", dtpenjualan.Text)
        rpt_faktur.SetParameterValue("subtotal", totalbelanja)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Public Sub cetak_surat()
        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
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
            baris("banyak") = GridView1.GetRowCellValue(i, "banyak")
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
        rpt_faktur.SetDataSource(tabel_faktur)

        rpt_faktur.SetParameterValue("nofaktur", kodepenjualan)
        rpt_faktur.SetParameterValue("namakasir", fmenu.statususer.Text)
        rpt_faktur.SetParameterValue("pembeli", txtcustomer.Text)
        rpt_faktur.SetParameterValue("jatem", dtjatuhtempo.Text)
        rpt_faktur.SetParameterValue("bayar", bayar)
        rpt_faktur.SetParameterValue("sisa", sisa)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("diskon", diskonnominal)
        rpt_faktur.SetParameterValue("grandtotal", grandtotal)
        rpt_faktur.SetParameterValue("ppn", ppnnominal)
        rpt_faktur.SetParameterValue("ongkir", ongkir)
        rpt_faktur.SetParameterValue("tanggal", dtpenjualan.Text)
        rpt_faktur.SetParameterValue("subtotal", totalbelanja)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub
    Public Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
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
        Dim statuspiutang As Boolean

        If cbvoid.Checked = False Then
            Call koneksii()
            sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE kode_penjualan = '" & txtnonota.Text & "' LIMIT 1"
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
                        If txtcustomer.Text IsNot "" Then
                            If txtgudang.Text IsNot "" Then
                                If cmbsales.Text IsNot "" Then
                                    If txtrekening.Text IsNot "" Then

                                        'isi disini sub updatenya
                                        Call perbarui(txtnonota.Text)
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
            Call inisialisasi(kodepenjualan)
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
            sql = "SELECT kode_penjualan FROM tb_penjualan WHERE kode_penjualan  = '" + txtgopenjualan.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgopenjualan.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub


    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 2
        fcaricust.ShowDialog()
    End Sub
    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupstok = 1
        kodegudangcari = cmbgudang.Text
        fcaristok.ShowDialog()
    End Sub
    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 2
        fcarigudang.ShowDialog()
    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        Call caribarang()
    End Sub
    Sub tambah()
        Dim hargajuallangsung As Double
        'Columns.Add("kode_barang")
        'Columns.Add("kode_stok")
        'Columns.Add("nama_barang")
        'Columns.Add("banyak", GetType(Double))
        'Columns.Add("satuan_barang")
        'Columns.Add("jenis_barang")
        'Columns.Add("harga_satuan", GetType(Double))
        'Columns.Add("diskon_persen", GetType(Double))
        'Columns.Add("diskon_nominal", GetType(Double))
        'Columns.Add("harga_diskon", GetType(Double))
        'Columns.Add("subtotal", GetType(Double))
        'Columns.Add("laba", GetType(Double))
        'Columns.Add("modal_barang", GetType(Double))

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtharga.Text = "" Or txtbanyak.Text = "" Or banyak <= 0 Then
            MsgBox("Barang Kosong Atau Pricelist Group belum terisi", MsgBoxStyle.Information, "Informasi")
            'Exit Sub
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan='" & cmbcustomer.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    If dr("jumlah_stok") < banyak Then
                        MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                        'Exit Sub
                    Else
                        tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * banyak, (Val(dr("harga_jual")) * banyak - Val(modalpenjualan) * banyak), modalpenjualan)
                        Call reload_tabel()
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000'  AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        If dr("jumlah_stok") < banyak Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            'Exit Sub
                        Else
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * banyak, (Val(dr("harga_jual")) * banyak - Val(modalpenjualan) * banyak), modalpenjualan)
                            Call reload_tabel()
                        End If
                    Else
                        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_kategori_barang ON tb_barang.kategori_barang = tb_kategori_barang.kode_kategori WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        If dr.HasRows Then

                            If dr("jumlah_stok") < banyak Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                'Exit Sub
                            Else
                                hargajuallangsung = Val(dr("modal_barang")) + Val(dr("selisih_kategori"))
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("modal_barang")) + Val(dr("selisih_kategori")), "0", "0", hargajuallangsung, hargajuallangsung * banyak, (hargajuallangsung * banyak - Val(modalpenjualan) * banyak), modalpenjualan)
                                Call reload_tabel()
                            End If
                        Else
                            MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                        End If
                    End If
                End If

            Else 'kalau ada isi
                Dim tbbanyak As Integer = 0
                Dim tbnilaipersen As Double = 0
                Dim tbnilainominal As Double = 0
                Dim lokasi As Integer = -1

                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '" & cmbcustomer.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If GridView1.GetRowCellValue(i, "kode_stok").Equals(txtkodestok.Text) Then
                            lokasi = i
                        End If
                    Next

                    tbbanyak = GridView1.GetRowCellValue(lokasi, "banyak")
                    tbnilaipersen = GridView1.GetRowCellValue(lokasi, "diskon_persen")
                    tbnilainominal = GridView1.GetRowCellValue(lokasi, "diskon_nominal")

                    If lokasi = -1 Then
                        'cek 1
                        'MsgBox("cek 1")

                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                            Call reload_tabel()
                        End If
                    Else
                        'cek 2
                        'MsgBox("cek 2")

                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                            Call reload_tabel()
                        End If
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan='00000000' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        For i As Integer = 0 To GridView1.RowCount - 1
                            If GridView1.GetRowCellValue(i, "kode_stok").Equals(txtkodestok.Text) Then
                                lokasi = i
                            End If
                        Next

                        tbbanyak = GridView1.GetRowCellValue(lokasi, "banyak")
                        tbnilaipersen = GridView1.GetRowCellValue(lokasi, "diskon_persen")
                        tbnilainominal = GridView1.GetRowCellValue(lokasi, "diskon_nominal")

                        If lokasi = -1 Then
                            'cek 3
                            'MsgBox("cek 3")

                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                                Call reload_tabel()
                            End If
                        Else
                            'cek 4
                            'MsgBox("cek 4")

                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                                Call reload_tabel()
                            End If
                        End If
                    Else
                        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_kategori_barang ON tb_barang.kategori_barang = tb_kategori_barang.kode_kategori WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        If dr.HasRows Then

                            For i As Integer = 0 To GridView1.RowCount - 1
                                If GridView1.GetRowCellValue(i, "kode_stok").Equals(txtkodestok.Text) Then
                                    lokasi = i
                                End If
                            Next

                            tbbanyak = GridView1.GetRowCellValue(lokasi, "banyak")
                            tbnilaipersen = GridView1.GetRowCellValue(lokasi, "diskon_persen")
                            tbnilainominal = GridView1.GetRowCellValue(lokasi, "diskon_nominal")
                            hargajuallangsung = Val(dr("modal_barang")) + Val(dr("selisih_kategori"))

                            If lokasi = -1 Then
                                'cek 5
                                'MsgBox("cek 5")

                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, hargajuallangsung, tbnilaipersen, tbnilainominal, hargajuallangsung - tbnilainominal, (hargajuallangsung - tbnilainominal) * (banyak + tbbanyak), (Val(hargajuallangsung - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                                    Call reload_tabel()
                                End If
                            Else
                                'cek 6
                                'MsgBox("cek 6")

                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                    tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, hargajuallangsung, tbnilaipersen, tbnilainominal, hargajuallangsung - tbnilainominal, (hargajuallangsung - tbnilainominal) * (banyak + tbbanyak), (Val(hargajuallangsung - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
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
        If e.Column.FieldName = "banyak" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "banyak", 1)
            End If

            Try
                'GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * e.Value)
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
            BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If

        If e.Column.FieldName = "harga_satuan" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "harga_satuan", 1)
            End If
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", e.Value - GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "banyak") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "banyak"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
            BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If

        If e.Column.FieldName = "diskon_persen" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value / 100 * GridView1.GetRowCellValue(e.RowHandle, "harga_satuan"))
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "banyak") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "banyak"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
            BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If

        If e.Column.FieldName = "diskon_nominal" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon_persen", e.Value / GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") * 100%)
                GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "banyak") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "banyak"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
            BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If
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
                'GridView1.SetRowCellValue(e.RowHandle, "harga_satuan", GridView1.GetRowCellValue(e.RowHandle, "modal_barang"))
                MsgBox("Harga Dibawah Modal", MsgBoxStyle.Information, "Peringatan")
            End If
            'Try
            '    GridView1.SetRowCellValue(e.RowHandle, "diskon_nominal", GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
            '    GridView1.SetRowCellValue(e.RowHandle, "harga_diskon", e.Value - GridView1.GetRowCellValue(e.RowHandle, "diskon_persen") / 100 * e.Value)
            '    GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "banyak") * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
            '    GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "harga_diskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "banyak"))
            'Catch ex As Exception
            '    'error jika nulai qty=blank
            '    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            'End Try
            'BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If

    End Sub

    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub dtpenjualan_ValueChanged(sender As Object, e As EventArgs) Handles dtpenjualan.ValueChanged
        dtjatuhtempo.MinDate = dtpenjualan.Value
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

        kodepenjualan = autonumber()
        kodegudang = cmbgudang.Text
        Call koneksii()

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & kodegudang & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "INSERT INTO tb_penjualan_detail ( kode_penjualan, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & kodepenjualan & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & GridView1.GetRowCellValue(i, "diskon_persen") & "','" & GridView1.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "modal_barang") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        If bayar.Equals(grandtotal) Then
            lunasstatus = 1
        Else
            lunasstatus = 0
        End If

        sql = "INSERT INTO tb_penjualan (kode_penjualan, kode_pelanggan, kode_gudang, kode_user, tgl_penjualan, tgl_jatuhtempo_penjualan, lunas_penjualan, void_penjualan, print_penjualan, posted_penjualan, keterangan_penjualan, diskon_penjualan, pajak_penjualan, ongkir_penjualan, total_penjualan, metode_pembayaran, rekening, bayar_penjualan, sisa_penjualan, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepenjualan & "','" & cmbcustomer.Text & "','" & kodegudang & "','" & cmbsales.Text & "' , '" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & lunasstatus & "','" & 0 & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & txtdiskonpersen.Text & "','" & txtppnpersen.Text & "','" & ongkir & "','" & grandtotal & "','" & cmbpembayaran.Text & "', '" & txtrekening.Text & "','" & bayar & "','" & sisa & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        kodepembayaran = cmbpembayaran.Text

        If kodepembayaran IsNot "" Then
            sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_penjualan, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepembayaran & "','" & kodepenjualan & "', 'AWAL','" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "', 'Transaksi Nota Nomor " & kodepenjualan & "','" & sisa & "', '" & bayar & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
        Call inisialisasi(kodepenjualan)
    End Sub

    Sub perbarui(nomornota As String)
        kodepenjualan = nomornota
        kodepembayaran = cmbpembayaran.Text
        kodegudang = cmbgudang.Text

        'periksa di barang di stok dulu
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim stokdatabasesementara As Integer
        Dim namastokdatabase As String
        Dim statusavailable As Boolean = True

        Dim kodegudangupdate As String

        'cari nota  yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT kode_gudang FROM tb_penjualan WHERE kode_penjualan = '" & kodepenjualan & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodegudangupdate = dr("kode_gudang")

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & kodegudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                namastokdatabase = dr("nama_stok")

                'mengambil selisih qty dari penjualan detail
                sql = "SELECT * FROM tb_penjualan_detail WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_penjualan ='" & kodepenjualan & "' LIMIT 1"
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
                    MsgBox("Stok dengan nama stok " + namastokdatabase + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " + GridView1.GetRowCellValue(i, "kode_stok") + " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            'hapus di tabel jual detail
            Call koneksii()
            sql = "DELETE FROM tb_penjualan_detail where kode_penjualan = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'update stok kembalikan 
            Call koneksii()
            sql = "SELECT * FROM tb_penjualan_detail_sementara where kode_penjualan = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            While dr.Read
                sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & dr("qty") & "' WHERE kode_stok = '" & dr("kode_stok") & "' AND kode_gudang ='" & kodegudangupdate & "'"
                cmmd = New OdbcCommand(sql, cnn)
                drpenjualan = cmmd.ExecuteReader()
            End While

            'hapus panjar
            sql = "DELETE FROM tb_transaksi_kas where kode_penjualan = '" & nomornota & "' and jenis_kas ='AWAL'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()


            'hapus di tabel jual sementara
            Call koneksii()
            sql = "DELETE FROM tb_penjualan_detail_sementara where kode_penjualan = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()



            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & kodegudang & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Next

            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "INSERT INTO tb_penjualan_detail ( kode_penjualan, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, updated_by, last_updated) VALUES ('" & kodepenjualan & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & GridView1.GetRowCellValue(i, "diskon_persen") & "','" & GridView1.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "modal_barang") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "',now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Next

            If bayar.Equals(grandtotal) Then
                lunasstatus = 1
            Else
                lunasstatus = 0
            End If

            Call koneksii()
            sql = "UPDATE tb_penjualan SET kode_pelanggan ='" & cmbcustomer.Text & "', kode_gudang ='" & kodegudang & "', kode_user ='" & cmbsales.Text & "' , tgl_penjualan ='" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "', tgl_jatuhtempo_penjualan ='" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "', lunas_penjualan = '" & lunasstatus & "',keterangan_penjualan ='" & txtketerangan.Text & "', diskon_penjualan ='" & txtdiskonpersen.Text & "', pajak_penjualan ='" & txtppnpersen.Text & "', ongkir_penjualan ='" & ongkir & "', total_penjualan ='" & grandtotal & "',metode_pembayaran ='" & cmbpembayaran.Text & "',rekening ='" & txtrekening.Text & "', bayar_penjualan ='" & bayar & "', sisa_penjualan ='" & sisa & "', updated_by ='" & fmenu.statususer.Text & "', last_updated = now() WHERE kode_penjualan ='" & kodepenjualan & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            kodepembayaran = cmbpembayaran.Text

            If kodepembayaran IsNot "" Then
                sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_penjualan, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepembayaran & "','" & kodepenjualan & "', 'AWAL', now(), 'Transaksi Nota Nomor " & kodepenjualan & "','" & sisa & "', '" & bayar & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            End If

            MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(txtnonota.Text)
            'Call inisialisasi(nomornota)
            btnedit.Text = "Edit"
        End If
    End Sub
    Private Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
        "{impersonationLevel=impersonate}!\\" &
        computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
        "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
        cpu_ids.Substring(2)

        Return cpu_ids
    End Function
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
            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                If stokdatabase < stok Then
                    MsgBox("Stok dengan nama " + dr("nama_stok") + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                    'Exit Sub
                End If
            Else
                MsgBox("Kode Stok Barang ini " + GridView1.GetRowCellValue(i, "kode_stok") + " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
                'Exit Sub
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
        If txtbanyak.Text = "" Then
            txtbanyak.Text = 0
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