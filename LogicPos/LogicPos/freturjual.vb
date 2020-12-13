Imports System.Data.Odbc
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports CrystalDecisions.CrystalReports.Engine
Imports ZXing
Imports System.IO

Public Class freturjual
    Public namaform As String = "transaksi-retur_jual"

    Public kodeakses As Integer
    Public statusizincetak As Boolean
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel1, tabel2 As DataTable
    Dim hitnumber As Integer

    'variabel dalam retur penjualan
    Dim jenis, satuan, idreturjual, iduser As String

    'variabel bantuan view penjualan
    Dim nomorretur, nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan As String
    Dim statusprint, statusposted, statusedit As Boolean
    Dim statusvoid As Integer
    Dim viewtglretur, viewtglpenjualan, viewtgljatuhtempo As DateTime
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

    Private Sub freturjual_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub freturjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        hitnumber = 0
        idreturjual = currentnumber()
        Call inisialisasi(idreturjual)

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

        Call historysave("Membuka Transaksi Retur Penjualan", "N/A", namaform)
    End Sub

    Function currentnumber()
        Call koneksii()
        sql = "SELECT id FROM tb_retur_penjualan ORDER BY id DESC LIMIT 1;"
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
        sql = "SELECT id FROM tb_retur_penjualan WHERE date_created < (SELECT date_created FROM tb_retur_penjualan WHERE id = '" & previousnumber & "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT id FROM tb_retur_penjualan WHERE date_created > (SELECT date_created FROM tb_retur_penjualan WHERE id = '" & nextingnumber & "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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
    Sub previewreturpenjualan(lihatjual As String, lihatretur As String)
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE id ='" & lihatjual & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            nomorcustomer = dr("pelanggan_id")
            nomorgudang = dr("gudang_id")
            viewtglpenjualan = dr("tgl_penjualan")
            viewtgljatuhtempo = dr("tgl_jatuhtempo_penjualan")

            dtpenjualan.Value = viewtglpenjualan
            dtjatuhtempo.Value = viewtgljatuhtempo
        End While

        sql = "SELECT * FROM tb_pelanggan WHERE id ='" & nomorcustomer & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            txtcustomer.Text = dr("nama_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
            txtalamat.Text = dr("alamat_pelanggan")
        End While

        sql = "SELECT * FROM tb_gudang WHERE id ='" & nomorgudang & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            txtgudang.Text = dr("nama_gudang")
        End While

        sql = "SELECT * FROM tb_penjualan_detail WHERE penjualan_id ='" & lihatjual & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - dr("diskon") / 100, Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")), dr("barang_id"), dr("stok_id"))

        End While
        GridControl1.RefreshDataSource()

        sql = "SELECT * FROM tb_retur_penjualan_detail WHERE retur_penjualan_id ='" & lihatretur & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel2.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - dr("diskon") / 100, Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")), dr("barang_id"), dr("stok_id"))

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
            iduser = Val(dr("id"))
            cmbsales.ForeColor = Color.Black
        Else
            iduser = 0
            cmbsales.ForeColor = Color.Red
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

        txtcustomer.Clear()
        txttelp.Clear()
        txtalamat.Clear()

        dtreturjual.Enabled = True
        dtreturjual.Value = Date.Now

        cbprinted.Checked = False
        cbposted.Checked = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = False

        'total tabel penjualan
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'buat tabel
        Call tabel_utama()
        Call tabel_retur()
    End Sub
    Sub inisialisasi(nomorkode As String)
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

        txtcustomer.Clear()
        txttelp.Clear()
        txtalamat.Clear()

        dtreturjual.Enabled = False
        dtreturjual.Value = Date.Now

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = False

        Call tabel_utama()
        Call tabel_retur()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        If nomorkode IsNot "" Then
            Call koneksii()
            sql = "SELECT * FROM tb_retur_penjualan WHERE id = '" & nomorkode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'header
                nomorretur = dr("id")
                nomorsales = dr("user_id")
                nomornota = dr("penjualan_id")
                viewtglretur = dr("tgl_returjual")

                statusprint = dr("print_returjual")
                statusposted = dr("posted_returjual")

                viewketerangan = dr("keterangan_returjual")

                txtnoretur.Text = nomorretur
                cmbsales.SelectedValue = nomorsales
                txtnonota.Text = nomornota
                dtreturjual.Value = viewtglretur

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted

                'isi tabel view pembelian

                Call previewreturpenjualan(nomornota, nomorkode)

                'total tabel pembelian

                txtketerangan.Text = viewketerangan

            End If
        Else
            txtnoretur.Clear()
            txtnonota.Clear()
            dtreturjual.Value = Date.Now
            cmbsales.SelectedIndex = -1

            cbprinted.Checked = False
            cbposted.Checked = False

            txtcustomer.Clear()
            txttelp.Clear()
            txtalamat.Clear()

            dtpenjualan.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            txtgudang.Text = ""
            txtketerangan.Text = ""
        End If
    End Sub
    Sub tabel_utama()
        tabel1 = New DataTable

        With tabel1
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl1.DataSource = tabel1

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
        GridColumn5.Width = 5

        GridColumn6.FieldName = "jenis_barang"
        GridColumn6.Caption = "Jenis Barang"
        GridColumn6.Width = 5

        GridColumn7.FieldName = "harga_satuan"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 10

        GridColumn8.FieldName = "diskon_persen"
        GridColumn8.Caption = "Diskon %"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 5

        GridColumn9.FieldName = "diskon_nominal"
        GridColumn9.Caption = "Diskon Nominal"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 10

        GridColumn10.FieldName = "harga_diskon"
        GridColumn10.Caption = "Harga Diskon"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 10

        GridColumn11.FieldName = "subtotal"
        GridColumn11.Caption = "Subtotal"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:n0}"
        GridColumn11.Width = 10

        GridColumn12.FieldName = "laba"
        GridColumn12.Caption = "Laba"
        GridColumn12.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn12.DisplayFormat.FormatString = "{0:n0}"
        GridColumn12.Width = 5
        GridColumn12.Visible = False

        GridColumn13.FieldName = "modal_barang"
        GridColumn13.Caption = "Modal Barang"
        GridColumn13.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn13.DisplayFormat.FormatString = "{0:n0}"
        GridColumn13.Width = 5
        GridColumn13.Visible = False

        GridColumn27.FieldName = "barang_id"
        GridColumn27.Caption = "Barang id"
        GridColumn27.Width = 5
        GridColumn27.Visible = False

        GridColumn28.FieldName = "stok_id"
        GridColumn28.Caption = "stok id"
        GridColumn28.Width = 5
        GridColumn28.Visible = False

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
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))
            .Columns.Add("barang_id")
            .Columns.Add("stok_id")
        End With

        GridControl2.DataSource = tabel2

        GridColumn14.FieldName = "kode_barang"
        GridColumn14.Caption = "Kode Barang"
        GridColumn14.Width = 10

        GridColumn15.FieldName = "kode_stok"
        GridColumn15.Caption = "Kode Stok"
        GridColumn15.Width = 10

        GridColumn16.FieldName = "nama_barang"
        GridColumn16.Caption = "Nama Barang"
        GridColumn16.Width = 35

        GridColumn17.FieldName = "qty"
        GridColumn17.Caption = "Qty"
        GridColumn17.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn17.DisplayFormat.FormatString = "{0:n0}"
        GridColumn17.Width = 5

        GridColumn18.FieldName = "satuan_barang"
        GridColumn18.Caption = "Satuan Barang"
        GridColumn18.Width = 5

        GridColumn19.FieldName = "jenis_barang"
        GridColumn19.Caption = "Jenis Barang"
        GridColumn19.Width = 5

        GridColumn20.FieldName = "harga_satuan"
        GridColumn20.Caption = "Harga Satuan"
        GridColumn20.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn20.DisplayFormat.FormatString = "{0:n0}"
        GridColumn20.Width = 10

        GridColumn21.FieldName = "diskon_persen"
        GridColumn21.Caption = "Diskon %"
        GridColumn21.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn21.DisplayFormat.FormatString = "{0:n0}"
        GridColumn21.Width = 5

        GridColumn22.FieldName = "diskon_nominal"
        GridColumn22.Caption = "Diskon Nominal"
        GridColumn22.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn22.DisplayFormat.FormatString = "{0:n0}"
        GridColumn22.Width = 10

        GridColumn23.FieldName = "harga_diskon"
        GridColumn23.Caption = "Harga Diskon"
        GridColumn23.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn23.DisplayFormat.FormatString = "{0:n0}"
        GridColumn23.Width = 10

        GridColumn24.FieldName = "subtotal"
        GridColumn24.Caption = "Subtotal"
        GridColumn24.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn24.DisplayFormat.FormatString = "{0:n0}"
        GridColumn24.Width = 10

        GridColumn25.FieldName = "laba"
        GridColumn25.Caption = "Laba"
        GridColumn25.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn25.DisplayFormat.FormatString = "{0:n0}"
        GridColumn25.Width = 5
        GridColumn25.Visible = False

        GridColumn26.FieldName = "modal_barang"
        GridColumn26.Caption = "Modal Barang"
        GridColumn26.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn26.DisplayFormat.FormatString = "{0:n0}"
        GridColumn26.Width = 5
        GridColumn26.Visible = False

        GridColumn29.FieldName = "barang_id"
        GridColumn29.Caption = "Barang id"
        GridColumn29.Width = 5
        GridColumn29.Visible = False

        GridColumn30.FieldName = "stok_id"
        GridColumn30.Caption = "stok id"
        GridColumn30.Width = 5
        GridColumn30.Visible = False
    End Sub
    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtcustomer.Clear()
    End Sub

    Sub statusnonota(status As Boolean)
        txtnonota.Enabled = status
        btncarinota.Enabled = status
        btngo.Enabled = status
    End Sub

    Sub loadingpenjualan(lihat As String)
        Call tabel_utama()
        Call tabel_retur()
        sql = "SELECT * FROM tb_penjualan_detail WHERE penjualan_id ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), Val(dr("qty")), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")), dr("barang_id"), dr("stok_id"))
        End While
        GridControl1.RefreshDataSource()
    End Sub
    Sub simpan()
        'ambil total retur dan total penjualan
        Dim total_penjualan As Double = GridView1.Columns("subtotal").SummaryItem.SummaryValue
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
            sql = "INSERT INTO tb_retur_penjualan(user_id, penjualan_id, tgl_returjual, print_returjual, posted_returjual, keterangan_returjual, total_retur, created_by, updated_by, date_created, last_updated) VALUES ('" & iduser & "','" & txtnonota.Text & "','" & Format(dtreturjual.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & total_retur & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now());SELECT LAST_INSERT_ID();"
            cmmd = New OdbcCommand(sql, cnn)
            idreturjual = CInt(cmmd.ExecuteScalar())

            For i As Integer = 0 To GridView2.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView2.GetRowCellValue(i, "qty") & "' WHERE id = '" & GridView2.GetRowCellValue(i, "stok_id") & "'"
                myCommand.ExecuteNonQuery()

                myCommand.CommandText = "INSERT INTO tb_retur_penjualan_detail (retur_penjualan_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by, date_created, last_updated) VALUES ('" & idreturjual & "','" & GridView2.GetRowCellValue(i, "barang_id") & "','" & GridView2.GetRowCellValue(i, "stok_id") & "','" & GridView2.GetRowCellValue(i, "kode_barang") & "','" & GridView2.GetRowCellValue(i, "kode_stok") & "','" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "qty") & "','" & GridView2.GetRowCellValue(i, "harga_satuan") & "','" & GridView2.GetRowCellValue(i, "diskon_persen") & "','" & GridView2.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView2.GetRowCellValue(i, "subtotal") & "','" & GridView2.GetRowCellValue(i, "modal_barang") & "','" & GridView2.GetRowCellValue(i, "laba") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "DELETE FROM tb_penjualan_detail WHERE penjualan_id = '" & txtnonota.Text & "'"
            myCommand.ExecuteNonQuery()

            If GridView1.DataRowCount = 0 Then
                statusvoid = 1
            Else
                statusvoid = 0
            End If

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "INSERT INTO tb_penjualan_detail(penjualan_id, barang_id, stok_id, kode_barang, kode_stok, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by, date_created, last_updated) VALUES ('" & txtnonota.Text & "','" & GridView1.GetRowCellValue(i, "barang_id") & "','" & GridView1.GetRowCellValue(i, "stok_id") & "','" & GridView1.GetRowCellValue(i, "kode_barang") & "','" & GridView1.GetRowCellValue(i, "kode_stok") & "','" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & GridView1.GetRowCellValue(i, "diskon_persen") & "','" & GridView1.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "modal_barang") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "UPDATE tb_penjualan SET total_penjualan = '" & total_penjualan & "', sisa_penjualan = '" & total_penjualan & "'- bayar_penjualan, void_penjualan ='" & statusvoid & "' WHERE id ='" & txtnonota.Text & "'"
            myCommand.ExecuteNonQuery()

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Retur Jual Kode " & idreturjual, idreturjual, namaform)
            '========================
            MsgBox("Retur Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(idreturjual)
        Catch e As Exception
            Try
                myTrans.Rollback()
            Catch ex As OdbcException
                If Not myTrans.Connection Is Nothing Then
                    'Console.WriteLine("An exception of type " + ex.GetType().ToString() + " was encountered while attempting to roll back the transaction.")
                End If
            End Try

            'Console.WriteLine("An exception of type " + e.GetType().ToString() + "was encountered while inserting the data.")
            'Console.WriteLine("Neither record was written to database.")
            MsgBox("Retur Gagal Dilakukan", MsgBoxStyle.Information, "Gagal")
            'MsgBox(e.ToString())
        End Try
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        If GridView2.DataRowCount > 0 Then
            If txtnonota.Text IsNot "" Then
                If cmbsales.Text IsNot "" And iduser > 0 Then
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

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        If tambahstatus.Equals(True) Then
            Call awalbaru()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            If cekcetakan(txtnonota.Text, namaform).Equals(True) Then
                statusizincetak = False
                passwordid = 9
                fpassword.kodetabel = txtnonota.Text
                fpassword.ShowDialog()
                If statusizincetak.Equals(True) Then
                    Call cetak_faktur()
                    Call koneksii()
                    sql = "UPDATE tb_retur_penjualan SET print_returjual = 1 WHERE id = '" & txtnonota.Text & "' "
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Mencetak Data Retur Jual Kode " & txtnonota.Text, txtnonota.Text, namaform)
                    '========================

                    cbprinted.Checked = True
                End If
            Else
                Call cetak_faktur()
                Call koneksii()
                sql = "UPDATE tb_retur_penjualan SET print_returjual = 1 WHERE kode_retur = '" & txtnonota.Text & "' "
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Mencetak Data Retur Jual Kode " + txtnonota.Text, txtnonota.Text, namaform)
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
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("satuan")
            .Columns.Add("jenis")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
        End With

        Dim baris As DataRow
        For a As Integer = 0 To GridView2.RowCount - 1
            baris = tabel_lama.NewRow
            baris("kode_barang") = GridView2.GetRowCellValue(a, "kode_barang")
            baris("kode_stok") = GridView2.GetRowCellValue(a, "kode_stok")
            baris("nama_barang") = GridView2.GetRowCellValue(a, "nama_barang")
            baris("qty") = GridView2.GetRowCellValue(a, "qty")
            baris("satuan") = GridView2.GetRowCellValue(a, "satuan_barang")
            baris("jenis") = GridView2.GetRowCellValue(a, "jenis_barang")
            baris("harga_satuan") = GridView2.GetRowCellValue(a, "harga_satuan")
            baris("diskon_persen") = GridView2.GetRowCellValue(a, "diskon_persen")
            baris("harga_diskon") = GridView2.GetRowCellValue(a, "harga_diskon")
            baris("subtotal") = GridView2.GetRowCellValue(a, "subtotal")
            baris("diskon_nominal") = GridView2.GetRowCellValue(a, "diskon_nominal")
            tabel_lama.Rows.Add(baris)
        Next

        rpt_faktur = New fakturreturpenjualan
        rpt_faktur.Database.Tables(0).SetDataSource(tabel_lama)
        rpt_faktur.Database.Tables(2).SetDataSource(tabel_barcode)

        rpt_faktur.SetParameterValue("nofaktur", txtnoretur.Text)
        rpt_faktur.SetParameterValue("namakasir", fmenu.kodeuser.Text)
        rpt_faktur.SetParameterValue("pembeli", txtcustomer.Text)
        rpt_faktur.SetParameterValue("jatem", dtjatuhtempo.Text)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("tanggal", Format(dtreturjual.Value, "dd MMMM yyyy HH:mm:ss").ToString)
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

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call inisialisasi(idreturjual)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnoretur.Text)
    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click
        If txtgoretur.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_retur_penjualan WHERE id = '" & txtgoretur.Text & "'"
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
        tutupreturjual = 1
        fcarireturjual.ShowDialog()
    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click
        Call nextnumber(txtnoretur.Text)
    End Sub

    Private Sub btncarinota_Click(sender As Object, e As EventArgs) Handles btncarinota.Click
        tutupjual = 1
        fcaripenjualan.Show()
    End Sub
    Sub cari_nota()
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan JOIN tb_pelanggan ON tb_pelanggan.id = tb_penjualan.pelanggan_id JOIN tb_gudang ON tb_gudang.id = tb_penjualan.gudang_id WHERE tb_penjualan.id = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            'jika ditemukan
            txtcustomer.Text = dr("nama_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
            txtalamat.Text = dr("alamat_pelanggan")

            viewtglpenjualan = dr("tgl_penjualan")
            viewtgljatuhtempo = dr("tgl_jatuhtempo_penjualan")

            dtpenjualan.Value = viewtglpenjualan
            dtjatuhtempo.Value = viewtgljatuhtempo
            txtgudang.Text = dr("nama_gudang")

            Call loadingpenjualan(txtnonota.Text)
        Else
            'jika tidak ditemukan

            txtcustomer.Text = ""
            txttelp.Text = ""
            txtalamat.Text = ""

            dtpenjualan.Value = Date.Now
            dtjatuhtempo.Value = Date.Now
            txtgudang.Text = ""

            MsgBox("Nota Tidak Ditemukan", MsgBoxStyle.Information, "Gagal")
        End If

    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call cari_nota()
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If btnsimpan.Enabled = True Then
            fretjual.kode_barang = GridView1.GetFocusedRowCellValue("kode_barang")
            fretjual.kode_stok = GridView1.GetFocusedRowCellValue("kode_stok")
            fretjual.nama_barang = GridView1.GetFocusedRowCellValue("nama_barang")
            fretjual.satuan_barang = GridView1.GetFocusedRowCellValue("satuan_barang")
            fretjual.jenis_barang = GridView1.GetFocusedRowCellValue("jenis_barang")
            fretjual.banyak = GridView1.GetFocusedRowCellValue("qty")
            fretjual.harga_satuan = GridView1.GetFocusedRowCellValue("harga_satuan")
            fretjual.diskon_persen = GridView1.GetFocusedRowCellValue("diskon_persen")
            fretjual.diskon_nominal = GridView1.GetFocusedRowCellValue("diskon_nominal")
            fretjual.harga_diskon = GridView1.GetFocusedRowCellValue("harga_diskon")
            fretjual.subtotal = GridView1.GetFocusedRowCellValue("subtotal")
            fretjual.laba = GridView1.GetFocusedRowCellValue("laba")
            fretjual.modal_barang = GridView1.GetFocusedRowCellValue("modal_barang")
            fretjual.idbarang = GridView1.GetFocusedRowCellValue("barang_id")
            fretjual.idstok = GridView1.GetFocusedRowCellValue("stok_id")

            fretjual.ShowDialog()
        End If
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Dim idstok As Integer = Val(GridView2.GetFocusedRowCellValue("stok_id"))
        Dim hargadiskon As Integer = GridView2.GetFocusedRowCellValue("harga_diskon")
        Dim hargamodal As Integer = GridView2.GetFocusedRowCellValue("modal_barang")

        Dim lokasi As Integer
        Dim counting As Boolean = False
        Dim banyakjual, banyakretur As Integer

        lokasi = -1
        banyakjual = 0
        banyakretur = 0
        counting = False

        If e.KeyCode = Keys.Delete And btnsimpan.Enabled = True Then
            If GridView1.RowCount = 0 Then
                tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("qty"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"), GridView2.GetFocusedRowCellValue("barang_id"), GridView2.GetFocusedRowCellValue("stok_id"))
                GridView2.DeleteSelectedRows()
            Else
                For i As Integer = 0 To GridView1.RowCount - 1
                    If Val(GridView1.GetRowCellValue(i, "stok_id")).Equals(idstok) Then
                        lokasi = i
                    End If
                Next

                If lokasi > -1 Then
                    banyakjual = Val(GridView1.GetRowCellValue(lokasi, "qty"))
                    banyakretur = Val(GridView2.GetFocusedRowCellValue("qty"))

                    GridView1.SetRowCellValue(lokasi, "qty", Val(banyakjual) + Val(banyakretur))
                    GridView1.SetRowCellValue(lokasi, "subtotal", hargadiskon * (Val(banyakjual) + Val(banyakretur)))
                    GridView1.SetRowCellValue(lokasi, "laba", (hargadiskon - hargamodal) * (Val(banyakjual) + Val(banyakretur)))

                    GridControl1.RefreshDataSource()
                    GridView2.DeleteSelectedRows()

                    counting = True
                End If

                If counting.Equals(False) Then
                    tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("qty"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"), GridView2.GetFocusedRowCellValue("barang_id"), GridView2.GetFocusedRowCellValue("stok_id"))
                    GridView2.DeleteSelectedRows()
                End If
            End If

            If GridView2.RowCount.Equals(0) Then
                statusnonota(True)
            End If
        End If
    End Sub
End Class