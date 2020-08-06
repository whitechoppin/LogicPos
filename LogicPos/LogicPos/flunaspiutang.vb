Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils
Imports ZXing

Public Class flunaspiutang
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim kode As String
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel, tabellunas As DataTable
    'body
    Dim kodepenjualantbh, kodepelanggantbh As String
    Dim tgljualtbh, tgljatuhtempotbh As Date
    Dim totaljualtbh, bayarpiutangtbh, sisapiutangtbh As Double
    '====
    'variabel bantuan view piutang
    Dim viewkodelunas, viewkodecustomer, viewkodesales, viewkodebayar, viewketerangan, viewnobukti As String
    Dim viewtotallunas As Double
    Dim statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpelunasan As DateTime
    '====
    Dim lunasstatus As Integer = 0
    Dim hitnumber As Integer
    Public kodelunaspiutang As String
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

    Private Sub flunaspiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        kodelunaspiutang = currentnumber()

        Call inisialisasi(kodelunaspiutang)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
            .Columns("bayar_piutang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_piutang", "{0:n0}")
            .Columns("sisa_piutang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_piutang", "{0:n0}")
            .Columns("terima_piutang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "terima_piutang", "{0:n0}")
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

        Call historysave("Membuka Administrasi Lunas Piutang", "N/A")
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
            'cnn.Close()
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
        sql = "SELECT kode_lunas FROM tb_pelunasan_piutang WHERE date_created > (SELECT date_created FROM tb_pelunasan_piutang WHERE kode_lunas = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
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

        cbvoid.Checked = False
        cbprinted.Checked = False
        cbposted.Checked = False

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
        btnsesuaikan.Enabled = True

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxcustomer()
        Call comboboxpembayaran()

        'buat tabel
        Call tabel_utama()

        txtselisih.Clear()
        txtselisih.Text = 0
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
        btngolunas.Enabled = True
        txtgolunas.Enabled = True
        btncaripelunasan.Enabled = True
        btnnext.Enabled = True

        'header
        txtnolunaspiutang.Clear()
        txtnolunaspiutang.Text = autonumber()
        txtnolunaspiutang.Enabled = False

        'cmbsales.SelectedIndex = 0
        cmbsales.Enabled = False

        dtpelunasan.Enabled = False
        dtpelunasan.Value = Date.Now

        'cmbcustomer.SelectedIndex = 0
        cmbcustomer.Enabled = False

        'cmbbayar.SelectedIndex = 0
        cmbbayar.Enabled = False

        txttotalbayar.Clear()
        txttotalbayar.Text = 0
        txttotalbayar.Enabled = False

        txtbukti.Clear()
        txtbukti.Text = ""
        txtbukti.Enabled = False

        'body
        txtkodepenjualan.Clear()
        txtkodepenjualan.Enabled = False
        btncarijual.Enabled = False

        txttotaljual.Clear()
        txttotaljual.Text = 0

        txtbayarjual.Clear()
        txtbayarjual.Text = 0

        txtsisajual.Clear()
        txtsisajual.Text = 0

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

        'isi combo box
        Call comboboxuser()
        Call comboboxcustomer()
        Call comboboxpembayaran()

        Call tabel_utama()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_pelunasan_piutang WHERE kode_lunas = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    viewkodelunas = dr("kode_lunas")
                    viewkodesales = dr("kode_user")
                    viewkodecustomer = dr("kode_pelanggan")
                    viewkodebayar = dr("kode_kas")
                    viewtglpelunasan = dr("tanggal_transaksi")
                    viewtotallunas = dr("bayar_lunas")
                    viewnobukti = dr("no_bukti")

                    statusvoid = dr("void_lunas")
                    statusprint = dr("print_lunas")
                    statusposted = dr("posted_lunas")

                    viewketerangan = dr("keterangan_lunas")

                    txtnolunaspiutang.Text = viewkodelunas
                    cmbsales.Text = viewkodesales
                    cmbcustomer.Text = viewkodecustomer
                    cmbbayar.Text = viewkodebayar
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
            End Using
        Else
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            txtnolunaspiutang.Clear()

            cmbsales.Text = ""
            cmbcustomer.Text = ""
            cmbbayar.Text = ""
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
        btnsesuaikan.Enabled = True

        'bersihkan dan set default value
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        'total tabel pembelian
        txtketerangan.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxcustomer()
        Call comboboxpembayaran()

        'simpan di tabel sementara
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
        tabel = New DataTable

        With tabel
            .Columns.Add("kode_penjualan")
            .Columns.Add("kode_customer")
            .Columns.Add("tanggal_penjualan")
            .Columns.Add("tanggal_jatuhtempo")
            .Columns.Add("total_penjualan", GetType(Double))
            .Columns.Add("bayar_piutang", GetType(Double))
            .Columns.Add("sisa_piutang", GetType(Double))
            .Columns.Add("terima_piutang", GetType(Double))
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_penjualan"
        GridColumn1.Caption = "Kode Penjualan"
        GridColumn1.Width = 15

        GridColumn2.FieldName = "kode_customer"
        GridColumn2.Caption = "Kode Customer"
        GridColumn2.Width = 15

        GridColumn3.FieldName = "tanggal_penjualan"
        GridColumn3.Caption = "Tgl Penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        GridColumn3.Width = 20

        GridColumn4.FieldName = "tanggal_jatuhtempo"
        GridColumn4.Caption = "Tgl Jatuh Tempo"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "total_penjualan"
        GridColumn5.Caption = "Total Nota"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 40

        GridColumn6.FieldName = "bayar_piutang"
        GridColumn6.Caption = "Telah Dibayar"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 40
        GridColumn6.Visible = False

        GridColumn7.FieldName = "sisa_piutang"
        GridColumn7.Caption = "Sisa Nota"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 40
        GridColumn7.Visible = False

        GridColumn8.FieldName = "terima_piutang"
        GridColumn8.Caption = "Terima Uang"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 40
    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("kode_lunas")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_piutang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn9.Caption = "Kode"
        GridColumn9.FieldName = "kode_lunas"

        GridColumn10.Caption = "Tanggal"
        GridColumn10.FieldName = "tgl_pelunasan"
        GridColumn11.Caption = "Terima"
        GridColumn11.FieldName = "terima_piutang"
        GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn11.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_penjualan")

        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" & kode & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            tabellunas.Rows.Add("DP : " + dr("kode_penjualan"), dr("last_updated"), dr("bayar_penjualan"))
        End If

        Call koneksii()
        sql = "SELECT tb_pelunasan_piutang_detail.kode_lunas, tb_pelunasan_piutang.tanggal_transaksi, tb_pelunasan_piutang_detail.terima_piutang FROM tb_pelunasan_piutang_detail JOIN tb_pelunasan_piutang ON tb_pelunasan_piutang.kode_lunas = tb_pelunasan_piutang_detail.kode_lunas WHERE tb_pelunasan_piutang_detail.kode_penjualan ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("kode_lunas"), dr("tanggal_transaksi"), dr("terima_piutang"))
        End While

        GridControl2.RefreshDataSource()


    End Sub

    Sub previewpelunasan(lihat As String)
        'With tabel
        '    .Columns.Add("kode_penjualan")
        '    .Columns.Add("kode_customer")
        '    .Columns.Add("tanggal_penjualan")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_penjualan", GetType(Double))
        '    .Columns.Add("bayar_piutang", GetType(Double))
        '    .Columns.Add("sisa_piutang", GetType(Double))
        '    .Columns.Add("terima_piutang", GetType(Double))
        'End With

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE kode_lunas='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_penjualan"), dr("kode_pelanggan"), dr("tanggal_penjualan"), dr("tanggal_jatuhtempo"), Val(dr("total_penjualan")), Val(dr("bayar_piutang")), Val(dr("sisa_piutang")), Val(dr("terima_piutang")))
            'GridControl1.RefreshDataSource()
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
        Dim totaljual(GridView1.RowCount - 1), bayarjual(GridView1.RowCount - 1), sisajual(GridView1.RowCount - 1) As Double


        For i As Integer = 0 To GridView1.RowCount - 1
            'cek ke penjualan
            Call koneksii()
            sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                totaljual(i) = Val(dr("total_penjualan"))
                bayarjual(i) = Val(dr("bayar_penjualan"))
            Else
                MsgBox("Kode penjualan : " + GridView1.GetRowCellValue(i, "kode_penjualan") + " tidak ditemukan !")
                Exit Sub
            End If

            'IFNULL(SUM(kredit_kas), 0)
            Call koneksii()
            sql = "SELECT IFNULL(SUM(terima_piutang), 0) AS terima_piutang FROM tb_pelunasan_piutang_detail WHERE kode_penjualan = '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "' AND kode_pelanggan ='" & cmbcustomer.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarjual(i) = bayarjual(i) + Val(dr("terima_piutang"))
                sisajual(i) = totaljual(i) - bayarjual(i)
            End If

            'hitung pembayaran
            If sisajual(i) < Val(GridView1.GetRowCellValue(i, "terima_piutang")) Then
                MsgBox("Kelebihan Bayar pada nota " + GridView1.GetRowCellValue(i, "kode_penjualan"))
                Exit Sub
            End If
        Next


        For i As Integer = 0 To GridView1.RowCount - 1
            If (bayarjual(i) + Val(GridView1.GetRowCellValue(i, "terima_piutang"))).Equals(totaljual(i)) Then
                lunasstatus = 1

                sql = "UPDATE tb_penjualan SET lunas_penjualan = '" & lunasstatus & "' WHERE kode_penjualan = '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Else
                lunasstatus = 0
            End If
        Next

        Call simpan()
    End Sub

    Sub simpan()
        kodelunaspiutang = autonumber()
        Dim tglpenjualansimpan, tgljatuhtemposimpan As Date

        Call koneksii()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction


        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            For i As Integer = 0 To GridView1.RowCount - 1
                tglpenjualansimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_penjualan"))
                tgljatuhtemposimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))

                myCommand.CommandText = "INSERT INTO tb_pelunasan_piutang_detail (kode_lunas, kode_penjualan, kode_pelanggan, tanggal_penjualan, tanggal_jatuhtempo, total_penjualan, bayar_piutang, sisa_piutang, terima_piutang, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunaspiutang & "', '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "', '" & GridView1.GetRowCellValue(i, "kode_customer") & "', '" & Format(tglpenjualansimpan, "yyyy-MM-dd HH:mm:ss") & "','" & Format(tgljatuhtemposimpan, "yyyy-MM-dd HH:mm:ss") & "','" & GridView1.GetRowCellValue(i, "total_penjualan") & "','" & GridView1.GetRowCellValue(i, "bayar_piutang") & "','" & GridView1.GetRowCellValue(i, "sisa_piutang") & "','" & GridView1.GetRowCellValue(i, "terima_piutang") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "INSERT INTO tb_pelunasan_piutang (kode_lunas, kode_user, tanggal_transaksi, kode_pelanggan, kode_kas, jenis_kas, bayar_lunas, no_bukti, keterangan_lunas, void_lunas, print_lunas, posted_lunas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunaspiutang & "', '" & cmbsales.Text & "', '" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', '" & cmbcustomer.Text & "', '" & cmbbayar.Text & "', '" & cmbbayar.Text & "', '" & totalbayar & "','" & txtbukti.Text & "','" & txtketerangan.Text & "','" & 0 & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            myCommand.ExecuteNonQuery()


            myCommand.CommandText = "INSERT INTO tb_transaksi_kas (kode_kas, kode_piutang, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & cmbbayar.Text & "','" & kodelunaspiutang & "', 'BAYAR', now(), 'Transaksi Pelunasan Nomor " & kodelunaspiutang & "','" & 0 & "', '" & totalbayar & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
            myCommand.ExecuteNonQuery()

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            MsgBox("Transaksi Berhasil", MsgBoxStyle.Information, "Berhasil")
            Me.Refresh()

            'history user ==========
            Call historysave("Menyimpan Data Lunas Piutang Kode " + kodelunaspiutang, kodelunaspiutang)
            '========================
            'kodelunaspiutang = txtnolunaspiutang.Text
            Call inisialisasi(kodelunaspiutang)

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
            If txtnolunaspiutang.Text IsNot "" Then
                If cmbsales.Text IsNot "" Then
                    If cmbbayar.Text IsNot "" Then
                        If txttotalbayar.Text > 0 Then
                            If totalbayar.Equals(Val(GridView1.Columns("terima_piutang").SummaryItem.SummaryValue)) Then
                                Call prosessimpan()
                            Else
                                If totalselisih > 0 Then
                                    MsgBox("Pembayaran Lebih " + Format(totalselisih, "##,##0").ToString)
                                ElseIf totalselisih < 0 Then
                                    MsgBox("Pembayaran Kurang " + Format(totalselisih, "##,##0").ToString)
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
                MsgBox("Isi No Nota Pelunasan")
            End If
        Else
            MsgBox("Isi Tabel Pelunasan")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then

            If cekcetakan(txtnolunaspiutang.Text).Equals(True) Then
                statusizincetak = False
                passwordid = 14
                fpassword.kodetabel = txtnolunaspiutang.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_pelunasan_piutang SET print_lunas = 1 WHERE kode_lunas = '" & txtnolunaspiutang.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Lunas Piutang Kode " + txtnolunaspiutang.Text, txtnolunaspiutang.Text)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_pelunasan_piutang SET print_lunas = 1 WHERE kode_lunas = '" & txtnolunaspiutang.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Lunas Piutang Kode " + txtnolunaspiutang.Text, txtnolunaspiutang.Text)
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
        baris_barcode("kode_barcode") = txtnolunaspiutang.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtnolunaspiutang.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_penjualan")
            .Columns.Add("kode_customer")
            .Columns.Add("tanggal_penjualan", GetType(Date))
            .Columns.Add("tanggal_jatuhtempo", GetType(Date))
            .Columns.Add("total_penjualan", GetType(Double))
            .Columns.Add("bayar_piutang", GetType(Double))
            .Columns.Add("sisa_piutang", GetType(Double))
            .Columns.Add("terima_piutang", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_penjualan") = GridView1.GetRowCellValue(i, "kode_penjualan")
            baris("kode_customer") = GridView1.GetRowCellValue(i, "kode_customer")
            baris("tanggal_penjualan") = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_penjualan"))
            baris("tanggal_jatuhtempo") = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))
            baris("total_penjualan") = GridView1.GetRowCellValue(i, "total_penjualan")
            baris("bayar_piutang") = GridView1.GetRowCellValue(i, "bayar_piutang")
            baris("sisa_piutang") = GridView1.GetRowCellValue(i, "sisa_piutang")
            baris("terima_piutang") = GridView1.GetRowCellValue(i, "terima_piutang")
            tabel_faktur.Rows.Add(baris)
        Next
        rpt_faktur = New fakturlunaspiutang
        'rpt_faktur.SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnolunaspiutang.Text)
        rpt_faktur.SetParameterValue("pembeli", txtcustomer.Text)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dtpelunasan.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("metode", cmbbayar.Text)
        rpt_faktur.SetParameterValue("bukti", txtbukti.Text)
        'rpt_faktur.SetParameterValue("totalbayar", totalbayar)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("namakasir", fmenu.statususer.Text)

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

    Sub prosesperbarui(nomornota As String)
        Dim totaljual(GridView1.RowCount - 1), bayarjual(GridView1.RowCount - 1), sisajual(GridView1.RowCount - 1) As Double

        For i As Integer = 0 To GridView1.RowCount - 1
            'cek ke penjualan
            Call koneksii()
            sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                totaljual(i) = Val(dr("total_penjualan"))
                bayarjual(i) = Val(dr("bayar_penjualan"))
            Else
                MsgBox("Kode penjualan : " + GridView1.GetRowCellValue(i, "kode_penjualan") + " tidak ditemukan !")
                Exit Sub
            End If

            'IFNULL(SUM(kredit_kas), 0)
            Call koneksii()
            sql = "SELECT IFNULL(SUM(terima_piutang), 0) AS terima_piutang FROM tb_pelunasan_piutang_detail WHERE kode_penjualan = '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "' AND kode_pelanggan ='" & cmbcustomer.Text & "' AND NOT kode_lunas ='" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarjual(i) = bayarjual(i) + Val(dr("terima_piutang"))
                sisajual(i) = totaljual(i) - bayarjual(i)
            End If

            'hitung pembayaran
            If sisajual(i) < Val(GridView1.GetRowCellValue(i, "terima_piutang")) Then
                MsgBox("Kelebihan Bayar pada nota " + GridView1.GetRowCellValue(i, "kode_penjualan"))
                Exit Sub
            End If
        Next


        'kembalikan status

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail_sementara where kode_lunas = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        While dr.Read
            sql = "UPDATE tb_penjualan SET lunas_penjualan = '" & 0 & "' WHERE kode_penjualan = '" & dr("kode_penjualan") & "' "
            cmmd = New OdbcCommand(sql, cnn)
            drlunaspenjualan = cmmd.ExecuteReader()
        End While

        '====

        For i As Integer = 0 To GridView1.RowCount - 1
            If (bayarjual(i) + Val(GridView1.GetRowCellValue(i, "terima_piutang"))).Equals(totaljual(i)) Then
                lunasstatus = 1

                sql = "UPDATE tb_penjualan SET lunas_penjualan = '" & lunasstatus & "' WHERE kode_penjualan = '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Else
                lunasstatus = 0
            End If
        Next

        Call perbarui(nomornota)
    End Sub

    Sub perbarui(nomornota As String)
        Dim tglpenjualansimpan, tgljatuhtemposimpan As Date
        kodelunaspiutang = nomornota

        Call koneksii()
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction


        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            myCommand.CommandText = "DELETE FROM tb_pelunasan_piutang_detail WHERE kode_lunas = '" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            For i As Integer = 0 To GridView1.RowCount - 1
                tglpenjualansimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_penjualan"))
                tgljatuhtemposimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))

                myCommand.CommandText = "INSERT INTO tb_pelunasan_piutang_detail (kode_lunas, kode_penjualan, kode_pelanggan, tanggal_penjualan, tanggal_jatuhtempo, total_penjualan, bayar_piutang, sisa_piutang, terima_piutang, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunaspiutang & "', '" & GridView1.GetRowCellValue(i, "kode_penjualan") & "', '" & GridView1.GetRowCellValue(i, "kode_customer") & "', '" & Format(tglpenjualansimpan, "yyyy-MM-dd HH:mm:ss") & "','" & Format(tgljatuhtemposimpan, "yyyy-MM-dd HH:mm:ss") & "','" & GridView1.GetRowCellValue(i, "total_penjualan") & "','" & GridView1.GetRowCellValue(i, "bayar_piutang") & "','" & GridView1.GetRowCellValue(i, "sisa_piutang") & "','" & GridView1.GetRowCellValue(i, "terima_piutang") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "UPDATE tb_pelunasan_piutang SET  kode_user='" & cmbsales.Text & "',tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', kode_pelanggan='" & cmbcustomer.Text & "',kode_kas='" & cmbbayar.Text & "', bayar_lunas='" & totalbayar & "', no_bukti='" & txtbukti.Text & "' ,keterangan_lunas='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now()  WHERE  kode_lunas='" & kodelunaspiutang & "'"
            myCommand.ExecuteNonQuery()

            myCommand.CommandText = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbbayar.Text & "', kode_piutang='" & kodelunaspiutang & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', kredit_kas='" & totalbayar & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_piutang='" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Mengedit Data Lunas Piutang Kode " + kodelunaspiutang, kodelunaspiutang)
            '========================
            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            Call inisialisasi(txtnolunaspiutang.Text)
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
                If txtnolunaspiutang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        If cmbbayar.Text IsNot "" Then
                            If txttotalbayar.Text > 0 Then
                                If totalbayar.Equals(Val(GridView1.Columns("terima_piutang").SummaryItem.SummaryValue)) Then
                                    Call prosesperbarui(txtnolunaspiutang.Text)
                                Else
                                    If totalselisih > 0 Then
                                        MsgBox("Pembayaran Lebih " + Format(totalselisih, "##,##0").ToString)
                                    ElseIf totalselisih < 0 Then
                                        MsgBox("Pembayaran Kurang " + Format(totalselisih, "##,##0").ToString)
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
                    MsgBox("Isi Nota Penjualan")
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(kodelunaspiutang)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnolunaspiutang.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnolunaspiutang.Text)
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
                Call inisialisasi(txtgolunas.Text)
            Else
                MsgBox("Pelunasan Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncaripelunasan_Click(sender As Object, e As EventArgs) Handles btncaripelunasan.Click
        tutupcaripelunasanpiutang = 1
        fcarilunaspiutang.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnolunaspiutang.Text)
    End Sub

    Sub caripenjualan()
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" & txtkodepenjualan.Text & "' AND kode_pelanggan ='" & cmbcustomer.Text & "' AND total_penjualan <> bayar_penjualan AND lunas_penjualan = 0 LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            kodepenjualantbh = dr("kode_penjualan")
            kodepelanggantbh = dr("kode_pelanggan")

            tgljualtbh = dr("tgl_penjualan")
            tgljatuhtempotbh = dr("tgl_jatuhtempo_penjualan")

            totaljualtbh = Val(dr("total_penjualan"))
            bayarpiutangtbh = Val(dr("bayar_penjualan"))
            sisapiutangtbh = 0

            'IFNULL(SUM(kredit_kas), 0)
            sql = "SELECT IFNULL(SUM(terima_piutang), 0) AS terima_piutang FROM tb_pelunasan_piutang_detail WHERE kode_penjualan = '" & txtkodepenjualan.Text & "' AND kode_pelanggan ='" & cmbcustomer.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarpiutangtbh = bayarpiutangtbh + Val(dr("terima_piutang"))
                sisapiutangtbh = totaljualtbh - bayarpiutangtbh
            End If

            txttotaljual.Text = Format(totaljualtbh, "##,##0")
            txtbayarjual.Text = Format(bayarpiutangtbh, "##,##0")
            txtsisajual.Text = Format(sisapiutangtbh, "##,##0")

        Else
            txttotaljual.Text = 0
            txtbayarjual.Text = 0
            txtsisajual.Text = 0
        End If
    End Sub

    Sub caripelanggan()
        Dim kodepelangganfokus As String
        kodepelangganfokus = cmbcustomer.Text

        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & kodepelangganfokus & "'"
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

    Private Sub cmbcustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbcustomer.TextChanged
        Call caripelanggan()
    End Sub

    Private Sub txtkodepenjualan_TextChanged(sender As Object, e As EventArgs) Handles txtkodepenjualan.TextChanged
        Call caripenjualan()
    End Sub

    Private Sub btncarijual_Click(sender As Object, e As EventArgs) Handles btncarijual.Click
        tutuplunasjual = 1
        kodelunascustomer = cmbcustomer.Text
        fcarilunasjual.ShowDialog()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        If e.Column.FieldName = "sisa_piutang" And btnbatal.Enabled = True Then
            GridView1.SetRowCellValue(e.RowHandle, "sisa_piutang", 0)
            GridView1.SetRowCellValue(e.RowHandle, "terima_piutang", Val(GridView1.GetRowCellValue(e.RowHandle, "total_penjualan")) - Val(GridView1.GetRowCellValue(e.RowHandle, "bayar_piutang")))
        End If
        GridView1.RefreshData()
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "terima_piutang" Then
            GridView1.SetRowCellValue(e.RowHandle, "sisa_piutang", Val(GridView1.GetRowCellValue(e.RowHandle, "total_penjualan")) - Val(GridView1.GetRowCellValue(e.RowHandle, "bayar_piutang")) - e.Value)
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

        txtkodepenjualan.Clear()
        txttotaljual.Clear()
        txttotaljual.Text = 0
        txtbayarjual.Clear()
        txtbayarjual.Text = 0
        txtsisajual.Clear()
        txtsisajual.Text = 0
    End Sub

    Sub tambah()
        'With tabel
        '    .Columns.Add("kode_penjualan")
        '    .Columns.Add("kode_customer")
        '    .Columns.Add("tanggal_penjualan")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_penjualan", GetType(Double))
        '    .Columns.Add("bayar_piutang", GetType(Double))
        '    .Columns.Add("terima_piutang", GetType(Double))
        'End With

        If txtkodepenjualan.Text = "" Or txttotaljual.Text = "" Or txtbayarjual.Text = "" Or txtsisajual.Text = "" Then
            MsgBox("Nota tidak ada !", MsgBoxStyle.Information, "Informasi")
            'Exit Sub
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" & txtkodepenjualan.Text & "' AND kode_pelanggan ='" & cmbcustomer.Text & "' AND total_penjualan <> bayar_penjualan AND lunas_penjualan = 0 LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                    tabel.Rows.Add(kodepenjualantbh, kodepelanggantbh, tgljualtbh, tgljatuhtempotbh, totaljualtbh, bayarpiutangtbh, sisapiutangtbh, 0)
                    Call reload_tabel()
                Else
                    Dim lokasi As Integer = -1

                    For i As Integer = 0 To GridView1.RowCount - 1
                        If String.Equals(GridView1.GetRowCellValue(i, "kode_penjualan").ToString, txtkodepenjualan.Text.ToUpper) Or GridView1.GetRowCellValue(i, "kode_penjualan").ToString.Equals(txtkodepenjualan.Text.ToUpper) Then
                            lokasi = i
                        End If
                    Next

                    If lokasi = -1 Then
                        tabel.Rows.Add(kodepenjualantbh, kodepelanggantbh, tgljualtbh, tgljatuhtempotbh, totaljualtbh, bayarpiutangtbh, sisapiutangtbh, 0)
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

    Private Sub flunaspiutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
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
        totalterima = GridView1.Columns("terima_piutang").SummaryItem.SummaryValue
        totalselisih = totalbayar - totalterima
        txtselisih.Text = totalselisih
    End Sub



    Private Sub btnsesuaikan_Click(sender As Object, e As EventArgs) Handles btnsesuaikan.Click
        Dim lokasi As Integer = -1
        If totalbayar > 0 Then
            If totalselisih > 0 Then
                For i As Integer = 0 To GridView1.RowCount - 1
                    If Val(GridView1.GetRowCellValue(i, "terima_piutang")).Equals(Val(GridView1.GetRowCellValue(i, "total_penjualan")) - Val(GridView1.GetRowCellValue(i, "bayar_piutang"))) Then
                        lokasi = -1
                    Else
                        lokasi = i
                        Exit For
                    End If
                Next

                If lokasi > -1 And totalselisih > 0 Then
                    GridView1.SetRowCellValue(lokasi, "terima_piutang", Val(GridView1.GetRowCellValue(lokasi, "total_penjualan")) - Val(GridView1.GetRowCellValue(lokasi, "bayar_piutang")))

                ElseIf lokasi = -1 And totalselisih > 0 Then
                    MsgBox("Tambahkan Pembayaran Nota, Uang Lebih " + Format(totalselisih, "##,##0").ToString)
                End If
            ElseIf totalselisih < 0 Then
                For i As Integer = 0 To GridView1.RowCount - 1
                    If Val(GridView1.GetRowCellValue(i, "terima_piutang")).Equals(Val(GridView1.GetRowCellValue(i, "total_penjualan")) - Val(GridView1.GetRowCellValue(i, "bayar_piutang"))) Then
                        lokasi = i
                    Else
                        lokasi = -1
                    End If
                Next

                If lokasi > -1 And totalselisih < 0 Then
                    If (totalselisih * -1) < Val(GridView1.GetRowCellValue(lokasi, "total_penjualan")) - Val(GridView1.GetRowCellValue(lokasi, "bayar_piutang")) Then
                        GridView1.SetRowCellValue(lokasi, "terima_piutang", Val(GridView1.GetRowCellValue(lokasi, "total_penjualan")) + totalselisih)
                    Else
                        GridView1.SetRowCellValue(lokasi, "terima_piutang", 0)
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