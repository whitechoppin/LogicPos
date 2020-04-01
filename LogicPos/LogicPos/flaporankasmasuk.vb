Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporankasmasuk
    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Public isi As String
    Public isi2 As String
    Private Sub flaporankasmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("saldo_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "saldo_kas", "{0:n0}")
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

        Call historysave("Membuka Laporan Kas Masuk", "N/A")
    End Sub
    Sub grid()
        GridColumn1.Caption = "No.Kas Masuk"
        GridColumn1.FieldName = "kode_kas_masuk"

        GridColumn2.Caption = "Kode Kas"
        GridColumn2.FieldName = "kode_kas"

        GridColumn3.Caption = "User"
        GridColumn3.FieldName = "kode_user"

        GridColumn4.Caption = "Jenis Kas"
        GridColumn4.FieldName = "jenis_kas"

        GridColumn5.Caption = "Tanggal Transaksi"
        GridColumn5.FieldName = "tanggal_transaksi"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn6.Caption = "Saldo Kas"
        GridColumn6.FieldName = "saldo_kas"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Keterangan"
        GridColumn7.FieldName = "keterangan_kas"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
            sql = "SELECT * FROM tb_kas_masuk WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_kas_masuk WHERE tanggal_transaksi BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
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
        Call historysave("Merefresh Laporan Kas Masuk", "N/A")
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
                Call historysave("Mengexport Laporan Kas Masuk", "N/A")
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

            Call koneksii()

            If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
                sql = "SELECT * FROM tb_kas_masuk WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_kas_masuk WHERE tanggal_transaksi BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekapkasmasuk

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

                flapkasmasuk.CrystalReportViewer1.ReportSource = rptrekap
                flapkasmasuk.ShowDialog()
                flapkasmasuk.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Kas Masuk", "N/A")
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporankasmasuk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class