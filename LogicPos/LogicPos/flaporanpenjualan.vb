Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporanpenjualan
    Public isi As String
    Public isi2 As String
    Private Sub flaporanpembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("subtotal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "subtotal", "{0:n0}")
            .Columns("keuntungan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "keuntungan", "{0:n0}")

        End With
    End Sub
    Sub grid()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_penjualan"

        GridColumn2.Caption = "Pelangan"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "Tanggal Penjualan"
        GridColumn3.FieldName = "tgl_penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Item"
        GridColumn4.FieldName = "nama_barang"

        GridColumn5.Caption = "Banyak"
        GridColumn5.FieldName = "qty"

        GridColumn6.Caption = "Satuan"
        GridColumn6.FieldName = "satuan_barang"

        'GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        'GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Harga Jual"
        GridColumn7.FieldName = "harga_jual"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridColumn8.Caption = "Subtotal"
        GridColumn8.FieldName = "subtotal"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "##,##0"

        GridColumn9.Caption = "Laba"
        GridColumn9.FieldName = "keuntungan"
        GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn9.DisplayFormat.FormatString = "##,##0"

        'GridColumn10.Caption = "Idbrg"
        'GridColumn10.FieldName = "idbarang"
        GridColumn10.Visible = False
        GridColumn11.Caption = "Kasir"
        GridColumn11.FieldName = "kode_user"
        GridColumn12.Caption = "Metode Bayar"
        GridColumn12.FieldName = "metode_pembayaran"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.kode_penjualan=tb_penjualan_detail.kode_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan=tb_penjualan.kode_pelanggan WHERE tgl_penjualan BETWEEN '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' AND '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
        End Using
    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs)
        'Dim rpt As ReportDocument
        'Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        'Dim crParameterFieldDefinition As ParameterFieldDefinition
        'Dim crParameterValues As ParameterValues
        'Dim crParameterDiscreteValue As ParameterDiscreteValue

        'Dim kcrParameterFieldDefinitions As ParameterFieldDefinitions
        'Dim kcrParameterFieldDefinition As ParameterFieldDefinition
        'Dim kcrParameterValues As ParameterValues
        'Dim kcrParameterDiscreteValue As ParameterDiscreteValue
        ''sql = "select * from tb_penjualan where tgljual>= '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and tgljual < '" & DateAdd(DateInterval.Day, 1, DateTimePicker2.Value.Date) & "' "
        'sql = "select * from tb_penjualan where tgljual between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
        'cmmd = New OdbcCommand(sql, cnn)
        'dr = cmmd.ExecuteReader
        'If dr.HasRows Then
        '    rpt = New rptlapjual
        '    crParameterFieldDefinitions = rpt.DataDefinition.ParameterFields

        '    'Access the specified parameter from the collection
        '    crParameterFieldDefinition = crParameterFieldDefinitions("item")
        '    crParameterValues = crParameterFieldDefinition.CurrentValues
        '    'isi parameter
        '    If listitem.Items.Count = 0 Then
        '        crParameterDiscreteValue = New ParameterDiscreteValue()
        '        crParameterDiscreteValue.Value = "All" '1st current value
        '        crParameterValues.Add(crParameterDiscreteValue)
        '    Else
        '        For a As Integer = 0 To listitem.Items.Count - 1
        '            crParameterDiscreteValue = New ParameterDiscreteValue()
        '            crParameterDiscreteValue.Value = listitem.Items(a).ToString '1st current value
        '            crParameterValues.Add(crParameterDiscreteValue)
        '        Next
        '    End If
        '    'All current parameter values must be applied for the parameter field.
        '    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)


        '    kcrParameterFieldDefinitions = rpt.DataDefinition.ParameterFields
        '    'Access the specified parameter from the collection
        '    kcrParameterFieldDefinition = kcrParameterFieldDefinitions("nama")
        '    kcrParameterValues = kcrParameterFieldDefinition.CurrentValues
        '    If listkasir.Items.Count = 0 Then
        '        kcrParameterDiscreteValue = New ParameterDiscreteValue()
        '        kcrParameterDiscreteValue.Value = "All" '1st current value
        '        kcrParameterValues.Add(kcrParameterDiscreteValue)
        '    Else
        '        For k As Integer = 0 To listkasir.Items.Count - 1
        '            kcrParameterDiscreteValue = New ParameterDiscreteValue()
        '            kcrParameterDiscreteValue.Value = listkasir.Items(k).ToString '1st current value
        '            kcrParameterValues.Add(kcrParameterDiscreteValue)
        '        Next
        '    End If
        '    kcrParameterFieldDefinition.ApplyCurrentValues(kcrParameterValues)

        '    Dim crPFDs As ParameterFieldDefinitions
        '    Dim crPFD As ParameterFieldDefinition
        '    Dim crPVs As New ParameterValues
        '    Dim crPDV As New ParameterDiscreteValue

        '    crPDV.Value = DateTimePicker1.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo)
        '    crPFDs = rpt.DataDefinition.ParameterFields
        '    crPFD = crPFDs.Item("tglawal") 'tanggal merupakan nama parameter
        '    crPVs.Clear()
        '    crPVs.Add(crPDV)
        '    crPFD.ApplyCurrentValues(crPVs)
        '    'Set the viewer to the report object to be previewed.
        '    'flappenjualan.CrystalReportViewer1.ReportSource = rpt
        '    'flappenjualan.ShowDialog()

        '    Dim bcrPFDs As ParameterFieldDefinitions
        '    Dim bcrPFD As ParameterFieldDefinition
        '    Dim bcrPVs As New ParameterValues
        '    Dim bcrPDV As New ParameterDiscreteValue

        '    bcrPDV.Value = DateTimePicker2.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo)
        '    bcrPFDs = rpt.DataDefinition.ParameterFields
        '    bcrPFD = bcrPFDs.Item("tglakhir") 'tanggal merupakan nama parameter
        '    bcrPVs.Clear()
        '    bcrPVs.Add(bcrPDV)
        '    bcrPFD.ApplyCurrentValues(bcrPVs)
        '    'Set the viewer to the report object to be previewed.
        '    flappenjualan.CrystalReportViewer1.ReportSource = rpt
        '    flappenjualan.ShowDialog()
        '    flappenjualan.WindowState = FormWindowState.Maximized
        'Else
        '    MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        'End If
    End Sub
    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click
        Dim rptrekap As ReportDocument
        Dim awalPFDs As ParameterFieldDefinitions
        Dim awalPFD As ParameterFieldDefinition
        Dim awalPVs As New ParameterValues
        Dim awalPDV As New ParameterDiscreteValue

        Dim akhirPFDs As ParameterFieldDefinitions
        Dim akhirPFD As ParameterFieldDefinition
        Dim akhirPVs As New ParameterValues
        Dim akhirPDV As New ParameterDiscreteValue

        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition As ParameterFieldDefinition
        Dim crParameterValues As ParameterValues
        Dim crParameterDiscreteValue As ParameterDiscreteValue

        sql = "SELECT * FROM tb_penjualan WHERE tgl_penjualan BETWEEN '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'  - INTERVAL 1 day AND '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' + INTERVAL 1 day"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptrekappenjualan

            awalPDV.Value = DateTimePicker1.Value.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
            awalPFDs = rptrekap.DataDefinition.ParameterFields
            awalPFD = awalPFDs.Item("tglawal") 'tanggal merupakan nama parameter
            awalPVs.Clear()
            awalPVs.Add(awalPDV)
            awalPFD.ApplyCurrentValues(awalPVs)

            akhirPDV.Value = DateTimePicker2.Value.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
            akhirPFDs = rptrekap.DataDefinition.ParameterFields
            akhirPFD = akhirPFDs.Item("tglakhir") 'tanggal merupakan nama parameter
            akhirPVs.Clear()
            akhirPVs.Add(akhirPDV)
            akhirPFD.ApplyCurrentValues(akhirPVs)

            'crParameterFieldDefinitions = rptrekap.DataDefinition.ParameterFields
            'crParameterFieldDefinition = crParameterFieldDefinitions("nama")
            'crParameterValues = crParameterFieldDefinition.CurrentValues
            'isi parameter
            'If listkasir.Items.Count = 0 Then
            '    crParameterDiscreteValue = New ParameterDiscreteValue()
            '    crParameterDiscreteValue.Value = "All" '1st current value
            '    crParameterValues.Add(crParameterDiscreteValue)
            'Else
            '    For a As Integer = 0 To listkasir.Items.Count - 1
            '        crParameterDiscreteValue = New ParameterDiscreteValue()
            '        crParameterDiscreteValue.Value = listkasir.Items(a).ToString '1st current value
            '        crParameterValues.Add(crParameterDiscreteValue)
            '    Next
            'End If
            'crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            'statPFDs = rptrekap.DataDefinition.ParameterFields
            'statPFD = akhirPFDs.Item("status") 'tanggal merupakan nama parameter
            'statPVs = statPFD.CurrentValues
            'If ckcash.Checked = True Then
            '    statPDV = New ParameterDiscreteValue()
            '    statPDV.Value = "CASH"
            '    statPVs.Add(statPDV)
            'End If
            'If ckcredit.Checked = True Then
            '    statPDV = New ParameterDiscreteValue()
            '    statPDV.Value = "CREDIT"
            '    statPVs.Add(statPDV)
            'End If
            'If ckcredit.Checked = False And ckcash.Checked = False Then
            '    statPDV = New ParameterDiscreteValue()
            '    statPDV.Value = "ALL"
            '    statPVs.Add(statPDV)
            'End If
            'statPFD.ApplyCurrentValues(statPVs)

            flappenjualan.CrystalReportViewer1.ReportSource = rptrekap
            flappenjualan.ShowDialog()
            flappenjualan.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        End If
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabel()
    End Sub
    Sub ExportToExcel()

        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If (Not System.IO.Directory.Exists(pathdata)) Then
            System.IO.Directory.CreateDirectory(pathdata)
        End If

        GridView1.ExportToXls(yourpath)
        MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
    End Sub
    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click

        ExportToExcel()
        MsgBox("Export successfull !")

    End Sub

    Private Sub btnfaktur_Click(sender As Object, e As EventArgs) Handles btnfaktur.Click
        Dim rptrekap As ReportDocument
        Dim awalPFDs As ParameterFieldDefinitions
        Dim awalPFD As ParameterFieldDefinition
        Dim awalPVs As New ParameterValues
        Dim awalPDV As New ParameterDiscreteValue

        Dim akhirPFDs As ParameterFieldDefinitions
        Dim akhirPFD As ParameterFieldDefinition
        Dim akhirPVs As New ParameterValues
        Dim akhirPDV As New ParameterDiscreteValue

        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition As ParameterFieldDefinition
        Dim crParameterValues As ParameterValues
        Dim crParameterDiscreteValue As ParameterDiscreteValue

        sql = "SELECT * FROM tb_penjualan WHERE tgl_penjualan BETWEEN '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'  - INTERVAL 1 day AND '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' + INTERVAL 1 day"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptperfakturjual

            awalPDV.Value = DateTimePicker1.Value.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
            awalPFDs = rptrekap.DataDefinition.ParameterFields
            awalPFD = awalPFDs.Item("tglawal") 'tanggal merupakan nama parameter
            awalPVs.Clear()
            awalPVs.Add(awalPDV)
            awalPFD.ApplyCurrentValues(awalPVs)

            akhirPDV.Value = DateTimePicker2.Value.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
            akhirPFDs = rptrekap.DataDefinition.ParameterFields
            akhirPFD = akhirPFDs.Item("tglakhir") 'tanggal merupakan nama parameter
            akhirPVs.Clear()
            akhirPVs.Add(akhirPDV)
            akhirPFD.ApplyCurrentValues(akhirPVs)

            flappenjualan.CrystalReportViewer1.ReportSource = rptrekap
            flappenjualan.ShowDialog()
            flappenjualan.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        End If
    End Sub
End Class