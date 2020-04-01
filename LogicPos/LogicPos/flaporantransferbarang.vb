Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporantransferbarang
    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim tabel1 As DataTable
    Public isi As String
    Public isi2 As String
    Private Sub flaporankaskeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
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

        Call historysave("Membuka Laporan Transfer Barang", "N/A")
    End Sub
    Sub new_tabel()
        tabel1 = New DataTable
        With tabel1
            .Columns.Add("kode_transfer_barang")
            .Columns.Add("kode_dari_gudang")
            .Columns.Add("kode_ke_gudang")
            .Columns.Add("tanggal_transfer_barang")
            .Columns.Add("keterangan_transfer_barang")

        End With
        GridControl1.DataSource = tabel1
        'readonly
        For i As Integer = 0 To GridView1.Columns.Count - 1
            GridView1.Columns(i).OptionsColumn.AllowEdit = False
        Next
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode Transfer Barang"
        GridColumn1.FieldName = "kode_transfer_barang"


        GridColumn2.Caption = "Gudang Asal"
        GridColumn2.FieldName = "kode_dari_gudang"

        GridColumn3.Caption = "Gudang Tujuan"
        GridColumn3.FieldName = "kode_ke_gudang"

        GridColumn4.Caption = "Tanggal Transfer"
        GridColumn4.FieldName = "tanggal_transfer_barang"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.FieldName = "keterangan_transfer_barang"

        GridColumn6.Caption = "Debet"
        GridColumn6.FieldName = "debet_kas"
        GridColumn6.Visible = False
        GridColumn7.Caption = "Kredit"
        GridColumn7.FieldName = "kredit_kas"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"
        GridColumn6.Visible = False
        GridColumn7.Visible = False

        GridColumn8.Caption = "Keterangan"
        GridColumn8.FieldName = "keterangan_transfer_kas"

        GridColumn9.Caption = "Laba"
        GridColumn9.FieldName = "keuntungan"
        GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn9.DisplayFormat.FormatString = "##,##0"
        GridColumn9.Visible = False

        'GridColumn10.Caption = "Idbrg"
        'GridColumn10.FieldName = "idbarang"
        GridColumn10.Visible = False
        GridColumn11.Caption = "Kasir"
        GridColumn11.FieldName = "kode_user"
        GridColumn11.Visible = False

        GridColumn12.Caption = "Metode Bayar"
        GridColumn12.FieldName = "metode_pembayaran"
        GridColumn12.Visible = False

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
            sql = "SELECT * FROM tb_transfer_barang WHERE DATE(tanggal_transfer_barang) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_transfer_barang WHERE tanggal_transfer_barang BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
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
        Call historysave("Merefresh Laporan Transfer Barang", "N/A")
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
                Call historysave("Mengexport Laporan Transfer Barang", "N/A")
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

            If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
                sql = "SELECT * FROM tb_transfer_barang WHERE DATE(tanggal_transfer_barang) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_transfer_barang WHERE tanggal_transfer_barang BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If

            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            If dr.HasRows Then
                rptrekap = New rptrekaptransferbarang

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

                flaptransferbarang.CrystalReportViewer1.ReportSource = rptrekap
                flaptransferbarang.ShowDialog()
                flaptransferbarang.WindowState = FormWindowState.Maximized

                Call historysave("Merekap Laporan Transfer Barang", "N/A")
            Else
                MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporantransferbarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class