Imports System.Data.Odbc

Public Class fcarigudang
    Dim pilih As String
    Dim kode As String
    Private Sub fcarigudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_gudang"
        GridColumn2.Caption = "Nama Gudang"
        GridColumn2.FieldName = "nama_gudang"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "Select tb_gudang.kode_gudang, tb_gudang.nama_gudang from tb_gudang"
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
        If tutupgudang = 1 Then
            fpembelian.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        ElseIf tutupgudang = 2 Then
            fpenjualan.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        ElseIf tutupgudang = 3 Then
            fbarangkeluar.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        ElseIf tutupgudang = 4 Then
            fbarangmasuk.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        ElseIf tutupgudang = 5 Then
            ftransferbarang.cmbdarigudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        ElseIf tutupgudang = 6 Then
            ftransferbarang.cmbkegudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        End If
        Me.Hide()
    End Sub
End Class