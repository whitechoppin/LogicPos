Imports System.Data.Odbc
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid

Public Class freturjual
    Public tabel1, tabel2 As DataTable

    'variabel dalam penjualan
    Dim jenis, satuan, kodereturjual As String
    Dim banyak, totalbelanja, grandtotal, ongkir, diskonpersen, diskonnominal, ppnpersen, ppnnominal, modalpenjualan, bayar, sisa As Double
    'variabel bantuan view penjualan
    Dim nomorretur, nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan As String
    Dim statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglretur, viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double

    Private Sub freturjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
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
        sql = "SELECT kode_retur FROM tb_retur_penjualan WHERE date_created < (SELECT date_created FROM tb_retur_penjualan WHERE kode_retur = '" + previousnumber + "') ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT kode_retur FROM tb_retur_penjualan WHERE date_created > (SELECT date_created FROM tb_retur_penjualan WHERE kode_retur = '" + nextingnumber + "') ORDER BY date_created ASC LIMIT 1"
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

    Sub previewreturpenjualan(lihatjual As String, lihatretur As String)

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

        sql = "SELECT * FROM tb_retur_penjualan_detail WHERE kode_retur ='" & lihatjual & "'"
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
        btnedit.Enabled = False
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

        cmbsales.Enabled = True

        txtcustomer.Clear()
        txttelp.Clear()
        txtalamat.Clear()

        dtreturjual.Enabled = True
        dtreturjual.Value = Date.Now

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        GridControl2.Enabled = True
        GridView2.OptionsBehavior.Editable = False

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
        btnedit.Enabled = True
        btnbatal.Enabled = False

        'button navigations
        btnprev.Enabled = True
        btngoretur.Enabled = True
        txtgoretur.Enabled = True
        btnnext.Enabled = True

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbsales.Enabled = False

        cmbgudang.Enabled = False

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

                    statusvoid = dr("void_returjual")
                    statusprint = dr("print_returjual")
                    statusposted = dr("posted_returjual")

                    viewketerangan = dr("keterangan_returjual")

                    txtnoretur.Text = nomorretur
                    cmbsales.Text = nomorsales
                    txtnonota.Text = nomornota
                    dtreturjual.Value = viewtglretur

                    cbvoid.Checked = statusvoid
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

            cbvoid.Checked = False
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
        Call koneksii()
        Dim koderetur As String = autonumber()

        Call koneksii()
        cnn.Open()

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
        sql = "update tb_penjualan set total_penjualan = '" & total_penjualan & "', sisa_penjualan= '" & total_penjualan & "'-bayar_penjualan"
        'sql = "INSERT INTO tb_penjualan (kode_penjualan, kode_pelanggan, kode_gudang, kode_user, tgl_penjualan, tgl_jatuhtempo_penjualan, lunas_penjualan, void_penjualan, print_penjualan, posted_penjualan, keterangan_penjualan, diskon_penjualan, pajak_penjualan, ongkir_penjualan, total_penjualan, metode_pembayaran, rekening, bayar_penjualan, sisa_penjualan, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepenjualan & "','" & cmbcustomer.Text & "','" & cmbgudang.Text & "','" & cmbsales.Text & "' , '" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & txtdiskonpersen.Text & "','" & txtppnpersen.Text & "','" & ongkir & "','" & grandtotal & "','" & cmbpembayaran.Text & "', '" & txtrekening.Text & "','" & bayar & "','" & sisa & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        For i As Integer = 0 To GridView2.RowCount - 1
            sql = "INSERT INTO tb_retur_penjualan_detail ( kode_retur, kode_stok, kode_barang, nama_barang, satuan_barang, jenis_barang, qty, harga_jual, diskon, harga_diskon, subtotal, modal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & koderetur & "', '" & GridView2.GetRowCellValue(i, "kode_stok") & "', '" & GridView2.GetRowCellValue(i, "kode_barang") & "', '" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "banyak") & "','" & GridView2.GetRowCellValue(i, "harga_satuan") & "','" & GridView2.GetRowCellValue(i, "diskon_persen") & "','" & GridView2.GetRowCellValue(i, "diskon_nominal") & "' ,'" & GridView2.GetRowCellValue(i, "subtotal") & "','" & GridView2.GetRowCellValue(i, "modal_barang") & "','" & GridView2.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        Dim total_retur As Double = GridView2.Columns("subtotal").SummaryItem.SummaryValue
        sql = "INSERT INTO tb_retur_penjualan (kode_retur,total_retur ,created_by, updated_by, date_created, last_updated) VALUES ('" & koderetur & "','" & total_retur & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        MsgBox("Retur Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
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
        Call awalbaru()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        'If btnedit.Text.Equals("Edit") Then
        '    btnedit.Text = "Update"
        '    Call awaledit()

        'ElseIf btnedit.Text.Equals("Update") Then
        '    If GridView1.DataRowCount > 0 Then
        '        If txtcustomer.Text IsNot "" Then
        '            If txtgudang.Text IsNot "" Then
        '                If cmbsales.Text IsNot "" Then
        '                    If txtrekening.Text IsNot "" Then

        '                        'isi disini sub updatenya
        '                        Call perbarui(txtnonota.Text)
        '                        'Call inisialisasi(txtnonota.Text)
        '                    Else
        '                        MsgBox("Isi Pembayaran")
        '                    End If
        '                Else
        '                    MsgBox("Isi Sales")
        '                End If
        '            Else
        '                MsgBox("Isi Gudang")
        '            End If
        '        Else
        '            MsgBox("Isi Customer")
        '        End If
        '    Else
        '        MsgBox("Keranjang Masih Kosong")
        '    End If
        'End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(kodereturjual)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnoretur.Text)
        End If
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

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

        Dim ea As DXMouseEventArgs = TryCast(e, DXMouseEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = view.CalcHitInfo(ea.Location)

        If info.InRow OrElse info.InRowCell Then
            Dim colCaption As String = If(info.Column Is Nothing, "N/A", info.Column.GetCaption())
            'MessageBox.Show(String.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption))
        End If
        'GridView1.DeleteRow(GridView1.GetRowHandle(info.RowHandle))
        fretjual.ShowDialog()
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        'Dim kode_stok As String = GridView1.GetFocusedRowCellValue("kode_stok")
        Dim kode_stok2 As String = GridView2.GetFocusedRowCellValue("kode_stok")
        Dim banyak2 As Integer = GridView2.GetFocusedRowCellValue("banyak")

        Dim hargadiskon As Integer = GridView1.GetFocusedRowCellValue("harga_diskon")
        'MsgBox(kode_stok2)
        Dim lokasi As Integer
        Dim counting As Boolean = False

        If e.KeyCode = Keys.Delete Then
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
                                'Exit Sub
                                counting = True
                            End If
                        Next
                    Else
                        'tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("banyak"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"))
                        'GridView2.DeleteSelectedRows()
                    End If
                Next

                If counting = False Then
                    tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("banyak"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"))
                    GridView2.DeleteSelectedRows()
                End If

                'For i As Integer = 0 To GridView1.RowCount - 1
                '    If GridView1.GetRowCellValue(i, "kode_stok") <> kode_stok2 Then

                '        'Dim banyak1 As Integer = GridView2.GetFocusedRowCellValue("banyak")
                '        'GridView1.SetRowCellValue(lokasi1, "banyak", Val(banyak) + Val(banyak1))
                '        tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("banyak"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"))
                '        GridView2.DeleteSelectedRows()
                '        'Exit Sub
                '    End If
                'Next
            End If
        End If
    End Sub
End Class