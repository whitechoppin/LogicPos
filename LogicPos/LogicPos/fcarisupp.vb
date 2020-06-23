Imports System.Data.Odbc

Public Class fcarisupp
    Dim pilih As String
    Dim kode As String
    Private Sub fcarisupp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_supplier"
        GridColumn2.Caption = "Nama Supplier"
        GridColumn2.FieldName = "nama_supplier"
        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "Select tb_supplier.kode_supplier, tb_supplier.nama_supplier from tb_supplier"
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
        If tutupsup = 1 Then
            fpembelian.cmbsupplier.Text = Me.GridView1.GetFocusedRowCellValue("kode_supplier")
        ElseIf tutupsup = 2 Then
            fbarangmasuk.cmbsupplier.Text = Me.GridView1.GetFocusedRowCellValue("kode_supplier")
        End If
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class