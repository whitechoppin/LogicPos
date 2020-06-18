Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fkalkulasiexpedisi
    Dim hitnumber As Integer
    Public tabel As DataTable
    'variabel dalam expedisi
    Public kodeexpedisi As String
    Dim totalhargaongkir, hargabarang, panjangbarang, lebarbarang, tinggibarang, volumebarang, banyakbarang, totalvolumebarang, ongkirbarang, totalongkirbarang, totalhargabarang, grandtotalbarang, grandtotalvolumebarang As Double

    'variabel bantuan view pengiriman
    Dim nomorform, nomorexpedisi, nomorsales, viewketerangan As String
    Dim statusprint, statusposted, statusedit As Boolean
    Dim viewtglpengiriman As DateTime

    'variabel bantuan hitung gridview
    Dim ongkirbarangulang, totalongkirbarangulang, totalvolumebarangbefore, totalhargabarangulang, grandtotalbarangulang As Double

    'Dim summarytotalvolumebefore, volumebarangbefore, qtybarangbefore As Double

    Private Sub fkalkulasiexpedisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        hitnumber = 0
        kodeexpedisi = currentnumber()
        Call inisialisasi(kodeexpedisi)

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("total_volume").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_volume", "{0:n0}")
            .Columns("ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ongkos_kirim", "{0:n0}")
            .Columns("total_ongkos_kirim").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_ongkos_kirim", "{0:n0}")
            .Columns("total_harga_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_harga_barang", "{0:n0}")
            .Columns("grand_total_barang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "grand_total_barang", "{0:n0}")
        End With
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_pengiriman,3) FROM tb_pengiriman WHERE DATE_FORMAT(MID(`kode_pengiriman`, 3 , 6), ' %y ')+ MONTH(MID(`kode_pengiriman`,3 , 6)) + DAY(MID(`kode_pengiriman`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_pengiriman,3) DESC"
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
            cnn.Close()
        End Try
        Return pesan
    End Function

    Function currentnumber()
        Call koneksii()
        sql = "SELECT kode_pengiriman FROM tb_pengiriman ORDER BY kode_pengiriman DESC LIMIT 1;"
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

    Sub previewpengiriman(lihat As String)
        Call koneksii()
        sql = "SELECT * FROM tb_pengiriman_detail WHERE kode_pengiriman ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel.Rows.Add(dr("kode_barang"), dr("nama_barang"), Val(dr("panjang_barang")), Val(dr("lebar_barang")), Val(dr("tinggi_barang")), Val(dr("volume_barang")), Val(dr("qty")), Val(dr("total_volume")), Val(dr("harga_barang")), Val(dr("ongkos_kirim")), Val(dr("total_ongkos_kirim")), Val(dr("total_harga_barang")), Val(dr("ongkos_kirim")))
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

        cmbsales.SelectedIndex = 0
        cmbsales.Enabled = True

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

        txtpanjangbarang.Clear()
        txtpanjangbarang.Text = 0
        txtpanjangbarang.Enabled = True

        txtlebarbarang.Clear()
        txtlebarbarang.Text = 0
        txtlebarbarang.Enabled = True

        txttinggibarang.Clear()
        txttinggibarang.Text = 0
        txttinggibarang.Enabled = True


        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

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

        txtpanjangbarang.Clear()
        txtpanjangbarang.Text = 0
        txtpanjangbarang.Enabled = False

        txtlebarbarang.Clear()
        txtlebarbarang.Text = 0
        txtlebarbarang.Enabled = False

        txttinggibarang.Clear()
        txttinggibarang.Text = 0
        txttinggibarang.Enabled = False


        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = False

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
            'Using cnn As New OdbcConnection(strConn)
            '    sql = "SELECT * FROM tb_pengiriman WHERE kode_pengiriman = '" + nomorkode.ToString + "'"
            '    cmmd = New OdbcCommand(sql, cnn)
            '    cnn.Open()
            '    dr = cmmd.ExecuteReader
            '    dr.Read()
            '    If dr.HasRows Then
            '        'header
            '        nomorform = dr("kode_form")
            '        nomorexpedisi = dr("kode_ex")
            '        nomorsales = dr("kode_user")

            '        statusprint = dr("print_pengiriman")
            '        statusposted = dr("posted_pengiriman")

            '        viewtglpengiriman = dr("tgl_pengiriman")

            '        viewketerangan = dr("keterangan_pengiriman")

            '        'isi data pengiriman
            '        txtnonota.Text = nomorform

            '        txtnamaexpedisi.Text = ""
            '        txtalamatexpedisi.Text = ""
            '        txttelpexpedisi.Text = ""

            '        cmbsales.Text = nomorsales

            '        cbprinted.Checked = statusprint
            '        cbposted.Checked = statusposted

            '        dtpengiriman.Value = viewtglpengiriman

            '        'isi tabel view pengiriman

            '        Call previewpengiriman(nomorkode)

            '        'total tabel pembelian

            '        txtketerangan.Text = viewketerangan

            '        cnn.Close()
            '    End If
            'End Using
        Else
            txtnonota.Clear()

            cmbsales.Text = ""

            txtnamaexpedisi.Text = ""
            txtalamatexpedisi.Text = ""
            txttelpexpedisi.Text = ""

            txttotalongkir.Text = 0

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
        'cmbsales.SelectedIndex = -1
        cmbsales.Enabled = True

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

        txtpanjangbarang.Clear()
        txtpanjangbarang.Text = 0
        txtpanjangbarang.Enabled = True

        txtlebarbarang.Clear()
        txtlebarbarang.Text = 0
        txtlebarbarang.Enabled = True

        txttinggibarang.Clear()
        txttinggibarang.Text = 0
        txttinggibarang.Enabled = True

        txtbanyakbarang.Clear()
        txtbanyakbarang.Text = 0
        txtbanyakbarang.Enabled = True

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
            .Columns.Add("panjang_barang", GetType(Double))
            .Columns.Add("lebar_barang", GetType(Double))
            .Columns.Add("tinggi_barang", GetType(Double))
            .Columns.Add("volume_barang", GetType(Double))
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("total_volume", GetType(Double))
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

        GridColumn3.FieldName = "panjang_barang"
        GridColumn3.Caption = "Panjang Barang"
        GridColumn3.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn3.DisplayFormat.FormatString = "{0:n0}"
        GridColumn3.Width = 5

        GridColumn4.FieldName = "lebar_barang"
        GridColumn4.Caption = "Lebar Barang"
        GridColumn4.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn4.DisplayFormat.FormatString = "{0:n0}"
        GridColumn4.Width = 5

        GridColumn5.FieldName = "tinggi_barang"
        GridColumn5.Caption = "Tinggi Barang"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn5.Width = 5

        GridColumn6.FieldName = "volume_barang"
        GridColumn6.Caption = "Volume Barang"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.Width = 5

        GridColumn7.FieldName = "qty"
        GridColumn7.Caption = "Qty"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.Width = 5

        GridColumn8.FieldName = "total_volume"
        GridColumn8.Caption = "Total Volume"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.Width = 20

        GridColumn9.FieldName = "harga_barang"
        GridColumn9.Caption = "Harga Barang"
        GridColumn9.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn9.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.Width = 30

        GridColumn10.FieldName = "ongkos_kirim"
        GridColumn10.Caption = "Ongkos_kirim"
        GridColumn10.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn10.DisplayFormat.FormatString = "{0:n0}"
        GridColumn10.Width = 30

        GridColumn11.FieldName = "total_ongkos_kirim"
        GridColumn11.Caption = "Total Ongkos Kirim"
        GridColumn11.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn11.DisplayFormat.FormatString = "{0:n0}"
        GridColumn11.Width = 30

        GridColumn12.FieldName = "total_harga_barang"
        GridColumn12.Caption = "Total Harga Barang"
        GridColumn12.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn12.DisplayFormat.FormatString = "{0:n0}"
        GridColumn12.Width = 30

        GridColumn13.FieldName = "grand_total_barang"
        GridColumn13.Caption = "Grand Total Barang"
        GridColumn13.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn13.DisplayFormat.FormatString = "{0:n0}"
        GridColumn13.Width = 30

    End Sub

    Sub reload_tabel()
        GridControl1.RefreshDataSource()

        txtkodebarang.Clear()
        txtnamabarang.Clear()
        txtpanjangbarang.Clear()
        txtlebarbarang.Clear()
        txttinggibarang.Clear()
        txtbanyakbarang.Clear()
        txthargabarang.Text = 0
        txtkodebarang.Focus()
    End Sub

    Sub cariuser()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtsales.Text = dr("nama_user")
        Else
            txtsales.Text = ""
        End If
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

        If btnedit.Text.Equals("Edit") Then
            Call inisialisasi(kodeexpedisi)
        ElseIf btnedit.Text.Equals("Update") Then
            btnedit.Text = "Edit"
            Call inisialisasi(txtnonota.Text)
        End If
    End Sub

    Private Sub txttotalongkir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalongkir.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txthargabarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthargabarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtpanjangbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpanjangbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtlebarbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtlebarbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txttinggibarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttinggibarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtbanyakbarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyakbarang.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txttotalongkir_TextChanged(sender As Object, e As EventArgs) Handles txttotalongkir.TextChanged
        If txttotalongkir.Text = "" Then
            txttotalongkir.Text = 0
        Else
            totalhargaongkir = txttotalongkir.Text
            txttotalongkir.Text = Format(totalhargaongkir, "##,##0")
            txttotalongkir.SelectionStart = Len(txttotalongkir.Text)
        End If
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

    Private Sub txtpanjangbarang_TextChanged(sender As Object, e As EventArgs) Handles txtpanjangbarang.TextChanged
        If txtpanjangbarang.Text = "" Then
            txtpanjangbarang.Text = 0
        Else
            panjangbarang = txtpanjangbarang.Text
            txtpanjangbarang.Text = Format(panjangbarang, "##,##0")
            txtpanjangbarang.SelectionStart = Len(txtpanjangbarang.Text)
        End If
    End Sub

    Private Sub txtlebarbarang_TextChanged(sender As Object, e As EventArgs) Handles txtlebarbarang.TextChanged
        If txtlebarbarang.Text = "" Then
            txtlebarbarang.Text = 0
        Else
            lebarbarang = txtlebarbarang.Text
            txtlebarbarang.Text = Format(lebarbarang, "##,##0")
            txtlebarbarang.SelectionStart = Len(txtlebarbarang.Text)
        End If
    End Sub

    Private Sub txttinggibarang_TextChanged(sender As Object, e As EventArgs) Handles txttinggibarang.TextChanged
        If txttinggibarang.Text = "" Then
            txttinggibarang.Text = 0
        Else
            tinggibarang = txttinggibarang.Text
            txttinggibarang.Text = Format(tinggibarang, "##,##0")
            txttinggibarang.SelectionStart = Len(txttinggibarang.Text)
        End If
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

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged

    End Sub

    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged

    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click

    End Sub

    Sub tambah()
        If txtkodebarang.Text = "" Or txtnamabarang.Text = "" Or txthargabarang.Text = "" Or txtpanjangbarang.Text = "" Or txtlebarbarang.Text = "" Or txttinggibarang.Text = "" Or txtbanyakbarang.Text = "" Or banyakbarang <= 0 Then
            Exit Sub
        End If

        volumebarang = panjangbarang * lebarbarang * tinggibarang
        totalvolumebarang = volumebarang * banyakbarang
        totalhargabarang = hargabarang * banyakbarang

        ongkirbarang = 0
        totalongkirbarang = 0
        grandtotalbarang = 0

        tabel.Rows.Add(txtkodebarang.Text, txtnamabarang.Text, Val(panjangbarang), Val(lebarbarang), Val(tinggibarang), Val(volumebarang), Val(banyakbarang), Val(totalvolumebarang), Val(hargabarang), Val(ongkirbarang), Val(totalongkirbarang), Val(totalhargabarang), Val(grandtotalbarang))
        Call reload_tabel()
    End Sub

    Private Sub btntambahbarang_Click(sender As Object, e As EventArgs) Handles btntambahbarang.Click
        Call tambah()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        For i As Integer = 0 To GridView1.RowCount - 1

            GridView1.SetRowCellValue(i, "ongkos_kirim", 0)
            GridView1.SetRowCellValue(i, "total_ongkos_kirim", 0)
            GridView1.SetRowCellValue(i, "grand_total_barang", 0)
        Next
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        grandtotalvolumebarang = Val(GridView1.Columns("total_volume").SummaryItem.SummaryValue)

        For i As Integer = 0 To GridView1.RowCount - 1
            ongkirbarangulang = totalhargaongkir * (Val(GridView1.GetRowCellValue(i, "volume_barang")) / grandtotalvolumebarang)

            totalongkirbarangulang = ongkirbarangulang * 1

            grandtotalbarangulang = totalhargabarangulang + totalongkirbarangulang

            GridView1.SetRowCellValue(i, "ongkos_kirim", ongkirbarangulang)
            GridView1.SetRowCellValue(i, "total_ongkos_kirim", totalongkirbarangulang)
            GridView1.SetRowCellValue(i, "grand_total_barang", grandtotalbarangulang)
        Next
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "qty" Then
            If e.Value = "" Or e.Value = "0" Then
                GridView1.SetRowCellValue(e.RowHandle, "qty", 1)
                Try
                    'rumus ongkos kirim
                    totalvolumebarangbefore = GridView1.GetRowCellValue(e.RowHandle, "volume_barang") * GridView1.GetRowCellValue(e.RowHandle, "qty")
                    grandtotalvolumebarang = (GridView1.Columns("total_volume").SummaryItem.SummaryValue - totalvolumebarangbefore) + GridView1.GetRowCellValue(e.RowHandle, "volume_barang") * e.Value
                    GridView1.SetRowCellValue(e.RowHandle, "total_volume", GridView1.GetRowCellValue(e.RowHandle, "volume_barang") * 1)

                Catch ex As Exception

                End Try
            Else

            End If
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete And btnbatal.Enabled = True Then
            GridView1.DeleteSelectedRows()
        End If
    End Sub

    Private Sub ritenumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ritenumber.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub fkalkulasiexpedisi_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

End Class