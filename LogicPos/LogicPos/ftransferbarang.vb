Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class ftransferbarang
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Public jenis, satuan, kodetransferbarang, kodetransaksi As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double
    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomordarigudang, nomorkegudang, viewketerangan As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtgltransfer As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    'variabel edit penjualan
    'variabel report
    Dim rpt_faktur As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    Dim countingbarang As Integer

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

    Private Sub ftransferbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        kodetransferbarang = currentnumber()
        Call inisialisasi(kodetransferbarang)
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

        Call historysave("Membuka Transaksi Transfer Barang", "N/A")
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_transfer_barang,3) FROM tb_transfer_barang WHERE DATE_FORMAT(MID(`kode_transfer_barang`, 3 , 6), ' %y ')+ MONTH(MID(`kode_transfer_barang`,3 , 6)) + DAY(MID(`kode_transfer_barang`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_transfer_barang,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "TB" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "TB" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "TB" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "TB" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_transfer_barang FROM tb_transfer_barang ORDER BY kode_transfer_barang DESC LIMIT 1;"
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
        sql = "SELECT kode_transfer_barang FROM tb_transfer_barang WHERE date_created < (SELECT date_created FROM tb_transfer_barang WHERE kode_transfer_barang = '" + previousnumber + "') ORDER BY date_created DESC LIMIT 1"
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
            cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_transfer_barang FROM tb_transfer_barang WHERE date_created > (SELECT date_created FROM tb_transfer_barang WHERE kode_transfer_barang = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
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
            cnn.Close()
        End Try
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

    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub previewtransferbarang(lihat As String)
        sql = "SELECT * FROM tb_transfer_barang_detail WHERE kode_transfer_barang ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"))
            GridControl1.RefreshDataSource()
        End While
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

    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodestok.Clear()
        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyak.Clear()
        txtkodestok.Focus()
    End Sub

    Sub comboboxkegudang()
        Call koneksii()
        cmbkegudang.Items.Clear()
        cmbkegudang.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_gudang", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbkegudang.AutoCompleteCustomSource.Add(dr("kode_gudang"))
                cmbkegudang.Items.Add(dr("kode_gudang"))
            End While
        End If
    End Sub

    Sub comboboxdarigudang()
        Call koneksii()
        cmbdarigudang.Items.Clear()
        cmbdarigudang.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_gudang", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbdarigudang.AutoCompleteCustomSource.Add(dr("kode_gudang"))
                cmbdarigudang.Items.Add(dr("kode_gudang"))
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
        txtgotransferbarang.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        cmbdarigudang.Enabled = True
        cmbdarigudang.SelectedIndex = 0
        btncaridarigudang.Enabled = True
        txtdarigudang.Enabled = False

        cmbkegudang.Enabled = True
        cmbkegudang.SelectedIndex = 0
        btncarikegudang.Enabled = True
        txtkegudang.Enabled = False

        dttransferbarang.Enabled = True
        dttransferbarang.Value = Date.Now


        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 0
        txtbanyak.Enabled = True

        lblsatuan.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxdarigudang()
        Call comboboxkegudang()

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
        txtgotransferbarang.Enabled = False
        btnnext.Enabled = False

        'header
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False


        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbdarigudang.Enabled = True
        btncaridarigudang.Enabled = True
        txtdarigudang.Enabled = False

        cmbkegudang.Enabled = True
        btncarikegudang.Enabled = True
        txtkegudang.Enabled = False

        dttransferbarang.Enabled = True

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 0
        txtbanyak.Enabled = True

        lblsatuan.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True


        'isi combo box
        Call comboboxuser()
        Call comboboxkegudang()
        Call comboboxdarigudang()

        'simpan di tabel sementara
        Call koneksii()

        'hapus di tabel jual sementara
        Call koneksii()
        sql = "DELETE FROM tb_transfer_barang_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'isi tabel sementara dengan data tabel detail
        sql = "INSERT INTO tb_transfer_barang_detail_sementara SELECT * FROM tb_transfer_barang_detail WHERE kode_transfer_barang ='" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

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
        txtgotransferbarang.Enabled = True
        btnnext.Enabled = True

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbsales.Enabled = False

        cmbdarigudang.Enabled = False
        btncaridarigudang.Enabled = False
        txtdarigudang.Enabled = False

        cmbkegudang.Enabled = False
        btncarikegudang.Enabled = False
        txtkegudang.Enabled = False

        dttransferbarang.Enabled = False
        dttransferbarang.Value = Date.Now

        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = False
        btncaribarang.Enabled = False

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 0
        txtbanyak.Enabled = False

        btntambah.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()
        Call comboboxdarigudang()
        Call comboboxkegudang()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_transfer_barang WHERE kode_transfer_barang = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomornota = dr("kode_transfer_barang")
                    nomorsales = dr("kode_user")
                    nomordarigudang = dr("kode_dari_gudang")
                    nomorkegudang = dr("kode_ke_gudang")

                    statusprint = dr("print_transfer_barang")
                    statusposted = dr("posted_transfer_barang")

                    viewtgltransfer = dr("tanggal_transfer_barang")
                    viewketerangan = dr("keterangan_transfer_barang")

                    txtnonota.Text = nomornota
                    cmbsales.Text = nomorsales
                    cmbdarigudang.Text = nomordarigudang
                    cmbkegudang.Text = nomorkegudang

                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    dttransferbarang.Value = viewtgltransfer

                    'isi tabel view pembelian

                    Call previewtransferbarang(nomorkode)

                    'total tabel pembelian

                    txtketerangan.Text = viewketerangan

                    cnn.Close()
                End If
            End Using
        Else
            txtnonota.Clear()
            cmbsales.Text = ""

            cmbdarigudang.Text = ""
            cmbkegudang.Text = ""

            cbprinted.Checked = False
            cbposted.Checked = False

            dttransferbarang.Value = Date.Now

            txtketerangan.Text = ""

        End If

    End Sub

    Sub caridarigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbdarigudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtdarigudang.Text = dr("nama_gudang")
        Else
            txtdarigudang.Text = ""
        End If
    End Sub

    Private Sub ftransferbarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub carikegudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbkegudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtkegudang.Text = dr("nama_gudang")
        Else
            txtkegudang.Text = ""
        End If
    End Sub

    Private Sub ritebanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritebanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan ='00000000' AND tb_stok.kode_gudang ='" & cmbdarigudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamabarang.Text = dr("nama_barang")
            txtkodebarang.Text = dr("kode_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            jenis = dr("jenis_barang")
        Else
            sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000' AND tb_stok.kode_gudang ='" & cmbdarigudang.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                txtnamabarang.Text = dr("nama_barang")
                txtkodebarang.Text = dr("kode_barang")
                satuan = dr("satuan_barang")
                lblsatuan.Text = satuan
                jenis = dr("jenis_barang")
            Else
                txtnamabarang.Text = ""
                txtkodebarang.Text = ""
                satuan = "satuan"
                lblsatuan.Text = satuan
                jenis = ""
            End If
        End If
    End Sub

    Private Sub cmbdarigudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbdarigudang.SelectedIndexChanged
        Call caridarigudang()
    End Sub

    Private Sub cmbkegudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkegudang.SelectedIndexChanged
        Call carikegudang()
    End Sub

    Private Sub cmbdarigudang_TextChanged(sender As Object, e As EventArgs) Handles cmbdarigudang.TextChanged
        Call caridarigudang()
    End Sub

    Private Sub cmbkegudang_TextChanged(sender As Object, e As EventArgs) Handles cmbkegudang.TextChanged
        Call carikegudang()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then
                If GridView1.DataRowCount > 0 Then
                    If txtdarigudang.Text IsNot "" Then
                        If txtkegudang.Text IsNot "" Then
                            If cmbsales.Text IsNot "" Then
                                'isi disini sub updatenya
                                Call perbarui(txtnonota.Text)
                            Else
                                MsgBox("Isi Sales")
                            End If
                        Else
                            MsgBox("Isi Ke Gudang")
                        End If
                    Else
                        MsgBox("Isi Dari Gudang")
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
            Call inisialisasi(kodetransferbarang)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub


    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            Call cetak_faktur()
            Call koneksii()
            sql = "UPDATE tb_transfer_barang SET print_transfer_barang = 1 WHERE kode_transfer_barang = '" & txtnonota.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'history user ==========
            Call historysave("Mencetak Data Transfer Barang Kode " + txtnonota.Text, txtnonota.Text)
            '========================

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
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

        rpt_faktur = New fakturtransferbarang
        rpt_faktur.SetDataSource(tabel_faktur)
        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        rpt_faktur.SetParameterValue("tanggal", dttransferbarang.Text)
        rpt_faktur.SetParameterValue("penerima", fmenu.statususer.Text)
        rpt_faktur.SetParameterValue("dari", txtdarigudang.Text)
        rpt_faktur.SetParameterValue("ke", txtkegudang.Text)

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
    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then
            If txtdarigudang.Text IsNot "" Then
                If txtkegudang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
                        Call proses()
                    Else
                        MsgBox("Isi Sales")
                    End If
                Else
                    MsgBox("Isi Ke Gudang")
                End If
            Else
                MsgBox("Isi Dari Gudang")
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

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgotransferbarang.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT kode_transfer_barang FROM tb_transfer_barang WHERE kode_transfer_barang  = '" + txtgotransferbarang.Text + "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgotransferbarang.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncaritransfer_Click(sender As Object, e As EventArgs) Handles btncaritransfer.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub

    Private Sub btncaridarigudang_Click(sender As Object, e As EventArgs) Handles btncaridarigudang.Click
        tutupgudang = 5
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btncarikegudang_Click(sender As Object, e As EventArgs) Handles btncarikegudang.Click
        tutupgudang = 6
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupstok = 3
        kodegudangcari = cmbdarigudang.Text
        fcaristok.ShowDialog()
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        Call caribarang()
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
    End Sub

    Sub tambah()
        'Columns.Add("kode_barang")
        'Columns.Add("kode_stok")
        'Columns.Add("nama_barang")
        'Columns.Add("banyak", GetType(Double))
        'Columns.Add("satuan_barang")
        'Columns.Add("jenis_barang")

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtbanyak.Text = "" Then
            MsgBox("Barang Kosong", MsgBoxStyle.Information, "Informasi")
            'Exit Sub
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND kode_gudang ='" & cmbdarigudang.Text & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    If dr("jumlah_stok") < banyak Then
                        MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                        'Exit Sub
                    Else
                        tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis)
                        Call reload_tabel()
                    End If
                End If
            Else 'kalau ada isi
                Dim tbbanyak As Integer = 0
                Dim tbnilaipersen As Double = 0
                Dim tbnilainominal As Double = 0
                Dim lokasi As Integer = -1

                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND kode_gudang ='" & cmbdarigudang.Text & "' LIMIT 1"
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
                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis)
                            Call reload_tabel()
                        End If
                    Else
                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis)
                            Call reload_tabel()
                        End If
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND kode_gudang ='" & cmbdarigudang.Text & "' LIMIT 1"
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
                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text) + tbbanyak, satuan, jenis)
                                Call reload_tabel()
                            End If
                        Else
                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text) + tbbanyak, satuan, jenis)
                                Call reload_tabel()
                            End If
                        End If
                    Else
                        MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                    End If
                End If
            End If
        End If
    End Sub

    Sub proses()
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim statusavailable As Boolean = True

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbdarigudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                If stokdatabase < stok Then
                    MsgBox("Stok dengan kode stok " + dr("kode_stok") + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
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

    Sub simpan()
        kodetransferbarang = autonumber()
        Call koneksii()

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbdarigudang.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbkegudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            If dr.HasRows Then
                sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbkegudang.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Else
                sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbkegudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "', now(), now() )"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            End If


        Next

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "INSERT INTO tb_transfer_barang_detail ( kode_transfer_barang, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by,date_created, last_updated) VALUES ('" & kodetransferbarang & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "INSERT INTO tb_transfer_barang (kode_transfer_barang, kode_dari_gudang, kode_ke_gudang, kode_user, tanggal_transfer_barang, print_transfer_barang, posted_transfer_barang, keterangan_transfer_barang, created_by, updated_by, date_created, last_updated) VALUES ('" & kodetransferbarang & "','" & cmbdarigudang.Text & "','" & cmbkegudang.Text & "','" & cmbsales.Text & "' , '" & Format(dttransferbarang.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'history user ==========
        Call historysave("Menyimpan Data Transfer Barang Kode " + kodetransferbarang, kodetransferbarang)
        '========================
        MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
        Call inisialisasi(kodetransferbarang)
    End Sub

    Sub perbarui(nomornota As String)
        kodetransferbarang = nomornota

        'periksa di barang di stok dulu
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim stokdatabasesementara As Integer
        Dim namastokdatabase As String
        Dim statusavailable As Boolean = True
        Dim kodedarigudangupdate, kodekegudangupdate As String

        'cari nota  yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT kode_dari_gudang, kode_ke_gudang FROM tb_transfer_barang WHERE kode_transfer_barang = '" & kodetransferbarang & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodedarigudangupdate = dr("kode_dari_gudang")
        kodekegudangupdate = dr("kode_ke_gudang")

        'periksa stok
        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbdarigudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                namastokdatabase = dr("nama_stok")

                'mengambil selisih qty dari penjualan detail
                sql = "SELECT * FROM tb_transfer_barang_detail WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_transfer_barang ='" & kodetransferbarang & "' LIMIT 1"
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
                    MsgBox("Stok dengan kode stok " + namastokdatabase + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
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
            sql = "DELETE FROM tb_transfer_barang_detail WHERE kode_transfer_barang = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'update stok kembalikan
            Call koneksii()
            sql = "SELECT * FROM tb_transfer_barang_detail_sementara WHERE kode_transfer_barang = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            While dr.Read
                sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & dr("qty") & "' WHERE kode_stok = '" & dr("kode_stok") & "' AND kode_gudang ='" & kodedarigudangupdate & "'"
                cmmd = New OdbcCommand(sql, cnn)
                drpenjualan = cmmd.ExecuteReader()

                sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & dr("qty") & "' WHERE kode_stok = '" & dr("kode_stok") & "' AND kode_gudang ='" & kodekegudangupdate & "'"
                cmmd = New OdbcCommand(sql, cnn)
                drpembelian = cmmd.ExecuteReader()
            End While

            'hapus di tabel jual sementara
            Call koneksii()
            sql = "DELETE FROM tb_transfer_barang_detail_sementara where kode_transfer_barang = '" & nomornota & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'gudang awal
            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbdarigudang.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Next

            'gudang tujuan
            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang='" & cmbkegudang.Text & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                If dr.HasRows Then
                    sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbkegudang.Text & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                Else
                    sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbkegudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "', now(), now() )"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                End If
            Next

            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "INSERT INTO tb_transfer_barang_detail ( kode_transfer_barang, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by, date_created, last_updated) VALUES ('" & kodetransferbarang & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Next

            sql = "UPDATE tb_transfer_barang SET kode_dari_gudang ='" & cmbdarigudang.Text & "', kode_ke_gudang ='" & cmbkegudang.Text & "', kode_user ='" & cmbsales.Text & "' , tanggal_transfer_barang ='" & Format(dttransferbarang.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_transfer_barang ='" & txtketerangan.Text & "', updated_by ='" & fmenu.statususer.Text & "', last_updated = now() WHERE kode_transfer_barang ='" & kodetransferbarang & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()


            'history user ==========
            Call historysave("Mengedit Data Transfer Barang Kode " + txtnonota.Text, txtnonota.Text)
            '========================

            MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
            'Call inisialisasi(nomornota)
            Call inisialisasi(txtnonota.Text)
            btnedit.Text = "Edit"
        End If

    End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
End Class