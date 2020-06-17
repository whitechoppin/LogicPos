Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class flunasutang
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel As DataTable
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
    Public kodelunasutang As String
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
        Call koneksii()

        hitnumber = 0
        kodelunasutang = currentnumber()

        Call inisialisasi(kodelunasutang)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("total_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_pembelian", "{0:n0}")
            .Columns("bayar_utang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_utang", "{0:n0}")
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

        Call historysave("Membuka Administrasi Lunas Utang", "N/A")
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

    Sub comboboxsupplier()
        Call koneksii()
        cmbsupplier.Items.Clear()
        cmbsupplier.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_supplier", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsupplier.AutoCompleteCustomSource.Add(dr("kode_supplier"))
                cmbsupplier.Items.Add(dr("kode_supplier"))
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
        txtnolunasutang.Clear()
        txtnolunasutang.Text = autonumber()
        txtnolunasutang.Enabled = False

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        dtpelunasan.Enabled = True
        dtpelunasan.Value = Date.Now

        cmbsupplier.SelectedIndex = 0
        cmbsupplier.Enabled = True

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

        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxsupplier()
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
        txtnolunasutang.Clear()
        txtnolunasutang.Text = autonumber()
        txtnolunasutang.Enabled = False

        'cmbsales.SelectedIndex = 0
        cmbsales.Enabled = False

        dtpelunasan.Enabled = False
        dtpelunasan.Value = Date.Now

        'cmbcustomer.SelectedIndex = 0
        cmbsupplier.Enabled = False

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

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        txtselisih.Clear()
        txtselisih.Text = 0

        'isi combo box
        Call comboboxuser()
        Call comboboxsupplier()
        Call comboboxpembayaran()

        Call tabel_utama()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_pelunasan_utang WHERE kode_lunas = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    viewkodelunas = dr("kode_lunas")
                    viewkodesales = dr("kode_user")
                    viewkodesupplier = dr("kode_supplier")
                    viewkodebayar = dr("kode_kas")
                    viewtglpelunasan = dr("tanggal_transaksi")
                    viewtotallunas = dr("bayar_lunas")
                    viewnobukti = dr("no_bukti")

                    statusvoid = dr("void_lunas")
                    statusprint = dr("print_lunas")
                    statusposted = dr("posted_lunas")

                    viewketerangan = dr("keterangan_lunas")

                    txtnolunasutang.Text = viewkodelunas
                    cmbsales.Text = viewkodesales
                    cmbsupplier.Text = viewkodesupplier
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

            txtnolunasutang.Clear()

            cmbsales.Text = ""
            cmbsupplier.Text = ""
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

        'total tabel pembelian
        txtketerangan.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxsupplier()
        Call comboboxpembayaran()

        'simpan di tabel sementara
        Call koneksii()

        sql = "DELETE FROM tb_pelunasan_utang_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'isi tabel sementara dengan data tabel detail
        sql = "INSERT INTO tb_pelunasan_utang_detail_sementara SELECT * FROM tb_pelunasan_utang_detail WHERE kode_lunas ='" & txtnolunasutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
    End Sub

    Sub tabel_utama()
        tabel = New DataTable

        With tabel
            .Columns.Add("kode_pembelian")
            .Columns.Add("kode_supplier")
            .Columns.Add("tanggal_pembelian")
            .Columns.Add("tanggal_jatuhtempo")
            .Columns.Add("total_pembelian", GetType(Double))
            .Columns.Add("bayar_utang", GetType(Double))
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_pembelian"
        GridColumn1.Caption = "Kode Pembelian"
        GridColumn1.Width = 15

        GridColumn2.FieldName = "kode_supplier"
        GridColumn2.Caption = "Kode Supplier"
        GridColumn2.Width = 15

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
        GridColumn5.Width = 40

        GridColumn6.FieldName = "bayar_utang"
        GridColumn6.Caption = "Telah Dibayar"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 40

        GridColumn7.FieldName = "terima_utang"
        GridColumn7.Caption = "Terima"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 40
    End Sub

    Sub previewpelunasan(lihat As String)
        'With tabel
        '    .Columns.Add("kode_pembelian")
        '    .Columns.Add("kode_supplier")
        '    .Columns.Add("tanggal_pembelian")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_pembelian", GetType(Double))
        '    .Columns.Add("bayar_utang", GetType(Double))
        '    .Columns.Add("terima_utang", GetType(Double))
        'End With

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE kode_lunas='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_pembelian"), dr("kode_supplier"), dr("tanggal_pembelian"), dr("tanggal_jatuhtempo"), Val(dr("total_pembelian")), Val(dr("bayar_utang")), Val(dr("terima_utang")))
            GridControl1.RefreshDataSource()
        End While
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub prosessimpan()
        Dim totalbeli(GridView1.RowCount - 1), bayarbeli(GridView1.RowCount - 1), sisabeli(GridView1.RowCount - 1) As Double


        For i As Integer = 0 To GridView1.RowCount - 1
            'cek ke penjualan
            Call koneksii()
            sql = "SELECT * FROM tb_pembelian WHERE kode_pembelian = '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                totalbeli(i) = Val(dr("total_pembelian"))
                bayarbeli(i) = 0
            Else
                MsgBox("Kode pembelian : " + GridView1.GetRowCellValue(i, "kode_pembelian") + " tidak ditemukan !")
                Exit Sub
            End If

            'IFNULL(SUM(kredit_kas), 0)
            Call koneksii()
            sql = "SELECT IFNULL(SUM(terima_utang), 0) AS terima_utang FROM tb_pelunasan_utang_detail WHERE kode_pembelian = '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "' AND kode_supplier ='" & cmbsupplier.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarbeli(i) = bayarbeli(i) + Val(dr("terima_utang"))
                sisabeli(i) = totalbeli(i) - bayarbeli(i)
            End If

            'hitung pembayaran
            If sisabeli(i) < Val(GridView1.GetRowCellValue(i, "terima_utang")) Then
                MsgBox("Kelebihan Bayar pada nota " + GridView1.GetRowCellValue(i, "kode_pembelian"))
                Exit Sub
            End If
        Next


        For i As Integer = 0 To GridView1.RowCount - 1
            If (bayarbeli(i) + Val(GridView1.GetRowCellValue(i, "terima_utang"))).Equals(totalbeli(i)) Then
                lunasstatus = 1

                sql = "UPDATE tb_pembelian SET lunas_pembelian = '" & lunasstatus & "' WHERE kode_pembelian = '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Else
                lunasstatus = 0
            End If
        Next

        Call simpan()
    End Sub
    Sub simpan()
        kodelunasutang = autonumber()
        Dim tglpembeliansimpan, tgljatuhtemposimpan As Date

        Call koneksii()

        For i As Integer = 0 To GridView1.RowCount - 1
            tglpembeliansimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_pembelian"))
            tgljatuhtemposimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))

            sql = "INSERT INTO tb_pelunasan_utang_detail (kode_lunas, kode_pembelian, kode_supplier, tanggal_pembelian, tanggal_jatuhtempo, total_pembelian, bayar_utang, terima_utang, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunasutang & "', '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "', '" & GridView1.GetRowCellValue(i, "kode_supplier") & "', '" & Format(tglpembeliansimpan, "yyyy-MM-dd HH:mm:ss") & "','" & Format(tgljatuhtemposimpan, "yyyy-MM-dd HH:mm:ss") & "','" & GridView1.GetRowCellValue(i, "total_pembelian") & "','" & GridView1.GetRowCellValue(i, "bayar_utang") & "','" & GridView1.GetRowCellValue(i, "terima_utang") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "INSERT INTO tb_pelunasan_utang (kode_lunas, kode_user, tanggal_transaksi, kode_supplier, kode_kas, jenis_kas, bayar_lunas, no_bukti, keterangan_lunas, void_lunas, print_lunas, posted_lunas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunasutang & "', '" & cmbsales.Text & "', '" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', '" & cmbsupplier.Text & "', '" & cmbbayar.Text & "', '" & cmbbayar.Text & "', '" & totalbayar & "','" & txtbukti.Text & "','" & txtketerangan.Text & "','" & 0 & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()


        sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_utang, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & cmbbayar.Text & "','" & kodelunasutang & "', 'BAYAR', now(), 'Transaksi Pelunasan Nomor " & kodelunasutang & "','" & totalbayar & "', '" & 0 & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        Me.Refresh()

        'history user ==========
        Call historysave("Menyimpan Data Lunas Utang Kode " + kodelunasutang, kodelunasutang)
        '========================

        'kodelunasutang = txtnolunasutang.Text
        Call inisialisasi(kodelunasutang)
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then
            If txtnolunasutang.Text IsNot "" Then
                If cmbsales.Text IsNot "" Then
                    If cmbbayar.Text IsNot "" Then
                        If txttotalbayar.Text > 0 Then
                            If totalbayar.Equals(Val(GridView1.Columns("terima_utang").SummaryItem.SummaryValue)) Then
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
            Call cetak_faktur()
            Call koneksii()
            sql = "UPDATE tb_pelunasan_utang SET print_lunas = 1 WHERE kode_lunas = '" & txtnolunasutang.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'history user ==========
            Call historysave("Mencetak Data Lunas Utang Kode " + txtnolunasutang.Text, txtnolunasutang.Text)
            '========================

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub


    Public Sub cetak_faktur()
        Dim faktur As String
        Dim tabel_faktur As New DataTable
        With tabel_faktur
            .Columns.Add("kode_pembelian")
            .Columns.Add("kode_supplier")
            .Columns.Add("tanggal_pembelian", GetType(Date))
            .Columns.Add("tanggal_jatuhtempo", GetType(Date))
            .Columns.Add("total_pembelian", GetType(Double))
            .Columns.Add("bayar_utang", GetType(Double))
            .Columns.Add("terima_utang", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_faktur.NewRow
            baris("kode_pembelian") = GridView1.GetRowCellValue(i, "kode_pembelian")
            baris("kode_supplier") = GridView1.GetRowCellValue(i, "kode_supplier")
            baris("tanggal_pembelian") = GridView1.GetRowCellValue(i, "tanggal_pembelian")
            baris("tanggal_jatuhtempo") = GridView1.GetRowCellValue(i, "tanggal_jatuhtempo")
            baris("total_pembelian") = GridView1.GetRowCellValue(i, "total_pembelian")
            baris("bayar_utang") = GridView1.GetRowCellValue(i, "bayar_utang")
            baris("terima_utang") = GridView1.GetRowCellValue(i, "terima_utang")
            tabel_faktur.Rows.Add(baris)
        Next
        rpt_faktur = New fakturlunasutang
        rpt_faktur.SetDataSource(tabel_faktur)

        rpt_faktur.SetParameterValue("nofaktur", txtnolunasutang.Text)
        rpt_faktur.SetParameterValue("supplier", txtsupplier.Text)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("tanggal", dtpelunasan.Value)
        rpt_faktur.SetParameterValue("metode", cmbbayar.Text)
        rpt_faktur.SetParameterValue("bukti", txtbukti.Text)
        rpt_faktur.SetParameterValue("totalbayar", totalbayar)
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
        Dim totalbeli(GridView1.RowCount - 1), bayarbeli(GridView1.RowCount - 1), sisabeli(GridView1.RowCount - 1) As Double

        For i As Integer = 0 To GridView1.RowCount - 1
            'cek ke penjualan
            Call koneksii()
            sql = "SELECT * FROM tb_pembelian WHERE kode_pembelian = '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                totalbeli(i) = Val(dr("total_pembelian"))
                bayarbeli(i) = 0
            Else
                MsgBox("Kode pembelian : " + GridView1.GetRowCellValue(i, "kode_pembelian") + " tidak ditemukan !")
                Exit Sub
            End If

            'IFNULL(SUM(kredit_kas), 0)
            Call koneksii()
            sql = "SELECT IFNULL(SUM(terima_utang), 0) AS terima_utang FROM tb_pelunasan_utang_detail WHERE kode_pembelian = '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "' AND kode_supplier ='" & cmbsupplier.Text & "' AND NOT kode_lunas ='" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                bayarbeli(i) = bayarbeli(i) + Val(dr("terima_utang"))
                sisabeli(i) = totalbeli(i) - bayarbeli(i)
            End If

            'hitung pembayaran
            If sisabeli(i) < Val(GridView1.GetRowCellValue(i, "terima_utang")) Then
                MsgBox("Kelebihan Bayar pada nota " + GridView1.GetRowCellValue(i, "kode_pembelian"))
                Exit Sub
            End If
        Next


        'kembalikan status

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang_detail_sementara where kode_lunas = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        While dr.Read
            sql = "UPDATE tb_pembelian SET lunas_pembelian = '" & 0 & "' WHERE kode_pembelian = '" & dr("kode_pembelian") & "' "
            cmmd = New OdbcCommand(sql, cnn)
            drlunaspembelian = cmmd.ExecuteReader()
        End While

        '====

        For i As Integer = 0 To GridView1.RowCount - 1
            If (bayarbeli(i) + Val(GridView1.GetRowCellValue(i, "terima_utang"))).Equals(totalbeli(i)) Then
                lunasstatus = 1

                sql = "UPDATE tb_pembelian SET lunas_pembelian = '" & lunasstatus & "' WHERE kode_pembelian = '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Else
                lunasstatus = 0
            End If
        Next

        Call perbarui(nomornota)
    End Sub

    Sub perbarui(nomornota As String)
        Dim tglpembeliansimpan, tgljatuhtemposimpan As Date
        kodelunasutang = nomornota

        Call koneksii()
        sql = "DELETE FROM tb_pelunasan_utang_detail WHERE kode_lunas = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        For i As Integer = 0 To GridView1.RowCount - 1
            tglpembeliansimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_pembelian"))
            tgljatuhtemposimpan = Date.Parse(GridView1.GetRowCellValue(i, "tanggal_jatuhtempo"))

            sql = "INSERT INTO tb_pelunasan_utang_detail (kode_lunas, kode_pembelian, kode_supplier, tanggal_pembelian, tanggal_jatuhtempo, total_pembelian, bayar_utang, terima_utang, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunasutang & "', '" & GridView1.GetRowCellValue(i, "kode_pembelian") & "', '" & GridView1.GetRowCellValue(i, "kode_supplier") & "', '" & Format(tglpembeliansimpan, "yyyy-MM-dd HH:mm:ss") & "','" & Format(tgljatuhtemposimpan, "yyyy-MM-dd HH:mm:ss") & "','" & GridView1.GetRowCellValue(i, "total_pembelian") & "','" & GridView1.GetRowCellValue(i, "bayar_utang") & "','" & GridView1.GetRowCellValue(i, "terima_utang") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "UPDATE tb_pelunasan_utang SET  kode_user='" & cmbsales.Text & "',tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', kode_supplier='" & cmbsupplier.Text & "',kode_kas='" & cmbbayar.Text & "', bayar_lunas='" & totalbayar & "', no_bukti='" & txtbukti.Text & "' ,keterangan_lunas='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now()  WHERE  kode_lunas='" & kodelunasutang & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbbayar.Text & "', kode_utang='" & kodelunasutang & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', debet_kas='" & totalbayar & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_utang='" & kodelunasutang & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'proses centang lunas penjualan
        'history user ==========
        Call historysave("Mengedit Data Lunas Utang Kode " + kodelunasutang, kodelunasutang)
        '========================

        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"

        Call inisialisasi(txtnolunasutang.Text)
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then
                If txtnolunasutang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        If cmbbayar.Text IsNot "" Then
                            If txttotalbayar.Text > 0 Then
                                If totalbayar.Equals(Val(GridView1.Columns("terima_utang").SummaryItem.SummaryValue)) Then
                                    Call prosesperbarui(txtnolunasutang.Text)
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
        Call prevnumber(txtnolunasutang.Text)
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

    Private Sub btncaripelunasan_Click(sender As Object, e As EventArgs) Handles btncaripelunasan.Click
        tutupcaripelunasanutang = 1
        fcarilunasutang.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnolunasutang.Text)
    End Sub

    Sub caripembelian()
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian WHERE kode_pembelian = '" & txtkodepembelian.Text & "' AND kode_supplier ='" & cmbsupplier.Text & "' AND lunas_pembelian = 0 LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            kodepembeliantbh = dr("kode_pembelian")
            kodesuppliertbh = dr("kode_supplier")

            tglbelitbh = dr("tgl_pembelian")
            tgljatuhtempotbh = dr("tgl_jatuhtempo_pembelian")

            totalbelitbh = Val(dr("total_pembelian"))
            bayarutangtbh = 0
            sisautangtbh = 0

            'IFNULL(SUM(kredit_kas), 0)
            sql = "SELECT IFNULL(SUM(terima_utang), 0) AS terima_utang FROM tb_pelunasan_utang_detail WHERE kode_pembelian = '" & txtkodepembelian.Text & "' AND kode_supplier ='" & cmbsupplier.Text & "'"
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

    Sub carisupplier()
        Dim kodesupplierfokus As String
        kodesupplierfokus = cmbsupplier.Text

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

    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged
        Call carisupplier()
    End Sub

    Private Sub txtkodepembelian_TextChanged(sender As Object, e As EventArgs) Handles txtkodepembelian.TextChanged
        Call caripembelian()
    End Sub

    Private Sub btncaribeli_Click(sender As Object, e As EventArgs) Handles btncaribeli.Click
        tutuplunasbeli = 1
        kodelunassupplier = cmbsupplier.Text
        fcarilunasbeli.ShowDialog()
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        If e.Column.FieldName = "total_pembelian" And btnbatal.Enabled = True Then
            GridView1.SetRowCellValue(e.RowHandle, "terima_utang", Val(GridView1.GetRowCellValue(e.RowHandle, "total_pembelian")) - Val(GridView1.GetRowCellValue(e.RowHandle, "bayar_utang")))
        End If
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
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
        '    .Columns.Add("kode_pembelian")
        '    .Columns.Add("kode_supplier")
        '    .Columns.Add("tanggal_pembelian")
        '    .Columns.Add("tanggal_jatuhtempo")
        '    .Columns.Add("total_pembelian", GetType(Double))
        '    .Columns.Add("bayar_utang", GetType(Double))
        '    .Columns.Add("terima_utang", GetType(Double))
        'End With

        If txtkodepembelian.Text = "" Or txttotalbeli.Text = "" Or txtbayarbeli.Text = "" Or txtsisabeli.Text = "" Then
            MsgBox("Nota tidak ada !", MsgBoxStyle.Information, "Informasi")
            'Exit Sub
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_pembelian WHERE kode_pembelian = '" & txtkodepembelian.Text & "' AND kode_supplier ='" & cmbsupplier.Text & "' AND lunas_pembelian = 0 LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                    tabel.Rows.Add(kodepembeliantbh, kodesuppliertbh, tglbelitbh, tgljatuhtempotbh, totalbelitbh, bayarutangtbh, 0)
                    Call reload_tabel()
                Else
                    Dim lokasi As Integer = -1

                    For i As Integer = 0 To GridView1.RowCount - 1
                        If GridView1.GetRowCellValue(i, "kode_pembelian").Equals(txtkodepembelian.Text) Then
                            lokasi = i
                        End If
                    Next

                    If lokasi = -1 Then
                        tabel.Rows.Add(kodepembeliantbh, kodesuppliertbh, tglbelitbh, tgljatuhtempotbh, totalbelitbh, bayarutangtbh, 0)
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
        totalterima = GridView1.Columns("terima_utang").SummaryItem.SummaryValue
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