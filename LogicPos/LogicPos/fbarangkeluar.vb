Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fbarangkeluar
    Public tabel As DataTable
    'variabel dalam penjualan
    Public jenis, satuan, kodebarangkeluar, kodetransaksi As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double

    'variabel bantuan view penjualan
    Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran, kodepembayaran As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    'variabel edit penjualan
    Dim countingbarang As Integer

    Private Sub fbarangkeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        kodebarangkeluar = currentnumber()
        Call inisialisasi(kodebarangkeluar)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
        End With
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
            cnn.Close()
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
            cnn.Close()
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
        sql = "SELECT kode_barang_keluar FROM tb_barang_keluar WHERE date_created > (SELECT date_created FROM tb_barang_keluar WHERE kode_barang_keluar = '" + nextingnumber + "')ORDER BY date_created ASC LIMIT 1"
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

    Sub previewbarangkeluar(lihat As String)
        sql = "SELECT * FROM tb_barang_keluar_detail WHERE kode_barang_keluar ='" & lihat & "'"
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
        txtgopenjualan.Enabled = False
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
        txtgopenjualan.Enabled = False
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
        sql = "INSERT INTO tb_barang_keluar_sementara SELECT * FROM tb_barang_keluar_detail WHERE kode_barang_keluar ='" & txtnonota.Text & "'"
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
        txtgopenjualan.Enabled = True
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
                    nomornota = dr("kode_penjualan")
                    nomorcustomer = dr("kode_pelanggan")
                    nomorsales = dr("kode_user")
                    nomorgudang = dr("kode_gudang")

                    statusprint = dr("print_penjualan")
                    statusposted = dr("posted_penjualan")

                    viewtglpenjualan = dr("tgl_penjualan")
                    viewketerangan = dr("keterangan_penjualan")

                    txtnonota.Text = nomornota
                    cmbcustomer.Text = nomorcustomer
                    cmbsales.Text = nomorsales
                    cmbgudang.Text = nomorgudang

                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    dtbarangkeluar.Value = viewtglpenjualan

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
        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan ='" & cmbcustomer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamabarang.Text = dr("nama_barang")
            txtkodebarang.Text = dr("kode_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            jenis = dr("jenis_barang")
        Else
            sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok= '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000'"
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

    Private Sub cmbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcustomer.SelectedIndexChanged
        caripelanggan()
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        carigudang()
    End Sub

    Private Sub cmbcustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbcustomer.TextChanged
        caripelanggan()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        carigudang()
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
        tutupstok = 2
        fcaristok.ShowDialog()
    End Sub


    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click

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
            Exit Sub
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan='" & cmbcustomer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                        MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                        Exit Sub
                    Else
                        tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis)
                        Call reload_tabel()
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '00000000'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                        MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Exit Sub
                    Else
                        tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(dr("harga_jual")), "0", "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * banyak, (Val(dr("harga_jual")) * banyak - Val(modalpenjualan) * banyak), modalpenjualan)
                        Call reload_tabel()
                    End If
                End If

            Else 'kalau ada isi
                Dim tbbanyak As Integer = 0
                Dim tbnilaipersen As Double = 0
                Dim tbnilainominal As Double = 0
                Dim lokasi As Integer = -1

                sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan = '" & cmbcustomer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
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
                        If dr("jumlah_stok") < (Val(txtbanyak.Text) + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                            Call reload_tabel()
                        End If
                    Else
                        If dr("jumlah_stok") < (Val(txtbanyak.Text) + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (banyak + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (banyak + tbbanyak) - (Val(modalpenjualan) * (banyak + tbbanyak))), modalpenjualan)
                            Call reload_tabel()
                        End If
                    End If
                Else
                    sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodestok.Text & "' AND tb_price_group.kode_pelanggan='00000000'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If GridView1.GetRowCellValue(i, "kode_stok").Equals(txtkodestok.Text) Then
                            lokasi = i
                        End If
                    Next

                    tbbanyak = GridView1.GetRowCellValue(lokasi, "banyak")
                    tbnilaipersen = GridView1.GetRowCellValue(lokasi, "diskon_persen")
                    tbnilainominal = GridView1.GetRowCellValue(lokasi, "diskon_nominal")

                    If lokasi = -1 Then
                        If dr("jumlah_stok") < (Val(txtbanyak.Text) + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text) + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (Val(txtbanyak.Text) + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (Val(txtbanyak.Text) + tbbanyak) - (Val(modalpenjualan) * Val(txtbanyak.Text) + tbbanyak)), modalpenjualan)
                            Call reload_tabel()
                        End If
                    Else
                        If dr("jumlah_stok") < (Val(txtbanyak.Text) + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(dr("kode_barang"), txtkodestok.Text, txtnamabarang.Text, Val(txtbanyak.Text) + tbbanyak, satuan, jenis, Val(dr("harga_jual")), tbnilaipersen, tbnilainominal, Val(dr("harga_jual")) - tbnilainominal, (Val(dr("harga_jual")) - tbnilainominal) * (Val(txtbanyak.Text) + tbbanyak), (Val(dr("harga_jual") - tbnilainominal) * (Val(txtbanyak.Text) + tbbanyak) - (Val(modalpenjualan) * Val(txtbanyak.Text) + tbbanyak)), modalpenjualan)
                            Call reload_tabel()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click

    End Sub
End Class