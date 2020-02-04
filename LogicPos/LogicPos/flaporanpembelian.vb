Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporanpembelian
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
            ' .Columns("keuntungan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "keuntungan", "{0:n0}")

        End With
    End Sub
    Sub grid()
        GridColumn1.Caption = "No.Nota"
        GridColumn1.FieldName = "kode_pembelian"

        GridColumn2.Caption = "Supplier"
        GridColumn2.FieldName = "nama_supplier"

        GridColumn3.Caption = "Tanggal Pembelian"
        GridColumn3.FieldName = "tgl_pembelian"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn4.Caption = "Item"
        GridColumn4.FieldName = "nama_barang"

        GridColumn5.Caption = "Banyak"
        GridColumn5.FieldName = "qty"

        GridColumn6.Caption = "Satuan"
        GridColumn6.FieldName = "satuan_barang"

        GridColumn7.Caption = "Harga Beli"
        GridColumn7.FieldName = "harga_beli"
        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridColumn8.Caption = "Subtotal"
        GridColumn8.FieldName = "subtotal"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "##,##0"

        GridColumn9.Caption = "Kasir Penerima"
        GridColumn9.FieldName = "kode_user"

        GridColumn10.Visible = False
        GridColumn11.Visible = False
        GridColumn12.Caption = "Metode Bayar"
        GridColumn12.FieldName = "pembayaran_pembelian"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
                sql = "SELECT * FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.kode_pembelian=tb_pembelian_detail.kode_pembelian JOIN tb_supplier ON tb_supplier.kode_supplier = tb_pembelian.kode_supplier WHERE DATE(tgl_pembelian) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.kode_pembelian=tb_pembelian_detail.kode_pembelian JOIN tb_supplier ON tb_supplier.kode_supplier = tb_pembelian.kode_supplier WHERE tgl_pembelian BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If
            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
        End Using
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

        sql = "SELECT * FROM tb_pembelian WHERE tgl_pembelian BETWEEN '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'  - INTERVAL 1 day AND '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' + INTERVAL 1 day"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptrekappembelian

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

            flappembelian.CrystalReportViewer1.ReportSource = rptrekap
            flappembelian.ShowDialog()
            flappembelian.WindowState = FormWindowState.Maximized
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

        If GridView1.DataRowCount > 0 Then
            ExportToExcel()
        Else
            MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
        End If

    End Sub

    Private Sub btnperfaktur_Click(sender As Object, e As EventArgs) Handles btnperfaktur.Click
        Dim rptrekap As ReportDocument
        Dim awalPFDs As ParameterFieldDefinitions
        Dim awalPFD As ParameterFieldDefinition
        Dim awalPVs As New ParameterValues
        Dim awalPDV As New ParameterDiscreteValue

        Dim akhirPFDs As ParameterFieldDefinitions
        Dim akhirPFD As ParameterFieldDefinition
        Dim akhirPVs As New ParameterValues
        Dim akhirPDV As New ParameterDiscreteValue

        'sql = "SELECT * FROM tb_penjualan WHERE tgl_penjualan BETWEEN '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'  - INTERVAL 1 day AND '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' + INTERVAL 1 day"
        sql = "SELECT * FROM tb_pembelian WHERE tgl_pembelian BETWEEN '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'  - INTERVAL 1 day AND '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' + INTERVAL 1 day"

        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptperfakturbeli

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

            flappembelian.CrystalReportViewer1.ReportSource = rptrekap
            flappembelian.ShowDialog()
            flappembelian.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        End If
    End Sub
End Class