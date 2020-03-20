Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class flunaspiutang
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel1 As DataTable
    Dim lunasstatus As Integer = 0
    Dim hitnumber As Integer
    Public kodelunaspiutang As String
    Dim totalbayar As Double
    Dim rpt_faktur As New ReportDocument

    Private Sub flunaspiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        kodelunaspiutang = currentnumber()

        'Call inisialisasi(kodelunaspiutang)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("bayar_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_penjualan", "{0:n0}")
            .Columns("terima_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "terima_penjualan", "{0:n0}")
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

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_lunas,3) FROM tb_pelunasan_piutang WHERE DATE_FORMAT(MID(`kode_lunas`, 3 , 6), ' %y ')+ MONTH(MID(`kode_lunas`,3 , 6)) + DAY(MID(`kode_lunas`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_lunas,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "PP" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "PP" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "PP" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "PP" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_lunas FROM tb_pelunasan_piutang ORDER BY kode_lunas DESC LIMIT 1;"
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
        sql = "SELECT kode_lunas FROM tb_pelunasan_piutang WHERE date_created < (SELECT date_created FROM tb_pelunasan_piutang WHERE kode_lunas = '" + previousnumber + "') ORDER BY date_created DESC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'Call inisialisasi(dr.Item(0).ToString)
                hitnumber = 0
            Else
                If hitnumber <= 2 Then
                    'Call inisialisasi(previousnumber)
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
        sql = "SELECT kode_lunas FROM tb_pelunasan_piutang WHERE date_created > (SELECT date_created FROM tb_pelunasan_piutang WHERE kode_lunas = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'Call inisialisasi(dr.Item(0).ToString)
                hitnumber = 0
            Else
                If hitnumber <= 2 Then
                    'Call inisialisasi(nextingnumber)
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

    'Sub caripelanggan()
    '    Dim kodepelangganfokus As String
    '    kodepelangganfokus = GridView1.GetFocusedRowCellValue("kode_customer")

    '    Call koneksii()
    '    sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & kodepelangganfokus & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    If dr.HasRows Then
    '        txtcustomer.Text = dr("nama_pelanggan")
    '        txtalamat.Text = dr("alamat_pelanggan")
    '        txttelp.Text = dr("telepon_pelanggan")
    '    Else
    '        txtcustomer.Text = ""
    '        txtalamat.Text = ""
    '        txttelp.Text = ""
    '    End If
    'End Sub

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
        btnnext.Enabled = False

        'header
        txtnolunaspiutang.Clear()
        txtnolunaspiutang.Text = autonumber()
        txtnolunaspiutang.Enabled = False

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        dtpelunasan.Enabled = True
        dtpelunasan.Value = Date.Now

        cmbcustomer.SelectedIndex = 0
        cmbcustomer.Enabled = True

        cmbbayar.SelectedIndex = 0
        cmbbayar.Enabled = True

        txttotalbayar.Clear()
        txttotalbayar.Text = 0
        txttotalbayar.Enabled = True

        txtbukti.Clear()
        txtbukti.Text = ""
        txtbukti.Enabled = True

        'body
        txtkodepenjualan.Clear()
        txtkodepenjualan.Enabled = True
        btncarijual.Enabled = True

        txttotaljual.Clear()
        txttotaljual.Text = 0

        txtbayarjual.Clear()
        txtbayarjual.Text = 0

        txtsisajual.Clear()
        txtsisajual.Text = 0

        btntambah.Enabled = True

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxcustomer()
        Call comboboxpembayaran()

        'buat tabel
        Call tabel_utama()

    End Sub

    'Sub inisialisasi(nomorkode As String)
    '    'bersihkan dan set default value
    '    GridControl1.Enabled = True
    '    GridControl2.Enabled = True
    '    GridView1.OptionsBehavior.Editable = False
    '    GridView2.OptionsBehavior.Editable = False

    '    'button tools
    '    btnbaru.Enabled = True
    '    btnsimpan.Enabled = False
    '    btnprint.Enabled = True
    '    btnedit.Enabled = True
    '    btnbatal.Enabled = False

    '    'button navigations
    '    btnprev.Enabled = True
    '    btngolunas.Enabled = True
    '    txtgolunas.Enabled = True
    '    btnnext.Enabled = True

    '    'header
    '    txtnolunaspiutang.Clear()
    '    txtnolunaspiutang.Text = autonumber()
    '    txtnolunaspiutang.Enabled = False

    '    txtnonota.Clear()
    '    txtnonota.Enabled = False

    '    dtpelunasan.Enabled = False
    '    dtpelunasan.Value = Date.Now

    '    cmbsales.Enabled = False

    '    cmbbayar.Enabled = False

    '    txttotalbayar.Clear()
    '    txttotalbayar.Enabled = False

    '    'total tabel penjualan
    '    txtketerangan.Enabled = False
    '    txtketerangan.Clear()

    '    'isi combo box
    '    Call comboboxuser()
    '    Call comboboxpembayaran()

    '    If nomorkode IsNot "" Then
    '        Call caripiutang(nomorkode)
    '    Else
    '        cbvoid.Checked = False
    '        cbprinted.Checked = False
    '        cbposted.Checked = False

    '        txtnolunaspiutang.Clear()
    '        txtnonota.Clear()
    '        dtpelunasan.Value = Date.Now
    '        cmbsales.Text = ""
    '        cmbbayar.Text = ""
    '        txttotalbayar.Clear()

    '        txtketerangan.Text = ""
    '    End If
    'End Sub

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
        btnnext.Enabled = False

        'header
        txtnolunaspiutang.Enabled = False
        dtpelunasan.Enabled = True
        cmbsales.Enabled = True
        cmbbayar.Enabled = True
        txttotalbayar.Enabled = True

        'header
        txtnolunaspiutang.Enabled = False
        cmbsales.Enabled = True
        dtpelunasan.Enabled = True
        cmbcustomer.Enabled = True
        cmbbayar.Enabled = True
        txttotalbayar.Enabled = True
        txtbukti.Enabled = True

        'body
        txtkodepenjualan.Clear()
        txtkodepenjualan.Enabled = True
        btncarijual.Enabled = True

        txttotaljual.Clear()
        txttotaljual.Text = 0

        txtbayarjual.Clear()
        txtbayarjual.Text = 0

        txtsisajual.Clear()
        txtsisajual.Text = 0

        btntambah.Enabled = True

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxcustomer()
        Call comboboxpembayaran()

        'simpan di tabel sementara
        Call koneksii()

        'hapus di tabel jual sementara
        Call koneksii()
        sql = "DELETE FROM tb_pelunasan_piutang_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'isi tabel sementara dengan data tabel detail
        sql = "INSERT INTO tb_pelunasan_piutang_detail_sementara SELECT * FROM tb_pelunasan_piutang_detail WHERE kode_lunas ='" & txtnolunaspiutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
    End Sub

    Sub tabel_utama()
        tabel1 = New DataTable

        With tabel1
            .Columns.Add("kode_penjualan")
            .Columns.Add("kode_customer")
            .Columns.Add("tanggal_penjualan")
            .Columns.Add("tanggal_jatuhtempo")
            .Columns.Add("total_penjualan", GetType(Double))
            .Columns.Add("bayar_penjualan", GetType(Double))
            .Columns.Add("terima_penjualan", GetType(Double))
        End With

        GridControl1.DataSource = tabel1

        GridColumn1.FieldName = "kode_penjualan"
        GridColumn1.Caption = "Kode Penjualan"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "kode_customer"
        GridColumn2.Caption = "Kode Customer"
        GridColumn2.Width = 20

        GridColumn3.FieldName = "tanggal_penjualan"
        GridColumn3.Caption = "Tgl Penjualan"
        GridColumn3.Width = 30

        GridColumn4.FieldName = "tanggal_jatuhtempo"
        GridColumn4.Caption = "Tgl Jatuh Tempo"
        GridColumn4.Width = 30

        GridColumn5.FieldName = "total_penjualan"
        GridColumn5.Caption = "Total"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 15

        GridColumn6.FieldName = "bayar_penjualan"
        GridColumn6.Caption = "Bayar"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "terima_penjualan"
        GridColumn7.Caption = "Terima"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 30

    End Sub

    Sub previewpelunasan(lihat As String)
        'With tabel1
        '    .Columns.Add("kode_penjualan")
        '    .Columns.Add("kode_customer")
        '    .Columns.Add("tanggal_penjualan")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_penjualan", GetType(Double))
        '    .Columns.Add("bayar_penjualan", GetType(Double))
        '    .Columns.Add("terima_penjualan", GetType(Double))
        'End With

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE kode_lunas='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_penjualan"), dr("kode_customer"), dr("tanggal_penjualan"), dr("tanggal_jatuhtempo"), Val(dr("total_penjualan")), Val(dr("bayar_penjualan")), Val(dr("terima_penjualan")))
            GridControl1.RefreshDataSource()
        End While
    End Sub

    'Sub carijual(cari As String)
    '    Dim kodepenjualanfokus As String
    '    kodepenjualanfokus = cari
    '    txtnonota.Text = kodepenjualanfokus
    '    Call loadinglunas(kodepenjualanfokus)
    'End Sub

    'Sub carilunas()
    '    Dim kodelunasfokus As String
    '    kodelunasfokus = GridView1.GetFocusedRowCellValue("kode_lunas")
    'End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    'Sub prosessimpan()
    '    Dim checkinglunas As Boolean
    '    Dim totaljual, bayarjual As Double

    '    'cek ke penjualan
    '    Call koneksii()
    '    sql = "SELECT total_penjualan FROM tb_penjualan WHERE kode_penjualan = '" & txtnonota.Text & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    If dr.HasRows Then
    '        totaljual = dr("total_penjualan")
    '    Else
    '        MsgBox("Penjualan tidak ditemukan !")
    '    End If

    '    'cek ke transaksi kas
    '    Call koneksii()
    '    sql = "SELECT IFNULL(SUM(kredit_kas), 0) As total_kas FROM tb_transaksi_kas WHERE kode_penjualan = '" & txtnonota.Text & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    dr.Read()

    '    If dr.HasRows Then
    '        bayarjual = Val(dr("total_kas"))
    '    Else
    '        bayarjual = 0
    '    End If

    '    'hitung pembayaran
    '    If (totaljual - bayarjual) < totalbayar Then
    '        checkinglunas = False
    '    Else
    '        checkinglunas = True
    '    End If


    '    If checkinglunas = True Then
    '        If (totalbayar + bayarjual).Equals(totaljual) Then
    '            lunasstatus = 1
    '        Else
    '            lunasstatus = 0
    '        End If
    '        Call simpan()
    '    Else
    '        MsgBox("Total lebih Bayar")
    '    End If

    'End Sub
    'Sub simpan()
    '    kodelunaspiutang = autonumber()
    '    Call koneksii()

    '    sql = "INSERT INTO tb_pelunasan_piutang (kode_lunas, kode_penjualan, tanggal_transaksi, kode_user, kode_kas, jenis_kas, bayar_lunas, keterangan_lunas, void_lunas, print_lunas, posted_lunas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunaspiutang & "', '" & txtnonota.Text & "', '" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', '" & cmbsales.Text & "', '" & cmbbayar.Text & "', '" & cmbbayar.Text & "', '" & totalbayar & "','" & txtketerangan.Text & "','" & 0 & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()


    '    sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_penjualan, kode_piutang, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & cmbbayar.Text & "','" & txtnonota.Text & "','" & txtnolunaspiutang.Text & "', 'BAYAR', now(), 'Transaksi Nota Nomor " & txtnonota.Text & "','" & 0 & "', '" & totalbayar & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()

    '    sql = "UPDATE tb_penjualan SET lunas_penjualan = '" & lunasstatus & "' WHERE kode_penjualan = '" & txtnonota.Text & "' "
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()

    '    'proses centang lunas penjualan


    '    MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
    '    Me.Refresh()

    '    kodelunaspiutang = txtnolunaspiutang.Text
    '    Call inisialisasi(kodelunaspiutang)
    '    Call carijual(txtnonota.Text)
    'End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If txtnolunaspiutang.Text IsNot "" Then
            If cmbsales.Text IsNot "" Then
                If cmbbayar.Text IsNot "" Then
                    If txttotalbayar.Text > 0 Then
                        'Call prosessimpan()
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
            MsgBox("Isi Nota Penjualan")
        End If

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            'Call cetak_faktur()

            sql = "UPDATE tb_pelunasan_piutang SET print_lunas = 1 WHERE kode_lunas = '" & txtnolunaspiutang.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    'Public Sub cetak_faktur()
    '    'Dim faktur As String
    '    'Dim tabel_faktur As New DataTable
    '    'With tabel_faktur
    '    '    .Columns.Add("kode_penjualan")
    '    '    .Columns.Add("kode_customer")
    '    '    .Columns.Add("kode_gudang")
    '    '    .Columns.Add("kode_user")
    '    '    .Columns.Add("tgl_penjualan")
    '    '    .Columns.Add("tgl_jatuhtempo_penjualan")
    '    '    .Columns.Add("diskon_penjualan", GetType(Double))
    '    '    .Columns.Add("pajak_penjualan", GetType(Double))
    '    '    .Columns.Add("ongkir_penjualan", GetType(Double))
    '    '    .Columns.Add("total_penjualan", GetType(Double))
    '    'End With

    '    'Dim baris As DataRow
    '    'For i As Integer = 0 To GridView1.RowCount - 1
    '    '    baris = tabel_faktur.NewRow
    '    '    baris("kode_penjualan") = GridView1.GetRowCellValue(i, "kode_penjualan")
    '    '    baris("kode_customer") = GridView1.GetRowCellValue(i, "kode_customer")
    '    '    baris("kode_gudang") = GridView1.GetRowCellValue(i, "kode_gudang")
    '    '    baris("kode_user") = GridView1.GetRowCellValue(i, "kode_user")
    '    '    baris("tgl_penjualan") = GridView1.GetRowCellValue(i, "tgl_penjualan")
    '    '    baris("tgl_jatuhtempo_penjualan") = GridView1.GetRowCellValue(i, "tgl_jatuhtempo_penjualan")
    '    '    baris("diskon_penjualan") = GridView1.GetRowCellValue(i, "diskon_penjualan")
    '    '    baris("ongkir_penjualan") = GridView1.GetRowCellValue(i, "ongkir_penjualan")
    '    '    baris("total_penjualan") = GridView1.GetRowCellValue(i, "total_penjualan")
    '    '    baris("pajak_penjualan") = GridView1.GetRowCellValue(i, "pajak_penjualan")
    '    '    tabel_faktur.Rows.Add(baris)
    '    'Next
    '    rpt_faktur = New fakturlunaspiutang
    '    'rpt_faktur.SetDataSource(tabel_faktur)

    '    rpt_faktur.SetParameterValue("nofaktur", txtnolunaspiutang.Text)
    '    rpt_faktur.SetParameterValue("penerima", fmenu.statususer.Text)

    '    rpt_faktur.SetParameterValue("tanggal", dtpelunasan.Text)
    '    rpt_faktur.SetParameterValue("totalbayar", totalbayar)
    '    rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
    '    rpt_faktur.SetParameterValue("nobayar", txtnonota.Text)

    '    SetReportPageSize("Faktur", 1)
    '    rpt_faktur.PrintToPrinter(1, False, 0, 0)
    'End Sub
    'Public Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
    '    Dim faktur As String
    '    Call koneksii()
    '    sql = "SELECT * FROM tb_printer WHERE nomor='2'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()
    '    If dr.HasRows Then
    '        faktur = dr("nama_printer")

    '    Else
    '        faktur = ""
    '    End If

    '    Try
    '        Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
    '        Dim PkSize As New System.Drawing.Printing.PaperSize
    '        ObjPrinterSetting.PrinterName = faktur
    '        For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
    '            If ObjPrinterSetting.PaperSizes.Item(i).PaperName = mPaperSize.Trim Then
    '                PkSize = ObjPrinterSetting.PaperSizes.Item(i)
    '                Exit For
    '            End If
    '        Next

    '        If PkSize IsNot Nothing Then
    '            Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = rpt_faktur.PrintOptions
    '            myAppPrintOptions.PrinterName = faktur
    '            myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
    '            rpt_faktur.PrintOptions.PaperOrientation = IIf(PaperOrientation = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)
    '        End If
    '        PkSize = Nothing
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'Sub prosesperbarui()
    '    Dim checkinglunas As Boolean
    '    Dim nilaihitung As String
    '    Dim totaljual, bayarjual As Double

    '    'cek ke penjualan
    '    Call koneksii()
    '    sql = "SELECT total_penjualan FROM tb_penjualan WHERE kode_penjualan = '" & txtnonota.Text & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    If dr.HasRows Then
    '        totaljual = dr("total_penjualan")
    '    Else
    '        MsgBox("Penjualan tidak ditemukan !")
    '    End If

    '    'cek ke transaksi kas
    '    Call koneksii()
    '    sql = "SELECT IFNULL(SUM(kredit_kas), 0) As total_kas FROM tb_transaksi_kas WHERE kode_penjualan = '" & txtnonota.Text & "' AND NOT kode_piutang ='" & txtnolunaspiutang.Text & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    dr.Read()

    '    If dr.HasRows Then
    '        bayarjual = Val(dr("total_kas"))
    '    Else
    '        bayarjual = 0
    '    End If

    '    'hitung pembayaran
    '    If (totaljual - bayarjual) < totalbayar Then
    '        checkinglunas = False
    '    Else
    '        checkinglunas = True
    '    End If


    '    If checkinglunas = True Then
    '        If (totalbayar + bayarjual).Equals(totaljual) Then
    '            lunasstatus = 1
    '        Else
    '            lunasstatus = 0
    '        End If
    '        Call perbarui()
    '    Else
    '        MsgBox("Total lebih Bayar")
    '    End If
    'End Sub

    'Sub perbarui()
    '    Call koneksii()

    '    sql = "UPDATE tb_pelunasan_piutang SET  kode_penjualan='" & txtnonota.Text & "', kode_user='" & cmbsales.Text & "', kode_kas='" & cmbbayar.Text & "', bayar_lunas='" & totalbayar & "', keterangan_lunas='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now()  WHERE  kode_lunas='" & txtnolunaspiutang.Text & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()

    '    sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbbayar.Text & "', kode_penjualan='" & txtnonota.Text & "', kode_piutang='" & txtnolunaspiutang.Text & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', kredit_kas='" & totalbayar & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_piutang='" & txtnolunaspiutang.Text & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()

    '    sql = "UPDATE tb_penjualan SET lunas_penjualan = '" & lunasstatus & "' WHERE kode_penjualan = '" & txtnonota.Text & "' "
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader()

    '    'proses centang lunas penjualan

    '    MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
    '    btnedit.Text = "Edit"

    '    Call inisialisasi(kodelunaspiutang)
    '    Call carijual(txtnonota.Text)
    'End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then
                If txtnolunaspiutang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        If cmbbayar.Text IsNot "" Then
                            If txttotalbayar.Text > 0 Then
                                'Call prosesperbarui()
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
                    MsgBox("Isi Nota Penjualan")
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            'Call inisialisasi(kodelunaspiutang)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            'Call inisialisasi(txtnolunaspiutang.Text)
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
            sql = "SELECT kode_lunas FROM tb_pelunasan_piutang WHERE kode_lunas  = '" + txtgolunas.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                'Call inisialisasi(txtgolunas.Text)
            Else
                MsgBox("Pelunasan Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtgolunas.Text)
    End Sub

    Private Sub btncarijual_Click(sender As Object, e As EventArgs) Handles btncarijual.Click

    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click

    End Sub

    'Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
    '    Call caripelanggan()
    'End Sub

    'Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
    '    Call carijual(GridView1.GetFocusedRowCellValue("kode_penjualan"))
    'End Sub

    'Sub caripiutang(nopiutang As String)
    '    sql = "SELECT * FROM tb_pelunasan_piutang WHERE kode_lunas  = '" + nopiutang + "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    dr.Read()

    '    If dr.HasRows Then
    '        txtnolunaspiutang.Text = dr("kode_lunas")
    '        txtnonota.Text = dr("kode_penjualan")
    '        dtpelunasan.Value = dr("tanggal_transaksi")
    '        cmbsales.Text = dr("kode_user")
    '        cmbbayar.Text = dr("kode_kas")
    '        txttotalbayar.Text = dr("bayar_lunas")
    '        txtketerangan.Text = dr("keterangan_lunas")
    '        cbvoid.Checked = dr("void_lunas")
    '        cbprinted.Checked = dr("print_lunas")
    '        cbposted.Checked = dr("posted_lunas")
    '        'cmbjenis.Text = dr("jenis_barang")
    '        'kode = dr("kode_barang")
    '        'modalbarang = dr("modal_barang")
    '        'txtmodal.Text = Format(modalbarang, "##,##0")
    '    End If

    'End Sub

    Private Sub flunaspiutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    'Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs)
    '    If btnsimpan.Enabled = False Then
    '        Call caripiutang(GridView2.GetFocusedRowCellValue("kode_lunas"))
    '    End If
    'End Sub

    'Private Sub txttotalbayar_TextChanged(sender As Object, e As EventArgs) Handles txttotalbayar.TextChanged
    '    If txttotalbayar.Text = "" Then
    '        txttotalbayar.Text = 0
    '    Else
    '        totalbayar = txttotalbayar.Text
    '        txttotalbayar.Text = Format(totalbayar, "##,##0")
    '        txttotalbayar.SelectionStart = Len(txttotalbayar.Text)
    '    End If
    'End Sub

    'Private Sub txttotalbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalbayar.KeyPress
    '    e.Handled = ValidAngka(e)
    'End Sub
End Class