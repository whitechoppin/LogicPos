Imports System.Data.Odbc

Public Class fcaripembelian
    Dim pilih As String
    Dim kode As String
    Private Sub fcaripembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_pembelian"
        GridColumn2.Caption = "Total Pembelian"
        GridColumn2.FieldName = "total_pembelian"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian FROM tb_pembelian"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupbeli = 1 Then
            freturbeli.txtnonota.Text = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")
        End If
        Me.Hide()
    End Sub
End Class