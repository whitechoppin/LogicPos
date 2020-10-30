Imports System.Data.Odbc
Imports System.IO
Imports DevExpress.Utils
Imports ZXing

Public Class ftransferbarang
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean

    Public tabel, tabelsementara As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Public jenis, satuan As String
    Dim idbarang, iddaristok, idkestok As Integer
    Dim idtransferbarang, iddarigudang, idkegudang, iduser As String
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
        idtransferbarang = currentnumber()
        Call inisialisasi(idtransferbarang)
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

    'Function autonumber()
    '    Call koneksii()
    '    sql = "SELECT RIGHT(kode_transfer_barang,3) FROM tb_transfer_barang WHERE DATE_FORMAT(MID(`kode_transfer_barang`, 3 , 6), ' %y ')+ MONTH(MID(`kode_transfer_barang`,3 , 6)) + DAY(MID(`kode_transfer_barang`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_transfer_barang,3) DESC"
    '    Dim pesan As String = ""
    '    Try
    '        cmmd = New OdbcCommand(sql, cnn)
    '        dr = cmmd.ExecuteReader
    '        If dr.HasRows Then
    '            dr.Read()
    '            If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
    '                Return "TB" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '            Else
    '                If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
    '                    Return "TB" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '                Else
    '                    If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
    '                        Return "TB" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Return "TB" + Format(Now.Date, "yyMMdd") + "001"
    '        End If

    '    Catch ex As Exception
    '        pesan = ex.Message.ToString
    '    Finally
    '        'cnn.Close()
    '    End Try
    '    Return pesan
    'End Function

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_transfer_barang ORDER BY id DESC LIMIT 1;"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Return dr.Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        End Try
        Return pesan
    End Function

    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT id FROM tb_transfer_barang WHERE date_created < (SELECT date_created FROM tb_transfer_barang WHERE id = '" + previousnumber + "') ORDER BY date_created DESC LIMIT 1"
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
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT id FROM tb_transfer_barang WHERE date_created > (SELECT date_created FROM tb_transfer_barang WHERE id = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
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
        End Try
    End Sub

    Private Sub txtbanyak_TextChanged(sender As Object, e As EventArgs) Handles txtbanyak.TextChanged
        If txtbanyak.Text = "" Or txtbanyak.Text = "0" Then
            txtbanyak.Text = 1
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
        sql = "SELECT * FROM tb_transfer_barang_detail WHERE transfer_barang_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("dari_stok_id"), dr("ke_stok_id"))
            tabelsementara.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("dari_stok_id"), dr("ke_stok_id"))
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
            .Columns.Add("barang_id")
            .Columns.Add("dari_stok_id")
            .Columns.Add("ke_stok_id")
        End With

        tabelsementara = New DataTable
        With tabelsementara
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("barang_id")
            .Columns.Add("dari_stok_id")
            .Columns.Add("ke_stok_id")
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 10

        GridColumn2.FieldName = "kode_stok"
        GridColumn2.Caption = "Kode Stok"
        GridColumn2.Width = 10

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 35

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

        GridColumn7.FieldName = "barang_id"
        GridColumn7.Caption = "id barang"
        GridColumn7.Width = 10

        GridColumn8.FieldName = "dari_stok_id"
        GridColumn8.Caption = "id dari stok"
        GridColumn8.Width = 10

        GridColumn9.FieldName = "ke_stok_id"
        GridColumn9.Caption = "id ke stok"
        GridColumn9.Width = 10

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
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbkegudang.DataSource = ds.Tables(0)
        cmbkegudang.ValueMember = "id"
        cmbkegudang.DisplayMember = "kode_gudang"
    End Sub

    Sub comboboxdarigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbdarigudang.DataSource = ds.Tables(0)
        cmbdarigudang.ValueMember = "id"
        cmbdarigudang.DisplayMember = "kode_gudang"
    End Sub
    Sub comboboxuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsales.DataSource = ds.Tables(0)
        cmbsales.ValueMember = "id"
        cmbsales.DisplayMember = "kode_user"
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
        btncaritransfer.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxuser()
        Call comboboxdarigudang()
        Call comboboxkegudang()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbdarigudang.Enabled = True
        cmbdarigudang.SelectedIndex = -1
        btncaridarigudang.Enabled = True
        txtdarigudang.Enabled = False

        cmbkegudang.Enabled = True
        cmbkegudang.SelectedIndex = -1
        btncarikegudang.Enabled = True
        txtkegudang.Enabled = False

        cbprinted.Checked = False
        cbposted.Checked = False

        dttransferbarang.Enabled = True
        dttransferbarang.Value = Date.Now


        'body
        txtkodestok.Clear()
        txtkodestok.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyak.Clear()
        txtbanyak.Text = 1
        txtbanyak.Enabled = True

        lblsatuan.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()


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
        btncaritransfer.Enabled = False
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
        txtbanyak.Text = 1
        txtbanyak.Enabled = True

        lblsatuan.Text = "satuan"

        btntambah.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True

    End Sub

    Sub inisialisasi(nomorkode As Integer)

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
        btncaritransfer.Enabled = True
        btnnext.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxdarigudang()
        Call comboboxkegudang()

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
        txtbanyak.Text = 1
        txtbanyak.Enabled = False

        btntambah.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode > 0 Then
            Call koneksii()
            sql = "SELECT * FROM tb_transfer_barang WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorsales = dr("user_id")
                nomordarigudang = dr("dari_gudang_id")
                nomorkegudang = dr("ke_gudang_id")

                statusprint = dr("print_transfer_barang")
                statusposted = dr("posted_transfer_barang")

                viewtgltransfer = dr("tanggal_transfer_barang")
                viewketerangan = dr("keterangan_transfer_barang")

                txtnonota.Text = nomornota
                cmbsales.SelectedValue = nomorsales
                cmbdarigudang.SelectedValue = nomordarigudang
                cmbkegudang.SelectedValue = nomorkegudang

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dttransferbarang.Value = viewtgltransfer

                'isi tabel view pembelian

                Call previewtransferbarang(nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan

            End If
        Else
            txtnonota.Clear()
            cmbsales.SelectedIndex = -1

            cmbdarigudang.SelectedIndex = -1
            cmbkegudang.SelectedIndex = -1

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
            iddarigudang = dr("id")
            txtdarigudang.Text = dr("nama_gudang")
        Else
            txtdarigudang.Text = ""
        End If
    End Sub

    Private Sub ftransferbarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call carisales()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call carisales()
    End Sub

    Sub carikegudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbkegudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idkegudang = dr("id")
            txtkegudang.Text = dr("nama_gudang")
        Else
            txtkegudang.Text = ""
        End If
    End Sub

    Sub carisales()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = dr("id")
        End If
    End Sub

    Private Sub ritebanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritebanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub caristok()
        Call koneksii()
        sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang FROM tb_stok JOIN tb_barang ON tb_barang.id = tb_stok.barang_id WHERE kode_stok = '" & txtkodestok.Text & "' AND gudang_id ='" & iddarigudang & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iddaristok = Val(dr("idstok"))
            idbarang = Val(dr("idbarang"))

            txtkodebarang.Text = dr("kode_barang")
            txtnamabarang.Text = dr("nama_barang")
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

                                If txtkegudang.Text.Equals(txtdarigudang.Text) Then
                                    MsgBox("Gudang Tidak Boleh Sama")
                                Else
                                    Call perbarui(txtnonota.Text)
                                End If

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
            Call inisialisasi(idtransferbarang)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub


    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then

            If cekcetakan(txtnonota.Text).Equals(True) Then
                statusizincetak = False
                passwordid = 12
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_transfer_barang SET print_transfer_barang = 1 WHERE id = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Transfer Barang Kode " + txtnonota.Text, txtnonota.Text)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_transfer_barang SET print_transfer_barang = 1 WHERE id = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Transfer Barang Kode " & txtnonota.Text, txtnonota.Text)
                '========================

                cbprinted.Checked = True
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub cetak_faktur()
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
        baris_barcode("kode_barcode") = txtnonota.Text

        writer.Options.Height = 200
        writer.Options.Width = 200
        writer.Format = BarcodeFormat.QR_CODE

        barcode = writer.Write(txtnonota.Text)
        barcode.Save(ms, Imaging.ImageFormat.Bmp)
        ms.ToArray()

        baris_barcode("gambar_barcode") = ms.ToArray
        tabel_barcode.Rows.Add(baris_barcode)
        '====================

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
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        rpt_faktur.SetParameterValue("tanggal", Format(dttransferbarang.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("penerima", fmenu.kodeuser.Text)
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
                        If txtkegudang.Text.Equals(txtdarigudang.Text) Then
                            MsgBox("Gudang Tidak Boleh Sama")
                        Else
                            Call proses()
                        End If
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
            sql = "SELECT id FROM tb_transfer_barang WHERE id= '" & txtgotransferbarang.Text & "' LIMIT 1"
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
        tutupcaritransferbarang = 1
        fcaritransferbarang.ShowDialog()
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
        tutupcaristok = 3
        idgudangcari = iddarigudang
        fcaristok.ShowDialog()
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        Call caristok()
    End Sub

    Private Sub txtkodestok_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodestok.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
        End If
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
    End Sub

    Sub tambah()
        Dim tbbanyak As Integer = 0
        Dim lokasi As Integer = -1
        'Columns.Add("kode_barang")
        'Columns.Add("kode_stok")
        'Columns.Add("nama_barang")
        'Columns.Add("banyak", GetType(Double))
        'Columns.Add("satuan_barang")
        'Columns.Add("jenis_barang")
        'Columns.Add("barang_id")
        'Columns.Add("dari_stok_id")
        'Columns.Add("ke_stok_id")

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtbanyak.Text = "" Or banyak = 0 Then
            MsgBox("Barang Kosong", MsgBoxStyle.Information, "Informasi")
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "SELECT * FROM tb_stok WHERE id = '" & iddaristok & "' AND gudang_id ='" & iddarigudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    If dr("jumlah_stok") < banyak Then
                        MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                    Else
                        tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, idbarang, iddaristok, 0)
                        Call reload_tabel()
                    End If
                End If
            Else 'kalau ada isi
                sql = "SELECT * FROM tb_stok WHERE id = '" & iddaristok & "' AND gudang_id ='" & iddarigudang & "' LIMIT 1"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If GridView1.GetRowCellValue(i, "dari_stok_id").Equals(iddaristok) Then
                            lokasi = i
                        End If
                    Next

                    tbbanyak = GridView1.GetRowCellValue(lokasi, "banyak")

                    If lokasi = -1 Then
                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, iddaristok, 0)
                            Call reload_tabel()
                        End If
                    Else
                        If dr("jumlah_stok") < (banyak + tbbanyak) Then
                            MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, iddaristok, 0)
                            Call reload_tabel()
                        End If
                    End If
                Else
                    MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                End If
            End If
        End If
    End Sub

    Sub proses()
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim statusavailable As Boolean = True

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "dari_stok_id") & "' AND gudang_id ='" & iddarigudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                If stokdatabase < stok Then
                    MsgBox("Stok dengan kode stok " + dr("kode_stok") + " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " + GridView1.GetRowCellValue(i, "kode_stok") + " tidak ada di gudang.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        If statusavailable = True Then
            Call simpan()
        End If
    End Sub

    Sub simpan()
        Call koneksii()

        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            'sediakan wadah stok nya dulu
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "jenis_barang").Equals("Satuan") Then
                    Call koneksii()
                    sql = "SELECT * FROM tb_stok WHERE barang_id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' AND gudang_id='" & idkegudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        idkestok = Val(dr("id"))
                        GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                    Else
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idkegudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idkestok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                    End If
                Else
                    Call koneksii()
                    sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idkegudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                    cmmd = New OdbcCommand(sql, cnn)
                    idkestok = CInt(cmmd.ExecuteScalar())
                    GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                End If
            Next

            sql = "INSERT INTO tb_transfer_barang(dari_gudang_id, ke_gudang_id, user_id, tanggal_transfer_barang, print_transfer_barang, posted_transfer_barang, keterangan_transfer_barang, created_by, updated_by, date_created, last_updated) VALUES ('" & iddarigudang & "','" & idkegudang & "','" & iduser & "' , '" & Format(dttransferbarang.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idtransferbarang = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & Val(GridView1.GetRowCellValue(i, "banyak")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "ke_stok_id") & "' AND gudang_id ='" & idkegudang & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & Val(GridView1.GetRowCellValue(i, "banyak")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "dari_stok_id") & "' AND gudang_id ='" & iddarigudang & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "INSERT INTO tb_transfer_barang_detail(transfer_barang_id, barang_id, dari_stok_id, ke_stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by,date_created, last_updated) VALUES ('" & idtransferbarang & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "dari_stok_id") & "','" & GridView1.GetRowCellValue(i, "ke_stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next


            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Transfer Barang Kode " + idtransferbarang, idtransferbarang)
            '========================
            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(idtransferbarang)
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
        'periksa di barang di stok dulu
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim stokdatabasesementara As Integer

        Dim kodestokdatabase As String
        Dim statusavailable As Boolean = True
        Dim kodedarigudanglama, kodekegudanglama As Integer

        'variabel transactional
        '=======================
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction
        '=======================

        'periksa stok
        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id='" & GridView1.GetRowCellValue(i, "dari_stok_id") & "' AND gudang_id ='" & iddarigudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "banyak")
                stokdatabase = dr("jumlah_stok")
                kodestokdatabase = dr("kode_stok")

                'mengambil selisih qty dari transfer barang detail
                sql = "SELECT * FROM tb_transfer_barang_detail WHERE dari_stok_id = '" & GridView1.GetRowCellValue(i, "dari_stok_id") & "' AND transfer_barang_id ='" & nomornota & "' LIMIT 1"
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
                    MsgBox("Stok dengan kode stok " & kodestokdatabase & " tidak mencukupi.", MsgBoxStyle.Information, "Information")
                    statusavailable = False
                End If
            Else
                MsgBox("Kode Stok Barang ini " & GridView1.GetRowCellValue(i, "kode_stok") & " tidak ada.", MsgBoxStyle.Information, "Informasi")
                statusavailable = False
            End If
        Next

        'cari nota  yang sebelumnya (kembalikan stok dulu)
        sql = "SELECT dari_gudang_id, ke_gudang_id FROM tb_transfer_barang WHERE id = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()
        kodedarigudanglama = Val(dr("dari_gudang_id"))
        kodekegudanglama = Val(dr("ke_gudang_id"))

        If statusavailable = True Then
            ' Start a local transaction
            myTrans = cnnx.BeginTransaction()
            myCommand.Connection = cnnx
            myCommand.Transaction = myTrans

            Try
                'sediakan wadah stok nya dulu
                For i As Integer = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "jenis_barang").Equals("Satuan") Then
                        Call koneksii()
                        sql = "SELECT * FROM tb_stok WHERE barang_id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' AND gudang_id='" & idkegudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                        If dr.HasRows Then
                            idkestok = Val(dr("id"))
                            GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                        Else
                            Call koneksii()
                            sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idkegudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                            cmmd = New OdbcCommand(sql, cnn)
                            idkestok = CInt(cmmd.ExecuteScalar())
                            GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                        End If
                    Else
                        'update di wadah yang sama
                        If GridView1.GetRowCellValue(i, "stok_id") = 0 Then
                            Call koneksii()
                            sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idkegudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                            cmmd = New OdbcCommand(sql, cnn)
                            idkestok = CInt(cmmd.ExecuteScalar())
                            GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                        Else
                            Call koneksii()
                            sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "ke_stok_id") & "' AND gudang_id='" & idkegudang & "' LIMIT 1"
                            cmmd = New OdbcCommand(sql, cnn)
                            dr = cmmd.ExecuteReader()
                            If dr.HasRows Then
                                idkestok = Val(dr("id"))
                                GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                            Else
                                Call koneksii()
                                sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idkegudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                                cmmd = New OdbcCommand(sql, cnn)
                                idkestok = CInt(cmmd.ExecuteScalar())
                                GridView1.SetRowCellValue(i, "ke_stok_id", idkestok)
                            End If
                        End If
                    End If
                Next

                'update kembali dari transaksi sebelumnya
                Call koneksii()
                For i As Integer = 0 To tabelsementara.Rows.Count - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & tabelsementara.Rows(i).Item(3) & "' WHERE id = '" & tabelsementara.Rows(i).Item(7) & "' AND gudang_id ='" & kodedarigudanglama & "'"
                    myCommand.ExecuteNonQuery()

                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & tabelsementara.Rows(i).Item(3) & "' WHERE id = '" & tabelsementara.Rows(i).Item(8) & "' AND gudang_id ='" & kodekegudanglama & "'"
                    myCommand.ExecuteNonQuery()
                Next

                'hapus di tabel transfer detail
                myCommand.CommandText = "DELETE FROM tb_transfer_barang_detail WHERE transfer_barang_id = '" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                'gudang awal
                For i As Integer = 0 To GridView1.RowCount - 1
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & Val(GridView1.GetRowCellValue(i, "banyak")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "ke_stok_id") & "' AND gudang_id ='" & idkegudang & "'"
                    myCommand.ExecuteNonQuery()

                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & Val(GridView1.GetRowCellValue(i, "banyak")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "dari_stok_id") & "' AND gudang_id ='" & iddarigudang & "'"
                    myCommand.ExecuteNonQuery()

                    myCommand.CommandText = "INSERT INTO tb_transfer_barang_detail(transfer_barang_id, barang_id, dari_stok_id, ke_stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, created_by, updated_by,date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "dari_stok_id") & "','" & GridView1.GetRowCellValue(i, "ke_stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                    myCommand.ExecuteNonQuery()
                Next

                myCommand.CommandText = "UPDATE tb_transfer_barang SET dari_gudang_id ='" & iddarigudang & "', ke_gudang_id ='" & idkegudang & "', user_id ='" & iduser & "' , tanggal_transfer_barang ='" & Format(dttransferbarang.Value, "yyyy-MM-dd HH:mm:ss") & "', keterangan_transfer_barang ='" & txtketerangan.Text & "', updated_by ='" & fmenu.kodeuser.Text & "', last_updated = now() WHERE id ='" & nomornota & "'"
                myCommand.ExecuteNonQuery()

                myTrans.Commit()
                Console.WriteLine("Both records are written to database.")

                'history user ==========
                Call historysave("Mengedit Data Transfer Barang Kode " & txtnonota.Text, txtnonota.Text)
                '=======================
                MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
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

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
End Class