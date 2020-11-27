Imports System.Data.Odbc

Public Class fpreviewutang
    Dim kode As String
    Dim status As Integer
    Public tabellunas As DataTable

    '==== autosize form ====
    Dim CuRWidth As Integer = Me.Width
    Dim CuRHeight As Integer = Me.Height

    Dim idsupplier As Integer

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

    Private Sub fpreviewutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Call comboboxsupplier()
        Call tabel_pembelian()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("total_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_pembelian", "{0:n0}")
            .Columns("bayar_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "bayar_pembelian", "{0:n0}")
            .Columns("sisa_pembelian").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "sisa_pembelian", "{0:n0}")
        End With
    End Sub

    Sub comboboxsupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsupplier.DataSource = ds.Tables(0)
        cmbsupplier.ValueMember = "id"
        cmbsupplier.DisplayMember = "kode_supplier"
    End Sub

    Sub carisupplier()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier ='" & cmbsupplier.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idsupplier = dr("id")
        Else

        End If
    End Sub

    Sub grid_pembelian()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Supplier"
        GridColumn2.FieldName = "nama_supplier"

        GridColumn3.Caption = "Tanggal pembelian"
        GridColumn3.FieldName = "tgl_pembelian"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tgl_jatuhtempo_pembelian"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Total pembelian"
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
            .Columns.Add("id")
            .Columns.Add("tgl_pelunasan")
            .Columns.Add("terima_utang", GetType(Double))
        End With

        GridControl2.DataSource = tabellunas

        GridColumn8.Caption = "id"
        GridColumn8.FieldName = "id"

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

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                If cmbsupplier.Text.Length > 0 And cmbstatus.Text.Length = 0 Then
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & idsupplier & "' AND DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
                ElseIf cmbsupplier.Text.Length = 0 And cmbstatus.Text.Length > 0 Then
                    If cmbstatus.SelectedIndex = 0 Then
                        status = 1
                    ElseIf cmbstatus.SelectedIndex = 1 Then
                        status = 0
                    End If
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.lunas_pembelian ='" & status & "' AND DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
                ElseIf cmbsupplier.Text.Length > 0 And cmbstatus.Text.Length > 0 Then
                    If cmbstatus.SelectedIndex = 0 Then
                        status = 1
                    ElseIf cmbstatus.SelectedIndex = 1 Then
                        status = 0
                    End If
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & idsupplier & "' AND tb_pembelian.lunas_pembelian ='" & status & "' AND DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
                ElseIf cmbsupplier.Text.Length = 0 And cmbstatus.Text.Length = 0 Then
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE DATE(tb_pembelian.tgl_pembelian) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
                End If
            Else

                If cmbsupplier.Text.Length > 0 And cmbstatus.Text.Length = 0 Then
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & idsupplier & "' AND tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
                ElseIf cmbsupplier.Text.Length = 0 And cmbstatus.Text.Length > 0 Then
                    If cmbstatus.SelectedIndex = 0 Then
                        status = 1
                    ElseIf cmbstatus.SelectedIndex = 1 Then
                        status = 0
                    End If
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.lunas_pembelian ='" & status & "' AND tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
                ElseIf cmbsupplier.Text.Length > 0 And cmbstatus.Text.Length > 0 Then
                    If cmbstatus.SelectedIndex = 0 Then
                        status = 1
                    ElseIf cmbstatus.SelectedIndex = 1 Then
                        status = 0
                    End If
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & idsupplier & "' AND tb_pembelian.lunas_pembelian ='" & status & "' AND tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
                ElseIf cmbsupplier.Text.Length = 0 And cmbstatus.Text.Length = 0 Then
                    sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
                End If

            End If
        Else
            '==================================================================================================================================================================================================================================================================================================================================================================================================================================================================================

            If cmbsupplier.Text.Length > 0 And cmbstatus.Text.Length = 0 Then
                sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & idsupplier & "'"
            ElseIf cmbsupplier.Text.Length = 0 And cmbstatus.Text.Length > 0 Then
                If cmbstatus.SelectedIndex = 0 Then
                    status = 1
                ElseIf cmbstatus.SelectedIndex = 1 Then
                    status = 0
                End If
                sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.lunas_pembelian ='" & status & "'"
            ElseIf cmbsupplier.Text.Length > 0 And cmbstatus.Text.Length > 0 Then
                If cmbstatus.SelectedIndex = 0 Then
                    status = 1
                ElseIf cmbstatus.SelectedIndex = 1 Then
                    status = 0
                End If
                sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id WHERE tb_pembelian.supplier_id='" & idsupplier & "' AND tb_pembelian.lunas_pembelian ='" & status & "'"
            ElseIf cmbsupplier.Text.Length = 0 And cmbstatus.Text.Length = 0 Then
                sql = "SELECT tb_pembelian.id, nama_supplier, tgl_pembelian, tgl_jatuhtempo_pembelian, total_pembelian, (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id) as bayar_pembelian, (total_pembelian - (SELECT IFNULL(SUM(terima_utang), 0) FROM tb_pelunasan_utang_detail WHERE tb_pelunasan_utang_detail.pembelian_id = tb_pembelian.id)) AS sisa_pembelian 
                        FROM tb_pembelian JOIN tb_supplier ON tb_pembelian.supplier_id = tb_supplier.id"
            End If
        End If


        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridControl2.DataSource = Nothing
        GridControl2.RefreshDataSource()

        Call grid_pembelian()
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
        End While

        GridControl2.RefreshDataSource()
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

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_pembelian()
    End Sub

    Private Sub fpreviewutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        Call tabel_lunas()
    End Sub

    Sub ExportToExcel1()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            GridView1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Sub ExportToExcel2()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            GridView2.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Private Sub btnexcel1_Click(sender As Object, e As EventArgs) Handles btnexcel1.Click
        Call ExportToExcel1()
    End Sub

    Private Sub btnexcel2_Click(sender As Object, e As EventArgs) Handles btnexcel2.Click
        Call ExportToExcel2()
    End Sub

    Private Sub cmbsupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsupplier.SelectedIndexChanged
        Call carisupplier()
    End Sub

    Private Sub cmbsupplier_TextChanged(sender As Object, e As EventArgs) Handles cmbsupplier.TextChanged
        Call carisupplier()
    End Sub
End Class