Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporanpenyesuaianstok
    Public namaform As String = "laporan-penyesuaian_stok"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim tabeltransfer As DataTable

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
    Private Sub flaporanpenyesuaianstok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        dtawal.MaxDate = Now
        dtakhir.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            '.Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
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

        Call historysave("Membuka Laporan Penyesuaian Stok", "N/A", namaform)
    End Sub

    Sub grid()
        GridColumn1.Caption = "No Faktur"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Gudang"
        GridColumn2.FieldName = "nama_gudang"

        GridColumn3.Caption = "Tanggal"
        GridColumn3.FieldName = "tanggal_penyesuaian_stok"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Kode Barang"
        GridColumn4.FieldName = "kode_barang"

        GridColumn5.Caption = "Kode Stok"
        GridColumn5.FieldName = "kode_stok"

        GridColumn6.Caption = "Nama Barang"
        GridColumn6.FieldName = "nama_barang"

        GridColumn7.Caption = "Qty"
        GridColumn7.FieldName = "qty"

        GridColumn8.Caption = "Status"
        GridColumn8.FieldName = "status_stok"

        GridColumn9.Caption = "User"
        GridColumn9.FieldName = "kode_user"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
            sql = "SELECT tb_penyesuaian_stok.id, tb_gudang.nama_gudang, tanggal_penyesuaian_stok, kode_barang, kode_stok, nama_barang, qty, status_stok, tb_user.kode_user FROM tb_penyesuaian_stok JOIN tb_penyesuaian_stok_detail ON tb_penyesuaian_stok.id = tb_penyesuaian_stok_detail.penyesuaian_stok_id JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id JOIN tb_user ON tb_user.id = tb_penyesuaian_stok.user_id WHERE DATE(tanggal_penyesuaian_stok) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT tb_penyesuaian_stok.id, tb_gudang.nama_gudang, tanggal_penyesuaian_stok, kode_barang, kode_stok, nama_barang, qty, status_stok, tb_user.kode_user FROM tb_penyesuaian_stok JOIN tb_penyesuaian_stok_detail ON tb_penyesuaian_stok.id = tb_penyesuaian_stok_detail.penyesuaian_stok_id JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id JOIN tb_user ON tb_user.id = tb_penyesuaian_stok.user_id WHERE tanggal_penyesuaian_stok BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
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
        Call historysave("Merefresh Laporan Penyesuaian Stok", "N/A", namaform)
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

            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                sql = "SELECT * FROM tb_penyesuaian_stok WHERE DATE(tanggal_penyesuaian_stok) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_penyesuaian_stok WHERE tanggal_penyesuaian_stok BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekappenyesuaianstok

                awalPDV.Value = Format(dtawal.Value, "yyyy-MM-dd")
                awalPFDs = rptrekap.DataDefinition.ParameterFields
                awalPFD = awalPFDs.Item("tglawal") 'tanggal merupakan nama parameter
                awalPVs.Clear()
                awalPVs.Add(awalPDV)
                awalPFD.ApplyCurrentValues(awalPVs)

                akhirPDV.Value = Format(dtakhir.Value, "yyyy-MM-dd")
                akhirPFDs = rptrekap.DataDefinition.ParameterFields
                akhirPFD = akhirPFDs.Item("tglakhir") 'tanggal merupakan nama parameter
                akhirPVs.Clear()
                akhirPVs.Add(akhirPDV)
                akhirPFD.ApplyCurrentValues(akhirPVs)

                flappenyesuaianstok.CrystalReportViewer1.ReportSource = rptrekap
                flappenyesuaianstok.ShowDialog()
                flappenyesuaianstok.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Penyesuaian Stok", "N/A", namaform)
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
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
                Call historysave("Mengexport Laporan Penyesuaian Stok", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporanpenyesuaianstok_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class