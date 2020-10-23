Imports System.Data.Odbc

Public Class fcarilunasjual
    Dim pilih As String
    Dim kode As String

    Public tabellunas As DataTable

    Private Sub fcarilunasjual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
        'With GridView1
        '    'agar muncul footer untuk sum/avg/count
        '    .OptionsView.ShowFooter = True
        '    'buat sum harga
        '    .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
        'End With
        dtawal.Value = Now
        dtakhir.Value = Now
        'dtawal.MaxDate = Now
        'dtakhir.MaxDate = Now
    End Sub

    Sub grid()
        GridColumn1.Caption = "id penjualan"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Tanggal"
        GridColumn2.FieldName = "tgl_penjualan"
        GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn2.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn3.Caption = "Nama Pelanggan"
        GridColumn3.FieldName = "nama_pelanggan"

        GridColumn4.Caption = "Total Penjualan"
        GridColumn4.FieldName = "total_penjualan"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "##,#0"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_penjualan"

        GridColumn6.Caption = "Bayar"
        GridColumn6.FieldName = "bayar_penjualan"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Sisa"
        GridColumn7.FieldName = "sisa_penjualan"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridControl1.Visible = True
    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("id")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_piutang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn8.Caption = "id"
        GridColumn8.FieldName = "id"

        GridColumn9.Caption = "Tanggal"
        GridColumn9.FieldName = "tgl_pelunasan"

        GridColumn10.Caption = "Terima"
        GridColumn10.FieldName = "terima_piutang"
        GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn10.DisplayFormat.FormatString = "Rp ##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT tb_penjualan.id, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id))) AS sisa_penjualan 
                FROM tb_penjualan JOIN tb_pelanggan ON tb_penjualan.pelanggan_id = tb_pelanggan.id WHERE tb_penjualan.pelanggan_id='" & kodelunascustomer & "' AND tb_penjualan.total_penjualan <> tb_penjualan.bayar_penjualan AND tb_penjualan.lunas_penjualan = 0 AND DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT tb_penjualan.id, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id))) AS sisa_penjualan 
                FROM tb_penjualan JOIN tb_pelanggan ON tb_penjualan.pelanggan_id = tb_pelanggan.id WHERE tb_penjualan.pelanggan_id='" & kodelunascustomer & "' AND tb_penjualan.total_penjualan <> tb_penjualan.bayar_penjualan AND tb_penjualan.lunas_penjualan = 0 AND tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            End If
        Else
            sql = "SELECT tb_penjualan.id, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id))) AS sisa_penjualan 
                FROM tb_penjualan JOIN tb_pelanggan ON tb_penjualan.pelanggan_id = tb_pelanggan.id WHERE tb_penjualan.pelanggan_id='" & kodelunascustomer & "' AND tb_penjualan.total_penjualan <> tb_penjualan.bayar_penjualan AND tb_penjualan.lunas_penjualan = 0"
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
        If tutuplunasjual = 1 Then
            flunaspiutang.txtkodepenjualan.Text = Me.GridView1.GetFocusedRowCellValue("id")
        End If
        Me.Close()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("id")

        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE id = '" & kode & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            tabellunas.Rows.Add("DP : " & dr("id"), dr("last_updated"), dr("bayar_penjualan"))
        End If

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE penjualan_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("pelunasan_piutang_id"), dr("last_updated"), dr("terima_piutang"))
        End While

        GridControl2.RefreshDataSource()
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