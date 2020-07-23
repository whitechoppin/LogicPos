Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fbarangkeluar
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Public jenis, satuan, kodebarangkeluar, kodetransaksi, kodegudang As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglbarangkeluar As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
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
        kodebarangkeluar = currentnumber()
        Call inisialisasi(kodebarangkeluar)
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

        Call historysave("Membuka Transaksi Barang Keluar", "N/A")
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_barang_keluar,3) FROM tb_barang_keluar WHERE DATE_FORMAT(MID(`kode_barang_keluar`, 3 , 6), ' %y ')+ MONTH(MID(`kode_barang_keluar`,3 , 6)) + DAY(MID(`kode_barang_keluar`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_barang_keluar,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "BK" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "BK" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "BK" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "BK" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_barang_keluar FROM tb_barang_keluar ORDER BY kode_barang_keluar DESC LIMIT 1;"
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
        sql = "SELECT kode_barang_keluar FROM tb_barang_keluar WHERE date_created < (SELECT date_created FROM tb_barang_keluar WHERE kode_barang_keluar = '" + previousnumber + "')ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT kode_barang_keluar FROM tb_barang_keluar WHERE date_created > (SELECT date_created FROM tb_barang_keluar WHERE kode_barang_keluar = '" + nextingnumber + "')ORDER BY date_created ASC LIMIT 1"
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

    Sub previewbarangkeluar(lihat As String)
        sql = "SELECT * FROM tb_barang_keluar_detail WHERE kode_barang_keluar ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"))
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
    Sub comboboxgudang()
        Call koneksii()
        cmbgudang.Items.Clear()
        cmbgudang.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_gudang", cnn)
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
        txtgobarangkeluar.Enabled = False
        btncarikeluar.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbcustomer.Enabled = True
        cmbcustomer.SelectedIndex = 0
        cmbcustomer.Text = "00000000"
        cmbcustomer.Focus()
        btncaricustomer.Enabled = True

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        txtalamat.Enabled = True
        txttelp.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = 0
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
        Call comboboxcustomer()
        Call comboboxuser()
        Call comboboxgudang()

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
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbcustomer.Enabled = True
        'cmbcustomer.SelectedIndex = -1
        cmbcustomer.Focus()
        btncaricustomer.Enabled = True

        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        txtalamat.Enabled = True
        txttelp.Enabled = True

        cmbgudang.Enabled = True
        'cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtbarangkeluar.Enabled = True

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
        Call comboboxcustomer()
        Call comboboxuser()
        Call comboboxgudang()

        'simpan di tabel sementara
        Call koneksii()

        'hapus di tabel jual sementara
        Call koneksii()
        sql = "DELETE FROM tb_barang_keluar_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'isi tabel sementara dengan data tabel detail
        sql = "INSERT INTO tb_barang_keluar_detail_sementara SELECT * FROM tb_barang_keluar_detail WHERE kode_barang_keluar ='" & txtnonota.Text & "'"
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
        txtgobarangkeluar.Enabled = True
        btncarikeluar.Enabled = True
        btnnext.Enabled = True

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbcustomer.Enabled = False
        btncaricustomer.Enabled = False

        cmbsales.Enabled = False

        txtalamat.Enabled = False
        txttelp.Enabled = False

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
        Call comboboxcustomer()
        Call comboboxuser()
        Call comboboxgudang()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_barang_keluar WHERE kode_barang_keluar = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomornota = dr("kode_barang_keluar")
                    nomorcustomer = dr("kode_pelanggan")
                    nomorsales = dr("kode_user")
                    nomorgudang = dr("kode_gudang")

                    statusprint = dr("print_barang_keluar")
                    statusposted = dr("posted_barang_keluar")

                    viewtglbarangkeluar = dr("tgl_barang_keluar")
                    viewketerangan = dr("keterangan_barang_keluar")

                    txtnonota.Text = nomornota
                    cmbcustomer.Text = nomorcustomer
                    cmbsales.Text = nomorsales
                    cmbgudang.Text = nomorgudang

                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    dtbarangkeluar.Value = viewtglbarangkeluar

                    'isi tabel view pembelian

                    Call previewbarangkeluar(nomorkode)

                    'total tabel pembelian

                    txtketerangan.Text = viewketerangan

                    cnn.Close()
                End If
            End Using
        Else
            txtnonota.Clear()
            cmbcustomer.Text = ""
            cmbsales.Text = ""

            txtalamat.Clear()
            txttelp.Clear()

            cmbgudang.Text = ""

            cbprinted.Checked = False
            cbposted.Checked = False

            dtbarangkeluar.Value = Date.Now

            txtketerangan.Text = ""

        End If

    End Sub

    Sub caripelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbcustomer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtalamat.Text = dr("alamat_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
        Else
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

    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_stok.kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
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
    End Sub

    Private Sub fbarangkeluar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub cmbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcustomer.SelectedIndexChanged
        Call caripelanggan()
    End Sub

    Private Sub ritebanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritebanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        Call carigudang()
    End Sub

    Private Sub cmbcustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbcustomer.TextChanged
        Call caripelanggan()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        Call carigudang()
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        Call caribarang()
    End Sub

    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 3
        fcaricust.ShowDialog()
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 3
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupcaristok = 2
        kodegudangcari = cmbgudang.Text
        fcaristok.ShowDialog()
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
            If cmbcustomer.Text IsNot "" Then
                If txtgudang.Text IsNot "" Then
                    If cmbsales.Text IsNot "" Then
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

            If cekcetakan(txtnonota.Text).Equals(True) Then
                statusizincetak = False
                passwordid = 11
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_barang_keluar SET print_barang_keluar = 1 WHERE kode_barang_keluar = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Barang Keluar Kode " + txtnonota.Text, txtnonota.Text)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_barang_keluar SET print_barang_keluar = 1 WHERE kode_barang_keluar = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Barang Keluar Kode " + txtnonota.Text, txtnonota.Text)
                '========================

                cbprinted.Checked = True
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub cetak_faktur()
        'Dim faktur As String
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
        rpt_faktur.SetDataSource(tabel_faktur)
        'rpt.SetParameterValue("total", total2)
        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        rpt_faktur.SetParameterValue("tanggal", Format(dtbarangkeluar.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("penerima", fmenu.statususer.Text)

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
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text.Equals("Edit") Then
                btnedit.Text = "Update"
                Call awaledit()
            ElseIf btnedit.Text.Equals("Update") Then
                If GridView1.DataRowCount > 0 Then
                    If cmbcustomer.Text IsNot "" Then
                        If txtgudang.Text IsNot "" Then
                            If cmbsales.Text IsNot "" Then
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
            Call inisialisasi(kodebarangkeluar)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(kodebarangkeluar)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgobarangkeluar.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT kode_barang_keluar FROM tb_barang_keluar WHERE kode_barang_keluar  = '" + txtgobarangkeluar.Text + "' LIMIT 1"
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
        Call nextnumber(kodebarangkeluar)
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
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
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

                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
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
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
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
            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                If stokdatabase < stok Then
                    MsgBox("Stok dengan nama stok " + dr("nama_stok") + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
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
        kodebarangkeluar = autonumber()
        kodegudang = cmbgudang.Text
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
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & kodegudang & "'"
                myCommand.ExecuteNonQuery()
            Next

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "INSERT INTO tb_barang_keluar_detail ( kode_barang_keluar, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by,date_created, last_updated) VALUES ('" & kodebarangkeluar & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "INSERT INTO tb_barang_keluar (kode_barang_keluar, kode_pelanggan, kode_gudang, kode_user, tgl_barang_keluar, print_barang_keluar, posted_barang_keluar, keterangan_barang_keluar, created_by, updated_by, date_created, last_updated) VALUES ('" & kodebarangkeluar & "','" & cmbcustomer.Text & "','" & kodegudang & "','" & cmbsales.Text & "' , '" & Format(dtbarangkeluar.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            myCommand.ExecuteNonQuery()


            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Barang Keluar Kode " + kodebarangkeluar, kodebarangkeluar)
            '========================
            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(kodebarangkeluar)

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
        kodebarangkeluar = nomornota

        'periksa di barang di stok dulu
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim stokdatabasesementara As Integer
        Dim namastokdatabase As String
        Dim statusavailable As Boolean = True
        Dim kodegudangupdate As String

        'variabel transactional
        '=======================
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction
        '=======================

        'cari nota  yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT kode_gudang FROM tb_barang_keluar WHERE kode_barang_keluar = '" & kodebarangkeluar & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodegudangupdate = dr("kode_gudang")

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbgudang.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                namastokdatabase = dr("nama_stok")

                'mengambil selisih qty dari penjualan detail
                sql = "SELECT * FROM tb_barang_keluar_detail WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_barang_keluar ='" & kodebarangkeluar & "' LIMIT 1"
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
                    MsgBox("Stok dengan nama stok " + namastokdatabase + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " + GridView1.GetRowCellValue(i, "kode_stok") + " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            ' Start a local transaction
            myTrans = cnnx.BeginTransaction()
            ' Must assign both transaction object and connection
            ' to Command object for a pending local transaction
            myCommand.Connection = cnnx
            myCommand.Transaction = myTrans

            Try
                'update stok kembalikan
                Call koneksii()
                sql = "SELECT * FROM tb_barang_keluar_detail_sementara where kode_barang_keluar = '" & nomornota & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                While dr.Read
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & dr("qty") & "' WHERE kode_stok = '" & dr("kode_stok") & "' AND kode_gudang ='" & kodegudangupdate & "'"
                    myCommand.ExecuteNonQuery()
                End While

                'hapus di tabel jual detail
                Call koneksii()
                myCommand.CommandText = "DELETE FROM tb_barang_keluar_detail where kode_barang_keluar = '" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                'hapus di tabel jual sementara
                Call koneksii()
                sql = "DELETE FROM tb_barang_keluar_detail_sementara where kode_barang_keluar = '" & nomornota & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "' AND kode_gudang ='" & cmbgudang.Text & "'"
                    myCommand.ExecuteNonQuery()
                Next

                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "INSERT INTO tb_barang_keluar_detail ( kode_barang_keluar, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by, date_created, last_updated) VALUES ('" & kodebarangkeluar & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                myCommand.CommandText = "UPDATE tb_barang_keluar SET kode_pelanggan ='" & cmbcustomer.Text & "', kode_gudang ='" & cmbgudang.Text & "', kode_user ='" & cmbsales.Text & "' , tgl_barang_keluar ='" & Format(dtbarangkeluar.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_barang_keluar ='" & txtketerangan.Text & "', updated_by ='" & fmenu.statususer.Text & "', last_updated = now() WHERE kode_barang_keluar ='" & kodebarangkeluar & "'"
                myCommand.ExecuteNonQuery()

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                'history user ==========
                Call historysave("Mengedit Data Barang Keluar Kode " + kodebarangkeluar, kodebarangkeluar)
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
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub

    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted

    End Sub

    Private Sub GridView1_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles GridView1.RowUpdated

    End Sub

End Class