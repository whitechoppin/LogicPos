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
        GridColumn1.Width = 15

        GridColumn2.Caption = "Nama Gudang"
        GridColumn2.FieldName = "nama_gudang"
        GridColumn2.Width = 35

        GridColumn3.Caption = "Alamat Gudang"
        GridColumn3.FieldName = "alamat_gudang"
        GridColumn3.Width = 55

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridControl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridControl1.KeyPress
        If e.KeyChar = Strings.Chr(Keys.Enter) Then
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
            ElseIf tutupgudang = 7 Then
                flaporanmutasibarang.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
            ElseIf tutupgudang = 8 Then
                fpenyesuaianstok.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
            End If
            Me.Hide()
        End If
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
        ElseIf tutupgudang = 7 Then
            flaporanmutasibarang.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        ElseIf tutupgudang = 8 Then
            fpenyesuaianstok.cmbgudang.Text = Me.GridView1.GetFocusedRowCellValue("kode_gudang")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class