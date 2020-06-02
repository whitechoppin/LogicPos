Imports System.Data.Odbc

Public Class fcaribarangkeluar
    Dim pilih As String
    Dim kode As String
    Private Sub fcaribarangkeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_barang_keluar"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_barang_keluar"
        GridColumn3.Caption = "Nama Pelanggan"
        GridColumn3.FieldName = "nama_pelanggan"
        GridColumn4.Caption = "Tgl Barang Keluar"
        GridColumn4.FieldName = "tgl_barang_keluar"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_barang_keluar"

        GridControl1.Visible = True
    End Sub

    Sub tabel()
        Call koneksii()

        If dtawal.Value.Equals(dtakhir.Value) Then
            sql = "SELECT * FROM tb_barang_keluar WHERE DATE(tgl_barang_keluar) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_barang_keluar WHERE tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcaribarangkeluar = 1 Then
            fbarangkeluar.txtgobarangkeluar.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang_keluar")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class