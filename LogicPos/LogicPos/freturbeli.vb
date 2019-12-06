Imports System.Data.Odbc
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid

Public Class freturbeli
    Public tabel1, tabel2 As DataTable
    'variabel dalam penjualan
    Dim jenis, satuan, kodepenjualan As String
    Dim banyak As Double
    'variabel bantuan view penjualan
    'Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran As String
    'Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    'Dim viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double
    'variabel edit penjualan
    'Dim countingbarang As Integer
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
    Sub simpan()
        Call koneksii()
        Dim koderetur As String = autonumber()
        cnn.Open()

        For i As Integer = 0 To GridView2.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok + '" & GridView2.GetRowCellValue(i, "banyak") & "' WHERE kode_stok = '" & GridView2.GetRowCellValue(i, "kode_stok") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "delete from tb_penjualan_detail where kode_penjualan = '" & txtnonota.Text & "'"
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
    Private Sub btnproses_Click(sender As Object, e As EventArgs) Handles btnproses.Click
        Call simpan()
    End Sub

    Private Sub freturbeli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()
        'kodepenjualan = currentnumber()
        'Call inisialisasi(kodepenjualan)
        Call awalbaru()
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
    End Sub
    Sub awalbaru()
        'header
        txtnonota.Clear()
        txtnonota.Enabled = True

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False

        'buat tabel
        Call tabel_utama()
        Call tabel_retur()
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

    End Sub
    Sub previewpembelian(lihat As String)
        sql = "SELECT * FROM tb_pembelian_detail WHERE kode_pembelian ='" & lihat & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabel1.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_beli")), Val(dr("subtotal")))
            'tabel.Rows.Add(dr("kode_stok"), dr("kode_barang"), dr("nama_barang"), dr("qty"), dr("satuan_barang"), dr("jenis_barang"), Val(dr("harga_jual")), dr("diskon"), 0, dr("harga_diskon"), dr("subtotal"), 0, 0)
            GridControl1.RefreshDataSource()
        End While

    End Sub
    Sub cari_nota()
        Call koneksii()
        sql = "Select * From tb_pembelian where tb_pembelian.kode_pembelian = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            'jika ditemukan
            Call previewpembelian(txtnonota.Text)
        End If

    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call cari_nota()
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        fretbeli.kode_barang = GridView1.GetFocusedRowCellValue("kode_barang")
        fretbeli.kode_stok = GridView1.GetFocusedRowCellValue("kode_stok")
        fretbeli.nama_barang = GridView1.GetFocusedRowCellValue("nama_barang")
        fretbeli.satuan_barang = GridView1.GetFocusedRowCellValue("satuan_barang")
        fretbeli.jenis_barang = GridView1.GetFocusedRowCellValue("jenis_barang")
        fretbeli.banyak = GridView1.GetFocusedRowCellValue("qty")
        fretbeli.harga_beli = GridView1.GetFocusedRowCellValue("harga_beli")

        fretbeli.subtotal = GridView1.GetFocusedRowCellValue("subtotal")
        'GridView1.DeleteRow(GridView1.GetRowHandle(info.RowHandle))
        fretbeli.ShowDialog()
    End Sub
    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Delete Then
            Dim kode_stok As String = GridView1.GetFocusedRowCellValue("kode_stok")

            Dim kode_stok2 As String = GridView2.GetFocusedRowCellValue("kode_stok")
            Dim banyak2 As Integer = GridView2.GetFocusedRowCellValue("qty")
            MsgBox(kode_stok2)
            For i As Integer = 0 To GridView1.RowCount - 1
                Dim lokasi1 As Integer
                If GridView1.GetRowCellValue(i, "kode_stok") = kode_stok2 Then
                    For a As Integer = 0 To GridView1.RowCount - 1
                        Dim banyak As Integer = GridView1.GetRowCellValue(a, "qty")
                        Dim lokasi2 As Integer
                        If GridView1.GetRowCellValue(a, "kode_stok") = kode_stok2 Then
                            lokasi2 = a
                            Dim banyak1 As Integer = GridView2.GetFocusedRowCellValue("qty")
                            GridView1.SetRowCellValue(lokasi2, "qty", Val(banyak) + Val(banyak1))
                            'tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("banyak"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_satuan"), GridView2.GetFocusedRowCellValue("diskon_persen"), GridView2.GetFocusedRowCellValue("diskon_nominal"), GridView2.GetFocusedRowCellValue("harga_diskon"), GridView2.GetFocusedRowCellValue("subtotal"), GridView2.GetFocusedRowCellValue("laba"), GridView2.GetFocusedRowCellValue("modal_barang"))
                            GridControl1.RefreshDataSource()
                            GridView2.DeleteSelectedRows()
                            Exit Sub
                        End If
                    Next

                End If
            Next

            For i As Integer = 0 To GridView1.RowCount - 1
                Dim lokasi1 As Integer
                If GridView1.GetRowCellValue(i, "kode_stok") <> kode_stok2 Then
                    lokasi1 = i
                    Dim banyak1 As Integer = GridView2.GetFocusedRowCellValue("qty")
                    'GridView1.SetRowCellValue(lokasi1, "banyak", Val(banyak) + Val(banyak1))
                    tabel1.Rows.Add(GridView2.GetFocusedRowCellValue("kode_stok"), GridView2.GetFocusedRowCellValue("kode_barang"), GridView2.GetFocusedRowCellValue("nama_barang"), GridView2.GetFocusedRowCellValue("qty"), GridView2.GetFocusedRowCellValue("satuan_barang"), GridView2.GetFocusedRowCellValue("jenis_barang"), GridView2.GetFocusedRowCellValue("harga_beli"), GridView2.GetFocusedRowCellValue("subtotal"))
                    GridView2.DeleteSelectedRows()
                    Exit Sub
                End If
            Next
        End If
    End Sub
End Class