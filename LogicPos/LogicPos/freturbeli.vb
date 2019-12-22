Imports System.Data.Odbc
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid

Public Class freturbeli
    Public tabel1, tabel2 As DataTable
    'variabel dalam penjualan
    Dim jenis, satuan, kodereturbeli As String
    Dim banyak As Double

    'variabel bantuan view pembelian
    'Dim nomornota, nomorcustomer, nomorsales, nomorgudang, viewketerangan, viewpembayaran As String
    'Dim statuslunas, statusvoid, statusprint, statusposted, statusedit As Boolean
    'Dim viewtglpenjualan, viewtgljatuhtempo As DateTime
    Dim nilaidiskon, nilaippn, nilaiongkir, nilaibayar As Double

    Private Sub freturbeli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        'Call printer()
        'Call cek_kas()

        'kodereturbeli = currentnumber()
        'Call inisialisasi(kodereturbeli)

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
            cnn.Close()
        End Try
        Return pesan
    End Function
    Sub simpan()
        Call koneksii()
        Dim koderetur As String = autonumber()
        cnn.Open()

        For i As Integer = 0 To GridView2.RowCount - 1
            sql = "UPDATE tb_stok SET jumlah_stok = jumlah_stok - '" & GridView2.GetRowCellValue(i, "qty") & "' WHERE kode_stok = '" & GridView2.GetRowCellValue(i, "kode_stok") & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        sql = "DELETE FROM tb_pembelian_detail WHERE kode_pembelian = '" & txtnonota.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        For i As Integer = 0 To GridView1.RowCount - 1
            sql = "INSERT INTO tb_pembelian_detail ( kode_pembelian, kode_barang, kode_stok, nama_barang,  jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by,date_created, last_updated) VALUES ('" & txtnonota.Text & "', '" & GridView1.GetRowCellValue(i, "kode_barang") & "', '" & GridView1.GetRowCellValue(i, "kode_stok") & "', '" & GridView1.GetRowCellValue(i, "nama_barang") & "','" & GridView1.GetRowCellValue(i, "jenis_barang") & "','" & GridView1.GetRowCellValue(i, "satuan_barang") & "','" & GridView1.GetRowCellValue(i, "qty") & "','" & GridView1.GetRowCellValue(i, "harga_beli") & "','" & GridView1.GetRowCellValue(i, "subtotal") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        Dim total_pembelian As Double = GridView1.Columns("subtotal").SummaryItem.SummaryValue
        sql = "UPDATE tb_pembelian SET total_pembelian = '" & total_pembelian & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        For i As Integer = 0 To GridView2.RowCount - 1
            sql = "INSERT INTO tb_retur_pembelian_detail (kode_retur, kode_barang, kode_stok, nama_barang, jenis_barang, satuan_barang, qty, harga_beli, subtotal, created_by, updated_by, date_created, last_updated) VALUES ('" & koderetur & "','" & GridView2.GetRowCellValue(i, "kode_barang") & "','" & GridView2.GetRowCellValue(i, "kode_stok") & "','" & GridView2.GetRowCellValue(i, "nama_barang") & "','" & GridView2.GetRowCellValue(i, "jenis_barang") & "','" & GridView2.GetRowCellValue(i, "satuan_barang") & "','" & GridView2.GetRowCellValue(i, "qty") & "','" & GridView2.GetRowCellValue(i, "harga_beli") & "','" & GridView2.GetRowCellValue(i, "subtotal") & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        Dim total_retur As Double = GridView2.Columns("subtotal").SummaryItem.SummaryValue
        sql = "INSERT INTO tb_retur_pembelian (kode_retur, kode_user,kode_pembelian ,total_retur ,created_by, updated_by, date_created, last_updated) VALUES ('" & koderetur & "','Testing','" & txtnonota.Text & "','" & total_retur & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        MsgBox("Retur Berhasil Dilakukan", MsgBoxStyle.Information, "Sukses")
        Call awalbaru()
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
        txtsupplier.Clear()
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

    Private Sub btncarinota_Click(sender As Object, e As EventArgs) Handles btncarinota.Click

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Call cari_nota()
    End Sub

    Private Sub btnbaru_Click(sender As Object, e As EventArgs) Handles btnbaru.Click

    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call simpan()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click

    End Sub

    Private Sub btnprev_Click(sender As Object, e As EventArgs) Handles btnprev.Click

    End Sub

    Private Sub btngoretur_Click(sender As Object, e As EventArgs) Handles btngoretur.Click

    End Sub

    Private Sub btnnext_Click(sender As Object, e As EventArgs) Handles btnnext.Click

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
                            GridView1.SetRowCellValue(lokasi2, "subtotal", (Val(banyak) + Val(banyak1)) * GridView1.GetRowCellValue(lokasi2, "harga_beli"))
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