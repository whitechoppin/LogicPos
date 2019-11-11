Imports System.Data.Odbc
Imports DevExpress.Utils
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports CrystalDecisions.Shared
Public Class fpenjualan
    Public tabel As DataTable
    'variabel dalam penjualan
    Public jenis, satuan, kodepenjualan As String
    Dim totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir As Double

    Private Sub fpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        kodepenjualan = currentnumber()
        Call inisialisasi(kodepenjualan)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With

        Call comboboxcustomer()
        Call comboboxgudang()
        Call comboboxuser()
        Call comboboxpembayaran()
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
            cnn.Close()
        End Try
        Return pesan
    End Function

    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT kode_penjualan FROM tb_penjualan WHERE date_created < (SELECT date_created FROM tb_penjualan WHERE kode_penjualan = '" + previousnumber + "')ORDER BY date_created DESC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'Call inisialisasi(dr.Item(0).ToString)
            Else
                'Call inisialisasi(previousnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_penjualan FROM tb_penjualan WHERE date_created > (SELECT date_created FROM tb_penjualan WHERE kode_penjualan = '" + nextingnumber + "')ORDER BY date_created ASC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'Call inisialisasi(dr.Item(0).ToString)
            Else
                'Call inisialisasi(nextingnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub
    Sub previewpenjualan(lihat As String)
        sql = "SELECT * FROM tb_penjualan_detail WHERE kode_penjualan ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("harga_jual"), dr("diskon"), dr("harga_jual") - dr("diskon") / 100, dr("harga_diskon"), dr("subtotal"), 0, 0)
            GridControl1.RefreshDataSource()
        End While

    End Sub

    Sub comboboxcustomer()
        Call koneksii()
        cmmd = New OdbcCommand("SELECT * FROM tb_pelanggan", cnn)
        cmbcustomer.Items.Clear()
        cmbcustomer.AutoCompleteCustomSource.Clear()
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
        cmmd = New OdbcCommand("SELECT * FROM tb_gudang", cnn)
        cmbgudang.Items.Clear()
        cmbgudang.AutoCompleteCustomSource.Clear()
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
        cmmd = New OdbcCommand("SELECT * FROM tb_user", cnn)
        cmbsales.Items.Clear()
        cmbsales.AutoCompleteCustomSource.Clear()
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

        cmbpembayaran.Items.Add("KREDIT")

        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbpembayaran.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbpembayaran.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    'Sub cek_kas()
    '    Dim tgl As Date
    '    Dim tutupkas As Date
    '    Call koneksii()
    '    cmmd = New OdbcCommand("SELECT * FROM tb_historikas where idkasir=  '" & fmenu.statususer.Text & "' order by id desc limit 1", cnn)
    '    dr = cmmd.ExecuteReader()
    '    If dr.HasRows Then
    '        'MsgBox("cek kas 1")
    '        tgl = dr("bukakas")
    '        tutupkas = dr("tutupkas")
    '        'MsgBox(tgl)
    '        If tgl = "1/1/1990" Then
    '            'MsgBox("cek kas 1 1/2")
    '            Call fmodal.Show()
    '        Else

    '            If tutupkas <> "1/1/1990" Then
    '                'MsgBox("cek kas 1 1 fmodal")
    '                fmodal.Show()
    '                'sql = "INSERT INTO tb_historikas ( idkasir ) VALUES ( '" & fmenu.statususer.Text & "')"
    '                'cmmd = New OdbcCommand(sql, cnn)
    '                'dr = cmmd.ExecuteReader()
    '                'Call awal()
    '            Else
    '                'MsgBox("cek kas 1 1 awal")
    '                Call awal()
    '            End If

    '        End If
    '    Else
    '        'MsgBox("cek kas 2")
    '        'sql = "INSERT INTO tb_historikas ( idkasir ) VALUES ( '" & fmenu.statususer.Text & "')"
    '        'cmmd = New OdbcCommand(sql, cnn)
    '        'dr = cmmd.ExecuteReader()
    '        fmodal.Show()
    '    End If
    '    With GridView1
    '        .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
    '        'buat sum harga
    '        .Columns("Subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Subtotal", "{0:n0}")
    '    End With
    'End Sub

    Sub awalbaru()
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = False
        btnsimpan.Enabled = True
        btnprint.Enabled = False
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgopembelian.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbcustomer.Enabled = True
        cmbcustomer.SelectedIndex = -1
        cmbcustomer.Focus()
        btncaricustomer.Enabled = True

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtpenjualan.Enabled = True
        dtpenjualan.Value = Date.Now

        dtjatuhtempo.Enabled = True
        dtjatuhtempo.Value = Date.Now

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

        cmbpembayaran.SelectedIndex = -1
        cmbpembayaran.Enabled = True
        btncarikas.Enabled = True

        txtrekening.Clear()

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
        btnbatal.Enabled = False

        'button navigations
        btnprev.Enabled = True
        btngo.Enabled = True
        txtgopembelian.Enabled = True
        btnnext.Enabled = True

        rbfaktur.Checked = True
        rbstruk.Checked = False

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
        dtjatuhtempo.Value = Date.Now

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

        txtbayar.Clear()
        txtbayar.Text = 0
        txtbayar.Enabled = False
        txtsisa.Clear()
        txtsisa.Text = 0

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
                    nilaidiskon = dr("diskon_penjualan")
                    nilaippn = dr("pajak_penjualan")
                    nilaiongkir = dr("ongkir_penjualan")

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

                    'cmbbayar.SelectedIndex = -1
                    cnn.Close()
                End If
            End Using
        End If

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
        If cmbpembayaran.Text.Equals("KREDIT") Then
            txtrekening.Text = "KREDIT"
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbpembayaran.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                txtrekening.Text = dr("nama_kas")
            Else
                txtrekening.Text = ""
            End If
        End If
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
        txtbayar.Enabled = True
        Call caripembayaran()
    End Sub

    Private Sub cmbpembayaran_TextChanged(sender As Object, e As EventArgs) Handles cmbpembayaran.TextChanged
        Call caripembayaran()
    End Sub

    Private Sub ritediskonpersen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritediskonpersen.KeyPress
        e.Handled = ValidAngka(e)

    End Sub
    Private Sub ritediskonnominal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritediskonnominal.KeyPress
        e.Handled = ValidAngka(e)

    End Sub
    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan ='" & cmbcustomer.Text & "'"
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
            modalpenjualan = dr("modal_barang")
        Else
            sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000'"
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
                modalpenjualan = dr("modal_barang")
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
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        'If pembayaran = "Home Credit" Or pembayaran = "Spektra" Or pembayaran = "Adira" Or pembayaran = "Kredit Plus" Then
        '    Call proses()
        'Else
        '    If bayar < total2 Then
        '        MsgBox("Pembayaran tidak mencukupi")
        '    Else
        '        Call proses()
        '    End If
        'End If
        Call proses()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call inisialisasi(kodepenjualan)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

    End Sub


    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 2
        fcaricust.ShowDialog()
    End Sub
    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupstok = 1
        fcaristok.ShowDialog()
    End Sub
    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 2
        fcarigudang.ShowDialog()
    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        'isi = txtkodeitem.Text
        'isicari = isi
        'If Strings.Left(txtkodeitem.Text, 1) Like "[A-Z, a-z]" Then

        '    Call search()

        '    'fcaribarang.txtcari.Focus()
        '    'fcaribarang.txtcari.DeselectAll()
        'Else
        '    Call cari()

        'End If

        Call caribarang()
    End Sub
    Sub tambah()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtharga.Text = "" Or txtbanyak.Text = "" Then
            MsgBox("Barang Kosong", MsgBoxStyle.Information, "Informasi")
            Exit Sub
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan='" & cmbcustomer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                        MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                        Exit Sub
                    Else
                        tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modalpenjualan) * Val(txtbanyak.Text)), modalpenjualan)
                        Call reload_tabel()
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                        MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Exit Sub
                    Else
                        tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modalpenjualan) * Val(txtbanyak.Text)), modalpenjualan)
                        Call reload_tabel()
                    End If
                End If

            Else 'kalau ada isi
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '" & cmbcustomer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If txtkodestok.Text = GridView1.GetRowCellValue(i, "kode_stok") Then
                            Dim banyak As Integer
                            Dim diskon As Double
                            Dim hargadiskon As Double
                            banyak = GridView1.GetRowCellValue(i, "banyak")
                            diskon = GridView1.GetRowCellValue(i, "diskon_persen")
                            hargadiskon = GridView1.GetRowCellValue(i, "harga_diskon")


                            If dr("jumlah_stok") + banyak < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(i))
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modalpenjualan) * Val(txtbanyak.Text)), modalpenjualan)
                                Call reload_tabel()
                                Exit Sub
                            End If
                        Else
                            If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modalpenjualan) * Val(txtbanyak.Text)), modalpenjualan)
                                Call reload_tabel()
                                Exit Sub
                            End If
                        End If
                    Next
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan='00000000'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If txtkodestok.Text = GridView1.GetRowCellValue(i, "kode_barang") Then
                            Dim banyak As Integer
                            Dim diskon As Double
                            Dim hargadiskon As Double
                            banyak = GridView1.GetRowCellValue(i, "banyak")
                            diskon = GridView1.GetRowCellValue(i, "diskon")
                            hargadiskon = GridView1.GetRowCellValue(i, "hargadiskon")


                            If dr("jumlah_stok") + banyak < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(i))
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modalpenjualan) * Val(txtbanyak.Text)), modalpenjualan)
                                Call reload_tabel()
                                Exit Sub
                            End If
                        Else
                            If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modalpenjualan) * Val(txtbanyak.Text)), modalpenjualan)
                                Call reload_tabel()
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "banyak" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "harga_diskon"))
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
        If e.KeyCode = Keys.Delete Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
    Private Sub GridView1_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles GridView1.RowUpdated
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub
    'Sub ambil_total()
    'total2 = GridView1.Columns("subtotal").SummaryItem.SummaryValue 'ambil isi summary gridview
    'total3 = total2
    'txttotal.Text = Format(total2, "##,##0")
    'txttotal.SelectionStart = Len(txttotal.Text)
    'lbltotal.Text = Format(total2, "##,##0")
    'End Sub

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

    Private Sub txtbayar_TextChanged(sender As Object, e As EventArgs) Handles txtbayar.TextChanged
        'If txtbayar.Text.Length = 0 Then
        'txtbayar.Text = 0
        'bayar = txtbayar.Text
        'txtbayar.Text = Format(bayar, "##,##0")
        'txtbayar.SelectionStart = Len(txtbayar.Text)
        'kembali = bayar - total2
        'sisa = total2 - bayar

        'txtkembali.Text = Format(kembali, "##,##0")
        'txtsisa.Text = Format(sisa, "##,##0")
        'lblkembali.Text = "Rp " + Format(kembali, "##,##0")
        'lblbayar.Text = "Rp " + Format(bayar, "##,##0")
        'Else
        'bayar = txtbayar.Text
        'txtbayar.Text = Format(bayar, "##,##0")
        'txtbayar.SelectionStart = Len(txtbayar.Text)
        'kembali = bayar - total2
        'sisa = total2 - bayar

        'txtkembali.Text = Format(kembali, "##,##0")
        'txtsisa.Text = Format(sisa, "##,##0")
        'lblkembali.Text = "Rp " + Format(hasil, "##,##0")
        'lblbayar.Text = "Rp " + Format(bayar, "##,##0")
        'End If
        If txtbayar.Text = "" Then
            txtbayar.Text = 0
        Else
            bayar = txtbayar.Text
            txtbayar.Text = Format(bayar, "##,##0")
            txtbayar.SelectionStart = Len(txtbayar.Text)
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

    'Sub hitung()
    'Dim total1 As Double
    'For i As Integer = 0 To GridView1.RowCount - 1
    'total1 = total1 + GridView1.GetRowCellValue(i, "subtotal")
    'Next
    'total3 = total1
    'total1 = total1 - diskon
    'txttotal.Text = Format(total1, "##,##0")
    'total2 = total1
    'lbltotal.Text = Format(total1, "##,##0")
    'kembali = bayar - total2
    'txtkembali.Text = Format(kembali, "##,##0")
    'End Sub

    'Sub cetak_faktur()
    '    Dim tabel2 As New DataTable
    '    With tabel2
    '        .Columns.Add("Nama")
    '        .Columns.Add("Banyak", GetType(Double))
    '        .Columns.Add("Satuan")
    '        .Columns.Add("harga", GetType(Double))
    '        .Columns.Add("diskon", GetType(Double))
    '        .Columns.Add("hargadiskon", GetType(Double))
    '        .Columns.Add("Subtotal", GetType(Double))
    '        .Columns.Add("kode")
    '        .Columns.Add("imei")
    '    End With

    '    Dim baris As DataRow
    '    For i As Integer = 0 To GridView1.RowCount - 1
    '        baris = tabel2.NewRow
    '        baris("Nama") = GridView1.GetRowCellValue(i, "Nama")
    '        baris("Banyak") = GridView1.GetRowCellValue(i, "Banyak")
    '        baris("Satuan") = GridView1.GetRowCellValue(i, "Satuan")
    '        baris("harga") = GridView1.GetRowCellValue(i, "harga")
    '        baris("diskon") = GridView1.GetRowCellValue(i, "diskon")
    '        baris("hargadiskon") = GridView1.GetRowCellValue(i, "hargadiskon")
    '        baris("Subtotal") = GridView1.GetRowCellValue(i, "Subtotal")
    '        baris("kode") = GridView1.GetRowCellValue(i, "kode")
    '        baris("imei") = GridView1.GetRowCellValue(i, "imei")
    '        tabel2.Rows.Add(baris)
    '    Next

    '    Dim rpt As ReportDocument
    '    rpt = New fakturpenjualan
    '    rpt.SetDataSource(tabel2)
    '    rpt.SetParameterValue("tanggal", tgl)
    '    rpt.SetParameterValue("total", total2)
    '    rpt.SetParameterValue("nofaktur", autonumber)
    '    rpt.SetParameterValue("kasir", fmenu.statususer.Text)
    '    rpt.SetParameterValue("bayar", pembayaran)
    '    'If cash = True Then
    '    '    rpt.SetParameterValue("bayar", "CASH")
    '    'Else
    '    '    rpt.SetParameterValue("bayar", "CREDIT")
    '    'End If

    '    'fakturjual.CrystalReportViewer1.ReportSource = rpt
    '    'rpt.PrintOptions.PrinterName = faktur
    '    rpt.PrintToPrinter(1, False, 0, 0)
    '    'fakturjual.ShowDialog()
    '    'fakturjual.Dispose()
    'End Sub
    'Sub cetak_struk()
    '    Dim tabel2 As New DataTable
    '    With tabel2
    '        .Columns.Add("Nama")
    '        .Columns.Add("Banyak", GetType(Double))
    '        .Columns.Add("Satuan")
    '        .Columns.Add("harga", GetType(Double))
    '        .Columns.Add("diskon", GetType(Double))
    '        .Columns.Add("hargadiskon", GetType(Double))
    '        .Columns.Add("Subtotal", GetType(Double))
    '        .Columns.Add("kode")
    '        .Columns.Add("imei")
    '    End With

    '    Dim baris As DataRow
    '    For i As Integer = 0 To GridView1.RowCount - 1
    '        baris = tabel2.NewRow
    '        baris("Nama") = GridView1.GetRowCellValue(i, "Nama")
    '        baris("Banyak") = GridView1.GetRowCellValue(i, "Banyak")
    '        baris("Satuan") = GridView1.GetRowCellValue(i, "Satuan")
    '        baris("harga") = GridView1.GetRowCellValue(i, "harga")
    '        baris("diskon") = GridView1.GetRowCellValue(i, "diskon")
    '        baris("hargadiskon") = GridView1.GetRowCellValue(i, "hargadiskon")
    '        baris("Subtotal") = GridView1.GetRowCellValue(i, "Subtotal")
    '        baris("kode") = GridView1.GetRowCellValue(i, "kode")
    '        baris("imei") = GridView1.GetRowCellValue(i, "imei")
    '        tabel2.Rows.Add(baris)
    '    Next

    '    Dim rpt As ReportDocument
    '    rpt = New rptstruk1
    '    rpt.SetDataSource(tabel2)
    '    'rpt.SetParameterValue("total", total2)
    '    rpt.SetParameterValue("nofaktur", autonumber)
    '    rpt.SetParameterValue("kasir", fmenu.statususer.Text)

    '    'If cash = True Then
    '    '    rpt.SetParameterValue("bayar", "CASH")
    '    'Else
    '    '    rpt.SetParameterValue("bayar", "CREDIT")
    '    'End If

    '    'fakturjual.CrystalReportViewer1.ReportSource = rpt
    '    'rpt.PrintOptions.PrinterName = faktur
    '    rpt.PrintToPrinter(1, False, 0, 0)
    '    'fakturjual.ShowDialog()
    '    'fakturjual.Dispose()
    'End Sub
    Sub simpan()
        Dim kodepenjualan As String
        kodepenjualan = autonumber()
        Call koneksii()

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "INSERT INTO tb_penjualan_detail ( kode_penjualan, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & kodepenjualan & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & GridView1.GetRowCellValue(i, "diskon") & "','" & GridView1.GetRowCellValue(i, "harga_diskon") & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "modal") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "INSERT INTO tb_penjualan (kode_penjualan, kode_pelanggan, kode_gudang, kode_user, tgl_penjualan, tgl_jatuhtempo_penjualan, lunas_penjualan, void_penjualan, print_penjualan, posted_penjualan, keterangan_penjualan, diskon_penjualan, pajak_penjualan, ongkir_penjualan, total_penjualan, metode_pembayaran, rekening, bayar_penjualan, sisa_penjualan, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepenjualan & "','" & cmbcustomer.Text & "','" & cmbgudang.Text & "','" & cmbsales.Text & "' , '" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & txtdiskonpersen.Text & "','" & txtppnpersen.Text & "','" & ongkir & "','" & grandtotal & "','" & cmbpembayaran.Text & "', '" & txtrekening.Text & "','" & bayar & "','" & sisa & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        ''MsgBox(metode)
        'If metode = "pembiayaan" Then
        '    'MsgBox("bayar pembiayaan")
        '    sql = "UPDATE tb_kas SET nontunai = nontunai + '" & total2 - bayar & "', tunai = tunai + '" & bayar & "' WHERE iduser = '" & fmenu.statususer.Text & "'"
        '    cmmd = New OdbcCommand(sql, cnn)
        '    dr = cmmd.ExecuteReader()
        'Else
        '    If metode = "CASH" Then
        '        'MsgBox("byr cash")
        '        sql = "UPDATE tb_kas SET tunai = tunai + '" & total2 & "' WHERE iduser = '" & fmenu.statususer.Text & "'"
        '        cmmd = New OdbcCommand(sql, cnn)
        '        dr = cmmd.ExecuteReader()
        '    Else
        '        'MsgBox("byr non tunai")
        '        sql = "UPDATE tb_kas SET nontunai = nontunai + '" & total2 & "' WHERE iduser = '" & fmenu.statususer.Text & "'"
        '        cmmd = New OdbcCommand(sql, cnn)
        '        dr = cmmd.ExecuteReader()
        '    End If
        'End If

        MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
        Call inisialisasi("123456")
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
        For i As Integer = 0 To GridView1.RowCount - 1
            Call koneksii()
            sql = "SELECT * FROM tb_stok JOIN tb_barang ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok ='" & GridView1.GetRowCellValue(i, "kode_stok") & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            Dim stok As Integer
            Dim stok1 As Integer
            stok = GridView1.GetRowCellValue(i, "banyak")
            stok1 = dr("jumlah_stok")
            If stok1 < stok Then
                MsgBox("Stok " + dr("nama_barang") + "dengan kode stok " + dr("kode_stok") + " tidak mencukupi", MsgBoxStyle.Information, "Information")
                Exit Sub
            End If
        Next

        Call simpan()
        ''If rbfaktur.Checked = True Then
        ''    Call cetak_faktur()
        ''    Call save()
        ''Else
        ''    Call cetak_struk()
        ''    Call save()
        ''End If

        ''Call cetak_struk()
        ''Call save()
        'fmsgbox.ShowDialog()
    End Sub

    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        e.Handled = ValidAngka(e)
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