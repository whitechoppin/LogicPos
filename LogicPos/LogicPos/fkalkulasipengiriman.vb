Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class fkalkulasipengiriman
    Dim hitnumber As Integer
    Public tabel As DataTable
    'variabel dalam expedisi
    Public kodepengiriman As String
    Dim hargakubik, hargabarang, banyakbarang, kubikbarang, hargatambahongkir, ongkirbarang, totalongkirbarang, totalhargabarang, grandtotalbarang As Double

    'variabel bantuan view pengiriman
    Dim nomorpengiriman, nomoruser, namaexpedisi, alamatexpedisi, telpexpedisi, viewketerangan As String
    Dim statusprint, statusposted, statusedit As Boolean
    Dim viewtglpengiriman As DateTime
    Dim viewhargacolly As Double

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
            .Columns("kubik").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "kubik", "{0:n0}")
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

        txthargakubik.Clear()
        txthargakubik.Text = 0
        txthargakubik.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True
        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txtkubik.Clear()
        txtkubik.Text = 0
        txtkubik.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

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

    Sub previewpengiriman(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_kirim_detail WHERE kode_kirim ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("nama_barang"), Val(dr("qty")), Val(dr("harga_barang")), Val(dr("kubik")), Val(dr("ongkos_kirim")), Val(dr("harga_tambah_ongkir")), Val(dr("total_ongkos_kirim")), Val(dr("total_harga_barang")), Val(dr("grand_total_barang")))
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

        txthargakubik.Enabled = False

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

        txtkubik.Clear()
        txtkubik.Text = 0
        txtkubik.Enabled = False

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = False

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

                viewhargacolly = dr("harga_colly")
                viewtglpengiriman = dr("tgl_pengiriman")

                viewketerangan = dr("keterangan_pengiriman")

                'isi data pengiriman
                txtnonota.Text = nomorpengiriman
                cmbuser.Text = nomoruser

                txtnamaexpedisi.Text = namaexpedisi
                txtalamatexpedisi.Text = alamatexpedisi
                txttelpexpedisi.Text = telpexpedisi

                txthargakubik.Text = viewhargacolly
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
            txthargakubik.Text = 0

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

        txthargakubik.Enabled = True

        'body
        txtkodebarang.Clear()
        txtkodebarang.Enabled = True

        btncaribarang.Enabled = True

        txtnamabarang.Clear()
        txtnamabarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

        txtkubik.Clear()
        txtkubik.Text = 0
        txtkubik.Enabled = True

        txthargabarang.Clear()
        txthargabarang.Text = 0
        txthargabarang.Enabled = True

        btntambahbarang.Enabled = True

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
            .Columns.Add("kubik", GetType(Double))
            .Columns.Add("ongkos_kirim", GetType(Double))
            .Columns.Add("harga_tambah_ongkir", GetType(Double))
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

        GridColumn5.FieldName = "kubik"
        GridColumn5.Caption = "kubik"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 30

        GridColumn6.FieldName = "ongkos_kirim"
        GridColumn6.Caption = "Ongkos_kirim"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 30

        GridColumn7.FieldName = "harga_tambah_ongkir"
        GridColumn7.Caption = "harga_tambah_ongkir"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 30

        GridColumn8.FieldName = "total_ongkos_kirim"
        GridColumn8.Caption = "Total Ongkos Kirim"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 30

        GridColumn9.FieldName = "total_harga_barang"
        GridColumn9.Caption = "Total Harga Barang"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "grand_total_barang"
        GridColumn10.Caption = "Grand Total Barang"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

    End Sub


    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtbanyakbarang.Clear()
        txtkubik.Clear()
        txthargabarang.Clear()
        txtkodebarang.Focus()
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click
        Call awalbaru()
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


    Sub caribarang()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang='" & txtkodebarang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamabarang.Text = dr("nama_barang")
            hargabarang = dr("modal_barang")
            txthargabarang.Text = Format(hargabarang, "##,##0")
        Else
            txtnamabarang.Text = ""
            txthargabarang.Text = 0
        End If
    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged
        Call caribarang()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click

    End Sub

    Private Sub txthargacolly_TextChanged(sender As Object, e As EventArgs) Handles txthargakubik.TextChanged
        If txthargakubik.Text = "" Then
            txthargakubik.Text = 0
        Else
            hargakubik = txthargakubik.Text
            txthargakubik.Text = Format(hargakubik, "##,##0")
            txthargakubik.SelectionStart = Len(txthargakubik.Text)
        End If
    End Sub

    Private Sub txthargacolly_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargakubik.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txthargabarang_TextChanged(sender As Object, e As EventArgs) Handles txthargabarang.TextChanged
        If txthargabarang.Text = "" Then
            txthargabarang.Text = 0
        Else
            hargabarang = txthargabarang.Text
            txthargabarang.Text = Format(hargabarang, "##,##0")
            txthargabarang.SelectionStart = Len(txthargabarang.Text)
        End If
    End Sub

    Private Sub txthargabarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargabarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtbanyakbarang_TextChanged(sender As Object, e As EventArgs) Handles txtbanyakbarang.TextChanged
        If txtbanyakbarang.Text = "" Then
            txtbanyakbarang.Text = 0
        Else
            banyakbarang = txtbanyakbarang.Text
            txtbanyakbarang.Text = Format(banyakbarang, "##,##0")
            txtbanyakbarang.SelectionStart = Len(txtbanyakbarang.Text)
        End If
    End Sub

    Private Sub txtbanyakbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyakbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtkubik_TextChanged(sender As Object, e As EventArgs) Handles txtkubik.TextChanged
        If txtkubik.Text = "" Then
            txtkubik.Text = 0
        Else
            kubikbarang = txtkubik.Text
            txtkubik.Text = Format(kubikbarang, "##,##0")
            txtkubik.SelectionStart = Len(txtkubik.Text)
        End If
    End Sub

    Private Sub txtkubik_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkubik.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub tambah()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txthargabarang.Text = "" Or txtbanyakbarang.Text = "" Or banyakbarang <= 0 Or txtkubik.Text = "" Or kubikbarang <= 0 Then
            Exit Sub
        Else
            totalongkirbarang = hargakubik * kubikbarang
            ongkirbarang = totalongkirbarang / banyakbarang
            hargatambahongkir = hargabarang + ongkirbarang
            totalhargabarang = hargabarang * banyakbarang
            grandtotalbarang = totalongkirbarang + totalhargabarang

            tabel.Rows.Add(txtkodebarang.Text, txtnamabarang.Text, Val(banyakbarang), Val(hargabarang), Val(kubikbarang), Val(ongkirbarang), Val(hargatambahongkir), Val(totalongkirbarang), Val(totalhargabarang), Val(grandtotalbarang))
            Call reload_tabel()
        End If
    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
    End Sub

    Sub kalkulasi()
        'tambahkan total barang ulang
        For i As Integer = 0 To GridView1.RowCount - 1
            'mendapatkan ongkir satuan barang
            'ongkirbarangulang = totalongkospengiriman * (Val(GridView1.GetRowCellValue(i, "volume_barang")) / grandtotalvolumebarang)
            ''mendapatkan total ongkir satuan barang  x qty
            'totalongkirbarangulang = ongkirbarangulang * Val(GridView1.GetRowCellValue(i, "qty"))
            ''mendapatkan total harga barang
            'totalhargabarangulang = Val(GridView1.GetRowCellValue(i, "harga_barang")) * Val(GridView1.GetRowCellValue(i, "qty"))

            'grandtotalbarangulang = totalhargabarangulang + totalongkirbarangulang

            'GridView1.SetRowCellValue(i, "ongkos_kirim", ongkirbarangulang)
            'GridView1.SetRowCellValue(i, "total_ongkos_kirim", totalongkirbarangulang)
            'GridView1.SetRowCellValue(i, "grand_total_barang", grandtotalbarangulang)
        Next

        GridView1.RefreshData()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call kalkulasi()
    End Sub

End Class