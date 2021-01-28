Imports System.Data.Odbc
Imports System.IO
Imports DevExpress.Utils
Imports ZXing

Public Class fpenyesuaianstok
    Public namaform As String = "transaksi-penyesuaian_stok"

    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean

    Public tabelplus, tabelminus As DataTable
    Dim hitnumber As Integer

    'variabel dalam penyesuaian
    Public jenis, satuan As String
    Dim idbarang, idstok As Integer
    Dim idpenyesuaianstok As String
    Dim idgudang, iduser As Integer
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
        Call koneksi()
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
        Call koneksi()
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
        Call koneksi()
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
        sql = "SELECT * FROM tb_penyesuaian_stok_detail WHERE penyesuaian_stok_id ='" & lihat & "' AND status_stok='PLUS'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabelplus.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), dr("barang_id"), dr("stok_id"), dr("status_stok"))
        End While
        GridControl1.RefreshDataSource()

        sql = "SELECT * FROM tb_penyesuaian_stok_detail WHERE penyesuaian_stok_id ='" & lihat & "' AND status_stok='MINUS'"
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
        GridColumn7.Visible = False

        GridColumn8.FieldName = "stok_id"
        GridColumn8.Caption = "id stok"
        GridColumn8.Width = 10
        GridColumn8.Visible = False

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
        GridColumn16.Visible = False

        GridColumn17.FieldName = "stok_id"
        GridColumn17.Caption = "id stok"
        GridColumn17.Width = 10
        GridColumn17.Visible = False

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
        Call koneksi()
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
        Call koneksi()
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
        txtgopenyesuaianstok.Enabled = False
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

        dttransaksi.Enabled = True
        dttransaksi.Value = Date.Now


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
        btnkurang.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = True

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
        txtgopenyesuaianstok.Enabled = False
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

        dttransaksi.Enabled = True

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
        btnkurang.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = True

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
        txtgopenyesuaianstok.Enabled = True
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

        dttransaksi.Enabled = False
        dttransaksi.Value = Date.Now

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
        btnkurang.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode > 0 Then
            Call koneksi()
            sql = "SELECT * FROM tb_penyesuaian_stok WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomornota = dr("id")
                nomorsales = dr("user_id")
                nomorgudang = dr("gudang_id")

                statusprint = dr("print_penyesuaian_stok")
                statusposted = dr("posted_penyesuaian_stok")

                viewtgltransfer = dr("tanggal_penyesuaian_stok")
                viewketerangan = dr("keterangan_penyesuaian_stok")

                txtnonota.Text = nomornota
                cmbsales.SelectedValue = nomorsales
                cmbgudang.SelectedValue = nomorgudang

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                dttransaksi.Value = viewtgltransfer

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

            dttransaksi.Value = Date.Now

            txtketerangan.Text = ""
        End If
    End Sub

    Sub carigudang()
        Call koneksi()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbgudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idgudang = Val(dr("id"))
            txtgudang.Text = dr("nama_gudang")
        Else
            idgudang = 0
            txtgudang.Text = ""
        End If
    End Sub

    Sub carisales()
        Call koneksi()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = Val(dr("id"))
            cmbsales.ForeColor = Color.Black
        Else
            iduser = 0
            cmbsales.ForeColor = Color.Red
        End If
    End Sub

    Sub caristok()
        Call koneksi()
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
            idstok = 0
            idbarang = 0

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

        Call historysave("Membuka Transaksi Penyesuaian Stok", "N/A", namaform)
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub proses()
        Dim stok As Integer
        Dim stokdatabase As Integer
        Dim statusavailable As Boolean = True

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "SELECT * FROM tb_stok WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                stok = GridView1.GetRowCellValue(i, "qty")
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
        Call koneksi()

        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            sql = "INSERT INTO tb_penyesuaian_stok(gudang_id, user_id, tanggal_penyesuaian_stok, print_penyesuaian_stok, posted_penyesuaian_stok, keterangan_penyesuaian_stok, created_by, updated_by, date_created, last_updated) VALUES ('" & idgudang & "','" & iduser & "' , '" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idpenyesuaianstok = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & Val(GridView1.GetRowCellValue(i, "qty")) & "' WHERE id = '" & GridView1.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "INSERT INTO tb_penyesuaian_stok_detail(penyesuaian_stok_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, status_stok, created_by, updated_by,date_created, last_updated) VALUES ('" & idpenyesuaianstok & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "status_stok") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            For i As Integer = 0 To GridView2.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & Val(GridView2.GetRowCellValue(i, "qty")) & "' WHERE id = '" & GridView2.GetRowCellValue(i, "stok_id") & "' AND gudang_id ='" & idgudang & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "INSERT INTO tb_penyesuaian_stok_detail(penyesuaian_stok_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, status_stok, created_by, updated_by,date_created, last_updated) VALUES ('" & idpenyesuaianstok & "','" & GridView2.GetRowCellValue(i, "barang_id") & "','" & GridView2.GetRowCellValue(i, "stok_id") & "','" & GridView2.GetRowCellValue(i, "kode_barang") & "','" & GridView2.GetRowCellValue(i, "kode_stok") & "','" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "qty") & "','" & GridView2.GetRowCellValue(i, "status_stok") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Penyesuaian Stok Kode " + idpenyesuaianstok, idpenyesuaianstok, namaform)
            '========================
            MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(idpenyesuaianstok)
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
        If GridView1.DataRowCount > 0 Or GridView2.DataRowCount > 0 Then
            If txtgudang.Text IsNot "" Then
                If cmbsales.Text IsNot "" And iduser > 0 Then
                    Call proses()
                Else
                    MsgBox("Isi Sales")
                End If
            Else
                MsgBox("Isi Gudang")
            End If
        Else
            MsgBox("Keranjang Masih Kosong")
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
            .Columns.Add("status_stok")
        End With

        Dim baris As DataRow
        If GridView1.RowCount > 0 Then
            For i As Integer = 0 To GridView1.RowCount - 1
                baris = tabel_faktur.NewRow
                baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
                baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
                baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
                baris("qty") = GridView1.GetRowCellValue(i, "qty")
                baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")
                baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
                baris("status_stok") = GridView1.GetRowCellValue(i, "status_stok")
                tabel_faktur.Rows.Add(baris)
            Next
        End If

        If GridView2.RowCount > 0 Then
            For i As Integer = 0 To GridView2.RowCount - 1
                baris = tabel_faktur.NewRow
                baris("kode_stok") = GridView2.GetRowCellValue(i, "kode_stok")
                baris("kode_barang") = GridView2.GetRowCellValue(i, "kode_barang")
                baris("nama_barang") = GridView2.GetRowCellValue(i, "nama_barang")
                baris("qty") = GridView2.GetRowCellValue(i, "qty")
                baris("satuan_barang") = GridView2.GetRowCellValue(i, "satuan_barang")
                baris("jenis_barang") = GridView2.GetRowCellValue(i, "jenis_barang")
                baris("status_stok") = GridView2.GetRowCellValue(i, "status_stok")
                tabel_faktur.Rows.Add(baris)
            Next
        End If


        rpt_faktur = New fakturpenyesuaianstok
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnonota.Text)
        rpt_faktur.SetParameterValue("keterangan", txtketerangan.Text)

        rpt_faktur.SetParameterValue("tanggal", Format(dttransaksi.Value, "dd MMMM yyyy HH:mm:ss").ToString)
        rpt_faktur.SetParameterValue("user", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("gudang", txtgudang.Text)

        SetReportPageSize("Faktur", 1)
        rpt_faktur.PrintToPrinter(1, False, 0, 0)

    End Sub

    Public Sub SetReportPageSize(ByVal mPaperSize As String, ByVal PaperOrientation As Integer)
        Dim faktur As String

        faktur = GetPrinterName(2)

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

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then

            If cekcetakan(txtnonota.Text, namaform).Equals(True) Then
                statusizincetak = False
                passwordid = 18
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksi()
                    sql = "UPDATE tb_penyesuaian_stok SET print_penyesuaian_stok = 1 WHERE id = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Penyesuaian Stok Kode " + txtnonota.Text, txtnonota.Text, namaform)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksi()
                sql = "UPDATE tb_penyesuaian_stok SET print_penyesuaian_stok = 1 WHERE id = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Penyesuaian Stok Kode " + txtnonota.Text, txtnonota.Text, namaform)
                '========================

                cbprinted.Checked = True
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call inisialisasi(idpenyesuaianstok)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnonota.Text)
    End Sub

    Private Sub btncaripenyesuaian_Click(sender As Object, e As EventArgs) Handles btncaripenyesuaian.Click
        tutupcaripenyesuaianstok = 1
        fcaripenyesuaianstok.ShowDialog()
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If txtgopenyesuaianstok.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksi()
            sql = "SELECT id FROM tb_penyesuaian_stok WHERE id= '" & txtgopenyesuaianstok.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgopenyesuaianstok.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnonota.Text)
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
        If txtgudang.Text = "" Then
            MsgBox("Isi Kode Gudang", MsgBoxStyle.Information, "Informasi")
        Else
            tutupcaristok = 5
            idgudangcari = idgudang
            fcaristok.ShowDialog()
        End If
    End Sub

    Private Sub txtkodestok_TextChanged(sender As Object, e As EventArgs) Handles txtkodestok.TextChanged
        If txtgudang.Text = "" Then
            MsgBox("Isi Kode Gudang", MsgBoxStyle.Information, "Informasi")
        Else
            Call caristok()
        End If

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