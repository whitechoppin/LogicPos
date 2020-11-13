Imports System.Data.Odbc
Imports System.IO
Imports DevExpress.Utils
Imports ZXing

Public Class fpenyesuaianstok
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean

    Public tabelplus, tabelminus As DataTable
    Dim hitnumber As Integer

    'variabel dalam penyesuaian
    Public jenis, satuan As String
    Dim idbarang, idstok As Integer
    Dim idpenyesuaianstok, idgudang, iduser As String
    Dim banyak As Double
    'variabel bantuan view penyesuaian
    Dim nomornota, nomorsales, nomorgudang, viewketerangan As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtgltransfer As DateTime

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

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_penyesuaian_stok ORDER BY id DESC LIMIT 1;"
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
        sql = "SELECT id FROM tb_penyesuaian_stok WHERE date_created < (SELECT date_created FROM tb_penyesuaian_stok WHERE id = '" & previousnumber & "') ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT id FROM tb_penyesuaian_stok WHERE date_created > (SELECT date_created FROM tb_penyesuaian_stok WHERE id = '" & nextingnumber & "') ORDER BY date_created ASC LIMIT 1"
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

    Sub previewtransferbarang(lihat As String)
        sql = "SELECT * FROM tb_penyesuaian_stok_detail WHERE penyesuaian_stok_id ='" & lihat & "' AND status_barang='PLUS'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabelplus.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"), dr("status_stok"))
        End While
        GridControl1.RefreshDataSource()

        sql = "SELECT * FROM tb_penyesuaian_stok_detail WHERE penyesuaian_stok_id ='" & lihat & "' AND status_barang='MINUS'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabelminus.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"), dr("status_stok"))
        End While
        GridControl2.RefreshDataSource()
    End Sub

    Sub tabel_utama()
        tabelplus = New DataTable
        With tabelplus
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
            .Columns.Add("status_stok")
        End With

        tabelminus = New DataTable
        With tabelminus
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
            .Columns.Add("status_stok")
        End With

        GridControl1.DataSource = tabelplus

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 10

        GridColumn2.FieldName = "kode_stok"
        GridColumn2.Caption = "Kode Stok"
        GridColumn2.Width = 10

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 35

        GridColumn4.FieldName = "qty"
        GridColumn4.Caption = "Qty"
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
        'GridColumn7.Visible = False

        GridColumn8.FieldName = "stok_id"
        GridColumn8.Caption = "id stok"
        GridColumn8.Width = 10
        'GridColumn8.Visible = False

        GridColumn9.FieldName = "status_stok"
        GridColumn9.Caption = "status stok"
        GridColumn9.Width = 10

        GridControl2.DataSource = tabelminus

        GridColumn10.FieldName = "kode_barang"
        GridColumn10.Caption = "Kode Barang"
        GridColumn10.Width = 10

        GridColumn11.FieldName = "kode_stok"
        GridColumn11.Caption = "Kode Stok"
        GridColumn11.Width = 10

        GridColumn12.FieldName = "nama_barang"
        GridColumn12.Caption = "Nama Barang"
        GridColumn12.Width = 35

        GridColumn13.FieldName = "qty"
        GridColumn13.Caption = "Qty"
        GridColumn13.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn13.DisplayFormat.FormatString = "{0:n0}"
        GridColumn13.Width = 5

        GridColumn14.FieldName = "satuan_barang"
        GridColumn14.Caption = "Satuan Barang"
        GridColumn14.Width = 10

        GridColumn15.FieldName = "jenis_barang"
        GridColumn15.Caption = "Jenis Barang"
        GridColumn15.Width = 10

        GridColumn16.FieldName = "barang_id"
        GridColumn16.Caption = "id barang"
        GridColumn16.Width = 10
        'GridColumn16.Visible = False

        GridColumn17.FieldName = "stok_id"
        GridColumn17.Caption = "id stok"
        GridColumn17.Width = 10
        'GridColumn17.Visible = False

        GridColumn18.FieldName = "status_stok"
        GridColumn18.Caption = "status stok"
        GridColumn18.Width = 10
    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        GridControl2.RefreshDataSource()

        txtkodestok.Clear()
        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyak.Clear()
        txtkodestok.Focus()
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

    Sub awalbaru()
        'bersihkan dan set default value
        'button tools
        btnbaru.Enabled = False
        btnsimpan.Enabled = True
        btnprint.Enabled = False
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgotransferbarang.Enabled = False
        btncaripenyesuaian.Enabled = False
        btnnext.Enabled = False

        'isi combo box
        Call comboboxuser()
        Call comboboxgudang()

        'header
        txtnonota.Clear()
        txtnonota.Enabled = False

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        cmbgudang.SelectedIndex = -1
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

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
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngo.Enabled = False
        txtgotransferbarang.Enabled = False
        btncaripenyesuaian.Enabled = False
        btnnext.Enabled = False

        'header
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False


        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        cmbgudang.Enabled = True
        btncarigudang.Enabled = True
        txtgudang.Enabled = False

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
        btnbatal.Enabled = False

        'button navigations
        btnprev.Enabled = True
        btngo.Enabled = True
        txtgotransferbarang.Enabled = True
        btncaripenyesuaian.Enabled = True
        btnnext.Enabled = True

        'isi combo box
        Call comboboxuser()
        Call comboboxgudang()

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False
        btncarigudang.Enabled = False
        txtgudang.Enabled = False

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
            sql = "SELECT * FROM tb_penyesuaian_stok WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorsales = dr("user_id")
                nomorgudang = dr("gudang_id")

                statusprint = dr("print_transfer_barang")
                statusposted = dr("posted_transfer_barang")

                viewtgltransfer = dr("tanggal_transfer_barang")
                viewketerangan = dr("keterangan_transfer_barang")

                txtnonota.Text = nomornota
                cmbsales.SelectedValue = nomorsales
                cmbgudang.SelectedValue = nomorgudang

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

            cmbgudang.SelectedIndex = -1

            cbprinted.Checked = False
            cbposted.Checked = False

            dttransferbarang.Value = Date.Now

            txtketerangan.Text = ""
        End If
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

    Sub carisales()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = dr("id")
        End If
    End Sub

    Sub caristok()
        Call koneksii()
        sql = "SELECT tb_stok.id as idstok, tb_barang.id as idbarang, tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang FROM tb_stok JOIN tb_barang ON tb_barang.id = tb_stok.barang_id WHERE kode_stok = '" & txtkodestok.Text & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idstok = Val(dr("idstok"))
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

    Private Sub fpenyesuaianstok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        idpenyesuaianstok = currentnumber()
        Call inisialisasi(idpenyesuaianstok)

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
        End With

        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
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

        Call historysave("Membuka Transaksi Penyesuaian Stok", "N/A")
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call inisialisasi(idpenyesuaianstok)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btncaripenyesuaian_Click(sender As Object, e As EventArgs) Handles btncaripenyesuaian.Click

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call carisales()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call carisales()
    End Sub

    Private Sub cmbgudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbgudang.SelectedIndexChanged
        Call carigudang()
    End Sub

    Private Sub cmbgudang_TextChanged(sender As Object, e As EventArgs) Handles cmbgudang.TextChanged
        Call carigudang()
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 8
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupcaristok = 5
        idgudangcari = idgudang
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
        'Columns.Add("stok_id")
        'Columns.Add("status_stok")

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtbanyak.Text = "" Or banyak = 0 Then
            MsgBox("Barang Kosong", MsgBoxStyle.Information, "Informasi")
        Else
            If GridView2.RowCount > 0 Then
                For i As Integer = 0 To GridView2.RowCount - 1
                    If Val(GridView2.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                        lokasi = i
                    End If
                Next

                If lokasi > -1 Then
                    MsgBox("Stok sudah ada di tabel minus", MsgBoxStyle.Information, "Informasi")
                Else
                    If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                        sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        If dr.HasRows Then
                            If dr("jumlah_stok") < banyak Then
                                MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabelplus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, idbarang, idstok, "PLUS")
                                Call reload_tabel()
                            End If
                        End If
                    Else 'kalau ada isi
                        sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            For i As Integer = 0 To GridView1.RowCount - 1
                                If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                                    lokasi = i
                                End If
                            Next

                            tbbanyak = GridView1.GetRowCellValue(lokasi, "qty")

                            If lokasi = -1 Then
                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    tabelplus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "PLUS")
                                    Call reload_tabel()
                                End If
                            Else
                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                    tabelplus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "PLUS")
                                    Call reload_tabel()
                                End If
                            End If
                        Else
                            MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                        End If
                    End If
                End If
            Else
                If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                    sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    If dr.HasRows Then
                        If dr("jumlah_stok") < banyak Then
                            MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabelplus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, idbarang, idstok, "PLUS")
                            Call reload_tabel()
                        End If
                    End If
                Else 'kalau ada isi
                    sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        For i As Integer = 0 To GridView1.RowCount - 1
                            If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                                lokasi = i
                            End If
                        Next

                        tbbanyak = GridView1.GetRowCellValue(lokasi, "qty")

                        If lokasi = -1 Then
                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabelplus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "PLUS")
                                Call reload_tabel()
                            End If
                        Else
                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(lokasi))
                                tabelplus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "PLUS")
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

    Sub kurang()
        Dim tbbanyak As Integer = 0
        Dim lokasi As Integer = -1
        'Columns.Add("kode_barang")
        'Columns.Add("kode_stok")
        'Columns.Add("nama_barang")
        'Columns.Add("banyak", GetType(Double))
        'Columns.Add("satuan_barang")
        'Columns.Add("jenis_barang")
        'Columns.Add("barang_id")
        'Columns.Add("stok_id")
        'Columns.Add("status_stok")

        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txtbanyak.Text = "" Or banyak = 0 Then
            MsgBox("Barang Kosong", MsgBoxStyle.Information, "Informasi")
        Else
            If GridView1.RowCount > 0 Then
                For i As Integer = 0 To GridView1.RowCount - 1
                    If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                        lokasi = i
                    End If
                Next

                If lokasi > -1 Then
                    MsgBox("Stok sudah ada di tabel plus", MsgBoxStyle.Information, "Informasi")
                Else
                    If GridView2.RowCount = 0 Then 'kondisi keranjang kosong
                        sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        If dr.HasRows Then
                            If dr("jumlah_stok") < banyak Then
                                MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabelminus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, idbarang, idstok, "MINUS")
                                Call reload_tabel()
                            End If
                        End If
                    Else 'kalau ada isi
                        sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                        cmmd = New OdbcCommand(sql, cnn)
                        dr = cmmd.ExecuteReader
                        dr.Read()
                        If dr.HasRows Then
                            For i As Integer = 0 To GridView2.RowCount - 1
                                If Val(GridView2.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                                    lokasi = i
                                End If
                            Next

                            tbbanyak = GridView2.GetRowCellValue(lokasi, "qty")

                            If lokasi = -1 Then
                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    tabelminus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "MINUS")
                                    Call reload_tabel()
                                End If
                            Else
                                If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                    MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Else
                                    GridView2.DeleteRow(GridView1.GetRowHandle(lokasi))
                                    tabelminus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "MINUS")
                                    Call reload_tabel()
                                End If
                            End If
                        Else
                            MsgBox("Stok Kosong", MsgBoxStyle.Information, "Informasi")
                        End If
                    End If
                End If
            Else
                If GridView2.RowCount = 0 Then 'kondisi keranjang kosong
                    sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    If dr.HasRows Then
                        If dr("jumlah_stok") < banyak Then
                            MsgBox("Stok Tidak Mencukupi", MsgBoxStyle.Information, "Informasi")
                        Else
                            tabelminus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak, satuan, jenis, idbarang, idstok, "MINUS")
                            Call reload_tabel()
                        End If
                    End If
                Else 'kalau ada isi
                    sql = "SELECT * FROM tb_stok WHERE id = '" & idstok & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr.HasRows Then
                        For i As Integer = 0 To GridView2.RowCount - 1
                            If Val(GridView2.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                                lokasi = i
                            End If
                        Next

                        tbbanyak = GridView2.GetRowCellValue(lokasi, "qty")

                        If lokasi = -1 Then
                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                tabelminus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "MINUS")
                                Call reload_tabel()
                            End If
                        Else
                            If dr("jumlah_stok") < (banyak + tbbanyak) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                            Else
                                GridView2.DeleteRow(GridView1.GetRowHandle(lokasi))
                                tabelminus.Rows.Add(txtkodebarang.Text, txtkodestok.Text, txtnamabarang.Text, banyak + tbbanyak, satuan, jenis, idbarang, idstok, "MINUS")
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

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
    End Sub

    Private Sub btnkurang_Click(sender As Object, e As EventArgs) Handles btnkurang.Click
        Call kurang()
    End Sub

    Private Sub riteqtyminus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles riteqtyminus.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub riteqtyplus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles riteqtyplus.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub

    Private Sub GridControl2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl2.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView2.DeleteSelectedRows()
        End If
    End Sub

    Private Sub fpenyesuaianstok_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class