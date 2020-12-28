Imports System.Data.Odbc

Public Class fjatuhtempopenjualan
    Dim kode As String
    Public tabellunas As DataTable

    '==== autosize form ====
    Dim CuRWidth As Integer = Me.Width
    Dim CuRHeight As Integer = Me.Height

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim RatioHeight As Double = (Me.Height - CuRHeight) / CuRHeight
        Dim RatioWidth As Double = (Me.Width - CuRWidth) / CuRWidth

        For Each ctrl As Control In Controls
            ctrl.Width += ctrl.Width * RatioWidth
            ctrl.Left += ctrl.Left * RatioWidth
            ctrl.Top += ctrl.Top * RatioHeight
            ctrl.Height += ctrl.Height * RatioHeight
        Next

        CuRHeight = Me.Height
        CuRWidth = Me.Width
    End Sub

    '=======================

    Private Sub fjatuhtempopenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call tabel_penjualan()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
            .Columns("bayar_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_penjualan", "{0:n0}")
            .Columns("sisa_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_penjualan", "{0:n0}")

        End With
    End Sub

    Sub grid_penjualan()
        GridColumn1.Caption = "No Nota"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Pelanggan"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "Tanggal Penjualan"
        GridColumn3.FieldName = "tgl_penjualan"
        'GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_penjualan"
        'GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total Penjualan"
        GridColumn5.FieldName = "total_penjualan"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

        GridColumn6.Caption = "Bayar"
        GridColumn6.FieldName = "bayar_penjualan"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Sisa"
        GridColumn7.FieldName = "sisa_penjualan"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("id")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_piutang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn8.Caption = "No Pelunasan"
        GridColumn8.FieldName = "id"

        GridColumn9.Caption = "Tanggal"
        GridColumn9.FieldName = "tgl_pelunasan"

        GridColumn10.Caption = "Terima"
        GridColumn10.FieldName = "terima_piutang"
        GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn10.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel_penjualan()
        Call koneksii()

        sql = "SELECT tb_penjualan.id, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.penjualan_id = tb_penjualan.id))) AS sisa_penjualan 
                FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.pelanggan_id = tb_pelanggan.id AND tb_penjualan.lunas_penjualan = 0 AND tb_penjualan.tgl_jatuhtempo_penjualan < now() AND tb_penjualan.total_penjualan > tb_penjualan.bayar_penjualan"

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid_penjualan()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("id")

        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE id = '" & kode & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            tabellunas.Rows.Add("PENJUALAN : " & dr("id"), dr("last_updated"), dr("bayar_penjualan"))
        End If

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE penjualan_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("id"), dr("last_updated"), dr("terima_piutang"))
        End While

        GridControl2.RefreshDataSource()
    End Sub

    Private Sub fjatuhtempopenjualan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Call tabel_penjualan()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_penjualan()
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub
End Class