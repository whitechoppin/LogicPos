Imports System.Data.Odbc

Public Class fjatuhtempopembelian
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
    Private Sub fjatuhtempopembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call tabel_pembelian()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("total_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_pembelian", "{0:n0}")
            .Columns("bayar_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_pembelian", "{0:n0}")
            .Columns("sisa_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_pembelian", "{0:n0}")
        End With
    End Sub

    Sub grid_pembelian()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Supplier"
        GridColumn2.FieldName = "nama_supplier"

        GridColumn3.Caption = "Tanggal Pembelian"
        GridColumn3.FieldName = "tgl_pembelian"
        'GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_pembelian"
        'GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total Pembelian"
        GridColumn5.FieldName = "total_pembelian"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

        GridColumn6.Caption = "Bayar"
        GridColumn6.FieldName = "bayar_pembelian"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Sisa"
        GridColumn7.FieldName = "sisa_pembelian"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

    End Sub

    Sub gridlunas()
        tabellunas = New DataTable

        With tabellunas
            .Columns.Add("kode_lunas")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn8.Caption = "Kode"
        GridColumn8.FieldName = "kode_lunas"

        GridColumn9.Caption = "Tanggal"
        GridColumn9.FieldName = "tgl_pelunasan"

        GridColumn10.Caption = "Terima"
        GridColumn10.FieldName = "terima_utang"
        GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn10.DisplayFormat.FormatString = "##,#0"

        GridControl2.Visible = True
    End Sub
    Sub tabel_pembelian()
        Call koneksii()

        sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.lunas_pembelian = 0 AND tb_pembelian.tgl_jatuhtempo_pembelian < now()"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid_pembelian()
    End Sub

    Sub tabel_lunas()
        Call gridlunas()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_pembelian")

        Call koneksii()
        sql = "SELECT * FROM tb_pelunasan_utang_detail WHERE pembelian_id ='" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        While dr.Read
            tabellunas.Rows.Add(dr("kode_lunas"), dr("last_updated"), dr("terima_utang"))
        End While

        GridControl2.RefreshDataSource()
    End Sub

    Private Sub fjatuhtempopembelian_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Call tabel_pembelian()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_pembelian()
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub
End Class