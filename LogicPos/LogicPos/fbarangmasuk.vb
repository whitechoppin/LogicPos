Imports System.Data.Odbc

Public Class fbarangmasuk
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Dim tabel As DataTable
    Dim hitnumber As Integer
    Dim harga, modalpembelian, ongkir, ppn, diskonpersen, diskonnominal, ppnpersen, ppnnominal, total1, total2, grandtotal, banyak As Double
    Dim satuan, jenis, supplier, kodebarangmasuk As String
    Public isi As String
    'variabel bantuan view pembelian
    Dim nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan, viewbayar As String
    Dim statusprint, statusposted, statusedit As Boolean

    Dim rpt_faktur As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Dim viewtglbarangmasuk As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir As Double

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

    Private Sub fbarangmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'mulai
        hitnumber = 0
        kodebarangmasuk = currentnumber()
        Call inisialisasi(kodebarangmasuk)
        With GridView1
            '.OptionsView.ColumnAutoWidth = False ' agar muncul scrol bar
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            '.OptionsView.ShowAutoFilterRow = True 'aktifkan autofilter
            '.OptionsView.EnableAppearanceOddRow = True 'aktifkan style
            '.OptionsPrint.EnableAppearanceOddRow = True 'aktifkan style saat print
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

        Call historysave("Membuka Transaksi Barang Masuk", "N/A")
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_barang_masuk,3) FROM tb_barang_masuk WHERE DATE_FORMAT(MID(`kode_barang_masuk`, 3 , 6), ' %y ')+ MONTH(MID(`kode_barang_masuk`,3 , 6)) + DAY(MID(`kode_barang_masuk`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_barang_masuk,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "BM" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "BM" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "BM" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "BM" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_barang_masuk FROM tb_barang_masuk ORDER BY kode_barang_masuk DESC LIMIT 1;"
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
        sql = "SELECT kode_barang_masuk FROM tb_barang_masuk WHERE date_created < (SELECT date_created FROM tb_barang_masuk WHERE kode_barang_masuk = '" + previousnumber + "')ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT kode_barang_masuk FROM tb_barang_masuk WHERE date_created > (SELECT date_created FROM tb_barang_masuk WHERE kode_barang_masuk = '" + nextingnumber + "')ORDER BY date_created ASC LIMIT 1"
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
    Sub previewpembelian(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_barang_masuk_detail WHERE kode_barang_masuk ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"))
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
        txtgobarangmasuk.Enabled = True
        btncarimasuk.Enabled = True
        btnnext.Enabled = True

        'buat tabel
        Call tabel_utama()

        Call koneksii()
        'bersihkan keranjang belanja
        sql = "DELETE FROM tb_barang_masuk_sementara" 'clear data
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbsupplier.Enabled = False
        btncarisupplier.Enabled = False

        txtalamat.Enabled = False
        txttelp.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False
        txtgudang.Enabled = False

        dtbarangmasuk.Enabled = False
        dtbarangmasuk.Value = Date.Now

        cbprinted.Checked = False
        cbposted.Checked = False

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False

        btncari.Enabled = False

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

        lblsatuan.Text = "satuan"

        btntambahbarang.Enabled = False
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        'total tabel pembelian
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_barang_masuk WHERE kode_barang_masuk = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomornota = dr("kode_barang_masuk")
                    nomorsupplier = dr("kode_supplier")
                    nomorsales = dr("kode_user")
                    nomorgudang = dr("kode_gudang")
                    statusprint = dr("print_barang_masuk")
                    statusposted = dr("posted_barang_masuk")
                    viewtglbarangmasuk = dr("tgl_barang_masuk")
                    viewketerangan = dr("keterangan_barang_masuk")

                    txtnonota.Text = nomornota
                    cmbsupplier.Text = nomorsupplier
                    cmbsales.Text = nomorsales
                    cmbgudang.Text = nomorgudang
                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    dtbarangmasuk.Value = viewtglbarangmasuk

                    'isi tabel view pembelian

                    Call previewpembelian(nomorkode)

                    'total tabel pembelian

                    txtketerangan.Text = viewketerangan

                    cnn.Close()
                End If
            End Using
        Else
            txtnonota.Clear()
            cmbsupplier.Text = ""

            txtalamat.Clear()
            txttelp.Clear()

            cmbsales.Text = ""
            cmbgudang.Text = ""
            cbprinted.Checked = False
            cbposted.Checked = False

            dtbarangmasuk.Value = Date.Now

            txtketerangan.Text = ""

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
        txtgobarangmasuk.Enabled = False
        btncarimasuk.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.SelectedIndex = 0
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True

        txtalamat.Enabled = True
        txttelp.Enabled = True

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = 0
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtbarangmasuk.Enabled = True
        dtbarangmasuk.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncari.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        lblsatuan.Text = "satuan"

        btntambahbarang.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        'buat tabel
        Call tabel_utama()

        'bersihkan keranjang belanja
        sql = "DELETE FROM tb_barang_masuk_sementara" 'clear data
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        statusedit = False
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
        txtgobarangmasuk.Enabled = False
        btncarimasuk.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True

        cmbsales.Enabled = True

        txtalamat.Enabled = True
        txttelp.Enabled = True

        cmbgudang.Enabled = True
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        dtbarangmasuk.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncari.Enabled = True

        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        lblsatuan.Text = "satuan"

        btntambahbarang.Enabled = True

        'validasi Grid
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        txtketerangan.Enabled = True

        Call koneksii()
        sql = "DELETE FROM tb_barang_masuk_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "INSERT INTO tb_barang_masuk_detail_sementara SELECT * FROM tb_barang_masuk_detail WHERE kode_barang_masuk ='" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

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

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        'bersihkan textbox
        lblsatuan.Text = "satuan"
        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyakbarang.Clear()
        txtnamabarang.Enabled = False
        txtkodebarang.Focus()
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
            jenis = dr("jenis_barang")
        Else
            txtnamabarang.Text = ""
            lblsatuan.Text = "satuan"
        End If
    End Sub

    Private Sub fbarangmasuk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
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

    Sub carisupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier = '" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtalamat.Text = dr("alamat_supplier")
            txttelp.Text = dr("telepon_supplier")
        Else
            txtalamat.Text = ""
            txttelp.Text = ""
        End If
    End Sub

    Private Sub riteqty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles riteqty.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub btncarisupplier_Click(sender As Object, e As EventArgs) Handles btncarisupplier.Click
        tutupsup = 2
        fcarisupp.ShowDialog()
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 4
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        tutup = 3
        fcaribarang.ShowDialog()
    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsupplier.SelectedIndexChanged
        Call carisupplier()
    End Sub

    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged
        Call carisupplier()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        Call carigudang()
    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged
        Call caribarang()
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub simpan()
        kodebarangmasuk = autonumber()
        Call koneksii()
        'total1 = GridView1.Columns("subtotal").SummaryItem.SummaryValue 'ambil isi summary gridview
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
                Call koneksii()
                For i As Integer = 0 To GridView1.RowCount - 1
                    sql = "INSERT INTO tb_barang_masuk_detail (kode_barang_masuk, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty,created_by, updated_by,date_created, last_updated) VALUES ('" & kodebarangmasuk & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                Next
                sql = "INSERT INTO tb_barang_masuk (kode_barang_masuk, kode_supplier, kode_gudang, kode_user, tgl_barang_masuk, print_barang_masuk, posted_barang_masuk, keterangan_barang_masuk, created_by, updated_by,date_created, last_updated) VALUES ('" & kodebarangmasuk & "','" & cmbsupplier.Text & "','" & cmbgudang.Text & "','" & cmbsales.Text & "','" & Format(dtbarangmasuk.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
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
                            sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbgudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "', now(), now() )"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()
                        End If

                    Else
                        sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbgudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "', now(), now() )"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If
                Next

                'history user ==========
                Call historysave("Menyimpan Data Barang Masuk Kode " + kodebarangmasuk, kodebarangmasuk)
                '========================

                MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
                Call inisialisasi(kodebarangmasuk)
            End If
        End If
    End Sub
    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView1.DataRowCount > 0 Then
            If cmbsupplier.Text IsNot "" Then
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

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            Call cetak_faktur()
            Call koneksii()
            sql = "UPDATE tb_barang_masuk SET print_barang_masuk = 1 WHERE kode_barang_masuk = '" & txtnonota.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'history user ==========
            Call historysave("Mencetak Data Barang Masuk Kode " + txtnonota.Text, txtnonota.Text)
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
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturbarangmasuk
        rpt_faktur.SetDataSource(tabel_faktur)
        'rpt.SetParameterValue("total", total2)
        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        rpt_faktur.SetParameterValue("tanggal", dtbarangmasuk.Text)
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
    Sub perbarui(nomornota As String)
        Call koneksii()
        Dim kodegudangupdate As String

        'cari nota  yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT kode_gudang FROM tb_barang_masuk WHERE kode_barang_masuk = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodegudangupdate = dr("kode_gudang")

        'update data yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT * FROM tb_barang_masuk_detail_sementara WHERE kode_barang_masuk = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        While dr.Read
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & dr("qty") & "' WHERE kode_stok = '" & dr("kode_stok") & "' AND kode_gudang='" & kodegudangupdate & "'"
            cmmd = New OdbcCommand(sql, cnn)
            drpembelian = cmmd.ExecuteReader()
        End While

        'hapus tb_pembelian_detail
        sql = "DELETE FROM tb_barang_masuk_detail WHERE kode_barang_masuk = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        '========================================================================================

        'loop isi pembelian detail

        If GridView1.RowCount = 0 Then
            MsgBox("Data masih kosong")
            Exit Sub
        Else
            For i As Integer = 0 To GridView1.RowCount - 1
                sql = "INSERT INTO tb_barang_masuk_detail (kode_barang_masuk, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, created_by, updated_by, date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
            Next
            sql = "UPDATE tb_barang_masuk SET kode_supplier = '" & cmbsupplier.Text & "', kode_gudang = '" & cmbgudang.Text & "', kode_user = '" & cmbsales.Text & "', tgl_barang_masuk = '" & Format(dtbarangmasuk.Value, "yyyy-MM-dd HH:mm:ss") & "', print_barang_masuk = 0, posted_barang_masuk = 1, keterangan_barang_masuk = '" & txtketerangan.Text & "', updated_by = '" & fmenu.statususer.Text & "', last_updated = now() WHERE kode_barang_masuk = '" & nomornota & "'"
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
                        sql = "INSERT INTO tb_stok ( kode_stok, nama_stok, status_stok, jumlah_stok, kode_barang, kode_gudang, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & cmbgudang.Text & "', '" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now() )"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                    End If

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

                End If
            Next

        End If

        'history user ==========
        Call historysave("Mengedit Data Barang Masuk Kode " + nomornota, nomornota)
        '========================

        MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
        Call inisialisasi(nomornota)
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text = "Edit" Then
                btnedit.Text = "Update"
                Call awaledit()
            Else
                If btnedit.Text = "Update" Then
                    If GridView1.DataRowCount > 0 Then
                        If cmbsupplier.Text IsNot "" Then
                            If txtgudang.Text IsNot "" Then
                                If cmbsales.Text IsNot "" Then
                                    btnedit.Text = "Edit"
                                    'isi disini sub updatenya
                                    Call perbarui(txtnonota.Text)
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
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(kodebarangmasuk)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgobarangmasuk.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT kode_barang_masuk FROM tb_barang_masuk WHERE kode_barang_masuk  = '" + txtgobarangmasuk.Text + "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgobarangmasuk.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
    End Sub

    Private Sub btncarimasuk_Click(sender As Object, e As EventArgs) Handles btncarimasuk.Click
        tutupbarangmasuk = 1
        fcaribarangmasuk.ShowDialog()
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
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

    Private Sub txtbanyakbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyakbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub tambah()
        Dim kode_stok, counter_angka As String
        Dim total_karakter, total_karakter_kode, tambah_counter As Integer

        Call koneksii()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtbanyakbarang.Text = "" Then
            Exit Sub
        End If

        If GridView1.RowCount = 0 Then  'data tidak ada
            If lblsatuan.Text = "Pcs" Then
                'tambahkan data ke tabel keranjang
                tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)
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
                    tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)

                    Call koneksii()
                    sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang, qty, satuan_barang, jenis_barang, nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "','1')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    Call reload_tabel()
                Else 'kalau belum ada stok
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text + "1", txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)

                    'simpan kedalam tabel pembelian sementara agar kode dapat dilanjutkan
                    Call koneksii()
                    sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + "1" & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "','1')"
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
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)
                        Call reload_tabel()
                    Else
                        qty1 = GridView1.GetRowCellValue(lokasi, "qty")
                        GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak + qty1), satuan, jenis)
                        Call reload_tabel()

                    End If
                Else
                    'MsgBox("bukan Pcs")
                    Call koneksii()
                    sql = "SELECT * FROM tb_barang_masuk_sementara WHERE kode_barang= '" & txtkodebarang.Text & "' ORDER BY nomor DESC LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        'ada data
                        kode_stok = dr("kode_stok")
                        total_karakter = Len(dr("kode_stok"))
                        total_karakter_kode = Len(txtkodebarang.Text)
                        counter_angka = CInt(Microsoft.VisualBasic.Right(kode_stok, total_karakter - total_karakter_kode))
                        tambah_counter = counter_angka + 1
                        tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)


                        sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang, qty, satuan_barang, jenis_barang, nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & " ', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & tambah_counter & "')"
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
                            tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)

                            Call koneksii()
                            sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "','1')"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            Call reload_tabel()
                        Else
                            tabel.Rows.Add(txtkodebarang.Text + "1", txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis)

                            sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + "1" & "', '" & txtkodebarang.Text & " ', '" & txtnamabarang.Text & "','" & Val(banyak) & "','" & satuan & "','" & jenis & "', '" & tambah_counter & "')"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            Call reload_tabel()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Dim hapuskode As String
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            hapuskode = GridView1.GetFocusedRowCellValue("kode_stok")
            sql = "DELETE FROM tb_barang_masuk_sementara WHERE  kode_stok='" & hapuskode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            GridView1.DeleteSelectedRows()
        End If
    End Sub
End Class