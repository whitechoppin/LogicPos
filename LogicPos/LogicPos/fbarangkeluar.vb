Imports System.Data.Odbc
Imports System.IO
Imports DevExpress.Utils
Imports ZXing

Public Class fbarangkeluar
    Public namaform As String = "transaksi-barang_keluar"

    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean

    Public tabel, tabelsementara As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Public jenis, satuan As String
    Dim idbarang, idstok As Integer
    Dim idbarangkeluar As String
    Dim idgudang, iduser, idpelanggan As Integer
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglbarangkeluar As DateTime
    'variabel edit penjualan
    Dim countingbarang As Integer
    'variabel report
    Dim rpt_faktur As New CrystalDecisions.CrystalReports.Engine.ReportDocument

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

    Private Sub fbarangkeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idbarangkeluar = currentnumber()
        Call inisialisasi(idbarangkeluar)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
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

        Call historysave("Membuka Transaksi Barang Keluar", "N/A", namaform)
    End Sub

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_barang_keluar ORDER BY id DESC LIMIT 1;"
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
        sql = "SELECT id FROM tb_barang_keluar WHERE date_created < (SELECT date_created FROM tb_barang_keluar WHERE id = '" & previousnumber & "')ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT id FROM tb_barang_keluar WHERE date_created > (SELECT date_created FROM tb_barang_keluar WHERE id = '" & nextingnumber & "')ORDER BY date_created ASC LIMIT 1"
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

    Private Sub txtbanyak_TextChanged(sender As Object, e As EventArgs) Handles txtbanyak.TextChanged
        If txtbanyak.Text = "" Or txtbanyak.Text = "0" Then
            txtbanyak.Text = 1
        Else
            banyak = txtbanyak.Text
            txtbanyak.Text = Format(banyak, "##,##0")
            txtbanyak.SelectionStart = Len(txtbanyak.Text)
        End If
    End Sub

    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub previewbarangkeluar(lihat As String)
        sql = "SELECT * FROM tb_barang_keluar_detail WHERE barang_keluar_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"))
            tabelsementara.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()
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
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        tabelsementara = New DataTable
        With tabelsementara
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
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

        GridColumn7.FieldName = "barang_id"
        GridColumn7.Caption = "Barang id"
        GridColumn7.Width = 15
        GridColumn7.Visible = False

        GridColumn8.FieldName = "stok_id"
        GridColumn8.Caption = "stok id"
        GridColumn8.Width = 15
        GridColumn8.Visible = False
    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodestok.Clear()
        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyak.Clear()
        txtkodestok.Focus()
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
        txtgobarangkeluar.Enabled = False
        btncarikeluar.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxpelanggan()
        Call comboboxuser()
        Call comboboxgudang()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbpelanggan.Enabled = True
        cmbpelanggan.SelectedIndex = -1
        cmbpelanggan.Focus()
        btncaricustomer.Enabled = True

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        txtalamat.Enabled = True
        txttelp.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtbarangkeluar.Enabled = True
        dtbarangkeluar.Value = Date.Now

        cbprinted.Checked = False
        cbposted.Checked = False

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = True

        lblsatuan.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'buat tabel
        Call tabel_utama()
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
        txtgobarangkeluar.Enabled = False
        btncarikeluar.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Enabled = False

        cmbpelanggan.Enabled = True
        cmbpelanggan.Focus()

        btncaricustomer.Enabled = True

        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        btncarigudang.Enabled = True

        dtbarangkeluar.Enabled = True

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = True

        lblsatuan.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
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
        txtgobarangkeluar.Enabled = True
        btncarikeluar.Enabled = True
        btnnext.Enabled = True

        'isi combo box
        Call comboboxpelanggan()
        Call comboboxuser()
        Call comboboxgudang()

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbpelanggan.Enabled = False
        btncaricustomer.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False
        txtgudang.Enabled = False

        dtbarangkeluar.Enabled = False
        dtbarangkeluar.Value = Date.Now

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = False
        btncaribarang.Enabled = False

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = False

        btntambah.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode > 0 Then
            Call koneksii()
            sql = "SELECT * FROM tb_barang_keluar WHERE id ='" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorcustomer = dr("pelanggan_id")
                nomorsales = dr("user_id")
                nomorgudang = dr("gudang_id")

                statusprint = dr("print_barang_keluar")
                statusposted = dr("posted_barang_keluar")

                viewtglbarangkeluar = dr("tgl_barang_keluar")
                viewketerangan = dr("keterangan_barang_keluar")

                txtnonota.Text = nomornota
                cmbpelanggan.SelectedValue = nomorcustomer
                cmbsales.SelectedValue = nomorsales
                cmbgudang.SelectedValue = nomorgudang

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dtbarangkeluar.Value = viewtglbarangkeluar

                'isi tabel view pembelian

                Call previewbarangkeluar(nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan

            End If
        Else
            txtnonota.Clear()
            cmbpelanggan.SelectedIndex = -1
            cmbsales.SelectedIndex = -1

            txtalamat.Clear()
            txttelp.Clear()

            cmbgudang.SelectedIndex = -1

            cbprinted.Checked = False
            cbposted.Checked = False

            dtbarangkeluar.Value = Date.Now

            txtketerangan.Text = ""

        End If

    End Sub

    Sub caripelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbpelanggan.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idpelanggan = Val(dr("id"))
            txtalamat.Text = dr("alamat_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
        Else
            idpelanggan = 0
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
            idgudang = Val(dr("id"))
            txtgudang.Text = dr("nama_gudang")
        Else
            idgudang = 0
            txtgudang.Text = ""
        End If
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = Val(dr("id"))
            cmbsales.ForeColor = Color.Black
        Else
            iduser = 0
            cmbsales.ForeColor = Color.Red
        End If
    End Sub

    Sub caristok()
        Call koneksii()
        sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang FROM tb_stok JOIN tb_barang ON tb_barang.id = tb_stok.barang_id WHERE kode_stok = '" & txtkodestok.Text & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idstok = Val(dr("idstok"))
            idbarang = Val(dr("idbarang"))

            txtkodebarang.Text = dr("kode_barang")
            txtnamabarang.Text = dr("nama_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            jenis = dr("jenis_barang")

            If idstok > 0 Then
                If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                    txtkodestok.ForeColor = Color.Black
                Else 'kalau ada isi
                    Dim lokasi As Integer = -1
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                            lokasi = i
                        End If
                    Next

                    If lokasi = -1 Then
                        txtkodestok.ForeColor = Color.Black
                    Else
                        txtkodestok.ForeColor = Color.Blue
                    End If
                End If
            End If
        Else
            idstok = 0
            idbarang = 0

            txtnamabarang.Text = ""
            txtkodebarang.Text = ""
            satuan = "satuan"
            lblsatuan.Text = satuan
            jenis = ""

            txtkodestok.ForeColor = Color.Red
        End If
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call cariuser()
    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call cariuser()
    End Sub

    Private Sub fbarangkeluar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub ritebanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritebanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        txtkodestok.Clear()
        Call carigudang()
    End Sub

    Private Sub cmbpelanggan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.SelectedIndexChanged
        txtkodestok.Clear()
        Call caripelanggan()
    End Sub

    Private Sub cmbpelanggan_TextChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.TextChanged
        txtkodestok.Clear()
        Call caripelanggan()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        txtkodestok.Clear()
        Call carigudang()
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        Call caristok()
    End Sub

    Private Sub txtkodestok_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodestok.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
        End If
    End Sub

    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 3
        fcaripelanggan.ShowDialog()
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 3
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        If txtgudang.Text = "" Then
            MsgBox("Isi Kode Gudang", MsgBoxStyle.Information, "Informasi")
        Else
            tutupcaristok = 2
            idgudangcari = idgudang
            fcaristok.ShowDialog()
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
            If cmbpelanggan.Text IsNot "" Then
                If txtgudang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" And iduser > 0 Then
                        Call proses()
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

            If cekcetakan(txtnonota.Text, namaform).Equals(True) Then
                statusizincetak = False
                passwordid = 11
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_barang_keluar SET print_barang_keluar = 1 WHERE id = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Barang Keluar Kode " + txtnonota.Text, txtnonota.Text, namaform)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_barang_keluar SET print_barang_keluar = 1 WHERE id = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Barang Keluar Kode " + txtnonota.Text, txtnonota.Text, namaform)
                '========================

                cbprinted.Checked = True
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
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("qty") = GridView1.GetRowCellValue(i, "banyak")
            baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturbarangkeluar
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        rpt_faktur.SetParameterValue("tanggal", Format(dtbarangkeluar.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("user", fmenu.kodeuser.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)
    End Sub

    Public Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
        Dim faktur As String

        faktur = GetPrinterName(2)

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
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then
                If GridView1.DataRowCount > 0 Then
                    If cmbpelanggan.Text IsNot "" Then
                        If txtgudang.Text IsNot "" Then
                            If cmbsales.Text IsNot "" And iduser > 0 Then
                                'isi disini sub updatenya
                                Call perbarui(txtnonota.Text)
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
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(idbarangkeluar)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(idbarangkeluar)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgobarangkeluar.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT id FROM tb_barang_keluar WHERE id = '" & txtgobarangkeluar.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgobarangkeluar.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncarikeluar_Click(sender As Object, e As EventArgs) Handles btncarikeluar.Click
        tutupcaribarangkeluar = 1
        fcaribarangkeluar.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(idbarangkeluar)
    End Sub

    Sub tambah()
        Dim tbbanyak As Integer = 0
        Dim lokasi As Integer = -1
        'Columns.Add("kode_barang")
        'Columns.Add("kode_stok")
        'Columns.Add("nama_barang")
        'Columns.Add("banyak", GetType(Double))
        'Columns.Add("satuan_barang")
        'Columns.Add("jenis_barang")
        '.Columns.Add("barang_id")
        '.Columns.Add("stok_id")

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtbanyak.Text = "" Or banyak <= 0 Then
            MsgBox("Barang Kosong", MsgBoxStyle.Information, "Informasi")
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    If dr("jumlah_stok") < banyak Then
                        MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                    Else
                        tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, idbarang, idstok)
                        Call reload_tabel()
                    End If
                End If
            Else 'kalau ada isi
                sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                            lokasi = i
                        End If
                    Next

                    tbbanyak = GridView1.GetRowCellValue(lokasi, "banyak")

                    If lokasi = -1 Then
                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok)
                            Call reload_tabel()
                        End If
                    Else
                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok)
                            Call reload_tabel()
                        End If
                    End If
                Else
                    MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                End If
            End If
        End If
    End Sub

    Sub proses()
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim statusavailable As Boolean = True

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                If stokdatabase < stok Then
                    MsgBox("Stok dengan kode stok " + dr("kode_stok") + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " + GridView1.GetRowCellValue(i, "stok_id") + " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            Call simpan()
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
            sql = "INSERT INTO tb_barang_keluar(pelanggan_id, gudang_id, user_id, tgl_barang_keluar, print_barang_keluar, posted_barang_keluar, keterangan_barang_keluar, created_by, updated_by, date_created, last_updated) VALUES ('" & idpelanggan & "','" & idgudang & "','" & iduser & "' , '" & Format(dtbarangkeluar.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idbarangkeluar = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "INSERT INTO tb_barang_keluar_detail (barang_keluar_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by,date_created, last_updated) VALUES ('" & idbarangkeluar & "', '" & GridView1.GetRowCellValue(i, "barang_id") & "', '" & GridView1.GetRowCellValue(i, "stok_id") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Barang Keluar Kode " & idbarangkeluar, idbarangkeluar, namaform)
            '========================
            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(idbarangkeluar)

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
            MsgBox("Transaksi Gagal Dilakukan", MsgBoxStyle.Information, "Gagal")
        End Try


    End Sub

    Sub perbarui(nomornota As String)
        'periksa di barang di stok dulu
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim stokdatabasesementara As Integer

        Dim namastokdatabase As String
        Dim statusavailable As Boolean = True
        Dim idgudanglama As String

        'variabel transactional
        '=======================
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction
        '=======================

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                namastokdatabase = dr("nama_stok")

                'mengambil selisih qty dari penjualan detail
                sql = "SELECT * FROM tb_barang_keluar_detail WHERE stok_id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND barang_keluar_id ='" & nomornota & "' LIMIT 1"
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
                    MsgBox("Stok dengan nama stok " & namastokdatabase & " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " & GridView1.GetRowCellValue(i, "kode_stok") & " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            ' Start a local transaction
            myTrans = cnnx.BeginTransaction()
            myCommand.Connection = cnnx
            myCommand.Transaction = myTrans

            'cari nota  yang sebelumnya (kembalikan stok dulu)
            sql = "SELECT gudang_id FROM tb_barang_keluar WHERE id = '" & idbarangkeluar & "'"
            cmmd = New OdbcCommand(sql, cnn)
            idgudanglama = Val(cmmd.ExecuteScalar())

            Try
                'update stok kembalikan
                Call koneksii()
                For i As Integer = 0 To tabelsementara.Rows.Count - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & tabelsementara.Rows(i).Item(3) & "' WHERE id = '" & tabelsementara.Rows(i).Item(7) & "' AND gudang_id ='" & idgudanglama & "'"
                    myCommand.ExecuteNonQuery()
                Next

                'hapus di tabel jual detail
                myCommand.CommandText = "DELETE FROM tb_barang_keluar_detail WHERE barang_keluar_id = '" & nomornota & "'"
                myCommand.ExecuteNonQuery()


                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()

                    myCommand.CommandText = "INSERT INTO tb_barang_keluar_detail (barang_keluar_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by,date_created, last_updated) VALUES ('" & nomornota & "', '" & GridView1.GetRowCellValue(i, "barang_id") & "', '" & GridView1.GetRowCellValue(i, "stok_id") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                myCommand.CommandText = "UPDATE tb_barang_keluar SET pelanggan_id ='" & idpelanggan & "', gudang_id ='" & idgudang & "', user_id ='" & iduser & "' , tgl_barang_keluar ='" & Format(dtbarangkeluar.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_barang_keluar ='" & txtketerangan.Text & "', updated_by ='" & fmenu.kodeuser.Text & "', last_updated = now() WHERE id ='" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                'history user ==========
                Call historysave("Mengedit Data Barang Keluar Kode " & idbarangkeluar, idbarangkeluar, namaform)
                '========================
                MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
                'Call inisialisasi(nomornota)
                Call inisialisasi(txtnonota.Text)
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
                MsgBox("Update Gagal", MsgBoxStyle.Information, "Gagal")
            End Try
        End If

    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
        txtkodestok.ForeColor = Color.Black
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
End Class