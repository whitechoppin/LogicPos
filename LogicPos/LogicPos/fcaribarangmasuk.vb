Imports System.Data.Odbc

Public Class fcaribarangmasuk
    Dim pilih As String
    Dim kode As String
    Private Sub fcaribarangmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode Barang Masuk"
        GridColumn1.FieldName = "kode_barang_masuk"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_barang_masuk"
        GridColumn3.Caption = "Kode Supplier"
        GridColumn3.FieldName = "kode_supplier"
        GridColumn4.Caption = "Tgl Barang Masuk"
        GridColumn4.FieldName = "tgl_barang_masuk"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_barang_masuk"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If dtawal.Value.Equals(dtakhir.Value) Then
            sql = "SELECT * FROM tb_barang_masuk WHERE DATE(tgl_barang_masuk) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_barang_masuk WHERE tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupcaribarangmasuk = 1 Then
            fbarangmasuk.txtgobarangmasuk.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang_masuk")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class