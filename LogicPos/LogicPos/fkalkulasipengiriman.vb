Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class fkalkulasipengiriman
    Dim hitnumber As Integer
    Public tabel As DataTable
    'variabel dalam expedisi
    Public kodepengiriman As String
    Dim totalongkospengiriman, hargabarang, panjangbarang, lebarbarang, tinggibarang, volumebarang, banyakbarang, totalvolumebarang, ongkirbarang, totalongkirbarang, totalhargabarang, grandtotalbarang, grandtotalvolumebarang As Double

    'variabel bantuan view pengiriman
    Dim nomorpengiriman, nomoruser, namaexpedisi, alamatexpedisi, telpexpedisi, viewketerangan As String
    Dim statusprint, statusposted, statusedit As Boolean
    Dim viewtglpengiriman As DateTime
    Dim viewtotalpengiriman As Double

    'variabel bantuan hitung gridview
    Dim ongkirbarangulang, totalongkirbarangulang, totalhargabarangulang, grandtotalbarangulang As Double

    Dim summarytotalvolumebefore, volumebarangbefore, qtybarangbefore, totalvolumebarangbefore As Double

    Dim rpt_faktur As New ReportDocument

    Private Sub fkalkulasipengiriman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        kodepengiriman = currentnumber()
        Call inisialisasi(kodepengiriman)

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ongkos_kirim", "{0:n0}")
            .Columns("total_ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_ongkos_kirim", "{0:n0}")
            .Columns("total_harga_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_harga_barang", "{0:n0}")
            .Columns("grand_total_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "grand_total_barang", "{0:n0}")
        End With
    End Sub

    Sub comboboxuser()
        Call koneksii()
        cmbuser.Items.Clear()
        cmbuser.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_user", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbuser.AutoCompleteCustomSource.Add(dr("kode_user"))
                cmbuser.Items.Add(dr("kode_user"))
            End While
        End If
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_kirim,3) FROM tb_kirim WHERE DATE_FORMAT(MID(`kode_kirim`, 3 , 6), ' %y ')+ MONTH(MID(`kode_kirim`,3 , 6)) + DAY(MID(`kode_kirim`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_kirim,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "KM" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "KM" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "KM" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "KM" + Format(Now.Date, "yyMMdd") + "001"
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
        sql = "SELECT kode_kirim FROM tb_kirim ORDER BY kode_kirim DESC LIMIT 1;"
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
        sql = "SELECT kode_kirim FROM tb_kirim WHERE date_created < (SELECT date_created FROM tb_kirim WHERE kode_kirim = '" + previousnumber + "' LIMIT 1) ORDER BY date_created DESC LIMIT 1"
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
        sql = "SELECT kode_kirim FROM tb_kirim WHERE date_created > (SELECT date_created FROM tb_kirim WHERE kode_kirim = '" + nextingnumber + "' LIMIT 1) ORDER BY date_created ASC LIMIT 1"
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

        cmbuser.SelectedIndex = 0
        cmbuser.Enabled = True

        txtnamaexpedisi.Clear()
        txtnamaexpedisi.Enabled = True

        txtalamatexpedisi.Clear()
        txtalamatexpedisi.Enabled = True

        txttelpexpedisi.Clear()
        txttelpexpedisi.Enabled = True

        cbprinted.Checked = False
        cbposted.Checked = False

        dtpengiriman.Enabled = True
        dtpengiriman.Value = Date.Now

        txttotalongkir.Clear()
        txttotalongkir.Text = 0
        txttotalongkir.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True
        btnrefresh.Enabled = True

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

    Sub previewpengiriman(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_kirim_detail WHERE kode_kirim ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("nama_barang"), Val(dr("qty")), Val(dr("harga_barang")), Val(dr("ongkos_kirim")), Val(dr("total_ongkos_kirim")), Val(dr("total_harga_barang")), Val(dr("grand_total_barang")))
            GridControl1.RefreshDataSource()
        End While
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

        cmbuser.Enabled = False

        txtnamaexpedisi.Enabled = False
        txtalamatexpedisi.Enabled = False
        txttelpexpedisi.Enabled = False

        txttotalongkir.Enabled = False

        dtpengiriman.Enabled = False
        dtpengiriman.Value = Date.Now

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = False

        btncaribarang.Enabled = False

        txtnamabarang.Clear()
        txtnamabarang.Enabled = False

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = False

        btntambahbarang.Enabled = False
        btnrefresh.Enabled = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        Call tabel_utama()

        'total tabel penjualan
        txtketerangan.Enabled = False
        txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()

        If nomorkode IsNot "" Then
            sql = "SELECT * FROM tb_kirim WHERE kode_kirim = '" + nomorkode.ToString + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                'header
                nomorpengiriman = dr("kode_pengiriman")
                nomoruser = dr("kode_user")

                namaexpedisi = dr("nama_expedisi")
                alamatexpedisi = dr("alamat_expedisi")
                telpexpedisi = dr("telp_expedisi")

                statusprint = dr("print_pengiriman")
                statusposted = dr("posted_pengiriman")

                viewtotalpengiriman = dr("total_pengiriman")
                viewtglpengiriman = dr("tgl_pengiriman")

                viewketerangan = dr("keterangan_pengiriman")

                'isi data pengiriman
                txtnonota.Text = nomorpengiriman
                cmbuser.Text = nomoruser

                txtnamaexpedisi.Text = namaexpedisi
                txtalamatexpedisi.Text = alamatexpedisi
                txttelpexpedisi.Text = telpexpedisi

                txttotalongkir.Text = viewtotalpengiriman
                txtketerangan.Text = viewketerangan

                dtpengiriman.Value = viewtglpengiriman

                cbprinted.Checked = statusprint
                cbposted.Checked = statusposted


                'isi tabel view pengiriman

                Call previewpengiriman(nomorkode)

                'total tabel pembelian



            End If
        Else
            txtnonota.Clear()
            cmbuser.Text = ""

            txtnamaexpedisi.Text = ""
            txtalamatexpedisi.Text = ""
            txttelpexpedisi.Text = ""

            dtpengiriman.Value = Date.Now
            txttotalongkir.Text = 0

            cbprinted.Checked = False
            cbposted.Checked = False

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
        'cmbsales.SelectedIndex = -1
        cmbuser.Enabled = True

        txtnamaexpedisi.Enabled = True
        txtalamatexpedisi.Enabled = True
        txttelpexpedisi.Enabled = True

        dtpengiriman.Enabled = True

        txttotalongkir.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True
        btnrefresh.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = True

        'total tabel pembelian
        txtketerangan.Enabled = True
        'txtketerangan.Clear()

        'isi combo box
        Call comboboxuser()

    End Sub

    Sub tabel_utama()
        tabel = New DataTable

        With tabel
            .Columns.Add("kode_barang")
            .Columns.Add("nama_barang")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("harga_barang", GetType(Double))
            .Columns.Add("ongkos_kirim", GetType(Double))
            .Columns.Add("total_ongkos_kirim", GetType(Double))
            .Columns.Add("total_harga_barang", GetType(Double))
            .Columns.Add("grand_total_barang", GetType(Double))

        End With

        GridControl1.DataSource = tabel

        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Caption = "Kode Barang"
        GridColumn1.Width = 20

        GridColumn2.FieldName = "nama_barang"
        GridColumn2.Caption = "Nama Barang"
        GridColumn2.Width = 40

        GridColumn3.FieldName = "qty"
        GridColumn3.Caption = "Qty"
        GridColumn3.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn3.DisplayFormat.FormatString = "{0:n0}"
        GridColumn3.Width = 5

        GridColumn4.FieldName = "harga_barang"
        GridColumn4.Caption = "Harga Barang"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        GridColumn4.Width = 30

        GridColumn5.FieldName = "ongkos_kirim"
        GridColumn5.Caption = "Ongkos_kirim"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 30

        GridColumn6.FieldName = "total_ongkos_kirim"
        GridColumn6.Caption = "Total Ongkos Kirim"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "total_harga_barang"
        GridColumn7.Caption = "Total Harga Barang"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 30

        GridColumn8.FieldName = "grand_total_barang"
        GridColumn8.Caption = "Grand Total Barang"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 30

    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyakbarang.Clear()
        txthargabarang.Text = 0
        txtkodebarang.Focus()
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

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btncaridata_Click(sender As Object, e As EventArgs) Handles btncaridata.Click

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click

    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click

    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs)

    End Sub
End Class