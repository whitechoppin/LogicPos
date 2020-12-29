Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporantransaksikas
    Public namaform As String = "laporan-transaksi_kas"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim tabel1 As DataTable
    Public isi As String
    Public isi2 As String

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
    Private Sub flaporantransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("debet_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "debet_kas", "{0:n0}")
            .Columns("kredit_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "kredit_kas", "{0:n0}")
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

        Call historysave("Membuka Laporan Transaksi Kas", "N/A", namaform)
    End Sub

    Sub grid()
        GridColumn1.Caption = "No.Transaksi"
        GridColumn1.FieldName = "kode_transaksi"

        GridColumn2.Caption = "Kode Kas"
        GridColumn2.FieldName = "kode_kas"

        GridColumn3.Caption = "No.Pembelian"
        GridColumn3.FieldName = "kode_pembelian"

        GridColumn4.Caption = "No.Penjualan"
        GridColumn4.FieldName = "kode_penjualan"

        GridColumn5.Caption = "No.Utang"
        GridColumn5.FieldName = "kode_utang"

        GridColumn6.Caption = "No.Piutang"
        GridColumn6.FieldName = "kode_piutang"

        GridColumn7.Caption = "No.Kas Masuk"
        GridColumn7.FieldName = "kode_kas_masuk"

        GridColumn8.Caption = "No.Kas Keluar"
        GridColumn8.FieldName = "kode_kas_keluar"

        GridColumn9.Caption = "No.Transfer Kas"
        GridColumn9.FieldName = "kode_transfer_kas"

        GridColumn10.Caption = "Jenis Kas"
        GridColumn10.FieldName = "jenis_kas"

        GridColumn11.Caption = "Tanggal"
        GridColumn11.FieldName = "tanggal_transaksi"
        GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn11.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

        GridColumn12.Caption = "Debet"
        GridColumn12.FieldName = "debet_kas"
        GridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn12.DisplayFormat.FormatString = "##,##0"

        GridColumn13.Caption = "Kredit"
        GridColumn13.FieldName = "kredit_kas"
        GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn13.DisplayFormat.FormatString = "##,##0"

        GridControl1.Visible = True
    End Sub

    Sub tabel()
        Call koneksii()

        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            sql = "SELECT * FROM tb_transaksi_kas WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' ORDER BY id ASC"
        Else
            sql = "SELECT * FROM tb_transaksi_kas WHERE tanggal_transaksi BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' ORDER BY id ASC"
        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub
    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabel()
        Call historysave("Merefresh Laporan Transaksi Kas", "N/A", namaform)
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
                Call historysave("Mengexport Laporan Transaksi Kas", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If

    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click
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

            Call koneksii()

            If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_transaksi_kas WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_transaksi_kas WHERE tanggal_transaksi BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekaptransaksikas

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

                flaptransaksikas.CrystalReportViewer1.ReportSource = rptrekap
                flaptransaksikas.ShowDialog()
                flaptransaksikas.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Transaksi Kas", "N/A", namaform)
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporantransaksikas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class