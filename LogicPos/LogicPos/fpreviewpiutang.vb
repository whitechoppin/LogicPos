Imports System.Data.Odbc

Public Class fpreviewpiutang
    Dim kode As String
    Dim status As Integer
    Public tabellunas As DataTable

    Private Sub fpreviewpiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call comboboxcustomer()
        Call tabel_penjualan()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("total_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_penjualan", "{0:n0}")
            .Columns("bayar_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_penjualan", "{0:n0}")
            .Columns("sisa_penjualan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_penjualan", "{0:n0}")
        End With
    End Sub

    Sub comboboxcustomer()
        Call koneksii()
        cmbcustomer.Items.Clear()
        cmbcustomer.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_pelanggan", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbcustomer.AutoCompleteCustomSource.Add(dr("kode_pelanggan"))
                cmbcustomer.Items.Add(dr("kode_pelanggan"))
            End While
        End If
    End Sub

    Sub grid_penjualan()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_penjualan"

        GridColumn2.Caption = "Customer"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "Tanggal penjualan"
        GridColumn3.FieldName = "tgl_penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_penjualan"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total penjualan"
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

        GridColumn8.Caption = "Kode"
        GridColumn8.FieldName = "kode_lunas"
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

        If dtawal.Value.Equals(dtakhir.Value) Then
            If cmbcustomer.Text.Length > 0 And cmbstatus.Text.Length = 0 Then
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.kode_pelanggan='" & cmbcustomer.Text & "' AND DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            ElseIf cmbcustomer.Text.Length = 0 And cmbstatus.Text.Length > 0 Then
                If cmbstatus.Text.Equals("Lunas") Then
                    status = 1
                Else
                    status = 0
                End If
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelangganAND tb_penjualan.lunas_penjualan =" & status & " AND DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            ElseIf cmbcustomer.Text.Length > 0 And cmbstatus.Text.Length > 0 Then
                If cmbstatus.Text.Equals("Lunas") Then
                    status = 1
                Else
                    status = 0
                End If
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.kode_pelanggan='" & cmbcustomer.Text & "' AND tb_penjualan.lunas_penjualan =" & status & " AND DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            ElseIf cmbcustomer.Text.Length = 0 And cmbstatus.Text.Length = 0 Then
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND DATE(tb_penjualan.tgl_penjualan) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            End If
        Else

            If cmbcustomer.Text.Length > 0 And cmbstatus.Text.Length = 0 Then
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.kode_pelanggan='" & cmbcustomer.Text & "' AND tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            ElseIf cmbcustomer.Text.Length = 0 And cmbstatus.Text.Length > 0 Then
                If cmbstatus.Text.Equals("Lunas") Then
                    status = 1
                Else
                    status = 0
                End If
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.lunas_penjualan =" & status & " AND tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            ElseIf cmbcustomer.Text.Length > 0 And cmbstatus.Text.Length > 0 Then
                If cmbstatus.Text.Equals("Lunas") Then
                    status = 1
                Else
                    status = 0
                End If
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.kode_pelanggan='" & cmbcustomer.Text & "' AND tb_penjualan.lunas_penjualan =" & status & " AND tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            ElseIf cmbcustomer.Text.Length = 0 And cmbstatus.Text.Length = 0 Then
                sql = "SELECT kode_penjualan, nama_pelanggan, tgl_penjualan, tgl_jatuhtempo_penjualan, total_penjualan, bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan) as bayar_penjualan, (total_penjualan - (bayar_penjualan + (SELECT IFNULL(SUM(terima_piutang), 0) FROM tb_pelunasan_piutang_detail WHERE tb_pelunasan_piutang_detail.kode_penjualan = tb_penjualan.kode_penjualan))) AS sisa_penjualan 
                        FROM tb_penjualan JOIN tb_pelanggan WHERE tb_penjualan.kode_pelanggan = tb_pelanggan.kode_pelanggan AND tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            End If

        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        Call grid_penjualan()
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

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_penjualan()
    End Sub

    Private Sub fpreviewpiutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub
End Class