Imports System.Data.Odbc

Public Class fbarangmasuk
    Dim tabel As DataTable
    Dim harga, modalpembelian, ongkir, ppn, diskonpersen, diskonnominal, ppnpersen, ppnnominal, total1, total2, grandtotal, banyak As Double
    Dim satuan, jenis, supplier, kodepembelian As String
    Public isi As String
    'variabel bantuan view pembelian
    Dim nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan, viewbayar As String
    Dim statusprint, statusposted, statusedit As Boolean

    Dim viewtglbarangmasuk As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir As Double
    Private Sub fbarangmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'mulai
        kodepembelian = currentnumber()
        Call inisialisasi(kodepembelian)
        With GridView1
            '.OptionsView.ColumnAutoWidth = False ' agar muncul scrol bar
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            '.OptionsView.ShowAutoFilterRow = True 'aktifkan autofilter
            '.OptionsView.EnableAppearanceOddRow = True 'aktifkan style
            '.OptionsPrint.EnableAppearanceOddRow = True 'aktifkan style saat print
        End With
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
            cnn.Close()
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
        sql = "SELECT kode_barang_masuk FROM tb_barang_masuk WHERE date_created > (SELECT date_created FROM tb_barang_masuk WHERE kode_barang_masuk = '" + nextingnumber + "')ORDER BY date_created ASC LIMIT 1"
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

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False
        txtgudang.Enabled = False

        dtbarangmasuk.Enabled = False
        dtbarangmasuk.Value = Date.Now

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
                    nomornota = dr("kode_pembelian")
                    nomorsupplier = dr("kode_supplier")
                    nomorsales = dr("kode_user")
                    nomorgudang = dr("kode_gudang")
                    statusprint = dr("print_pembelian")
                    statusposted = dr("posted_pembelian")
                    viewtglbarangmasuk = dr("tgl_pembelian")
                    viewketerangan = dr("keterangan_pembelian")

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
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.SelectedIndex = 0
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True

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

    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged

    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        Call carigudang()
    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged
        Call caribarang()
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

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

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
                tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis)
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
                    tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis)

                    Call koneksii()
                    sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(txtbanyakbarang.Text) & "','" & satuan & "','" & jenis & "','1')"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    Call reload_tabel()
                Else 'kalau belum ada stok
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text + "1", txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis)

                    'simpan kedalam tabel pembelian sementara agar kode dapat dilanjutkan
                    Call koneksii()
                    sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + "1" & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(txtbanyakbarang.Text) & "','" & satuan & "','" & jenis & "','1')"
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
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis)
                        Call reload_tabel()
                    Else
                        qty1 = GridView1.GetRowCellValue(lokasi, "qty")
                        GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, (banyak + qty1), satuan, jenis)
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
                        tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis)


                        sql = "INSERT INTO tb_barang_masuk_sementara (kode_stok, kode_barang, nama_barang, qty, satuan_barang, jenis_barang, nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & " ', '" & txtnamabarang.Text & "','" & Val(txtbanyakbarang.Text) & "','" & satuan & "','" & jenis & "', '" & tambah_counter & "')"
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
                            tabel.Rows.Add(txtkodebarang.Text + CStr(tambah_counter), txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis)

                            Call koneksii()
                            sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + CStr(tambah_counter) & "', '" & txtkodebarang.Text & "', '" & txtnamabarang.Text & "','" & Val(txtbanyakbarang.Text) & "','" & satuan & "','" & jenis & "','1')"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()

                            Call reload_tabel()
                        Else
                            tabel.Rows.Add(txtkodebarang.Text + "1", txtkodebarang.Text, txtnamabarang.Text, banyak, satuan, jenis, Val(harga), Val(txtbanyakbarang.Text) * Val(harga))

                            sql = "INSERT INTO tb_pembelian_sementara (kode_stok, kode_barang, nama_barang,qty,satuan_barang,jenis_barang,nomor) VALUES ('" & txtkodebarang.Text + "1" & "', '" & txtkodebarang.Text & " ', '" & txtnamabarang.Text & "','" & Val(txtbanyakbarang.Text) & "','" & satuan & "','" & jenis & "', '" & tambah_counter & "')"
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
End Class