Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporantransferkas
    Public namaform As String = "laporan-transfer_kas"

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

    Private Sub flaporankaskeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("saldo_transfer_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "saldo_transfer_kas", "{0:n0}")
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

        Call historysave("Membuka Laporan Transfer Kas", "N/A", namaform)
    End Sub

    Sub grid()
        GridColumn1.Caption = "No Faktur"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Dari Kas"
        GridColumn2.FieldName = "dari_kas"

        GridColumn3.Caption = "Ke Kas"
        GridColumn3.FieldName = "ke_kas"

        GridColumn4.Caption = "User"
        GridColumn4.FieldName = "nama_user"

        GridColumn5.Caption = "Jenis Transfer"
        GridColumn5.FieldName = "jenis_transfer_kas"

        GridColumn6.Caption = "Tanggal Transfer"
        GridColumn6.FieldName = "tanggal"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn7.Caption = "Saldo Transfer"
        GridColumn7.FieldName = "saldo_transfer_kas"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridColumn8.Caption = "Keterangan"
        GridColumn8.FieldName = "keterangan_transfer_kas"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            sql = "SELECT tk.id, dari.nama_kas AS dari_kas, ke.nama_kas AS ke_kas, usr.nama_user, tk.jenis_transfer_kas, tk.tanggal, tk.saldo_transfer_kas, tk.keterangan_transfer_kas FROM tb_transfer_kas AS tk JOIN tb_kas AS dari ON dari.id = tk.dari_kas_id JOIN tb_kas AS ke ON ke.id = tk.ke_kas_id JOIN tb_user AS usr ON usr.id = tk.user_id WHERE DATE(tanggal) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT tk.id, dari.nama_kas AS dari_kas, ke.nama_kas AS ke_kas, usr.nama_user, tk.jenis_transfer_kas, tk.tanggal, tk.saldo_transfer_kas, tk.keterangan_transfer_kas FROM tb_transfer_kas AS tk JOIN tb_kas AS dari ON dari.id = tk.dari_kas_id JOIN tb_kas AS ke ON ke.id = tk.ke_kas_id JOIN tb_user AS usr ON usr.id = tk.user_id WHERE tanggal BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
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
        Call historysave("Merefresh Laporan Transfer Kas", "N/A", namaform)
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
                Call historysave("Mengexport Laporan Transfer Kas", "N/A", namaform)
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
                sql = "SELECT * FROM tb_transfer_kas WHERE DATE(tanggal) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_transfer_kas WHERE tanggal BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekaptransferkas

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

                flaptransferkas.CrystalReportViewer1.ReportSource = rptrekap
                flaptransferkas.ShowDialog()
                flaptransferkas.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Transfer Kas", "N/A", namaform)
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporantransferkas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class