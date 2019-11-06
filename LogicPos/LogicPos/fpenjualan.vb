Imports System.Data.Odbc
Imports DevExpress.Utils
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports CrystalDecisions.Shared
Public Class fpenjualan
    Public tabel As DataTable
    Dim tgl As Date
    Dim harga As Double = 0
    Public total2 As Double = 0
    Public total3 As Double = 0
    Public customer, struk, faktur, jenis, satuan, kode_barang As String
    Public diskon As Double = 0
    Public bayar As Double = 0
    Public kembali As Double = 0

    Dim sisa As Double = 0
    Public isi As String
    Public metode As String
    Dim stok As Double
    Dim namabaru As String
    Dim penggunaan, modal As Double

    'variabel bantuan view pembelian
    Dim nomornota, nomorsupplier, nomorsales, nomorgudang, viewketerangan As String
    Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    Dim viewtglpembelian, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir As Double
    Private Sub fpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        Call awal()
        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
        End With
        tgl = Now()
    End Sub

    'Sub cek_kas()
    '    Dim tgl As Date
    '    Dim tutupkas As Date
    '    Call koneksii()
    '    cmmd = New OdbcCommand("SELECT * FROM tb_historikas where idkasir=  '" & fmenu.statususer.Text & "' order by id desc limit 1", cnn)
    '    dr = cmmd.ExecuteReader()
    '    If dr.HasRows Then
    '        'MsgBox("cek kas 1")
    '        tgl = dr("bukakas")
    '        tutupkas = dr("tutupkas")
    '        'MsgBox(tgl)
    '        If tgl = "1/1/1990" Then
    '            'MsgBox("cek kas 1 1/2")
    '            Call fmodal.Show()
    '        Else

    '            If tutupkas <> "1/1/1990" Then
    '                'MsgBox("cek kas 1 1 fmodal")
    '                fmodal.Show()
    '                'sql = "INSERT INTO tb_historikas ( idkasir ) VALUES ( '" & fmenu.statususer.Text & "')"
    '                'cmmd = New OdbcCommand(sql, cnn)
    '                'dr = cmmd.ExecuteReader()
    '                'Call awal()
    '            Else
    '                'MsgBox("cek kas 1 1 awal")
    '                Call awal()
    '            End If

    '        End If
    '    Else
    '        'MsgBox("cek kas 2")
    '        'sql = "INSERT INTO tb_historikas ( idkasir ) VALUES ( '" & fmenu.statususer.Text & "')"
    '        'cmmd = New OdbcCommand(sql, cnn)
    '        'dr = cmmd.ExecuteReader()
    '        fmodal.Show()
    '    End If
    '    With GridView1
    '        .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
    '        'buat sum harga
    '        .Columns("Subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Subtotal", "{0:n0}")
    '    End With
    'End Sub

    Sub awal()

        metode = "CASH"
        'Call autonumber()
        lbltotal.Text = 0
        rbfaktur.Checked = True
        txtinformasi.Enabled = False
        txtharga.Enabled = False
        txtkodebarang.Focus()
        txtbanyak.Clear()
        txtkodebarang.Clear()
        txtnama.Clear()
        txtcustomer.Clear()
        txtsisa.Enabled = False

        txtnama.Enabled = False
        sisa = 0
        txtsisa.Clear()
        txttotal.Clear()
        txttotal.Enabled = False
        txtdiskon.Clear()
        txtdiskon.Enabled = False
        txtbayar.Clear()
        txtbayar.Enabled = True
        txtkembali.Clear()
        txtkembali.Enabled = False
        'cmbcustomer.SelectedIndex = -1

        tabel = New DataTable
        With tabel
            .Columns.Add("nama_barang")
            .Columns.Add("banyak", GetType(Double))
            .Columns.Add("satuan_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan", GetType(Double))
            .Columns.Add("diskon", GetType(Double))
            .Columns.Add("hargadiskon", GetType(Double))
            .Columns.Add("subtotal", GetType(Double))
            .Columns.Add("kode_stok")
            .Columns.Add("laba")
            .Columns.Add("modal_barang")
            .Columns.Add("kode_barang")

        End With

        GridControl1.DataSource = tabel
        GridColumn1.FieldName = "nama_barang"
        GridColumn2.FieldName = "banyak"
        GridColumn3.FieldName = "satuan_barang"
        GridColumn4.FieldName = "jenis_barang"
        GridColumn5.FieldName = "harga_satuan"
        GridColumn5.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn5.DisplayFormat.FormatString = "{0:n0}"
        GridColumn6.FieldName = "diskon"
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"
        GridColumn7.FieldName = "hargadiskon"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        GridColumn8.FieldName = "subtotal"
        GridColumn8.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn8.DisplayFormat.FormatString = "{0:n0}"
        GridColumn9.FieldName = "kode_stok"
        GridColumn9.Visible = False
        GridColumn10.FieldName = "laba"
        'GridColumn10.Visible = False
        GridColumn11.FieldName = "modal_barang"
        'GridColumn11.Visible = False
        GridColumn12.FieldName = "kode_barang"

    End Sub
    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_penjualan,3) FROM tb_penjualan WHERE DATE_FORMAT(MID(`kode_penjualan`, 3 , 6), ' %y ')+ MONTH(MID(`kode_penjualan`,3 , 6)) + DAY(MID(`kode_penjualan`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_penjualan,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "JL" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "JL" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "JL" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "JL" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
    Sub caripelanggan()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbcustomer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtcustomer.Text = dr("nama_pelanggan")
        Else
            txtcustomer.Text = ""
        End If
    End Sub

    Sub carigudang()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang='" & cmbgudang.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtgudang.Text = dr("nama_gudang")
        Else
            txtgudang.Text = ""
        End If
    End Sub

    Sub cari()
        Call koneksii()
        sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok = '" & txtkodebarang.Text & "' AND tb_price_group.kode_pelanggan ='" & cmbcustomer.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnama.Text = dr("nama_barang")
            satuan = dr("satuan_barang")
            lblsatuan.Text = satuan
            lblsatuanjual.Text = satuan
            jenis = dr("jenis_barang")
            txtharga.Text = Format(dr("harga_jual"), "##,##0")
            txtharga.SelectionStart = Len(txtharga.Text)
            modal = dr("modal_barang")
            kode_barang = dr("kode_barang")
        Else
            sql = "SELECT * FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang JOIN tb_price_group ON tb_barang.kode_barang = tb_price_group.kode_barang WHERE kode_stok= '" & txtkodebarang.Text & "' AND tb_price_group.kode_pelanggan = '00000000'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                txtnama.Text = dr("nama_barang")
                satuan = dr("satuan_barang")
                lblsatuan.Text = satuan
                lblsatuanjual.Text = satuan
                jenis = dr("jenis_barang")
                txtharga.Text = Format(dr("harga_jual"), "##,##0")
                txtharga.SelectionStart = Len(txtharga.Text)
                modal = dr("modal_barang")
                kode_barang = dr("kode_barang")
            Else
                txtnama.Text = ""
                satuan = ""
                lblsatuan.Text = satuan
                lblsatuanjual.Text = satuan
                jenis = ""
                txtharga.Text = ""
            End If
        End If
    End Sub
    Sub reload_tabel()
        GridControl1.RefreshDataSource()
        txtkodebarang.Clear()
        txtnama.Clear()
        txtbanyak.Clear()
        txtharga.Text = 0
        txtnama.Enabled = False
        txtkodebarang.Focus()
    End Sub
    Private Sub txtkodebarang_TextChanged(sender As Object, e As EventArgs) Handles txtkodebarang.TextChanged
        'isi = txtkodeitem.Text
        'isicari = isi
        'If Strings.Left(txtkodeitem.Text, 1) Like "[A-Z, a-z]" Then

        '    Call search()

        '    'fcaribarang.txtcari.Focus()
        '    'fcaribarang.txtcari.DeselectAll()
        'Else
        '    Call cari()

        'End If
        Call cari()
    End Sub
    Sub tambah()
        If txtnama.Text = "" Or txtharga.Text = "" Or txtbanyak.Text = "" Then
            Exit Sub
        Else
            If GridView1.RowCount = 0 Then 'kondisi keranjang kosong
                sql = "select * from tb_barang join tb_stok on tb_barang.kode_barang=tb_stok.kode_barang join tb_price_group on tb_barang.kode_barang=tb_price_group.kode_barang where kode_stok= '" & txtkodebarang.Text & "' and tb_price_group.kode_pelanggan='" & cmbcustomer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                        MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Exit Sub
                    Else
                        tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), txtkodebarang.Text, (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modal) * Val(txtbanyak.Text)), modal, dr("kode_barang"))
                        Call reload_tabel()
                    End If
                Else
                    sql = "select * from tb_barang join tb_stok on tb_barang.kode_barang=tb_stok.kode_barang join tb_price_group on tb_barang.kode_barang=tb_price_group.kode_barang where kode_stok= '" & txtkodebarang.Text & "' and tb_price_group.kode_pelanggan='00000000'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                        MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                        Exit Sub
                    Else
                        tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), txtkodebarang.Text, (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modal) * Val(txtbanyak.Text)), modal, dr("kode_barang"))
                        Call reload_tabel()
                    End If
                End If

            Else 'kalau ada isi
                sql = "select * from tb_barang join tb_stok on tb_barang.kode_barang=tb_stok.kode_barang join tb_price_group on tb_barang.kode_barang=tb_price_group.kode_barang where kode_stok= '" & txtkodebarang.Text & "' and tb_price_group.kode_pelanggan='" & cmbcustomer.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If txtkodebarang.Text = GridView1.GetRowCellValue(i, "kode_stok") Then
                            Dim banyak As Integer
                            Dim diskon As Double
                            Dim hargadiskon As Double
                            banyak = GridView1.GetRowCellValue(i, "banyak")
                            diskon = GridView1.GetRowCellValue(i, "diskon")
                            hargadiskon = GridView1.GetRowCellValue(i, "hargadiskon")


                            If dr("jumlah_stok") + banyak < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(i))
                                tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text) + banyak, satuan, jenis, Val(dr("harga_jual")), "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * (Val(txtbanyak.Text) + banyak), txtkodebarang.Text, (Val(dr("harga_jual")) - Val(modal)) * (Val(txtbanyak.Text) + banyak), modal, dr("kode_barang"))
                                Call reload_tabel()
                                Exit Sub
                            End If
                        Else
                            If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), txtkodebarang.Text, (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modal) * Val(txtbanyak.Text)), modal, dr("kode_barang"))
                                Call reload_tabel()
                                Exit Sub
                            End If
                        End If
                    Next
                Else
                    sql = "select * from tb_barang join tb_stok on tb_barang.kode_barang=tb_stok.kode_barang join tb_price_group on tb_barang.kode_barang=tb_price_group.kode_barang where kode_stok= '" & txtkodebarang.Text & "' and tb_price_group.kode_pelanggan='00000000'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    dr.Read()
                    For i As Integer = 0 To GridView1.RowCount - 1
                        If txtkodebarang.Text = GridView1.GetRowCellValue(i, "kode_barang") Then
                            Dim banyak As Integer
                            Dim diskon As Double
                            Dim hargadiskon As Double
                            banyak = GridView1.GetRowCellValue(i, "banyak")
                            diskon = GridView1.GetRowCellValue(i, "diskon")
                            hargadiskon = GridView1.GetRowCellValue(i, "hargadiskon")


                            If dr("jumlah_stok") + banyak < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                GridView1.DeleteRow(GridView1.GetRowHandle(i))
                                tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text) + banyak, satuan, jenis, Val(dr("harga_jual")), "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * (Val(txtbanyak.Text) + banyak), txtkodebarang.Text, (Val(dr("harga_jual")) - Val(modal)) * (Val(txtbanyak.Text) + banyak), modal, dr("kode_barang"))
                                Call reload_tabel()
                                Exit Sub
                            End If
                        Else
                            If dr("jumlah_stok") < Val(txtbanyak.Text) Then
                                MsgBox("Stok Tidak mencukupi", MsgBoxStyle.Information, "Informasi")
                                Exit Sub
                            Else
                                tabel.Rows.Add(txtnama.Text, Val(txtbanyak.Text), satuan, jenis, Val(dr("harga_jual")), "0", Val(dr("harga_jual")), Val(dr("harga_jual")) * Val(txtbanyak.Text), txtkodebarang.Text, (Val(dr("harga_jual")) * Val(txtbanyak.Text) - Val(modal) * Val(txtbanyak.Text)), modal, dr("kode_barang"))
                                Call reload_tabel()
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub
    Sub ambil_total()
        total2 = GridView1.Columns("subtotal").SummaryItem.SummaryValue 'ambil isi summary gridview
        total3 = total2
        txttotal.Text = Format(total2, "##,##0")
        txttotal.SelectionStart = Len(txttotal.Text)
        lbltotal.Text = Format(total2, "##,##0")
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        Call tambah()
        Call ambil_total()
    End Sub
    Sub hitung()
        Dim total1 As Double
        For i As Integer = 0 To GridView1.RowCount - 1
            total1 = total1 + GridView1.GetRowCellValue(i, "subtotal")
        Next
        total3 = total1
        total1 = total1 - diskon
        txttotal.Text = Format(total1, "##,##0")
        total2 = total1
        lbltotal.Text = Format(total1, "##,##0")
        'kembali = bayar - total2
        'txtkembali.Text = Format(kembali, "##,##0")
    End Sub
    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.FieldName = "banyak" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", e.Value * GridView1.GetRowCellValue(e.RowHandle, "hargadiskon"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        End If

        If e.Column.FieldName = "diskon" Then

            Try
                GridView1.SetRowCellValue(e.RowHandle, "hargadiskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "banyak") * GridView1.GetRowCellValue(e.RowHandle, "hargadiskon"))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (GridView1.GetRowCellValue(e.RowHandle, "hargadiskon") - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "banyak"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        End If

        If e.Column.FieldName = "hargadiskon" Then
            Try
                GridView1.SetRowCellValue(e.RowHandle, "diskon", GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - e.Value)
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", GridView1.GetRowCellValue(e.RowHandle, "banya") * (GridView1.GetRowCellValue(e.RowHandle, "harga_satuan") - GridView1.GetRowCellValue(e.RowHandle, "diskon")))
                GridView1.SetRowCellValue(e.RowHandle, "laba", (e.Value - GridView1.GetRowCellValue(e.RowHandle, "modal_barang")) * GridView1.GetRowCellValue(e.RowHandle, "banyak"))
            Catch ex As Exception
                'error jika nulai qty=blank
                GridView1.SetRowCellValue(e.RowHandle, "subtotal", 0)
            End Try
        End If
        Call hitung()
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            GridView1.DeleteSelectedRows()
            Call hitung()
        End If
    End Sub
    Private Sub txtbayar_TextChanged(sender As Object, e As EventArgs) Handles txtbayar.TextChanged
        If txtbayar.Text.Length = 0 Then
            txtbayar.Text = 0
            bayar = txtbayar.Text
            txtbayar.Text = Format(bayar, "##,##0")
            txtbayar.SelectionStart = Len(txtbayar.Text)
            kembali = bayar - total2
            sisa = total2 - bayar

            txtkembali.Text = Format(kembali, "##,##0")
            txtsisa.Text = Format(sisa, "##,##0")
            'lblkembali.Text = "Rp " + Format(kembali, "##,##0")
            'lblbayar.Text = "Rp " + Format(bayar, "##,##0")
        Else
            bayar = txtbayar.Text
            txtbayar.Text = Format(bayar, "##,##0")
            txtbayar.SelectionStart = Len(txtbayar.Text)
            kembali = bayar - total2
            sisa = total2 - bayar

            txtkembali.Text = Format(kembali, "##,##0")
            txtsisa.Text = Format(sisa, "##,##0")
            'lblkembali.Text = "Rp " + Format(hasil, "##,##0")
            'lblbayar.Text = "Rp " + Format(bayar, "##,##0")
        End If
    End Sub
    'Sub cetak_faktur()
    '    Dim tabel2 As New DataTable
    '    With tabel2
    '        .Columns.Add("Nama")
    '        .Columns.Add("Banyak", GetType(Double))
    '        .Columns.Add("Satuan")
    '        .Columns.Add("harga", GetType(Double))
    '        .Columns.Add("diskon", GetType(Double))
    '        .Columns.Add("hargadiskon", GetType(Double))
    '        .Columns.Add("Subtotal", GetType(Double))
    '        .Columns.Add("kode")
    '        .Columns.Add("imei")
    '    End With

    '    Dim baris As DataRow
    '    For i As Integer = 0 To GridView1.RowCount - 1
    '        baris = tabel2.NewRow
    '        baris("Nama") = GridView1.GetRowCellValue(i, "Nama")
    '        baris("Banyak") = GridView1.GetRowCellValue(i, "Banyak")
    '        baris("Satuan") = GridView1.GetRowCellValue(i, "Satuan")
    '        baris("harga") = GridView1.GetRowCellValue(i, "harga")
    '        baris("diskon") = GridView1.GetRowCellValue(i, "diskon")
    '        baris("hargadiskon") = GridView1.GetRowCellValue(i, "hargadiskon")
    '        baris("Subtotal") = GridView1.GetRowCellValue(i, "Subtotal")
    '        baris("kode") = GridView1.GetRowCellValue(i, "kode")
    '        baris("imei") = GridView1.GetRowCellValue(i, "imei")
    '        tabel2.Rows.Add(baris)
    '    Next

    '    Dim rpt As ReportDocument
    '    rpt = New fakturpenjualan
    '    rpt.SetDataSource(tabel2)
    '    rpt.SetParameterValue("tanggal", tgl)
    '    rpt.SetParameterValue("total", total2)
    '    rpt.SetParameterValue("nofaktur", autonumber)
    '    rpt.SetParameterValue("kasir", fmenu.statususer.Text)
    '    rpt.SetParameterValue("bayar", pembayaran)
    '    'If cash = True Then
    '    '    rpt.SetParameterValue("bayar", "CASH")
    '    'Else
    '    '    rpt.SetParameterValue("bayar", "CREDIT")
    '    'End If

    '    'fakturjual.CrystalReportViewer1.ReportSource = rpt
    '    'rpt.PrintOptions.PrinterName = faktur
    '    rpt.PrintToPrinter(1, False, 0, 0)
    '    'fakturjual.ShowDialog()
    '    'fakturjual.Dispose()
    'End Sub
    'Sub cetak_struk()
    '    Dim tabel2 As New DataTable
    '    With tabel2
    '        .Columns.Add("Nama")
    '        .Columns.Add("Banyak", GetType(Double))
    '        .Columns.Add("Satuan")
    '        .Columns.Add("harga", GetType(Double))
    '        .Columns.Add("diskon", GetType(Double))
    '        .Columns.Add("hargadiskon", GetType(Double))
    '        .Columns.Add("Subtotal", GetType(Double))
    '        .Columns.Add("kode")
    '        .Columns.Add("imei")
    '    End With

    '    Dim baris As DataRow
    '    For i As Integer = 0 To GridView1.RowCount - 1
    '        baris = tabel2.NewRow
    '        baris("Nama") = GridView1.GetRowCellValue(i, "Nama")
    '        baris("Banyak") = GridView1.GetRowCellValue(i, "Banyak")
    '        baris("Satuan") = GridView1.GetRowCellValue(i, "Satuan")
    '        baris("harga") = GridView1.GetRowCellValue(i, "harga")
    '        baris("diskon") = GridView1.GetRowCellValue(i, "diskon")
    '        baris("hargadiskon") = GridView1.GetRowCellValue(i, "hargadiskon")
    '        baris("Subtotal") = GridView1.GetRowCellValue(i, "Subtotal")
    '        baris("kode") = GridView1.GetRowCellValue(i, "kode")
    '        baris("imei") = GridView1.GetRowCellValue(i, "imei")
    '        tabel2.Rows.Add(baris)
    '    Next

    '    Dim rpt As ReportDocument
    '    rpt = New rptstruk1
    '    rpt.SetDataSource(tabel2)
    '    'rpt.SetParameterValue("total", total2)
    '    rpt.SetParameterValue("nofaktur", autonumber)
    '    rpt.SetParameterValue("kasir", fmenu.statususer.Text)

    '    'If cash = True Then
    '    '    rpt.SetParameterValue("bayar", "CASH")
    '    'Else
    '    '    rpt.SetParameterValue("bayar", "CREDIT")
    '    'End If

    '    'fakturjual.CrystalReportViewer1.ReportSource = rpt
    '    'rpt.PrintOptions.PrinterName = faktur
    '    rpt.PrintToPrinter(1, False, 0, 0)
    '    'fakturjual.ShowDialog()
    '    'fakturjual.Dispose()
    'End Sub
    Sub simpan()
        Dim kodepenjualan As String
        kodepenjualan = autonumber()
        Call koneksii()

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView1.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView1.GetRowCellValue(i, "kode_stok") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "INSERT INTO tb_penjualan_detail ( kode_penjualan, kode_stok, kode_barang, nama_barang, satuan, qty, harga_jual, diskon, harga_diskon, subtotal, keuntungan, created_by, updated_by,date_created, last_updated) VALUES ('" & kodepenjualan & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "banyak") & "','" & GridView1.GetRowCellValue(i, "harga_satuan") & "','" & GridView1.GetRowCellValue(i, "diskon") & "','" & GridView1.GetRowCellValue(i, "hargadiskon") & "' ,'" & GridView1.GetRowCellValue(i, "subtotal") & "','" & GridView1.GetRowCellValue(i, "laba") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "INSERT INTO tb_penjualan (kode_penjualan, kode_pelanggan, kode_gudang, kode_user, tgl_penjualan, tgl_jatuhtempo_penjualan, lunas_penjualan, void_penjualan, print_penjualan, posted_penjualan, keterangan_penjualan, diskon_penjualan, pajak_penjualan, ongkir_penjualan, total_penjualan, metode_pembayaran, rekening, bayar_penjualan, sisa_penjualan, created_by, updated_by, date_created, last_updated) VALUES ('" & kodepenjualan & "','" & cmbcustomer.Text & "','" & cmbgudang.Text & "','" & cmbsales.Text & "' , '" & Format(dtpenjualan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtjatuhtempo.Value, "yyyy-MM-dd HH:mm:ss") & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "', '" & txtketerangan.Text & "','" & diskon & "','" & 0 & "','" & 0 & "','" & total2 & "','CASH', 'CASH','" & bayar & "','" & sisa & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        ''MsgBox(metode)
        'If metode = "pembiayaan" Then
        '    'MsgBox("bayar pembiayaan")
        '    sql = "UPDATE tb_kas SET nontunai = nontunai + '" & total2 - bayar & "', tunai = tunai + '" & bayar & "' WHERE iduser = '" & fmenu.statususer.Text & "'"
        '    cmmd = New OdbcCommand(sql, cnn)
        '    dr = cmmd.ExecuteReader()
        'Else
        '    If metode = "CASH" Then
        '        'MsgBox("byr cash")
        '        sql = "UPDATE tb_kas SET tunai = tunai + '" & total2 & "' WHERE iduser = '" & fmenu.statususer.Text & "'"
        '        cmmd = New OdbcCommand(sql, cnn)
        '        dr = cmmd.ExecuteReader()
        '    Else
        '        'MsgBox("byr non tunai")
        '        sql = "UPDATE tb_kas SET nontunai = nontunai + '" & total2 & "' WHERE iduser = '" & fmenu.statususer.Text & "'"
        '        cmmd = New OdbcCommand(sql, cnn)
        '        dr = cmmd.ExecuteReader()
        '    End If
        'End If

        MsgBox("Transaksi Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
        Call awal()
        txtsisa.Text = 0

    End Sub
    Private Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
        "{impersonationLevel=impersonate}!\\" &
        computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
        "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
        cpu_ids.Substring(2)

        Return cpu_ids
    End Function
    'Sub printer()
    '    sql = "select * from tb_printer where cpuid='" & CpuId() & "'"
    '    cmmd = New OdbcCommand(sql, cnn)
    '    dr = cmmd.ExecuteReader
    '    If dr.HasRows Then
    '        faktur = dr("faktur")
    '        struk = dr("struk")
    '    Else
    '        MsgBox("Printer Belum di Setting", vbInformation.Information, "Error....")
    '        MsgBox("Gagal Mencetak", vbInformation.Information, "Error....")
    '    End If
    'End Sub
    Sub proses()
        For i As Integer = 0 To GridView1.RowCount - 1
            Call koneksii()
            sql = "SELECT * FROM tb_stok JOIN tb_barang ON tb_barang.kode_barang = tb_stok.kode_barang WHERE kode_stok ='" & GridView1.GetRowCellValue(i, "kode_stok") & "' "
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            dr.Read()

            Dim stok As Integer
            Dim stok1 As Integer
            stok = GridView1.GetRowCellValue(i, "banyak")
            stok1 = dr("jumlah_stok")
            If stok1 < stok Then
                MsgBox("Stok " + dr("nama_barang") + "dengan kode stok " + dr("kode_stok") + " tidak mencukupi", MsgBoxStyle.Information, "Information")
                Exit Sub
            End If
        Next

        Call simpan()
        ''If rbfaktur.Checked = True Then
        ''    Call cetak_faktur()
        ''    Call save()
        ''Else
        ''    Call cetak_struk()
        ''    Call save()
        ''End If

        ''Call cetak_struk()
        ''Call save()
        'fmsgbox.ShowDialog()
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 2
        fcarigudang.ShowDialog()
    End Sub

    Private Sub cmbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcustomer.SelectedIndexChanged
        Call caripelanggan()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        'If pembayaran = "Home Credit" Or pembayaran = "Spektra" Or pembayaran = "Adira" Or pembayaran = "Kredit Plus" Then
        '    Call proses()
        'Else
        '    If bayar < total2 Then
        '        MsgBox("Pembayaran tidak mencukupi")
        '    Else
        '        Call proses()
        '    End If
        'End If
        Call proses()
    End Sub

    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 2
        fcaricust.ShowDialog()
    End Sub

    Sub search()
        'tutup = 1
        'Dim panjang As Integer = txtkodeitem.Text.Length
        'fcaribarang.Show()
        'fcaribarang.txtcari.Focus()
        'fcaribarang.txtcari.DeselectAll()
        'fcaribarang.txtcari.SelectionStart = panjang
        'Me.txtkodeitem.Clear()
    End Sub
    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupstok = 1
        fcaristok.ShowDialog()
    End Sub
    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        If Not (Char.IsDigit(e.KeyChar)) And e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtkodeitem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtkodebarang.KeyDown
        'If e.KeyCode = Keys.F2 Then
        '   Call awal()
        'End If
    End Sub
    Private Sub txtbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbayar.KeyPress

        'If pembayaran = "CASH" Then
        If Not (Char.IsDigit(e.KeyChar)) And e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
        'Else
        'If e.KeyChar <> ChrW(Keys.F10) And e.KeyChar <> ChrW(Keys.F9) Then
        '        e.Handled = True
        '    End If
        'End If
    End Sub
    Private Sub txtbayar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbayar.KeyDown
        'If e.KeyCode = Keys.F9 Then
        '    Call fdiskon.ShowDialog()
        'End If
        'If e.KeyCode = Keys.F10 Then
        '    Call fmetodepembayaran.ShowDialog()
        'End If
    End Sub
End Class