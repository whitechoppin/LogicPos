Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid

Public Class freturbeli
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, printstatus As Boolean
    Public tabel1, tabel2 As DataTable
    Dim hitnumber As Integer
    'variabel dalam penjualan
    Dim jenis, satuan, kodereturbeli As String
    Dim banyak As Double
    Dim rpt_faktur As New ReportDocument
    'variabel bantuan view pembelian
    Dim nomorretur, nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglretur, viewtglpembelian, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double

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
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        hitnumber = 0
        kodereturbeli = currentnumber()
        Call inisialisasi(kodereturbeli)

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

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_retur,3) FROM tb_retur_pembelian WHERE DATE_FORMAT(MID(`kode_retur`, 3 , 6), ' %y ')+ MONTH(MID(`kode_retur`,3 , 6)) + DAY(MID(`kode_retur`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_retur,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "RB" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "RB" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "RB" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "RB" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            'cnn.Close()
        End Try
        Return pesan
    End Function

    Function currentnumber()
        Call koneksii()
        sql = "SELECT kode_retur FROM tb_retur_pembelian ORDER BY kode_retur DESC LIMIT 1;"
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
            'cnn.Close()
        End Try
        Return pesan
    End Function

    Private Sub prevnumber(previousnumber As String)
        Call koneksii()
        sql = "SELECT kode_retur FROM tb_retur_pembelian WHERE date_created < (SELECT date_created FROM tb_retur_pembelian WHERE kode_retur = '" + previousnumber + "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
            'cnn.Close()
        End Try
    End Sub
    Private Sub nextnumber(nextingnumber As String)
        Call koneksii()
        sql = "SELECT kode_retur FROM tb_retur_pembelian WHERE date_created > (SELECT date_created FROM tb_retur_pembelian WHERE kode_retur = '" + nextingnumber + "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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
            'cnn.Close()
        End Try
    End Sub

    Sub previewreturpembelian(lihatbeli As String, lihatretur As String)
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian WHERE kode_pembelian ='" & lihatbeli & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            nomorsupplier = dr("kode_supplier")
            nomorgudang = dr("kode_gudang")
            viewtglpembelian = dr("tgl_pembelian")
            viewtgljatuhtempo = dr("tgl_jatuhtempo_pembelian")

            cmbgudang.Text = nomorgudang
            dtpembelian.Value = viewtglpembelian
            dtjatuhtempo.Value = viewtgljatuhtempo
        End While

        sql = "SELECT * FROM tb_supplier WHERE kode_supplier ='" & nomorsupplier & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            txtsupplier.Text = dr("nama_supplier")
            txttelp.Text = dr("telepon_supplier")
            txtalamat.Text = dr("alamat_supplier")
        End While

        sql = "SELECT * FROM tb_pembelian_detail WHERE kode_pembelian ='" & lihatbeli & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")))
            'GridControl1.RefreshDataSource()
        End While
        GridControl1.RefreshDataSource()

        sql = "SELECT * FROM tb_retur_pembelian_detail WHERE kode_retur ='" & lihatretur & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel2.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")))
            'GridControl2.RefreshDataSource()
        End While
        GridControl2.RefreshDataSource()

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
        btnbatal.Enabled = True

        'button navigations
        btnprev.Enabled = False
        btngoretur.Enabled = False
        txtgoretur.Enabled = False
        btncariretur.Enabled = False
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
        btncariretur.Enabled = True
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

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_retur_pembelian WHERE kode_retur = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    nomorretur = dr("kode_retur")
                    nomorsales = dr("kode_user")
                    nomornota = dr("kode_pembelian")
                    viewtglretur = dr("tgl_returbeli")

                    statusprint = dr("print_returbeli")
                    statusposted = dr("posted_returbeli")

                    viewketerangan = dr("keterangan_returbeli")

                    txtnoretur.Text = nomorretur
                    cmbsales.Text = nomorsales
                    txtnonota.Text = nomornota
                    dtreturbeli.Value = viewtglretur

                    cbprinted.Checked = statusprint
                    cbposted.Checked = statusposted

                    'isi tabel view pembelian

                    Call previewreturpembelian(nomornota, nomorkode)

                    'total tabel pembelian

                    txtketerangan.Text = viewketerangan

                    cnn.Close()
                End If
            End Using
        Else
            txtnoretur.Clear()
            txtnonota.Clear()
            dtreturbeli.Value = Date.Now
            cmbsales.Text = ""

            cbprinted.Checked = False
            cbposted.Checked = False

            txtsupplier.Clear()
            txttelp.Clear()
            txtalamat.Clear()

            dtpembelian.Value = Date.Now
            dtjatuhtempo.Value = Date.Now

            cmbgudang.Text = ""

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

        End With
        GridControl1.DataSource = tabel1

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

        GridColumn7.FieldName = "harga_beli"
        GridColumn7.Caption = "Harga Satuan"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 50

        GridColumn8.FieldName = "subtotal"
        GridColumn8.Caption = "Subtotal"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 55

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

        End With
        GridControl2.DataSource = tabel2

        GridColumn9.FieldName = "kode_stok"
        GridColumn9.Caption = "Kode Stok"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "kode_barang"
        GridColumn10.Caption = "Kode Barang"
        GridColumn10.Visible = False

        GridColumn11.FieldName = "nama_barang"
        GridColumn11.Caption = "Nama Barang"
        GridColumn11.Width = 70

        GridColumn12.Caption = "Qty"
        GridColumn12.FieldName = "qty"
        GridColumn12.Width = 20

        GridColumn13.FieldName = "satuan_barang"
        GridColumn13.Caption = "Satuan Barang"
        GridColumn13.Width = 30

        GridColumn14.FieldName = "jenis_barang"
        GridColumn14.Caption = "Jenis Barang"
        GridColumn14.Width = 30

        GridColumn15.FieldName = "harga_beli"
        GridColumn15.Caption = "Harga Satuan"
        GridColumn15.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn15.DisplayFormat.FormatString = "{0:n0}"
        GridColumn15.Width = 50

        GridColumn16.FieldName = "subtotal"
        GridColumn16.Caption = "Subtotal"
        GridColumn16.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn16.DisplayFormat.FormatString = "{0:n0}"
        GridColumn16.Width = 55
    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtnonota.Clear()
        txtsupplier.Clear()
    End Sub
    Sub loadingpembelian(lihat As String)
        Call tabel_utama()
        Call tabel_retur()
        sql = "SELECT * FROM tb_pembelian_detail WHERE kode_pembelian ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")))
            'tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), dr("diskon"), 0, dr("harga_diskon"), dr("subtotal"), 0, 0)
            'GridControl1.RefreshDataSource()
        End While
        GridControl1.RefreshDataSource()
    End Sub
    Sub cari_nota()
        Call koneksii()
        sql = "SELECT * FROM tb_pembelian JOIN tb_supplier ON tb_supplier.kode_supplier = tb_pembelian.kode_supplier WHERE tb_pembelian.kode_pembelian = '" & txtnonota.Text & "'"
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
            cmbgudang.Text = dr("kode_gudang")

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
        kodereturbeli = autonumber()
        'ambil total retur dan total penjualan
        Dim total_retur As Double = GridView2.Columns("subtotal").SummaryItem.SummaryValue
        Dim total_pembelian As Double = GridView1.Columns("subtotal").SummaryItem.SummaryValue
        Dim statusvoid As Integer = 0

        Call koneksii()

        Dim myCommand As OdbcCommand = cnnx.CreateCommand()
        Dim myTrans As OdbcTransaction

        ' Start a local transaction
        myTrans = cnnx.BeginTransaction()
        ' Must assign both transaction object and connection
        ' to Command object for a pending local transaction
        myCommand.Connection = cnnx
        myCommand.Transaction = myTrans

        Try
            For i As Integer = 0 To GridView2.RowCount - 1
                myCommand.CommandText = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView2.GetRowCellValue(i, "qty") & "' WHERE kode_stok = '" & GridView2.GetRowCellValue(i, "kode_stok") & "'"
                myCommand.ExecuteNonQuery()
            Next

            myCommand.CommandText = "DELETE FROM tb_pembelian_detail WHERE kode_pembelian = '" & txtnonota.Text & "'"
            myCommand.ExecuteNonQuery()

            For i As Integer = 0 To GridView1.RowCount - 1
                myCommand.CommandText = "INSERT INTO tb_pembelian_detail ( kode_pembelian, kode_barang, kode_stok, nama_barang,  jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by,date_created, last_updated) VALUES ('" & txtnonota.Text & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "harga_beli") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next

            If GridView1.DataRowCount = 0 Then
                statusvoid = 1
            End If

            Call koneksii()
            myCommand.CommandText = "UPDATE tb_pembelian SET total_pembelian = '" & total_pembelian & "', void_pembelian ='" & statusvoid & "' WHERE kode_pembelian ='" & txtnonota.Text & "'"
            myCommand.ExecuteNonQuery()

            For i As Integer = 0 To GridView2.RowCount - 1
                myCommand.CommandText = "INSERT INTO tb_retur_pembelian_detail (kode_retur, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by, date_created, last_updated) VALUES ('" & kodereturbeli & "','" & GridView2.GetRowCellValue(i, "kode_barang") & "','" & GridView2.GetRowCellValue(i, "kode_stok") & "','" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "qty") & "','" & GridView2.GetRowCellValue(i, "harga_beli") & "','" & GridView2.GetRowCellValue(i, "subtotal") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
                myCommand.ExecuteNonQuery()
            Next


            myCommand.CommandText = "INSERT INTO tb_retur_pembelian (kode_retur, kode_user, kode_pembelian, tgl_returbeli, print_returbeli, posted_returbeli, keterangan_returbeli, total_retur, created_by, updated_by, date_created, last_updated) VALUES ('" & kodereturbeli & "','" & cmbsales.Text & "','" & txtnonota.Text & "','" & Format(dtreturbeli.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & total_retur & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "', now() , now())"
            myCommand.ExecuteNonQuery()

            myTrans.Commit()
            Console.WriteLine("Both records are written to database.")

            'history user ==========
            Call historysave("Menyimpan Data Retur Beli Kode " + kodereturbeli, kodereturbeli)
            '========================
            MsgBox("Retur Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
            Call inisialisasi(kodereturbeli)
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

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            Call cetak_faktur()
            Call koneksii()
            sql = "UPDATE tb_retur_pembelian SET print_returbeli = 1 WHERE kode_retur = '" & txtnonota.Text & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()

            'history user ==========
            Call historysave("Mencetak Data Retur Beli Kode " + txtnonota.Text, txtnonota.Text)
            '========================

            cbprinted.Checked = True
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Public Sub cetak_faktur()
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
        rpt_faktur.SetDataSource(tabel_lama)

        rpt_faktur.SetParameterValue("nofaktur", txtnoretur.Text)
        rpt_faktur.SetParameterValue("namakasir", fmenu.statususer.Text)
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
        Call inisialisasi(kodereturbeli)
    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click
        Call prevnumber(txtnoretur.Text)
    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click
        If txtgoretur.Text = "" Then
            MsgBox("Transaksi Tidak Ditemukan !", MsgBoxStyle.Information, "Gagal")
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_retur_pembelian WHERE kode_retur = '" + txtgoretur.Text + "'"
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

            fretbeli.ShowDialog()
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
                            Dim banyak As Integer = GridView1.GetRowCellValue(a, "qty")
                            If GridView1.GetRowCellValue(a, "kode_stok") = kode_stok2 Then
                                lokasi = a
                                Dim banyak1 As Integer = GridView2.GetFocusedRowCellValue("qty")
                                GridView1.SetRowCellValue(lokasi, "qty", Val(banyak) + Val(banyak1))
                                GridView1.SetRowCellValue(lokasi, "subtotal", (Val(banyak) + Val(banyak1)) * GridView1.GetRowCellValue(lokasi, "harga_beli"))

                                GridControl1.RefreshDataSource()
                                GridView2.DeleteSelectedRows()

                                counting = True
                            End If
                        Next
                    End If
                Next

                If counting = False Then
                    tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("qty"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_beli"), GridView2.GetFocusedRowCellValue("subtotal"))
                    GridView2.DeleteSelectedRows()
                End If
            End If
        End If
    End Sub
End Class