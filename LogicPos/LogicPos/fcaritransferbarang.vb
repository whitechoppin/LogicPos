Imports System.Data.Odbc

Public Class fcaritransferbarang
    Dim pilih As String
    Dim kode As String

    Public tabelbarang As DataTable
    Private Sub fcaritransferbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
        'With GridView1
        '    'agar muncul footer untuk sum/avg/count
        '    .OptionsView.ShowFooter = True
        '    'buat sum harga
        '    .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
        'End With
        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_transfer_barang"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tanggal_transfer_barang"
        GridColumn3.Caption = "Dari Gudang"
        GridColumn3.FieldName = "kode_dari_gudang"
        GridColumn4.Caption = "Ke Gudang"
        GridColumn4.FieldName = "kode_ke_gudang"
        GridColumn5.Caption = "User"
        GridColumn5.FieldName = "kode_user"

        GridControl1.Visible = True
    End Sub

    Sub gridlunas()
        tabelbarang = New DataTable

        With tabelbarang
            .Columns.Add("kode_stok")
            .Columns.Add("kode_barang")
            .Columns.Add("qty", GetType(Double))
        End With

        GridControl2.DataSource = tabelbarang

        GridColumn6.Caption = "Kode Stok"
        GridColumn6.FieldName = "kode_stok"
        GridColumn6.Caption = "Kode Barang"
        GridColumn6.FieldName = "kode_barang"
        GridColumn7.Caption = "Qty"
        GridColumn7.FieldName = "qty"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If dtawal.Value.Equals(dtakhir.Value) Then
            sql = "SELECT tb_penjualan.kode_penjualan, tb_penjualan.total_penjualan, tb_penjualan.tgl_penjualan, tb_penjualan.keterangan_penjualan, tb_pelanggan.nama_pelanggan FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.kode_pelanggan='" & kodelunascustomer & "' AND tb_penjualan.total_penjualan <> tb_penjualan.bayar_penjualan AND tb_penjualan.lunas_penjualan = 0 AND DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT tb_penjualan.kode_penjualan, tb_penjualan.total_penjualan, tb_penjualan.tgl_penjualan, tb_penjualan.keterangan_penjualan, tb_pelanggan.nama_pelanggan FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.kode_pelanggan='" & kodelunascustomer & "' AND tb_penjualan.total_penjualan <> tb_penjualan.bayar_penjualan AND tb_penjualan.lunas_penjualan = 0 AND tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutuplunasjual = 1 Then
            flunaspiutang.txtkodepenjualan.Text = Me.GridView1.GetFocusedRowCellValue("kode_penjualan")
        End If
        Me.Close()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_transfer_barang")

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE kode_penjualan ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabelbarang.Rows.Add(dr("kode_lunas"), dr("last_updated"), dr("terima_piutang"))
        End While

        GridControl2.RefreshDataSource()
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call tabel_lunas()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click

    End Sub
End Class