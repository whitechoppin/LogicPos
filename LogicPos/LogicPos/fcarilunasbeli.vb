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
        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "id pembelian"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_pembelian"
        GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn2.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn3.Caption = "Nama supplier"
        GridColumn3.FieldName = "nama_supplier"

        GridColumn4.Caption = "Total pembelian"
        GridColumn4.FieldName = "total_pembelian"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "##,#0"

        GridColumn5.Caption = "No Nota Pembelian"
        GridColumn5.FieldName = "no_nota_pembelian"

        GridColumn6.Caption = "Keterangan"
        GridColumn6.FieldName = "keterangan_pembelian"

        GridColumn7.Caption = "Bayar"
        GridColumn7.FieldName = "bayar_pembelian"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridColumn8.Caption = "Sisa"
        GridColumn8.FieldName = "sisa_pembelian"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "##,##0"

        GridControl1.Visible = True
    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("id")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn9.Caption = "id"
        GridColumn9.FieldName = "id"

        GridColumn10.Caption = "Tanggal"
        GridColumn10.FieldName = "tgl_pelunasan"

        GridColumn11.Caption = "Terima"
        GridColumn11.FieldName = "terima_utang"
        GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn11.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian, no_nota_pembelian, keterangan_pembelian 
                    FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & kodelunassupplier & "' AND tb_pembelian.lunas_pembelian =0 AND DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian, no_nota_pembelian, keterangan_pembelian 
                    FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & kodelunassupplier & "' AND tb_pembelian.lunas_pembelian =0 AND tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            End If
        Else
            sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian, no_nota_pembelian, keterangan_pembelian 
                   FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & kodelunassupplier & "' AND tb_pembelian.lunas_pembelian =0"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()
        Call grid()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutuplunasbeli = 1 Then
            flunasutang.txtkodepembelian.Text = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Close()
    End Sub
    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("id")

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE pembelian_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("pelunasan_utang_id"), dr("last_updated"), dr("terima_utang"))
            GridControl2.RefreshDataSource()
        End While
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call tabel_lunas()
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