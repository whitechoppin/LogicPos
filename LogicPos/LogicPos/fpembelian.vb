Imports System.Data.Odbc
Imports DevExpress.Utils
Imports CrystalDecisions.CrystalReports.Engine
Imports ZXing
Imports System.IO

Public Class fpembelian
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean

    Dim tabel, tabelsementara As DataTable
    Dim hargamodalbarang As Double
    Dim hitnumber As Integer
    'variabel dalam pembelian
    Dim lunasstatus As Integer = 0

    Dim dateterm, datetermnow As Date
    Dim idsupplier, idpembelian, idgudang, iduser As String
    'variable detail pembelian
    Dim satuan, jenis As String
    Dim idbarang, idstok As Integer

    Dim hargabarang, modalpembelian, ongkir, ppn, diskonpersen, diskonnominal, ppnpersen, ppnnominal, subtotalsummary, grandtotal, banyak, term As Double

    'variabel bantuan view pembelian
    Dim nomornota, nomorsupplier, nomorsales, nomorgudang As String
    Dim viewketerangan, viewnomorsupplier As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewterm As Integer
    Dim viewtglpembelian, viewtgljatuhtempo, viewtglcreated, viewtglupdated As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir As Double
    Dim rpt_faktur As New ReportDocument

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

    Private Sub fpembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idpembelian = currentnumber()
        Call inisialisasi(idpembelian)
        With GridView1
            '.OptionsView.ColumnAutoWidth = False ' agar muncul scrol bar
            '.OptionsView.ShowAutoFilterRow = True 'aktifkan autofilter
            '.OptionsView.EnableAppearanceOddRow = True 'aktifkan style
            '.OptionsPrint.EnableAppearanceOddRow = True 'aktifkan style saat print

            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
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

        Call historysave("Membuka Transaksi Pembelian", "N/A")
    End Sub
    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_pembelian ORDER BY id DESC LIMIT 1;"
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
        sql = "SELECT id FROM tb_pembelian WHERE date_created < (SELECT date_created FROM tb_pembelian WHERE id = '" + previousnumber + "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT id FROM tb_pembelian WHERE date_created > (SELECT date_created FROM tb_pembelian WHERE id = '" + nextingnumber + "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        tabelsementara = New DataTable
        With tabelsementara
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Caption = "Kode Stok"
        GridColumn1.Width = 15

        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.Width = 15

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 60

        GridColumn4.Caption = "Qty"
        GridColumn4.FieldName = "qty"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        GridColumn4.Width = 10

        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn5.Width = 10

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 10

        GridColumn7.FieldName = "harga"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 30

        GridColumn8.FieldName = "subtotal"
        GridColumn8.Caption = "Subtotal"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 35

        GridColumn9.FieldName = "barang_id"
        GridColumn9.Caption = "Barang id"
        GridColumn9.Width = 15
        GridColumn9.Visible = False

        GridColumn10.FieldName = "stok_id"
        GridColumn10.Caption = "stok id"
        GridColumn10.Width = 15
        GridColumn10.Visible = False
    End Sub

    Sub previewpembelian(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian_detail WHERE pembelian_id='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("harga_beli"), dr("subtotal"), dr("barang_id"), dr("stok_id"))
            tabelsementara.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("harga_beli"), dr("subtotal"), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()
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
        txtgopembelian.Enabled = True
        btncaripembelian.Enabled = True
        btnnext.Enabled = True

        rbfaktur.Checked = False
        rbpo.Checked = True

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        'buat tabel
        Call tabel_utama()

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

        txtterm.Clear()
        txtterm.Text = 0
        txtterm.Enabled = False

        dtjatuhtempo.Enabled = False
        dtjatuhtempo.Value = dtpembelian.Value

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False

        btncari.Enabled = False

        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 1
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

        txtnosupplier.Enabled = False
        txtnosupplier.Clear()

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

        If nomorkode > 0 Then
            Call koneksii()
            sql = "SELECT * FROM tb_pembelian WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorsupplier = dr("supplier_id")
                nomorsales = dr("user_id")
                nomorgudang = dr("gudang_id")

                statuslunas = dr("lunas_pembelian")
                statusvoid = dr("void_pembelian")
                statusprint = dr("print_pembelian")
                statusposted = dr("posted_pembelian")

                viewtglpembelian = dr("tgl_pembelian")
                viewterm = dr("term_pembelian")
                viewtgljatuhtempo = dr("tgl_jatuhtempo_pembelian")

                viewtglcreated = dr("date_created")
                viewtglupdated = dr("last_updated")

                viewketerangan = dr("keterangan_pembelian")
                viewnomorsupplier = dr("no_nota_pembelian")
                nilaidiskon = dr("diskon_pembelian")
                nilaippn = dr("pajak_pembelian")
                nilaiongkir = dr("ongkir_pembelian")

                txtnonota.Text = nomornota
                cmbsupplier.SelectedValue = nomorsupplier
                cmbsales.SelectedValue = nomorsales
                cmbgudang.SelectedValue = nomorgudang

                cblunas.Checked = statuslunas
                cbvoid.Checked = statusvoid
                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dtpembelian.Value = viewtglpembelian
                txtterm.Text = viewterm
                dtjatuhtempo.Value = viewtgljatuhtempo

                dtcreated.Value = viewtglcreated
                dtupdated.Value = viewtglupdated

                'isi tabel view pembelian

                Call previewpembelian(nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan
                txtnosupplier.Text = viewnomorsupplier

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
            End If
        Else
            txtnonota.Clear()
            cmbsupplier.SelectedIndex = -1
            cmbsales.SelectedIndex = -1
            cmbgudang.SelectedIndex = -1
            cblunas.Checked = False
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            dtpembelian.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            txtketerangan.Text = ""
            txtnosupplier.Text = ""

            cbdiskon.Checked = False
            txtdiskonpersen.Text = 0

            cbppn.Checked = False
            txtppnpersen.Text = 0

            cbongkir.Checked = False
            txtongkir.Text = 0

            txtongkir.Enabled = False
            txtppnpersen.Enabled = False
            txtdiskonpersen.Enabled = False
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
        btncaripembelian.Enabled = True
        btnnext.Enabled = False

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.SelectedIndex = -1
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True
        txtsupplier.Enabled = False

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        cblunas.Checked = False
        cbvoid.Checked = False
        cbprinted.Checked = False
        cbposted.Checked = False

        dtpembelian.Enabled = True
        dtpembelian.Value = Date.Now

        txtterm.Clear()
        txtterm.Text = 0
        txtterm.Enabled = True

        dtjatuhtempo.Enabled = False
        dtjatuhtempo.Value = dtpembelian.Value

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncari.Enabled = True

        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 1
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

        txtnosupplier.Enabled = True
        txtnosupplier.Clear()

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

        dtcreated.Value = Now
        dtupdated.Value = Now

        'buat tabel
        Call tabel_utama()

        statusedit = False
    End Sub

    Sub comboboxsupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsupplier.DataSource = ds.Tables(0)
        cmbsupplier.ValueMember = "id"
        cmbsupplier.DisplayMember = "kode_supplier"
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

    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang='" & txtkodebarang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idbarang = Val(dr("id"))
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
            idsupplier = dr("id")
            txtsupplier.Text = dr("nama_supplier")
            txtalamat.Text = dr("alamat_supplier")
            txttelp.Text = dr("telepon_supplier")
        Else
            idsupplier = "0"
            txtsupplier.Text = ""
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
            idgudang = dr("id")
            txtgudang.Text = dr("nama_gudang")
        Else
            idgudang = "0"
            txtgudang.Text = ""
        End If
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT id FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            iduser = dr("id")
        Else
            iduser = "0"
        End If
    End Sub

    Private Sub dtpembelian_ValueChanged(sender As Object, e As EventArgs) Handles dtpembelian.ValueChanged
        dtjatuhtempo.MinDate = dtpembelian.Value

        term = Val(txtterm.Text)
        dateterm = dtpembelian.Value
        datetermnow = dateterm.AddDays(term)
        dtjatuhtempo.Value = datetermnow
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
                dateterm = dtpembelian.Value
                datetermnow = dateterm.AddDays(term)
                dtjatuhtempo.Value = datetermnow
            End If
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
        txtkodebarang.Focus()
    End Sub
    Sub tambah()
        Call koneksii()
        If txtkodebarang.Text = "" Or txthargabarang.Text = "" Or txtbanyakbarang.Text = "" Or banyak <= 0 Then
            MsgBox("Isi Kode Barang")
        Else
            idstok = 0
            If GridView1.RowCount = 0 Then
                If lblsatuan.Text = "Pcs" Then
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(hargabarang), Val(banyak) * Val(hargabarang), idbarang, idstok)
                    Call reload_tabel()
                Else
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(hargabarang), Val(banyak) * Val(hargabarang), idbarang, idstok)
                    Call reload_tabel()
                End If
            Else 'data ada
                Dim lokasi As Integer = -1
                Dim qtytambah As Integer
                If GridView1.RowCount <> 0 Then
                    'MsgBox("data ada")
                    If lblsatuan.Text = "Pcs" Then
                        'MsgBox("ini pcs")
                        For i As Integer = 0 To GridView1.RowCount - 1
                            If GridView1.GetRowCellValue(i, "kode_barang").Equals(txtkodebarang.Text) And GridView1.GetRowCellValue(i, "satuan_barang").Equals("Pcs") Then
                                lokasi = i
                            End If
                        Next

                        If lokasi = -1 Then
                            tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(hargabarang), Val(banyak) * Val(hargabarang), idbarang, idstok)
                            Call reload_tabel()
                        Else
                            qtytambah = Val(GridView1.GetRowCellValue(lokasi, "qty"))

                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak + qtytambah), satuan, jenis, Val(hargabarang), (Val(banyak) + qtytambah) * Val(hargabarang), idbarang, idstok)
                            Call reload_tabel()
                        End If
                    Else
                        'MsgBox("bukan Pcs")
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, Val(hargabarang), Val(banyak) * Val(hargabarang), idbarang, idstok)
                        Call reload_tabel()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged
        Call caribarang()
    End Sub

    Private Sub txtkodebarang_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodebarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
            BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
        End If
    End Sub

    Private Sub txthargabarang_TextChanged(sender As Object, e As EventArgs) Handles txthargabarang.TextChanged
        If txthargabarang.Text = "" Then
            txthargabarang.Text = 0
        Else
            hargabarang = txthargabarang.Text
            txthargabarang.Text = Format(hargabarang, "##,##0")
            txthargabarang.SelectionStart = Len(txthargabarang.Text)
        End If
    End Sub
    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
        BeginInvoke(New MethodInvoker(AddressOf UpdateTotalText))
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
            'sediakan wadah stok nya dulu
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                    Call koneksii()
                    sql = "SELECT * FROM tb_stok WHERE barang_id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' AND gudang_id='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        idstok = Val(dr("id"))
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    Else
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idstok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    End If
                Else
                    Call koneksii()
                    sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                    cmmd = New OdbcCommand(sql, cnn)
                    idstok = CInt(cmmd.ExecuteScalar())
                    GridView1.SetRowCellValue(i, "stok_id", idstok)
                End If
            Next

            'Mulai penginputan stok dan data pembelian

            Call koneksii()
            sql = "INSERT INTO tb_pembelian(supplier_id, gudang_id, user_id, tgl_pembelian, tgl_jatuhtempo_pembelian, term_pembelian, lunas_pembelian, void_pembelian, print_pembelian, posted_pembelian, keterangan_pembelian, no_nota_pembelian, diskon_pembelian, pajak_pembelian, ongkir_pembelian, total_pembelian, created_by, updated_by, date_created, last_updated) VALUES ('" & idsupplier & "','" & idgudang & "','" & iduser & "','" & Format(dtpembelian.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & term & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & txtnosupplier.Text & "','" & txtdiskonpersen.Text & "','" & txtppnpersen.Text & "','" & txtongkir.Text & "','" & grandtotal & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idpembelian = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                Else
                    GridView1.SetRowCellValue(i, "kode_stok", String.Concat(GridView1.GetRowCellValue(i, "kode_barang"), GridView1.GetRowCellValue(i, "stok_id")))
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "', kode_stok = CONCAT(kode_barang,id), status_stok='1' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                End If

                sql = "SELECT * FROM tb_barang WHERE id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                If dr.HasRows Then
                    hargamodalbarang = dr("modal_barang")

                    If hargamodalbarang < Val(GridView1.GetRowCellValue(i, "harga")) Then
                        myCommand.CommandText = "UPDATE tb_barang SET modal_barang = '" & Val(GridView1.GetRowCellValue(i, "harga")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "barang_id") & "'"
                        myCommand.ExecuteNonQuery()
                    End If
                End If

                myCommand.CommandText = "INSERT INTO tb_pembelian_detail(pembelian_id, barang_id, stok_id,kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by, date_created, last_updated) VALUES ('" & idpembelian & "','" & GridView1.GetRowCellValue(i, "barang_id") & "', '" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "', '" & GridView1.GetRowCellValue(i, "harga") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            'history user ==========
            Call historysave("Menyimpan Data Pembelian Kode " + idpembelian, idpembelian)
            '========================
            Call inisialisasi(idpembelian)

        Catch e As Exception
            Try
                myTrans.Rollback()
            Catch ex As OdbcException
                If Not myTrans.Connection Is Nothing Then
                    Console.WriteLine("An exception of type " + ex.GetType().ToString() +
                    " was encountered while attempting to roll back the transaction.")
                End If
            End Try

            Console.WriteLine("An exception of type " + e.GetType().ToString() + "was encountered while inserting the data.")
            Console.WriteLine("Neither record was written to database.")
            MsgBox("Transaksi Gagal Dilakukan", MsgBoxStyle.Information, "Sukses")
        End Try

    End Sub
    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then
            If txtsupplier.Text IsNot "" Then
                If txtgudang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        Call simpan()
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
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(idpembelian)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub
    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            If cbvoid.Checked = False Then
                If cekcetakan(txtnonota.Text).Equals(True) Then
                    statusizincetak = False
                    passwordid = 6
                    fpassword.kodetabel = txtnonota.Text
                    fpassword.ShowDialog()
                    If statusizincetak.Equals(True) Then
                        If rbfaktur.Checked Then
                            Call cetak_faktur()

                            Call koneksii()
                            sql = "UPDATE tb_pembelian SET print_pembelian = 1 WHERE id = '" & txtnonota.Text & "' "
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            'history user ==========
                            Call historysave("Mencetak Data Pembelian Kode " + txtnonota.Text, txtnonota.Text)
                            '========================

                            cbprinted.Checked = True
                        ElseIf rbpo.Checked Then
                            Call cetak_po()

                            Call koneksii()
                            sql = "UPDATE tb_pembelian SET print_pembelian = 1 WHERE id = '" & txtnonota.Text & "' "
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            'history user ==========
                            Call historysave("Mencetak Data Pembelian Kode " + txtnonota.Text, txtnonota.Text)
                            '========================

                            cbprinted.Checked = True
                        End If
                    End If
                Else
                    If rbfaktur.Checked Then
                        Call cetak_faktur()

                        Call koneksii()
                        sql = "UPDATE tb_pembelian SET print_pembelian = 1 WHERE id = '" & txtnonota.Text & "' "
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        'history user ==========
                        Call historysave("Mencetak Data Pembelian Kode " + txtnonota.Text, txtnonota.Text)
                        '========================

                        cbprinted.Checked = True
                    ElseIf rbpo.Checked Then
                        Call cetak_po()

                        Call koneksii()
                        sql = "UPDATE tb_pembelian SET print_pembelian = 1 WHERE id = '" & txtnonota.Text & "' "
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()

                        'history user ==========
                        Call historysave("Mencetak Data Pembelian Kode " + txtnonota.Text, txtnonota.Text)
                        '========================

                        cbprinted.Checked = True
                    End If
                End If
            Else
                MsgBox("Nota sudah void !")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub cetak_faktur()
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

        rpt_faktur = New fakturpembelian
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("jatem", Format(dtjatuhtempo.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("diskon", diskonnominal)
        rpt_faktur.SetParameterValue("grandtotal", grandtotal)
        rpt_faktur.SetParameterValue("ppn", ppnnominal)
        rpt_faktur.SetParameterValue("ongkir", ongkir)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpembelian.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("supplier", txtsupplier.Text)
        rpt_faktur.SetParameterValue("user", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Sub cetak_po()
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

        rpt_faktur = New fakturpopembelian
        rpt_faktur.SetDataSource(tabel_faktur)
        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("jatem", Format(dtjatuhtempo.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("diskon", diskonnominal)
        rpt_faktur.SetParameterValue("grandtotal", grandtotal)
        rpt_faktur.SetParameterValue("ppn", ppnnominal)
        rpt_faktur.SetParameterValue("ongkir", ongkir)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpembelian.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("supplier", txtsupplier.Text)
        rpt_faktur.SetParameterValue("user", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

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
    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgopembelian.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT id FROM tb_pembelian WHERE id  = '" & txtgopembelian.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgopembelian.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncaripembelian_Click(sender As Object, e As EventArgs) Handles btncaripembelian.Click
        tutupbeli = 2
        fcaripembelian.ShowDialog()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call cariuser()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub
    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "qty" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "qty", 1)
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 1 * GridView1.GetRowCellValue(e.RowHandle, "harga"))
                Catch ex As Exception
                    'error jika nilai qty=blank
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                End Try
            Else
                Try
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "harga"))
                Catch ex As Exception
                    'error jika nilai qty=blank
                    GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                End Try
            End If
        Else
            If e.Column.FieldName = "harga" Then
                If e.Value = "" Or e.Value = "0" Then
                    GridView1.SetRowCellValue(e.RowHandle, "harga", 1)
                    Try
                        GridView1.SetRowCellValue(e.RowHandle, "subtotal", 1 * GridView1.GetRowCellValue(e.RowHandle, "qty"))
                    Catch ex As Exception
                        'error jika nilai qty=blank
                        GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                    End Try
                Else
                    Try
                        GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "qty"))
                    Catch ex As Exception
                        'error jika nilai qty=blank
                        GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
                    End Try
                End If
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

    Private Sub fpembelian_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Dim statusutang As Boolean = False

        If cbvoid.Checked = False Then
            'Call koneksii()
            'sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE pembelian_id = '" & txtnonota.Text & "' LIMIT 1"
            'cmmd = New OdbcCommand(sql, cnn)
            'dr = cmmd.ExecuteReader
            'dr.Read()

            'If dr.HasRows Then
            '    statusutang = True
            'Else
            '    statusutang = False
            'End If

            If statusutang = False Then
                If btnedit.Text = "Edit" Then
                    btnedit.Text = "Update"
                    Call awaledit()
                Else
                    If btnedit.Text = "Update" Then
                        If GridView1.DataRowCount > 0 Then
                            If txtsupplier.Text IsNot "" Then
                                If txtgudang.Text IsNot "" Then
                                    If cmbsales.Text IsNot "" Then
                                        btnedit.Text = "Edit"
                                        'isi disini sub updatenya
                                        Call perbaharui(txtnonota.Text)
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
            Else
                MsgBox("Nota sudah lunas !")
            End If
        Else
            MsgBox("Nota sudah void !")
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
        btncaripembelian.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True

        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtpembelian.Enabled = True
        txtterm.Enabled = True
        dtjatuhtempo.Enabled = False

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncari.Enabled = True

        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 1
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

        txtnosupplier.Enabled = True

        cbongkir.Enabled = True
        cbppn.Enabled = True
        cbdiskon.Enabled = True

        txtongkir.Enabled = False
        txtppnpersen.Enabled = False
        txtppnnominal.Enabled = False
        txtdiskonpersen.Enabled = False
        txtdiskonnominal.Enabled = False
    End Sub

    Private Sub ritenumber_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = ValidAngka(e)
    End Sub

    Sub perbaharui(nomornota As Integer)
        Dim idgudanglama As Integer

        'variabel transactional
        '=======================
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction
        '=======================

        Call koneksii()
        sql = "SELECT gudang_id FROM tb_pembelian WHERE id = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        idgudanglama = Val(cmmd.ExecuteScalar())

        'Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            'sediakan wadah stok nya dulu
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                    Call koneksii()
                    sql = "SELECT * FROM tb_stok WHERE barang_id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' AND gudang_id='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        idstok = Val(dr("id"))
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    Else
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idstok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    End If
                Else
                    'update di wadah yang sama
                    If GridView1.GetRowCellValue(i, "stok_id") = 0 Then
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idstok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    Else
                        Call koneksii()
                        sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                        If dr.HasRows Then
                            idstok = Val(dr("id"))
                            GridView1.SetRowCellValue(i, "stok_id", idstok)
                        Else
                            Call koneksii()
                            sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                            cmmd = New OdbcCommand(sql, cnn)
                            idstok = CInt(cmmd.ExecuteScalar())
                            GridView1.SetRowCellValue(i, "stok_id", idstok)
                        End If
                    End If

                End If
            Next

            'update kembali dari transaksi sebelumnya
            Call koneksii()
            For i As Integer = 0 To tabelsementara.Rows.Count - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & tabelsementara.Rows(i).Item(3) & "' WHERE id = '" & tabelsementara.Rows(i).Item(9) & "' AND gudang_id ='" & idgudanglama & "'"
                myCommand.ExecuteNonQuery()
            Next

            'hapus tb_pembelian_detail
            Call koneksii()
            myCommand.CommandText = "DELETE FROM tb_pembelian_detail WHERE pembelian_id = '" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            'loop isi pembelian detail

            Call koneksii()
            myCommand.CommandText = "UPDATE tb_pembelian SET supplier_id = '" & idsupplier & "', gudang_id = '" & idgudang & "', user_id = '" & iduser & "', tgl_pembelian = '" & Format(dtpembelian.Value, "yyyy-MM-dd HH:mm:ss") & "', tgl_jatuhtempo_pembelian = '" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "', term_pembelian='" & term & "', lunas_pembelian = 0 ,void_pembelian = 0, print_pembelian = 0, posted_pembelian = 1, keterangan_pembelian = '" & txtketerangan.Text & "', no_nota_pembelian = '" & txtnosupplier.Text & "', diskon_pembelian = '" & txtdiskonpersen.Text & "', pajak_pembelian = '" & txtppnpersen.Text & "', ongkir_pembelian = '" & ongkir & "', total_pembelian = '" & grandtotal & "',updated_by = '" & fmenu.kodeuser.Text & "', last_updated = now() WHERE id = '" & nomornota & "' "
            myCommand.ExecuteNonQuery()

            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "satuan_barang") = "Pcs" Then
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                Else
                    GridView1.SetRowCellValue(i, "kode_stok", String.Concat(GridView1.GetRowCellValue(i, "kode_barang"), GridView1.GetRowCellValue(i, "stok_id")))
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "', kode_stok = CONCAT(kode_barang,id), status_stok='1' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                End If

                sql = "SELECT * FROM tb_barang WHERE id='" & GridView1.GetRowCellValue(i, "barang_id") & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                If dr.HasRows Then
                    hargamodalbarang = dr("modal_barang")

                    If hargamodalbarang < Val(GridView1.GetRowCellValue(i, "harga")) Then
                        myCommand.CommandText = "UPDATE tb_barang SET modal_barang = '" & Val(GridView1.GetRowCellValue(i, "harga")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "barang_id") & "'"
                        myCommand.ExecuteNonQuery()
                    End If
                End If

                myCommand.CommandText = "INSERT INTO tb_pembelian_detail(pembelian_id, barang_id, stok_id,kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by, date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "barang_id") & "', '" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "', '" & GridView1.GetRowCellValue(i, "harga") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next


            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Mengedit Data Pembelian Kode " + nomornota.ToString, nomornota)
            '========================
            MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(nomornota)
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
            MsgBox("Update Gagal", MsgBoxStyle.Information, "Sukses")
        End Try
    End Sub
    Private Sub txtongkir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtongkir.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        tutupcaribarang = 2
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
        If txtbanyakbarang.Text = "" Or txtbanyakbarang.Text = "0" Then
            txtbanyakbarang.Text = 1
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