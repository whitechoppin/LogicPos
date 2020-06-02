Imports System.Data.Odbc

Public Class fcarilunasutang
    Dim pilih As String
    Dim kode As String

    Private Sub fcarilunasutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_lunas"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tanggal_transaksi"
        GridColumn3.Caption = "Nama Supplier"
        GridColumn3.FieldName = "nama_supplier"
        GridColumn4.Caption = "Bayar"
        GridColumn4.FieldName = "bayar_lunas"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "##,#0"
        GridColumn5.Caption = "No Bukti"
        GridColumn5.FieldName = "no_bukti"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If dtawal.Value.Equals(dtakhir.Value) Then
            sql = "SELECT tb_pelunasan_utang.kode_lunas, tb_pelunasan_utang.bayar_lunas, tb_pelunasan_utang.tanggal_transaksi, tb_supplier.nama_supplier, tb_pelunasan_utang.no_bukti FROM tb_pelunasan_utang JOIN tb_supplier WHERE tb_pelunasan_utang.kode_supplier = tb_supplier.kode_supplier AND DATE(tb_pelunasan_utang.tanggal_transaksi) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT tb_pelunasan_utang.kode_lunas, tb_pelunasan_utang.bayar_lunas, tb_pelunasan_utang.tanggal_transaksi, tb_supplier.nama_supplier, tb_pelunasan_utang.no_bukti FROM tb_pelunasan_utang JOIN tb_supplier WHERE tb_pelunasan_utang.kode_supplier = tb_supplier.kode_supplier AND tb_pelunasan_utang.tanggal_transaksi BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcaripelunasanutang = 1 Then
            flunasutang.txtgolunas.Text = Me.GridView1.GetFocusedRowCellValue("kode_lunas")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class