Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class flunasutang
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel1, tabel2 As DataTable
    Dim lunasstatus As Integer = 0
    Dim hitnumber As Integer
    Public kodelunasutang As String
    Dim totalbayar As Double
    Dim rpt_faktur As New ReportDocument


    Private Sub flunasutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        hitnumber = 0
        kodelunasutang = currentnumber()
        Call loadingpembelian()
        Call inisialisasi(kodelunasutang)

        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("bayar_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_kas", "{0:n0}")
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
        cmbbayar.Items.Clear()
        cmbbayar.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbbayar.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbbayar.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_lunas,3) FROM tb_pelunasan_utang WHERE DATE_FORMAT(MID(`kode_lunas`, 3 , 6), ' %y ')+ MONTH(MID(`kode_lunas`,3 , 6)) + DAY(MID(`kode_lunas`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_lunas,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "PU" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "PU" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "PU" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "PU" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_lunas FROM tb_pelunasan_utang ORDER BY kode_lunas DESC LIMIT 1;"
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
        Finally
            'cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
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
        Finally
            'cnn.Close()
        End Try
    End Sub

    Sub carisupplier()
        Dim kodesupplierfokus As String
        kodesupplierfokus = GridView1.GetFocusedRowCellValue("kode_supplier")

        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier = '" & kodesupplierfokus & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtsupplier.Text = dr("nama_supplier")
            txtalamat.Text = dr("alamat_supplier")
            txttelp.Text = dr("telepon_supplier")
        Else
            txtsupplier.Text = ""
            txtalamat.Text = ""
            txttelp.Text = ""
        End If
    End Sub

    Sub awalbaru()
        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridControl2.Enabled = True
        GridView1.OptionsBehavior.Editable = False
        GridView2.OptionsBehavior.Editable = False

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
        btnnext.Enabled = False

        'header
        txtnolunasutang.Clear()
        txtnolunasutang.Text = autonumber()
        txtnolunasutang.Enabled = False

        txtnonota.Clear()
        txtnonota.Enabled = False

        dtpelunasan.Enabled = True
        dtpelunasan.Value = Date.Now

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        cmbbayar.SelectedIndex = 0
        cmbbayar.Enabled = True

        txttotalbayar.Clear()
        txttotalbayar.Enabled = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxpembayaran()

    End Sub
    Sub inisialisasi(nomorkode As String)
        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridControl2.Enabled = True
        GridView1.OptionsBehavior.Editable = False
        GridView2.OptionsBehavior.Editable = False

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
        btnnext.Enabled = True

        'header
        txtnolunasutang.Clear()
        txtnolunasutang.Text = autonumber()
        txtnolunasutang.Enabled = False

        txtnonota.Clear()
        txtnonota.Enabled = False

        dtpelunasan.Enabled = False
        dtpelunasan.Value = Date.Now

        cmbsales.Enabled = False

        cmbbayar.Enabled = False

        txttotalbayar.Clear()
        txttotalbayar.Enabled = False

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxpembayaran()

        If nomorkode IsNot "" Then
            Call cariutang(nomorkode)
        Else
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            txtnolunasutang.Clear()
            txtnonota.Clear()
            dtpelunasan.Value = Date.Now
            cmbsales.Text = ""
            cmbbayar.Text = ""
            txttotalbayar.Clear()

            txtketerangan.Text = ""
        End If
    End Sub
    Sub awaledit()
        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridControl2.Enabled = True
        GridView1.OptionsBehavior.Editable = False
        GridView2.OptionsBehavior.Editable = False

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
        btnnext.Enabled = False

        'header
        txtnolunasutang.Enabled = False
        txtnonota.Enabled = False
        dtpelunasan.Enabled = True
        cmbsales.Enabled = True
        cmbbayar.Enabled = True
        txttotalbayar.Enabled = True

        'total tabel pembelian
        txtketerangan.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxpembayaran()
    End Sub

    Sub tabel_utama()
        tabel1 = New DataTable

        With tabel1
            .Columns.Add("kode_pembelian")
            .Columns.Add("kode_supplier")
            .Columns.Add("kode_gudang")
            .Columns.Add("kode_user")
            .Columns.Add("tgl_pembelian")
            .Columns.Add("tgl_jatuhtempo_pembelian")
            .Columns.Add("diskon_pembelian", GetType(Double))
            .Columns.Add("pajak_pembelian", GetType(Double))
            .Columns.Add("ongkir_pembelian", GetType(Double))
            .Columns.Add("total_pembelian", GetType(Double))
        End With

        GridControl1.DataSource = tabel1

        GridColumn1.FieldName = "kode_pembelian"
        GridColumn1.Caption = "Kode Pembelian"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "kode_supplier"
        GridColumn2.Caption = "Kode Supplier"
        GridColumn2.Width = 20

        GridColumn3.FieldName = "kode_gudang"
        GridColumn3.Caption = "Kode Gudang"
        GridColumn3.Width = 20

        GridColumn4.FieldName = "kode_user"
        GridColumn4.Caption = "Kode User"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "tgl_pembelian"
        GridColumn5.Caption = "Tgl Pembelian"
        GridColumn5.Width = 30

        GridColumn6.FieldName = "tgl_jatuhtempo_pembelian"
        GridColumn6.Caption = "Tgl Jatuh Tempo"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "diskon_pembelian"
        GridColumn7.Caption = "Diskon"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 15

        GridColumn8.FieldName = "pajak_pembelian"
        GridColumn8.Caption = "Pajak"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 15

        GridColumn9.FieldName = "ongkir_pembelian"
        GridColumn9.Caption = "Ongkir"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "total_pembelian"
        GridColumn10.Caption = "Total"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

    End Sub
    Sub tabel_lunas()
        tabel2 = New DataTable

        With tabel2
            .Columns.Add("kode_lunas")
            .Columns.Add("kode_pembelian")
            .Columns.Add("tanggal_transaksi")
            .Columns.Add("kode_user")
            .Columns.Add("kode_kas")
            .Columns.Add("bayar_kas", GetType(Double))

        End With

        GridControl2.DataSource = tabel2

        GridColumn11.FieldName = "kode_lunas"
        GridColumn11.Caption = "Kode Lunas"
        GridColumn11.Width = 20

        GridColumn12.FieldName = "kode_pembelian"
        GridColumn12.Caption = "Kode Pembelian"
        GridColumn12.Width = 20

        GridColumn13.FieldName = "tanggal_transaksi"
        GridColumn13.Caption = "Tanggal Transaksi"
        GridColumn13.Width = 20

        GridColumn14.FieldName = "kode_user"
        GridColumn14.Caption = "Kode User"
        GridColumn14.Width = 20

        GridColumn15.FieldName = "kode_kas"
        GridColumn15.Caption = "Kode Kas"
        GridColumn15.Width = 20

        GridColumn16.FieldName = "bayar_kas"
        GridColumn16.Caption = "Bayar Kas"
        GridColumn16.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn16.DisplayFormat.FormatString = "{0:n0}"
        GridColumn16.Width = 20

    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtsupplier.Clear()
    End Sub
    Sub loadingpembelian()
        Call tabel_utama()
        Call tabel_lunas()
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_pembelian"), dr("kode_supplier"), dr("kode_gudang"), dr("kode_user"), dr("tgl_pembelian"), dr("tgl_jatuhtempo_pembelian"), Val(dr("diskon_pembelian")), Val(dr("pajak_pembelian")), Val(dr("ongkir_pembelian")), Val(dr("total_pembelian")))
            GridControl1.RefreshDataSource()
        End While
    End Sub

    Sub loadinglunas(lihat As String)
        Call tabel_lunas()
        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang WHERE kode_pembelian='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel2.Rows.Add(dr("kode_lunas"), dr("kode_pembelian"), dr("tanggal_transaksi"), dr("kode_user"), dr("kode_kas"), Val(dr("bayar_lunas")))
            GridControl2.RefreshDataSource()
        End While
    End Sub

    Sub caribeli(cari As String)
        Dim kodepembelianfokus As String
        kodepembelianfokus = cari
        txtnonota.Text = kodepembelianfokus
        Call loadinglunas(kodepembelianfokus)
    End Sub

    Sub carilunas()
        Dim kodelunasfokus As String
        kodelunasfokus = GridView1.GetFocusedRowCellValue("kode_lunas")
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub prosessimpan()
        Dim checkinglunas As Boolean
        Dim totalbeli, bayarbeli As Double

        'cek ke pembelian
        Call koneksii()
        sql = "SELECT total_pembelian FROM tb_pembelian WHERE kode_pembelian = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            totalbeli = dr("total_pembelian")
        Else
            MsgBox("Pembelian tidak ditemukan !")
        End If

        'cek ke transaksi kas
        Call koneksii()
        sql = "SELECT IFNULL(SUM(debet_kas), 0) As total_kas FROM tb_transaksi_kas WHERE kode_pembelian = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            bayarbeli = Val(dr("total_kas"))
        Else
            bayarbeli = 0
        End If

        'hitung pembayaran
        If (totalbeli - bayarbeli) < totalbayar Then
            checkinglunas = False
        Else
            checkinglunas = True
        End If


        If checkinglunas = True Then
            If (totalbayar + bayarbeli).Equals(totalbeli) Then
                lunasstatus = 1
            Else
                lunasstatus = 0
            End If
            Call simpan()
        Else
            MsgBox("Total lebih Bayar")
        End If

    End Sub
    Sub simpan()
        kodelunasutang = autonumber()
        Call koneksii()

        sql = "INSERT INTO tb_pelunasan_utang (kode_lunas, kode_pembelian, tanggal_transaksi, kode_user, kode_kas, jenis_kas, bayar_lunas, keterangan_lunas, void_lunas, print_lunas, posted_lunas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunasutang & "', '" & txtnonota.Text & "', '" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', '" & cmbsales.Text & "', '" & cmbbayar.Text & "', '" & cmbbayar.Text & "', '" & totalbayar & "','" & txtketerangan.Text & "','" & 0 & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()


        sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_pembelian, kode_utang, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & cmbbayar.Text & "','" & txtnonota.Text & "','" & txtnolunasutang.Text & "', 'BAYAR', now(), 'Transaksi Nota Nomor " & txtnonota.Text & "','" & totalbayar & "', '" & 0 & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "UPDATE tb_pembelian SET lunas_pembelian = '" & lunasstatus & "' WHERE kode_pembelian = '" & txtnonota.Text & "' "
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'proses centang lunas pembelian

        MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        Me.Refresh()

        kodelunasutang = txtnolunasutang.Text
        Call inisialisasi(kodelunasutang)
        Call caribeli(txtnonota.Text)
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If txtnonota.Text IsNot "" Then
            If cmbsales.Text IsNot "" Then
                If cmbbayar.Text IsNot "" Then
                    If txttotalbayar.Text > 0 Then
                        Call prosessimpan()
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
            MsgBox("Isi Nota Pembelian")
        End If

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            Call cetak_faktur()

            sql = "UPDATE tb_pelunasan_utang SET print_lunas = 1 WHERE kode_lunas = '" & txtnonota.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Public Sub cetak_faktur()
        'Dim faktur As String
        'Dim tabel_faktur As New DataTable
        'With tabel_faktur
        '    .Columns.Add("kode_pembelian")
        '    .Columns.Add("kode_supplier")
        '    .Columns.Add("kode_gudang")
        '    .Columns.Add("kode_user")
        '    .Columns.Add("tgl_pembelian")
        '    .Columns.Add("tgl_jatuhtempo_pembelian")
        '    .Columns.Add("diskon_pembelian", GetType(Double))
        '    .Columns.Add("pajak_pembelian", GetType(Double))
        '    .Columns.Add("ongkir_pembelian", GetType(Double))
        '    .Columns.Add("total_pembelian", GetType(Double))
        'End With

        'Dim baris As DataRow
        'For i As Integer = 0 To GridView1.RowCount - 1
        '    baris = tabel_faktur.NewRow
        '    baris("kode_pembelian") = GridView1.GetRowCellValue(i, "kode_pembelian")
        '    baris("kode_supplier") = GridView1.GetRowCellValue(i, "kode_supplier")
        '    baris("kode_gudang") = GridView1.GetRowCellValue(i, "kode_gudang")
        '    baris("kode_user") = GridView1.GetRowCellValue(i, "kode_user")
        '    baris("tgl_pembelian") = GridView1.GetRowCellValue(i, "tgl_pembelian")
        '    baris("tgl_jatuhtempo_pembelian") = GridView1.GetRowCellValue(i, "tgl_jatuhtempo_pembelian")
        '    baris("diskon_pembelian") = GridView1.GetRowCellValue(i, "diskon_pembelian")
        '    baris("ongkir_pembelian") = GridView1.GetRowCellValue(i, "ongkir_pembelian")
        '    baris("total_pembelian") = GridView1.GetRowCellValue(i, "total_pembelian")
        '    baris("pajak_pembelian") = GridView1.GetRowCellValue(i, "pajak_pembelian")
        '    tabel_faktur.Rows.Add(baris)
        'Next
        rpt_faktur = New fakturlunasutang
        'rpt_faktur.SetDataSource(tabel_faktur)

        rpt_faktur.SetParameterValue("nofaktur", txtnolunasutang.Text)
        rpt_faktur.SetParameterValue("penerima", fmenu.statususer.Text)

        rpt_faktur.SetParameterValue("tanggal", dtpelunasan.Text)
        rpt_faktur.SetParameterValue("totalbayar", totalbayar)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("nobayar", txtnonota.Text)

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
    Sub prosesperbarui()
        Dim checkinglunas As Boolean
        Dim totalbeli, bayarbeli As Double

        'cek ke penjualan
        Call koneksii()
        sql = "SELECT total_pembelian FROM tb_pembelian WHERE kode_pembelian = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            totalbeli = dr("total_pembelian")
        Else
            MsgBox("Pembelian tidak ditemukan !")
        End If

        'cek ke transaksi kas
        Call koneksii()
        sql = "SELECT IFNULL(SUM(debet_kas), 0) As total_kas FROM tb_transaksi_kas WHERE kode_pembelian = '" & txtnonota.Text & "' AND NOT kode_utang ='" & txtnolunasutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            bayarbeli = Val(dr("total_kas"))
        Else
            bayarbeli = 0
        End If

        'hitung pembayaran
        If (totalbeli - bayarbeli) < totalbayar Then
            checkinglunas = False
        Else
            checkinglunas = True
        End If


        If checkinglunas = True Then
            If (totalbayar + bayarbeli).Equals(totalbeli) Then
                lunasstatus = 1
            Else
                lunasstatus = 0
            End If
            Call perbarui()
        Else
            MsgBox("Total lebih Bayar")
        End If
    End Sub

    Sub perbarui()
        Call koneksii()

        sql = "UPDATE tb_pelunasan_utang SET  kode_pembelian='" & txtnonota.Text & "', kode_user='" & cmbsales.Text & "', kode_kas='" & cmbbayar.Text & "', bayar_lunas='" & totalbayar & "', keterangan_lunas='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now()  WHERE  kode_lunas='" & txtnolunasutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbbayar.Text & "', kode_pembelian='" & txtnonota.Text & "', kode_utang='" & txtnolunasutang.Text & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', debet_kas='" & totalbayar & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_utang='" & txtnolunasutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "UPDATE tb_pembelian SET lunas_pembelian = '" & lunasstatus & "' WHERE kode_pembelian = '" & txtnonota.Text & "' "
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'proses centang lunas pembelian

        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"

        Call inisialisasi(kodelunasutang)
        Call caribeli(txtnonota.Text)
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then
                If txtnonota.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        If cmbbayar.Text IsNot "" Then
                            If txttotalbayar.Text > 0 Then
                                Call prosesperbarui()
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
                    MsgBox("Isi Nota Pembelian")
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(kodelunasutang)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnolunasutang.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtgolunas.Text)
    End Sub

    Private Sub btngolunas_Click(sender As Object, e As EventArgs) Handles btngolunas.Click
        If txtgolunas.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT kode_lunas FROM tb_pelunasan_utang WHERE kode_lunas  = '" + txtgolunas.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgolunas.Text)
            Else
                MsgBox("Pelunasan Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtgolunas.Text)
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call carisupplier()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Call caribeli(GridView1.GetFocusedRowCellValue("kode_pembelian"))
    End Sub

    Sub cariutang(noutang As String)
        sql = "SELECT * FROM tb_pelunasan_utang WHERE kode_lunas  = '" + noutang + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            txtnolunasutang.Text = dr("kode_lunas")
            txtnonota.Text = dr("kode_pembelian")
            dtpelunasan.Value = dr("tanggal_transaksi")
            cmbsales.Text = dr("kode_user")
            cmbbayar.Text = dr("kode_kas")
            txttotalbayar.Text = dr("bayar_lunas")
            txtketerangan.Text = dr("keterangan_lunas")
            cbvoid.Checked = dr("void_lunas")
            cbprinted.Checked = dr("print_lunas")
            cbposted.Checked = dr("posted_lunas")
            'cmbjenis.Text = dr("jenis_barang")
            'kode = dr("kode_barang")
            'modalbarang = dr("modal_barang")
            'txtmodal.Text = Format(modalbarang, "##,##0")
        End If

    End Sub

    Private Sub flunasutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        If btnsimpan.Enabled = False Then
            Call cariutang(GridView2.GetFocusedRowCellValue("kode_lunas"))
        End If
    End Sub

    Private Sub txttotalbayar_TextChanged(sender As Object, e As EventArgs) Handles txttotalbayar.TextChanged
        If txttotalbayar.Text = "" Then
            txttotalbayar.Text = 0
        Else
            totalbayar = txttotalbayar.Text
            txttotalbayar.Text = Format(totalbayar, "##,##0")
            txttotalbayar.SelectionStart = Len(txttotalbayar.Text)
        End If
    End Sub

    Private Sub txttotalbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalbayar.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
End Class