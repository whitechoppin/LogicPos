Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporanlunaspiutang
    Public namaform As String = "laporan-lunas_piutang"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean

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

    Private Sub flaporanpiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksi()

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("terima_piutang").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "terima_piutang", "{0:n0}")
        End With

        Select Case kodeakses
            Case 1
                printstatus = True
                exportstatus = False
            Case 3
                printstatus = False
                exportstatus = True
            Case 4
                printstatus = True
                exportstatus = True
        End Select

        Call historysave("Membuka Laporan Pelunasan Piutang", "N/A", namaform)
    End Sub
    Sub grid()
        GridColumn1.Caption = "id lunas piutang"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "No Penjualan"
        GridColumn2.FieldName = "penjualan_id"

        GridColumn3.Caption = "Tanggal Penjualan"
        GridColumn3.FieldName = "tanggal_penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

        GridColumn4.Caption = "Tanggal Jatuh Tempo"
        GridColumn4.FieldName = "tanggal_jatuhtempo"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

        GridColumn5.Caption = "Pelanggan"
        GridColumn5.FieldName = "nama_pelanggan"

        GridColumn6.Caption = "Total Penjualan"
        GridColumn6.FieldName = "total_penjualan"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Pelunasan"
        GridColumn7.FieldName = "terima_piutang"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridColumn8.Caption = "User"
        GridColumn8.FieldName = "nama_user"

        GridColumn9.Caption = "Kas"
        GridColumn9.FieldName = "nama_kas"

        GridColumn10.Caption = "Tanggal Transaksi"
        GridColumn10.FieldName = "tanggal_transaksi"
        GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn10.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

        GridColumn11.Caption = "No bukti"
        GridColumn11.FieldName = "no_bukti"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()
        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            sql = "SELECT tb_pelunasan_piutang.id, penjualan_id, tb_pelanggan.nama_pelanggan, tanggal_penjualan, tanggal_jatuhtempo, total_penjualan, terima_piutang, nama_user, nama_kas, tanggal_transaksi, no_bukti FROM tb_pelunasan_piutang_detail JOIN tb_pelunasan_piutang ON tb_pelunasan_piutang.id = tb_pelunasan_piutang_detail.pelunasan_piutang_id JOIN tb_pelanggan ON tb_pelanggan.id = tb_pelunasan_piutang.pelanggan_id JOIN tb_user ON tb_user.id = tb_pelunasan_piutang.user_id JOIN tb_kas ON tb_kas.id = tb_pelunasan_piutang.kas_id WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' ORDER BY tb_pelunasan_piutang.id ASC"
        Else
            sql = "SELECT tb_pelunasan_piutang.id, penjualan_id, tb_pelanggan.nama_pelanggan, tanggal_penjualan, tanggal_jatuhtempo, total_penjualan, terima_piutang, nama_user, nama_kas, tanggal_transaksi, no_bukti FROM tb_pelunasan_piutang_detail JOIN tb_pelunasan_piutang ON tb_pelunasan_piutang.id = tb_pelunasan_piutang_detail.pelunasan_piutang_id JOIN tb_pelanggan ON tb_pelanggan.id = tb_pelunasan_piutang.pelanggan_id JOIN tb_user ON tb_user.id = tb_pelunasan_piutang.user_id JOIN tb_kas ON tb_kas.id = tb_pelunasan_piutang.kas_id WHERE DATE(tanggal_transaksi) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' ORDER BY tb_pelunasan_piutang.id ASC"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabel()
        Call historysave("Merefresh Laporan Pelunasan Piutang", "N/A", namaform)
    End Sub
    Sub ExportToExcel()
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
    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        If exportstatus.Equals(True) Then
            If GridView1.DataRowCount > 0 Then
                ExportToExcel()

                Call historysave("Mengexport Laporan Pelunasan Piutang", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnrekap_Click_1(sender As Object, e As EventArgs) Handles btnrekap.Click
        If printstatus.Equals(True) Then
            Dim rptrekap As ReportDocument
            Dim awalPFDs As ParameterFieldDefinitions
            Dim awalPFD As ParameterFieldDefinition
            Dim awalPVs As New ParameterValues
            Dim awalPDV As New ParameterDiscreteValue

            Dim akhirPFDs As ParameterFieldDefinitions
            Dim akhirPFD As ParameterFieldDefinition
            Dim akhirPVs As New ParameterValues
            Dim akhirPDV As New ParameterDiscreteValue

            If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_pelunasan_piutang WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "Select * FROM tb_pelunasan_piutang WHERE DATE(tanggal_transaksi) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekaplunaspiutang

                awalPDV.Value = Format(DateTimePicker1.Value, "yyyy-MM-dd")
                awalPFDs = rptrekap.DataDefinition.ParameterFields
                awalPFD = awalPFDs.Item("tglawal") 'tanggal merupakan nama parameter
                awalPVs.Clear()
                awalPVs.Add(awalPDV)
                awalPFD.ApplyCurrentValues(awalPVs)

                akhirPDV.Value = Format(DateTimePicker2.Value, "yyyy-MM-dd")
                akhirPFDs = rptrekap.DataDefinition.ParameterFields
                akhirPFD = akhirPFDs.Item("tglakhir") 'tanggal merupakan nama parameter
                akhirPVs.Clear()
                akhirPVs.Add(akhirPDV)
                akhirPFD.ApplyCurrentValues(akhirPVs)

                flaplunaspiutang.CrystalReportViewer.ReportSource = rptrekap
                flaplunaspiutang.ShowDialog()
                flaplunaspiutang.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Pelunasan Piutang", "N/A", namaform)
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btndetailrekap_Click(sender As Object, e As EventArgs) Handles btndetailrekap.Click
        If printstatus.Equals(True) Then
            Dim rptrekap As ReportDocument
            Dim awalPFDs As ParameterFieldDefinitions
            Dim awalPFD As ParameterFieldDefinition
            Dim awalPVs As New ParameterValues
            Dim awalPDV As New ParameterDiscreteValue

            Dim akhirPFDs As ParameterFieldDefinitions
            Dim akhirPFD As ParameterFieldDefinition
            Dim akhirPVs As New ParameterValues
            Dim akhirPDV As New ParameterDiscreteValue

            If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_pelunasan_piutang WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "Select * FROM tb_pelunasan_piutang WHERE tanggal_transaksi BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekapdetaillunaspiutang

                awalPDV.Value = Format(DateTimePicker1.Value, "yyyy-MM-dd")
                awalPFDs = rptrekap.DataDefinition.ParameterFields
                awalPFD = awalPFDs.Item("tglawal") 'tanggal merupakan nama parameter
                awalPVs.Clear()
                awalPVs.Add(awalPDV)
                awalPFD.ApplyCurrentValues(awalPVs)

                akhirPDV.Value = Format(DateTimePicker2.Value, "yyyy-MM-dd")
                akhirPFDs = rptrekap.DataDefinition.ParameterFields
                akhirPFD = akhirPFDs.Item("tglakhir") 'tanggal merupakan nama parameter
                akhirPVs.Clear()
                akhirPVs.Add(akhirPDV)
                akhirPFD.ApplyCurrentValues(akhirPVs)

                flaplunaspiutang.CrystalReportViewer.ReportSource = rptrekap
                flaplunaspiutang.ShowDialog()
                flaplunaspiutang.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Detail Pelunasan Piutang", "N/A", namaform)
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporanpiutang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class