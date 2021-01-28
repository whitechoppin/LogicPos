Imports System.Data.Odbc

Public Class fcarikas
    Dim pilih As String
    Dim kode As String
    Private Sub fcarikas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_kas"
        GridColumn2.Caption = "Nama kas"
        GridColumn2.FieldName = "nama_kas"
        GridColumn3.Caption = "Keterangan kas"
        GridColumn3.FieldName = "keterangan_kas"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()
        'Using cnn As New OdbcConnection(strConn)
        sql = "SELECT tb_kas.kode_kas, tb_kas.nama_kas, tb_kas.keterangan_kas FROM tb_kas"
        da = New OdbcDataAdapter(sql, cnn)
        'cnn.Open()
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
        'cnn.Close()
        'End Using
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupkas = 1 Then
            fpenjualan.cmbpembayaran.Text = Me.GridView1.GetFocusedRowCellValue("kode_kas")
        ElseIf tutupkas = 2 Then

        End If
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class