Imports System.Data.Odbc

Public Class fcarilunasbeli
    Dim pilih As String
    Dim kode As String

    Public tabellunas As DataTable
    Private Sub fcarilunasbeli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
        'With GridView1
        '    'agar muncul footer untuk sum/avg/count
        '    .OptionsView.ShowFooter = True
        '    'buat sum harga
        '    .Columns("total_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_pembelian", "{0:n0}")
        'End With
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
        GridColumn6.Caption = "Keterangan"
        GridColumn6.FieldName = "keterangan_pembelian"

        GridControl1.Visible = True
    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("kode_lunas")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn7.Caption = "Kode"
        GridColumn7.FieldName = "kode_lunas"
        GridColumn8.Caption = "Tanggal"
        GridColumn8.FieldName = "tgl_pelunasan"
        GridColumn9.Caption = "Terima"
        GridColumn9.FieldName = "terima_utang"
        GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn9.DisplayFormat.FormatString = "Rp ##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_pembelian.keterangan_pembelian, tb_pembelian.no_nota_pembelian, tb_supplier.nama_supplier FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier AND tb_pembelian.kode_supplier='" & kodelunassupplier & "' AND tb_pembelian.lunas_pembelian =0"
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
        Me.Close()
    End Sub
    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE kode_pembelian ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("kode_lunas"), dr("last_updated"), dr("terima_utang"))
            GridControl2.RefreshDataSource()
        End While
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call tabel_lunas()
    End Sub
End Class