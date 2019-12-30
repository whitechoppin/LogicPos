Imports System.Data.Odbc
Imports DevExpress.Utils
Imports CrystalDecisions.CrystalReports.Engine

Public Class fpembelian
    Dim tabel As DataTable
    Dim harga, modalpembelian, ongkir, ppn, diskonpersen, diskonnominal, ppnpersen, ppnnominal, subtotalsummary, grandtotal, banyak As Double
    Dim satuan, jenis, supplier, kodepembelian As String
    Public isi As String
    'variabel bantuan view pembelian
    Dim nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan, viewbayar, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpembelian, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir As Double
    Private Sub fpembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'mulai
        kodepembelian = currentnumber()
        Call inisialisasi(kodepembelian)
        With GridView1
            '.OptionsView.ColumnAutoWidth = False ' agar muncul scrol bar
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
            '.OptionsView.ShowAutoFilterRow = True 'aktifkan autofilter
            '.OptionsView.EnableAppearanceOddRow = True 'aktifkan style
            '.OptionsPrint.EnableAppearanceOddRow = True 'aktifkan style saat print
        End With
    End Sub
    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_pembelian,3) FROM tb_pembelian WHERE DATE_FORMAT(MID(`kode_pembelian`, 3 , 6), ' %y ')+ MONTH(MID(`kode_pembelian`,3 , 6)) + DAY(MID(`kode_pembelian`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_pembelian,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "BL" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "BL" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "BL" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "BL" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_pembelian FROM tb_pembelian ORDER BY kode_pembelian DESC LIMIT 1;"
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
        sql = "SELECT kode_pembelian FROM tb_pembelian WHERE date_created < (SELECT date_created FROM tb_pembelian WHERE kode_pembelian = '" + previousnumber + "')ORDER BY date_created DESC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Call inisialisasi(dr.Item(0).ToString)
            Else
                Call inisialisasi(previousnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_pembelian FROM tb_pembelian WHERE date_created > (SELECT date_created FROM tb_pembelian WHERE kode_pembelian = '" + nextingnumber + "')ORDER BY date_created ASC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Call inisialisasi(dr.Item(0).ToString)
            Else
                Call inisialisasi(nextingnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub
    Sub previewpembelian(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian_detail WHERE kode_pembelian ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("harga_beli"), dr("subtotal"))
            GridControl1.RefreshDataSource()
        End While
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
        txtgopembelian.Enabled = True
        btnnext.Enabled = True

        'buat tabel
        Call tabel_utama()

        Call koneksii()
        'bersihkan keranjang belanja
        sql = "DELETE FROM tb_pembelian_sementara" 'clear data
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbsupplier.Enabled = False
        btncarisupplier.Enabled = False
        txtsupplier.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False
        txtgudang.Enabled = False

        dtpembelian.Enabled = False
        dtpembelian.Value = Date.Now

        dtjatuhtempo.Enabled = False
        dtjatuhtempo.Value = dtpembelian.Value

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False

        btncari.Enabled = False

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = False

        lblsatuan.Text = "satuan"
        lblsatuanbeli.Text = "satuan"

        btntambahbarang.Enabled = False
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        'total tabel pembelian
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

        cmbbayar.Enabled = False

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_pembelian WHERE kode_pembelian = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomornota = dr("kode_pembelian")
                    nomorsupplier = dr("kode_supplier")
                    nomorsales = dr("kode_user")
                    nomorgudang = dr("kode_gudang")
                    statuslunas = dr("lunas_pembelian")
                    statusvoid = dr("void_pembelian")
                    statusprint = dr("print_pembelian")
                    statusposted = dr("posted_pembelian")
                    viewtglpembelian = dr("tgl_pembelian")
                    viewtgljatuhtempo = dr("tgl_jatuhtempo_pembelian")
                    viewketerangan = dr("keterangan_pembelian")
                    nilaidiskon = dr("diskon_pembelian")
                    nilaippn = dr("pajak_pembelian")
                    nilaiongkir = dr("ongkir_pembelian")
                    viewbayar = dr("pembayaran_pembelian")

                    txtnonota.Text = nomornota
                    cmbsupplier.Text = nomorsupplier
                    cmbsales.Text = nomorsales
                    cmbgudang.Text = nomorgudang
                    cblunas.Checked = statuslunas
                    cbvoid.Checked = statusvoid
                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    dtpembelian.Value = viewtglpembelian
                    dtjatuhtempo.Value = viewtgljatuhtempo

                    'isi tabel view pembelian

                    Call previewpembelian(nomorkode)

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

                    cmbbayar.Text = viewbayar

                    cnn.Close()
                End If
            End Using
        Else
            txtnonota.Clear()
            cmbsupplier.Text = ""
            cmbsales.Text = ""
            cmbgudang.Text = ""
            cblunas.Checked = False
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            dtpembelian.Value = Date.Now
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

            cmbbayar.Text = ""
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
        txtgopembelian.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.SelectedIndex = 0
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True
        txtsupplier.Enabled = False

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = 0
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtpembelian.Enabled = True
        dtpembelian.Value = Date.Now

        dtjatuhtempo.Enabled = True
        dtjatuhtempo.Value = dtpembelian.Value

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncari.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        lblsatuan.Text = "satuan"
        lblsatuanbeli.Text = "satuan"

        btntambahbarang.Enabled = True

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

        cmbbayar.SelectedIndex = -1
        cmbbayar.Enabled = True

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        'buat tabel
        Call tabel_utama()

        'bersihkan keranjang belanja
        sql = "DELETE FROM tb_pembelian_sementara" 'clear data
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        statusedit = False
    End Sub
    Sub tabel_utama()
        tabel = New DataTable
        With tabel
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))

        End With
        GridControl1.DataSource = tabel
        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Caption = "Kode Stok"
        GridColumn1.Width = 30

        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.Visible = False

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 70

        GridColumn4.Caption = "Qty"
        GridColumn4.FieldName = "qty"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn5.Width = 30

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "harga"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 50

        GridColumn8.FieldName = "subtotal"
        GridColumn8.Caption = "Subtotal"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 55
    End Sub
    Sub comboboxsupplier()
        Call koneksii()
        cmmd = New OdbcCommand("SELECT * FROM tb_supplier", cnn)
        cmbsupplier.Items.Clear()
        cmbsupplier.AutoCompleteCustomSource.Clear()
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsupplier.AutoCompleteCustomSource.Add(dr("kode_supplier"))
                cmbsupplier.Items.Add(dr("kode_supplier"))
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
        cmbbayar.Items.Clear()
        cmbbayar.AutoCompleteCustomSource.Clear()
        cmbbayar.AutoCompleteCustomSource.Add("TUNAI")
        cmbbayar.Items.Add("TUNAI")
        sql = "SELECT * FROM tb_rekening_supplier WHERE kode_supplier='" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbbayar.AutoCompleteCustomSource.Add(dr("nama_bank") + " - " + dr("nomor_rekening") + " - " + dr("nama_rekening"))
                cmbbayar.Items.Add(dr("nama_bank") + " - " + dr("nomor_rekening") + " - " + dr("nama_rekening"))
            End While
        End If
    End Sub
    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang='" & txtkodebarang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamabarang.Text = dr("nama_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            lblsatuanbeli.Text = satuan
            jenis = dr("jenis_barang")
            modalpembelian = dr("modal_barang")
            txthargabarang.Text = Format(modalpembelian, "##,##0")
        Else
            txtnamabarang.Text = ""
            lblsatuan.Text = "satuan"
            lblsatuanbeli.Text = "satuan"
            txthargabarang.Text = 0
        End If
    End Sub
    Sub carisupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier='" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtsupplier.Text = dr("nama_supplier")
        Else
            txtsupplier.Text = ""
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
    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        'bersihkan textbox
        lblsatuan.Text = "satuan"
        lblsatuanbeli.Text = "satuan"
        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyakbarang.Clear()
        txthargabarang.Clear()
        txtnamabarang.Enabled = False
        txtkodebarang.Focus()
    End Sub
    Sub tambah()
        Dim kode_stok, counter_angka As String
        Dim total_karakter, total_karakter_kode, tambah_counter As Integer

        Call koneksii()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txthargabarang.Text = "" Or txtbanyakbarang.Text = "" Or banyak <= 0 Then
            Exit Sub
        End If

        If GridView1.RowCount = 0 Then  'data tidak ada
            If lblsatuan.Text = "Pcs" Then
                'tambahkan data ke tabel keranjang
                tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(banyak) * Val(harga))
                Call reload_tabel()
            Else

                'cek ke database stok untuk mendapatkan kode stok baru
                sql = "SELECT *, REPLACE(kode_stok, '" & txtkodebarang.Text & "', '') FROM tb_stok WHERE kode_barang = '" & txtkodebarang.Text & "'  ORDER BY REPLACE(kode_stok, '" & txtkodebarang.Text & "', '') DESC LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                If dr.HasRows Then 'kalau sdh ada stok sebelumnya
                    'tambahkan data
                    kode_stok = dr("kode_stok")
                    total_karakter = Len(kode_stok)
                    total_karakter_kode = Len(txtkodebarang.Text)
                    counter_angka = CInt(Microsoft.VisualBasic.Right(kode_stok, total_karakter - total_karakter_kode))
                    tambah_counter = counter_angka + 1
                    tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(banyak) * Val(harga))

                    Call koneksii()
                    sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(banyak) * Val(harga) & "' ,'1')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    Call reload_tabel()
                Else 'kalau belum ada stok
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text + "1", txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(banyak) * Val(harga))

                    'simpan kedalam tabel pembelian sementara agar kode dapat dilanjutkan
                    Call koneksii()
                    sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang, qty, satuan_barang, jenis_barang, harga_satuan, subtotal, nomor) VALUES ('" & txtkodebarang.Text + "1" & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(banyak) * Val(harga) & "' ,'1')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    Call reload_tabel()
                End If
            End If
        Else 'data ada
            Dim lokasi As Integer = -1
            Dim qty1 As Integer
            If GridView1.RowCount <> 0 Then
                'MsgBox("data ada")
                If lblsatuan.Text = "Pcs" Then
                    'MsgBox("ini pcs")
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If GridView1.GetRowCellValue(i, "kode_barang") = txtkodebarang.Text And GridView1.GetRowCellValue(i, "satuan_barang").Equals("Pcs") Then
                            'MsgBox(GridView1.GetRowCellValue(i, "kode_barang"))
                            lokasi = i
                        End If
                    Next
                    If lokasi = -1 Then
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(banyak) * Val(harga))
                        Call reload_tabel()
                    Else
                        qty1 = GridView1.GetRowCellValue(lokasi, "qty")
                        GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak + qty1), satuan, jenis, Val(harga), (Val(banyak) + qty1) * Val(harga))
                        Call reload_tabel()

                    End If
                Else
                    'MsgBox("bukan Pcs")
                    Call koneksii()
                    sql = "SELECT * FROM tb_pembelian_sementara WHERE kode_barang= '" & txtkodebarang.Text & "' ORDER BY nomor DESC LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        'ada data
                        kode_stok = dr("kode_stok")
                        total_karakter = Len(dr("kode_stok"))
                        total_karakter_kode = Len(txtkodebarang.Text)
                        counter_angka = CInt(Microsoft.VisualBasic.Right(kode_stok, total_karakter - total_karakter_kode))
                        tambah_counter = counter_angka + 1
                        tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(txtbanyakbarang.Text) * Val(harga))


                        sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & " ', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(banyak) * Val(harga) & "', '" & tambah_counter & "')"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        Call reload_tabel()
                    Else
                        'tidak ada data
                        'sql = "SELECT *, REPLACE(kode_stok, '" & txtkodebarang.Text & "', '') FROM tb_stok WHERE kode_barang = '" & txtkodebarang.Text & "'  ORDER BY REPLACE(kode_stok, '" & txtkodebarang.Text & "', '') DESC LIMIT 1"
                        sql = "SELECT *, (substring(kode_stok,LENGTH(kode_barang)+1)) as nomor, LENGTH(kode_barang) as panjang  FROM tb_stok WHERE kode_barang= '" & txtkodebarang.Text & "' ORDER BY nomor + 0 DESC LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)

                        dr = cmmd.ExecuteReader()

                        If dr.HasRows Then
                            'tambahkan data
                            'kode_stok = dr("kode_stok")
                            'total_karakter = Len(kode_stok)
                            'total_karakter_kode = Len(txtkodebarang.Text)
                            'counter_angka = CInt(Microsoft.VisualBasic.Right(kode_stok, total_karakter - total_karakter_kode))
                            tambah_counter = dr("nomor") + 1
                            tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(banyak) * Val(harga))

                            Call koneksii()
                            sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(banyak) * Val(harga) & "' ,'1')"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            Call reload_tabel()
                        Else
                            tabel.Rows.Add(txtkodebarang.Text + "1", txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(harga), Val(banyak) * Val(harga))

                            sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,harga_satuan,subtotal,nomor) VALUES ('" & txtkodebarang.Text + "1" & "', '" & txtkodebarang.Text & " ', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & Val(harga) & "','" & Val(banyak) * Val(harga) & "', '" & tambah_counter & "')"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            Call reload_tabel()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged, txtnonota.TextChanged
        Call caribarang()
    End Sub
    Private Sub txthargabarang_TextChanged(sender As Object, e As EventArgs) Handles txthargabarang.TextChanged
        If txthargabarang.Text = "" Then
            txthargabarang.Text = 0
        Else
            harga = txthargabarang.Text
            txthargabarang.Text = Format(harga, "##,##0")
            txthargabarang.SelectionStart = Len(txthargabarang.Text)
        End If
    End Sub
    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub
    Sub simpan()
        kodepembelian = autonumber()
        Call koneksii()

        sql = "SELECT * FROM tb_supplier  WHERE '" & cmbsupplier.Text & "' =  kode_supplier"
        'Strings.Right()
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        If dr.HasRows = 0 Then
            MsgBox("Nama Supplier Tidak Ditemukan!")
            Exit Sub
        Else
            If GridView1.RowCount = 0 Then
                MsgBox("Data masih kosong")
                Exit Sub
            Else
                For i As Integer = 0 To GridView1.RowCount - 1
                    sql = "INSERT INTO tb_pembelian_detail (kode_pembelian, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty,harga_beli, subtotal,created_by, updated_by,date_created, last_updated) VALUES ('" & kodepembelian & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "', '" & GridView1.GetRowCellValue(i, "harga") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                    cmmd = New OdbcCommand(sql, cnn)
                    'cnn.Open()
                    dr = cmmd.ExecuteReader()
                Next
                sql = "INSERT INTO tb_pembelian (kode_pembelian,kode_supplier,kode_gudang,kode_user,tgl_pembelian,tgl_jatuhtempo_pembelian,lunas_pembelian,void_pembelian,print_pembelian,posted_pembelian,keterangan_pembelian,diskon_pembelian,pajak_pembelian,ongkir_pembelian,total_pembelian,pembayaran_pembelian,created_by, updated_by,date_created, last_updated) VALUES ('" & kodepembelian & "','" & cmbsupplier.Text & "','" & cmbgudang.Text & "','" & cmbsales.Text & "','" & Format(dtpembelian.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & txtdiskonpersen.Text & "','" & txtppnpersen.Text & "','" & txtongkir.Text & "','" & grandtotal & "', '" & cmbbayar.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                For i As Integer = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                        sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbgudang.Text & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                        If dr.HasRows Then
                            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbgudang.Text & "'"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()
                        Else
                            sql = "INSERT INTO tb_stok (kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbgudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()
                        End If

                        sql = "UPDATE tb_barang SET modal_barang = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode_barang = '" & GridView1.GetRowCellValue(i, "kode_barang") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                    Else
                        sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbgudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        'update modal pada barang apabila ada harga baru
                        sql = "UPDATE tb_barang SET modal_barang = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode_barang = '" & GridView1.GetRowCellValue(i, "kode_barang") & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If
                Next

                kodepembayaran = cmbbayar.Text

                If kodepembayaran IsNot "" Then
                    sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_pembelian, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepembayaran & "','" & kodepembelian & "', 'AWAL', now(), 'Transaksi Nota Nomor " & kodepembelian & "','" & 0 & "', '" & 0 & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                End If

                MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")

                Call inisialisasi(kodepembelian)
            End If
        End If
    End Sub
    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then
            If txtsupplier.Text IsNot "" Then
                If txtgudang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        If cmbbayar.Text IsNot "" Then
                            Call simpan()
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
                MsgBox("Isi Supplier")
            End If
        Else
            MsgBox("Keranjang Masih Kosong")
        End If

    End Sub
    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(kodepembelian)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If

    End Sub
    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Call cetak_faktur()
    End Sub
    Sub cetak_faktur()
        Dim faktur As String
        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
            baris("harga") = GridView1.GetRowCellValue(i, "harga")
            baris("subtotal") = GridView1.GetRowCellValue(i, "subtotal")
            tabel_faktur.Rows.Add(baris)
        Next

        Call koneksii()
        sql = "select * from tb_printer where nomor='2'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            faktur = dr("nama_printer")

        Else
            faktur = ""
        End If

        Dim rpt As ReportDocument
        rpt = New fakturpembelian
        rpt.SetDataSource(tabel_faktur)
        'rpt.SetParameterValue("total", total2)
        'rpt.SetParameterValue("nofaktur", autonumber)
        'rpt.SetParameterValue("kasir", fmenu.statususer.Text)
        'rpt.SetParameterValue("customer", txtcustomer.Text)

        'fakturjual.CrystalReportViewer1.ReportSource = rpt
        'rpt.PrintOptions.PrinterName = "EPSON TM-U220 Receipt"
        rpt.PrintOptions.PrinterName = "EPSON LX-310 ESC/P (Copy 1)"
        'rpt.PrintOptions.PrinterName = "58 Printer"
        rpt.PrintToPrinter(1, False, 0, 0)
        'fakturjual.ShowDialog()
        'fakturjual.Dispose()
    End Sub
    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgopembelian.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT kode_pembelian FROM tb_pembelian WHERE kode_pembelian  = '" + txtgopembelian.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgopembelian.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub
    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub
    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging

        If e.Column.FieldName = "qty" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "harga"))
            Catch ex As Exception
                'error jika nilai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        Else
            If e.Column.FieldName = "harga" Then
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "qty"))
                Catch ex As Exception
                    'error jika nilai qty=blank
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                End Try
            End If
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub
    Private Sub txthargabarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargabarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txthargabarang_KeyDown(sender As Object, e As KeyEventArgs) Handles txthargabarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
        End If
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Dim hapuskode As String
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            hapuskode = GridView1.GetFocusedRowCellValue("kode_stok")
            sql = "DELETE FROM tb_pembelian_sementara WHERE  kode_stok='" & hapuskode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            GridView1.DeleteSelectedRows()
        End If
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Update"
            Call awaledit()
        Else
            If btnedit.Text = "Update" Then
                If GridView1.DataRowCount > 0 Then
                    If txtsupplier.Text IsNot "" Then
                        If txtgudang.Text IsNot "" Then
                            If cmbsales.Text IsNot "" Then
                                If cmbbayar.Text IsNot "" Then
                                    btnedit.Text = "Edit"
                                    'isi disini sub updatenya
                                    Call perbarui(txtnonota.Text)
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
                        MsgBox("Isi Supllier")
                    End If
                Else
                    MsgBox("Keranjang Masih Kosong")
                End If

            End If
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
        txtgopembelian.Enabled = False
        btnnext.Enabled = False

        'header
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        'cmbcustomer.SelectedIndex = -1
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True

        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        'cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtpembelian.Enabled = True
        'dtpenjualan.Value = Date.Now

        dtjatuhtempo.Enabled = True
        'dtjatuhtempo.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncari.Enabled = True

        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0

        lblsatuan.Text = "satuan"
        lblsatuanbeli.Text = "satuan"

        btntambahbarang.Enabled = True

        'validasi Grid
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        txtketerangan.Enabled = True
        'txtketerangan.Clear()

        cbongkir.Enabled = True
        cbppn.Enabled = True
        cbdiskon.Enabled = True

        'cbongkir.Checked = False
        'cbppn.Checked = False
        'cbdiskon.Checked = False

        txtongkir.Enabled = False
        txtppnpersen.Enabled = False
        txtppnnominal.Enabled = False
        txtdiskonpersen.Enabled = False
        txtdiskonnominal.Enabled = False

        cmbbayar.Enabled = True

        Call koneksii()
        sql = "DELETE FROM tb_pembelian_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "INSERT INTO tb_pembelian_detail_sementara SELECT * FROM tb_pembelian_detail WHERE kode_pembelian ='" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

    End Sub
    Private Sub dtpembelian_ValueChanged(sender As Object, e As EventArgs) Handles dtpembelian.ValueChanged
        dtjatuhtempo.MinDate = dtpembelian.Value
    End Sub
    Sub perbarui(nomornota As String)
        Call koneksii()
        Dim kodegudangupdate As String

        'cari nota  yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT kode_gudang FROM tb_pembelian WHERE kode_pembelian = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodegudangupdate = dr("kode_gudang")

        'update data yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT * FROM tb_pembelian_detail_sementara WHERE kode_pembelian = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        While dr.Read
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & dr("qty") & "' WHERE kode_stok = '" & dr("kode_stok") & "' AND kode_gudang='" & kodegudangupdate & "'"
            cmmd = New OdbcCommand(sql, cnn)
            drpembelian = cmmd.ExecuteReader()
        End While

        'hapus tb_pembelian_detail
        sql = "DELETE FROM tb_pembelian_detail WHERE kode_pembelian = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'hapus panjar
        sql = "DELETE FROM tb_transaksi_kas where kode_pembelian = '" & nomornota & "' and jenis_kas ='AWAL'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        '========================================================================================

        'loop isi pembelian detail

        If GridView1.RowCount = 0 Then
            MsgBox("Data masih kosong")
            Exit Sub
        Else
            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "INSERT INTO tb_pembelian_detail (kode_pembelian, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty,harga_beli, subtotal,created_by, updated_by,date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "', '" & GridView1.GetRowCellValue(i, "harga") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Next
            sql = "UPDATE tb_pembelian SET kode_supplier = '" & cmbsupplier.Text & "', kode_gudang = '" & cmbgudang.Text & "', kode_user = '" & cmbsales.Text & "', tgl_pembelian = '" & Format(dtpembelian.Value, "yyyy-MM-dd HH:mm:ss") & "', tgl_jatuhtempo_pembelian = '" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "', lunas_pembelian = 0 ,void_pembelian = 0, print_pembelian = 0, posted_pembelian = 1, keterangan_pembelian = '" & txtketerangan.Text & "', diskon_pembelian = '" & txtdiskonpersen.Text & "', pajak_pembelian = '" & txtppnpersen.Text & "', ongkir_pembelian = '" & ongkir & "', total_pembelian = '" & grandtotal & "', pembayaran_pembelian = '" & cmbbayar.Text & "', updated_by = '" & fmenu.statususer.Text & "', last_updated = now() WHERE kode_pembelian = '" & nomornota & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                    sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbgudang.Text & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbgudang.Text & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    Else
                        sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_gudang") & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If

                    sql = "UPDATE tb_barang SET modal_barang = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode_barang = '" & GridView1.GetRowCellValue(i, "kode_barang") & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                Else
                    sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbgudang.Text & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbgudang.Text & "'"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    Else
                        sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbgudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If

                    sql = "UPDATE tb_barang SET modal_barang = '" & GridView1.GetRowCellValue(i, "harga") & "' WHERE kode_barang = '" & GridView1.GetRowCellValue(i, "kode_barang") & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                End If
            Next

            kodepembayaran = cmbbayar.Text

            If kodepembayaran IsNot "" Then
                sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_pembelian, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepembayaran & "','" & kodepembelian & "', 'AWAL', now(), 'Transaksi Nota Nomor " & kodepembelian & "','" & 0 & "', '" & 0 & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            End If

            MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(nomornota)

        End If
    End Sub
    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsupplier.SelectedIndexChanged
        Call comboboxpembayaran()
    End Sub
    Private Sub txtongkir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtongkir.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        tutup = 2
        fcaribarang.ShowDialog()
    End Sub
    Private Sub btncarisupplier_Click(sender As Object, e As EventArgs) Handles btncarisupplier.Click
        tutupsup = 1
        fcarisupp.ShowDialog()
    End Sub
    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 1
        fcarigudang.ShowDialog()
    End Sub
    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged
        Call carisupplier()
    End Sub
    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        Call carigudang()
    End Sub
    Private Sub txtppn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtppnpersen.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtdiskonnominal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdiskonnominal.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtdiskonpersen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdiskonpersen.KeyPress
        e.Handled = ValidAngka(e)
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
    Private Sub txttotal_TextChanged(sender As Object, e As EventArgs) Handles txttotal.TextChanged
        If txttotal.Text = "" Then
            txttotal.Text = 0
        Else
            txttotal.Text = Format(grandtotal, "##,##0")
            txttotal.SelectionStart = Len(txttotal.Text)
        End If
    End Sub
    Private Sub txtbanyakbarang_TextChanged(sender As Object, e As EventArgs) Handles txtbanyakbarang.TextChanged
        If txtbanyakbarang.Text = "" Then
            txtbanyakbarang.Text = 0
        Else
            banyak = txtbanyakbarang.Text
            txtbanyakbarang.Text = Format(banyak, "##,##0")
            txtbanyakbarang.SelectionStart = Len(txtbanyakbarang.Text)
        End If
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
    Private Sub txtdiskonnominal_TextChanged(sender As Object, e As EventArgs) Handles txtdiskonnominal.TextChanged
        If txtdiskonnominal.Text = "" Then
            txtdiskonnominal.Text = 0
        Else
            diskonnominal = txtdiskonnominal.Text
            txtdiskonnominal.Text = Format(diskonnominal, "##,##0")
            txtdiskonnominal.SelectionStart = Len(txtdiskonnominal.Text)
        End If
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
    Private Sub GridView1_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles GridView1.RowUpdated
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub
    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
    End Sub
    Private Sub cmbongkir_CheckedChanged(sender As Object, e As EventArgs) Handles cbongkir.CheckedChanged
        If cbongkir.Checked = True Then
            txtongkir.Enabled = True
        Else
            txtongkir.Enabled = False
            txtongkir.Text = 0
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
    Private Sub txtbanyakbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyakbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub UpdateTotalText()
        subtotalsummary = GridView1.Columns("subtotal").SummaryItem.SummaryValue
        grandtotal = subtotalsummary
        If cbdiskon.Checked = True And cbppn.Checked = False And cbongkir.Checked = False Then
            txtdiskonnominal.Text = subtotalsummary * txtdiskonpersen.Text / 100
            grandtotal = subtotalsummary - (subtotalsummary * txtdiskonpersen.Text / 100)
        ElseIf cbppn.Checked = True And cbdiskon.Checked = False And cbongkir.Checked = False Then
            txtppnnominal.Text = subtotalsummary * txtppnpersen.Text / 100
            grandtotal = subtotalsummary + (subtotalsummary * txtppnpersen.Text / 100)
        ElseIf cbppn.Checked = True And cbdiskon.Checked = True And cbongkir.Checked = False Then
            txtdiskonnominal.Text = subtotalsummary * txtdiskonpersen.Text / 100
            txtppnnominal.Text = (subtotalsummary - txtdiskonnominal.Text) * txtppnpersen.Text / 100
            grandtotal = subtotalsummary - txtdiskonnominal.Text + txtppnnominal.Text
        ElseIf cbdiskon.Checked = True And cbppn.Checked = False And cbongkir.Checked = True Then
            txtdiskonnominal.Text = subtotalsummary * txtdiskonpersen.Text / 100
            grandtotal = subtotalsummary - (subtotalsummary * txtdiskonpersen.Text / 100) + txtongkir.Text
        ElseIf cbppn.Checked = True And cbdiskon.Checked = False And cbongkir.Checked = True Then
            txtppnnominal.Text = subtotalsummary * txtppnpersen.Text / 100
            grandtotal = subtotalsummary + (subtotalsummary * txtppnpersen.Text / 100) + txtongkir.Text
        ElseIf cbppn.Checked = True And cbdiskon.Checked = True And cbongkir.Checked = True Then
            txtdiskonnominal.Text = subtotalsummary * txtdiskonpersen.Text / 100
            txtppnnominal.Text = (subtotalsummary - txtdiskonnominal.Text) * txtppnpersen.Text / 100
            grandtotal = subtotalsummary - txtdiskonnominal.Text + txtppnnominal.Text + txtongkir.Text
        ElseIf cbppn.Checked = False And cbdiskon.Checked = False And cbongkir.Checked = True Then
            grandtotal = subtotalsummary + txtongkir.Text
        End If
        txttotal.Text = grandtotal
    End Sub
End Class