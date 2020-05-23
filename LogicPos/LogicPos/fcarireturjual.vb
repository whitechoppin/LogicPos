Imports System.Data.Odbc

Public Class fcarireturjual
    Dim pilih As String
    Dim kode As String
    Private Sub fcarireturjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode Retur"
        GridColumn1.FieldName = "kode_retur"
        GridColumn2.Caption = "Kode User"
        GridColumn2.FieldName = "kode_user"
        GridColumn3.Caption = "Kode Penjualan"
        GridColumn3.FieldName = "kode_penjualan"
        GridColumn4.Caption = "Tanggal"
        GridColumn4.FieldName = "tgl_returjual"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If dtawal.Value.Equals(dtakhir.Value) Then
            sql = "SELECT * FROM tb_retur_penjualan WHERE DATE(tgl_returjual) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_retur_penjualan WHERE tgl_returjual BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupreturjual = 1 Then
            freturjual.txtgoretur.Text = Me.GridView1.GetFocusedRowCellValue("kode_retur")
        End If
    End Sub
End Class