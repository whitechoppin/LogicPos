Imports System.Data.Odbc

Public Class fcaripembelian
    Dim pilih As String
    Dim kode As String
    Private Sub fcaripembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()

        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_pembelian"
        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_pembelian"
        GridColumn3.Caption = "Nama Supplier"
        GridColumn3.FieldName = "nama_supplier"
        GridColumn4.Caption = "Total Pembelian"
        GridColumn4.FieldName = "total_pembelian"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "##,#0"
        GridColumn5.Caption = "No Nota Pembelian"
        GridColumn5.FieldName = "no_nota_pembelian"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If cbperiode.Checked = True Then
            If dtawal.Value.Equals(dtakhir.Value) Then
                sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_supplier.nama_supplier, tb_pembelian.no_nota_pembelian FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier AND DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_supplier.nama_supplier, tb_pembelian.no_nota_pembelian FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier AND tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            End If
        Else
            sql = "SELECT tb_pembelian.kode_pembelian, tb_pembelian.total_pembelian, tb_pembelian.tgl_pembelian, tb_supplier.nama_supplier, tb_pembelian.no_nota_pembelian FROM tb_pembelian JOIN tb_supplier WHERE tb_pembelian.kode_supplier = tb_supplier.kode_supplier"
        End If


        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupbeli = 1 Then
            freturbeli.txtnonota.Text = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")
        ElseIf tutupbeli = 2 Then
            fpembelian.txtgopembelian.Text = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")
        End If
        Me.Hide()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub

    Private Sub cbperiode_CheckedChanged(sender As Object, e As EventArgs) Handles cbperiode.CheckedChanged
        If cbperiode.Checked = True Then
            dtawal.Enabled = True
            dtakhir.Enabled = True
        Else
            dtawal.Enabled = False
            dtakhir.Enabled = False
        End If
    End Sub
End Class