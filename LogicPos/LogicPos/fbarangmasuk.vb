Imports System.Data.Odbc
Imports System.IO
Imports ZXing

Public Class fbarangmasuk
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean
    Dim tabel, tabelsementara As DataTable
    Dim hitnumber As Integer

    Dim harga, modalpembelian, ongkir, ppn, diskonpersen, diskonnominal, ppnpersen, ppnnominal, total1, total2, grandtotal, banyak As Double
    Dim satuan, jenis, idsupplier, idbarangmasuk, idgudang, iduser As String
    Dim idbarang, idstok As Integer

    'variabel bantuan view pembelian
    Dim nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan, viewbayar As String
    Dim viewtglbarangmasuk As DateTime
    Dim statusprint, statusposted, statusedit As Boolean

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

    Private Sub fbarangmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idbarangmasuk = currentnumber()
        Call inisialisasi(idbarangmasuk)
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

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_barang_masuk ORDER BY id DESC LIMIT 1;"
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
        sql = "SELECT id FROM tb_barang_masuk WHERE date_created < (SELECT date_created FROM tb_barang_masuk WHERE id = '" & previousnumber & "')ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT id FROM tb_barang_masuk WHERE date_created > (SELECT date_created FROM tb_barang_masuk WHERE id = '" & nextingnumber & "')ORDER BY date_created ASC LIMIT 1"
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
    Sub previewpembelian(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_barang_masuk_detail WHERE barang_masuk_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"))
            tabelsementara.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()
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
        txtgobarangmasuk.Enabled = True
        btncarimasuk.Enabled = True
        btnnext.Enabled = True

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        'buat tabel
        Call tabel_utama()

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
        txtbanyakbarang.Text = 1
        txtbanyakbarang.Enabled = False

        lblsatuan.Text = "satuan"

        btntambahbarang.Enabled = False
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        'total tabel pembelian
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode > 0 Then
            Call koneksii()
            sql = "SELECT * FROM tb_barang_masuk WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorsupplier = dr("supplier_id")
                nomorsales = dr("user_id")
                nomorgudang = dr("gudang_id")

                statusprint = dr("print_barang_masuk")
                statusposted = dr("posted_barang_masuk")
                viewtglbarangmasuk = dr("tgl_barang_masuk")
                viewketerangan = dr("keterangan_barang_masuk")

                txtnonota.Text = nomornota
                cmbsupplier.SelectedValue = nomorsupplier
                cmbsales.SelectedValue = nomorsales
                cmbgudang.SelectedValue = nomorgudang

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dtbarangmasuk.Value = viewtglbarangmasuk

                'isi tabel view pembelian

                Call previewpembelian(nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan

            End If
        Else
            txtnonota.Clear()
            cmbsupplier.SelectedIndex = -1
            cmbsales.SelectedIndex = -1
            cmbgudang.SelectedIndex = -1
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

        'isi combo box
        Call comboboxsupplier()
        Call comboboxuser()
        Call comboboxgudang()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbsupplier.Enabled = True
        cmbsupplier.SelectedIndex = -1
        cmbsupplier.Focus()
        btncarisupplier.Enabled = True

        txtalamat.Enabled = True
        txttelp.Enabled = True

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

        cbprinted.Checked = False
        cbposted.Checked = False

        dtbarangmasuk.Enabled = True
        dtbarangmasuk.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncari.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 1
        txtbanyakbarang.Enabled = True

        lblsatuan.Text = "satuan"

        btntambahbarang.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'buat tabel
        Call tabel_utama()

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
        txtbanyakbarang.Text = 1
        txtbanyakbarang.Enabled = True

        lblsatuan.Text = "satuan"

        btntambahbarang.Enabled = True

        'validasi Grid
        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        txtketerangan.Enabled = True
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
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        tabelsementara = New DataTable
        With tabelsementara
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Caption = "Kode Stok"
        GridColumn1.Width = 15

        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.Width = 15

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 40

        GridColumn4.Caption = "Qty"
        GridColumn4.FieldName = "qty"
        GridColumn4.Width = 20

        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn5.Width = 10

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 10

        GridColumn7.FieldName = "barang_id"
        GridColumn7.Caption = "Barang id"
        GridColumn7.Width = 15
        GridColumn7.Visible = False

        GridColumn8.FieldName = "stok_id"
        GridColumn8.Caption = "stok id"
        GridColumn8.Width = 15
        GridColumn8.Visible = False
    End Sub

    Sub comboboxsupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsupplier.DataSource = ds.Tables(0)
        cmbsupplier.ValueMember = "id"
        cmbsupplier.DisplayMember = "kode_supplier"
    End Sub
    Sub comboboxgudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbgudang.DataSource = ds.Tables(0)
        cmbgudang.ValueMember = "id"
        cmbgudang.DisplayMember = "kode_gudang"
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
            idbarang = dr("id")
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
            idgudang = dr("id")
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
            idsupplier = dr("id")
            txtalamat.Text = dr("alamat_supplier")
            txttelp.Text = dr("telepon_supplier")
        Else
            txtalamat.Text = ""
            txttelp.Text = ""
        End If
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT id FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            iduser = dr("id")
        Else
            iduser = 0
        End If
    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call cariuser()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call cariuser()
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
        tutupcaribarang = 3
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

    Private Sub txtkodebarang_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodebarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call tambah()
        End If
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
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
                    sql = "SELECT * FROM tb_stok WHERE barang_id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' AND gudang_id='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        idstok = Val(dr("id"))
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    Else
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idstok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    End If
                Else
                    Call koneksii()
                    sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                    cmmd = New OdbcCommand(sql, cnn)
                    idstok = CInt(cmmd.ExecuteScalar())
                    GridView1.SetRowCellValue(i, "stok_id", idstok)
                End If
            Next

            Call koneksii()
            sql = "INSERT INTO tb_barang_masuk(supplier_id, gudang_id, user_id, tgl_barang_masuk, print_barang_masuk, posted_barang_masuk, keterangan_barang_masuk, created_by, updated_by,date_created, last_updated) VALUES ('" & idsupplier & "','" & idgudang & "','" & iduser & "','" & Format(dtbarangmasuk.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idbarangmasuk = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "jenis_barang").Equals("Satuan") Then
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                Else
                    GridView1.SetRowCellValue(i, "kode_stok", String.Concat(GridView1.GetRowCellValue(i, "kode_barang"), GridView1.GetRowCellValue(i, "stok_id")))
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "', kode_stok = CONCAT(kode_barang,id), status_stok='1' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                End If

                myCommand.CommandText = "INSERT INTO tb_barang_masuk_detail (barang_masuk_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty,created_by, updated_by,date_created, last_updated) VALUES ('" & idbarangmasuk & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Barang Masuk Kode " + idbarangmasuk, idbarangmasuk)
            '========================
            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(idbarangmasuk)
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
            If cekcetakan(txtnonota.Text).Equals(True) Then
                statusizincetak = False
                passwordid = 10
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_barang_masuk SET print_barang_masuk = 1 WHERE id = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Barang Masuk Kode " + txtnonota.Text, txtnonota.Text)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_barang_masuk SET print_barang_masuk = 1 WHERE id = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Barang Masuk Kode " + txtnonota.Text, txtnonota.Text)
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
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")
            baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
            tabel_faktur.Rows.Add(baris)
        Next

        rpt_faktur = New fakturbarangmasuk
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dtbarangmasuk.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("penerima", fmenu.kodeuser.Text)

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
        Dim idgudanglama As String

        'variabel transactional
        '=======================
        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction
        '=======================

        Call koneksii()
        sql = "SELECT gudang_id FROM tb_barang_masuk WHERE id = '" & nomornota & "'"
        cmmd = New OdbcCommand(sql, cnn)
        idgudanglama = Val(cmmd.ExecuteScalar())

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            'update data yang sebelumnya (kembalikan stok dulu)
            'sediakan wadah stok nya dulu
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "jenis_barang").Equals("Satuan") Then
                    Call koneksii()
                    sql = "SELECT * FROM tb_stok WHERE barang_id = '" & GridView1.GetRowCellValue(i, "barang_id") & "' AND gudang_id='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()
                    If dr.HasRows Then
                        idstok = Val(dr("id"))
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    Else
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','1', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idstok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    End If
                Else
                    'update di wadah yang sama
                    If GridView1.GetRowCellValue(i, "stok_id") = 0 Then
                        Call koneksii()
                        sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                        cmmd = New OdbcCommand(sql, cnn)
                        idstok = CInt(cmmd.ExecuteScalar())
                        GridView1.SetRowCellValue(i, "stok_id", idstok)
                    Else
                        Call koneksii()
                        sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader()
                        If dr.HasRows Then
                            idstok = Val(dr("id"))
                            GridView1.SetRowCellValue(i, "stok_id", idstok)
                        Else
                            Call koneksii()
                            sql = "INSERT INTO tb_stok(kode_stok, kode_barang, nama_stok, status_stok, jumlah_stok, barang_id, gudang_id, created_by, updated_by, date_created, last_updated) VALUES ('" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','0', '0','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & idgudang & "', '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
                            cmmd = New OdbcCommand(sql, cnn)
                            idstok = CInt(cmmd.ExecuteScalar())
                            GridView1.SetRowCellValue(i, "stok_id", idstok)
                        End If
                    End If

                End If
            Next

            'update kembali dari transaksi sebelumnya
            Call koneksii()
            For i As Integer = 0 To tabelsementara.Rows.Count - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & tabelsementara.Rows(i).Item(3) & "' WHERE id = '" & tabelsementara.Rows(i).Item(7) & "' AND gudang_id ='" & idgudanglama & "'"
                myCommand.ExecuteNonQuery()
            Next

            'hapus tb_barang_masuk_detail
            Call koneksii()
            myCommand.CommandText = "DELETE FROM tb_barang_masuk_detail WHERE barang_masuk_id = '" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            'loop isi barang masuk detail

            Call koneksii()
            myCommand.CommandText = "UPDATE tb_barang_masuk SET supplier_id = '" & idsupplier & "', gudang_id = '" & idgudang & "', user_id = '" & iduser & "', tgl_barang_masuk = '" & Format(dtbarangmasuk.Value, "yyyy-MM-dd HH:mm:ss") & "', print_barang_masuk = 0, posted_barang_masuk = 1, keterangan_barang_masuk = '" & txtketerangan.Text & "', updated_by = '" & fmenu.kodeuser.Text & "', last_updated = now() WHERE id = '" & nomornota & "'"
            myCommand.ExecuteNonQuery()

            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "jenis_barang").Equals("Satuan") Then
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                Else
                    myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView1.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id='" & idgudang & "'"
                    myCommand.ExecuteNonQuery()
                End If

                myCommand.CommandText = "INSERT INTO tb_barang_masuk_detail(barang_masuk_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty,created_by, updated_by,date_created, last_updated) VALUES ('" & nomornota & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Mengedit Data Barang Masuk Kode " + nomornota, nomornota)
            '========================
            MsgBox("Update Berhasil", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(nomornota)
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
            Call inisialisasi(idbarangmasuk)
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
            sql = "SELECT id FROM tb_barang_masuk WHERE id = '" + txtgobarangmasuk.Text + "' LIMIT 1"
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
        tutupcaribarangmasuk = 1
        fcaribarangmasuk.ShowDialog()
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub

    Private Sub txtbanyakbarang_TextChanged(sender As Object, e As EventArgs) Handles txtbanyakbarang.TextChanged
        If txtbanyakbarang.Text = "" Or txtbanyakbarang.Text = "0" Then
            txtbanyakbarang.Text = 1
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
        Call koneksii()
        If txtkodebarang.Text = "" Or txtbanyakbarang.Text = "" Or banyak <= 0 Then
            MsgBox("Isi Kode Barang")
        Else
            idstok = 0
            If GridView1.RowCount = 0 Then  'data tidak ada
                If jenis.Equals("Satuan") Then
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, idbarang, idstok)
                    Call reload_tabel()
                Else
                    'tambahkan data ke tabel keranjang
                    tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, idbarang, idstok)
                    Call reload_tabel()
                End If
            Else 'data ada
                Dim lokasi As Integer = -1
                Dim qtytambah As Integer
                If GridView1.RowCount <> 0 Then
                    'MsgBox("data ada")
                    If jenis.Equals("Satuan") Then
                        'MsgBox("ini pcs")
                        For i As Integer = 0 To GridView1.RowCount - 1
                            If GridView1.GetRowCellValue(i, "kode_barang").Equals(txtkodebarang.Text) And GridView1.GetRowCellValue(i, "satuan_barang").Equals("Pcs") Then
                                lokasi = i
                            End If
                        Next

                        If lokasi = -1 Then
                            tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, idbarang, idstok)
                            Call reload_tabel()
                        Else
                            qtytambah = Val(GridView1.GetRowCellValue(lokasi, "qty"))

                            GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                            tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak + qtytambah), satuan, jenis, idbarang, idstok)
                            Call reload_tabel()
                        End If
                    Else
                        'tambahkan data ke tabel keranjang
                        tabel.Rows.Add(txtkodebarang.Text, txtkodebarang.Text, txtnamabarang.Text, Val(banyak), satuan, jenis, idbarang, idstok)
                        Call reload_tabel()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub
End Class