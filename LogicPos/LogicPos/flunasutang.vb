Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils
Imports ZXing

Public Class flunasutang
    Public namaform As String = "administrasi-lunas_utang"

    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim kode As String
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel, tabelsementara, tabellunas As DataTable
    'body
    Dim kodepembeliantbh, kodesuppliertbh As String
    Dim tglbelitbh, tgljatuhtempotbh As Date
    Dim totalbelitbh, bayarutangtbh, sisautangtbh As Double
    '====
    'variabel bantuan view pembelian
    Dim viewkodelunas, viewkodesupplier, viewkodesales, viewkodebayar, viewketerangan, viewnobukti As String
    Dim viewtotallunas As Double
    Dim statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpelunasan As DateTime
    '====
    Dim lunasstatus As Integer = 0
    Dim hitnumber As Integer

    Dim idlunasutang As String
    Dim idsupplier, iduser, idkas As Integer
    Dim totalbayar, totalterima, totalselisih As Double
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

    Private Sub flunasutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idlunasutang = currentnumber()

        Call inisialisasi(idlunasutang)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("total_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_pembelian", "{0:n0}")
            .Columns("bayar_utang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_utang", "{0:n0}")
            .Columns("sisa_utang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_utang", "{0:n0}")
            .Columns("terima_utang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "terima_utang", "{0:n0}")
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

        Call historysave("Membuka Administrasi Lunas Utang", "N/A", namaform)
    End Sub

    Sub comboboxuser()
        Call koneksi()
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
        Call koneksi()
        sql = "SELECT * FROM tb_kas"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbbayar.DataSource = ds.Tables(0)
        cmbbayar.ValueMember = "id"
        cmbbayar.DisplayMember = "kode_kas"
    End Sub

    Sub comboboxsupplier()
        Call koneksi()
        sql = "SELECT * FROM tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsupplier.DataSource = ds.Tables(0)
        cmbsupplier.ValueMember = "id"
        cmbsupplier.DisplayMember = "kode_supplier"
    End Sub

    Function currentnumber()
        Call koneksi()
        sql = "SELECT id FROM tb_pelunasan_utang ORDER BY id DESC LIMIT 1;"
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
        Call koneksi()
        sql = "SELECT kode_lunas FROM tb_pelunasan_utang WHERE date_created < (SELECT date_created FROM tb_pelunasan_utang WHERE kode_lunas = '" + previousnumber + "') ORDER BY date_created DESC LIMIT 1"
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
        Call koneksi()
        sql = "SELECT kode_lunas FROM tb_pelunasan_utang WHERE date_created > (SELECT date_created FROM tb_pelunasan_utang WHERE kode_lunas = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
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
        btngolunas.Enabled = False
        txtgolunas.Enabled = False
        btncaripelunasan.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxuser()
        Call comboboxsupplier()
        Call comboboxpembayaran()

        'header
        txtnolunasutang.Clear()
        txtnolunasutang.Enabled = False

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        dtpelunasan.Enabled = True
        dtpelunasan.Value = Date.Now

        cmbsupplier.SelectedIndex = -1
        cmbsupplier.Enabled = True
        btncarisupplier.Enabled = True

        cbvoid.Checked = False
        cbprinted.Checked = False
        cbposted.Checked = False

        cmbbayar.SelectedIndex = -1
        cmbbayar.Enabled = True

        txttotalbayar.Clear()
        txttotalbayar.Text = 0
        txttotalbayar.Enabled = True

        txtbukti.Clear()
        txtbukti.Text = ""
        txtbukti.Enabled = True

        'body
        txtkodepembelian.Clear()
        txtkodepembelian.Enabled = True
        btncaribeli.Enabled = True

        txttotalbeli.Clear()
        txttotalbeli.Text = 0

        txtbayarbeli.Clear()
        txtbayarbeli.Text = 0

        txtsisabeli.Clear()
        txtsisabeli.Text = 0

        btntambah.Enabled = True
        btnsesuaikan.Enabled = True

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'buat tabel
        Call tabel_utama()

        txtselisih.Clear()
        txtselisih.Text = 0
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
        btngolunas.Enabled = True
        txtgolunas.Enabled = True
        btncaripelunasan.Enabled = True
        btnnext.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxsupplier()
        Call comboboxpembayaran()

        'header
        txtnolunasutang.Clear()

        'cmbsales.SelectedIndex = 0
        cmbsales.Enabled = False

        dtpelunasan.Enabled = False
        dtpelunasan.Value = Date.Now

        'cmbcustomer.SelectedIndex = 0
        cmbsupplier.Enabled = False
        btncarisupplier.Enabled = False

        'cmbbayar.SelectedIndex = 0
        cmbbayar.Enabled = False

        txttotalbayar.Clear()
        txttotalbayar.Text = 0
        txttotalbayar.Enabled = False

        txtbukti.Clear()
        txtbukti.Text = ""
        txtbukti.Enabled = False

        'body
        txtkodepembelian.Clear()
        txtkodepembelian.Enabled = False
        btncaribeli.Enabled = False

        txttotalbeli.Clear()
        txttotalbeli.Text = 0

        txtbayarbeli.Clear()
        txtbayarbeli.Text = 0

        txtsisabeli.Clear()
        txtsisabeli.Text = 0

        btntambah.Enabled = False
        btnsesuaikan.Enabled = False

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        txtselisih.Clear()
        txtselisih.Text = 0

        Call tabel_utama()

        If nomorkode > 0 Then
            Call koneksi()
            sql = "SELECT * FROM tb_pelunasan_utang WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                viewkodelunas = dr("id")
                viewkodesales = dr("user_id")
                viewkodesupplier = dr("supplier_id")
                viewkodebayar = dr("kas_id")
                viewtglpelunasan = dr("tanggal_transaksi")
                viewtotallunas = dr("bayar_lunas")
                viewnobukti = dr("no_bukti")

                statusvoid = dr("void_lunas")
                statusprint = dr("print_lunas")
                statusposted = dr("posted_lunas")

                viewketerangan = dr("keterangan_lunas")

                txtnolunasutang.Text = viewkodelunas
                cmbsales.SelectedValue = viewkodesales
                cmbsupplier.SelectedValue = viewkodesupplier
                cmbbayar.SelectedValue = viewkodebayar
                txttotalbayar.Text = viewtotallunas
                txtbukti.Text = viewnobukti

                cbvoid.Checked = statusvoid
                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dtpelunasan.Value = viewtglpelunasan

                'isi tabel view pembelian

                Call previewpelunasan(nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan
            End If
        Else
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            txtnolunasutang.Clear()

            cmbsales.SelectedIndex = -1
            cmbsupplier.SelectedIndex = -1
            cmbbayar.SelectedIndex = -1
            txttotalbayar.Clear()
            txtbukti.Clear()

            dtpelunasan.Value = Date.Now
            txtketerangan.Text = ""
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
        btngolunas.Enabled = False
        txtgolunas.Enabled = False
        btncaripelunasan.Enabled = False
        btnnext.Enabled = False

        'header
        txtnolunasutang.Enabled = False
        dtpelunasan.Enabled = True
        cmbsales.Enabled = True
        cmbbayar.Enabled = True
        txttotalbayar.Enabled = True

        'header
        txtnolunasutang.Enabled = False
        cmbsales.Enabled = True
        dtpelunasan.Enabled = True
        cmbsupplier.Enabled = True
        btncarisupplier.Enabled = True
        cmbbayar.Enabled = True
        txttotalbayar.Enabled = True
        txtbukti.Enabled = True

        'body
        txtkodepembelian.Clear()
        txtkodepembelian.Enabled = True
        btncaribeli.Enabled = True

        txttotalbeli.Clear()
        txttotalbeli.Text = 0

        txtbayarbeli.Clear()
        txtbayarbeli.Text = 0

        txtsisabeli.Clear()
        txtsisabeli.Text = 0

        btntambah.Enabled = True
        btnsesuaikan.Enabled = True

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        'total tabel pembelian
        txtketerangan.Enabled = True

    End Sub

    Sub tabel_utama()
        tabel = New DataTable
        With tabel
            .Columns.Add("pembelian_id")
            .Columns.Add("supplier_id")
            .Columns.Add("tanggal_pembelian")
            .Columns.Add("tanggal_jatuhtempo")
            .Columns.Add("total_pembelian", GetType(Double))
            .Columns.Add("bayar_utang", GetType(Double))
            .Columns.Add("sisa_utang", GetType(Double))
            .Columns.Add("terima_utang", GetType(Double))
        End With

        tabelsementara = New DataTable
        With tabelsementara
            .Columns.Add("pembelian_id")
            .Columns.Add("supplier_id")
            .Columns.Add("tanggal_pembelian")
            .Columns.Add("tanggal_jatuhtempo")
            .Columns.Add("total_pembelian", GetType(Double))
            .Columns.Add("bayar_utang", GetType(Double))
            .Columns.Add("sisa_utang", GetType(Double))
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "pembelian_id"
        GridColumn1.Caption = "id pembelian"
        GridColumn1.Width = 5

        GridColumn2.FieldName = "supplier_id"
        GridColumn2.Caption = "id supplier"
        GridColumn2.Width = 5

        GridColumn3.FieldName = "tanggal_pembelian"
        GridColumn3.Caption = "Tgl Pembelian"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        GridColumn3.Width = 20

        GridColumn4.FieldName = "tanggal_jatuhtempo"
        GridColumn4.Caption = "Tgl Jatuh Tempo"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "total_pembelian"
        GridColumn5.Caption = "Total Nota"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 20

        GridColumn6.FieldName = "bayar_utang"
        GridColumn6.Caption = "Telah Dibayar"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 20
        GridColumn6.Visible = False

        GridColumn7.FieldName = "sisa_utang"
        GridColumn7.Caption = "Sisa Nota"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 20
        GridColumn7.Visible = False

        GridColumn8.FieldName = "terima_utang"
        GridColumn8.Caption = "Terima Uang"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 20
    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("id")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn9.Caption = "id"
        GridColumn9.FieldName = "id"

        GridColumn10.Caption = "Tanggal"
        GridColumn10.FieldName = "tgl_pelunasan"

        GridColumn11.Caption = "Terima"
        GridColumn11.FieldName = "terima_utang"
        GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn11.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("pembelian_id")

        Call koneksi()
        sql = "SELECT tb_pelunasan_utang_detail.pelunasan_utang_id, tb_pelunasan_utang.tanggal_transaksi, tb_pelunasan_utang_detail.terima_utang FROM tb_pelunasan_utang_detail JOIN tb_pelunasan_utang ON tb_pelunasan_utang.id = tb_pelunasan_utang_detail.pelunasan_utang_id WHERE tb_pelunasan_utang_detail.pembelian_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("pelunasan_utang_id"), dr("tanggal_transaksi"), dr("terima_utang"))
        End While

        GridControl2.RefreshDataSource()

    End Sub

    Sub previewpelunasan(lihat As String)
        'With tabel
        '    .Columns.Add("pembelian_id")
        '    .Columns.Add("supplier_id")
        '    .Columns.Add("tanggal_pembelian")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_pembelian", GetType(Double))
        '    .Columns.Add("bayar_utang", GetType(Double))
        '    .Columns.Add("sisa_utang", GetType(Double))
        '    .Columns.Add("terima_utang", GetType(Double))
        'End With

        Call koneksi()
        sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE pelunasan_utang_id='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("pembelian_id"), dr("supplier_id"), dr("tanggal_pembelian"), dr("tanggal_jatuhtempo"), Val(dr("total_pembelian")), Val(dr("bayar_utang")), Val(dr("sisa_utang")), Val(dr("terima_utang")))
            tabelsementara.Rows.Add(dr("pembelian_id"), dr("supplier_id"), dr("tanggal_pembelian"), dr("tanggal_jatuhtempo"), Val(dr("total_pembelian")), Val(dr("bayar_utang")), Val(dr("sisa_utang")), Val(dr("terima_utang")))
        End While
        GridControl1.RefreshDataSource()
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub prosessimpan()
        'ini array


        Call simpan()
    End Sub
    Sub simpan()
        Dim tglpembeliansimpan, tgljatuhtemposimpan As Date
        Dim totalbeli(GridView1.RowCount - 1), bayarbeli(GridView1.RowCount - 1), sisabeli(GridView1.RowCount - 1) As Double

        Call koneksi()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        For i As Integer = 0 To GridView1.RowCount - 1
            'cek ke penjualan
            Call koneksi()
            sql = "SELECT * FROM tb_pembelian WHERE id = '" & GridView1.GetRowCellValue(i, "pembelian_id") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                totalbeli(i) = Val(dr("total_pembelian"))
                bayarbeli(i) = 0
            Else
                MsgBox("id pembelian : " + GridView1.GetRowCellValue(i, "pembelian_id") & " tidak ditemukan !")
                Exit Sub
            End If

            'IFNULL(SUM(kredit_kas), 0)
            Call koneksi()
            sql = "SELECT IFNULL(SUM(terima_utang), 0) AS terima_utang FROM tb_pelunasan_utang_detail WHERE pembelian_id = '" & GridView1.GetRowCellValue(i, "pembelian_id") & "' AND supplier_id ='" & idsupplier & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarbeli(i) = bayarbeli(i) + Val(dr("terima_utang"))
                sisabeli(i) = totalbeli(i) - bayarbeli(i)
            End If

            'hitung pembayaran
            If sisabeli(i) < Val(GridView1.GetRowCellValue(i, "terima_utang")) Then
                MsgBox("Kelebihan Bayar pada nota " + GridView1.GetRowCellValue(i, "pembelian_id"))
                Exit Sub
            End If
        Next


        Try
            sql = "INSERT INTO tb_pelunasan_utang(user_id, supplier_id, kas_id, tanggal_transaksi, jenis_kas, bayar_lunas, no_bukti, keterangan_lunas, void_lunas, print_lunas, posted_lunas, created_by, updated_by, date_created, last_updated) VALUES ('" & iduser & "','" & idsupplier & "', '" & idkas & "','" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & cmbbayar.Text & "', '" & totalbayar & "','" & txtbukti.Text & "','" & txtketerangan.Text & "','" & 0 & "','" & 0 & "','" & 1 & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idlunasutang = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                tglpembeliansimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_pembelian"))
                tgljatuhtemposimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))

                myCommand.CommandText = "INSERT INTO tb_pelunasan_utang_detail (pelunasan_utang_id, pembelian_id, supplier_id, tanggal_pembelian, tanggal_jatuhtempo, total_pembelian, bayar_utang, sisa_utang, terima_utang, created_by, updated_by, date_created, last_updated) VALUES ('" & idlunasutang & "', '" & GridView1.GetRowCellValue(i, "pembelian_id") & "', '" & GridView1.GetRowCellValue(i, "supplier_id") & "', '" & Format(tglpembeliansimpan, "yyyy-MM-dd HH:mm:ss") & "','" & Format(tgljatuhtemposimpan, "yyyy-MM-dd HH:mm:ss") & "','" & GridView1.GetRowCellValue(i, "total_pembelian") & "','" & GridView1.GetRowCellValue(i, "bayar_utang") & "','" & GridView1.GetRowCellValue(i, "sisa_utang") & "','" & GridView1.GetRowCellValue(i, "terima_utang") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "INSERT INTO tb_transaksi_kas (kode_kas, kode_utang, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & idkas & "','" & idlunasutang & "', 'BAYAR', now(), 'Transaksi Pelunasan Nomor " & idlunasutang & "','" & totalbayar & "', '" & 0 & "', '" & fmenu.kodeuser.Text & "', '" & fmenu.kodeuser.Text & "', now(), now())"
            myCommand.ExecuteNonQuery()


            For i As Integer = 0 To GridView1.RowCount - 1
                If (bayarbeli(i) + Val(GridView1.GetRowCellValue(i, "terima_utang"))).Equals(totalbeli(i)) Then
                    lunasstatus = 1

                    myCommand.CommandText = "UPDATE tb_pembelian SET lunas_pembelian = '" & lunasstatus & "' WHERE id = '" & GridView1.GetRowCellValue(i, "pembelian_id") & "' "
                    myCommand.ExecuteNonQuery()
                Else
                    lunasstatus = 0
                End If
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            MsgBox("Transaksi Berhasil", MsgBoxStyle.Information, "Berhasil")
            Me.Refresh()

            'history user ==========
            Call historysave("Menyimpan Data Lunas Utang Kode " & idlunasutang, idlunasutang, namaform)
            '=======================
            'kodelunasutang = txtnolunasutang.Text
            Call inisialisasi(idlunasutang)

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

            MsgBox("Transaksi Gagal", MsgBoxStyle.Information, "Gagal")
        End Try

    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then

            If cmbsales.Text IsNot "" And iduser > 0 Then
                If cmbbayar.Text IsNot "" Then
                    If txttotalbayar.Text > 0 Then
                        If totalbayar.Equals(Val(GridView1.Columns("terima_utang").SummaryItem.SummaryValue)) Then
                            Call prosessimpan()
                        Else
                            If totalselisih > 0 Then
                                MsgBox("Pembayaran Lebih " & Format(totalselisih, "##,##0").ToString)
                            ElseIf totalselisih < 0 Then
                                MsgBox("Pembayaran Kurang " & Format(totalselisih, "##,##0").ToString)
                            End If
                        End If
                    Else
                        MsgBox("Isi Nominal Pembayaran")
                    End If
                Else
                    MsgBox("Isi Pembayaran")
                End If
            Else
                MsgBox("Isi User")
            End If

        Else
            MsgBox("Isi Tabel Pelunasan")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then

            If cekcetakan(txtnolunasutang.Text, namaform).Equals(True) Then
                statusizincetak = False
                passwordid = 13
                fpassword.kodetabel = txtnolunasutang.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksi()
                    sql = "UPDATE tb_pelunasan_utang SET print_lunas = 1 WHERE id = '" & txtnolunasutang.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Lunas Utang Kode " & txtnolunasutang.Text, txtnolunasutang.Text, namaform)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksi()
                sql = "UPDATE tb_pelunasan_utang SET print_lunas = 1 WHERE id = '" & txtnolunasutang.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Lunas Utang Kode " & txtnolunasutang.Text, txtnolunasutang.Text, namaform)
                '========================

                cbprinted.Checked = True
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
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
        baris_barcode("kode_barcode") = txtnolunasutang.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtnolunasutang.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

        Dim tabel_faktur As New DataTable

        With tabel_faktur
            .Columns.Add("kode_pembelian")
            .Columns.Add("kode_supplier")
            .Columns.Add("tanggal_pembelian", GetType(Date))
            .Columns.Add("tanggal_jatuhtempo", GetType(Date))
            .Columns.Add("total_pembelian", GetType(Double))
            .Columns.Add("bayar_utang", GetType(Double))
            .Columns.Add("sisa_utang", GetType(Double))
            .Columns.Add("terima_utang", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_pembelian") = GridView1.GetRowCellValue(i, "pembelian_id")
            baris("kode_supplier") = GridView1.GetRowCellValue(i, "supplier_id")
            baris("tanggal_pembelian") = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_pembelian"))
            baris("tanggal_jatuhtempo") = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))
            baris("total_pembelian") = GridView1.GetRowCellValue(i, "total_pembelian")
            baris("bayar_utang") = GridView1.GetRowCellValue(i, "bayar_utang")
            baris("sisa_utang") = GridView1.GetRowCellValue(i, "sisa_utang")
            baris("terima_utang") = GridView1.GetRowCellValue(i, "terima_utang")
            tabel_faktur.Rows.Add(baris)
        Next
        rpt_faktur = New fakturlunasutang
        'rpt_faktur.SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnolunasutang.Text)
        rpt_faktur.SetParameterValue("supplier", txtsupplier.Text)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpelunasan.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("metode", cmbbayar.Text)
        rpt_faktur.SetParameterValue("bukti", txtbukti.Text)
        'rpt_faktur.SetParameterValue("totalbayar", totalbayar)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("namakasir", fmenu.kodeuser.Text)

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

    Sub perbarui(nomornota As String)
        Dim tglpembeliansimpan, tgljatuhtemposimpan As Date
        Dim totalbeli(GridView1.RowCount - 1), bayarbeli(GridView1.RowCount - 1), sisabeli(GridView1.RowCount - 1) As Double
        idlunasutang = nomornota

        Call koneksi()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        For i As Integer = 0 To GridView1.RowCount - 1
            'cek ke penjualan
            Call koneksi()
            sql = "SELECT * FROM tb_pembelian WHERE id = '" & GridView1.GetRowCellValue(i, "pembelian_id") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                totalbeli(i) = Val(dr("total_pembelian"))
                bayarbeli(i) = 0
            Else
                MsgBox("id pembelian : " & GridView1.GetRowCellValue(i, "pembelian_id") & " tidak ditemukan !")
                Exit Sub
            End If

            'IFNULL(SUM(kredit_kas), 0)
            Call koneksi()
            sql = "SELECT IFNULL(SUM(terima_utang), 0) AS terima_utang FROM tb_pelunasan_utang_detail WHERE pembelian_id = '" & GridView1.GetRowCellValue(i, "pembelian_id") & "' AND supplier_id ='" & idsupplier & "' AND NOT pelunasan_utang_id ='" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarbeli(i) = bayarbeli(i) + Val(dr("terima_utang"))
                sisabeli(i) = totalbeli(i) - bayarbeli(i)
            End If

            'hitung pembayaran
            If sisabeli(i) < Val(GridView1.GetRowCellValue(i, "terima_utang")) Then
                MsgBox("Kelebihan Bayar pada nota " & GridView1.GetRowCellValue(i, "pembelian_id"))
                Exit Sub
            End If
        Next

        Try
            myCommand.CommandText = "DELETE FROM tb_pelunasan_utang_detail WHERE pelunasan_utang_id = '" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            'kembalikan status
            For i As Integer = 0 To tabelsementara.Rows.Count - 1
                myCommand.CommandText = "UPDATE tb_pembelian SET lunas_pembelian = '" & 0 & "' WHERE id = '" & tabelsementara.Rows(i).Item(0) & "'"
                myCommand.ExecuteNonQuery()
            Next
            '====

            For i As Integer = 0 To GridView1.RowCount - 1
                tglpembeliansimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_pembelian"))
                tgljatuhtemposimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))

                myCommand.CommandText = "INSERT INTO tb_pelunasan_utang_detail (pelunasan_utang_id, pembelian_id, supplier_id, tanggal_pembelian, tanggal_jatuhtempo, total_pembelian, bayar_utang, sisa_utang, terima_utang, created_by, updated_by, date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "pembelian_id") & "','" & GridView1.GetRowCellValue(i, "supplier_id") & "', '" & Format(tglpembeliansimpan, "yyyy-MM-dd HH:mm:ss") & "','" & Format(tgljatuhtemposimpan, "yyyy-MM-dd HH:mm:ss") & "','" & GridView1.GetRowCellValue(i, "total_pembelian") & "','" & GridView1.GetRowCellValue(i, "bayar_utang") & "','" & GridView1.GetRowCellValue(i, "sisa_utang") & "','" & GridView1.GetRowCellValue(i, "terima_utang") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "UPDATE tb_pelunasan_utang SET user_id='" & iduser & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', supplier_id='" & idsupplier & "', kas_id='" & idkas & "', bayar_lunas='" & totalbayar & "', no_bukti='" & txtbukti.Text & "' ,keterangan_lunas='" & txtketerangan.Text & "', updated_by='" & fmenu.kodeuser.Text & "', last_updated=now()  WHERE id='" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            myCommand.CommandText = "UPDATE tb_transaksi_kas SET kode_kas='" & idkas & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', debet_kas='" & totalbayar & "', updated_by='" & fmenu.kodeuser.Text & "', last_updated=now() WHERE kode_utang='" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            For i As Integer = 0 To GridView1.RowCount - 1
                If (bayarbeli(i) + Val(GridView1.GetRowCellValue(i, "terima_utang"))).Equals(totalbeli(i)) Then
                    lunasstatus = 1

                    myCommand.CommandText = "UPDATE tb_pembelian SET lunas_pembelian = '" & lunasstatus & "' WHERE id = '" & GridView1.GetRowCellValue(i, "pembelian_id") & "'"
                    myCommand.ExecuteNonQuery()
                Else
                    lunasstatus = 0
                End If
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Mengedit Data Lunas Utang Kode " & nomornota, nomornota, namaform)
            '========================
            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            Call inisialisasi(txtnolunasutang.Text)

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
            MsgBox("Data Gagal Update", MsgBoxStyle.Information, "Gagal")
        End Try


    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then

                If cmbsales.Text IsNot "" And iduser > 0 Then
                    If cmbbayar.Text IsNot "" Then
                        If txttotalbayar.Text > 0 Then
                            If totalbayar.Equals(Val(GridView1.Columns("terima_utang").SummaryItem.SummaryValue)) Then
                                Call perbarui(txtnolunasutang.Text)
                            Else
                                If totalselisih > 0 Then
                                    MsgBox("Pembayaran Lebih " & Format(totalselisih, "##,##0").ToString)
                                ElseIf totalselisih < 0 Then
                                    MsgBox("Pembayaran Kurang " & Format(totalselisih, "##,##0").ToString)
                                End If
                            End If
                        Else
                            MsgBox("Isi Nominal Pembayaran")
                        End If
                    Else
                        MsgBox("Isi Pembayaran")
                    End If
                Else
                    MsgBox("Isi User")
                End If

            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(idlunasutang)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnolunasutang.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnolunasutang.Text)
    End Sub

    Private Sub btngolunas_Click(sender As Object, e As EventArgs) Handles btngolunas.Click
        If txtgolunas.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksi()
            sql = "SELECT kode_lunas FROM tb_pelunasan_utang WHERE id = '" & txtgolunas.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgolunas.Text)
            Else
                MsgBox("Pelunasan Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncaripelunasan_Click(sender As Object, e As EventArgs) Handles btncaripelunasan.Click
        tutupcaripelunasanutang = 1
        fcarilunasutang.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnolunasutang.Text)
    End Sub

    Sub caripembelian()
        Call koneksi()
        sql = "SELECT * FROM tb_pembelian WHERE id = '" & txtkodepembelian.Text & "' AND supplier_id ='" & idsupplier & "' AND lunas_pembelian = 0 LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            kodepembeliantbh = dr("id")
            kodesuppliertbh = dr("supplier_id")

            tglbelitbh = dr("tgl_pembelian")
            tgljatuhtempotbh = dr("tgl_jatuhtempo_pembelian")

            totalbelitbh = Val(dr("total_pembelian"))
            bayarutangtbh = 0
            sisautangtbh = 0

            'IFNULL(SUM(kredit_kas), 0)
            sql = "SELECT IFNULL(SUM(terima_utang), 0) AS terima_utang FROM tb_pelunasan_utang_detail WHERE pembelian_id = '" & txtkodepembelian.Text & "' AND supplier_id ='" & idsupplier & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarutangtbh = bayarutangtbh + Val(dr("terima_utang"))
                sisautangtbh = totalbelitbh - bayarutangtbh
            End If

            txttotalbeli.Text = Format(totalbelitbh, "##,##0")
            txtbayarbeli.Text = Format(bayarutangtbh, "##,##0")
            txtsisabeli.Text = Format(sisautangtbh, "##,##0")

        Else
            txttotalbeli.Text = 0
            txtbayarbeli.Text = 0
            txtsisabeli.Text = 0
        End If
    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call cariuser()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call cariuser()
    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsupplier.SelectedIndexChanged
        Call carisupplier()
    End Sub

    Private Sub cmbbayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbbayar.SelectedIndexChanged
        Call caribayar()
    End Sub

    Private Sub cmbbayar_TextChanged(sender As Object, e As EventArgs) Handles cmbbayar.TextChanged
        Call caribayar()
    End Sub

    Sub caribayar()
        Call koneksi()
        sql = "SELECT * FROM tb_kas WHERE kode_kas ='" & cmbbayar.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idkas = Val(dr("id"))
        Else
            idkas = 0
        End If
    End Sub

    Sub cariuser()
        Call koneksi()
        sql = "SELECT id FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
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

    Private Sub btncarisupplier_Click(sender As Object, e As EventArgs) Handles btncarisupplier.Click
        tutupsup = 4
        fcarisupplier.ShowDialog()
    End Sub

    Sub carisupplier()
        Call koneksi()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier ='" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idsupplier = Val(dr("id"))
            txtsupplier.Text = dr("nama_supplier")
            txtalamat.Text = dr("alamat_supplier")
            txttelp.Text = dr("telepon_supplier")
        Else
            idsupplier = 0
            txtsupplier.Text = ""
            txtalamat.Text = ""
            txttelp.Text = ""
        End If
    End Sub

    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged
        Call carisupplier()
    End Sub

    Private Sub txtkodepembelian_TextChanged(sender As Object, e As EventArgs) Handles txtkodepembelian.TextChanged
        Call caripembelian()
    End Sub

    Private Sub btncaribeli_Click(sender As Object, e As EventArgs) Handles btncaribeli.Click
        tutuplunasbeli = 1
        kodelunassupplier = idsupplier
        fcarilunasbeli.ShowDialog()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        If e.Column.FieldName = "sisa_utang" And btnbatal.Enabled = True Then
            GridView1.SetRowCellValue(e.RowHandle, "sisa_utang", 0)
            GridView1.SetRowCellValue(e.RowHandle, "terima_utang", Val(GridView1.GetRowCellValue(e.RowHandle, "total_pembelian")) - Val(GridView1.GetRowCellValue(e.RowHandle, "bayar_utang")))
        End If
        GridView1.RefreshData()
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "terima_utang" Then
            GridView1.SetRowCellValue(e.RowHandle, "sisa_utang", Val(GridView1.GetRowCellValue(e.RowHandle, "total_pembelian")) - Val(GridView1.GetRowCellValue(e.RowHandle, "bayar_utang")) - e.Value)
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateSelisihText))
    End Sub

    Private Sub GridView1_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles GridView1.RowUpdated
        BeginInvoke(New MethodInvoker(AddressOf UpdateSelisihText))
    End Sub

    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        BeginInvoke(New MethodInvoker(AddressOf UpdateSelisihText))
    End Sub

    Private Sub riteterimapelunasan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles riteterimapelunasan.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodepembelian.Clear()
        txttotalbeli.Clear()
        txttotalbeli.Text = 0
        txtbayarbeli.Clear()
        txtbayarbeli.Text = 0
        txtsisabeli.Clear()
        txtsisabeli.Text = 0
    End Sub

    Sub tambah()
        'With tabel
        '    .Columns.Add("pembelian_id")
        '    .Columns.Add("supplier_id")
        '    .Columns.Add("tanggal_pembelian")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_pembelian", GetType(Double))
        '    .Columns.Add("bayar_utang", GetType(Double))
        '    .Columns.Add("sisa_utang", GetType(Double))
        '    .Columns.Add("terima_utang", GetType(Double))
        'End With

        If txtkodepembelian.Text = "" Or txttotalbeli.Text = "" Or txtbayarbeli.Text = "" Or txtsisabeli.Text = "" Then
            MsgBox("Nota tidak ada !", MsgBoxStyle.Information, "Informasi")
            'Exit Sub
        Else
            Call koneksi()
            sql = "SELECT * FROM tb_pembelian WHERE id = '" & kodepembeliantbh & "' AND supplier_id ='" & kodesuppliertbh & "' AND lunas_pembelian = 0 LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                    tabel.Rows.Add(kodepembeliantbh, kodesuppliertbh, tglbelitbh, tgljatuhtempotbh, totalbelitbh, bayarutangtbh, sisautangtbh, 0)
                    Call reload_tabel()
                Else
                    Dim lokasi As Integer = -1

                    For i As Integer = 0 To GridView1.RowCount - 1
                        If String.Equals(GridView1.GetRowCellValue(i, "pembelian_id").ToString, kodepembeliantbh.ToUpper) Or GridView1.GetRowCellValue(i, "pembelian_id").ToString.Equals(kodepembeliantbh.ToUpper) Then
                            lokasi = i
                        End If
                    Next

                    If lokasi = -1 Then
                        tabel.Rows.Add(kodepembeliantbh, kodesuppliertbh, tglbelitbh, tgljatuhtempotbh, totalbelitbh, bayarutangtbh, sisautangtbh, 0)
                        Call reload_tabel()
                    Else
                        MsgBox("Nota sudah di tabel pelunasan !")
                    End If
                End If
                BeginInvoke(New MethodInvoker(AddressOf UpdateSelisihText))
            Else
                MsgBox("Nota tidak terdaftar !")
            End If
        End If
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
    End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub

    Private Sub flunasutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub txttotalbayar_TextChanged(sender As Object, e As EventArgs) Handles txttotalbayar.TextChanged
        If txttotalbayar.Text = "" Then
            txttotalbayar.Text = 0
        Else
            totalbayar = txttotalbayar.Text
            txttotalbayar.Text = Format(totalbayar, "##,##0")
            txttotalbayar.SelectionStart = Len(txttotalbayar.Text)
        End If
        BeginInvoke(New MethodInvoker(AddressOf UpdateSelisihText))
    End Sub

    Private Sub txttotalbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalbayar.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtselisih_TextChanged(sender As Object, e As EventArgs) Handles txtselisih.TextChanged
        If txtselisih.Text = "" Then
            txtselisih.Text = 0
        Else
            totalselisih = txtselisih.Text
            txtselisih.Text = Format(totalselisih, "##,##0")
            txtselisih.SelectionStart = Len(txtselisih.Text)
        End If
    End Sub

    Private Sub UpdateSelisihText()
        totalterima = Val(GridView1.Columns("terima_utang").SummaryItem.SummaryValue)
        totalselisih = totalbayar - totalterima
        txtselisih.Text = totalselisih
    End Sub

    Private Sub btnsesuaikan_Click(sender As Object, e As EventArgs) Handles btnsesuaikan.Click
        Dim lokasi As Integer = -1
        If totalbayar > 0 Then
            If totalselisih > 0 Then
                For i As Integer = 0 To GridView1.RowCount - 1
                    If Val(GridView1.GetRowCellValue(i, "terima_utang")).Equals(Val(GridView1.GetRowCellValue(i, "total_pembelian")) - Val(GridView1.GetRowCellValue(i, "bayar_utang"))) Then
                        lokasi = -1
                    Else
                        lokasi = i
                        Exit For
                    End If
                Next

                If lokasi > -1 And totalselisih > 0 Then
                    GridView1.SetRowCellValue(lokasi, "terima_utang", Val(GridView1.GetRowCellValue(lokasi, "total_pembelian")) - Val(GridView1.GetRowCellValue(lokasi, "bayar_utang")))

                ElseIf lokasi = -1 And totalselisih > 0 Then
                    MsgBox("Tambahkan Pembayaran Nota, Uang Lebih " + Format(totalselisih, "##,##0").ToString)
                End If
            ElseIf totalselisih < 0 Then
                For i As Integer = 0 To GridView1.RowCount - 1
                    If Val(GridView1.GetRowCellValue(i, "terima_utang")).Equals(Val(GridView1.GetRowCellValue(i, "total_pembelian")) - Val(GridView1.GetRowCellValue(i, "bayar_utang"))) Then
                        lokasi = i
                    Else
                        lokasi = -1
                    End If
                Next

                If lokasi > -1 And totalselisih < 0 Then
                    If (totalselisih * -1) < Val(GridView1.GetRowCellValue(lokasi, "total_pembelian")) - Val(GridView1.GetRowCellValue(lokasi, "bayar_utang")) Then
                        GridView1.SetRowCellValue(lokasi, "terima_utang", Val(GridView1.GetRowCellValue(lokasi, "total_pembelian")) + totalselisih)
                    Else
                        GridView1.SetRowCellValue(lokasi, "terima_utang", 0)
                    End If

                ElseIf lokasi = -1 And totalselisih < 0 Then
                    MsgBox("Kurangi Pembayaran Nota, Uang Kurang " + Format(totalselisih, "##,##0").ToString)
                    'End If
                ElseIf totalselisih = 0 Then
                    MsgBox("Pembayaran Tepat ")
                End If
            End If

            GridView1.RefreshData()
            BeginInvoke(New MethodInvoker(AddressOf UpdateSelisihText))

        Else
            MsgBox("Isi Total Pembayaran ")
        End If
    End Sub
End Class