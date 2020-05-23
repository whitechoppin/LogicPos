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
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_penjualan"

        GridColumn2.Caption = "Pelanggan"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "Tanggal Penjualan"
        GridColumn3.FieldName = "tgl_penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_penjualan"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

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
            .Columns.Add("kode_lunas")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_piutang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn6.Caption = "Kode"
        GridColumn6.FieldName = "kode_lunas"
        GridColumn7.Caption = "Tanggal"
        GridColumn7.FieldName = "tgl_pelunasan"
        GridColumn8.Caption = "Terima"
        GridColumn8.FieldName = "terima_piutang"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "Rp ##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel_penjualan()
        Call koneksii()

        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan = tb_penjualan.kode_pelanggan WHERE tb_penjualan.lunas_penjualan = 0 AND tb_penjualan.tgl_jatuhtempo_penjualan < now() AND tb_penjualan.total_penjualan > tb_penjualan.bayar_penjualan"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid_penjualan()
            cnn.Close()
        End Using
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_penjualan")

        Call koneksii()
        sql = "SELECT * FROM tb_penjualan WHERE kode_penjualan = '" & kode & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            tabellunas.Rows.Add("DP : " + dr("kode_penjualan"), dr("last_updated"), dr("bayar_penjualan"))
        End If

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_piutang_detail WHERE kode_penjualan ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("kode_lunas"), dr("last_updated"), dr("terima_piutang"))
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

    End Sub
End Class