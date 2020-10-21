Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports ZXing

Public Class freturbeli
    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel1, tabel2 As DataTable
    Dim hitnumber As Integer

    'variabel dalam penjualan
    Dim jenis, satuan, idreturbeli, iduser As String

    'variabel bantuan view pembelian
    Dim nomorretur, nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan As String
    Dim statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglretur, viewtglpembelian, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    Dim rpt_faktur As New ReportDocument

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

    Private Sub freturbeli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idreturbeli = currentnumber()
        Call inisialisasi(idreturbeli)

        'Call awalbaru()
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With
        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
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

        Call historysave("Membuka Transaksi Retur Pembelian", "N/A")
    End Sub

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_retur_pembelian ORDER BY id DESC LIMIT 1;"
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
        sql = "SELECT id FROM tb_retur_pembelian WHERE date_created < (SELECT date_created FROM tb_retur_pembelian WHERE id = '" & previousnumber & "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT id FROM tb_retur_pembelian WHERE date_created > (SELECT date_created FROM tb_retur_pembelian WHERE id = '" & nextingnumber & "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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

    Sub previewreturpembelian(lihatbeli As String, lihatretur As String)
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian WHERE id ='" & lihatbeli & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            nomorsupplier = dr("supplier_id")
            nomorgudang = dr("gudang_id")
            viewtglpembelian = dr("tgl_pembelian")
            viewtgljatuhtempo = dr("tgl_jatuhtempo_pembelian")

            dtpembelian.Value = viewtglpembelian
            dtjatuhtempo.Value = viewtgljatuhtempo
        End While

        sql = "SELECT * FROM tb_supplier WHERE id ='" & nomorsupplier & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            txtsupplier.Text = dr("nama_supplier")
            txttelp.Text = dr("telepon_supplier")
            txtalamat.Text = dr("alamat_supplier")
        End While

        sql = "SELECT * FROM tb_gudang WHERE id ='" & nomorgudang & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            txtgudang.Text = dr("nama_gudang")
        End While

        sql = "SELECT * FROM tb_pembelian_detail WHERE pembelian_id ='" & lihatbeli & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()

        sql = "SELECT * FROM tb_retur_pembelian_detail WHERE retur_pembelian_id ='" & lihatretur & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel2.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")), dr("barang_id"), dr("stok_id"))
        End While
        GridControl2.RefreshDataSource()

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

    Sub carisales()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user ='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = dr("id")
        End If
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
        btngoretur.Enabled = False
        txtgoretur.Enabled = False
        btncariretur.Enabled = False
        btnnext.Enabled = False

        Call comboboxuser()

        'header
        txtnoretur.Clear()
        txtnoretur.Enabled = False

        txtnonota.Clear()
        txtnonota.Enabled = True
        btncarinota.Enabled = True
        btngo.Enabled = True

        cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

        txtsupplier.Clear()
        txttelp.Clear()
        txtalamat.Clear()

        dtreturbeli.Enabled = True
        dtreturbeli.Value = Date.Now

        cbprinted.Checked = False
        cbposted.Checked = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = False

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'buat tabel
        Call tabel_utama()
        Call tabel_retur()
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
        btngoretur.Enabled = True
        txtgoretur.Enabled = True
        btncariretur.Enabled = True
        btnnext.Enabled = True

        Call comboboxuser()

        'header
        txtnoretur.Clear()
        txtnoretur.Enabled = False

        txtnonota.Clear()
        txtnonota.Enabled = False
        btncarinota.Enabled = False
        btngo.Enabled = False

        cmbsales.Enabled = False

        txtsupplier.Clear()
        txttelp.Clear()
        txtalamat.Clear()

        dtreturbeli.Enabled = False
        dtreturbeli.Value = Date.Now

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = False

        Call tabel_utama()
        Call tabel_retur()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode > 0 Then
            Call koneksii()
            sql = "SELECT * FROM tb_retur_pembelian WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomorretur = dr("id")
                nomorsales = dr("user_id")
                nomornota = dr("pembelian_id")
                viewtglretur = dr("tgl_returbeli")

                statusprint = dr("print_returbeli")
                statusposted = dr("posted_returbeli")

                viewketerangan = dr("keterangan_returbeli")

                txtnoretur.Text = nomorretur
                cmbsales.SelectedValue = nomorsales
                txtnonota.Text = nomornota
                dtreturbeli.Value = viewtglretur

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                'isi tabel view pembelian

                Call previewreturpembelian(nomornota, nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan

            End If
        Else
            txtnoretur.Clear()
            txtnonota.Clear()
            dtreturbeli.Value = Date.Now
            cmbsales.SelectedIndex = -1

            cbprinted.Checked = False
            cbposted.Checked = False

            txtsupplier.Clear()
            txttelp.Clear()
            txtalamat.Clear()

            dtpembelian.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            txtgudang.Text = ""
            txtketerangan.Text = ""
        End If
    End Sub

    Private Sub freturbeli_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub tabel_utama()
        tabel1 = New DataTable
        With tabel1
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_beli", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl1.DataSource = tabel1

        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Caption = "Kode Stok"
        GridColumn1.Width = 10

        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.Width = 10

        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.Width = 35

        GridColumn4.Caption = "Qty"
        GridColumn4.FieldName = "qty"
        GridColumn4.Width = 5

        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Caption = "Satuan Barang"
        GridColumn5.Width = 10

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 10

        GridColumn7.FieldName = "harga_beli"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 10

        GridColumn8.FieldName = "subtotal"
        GridColumn8.Caption = "Subtotal"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 10

        GridColumn17.FieldName = "barang_id"
        GridColumn17.Caption = "Barang id"
        GridColumn17.Width = 5
        'GridColumn17.Visible = False

        GridColumn18.FieldName = "stok_id"
        GridColumn18.Caption = "stok id"
        GridColumn18.Width = 5
        'GridColumn18.Visible = False

    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged
        Call carisales()
    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged
        Call carisales()
    End Sub

    Sub tabel_retur()
        tabel2 = New DataTable
        With tabel2
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_beli", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl2.DataSource = tabel2

        GridColumn9.FieldName = "kode_stok"
        GridColumn9.Caption = "Kode Stok"
        GridColumn9.Width = 10

        GridColumn10.FieldName = "kode_barang"
        GridColumn10.Caption = "Kode Barang"
        GridColumn10.Width = 10

        GridColumn11.FieldName = "nama_barang"
        GridColumn11.Caption = "Nama Barang"
        GridColumn11.Width = 35

        GridColumn12.Caption = "Qty"
        GridColumn12.FieldName = "qty"
        GridColumn12.Width = 5

        GridColumn13.FieldName = "satuan_barang"
        GridColumn13.Caption = "Satuan Barang"
        GridColumn13.Width = 10

        GridColumn14.FieldName = "jenis_barang"
        GridColumn14.Caption = "Jenis Barang"
        GridColumn14.Width = 10

        GridColumn15.FieldName = "harga_beli"
        GridColumn15.Caption = "Harga Satuan"
        GridColumn15.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn15.DisplayFormat.FormatString = "{0:n0}"
        GridColumn15.Width = 10

        GridColumn16.FieldName = "subtotal"
        GridColumn16.Caption = "Subtotal"
        GridColumn16.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn16.DisplayFormat.FormatString = "{0:n0}"
        GridColumn16.Width = 10

        GridColumn19.FieldName = "barang_id"
        GridColumn19.Caption = "Barang id"
        GridColumn19.Width = 5
        'GridColumn19.Visible = False

        GridColumn20.FieldName = "stok_id"
        GridColumn20.Caption = "stok id"
        GridColumn20.Width = 5
        'GridColumn20.Visible = False
    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtsupplier.Clear()
    End Sub

    Sub statusnonota(status As Boolean)
        txtnonota.Enabled = status
        btncarinota.Enabled = status
        btngo.Enabled = status
    End Sub

    Sub loadingpembelian(lihat As String)
        Call tabel_utama()
        Call tabel_retur()
        sql = "SELECT * FROM tb_pembelian_detail WHERE pembelian_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()
    End Sub
    Sub cari_nota()
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian JOIN tb_supplier ON tb_supplier.id = tb_pembelian.supplier_id JOIN tb_gudang ON tb_gudang.id = tb_pembelian.gudang_id WHERE tb_pembelian.id = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            'jika ditemukan
            txtsupplier.Text = dr("nama_supplier")
            txttelp.Text = dr("telepon_supplier")
            txtalamat.Text = dr("alamat_supplier")

            viewtglpembelian = dr("tgl_pembelian")
            viewtgljatuhtempo = dr("tgl_jatuhtempo_pembelian")

            dtpembelian.Value = viewtglpembelian
            dtjatuhtempo.Value = viewtgljatuhtempo
            txtgudang.Text = dr("nama_gudang")

            Call loadingpembelian(txtnonota.Text)
        Else
            'jika tidak ditemukan

            txtsupplier.Text = ""
            txttelp.Text = ""
            txtalamat.Text = ""

            MsgBox("Nota Tidak Ditemukan", MsgBoxStyle.Information, "Gagal")
        End If

    End Sub

    Private Sub btncarinota_Click(sender As Object, e As EventArgs) Handles btncarinota.Click
        tutupbeli = 1
        fcaripembelian.Show()
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call cari_nota()
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub simpan()
        'ambil total retur dan total penjualan
        Dim total_pembelian As Double = GridView1.Columns("subtotal").SummaryItem.SummaryValue
        Dim total_retur As Double = GridView2.Columns("subtotal").SummaryItem.SummaryValue
        statusvoid = 0

        Call koneksii()

        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            Call koneksii()
            sql = "INSERT INTO tb_retur_pembelian (user_id, pembelian_id, tgl_returbeli, print_returbeli, posted_returbeli, keterangan_returbeli, total_retur, created_by, updated_by, date_created, last_updated) VALUES ('" & iduser & "','" & txtnonota.Text & "','" & Format(dtreturbeli.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & total_retur & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idreturbeli = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView2.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView2.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView2.GetRowCellValue(i, "stok_id") & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "INSERT INTO tb_retur_pembelian_detail(retur_pembelian_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by, date_created, last_updated) VALUES ('" & idreturbeli & "','" & GridView2.GetRowCellValue(i, "barang_id") & "','" & GridView2.GetRowCellValue(i, "stok_id") & "','" & GridView2.GetRowCellValue(i, "kode_barang") & "','" & GridView2.GetRowCellValue(i, "kode_stok") & "','" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "qty") & "','" & GridView2.GetRowCellValue(i, "harga_beli") & "','" & GridView2.GetRowCellValue(i, "subtotal") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "DELETE FROM tb_pembelian_detail WHERE pembelian_id = '" & txtnonota.Text & "'"
            myCommand.ExecuteNonQuery()

            If GridView1.DataRowCount = 0 Then
                statusvoid = 1
            End If

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "INSERT INTO tb_pembelian_detail(pembelian_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by, date_created, last_updated) VALUES ('" & txtnonota.Text & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "harga_beli") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "UPDATE tb_pembelian SET total_pembelian = '" & total_pembelian & "', void_pembelian ='" & statusvoid & "' WHERE id ='" & txtnonota.Text & "'"
            myCommand.ExecuteNonQuery()

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Retur Beli Kode " & idreturbeli, idreturbeli)
            '========================
            MsgBox("Retur Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(idreturbeli)
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
            MsgBox("Retur Gagal Dilakukan", MsgBoxStyle.Information, "Gagal")
        End Try
    End Sub
    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView2.DataRowCount > 0 Then
            If txtnonota.Text IsNot "" Then
                If cmbsales.Text IsNot "" Then
                    Call simpan()
                Else
                    MsgBox("Isi Sales")
                End If
            Else
                MsgBox("Isi No Retur")
            End If
        Else
            MsgBox("Keranjang Retur Masih Kosong")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then

            If cekcetakan(txtnonota.Text).Equals(True) Then
                statusizincetak = False
                passwordid = 8
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_retur_pembelian SET print_returbeli = 1 WHERE id = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Retur Beli Kode " & txtnonota.Text, txtnonota.Text)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_retur_pembelian SET print_returbeli = 1 WHERE id = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Retur Beli Kode " + txtnonota.Text, txtnonota.Text)
                '========================

                cbprinted.Checked = True
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Public Sub cetak_faktur()
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

        Dim tabel_lama As New DataTable
        With tabel_lama
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
        End With

        Dim baris As DataRow
        For i As Integer = 0 To GridView2.RowCount - 1
            baris = tabel_lama.NewRow
            baris("kode_stok") = GridView2.GetRowCellValue(i, "kode_stok")
            baris("kode_barang") = GridView2.GetRowCellValue(i, "kode_barang")
            baris("nama_barang") = GridView2.GetRowCellValue(i, "nama_barang")
            baris("qty") = GridView2.GetRowCellValue(i, "qty")
            baris("satuan_barang") = GridView2.GetRowCellValue(i, "satuan_barang")
            baris("jenis_barang") = GridView2.GetRowCellValue(i, "jenis_barang")
            baris("harga") = GridView2.GetRowCellValue(i, "harga_beli")
            baris("subtotal") = GridView2.GetRowCellValue(i, "subtotal")
            tabel_lama.Rows.Add(baris)
        Next

        rpt_faktur = New fakturreturpembelian
        'rpt_faktur.SetDataSource(tabel_faktur)
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_lama)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnoretur.Text)
        rpt_faktur.SetParameterValue("namakasir", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("supplier", txtsupplier.Text)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dtreturbeli.Value, "dd MMMM yyyy HH:mm:ss").ToString)
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

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call inisialisasi(idreturbeli)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnoretur.Text)
    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click
        If txtgoretur.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_retur_pembelian WHERE id = '" & txtgoretur.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgoretur.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
    End Sub

    Private Sub btncariretur_Click(sender As Object, e As EventArgs) Handles btncariretur.Click
        tutupreturbeli = 1
        fcarireturbeli.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnoretur.Text)
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If btnsimpan.Enabled = True Then
            fretbeli.kode_barang = GridView1.GetFocusedRowCellValue("kode_barang")
            fretbeli.kode_stok = GridView1.GetFocusedRowCellValue("kode_stok")
            fretbeli.nama_barang = GridView1.GetFocusedRowCellValue("nama_barang")
            fretbeli.satuan_barang = GridView1.GetFocusedRowCellValue("satuan_barang")
            fretbeli.jenis_barang = GridView1.GetFocusedRowCellValue("jenis_barang")
            fretbeli.banyak = GridView1.GetFocusedRowCellValue("qty")
            fretbeli.harga_beli = GridView1.GetFocusedRowCellValue("harga_beli")
            fretbeli.subtotal = GridView1.GetFocusedRowCellValue("subtotal")
            fretbeli.idbarang = GridView1.GetFocusedRowCellValue("barang_id")
            fretbeli.idstok = GridView1.GetFocusedRowCellValue("stok_id")

            fretbeli.ShowDialog()
        End If
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Dim idstok As String = GridView2.GetFocusedRowCellValue("stok_id")
        Dim hargabeli As Integer = GridView2.GetFocusedRowCellValue("harga_beli")

        Dim lokasi As Integer
        Dim counting As Boolean = False
        Dim banyakbeli, banyakretur As Integer

        lokasi = -1
        banyakbeli = 0
        banyakretur = 0
        counting = False

        If e.KeyCode = Keys.Delete And btnsimpan.Enabled = True Then
            If GridView1.RowCount = 0 Then
                tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("qty"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_beli"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("barang_id"), GridView2.GetFocusedRowCellValue("stok_id"))
                GridView2.DeleteSelectedRows()
            Else
                For i As Integer = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "stok_id").Equals(idstok) Then
                        lokasi = i
                    End If
                Next

                If lokasi > -1 Then
                    banyakbeli = GridView1.GetRowCellValue(lokasi, "qty")
                    banyakretur = GridView2.GetFocusedRowCellValue("qty")

                    GridView1.SetRowCellValue(lokasi, "qty", Val(banyakbeli) + Val(banyakretur))
                    GridView1.SetRowCellValue(lokasi, "subtotal", hargabeli * (Val(banyakbeli) + Val(banyakretur)))

                    GridControl1.RefreshDataSource()
                    GridView2.DeleteSelectedRows()

                    counting = True
                End If

                If counting = False Then
                    tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("qty"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_beli"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("barang_id"), GridView2.GetFocusedRowCellValue("stok_id"))
                    GridView2.DeleteSelectedRows()
                End If
            End If

            If GridView2.RowCount.Equals(0) Then
                statusnonota(True)
            End If
        End If
    End Sub
End Class