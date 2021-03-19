Imports System.Data.Odbc

Public Class fcarisupplier
    Dim pilih As String
    Dim kode As String
    Private Sub fcarisupp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub

    Sub tabel()
        Call koneksi()
        sql = "SELECT * from tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_supplier"

        GridColumn2.Caption = "Nama Supplier"
        GridColumn2.FieldName = "nama_supplier"

        GridColumn3.Caption = "id"
        GridColumn3.FieldName = "id"
        GridColumn3.Visible = False

        GridControl1.Visible = True
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupsup = 1 Then
            fpembelian.cmbsupplier.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        ElseIf tutupsup = 2 Then
            fbarangmasuk.cmbsupplier.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        ElseIf tutupsup = 3 Then
            fpreviewutang.cmbsupplier.SelectedValue = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class