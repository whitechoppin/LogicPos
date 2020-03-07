Imports System.Data.Odbc
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports CrystalDecisions.CrystalReports.Engine

Public Class freturjual
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel1, tabel2 As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Dim jenis, satuan, kodereturjual As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double
    'variabel bantuan view penjualan
    Dim nomorretur, nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan As String
    Dim statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglretur, viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    Dim rpt_faktur As New ReportDocument

    Private Sub freturjual_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub freturjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        hitnumber = 0
        kodereturjual = currentnumber()
        Call inisialisasi(kodereturjual)

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With
        With GridView2
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
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
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_retur,3) FROM tb_retur_penjualan WHERE DATE_FORMAT(MID(`kode_retur`, 3 , 6), ' %y ')+ MONTH(MID(`kode_retur`,3 , 6)) + DAY(MID(`kode_retur`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_retur,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "RJ" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "RJ" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "RJ" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "RJ" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_retur FROM tb_retur_penjualan ORDER BY kode_retur DESC LIMIT 1;"
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
        sql = "SELECT kode_retur FROM tb_retur_penjualan WHERE date_created < (SELECT date_created FROM tb_retur_penjualan WHERE kode_retur = '" + previousnumber + "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
            cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_retur FROM tb_retur_penjualan WHERE date_created > (SELECT date_created FROM tb_retur_penjualan WHERE kode_retur = '" + nextingnumber + "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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
            cnn.Close()
        End Try
    End Sub
    Sub previewreturpenjualan(lihatjual As String, lihatretur As String)
        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan ='" & lihatjual & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            nomorcustomer = dr("kode_pelanggan")
            nomorgudang = dr("kode_gudang")
            viewtglpenjualan = dr("tgl_penjualan")
            viewtgljatuhtempo = dr("tgl_jatuhtempo_penjualan")

            cmbgudang.Text = nomorgudang
            dtpenjualan.Value = viewtglpenjualan
            dtjatuhtempo.Value = viewtgljatuhtempo
        End While

        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan ='" & nomorcustomer & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            txtcustomer.Text = dr("nama_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
            txtalamat.Text = dr("alamat_pelanggan")
        End While

        sql = "SELECT * FROM tb_penjualan_detail WHERE kode_penjualan ='" & lihatjual & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - dr("diskon") / 100, Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")))
            GridControl1.RefreshDataSource()
        End While

        sql = "SELECT * FROM tb_retur_penjualan_detail WHERE kode_retur ='" & lihatretur & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel2.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - dr("diskon") / 100, Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")))
            GridControl2.RefreshDataSource()
        End While

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
        'btnedit.Enabled = False
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngoretur.Enabled = False
        txtgoretur.Enabled = False
        btnnext.Enabled = False

        'header
        txtnoretur.Clear()
        txtnoretur.Text = autonumber()
        txtnoretur.Enabled = False

        txtnonota.Clear()
        txtnonota.Enabled = True
        btncarinota.Enabled = True
        btngo.Enabled = True

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

        Call comboboxuser()
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
        btnnext.Enabled = True

        'header
        txtnoretur.Clear()
        txtnoretur.Text = autonumber()
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
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_retur_penjualan WHERE kode_retur = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomorretur = dr("kode_retur")
                    nomorsales = dr("kode_user")
                    nomornota = dr("kode_penjualan")
                    viewtglretur = dr("tgl_returjual")

                    statusprint = dr("print_returjual")
                    statusposted = dr("posted_returjual")

                    viewketerangan = dr("keterangan_returjual")

                    txtnoretur.Text = nomorretur
                    cmbsales.Text = nomorsales
                    txtnonota.Text = nomornota
                    dtreturjual.Value = viewtglretur

                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    'isi tabel view pembelian

                    Call previewreturpenjualan(nomornota, nomorkode)

                    'total tabel pembelian

                    txtketerangan.Text = viewketerangan

                    cnn.Close()
                End If
            End Using
        Else
            txtnoretur.Clear()
            txtnonota.Clear()
            dtreturjual.Value = Date.Now
            cmbsales.Text = ""

            cbprinted.Checked = False
            cbposted.Checked = False

            txtcustomer.Clear()
            txttelp.Clear()
            txtalamat.Clear()

            dtpenjualan.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            cmbgudang.Text = ""

            txtketerangan.Text = ""
        End If

    End Sub
    Sub tabel_utama()
        tabel1 = New DataTable

        With tabel1
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))
        End With

        GridControl1.DataSource = tabel1

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

        GridColumn7.FieldName = "harga_satuan"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 20

        GridColumn8.FieldName = "diskon_persen"
        GridColumn8.Caption = "Diskon %"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 20

        GridColumn9.FieldName = "diskon_nominal"
        GridColumn9.Caption = "Diskon Nominal"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "harga_diskon"
        GridColumn10.Caption = "Harga Diskon"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

        GridColumn11.FieldName = "subtotal"
        GridColumn11.Caption = "Subtotal"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:n0}"
        GridColumn11.Width = 30

        GridColumn12.FieldName = "laba"
        GridColumn12.Caption = "Laba"
        GridColumn12.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn12.DisplayFormat.FormatString = "{0:n0}"
        GridColumn12.Width = 20
        GridColumn12.Visible = False

        GridColumn13.FieldName = "modal_barang"
        GridColumn13.Caption = "Modal Barang"
        GridColumn13.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn13.DisplayFormat.FormatString = "{0:n0}"
        GridColumn13.Width = 20
        GridColumn13.Visible = False

    End Sub
    Sub tabel_retur()
        tabel2 = New DataTable

        With tabel2
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))

        End With

        GridControl2.DataSource = tabel2

        GridColumn14.FieldName = "kode_barang"
        GridColumn14.Caption = "Kode Barang"
        GridColumn14.Width = 20

        GridColumn15.FieldName = "kode_stok"
        GridColumn15.Caption = "Kode Stok"
        GridColumn15.Width = 20

        GridColumn16.FieldName = "nama_barang"
        GridColumn16.Caption = "Nama Barang"
        GridColumn16.Width = 70

        GridColumn17.FieldName = "banyak"
        GridColumn17.Caption = "banyak"
        GridColumn17.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn17.DisplayFormat.FormatString = "{0:n0}"
        GridColumn17.Width = 5

        GridColumn18.FieldName = "satuan_barang"
        GridColumn18.Caption = "Satuan Barang"
        GridColumn18.Width = 10

        GridColumn19.FieldName = "jenis_barang"
        GridColumn19.Caption = "Jenis Barang"
        GridColumn19.Width = 10

        GridColumn20.FieldName = "harga_satuan"
        GridColumn20.Caption = "Harga Satuan"
        GridColumn20.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn20.DisplayFormat.FormatString = "{0:n0}"
        GridColumn20.Width = 20

        GridColumn21.FieldName = "diskon_persen"
        GridColumn21.Caption = "Diskon %"
        GridColumn21.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn21.DisplayFormat.FormatString = "{0:n0}"
        GridColumn21.Width = 20

        GridColumn22.FieldName = "diskon_nominal"
        GridColumn22.Caption = "Diskon Nominal"
        GridColumn22.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn22.DisplayFormat.FormatString = "{0:n0}"
        GridColumn22.Width = 30

        GridColumn23.FieldName = "harga_diskon"
        GridColumn23.Caption = "Harga Diskon"
        GridColumn23.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn23.DisplayFormat.FormatString = "{0:n0}"
        GridColumn23.Width = 30

        GridColumn24.FieldName = "subtotal"
        GridColumn24.Caption = "Subtotal"
        GridColumn24.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn24.DisplayFormat.FormatString = "{0:n0}"
        GridColumn24.Width = 30

        GridColumn25.FieldName = "laba"
        GridColumn25.Caption = "Laba"
        GridColumn25.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn25.DisplayFormat.FormatString = "{0:n0}"
        GridColumn25.Width = 20
        GridColumn25.Visible = False

        GridColumn26.FieldName = "modal_barang"
        GridColumn26.Caption = "Modal Barang"
        GridColumn26.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn26.DisplayFormat.FormatString = "{0:n0}"
        GridColumn26.Width = 20
        GridColumn26.Visible = False
    End Sub
    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtcustomer.Clear()
    End Sub
    Sub loadingpenjualan(lihat As String)
        Call tabel_utama()
        Call tabel_retur()
        sql = "SELECT * FROM tb_penjualan_detail WHERE kode_penjualan ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")))
            'tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), dr("diskon"), 0, dr("harga_diskon"), dr("subtotal"), 0, 0)
            GridControl1.RefreshDataSource()
        End While

    End Sub
    Sub simpan()
        Dim koderetur As String = autonumber()

        Call koneksii()
        For i As Integer = 0 To GridView2.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView2.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView2.GetRowCellValue(i, "kode_stok") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "DELETE FROM tb_penjualan_detail WHERE kode_penjualan = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "INSERT INTO tb_penjualan_detail ( kode_penjualan, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & txtnonota.Text & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & GridView1.GetRowCellValue(i, "diskon_persen") & "','" & GridView1.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "modal_barang") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        Dim total_penjualan As Double = GridView1.Columns("subtotal").SummaryItem.SummaryValue
        Dim statusvoid As Integer = 0

        If GridView1.DataRowCount = 0 Then
            statusvoid = 1
        End If

        sql = "UPDATE tb_penjualan SET total_penjualan = '" & total_penjualan & "', sisa_penjualan = '" & total_penjualan & "'- bayar_penjualan, void_penjualan ='" & statusvoid & "' WHERE kode_penjualan ='" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        For i As Integer = 0 To GridView2.RowCount - 1
            sql = "INSERT INTO tb_retur_penjualan_detail (kode_retur, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & koderetur & "', '" & GridView2.GetRowCellValue(i, "kode_stok") & "', '" & GridView2.GetRowCellValue(i, "kode_barang") & "', '" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "banyak") & "','" & GridView2.GetRowCellValue(i, "harga_satuan") & "','" & GridView2.GetRowCellValue(i, "diskon_persen") & "','" & GridView2.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView2.GetRowCellValue(i, "subtotal") & "','" & GridView2.GetRowCellValue(i, "modal_barang") & "','" & GridView2.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        Dim total_retur As Double = GridView2.Columns("subtotal").SummaryItem.SummaryValue
        sql = "INSERT INTO tb_retur_penjualan (kode_retur, kode_user, kode_penjualan, tgl_returjual, print_returjual, posted_returjual, keterangan_returjual, total_retur, created_by, updated_by, date_created, last_updated) VALUES ('" & koderetur & "','" & cmbsales.Text & "','" & txtnonota.Text & "','" & Format(dtreturjual.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & total_retur & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        MsgBox("Retur Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
        Call inisialisasi(kodereturjual)
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click

        If GridView2.DataRowCount > 0 Then
            If txtnoretur.Text IsNot "" Then
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
                MsgBox("Isi No Nota")
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
            Call cetak_faktur()

            sql = "UPDATE tb_retur_penjualan SET print_returjual = 1 WHERE kode_retur = '" & txtnonota.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Public Sub cetak_faktur()
        Dim tabel_lama As New DataTable
        With tabel_lama
            .Columns.Add("kode_barang1")
            .Columns.Add("kode_stok1")
            .Columns.Add("nama_barang1")
            .Columns.Add("banyak1", GetType(Double))
            .Columns.Add("satuan1")
            .Columns.Add("jenis1")
            .Columns.Add("harga_satuan1", GetType(Double))
            .Columns.Add("diskon_persen1", GetType(Double))
            .Columns.Add("harga_diskon1", GetType(Double))
            .Columns.Add("subtotal1", GetType(Double))
            .Columns.Add("diskon_nominal1", GetType(Double))
        End With

        Dim baris As DataRow
        For a As Integer = 0 To GridView2.RowCount - 1
            baris = tabel_lama.NewRow
            baris("kode_barang1") = GridView2.GetRowCellValue(a, "kode_barang")
            baris("kode_stok1") = GridView2.GetRowCellValue(a, "kode_stok")
            baris("nama_barang1") = GridView2.GetRowCellValue(a, "nama_barang")
            baris("banyak1") = GridView2.GetRowCellValue(a, "banyak")
            baris("satuan1") = GridView2.GetRowCellValue(a, "satuan_barang")
            baris("jenis1") = GridView2.GetRowCellValue(a, "jenis_barang")
            baris("harga_satuan1") = GridView2.GetRowCellValue(a, "harga_satuan")
            baris("diskon_persen1") = GridView2.GetRowCellValue(a, "diskon_persen")
            baris("harga_diskon1") = GridView2.GetRowCellValue(a, "harga_diskon")
            baris("subtotal1") = GridView2.GetRowCellValue(a, "subtotal")
            baris("diskon_nominal1") = GridView2.GetRowCellValue(a, "diskon_nominal")
            tabel_lama.Rows.Add(baris)
        Next

        rpt_faktur = New fakturreturpenjualan
        rpt_faktur.SetDataSource(tabel_lama)

        rpt_faktur.SetParameterValue("nofaktur", txtnoretur.Text)
        rpt_faktur.SetParameterValue("namakasir", fmenu.statususer.Text)
        rpt_faktur.SetParameterValue("pembeli", txtcustomer.Text)
        rpt_faktur.SetParameterValue("jatem", dtjatuhtempo.Text)
        rpt_faktur.SetParameterValue("alamat", txtalamat.Text)
        rpt_faktur.SetParameterValue("tanggal", dtreturjual.Text)
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
        Call inisialisasi(kodereturjual)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnoretur.Text)
    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click
        If txtgoretur.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_retur_penjualan WHERE kode_retur = '" + txtgoretur.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                Call inisialisasi(txtgoretur.Text)
            Else
                MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
            End If
        End If
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
        sql = "SELECT * FROM tb_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan = tb_penjualan.kode_pelanggan WHERE tb_penjualan.kode_penjualan = '" & txtnonota.Text & "'"
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
            cmbgudang.Text = dr("kode_gudang")

            Call loadingpenjualan(txtnonota.Text)
        Else
            'jika tidak ditemukan

            txtcustomer.Text = ""
            txttelp.Text = ""
            txtalamat.Text = ""

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
            fretjual.banyak = GridView1.GetFocusedRowCellValue("banyak")
            fretjual.harga_satuan = GridView1.GetFocusedRowCellValue("harga_satuan")
            fretjual.diskon_persen = GridView1.GetFocusedRowCellValue("diskon_persen")
            fretjual.diskon_nominal = GridView1.GetFocusedRowCellValue("diskon_nominal")
            fretjual.harga_diskon = GridView1.GetFocusedRowCellValue("harga_diskon")
            fretjual.subtotal = GridView1.GetFocusedRowCellValue("subtotal")
            fretjual.laba = GridView1.GetFocusedRowCellValue("laba")
            fretjual.modal_barang = GridView1.GetFocusedRowCellValue("modal_barang")

            fretjual.ShowDialog()
        End If
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Dim kode_stok As String = GridView1.GetFocusedRowCellValue("kode_stok")
        Dim kode_stok2 As String = GridView2.GetFocusedRowCellValue("kode_stok")
        Dim banyak2 As Integer = GridView2.GetFocusedRowCellValue("banyak")

        Dim hargadiskon As Integer = GridView1.GetFocusedRowCellValue("harga_diskon")
        'MsgBox(kode_stok2)
        Dim lokasi As Integer
        Dim counting As Boolean = False

        If e.KeyCode = Keys.Delete And btnsimpan.Enabled = True Then
            If GridView1.RowCount = 0 Then
                tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("banyak"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"))
                GridView2.DeleteSelectedRows()
            Else
                counting = False

                For i As Integer = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "kode_stok").Equals(kode_stok2) Then
                        For a As Integer = 0 To GridView1.RowCount - 1
                            Dim banyak As Integer = GridView1.GetRowCellValue(a, "banyak")
                            If GridView1.GetRowCellValue(a, "kode_stok") = kode_stok2 Then
                                lokasi = a
                                Dim banyak1 As Integer = GridView2.GetFocusedRowCellValue("banyak")
                                GridView1.SetRowCellValue(lokasi, "banyak", Val(banyak) + Val(banyak1))
                                GridView1.SetRowCellValue(lokasi, "subtotal", hargadiskon * (Val(banyak) + Val(banyak1)))

                                GridControl1.RefreshDataSource()
                                GridView2.DeleteSelectedRows()

                                counting = True
                            End If
                        Next
                    End If
                Next

                If counting = False Then
                    tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("banyak"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"))
                    GridView2.DeleteSelectedRows()
                End If
            End If
        End If
    End Sub
End Class