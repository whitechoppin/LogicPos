Imports System.Data.Odbc

Public Class fcarilunasbeli
    Dim pilih As String
    Dim kode As String
    Private Sub fcarilunasbeli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_pembelian"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_pembelian"
        GridColumn3.Caption = "Nama supplier"
        GridColumn3.FieldName = "nama_supplier"
        GridColumn4.Caption = "Total pembelian"
        GridColumn4.FieldName = "total_pembelian"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "Rp ##,#0"
        GridColumn5.Caption = "No Nota Pembelian"
        GridColumn5.FieldName = "no_nota_pembelian"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_supplier.nama_supplier FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier AND tb_pembelian.kode_supplier='" & kodelunassupplier & "' AND tb_pembelian.lunas_pembelian =0"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutuplunasbeli = 1 Then
            flunasutang.txtkodepembelian.Text = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")
        End If
        Me.Hide()
    End Sub
End Class