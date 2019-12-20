Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class flunaspiutang
    Public tabel1, tabel2 As DataTable
    Public kodelunaspiutang As String
    Dim totalbayar As Double


    Private Sub flunaspiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        kodelunaspiutang = currentnumber()
        Call loadingpenjualan()
        Call inisialisasi(kodelunaspiutang)

        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("bayar_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_kas", "{0:n0}")
        End With
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
            cnn.Close()
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
            Else
                Call inisialisasi(previousnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
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
            Else
                Call inisialisasi(nextingnumber)
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
    End Sub

    Sub caripelanggan()
        Dim kodepelangganfokus As String
        kodepelangganfokus = GridView1.GetFocusedRowCellValue("kode_customer")

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
        txtnolunaspiutang.Clear()
        txtnolunaspiutang.Text = autonumber()
        txtnolunaspiutang.Enabled = False

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
        txtnolunaspiutang.Clear()
        txtnolunaspiutang.Text = autonumber()
        txtnolunaspiutang.Enabled = False

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
            Call caripiutang(nomorkode)
        Else
            cbvoid.Checked = False
            cbprinted.Checked = False
            cbposted.Checked = False

            txtnolunaspiutang.Clear()
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
        txtnolunaspiutang.Enabled = False
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
            .Columns.Add("kode_penjualan")
            .Columns.Add("kode_customer")
            .Columns.Add("kode_gudang")
            .Columns.Add("kode_user")
            .Columns.Add("tgl_penjualan")
            .Columns.Add("tgl_jatuhtempo_penjualan")
            .Columns.Add("diskon_penjualan", GetType(Double))
            .Columns.Add("pajak_penjualan", GetType(Double))
            .Columns.Add("ongkir_penjualan", GetType(Double))
            .Columns.Add("total_penjualan", GetType(Double))
        End With

        GridControl1.DataSource = tabel1

        GridColumn1.FieldName = "kode_penjualan"
        GridColumn1.Caption = "Kode Penjualan"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "kode_customer"
        GridColumn2.Caption = "Kode Customer"
        GridColumn2.Width = 20

        GridColumn3.FieldName = "kode_gudang"
        GridColumn3.Caption = "Kode Gudang"
        GridColumn3.Width = 20

        GridColumn4.FieldName = "kode_user"
        GridColumn4.Caption = "Kode User"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "tgl_penjualan"
        GridColumn5.Caption = "Tgl Penjualan"
        GridColumn5.Width = 30

        GridColumn6.FieldName = "tgl_jatuhtempo_penjualan"
        GridColumn6.Caption = "Tgl Jatuh Tempo"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "diskon_penjualan"
        GridColumn7.Caption = "Diskon"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 15

        GridColumn8.FieldName = "pajak_penjualan"
        GridColumn8.Caption = "Pajak"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 15

        GridColumn9.FieldName = "ongkir_penjualan"
        GridColumn9.Caption = "Ongkir"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "total_penjualan"
        GridColumn10.Caption = "Total"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

    End Sub
    Sub tabel_lunas()
        tabel2 = New DataTable

        With tabel2
            .Columns.Add("kode_lunas")
            .Columns.Add("kode_penjualan")
            .Columns.Add("tanggal_transaksi")
            .Columns.Add("kode_user")
            .Columns.Add("kode_kas")
            .Columns.Add("bayar_kas", GetType(Double))

        End With

        GridControl2.DataSource = tabel2

        GridColumn11.FieldName = "kode_lunas"
        GridColumn11.Caption = "Kode Lunas"
        GridColumn11.Width = 20

        GridColumn12.FieldName = "kode_penjualan"
        GridColumn12.Caption = "Kode Penjualan"
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
        txtcustomer.Clear()
    End Sub
    Sub loadingpenjualan()
        Call tabel_utama()
        Call tabel_lunas()
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_penjualan"), dr("kode_pelanggan"), dr("kode_gudang"), dr("kode_user"), dr("tgl_penjualan"), dr("tgl_jatuhtempo_penjualan"), Val(dr("diskon_penjualan")), Val(dr("pajak_penjualan")), Val(dr("ongkir_penjualan")), Val(dr("total_penjualan")))
            GridControl1.RefreshDataSource()
        End While
    End Sub

    Sub loadinglunas(lihat As String)
        Call tabel_lunas()
        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang WHERE kode_penjualan='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel2.Rows.Add(dr("kode_lunas"), dr("kode_penjualan"), dr("tanggal_transaksi"), dr("kode_user"), dr("kode_kas"), Val(dr("bayar_lunas")))
            GridControl2.RefreshDataSource()
        End While
    End Sub

    Sub carijual()
        Dim kodepenjualanfokus As String
        kodepenjualanfokus = GridView1.GetFocusedRowCellValue("kode_penjualan")
        txtnonota.Text = kodepenjualanfokus
        Call loadinglunas(kodepenjualanfokus)
    End Sub

    Sub carilunas()
        Dim kodelunasfokus As String
        kodelunasfokus = GridView1.GetFocusedRowCellValue("kode_lunas")
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
    End Sub
    Sub prosessimpan()
        Dim checkinglunas As Boolean
        Dim sisajual, bayarjual As Double

        'cek ke penjualan
        Call koneksii()
        sql = "SELECT sisa_penjualan FROM tb_penjualan WHERE kode_penjualan = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            sisajual = dr("sisa_penjualan")
        Else
            MsgBox("Penjualan tidak ditemukan !")
        End If

        'cek ke transaksi kas
        Call koneksii()
        sql = "SELECT IFNULL(SUM(kredit_kas), 0) As total_kas FROM tb_transaksi_kas WHERE kode_penjualan = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            bayarjual = Val(dr("total_kas"))
        Else
            bayarjual = 0
        End If

        'hitung pembayaran
        If (sisajual - bayarjual) < totalbayar Then
            checkinglunas = False
        Else
            checkinglunas = True
        End If


        If checkinglunas = True Then
            Call simpan()
        Else
            MsgBox("Total lebih Bayar")
        End If

    End Sub
    Sub simpan()
        kodelunaspiutang = autonumber()
        Call koneksii()

        sql = "INSERT INTO tb_pelunasan_piutang (kode_lunas, kode_penjualan, tanggal_transaksi, kode_user, kode_kas, jenis_kas, bayar_lunas, keterangan_lunas, void_lunas, print_lunas, posted_lunas, created_by, updated_by, date_created, last_updated) VALUES ('" & kodelunaspiutang & "', '" & txtnonota.Text & "', '" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', '" & cmbsales.Text & "', '" & cmbbayar.Text & "', '" & cmbbayar.Text & "', '" & totalbayar & "','" & txtketerangan.Text & "','" & 0 & "','" & 0 & "','" & 1 & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()


        sql = "INSERT INTO tb_transaksi_kas (kode_kas, kode_penjualan, kode_piutang, jenis_kas, tanggal_transaksi, keterangan_kas, debet_kas, kredit_kas, created_by, updated_by, date_created, last_updated) VALUES ('" & cmbbayar.Text & "','" & txtnonota.Text & "','" & txtnolunaspiutang.Text & "', 'BAYAR', now(), 'Transaksi Nota Nomor " & txtnonota.Text & "','" & 0 & "', '" & totalbayar & "', '" & fmenu.statususer.Text & "', '" & fmenu.statususer.Text & "', now(), now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        Me.Refresh()

        kodelunaspiutang = txtnolunaspiutang.Text
        Call inisialisasi(kodelunaspiutang)
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
            MsgBox("Isi Nota Penjualan")
        End If

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Sub prosesperbarui()
        Dim checkinglunas As Boolean
        Dim nilaihitung As String
        Dim sisajual, bayarjual As Double

        'cek ke penjualan
        Call koneksii()
        sql = "SELECT sisa_penjualan FROM tb_penjualan WHERE kode_penjualan = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            sisajual = dr("sisa_penjualan")
        Else
            MsgBox("Penjualan tidak ditemukan !")
        End If

        'cek ke transaksi kas
        Call koneksii()
        sql = "SELECT IFNULL(SUM(kredit_kas), 0) As total_kas FROM tb_transaksi_kas WHERE kode_penjualan = '" & txtnonota.Text & "' AND NOT kode_piutang ='" & txtnolunaspiutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            bayarjual = Val(dr("total_kas"))
        Else
            bayarjual = 0
        End If

        'hitung pembayaran
        If (sisajual - bayarjual) < totalbayar Then
            checkinglunas = False
        Else
            checkinglunas = True
        End If


        If checkinglunas = True Then
            Call perbarui()
        Else
            MsgBox("Total lebih Bayar")
        End If
    End Sub

    Sub perbarui()
        Call koneksii()

        sql = "UPDATE tb_pelunasan_piutang SET  kode_penjualan='" & txtnonota.Text & "', kode_user='" & cmbsales.Text & "', kode_kas='" & cmbbayar.Text & "', bayar_lunas='" & totalbayar & "', keterangan_lunas='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now()  WHERE  kode_lunas='" & txtnolunaspiutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        sql = "UPDATE tb_transaksi_kas SET kode_kas='" & cmbbayar.Text & "', kode_penjualan='" & txtnonota.Text & "', kode_piutang='" & txtnolunaspiutang.Text & "', tanggal_transaksi='" & Format(dtpelunasan.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_kas='" & txtketerangan.Text & "', kredit_kas='" & totalbayar & "', updated_by='" & fmenu.statususer.Text & "', last_updated=now() WHERE kode_piutang='" & txtnolunaspiutang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"

        Call inisialisasi(kodelunaspiutang)
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
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
                MsgBox("Isi Nota Penjualan")
            End If
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
        Call prevnumber(kodelunaspiutang)
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

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(kodelunaspiutang)
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call caripelanggan()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Call carijual()
    End Sub

    Sub caripiutang(nopiutang As String)
        sql = "SELECT * FROM tb_pelunasan_piutang WHERE kode_lunas  = '" + nopiutang + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            txtnolunaspiutang.Text = dr("kode_lunas")
            txtnonota.Text = dr("kode_penjualan")
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

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        If btnsimpan.Enabled = False Then
            Call caripiutang(GridView2.GetFocusedRowCellValue("kode_lunas"))
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