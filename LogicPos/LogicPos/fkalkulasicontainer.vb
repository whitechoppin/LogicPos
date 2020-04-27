Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fkalkulasicontainer
    Dim hitnumber As Integer
    Public tabel As DataTable
    'variabel dalam penjualan
    Public kodeexpedisi As String
    Private Sub fkalkulasicontainer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        kodeexpedisi = currentnumber()
        Call inisialisasi(kodeexpedisi)
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("banyak").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "banyak", "{0:n0}")
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_expedisi,3) FROM tb_expedisi WHERE DATE_FORMAT(MID(`kode_expedisi`, 3 , 6), ' %y ')+ MONTH(MID(`kode_expedisi`,3 , 6)) + DAY(MID(`kode_expedisi`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_expedisi,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "EX" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "EX" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "EX" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "EX" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_expedisi FROM tb_expedisi ORDER BY kode_expedisi DESC LIMIT 1;"
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

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click

    End Sub

    Private Sub btncaripembelian_Click(sender As Object, e As EventArgs) Handles btncaridata.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

    End Sub

    Sub previewexpedisi(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_expedisi_detail WHERE kode_expedisi ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            'tabel.Rows.Add(dr("kode_barang"), dr("kode_stok"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), Val(dr("diskon")), Val(dr("harga_jual")) * Val(dr("diskon")) / 100, dr("harga_jual") - (Val(dr("harga_jual")) * Val(dr("diskon")) / 100), Val(dr("subtotal")), Val(dr("keuntungan")), Val(dr("modal")))
            GridControl1.RefreshDataSource()
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
        btngo.Enabled = False
        txtgodata.Enabled = False
        btncaridata.Enabled = False
        btnnext.Enabled = False

        'header
        txtnonota.Clear()
        txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbkodeexpedisi.Enabled = True
        cmbkodeexpedisi.SelectedIndex = 0
        cmbkodeexpedisi.Text = ""
        cmbkodeexpedisi.Focus()
        btncariexpedisi.Enabled = True

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

        cbprinted.Checked = False
        cbposted.Checked = False

        dtpengiriman.Enabled = True
        dtpengiriman.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0

        btntambahbarang.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()

        'buat tabel
        Call tabel_utama()

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
        txtgodata.Enabled = True
        btncaridata.Enabled = True
        btnnext.Enabled = True

        'header
        txtnonota.Clear()
        txtnonota.Text = ""
        txtnonota.Enabled = False

        cmbsales.Enabled = False

        dtpengiriman.Enabled = False
        dtpengiriman.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False
        btncaribarang.Enabled = False

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

        txthargabarang.Clear()
        txthargabarang.Text = 0

        btntambahbarang.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()

        If nomorkode IsNot "" Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_expedisi WHERE kode_expedisi = '" + nomorkode.ToString + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    'header
                    'nomornota = dr("kode_penjualan")
                    'nomorcustomer = dr("kode_pelanggan")
                    'nomorsales = dr("kode_user")

                    'statusprint = dr("print_penjualan")
                    'statusposted = dr("posted_penjualan")

                    'viewtglpenjualan = dr("tgl_penjualan")

                    'viewketerangan = dr("keterangan_penjualan")

                    'txtnonota.Text = nomornota
                    'cmbcustomer.Text = nomorcustomer
                    'cmbsales.Text = nomorsales
                    'cbprinted.Checked = statusprint
                    'cbposted.Checked = statusposted

                    'dtpenjualan.Value = viewtglpenjualan

                    ''isi tabel view pembelian

                    'Call previewexpedisi(nomorkode)

                    ''total tabel pembelian

                    'txtketerangan.Text = viewketerangan

                    cnn.Close()
                End If
            End Using
        Else
            txtnonota.Clear()
            cmbkodeexpedisi.Text = ""
            cmbsales.Text = ""
            cbprinted.Checked = False
            cbposted.Checked = False

            dtpengiriman.Value = Date.Now

            txtketerangan.Text = ""

        End If

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
        txtgodata.Enabled = False
        btncaridata.Enabled = False
        btnnext.Enabled = False

        'header
        'txtnonota.Clear()
        'txtnonota.Text = autonumber()
        txtnonota.Enabled = False

        cmbkodeexpedisi.Enabled = True
        'cmbcustomer.SelectedIndex = -1
        cmbkodeexpedisi.Focus()
        btncariexpedisi.Enabled = True

        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True


        dtpengiriman.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncaribarang.Enabled = True

        txtkodebarang.Clear()
        txtnamabarang.Clear()

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0

        btntambahbarang.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        'txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()

        'simpan di tabel sementara
        Call koneksii()

        'hapus di tabel jual sementara
        Call koneksii()
        sql = "DELETE FROM tb_penjualan_detail_sementara"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        'isi tabel sementara dengan data tabel detail
        sql = "INSERT INTO tb_penjualan_detail_sementara SELECT * FROM tb_penjualan_detail WHERE kode_penjualan ='" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

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
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon_persen", GetType(Double))
            .Columns.Add("diskon_nominal", GetType(Double))
            .Columns.Add("harga_diskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("laba", GetType(Double))
            .Columns.Add("modal_barang", GetType(Double))

        End With

        GridControl1.DataSource = tabel

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


    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyakbarang.Clear()
        txthargabarang.Text = 0
        txtkodebarang.Focus()
    End Sub

    Sub caripelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbkodeexpedisi.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamaexpedisi.Text = dr("nama_pelanggan")
        Else
            txtnamaexpedisi.Text = ""
        End If
    End Sub




    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click

    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click

    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkodeexpedisi.SelectedIndexChanged

    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged

    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged

    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click

    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click

    End Sub
End Class