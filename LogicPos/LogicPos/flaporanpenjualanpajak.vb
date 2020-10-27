Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporanpenjualanpajak
    Public kodeakses As Integer
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
    Private Sub flaporanpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
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
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Pelangan"
        GridColumn2.FieldName = "nama_pelanggan"

        GridColumn3.Caption = "Tanggal Penjualan"
        GridColumn3.FieldName = "tgl_penjualan"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy"

        GridColumn4.Caption = "Barang"
        GridColumn4.FieldName = "nama_barang"

        GridColumn5.Caption = "Qty"
        GridColumn5.FieldName = "qty"

        GridColumn6.Caption = "Satuan"
        GridColumn6.FieldName = "satuan_barang"

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
        GridColumn9.Visible = False

        GridColumn10.Caption = "Kasir"
        GridColumn10.FieldName = "kode_user"

        GridColumn11.Caption = "Metode Bayar"
        GridColumn11.FieldName = "metode_pembayaran"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            sql = "SELECT tb_penjualan_detail.kode_penjualan, tb_penjualan.tgl_penjualan,tb_pelanggan.nama_pelanggan, tb_penjualan_detail.nama_barang, tb_penjualan_detail.nama_barang, tb_penjualan_detail.qty, tb_penjualan_detail.satuan_barang, (tb_penjualan_detail.harga_jual - (tb_penjualan_detail.harga_jual * (select tb_pajak.persen FROM tb_pajak)/100)) AS harga_jual, (tb_penjualan_detail.subtotal - (tb_penjualan_detail.subtotal  * (select tb_pajak.persen FROM tb_pajak)/100)) as subtotal, tb_penjualan.metode_pembayaran, tb_penjualan.kode_user FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.kode_penjualan=tb_penjualan_detail.kode_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan=tb_penjualan.kode_pelanggan WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT tb_penjualan_detail.kode_penjualan, tb_penjualan.tgl_penjualan,tb_pelanggan.nama_pelanggan, tb_penjualan_detail.nama_barang, tb_penjualan_detail.nama_barang, tb_penjualan_detail.qty, tb_penjualan_detail.satuan_barang, (tb_penjualan_detail.harga_jual - (tb_penjualan_detail.harga_jual * (select tb_pajak.persen FROM tb_pajak)/100)) AS harga_jual, (tb_penjualan_detail.subtotal - (tb_penjualan_detail.subtotal  * (select tb_pajak.persen FROM tb_pajak)/100)) as subtotal, tb_penjualan.metode_pembayaran, tb_penjualan.kode_user FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.kode_penjualan=tb_penjualan_detail.kode_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan=tb_penjualan.kode_pelanggan WHERE tgl_penjualan BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
        End If

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
        'End Using
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

        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            sql = "SELECT * FROM tb_penjualan WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' ORDER BY metode_pembayaran ASC"
        Else
            sql = "SELECT * FROM tb_penjualan WHERE tgl_penjualan BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' ORDER BY metode_pembayaran ASC"
        End If

        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptrekappenjualanpajak

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

            flapreturpenjualan.CrystalReportViewer1.ReportSource = rptrekap
            flapreturpenjualan.ShowDialog()
            flapreturpenjualan.WindowState = FormWindowState.Maximized
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

        If Format(DateTimePicker1.Value, "yyyy-MM-dd").Equals(Format(DateTimePicker2.Value, "yyyy-MM-dd")) Then
            sql = "SELECT * FROM tb_penjualan WHERE DATE(tgl_penjualan) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_penjualan WHERE tgl_penjualan BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
        End If

        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptperfakturjualpajak

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

            flappenjualan.CrystalReportViewer1.ReportSource = rptrekap
            flappenjualan.ShowDialog()
            flappenjualan.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        End If
    End Sub

    Private Sub flaporanpenjualanpajak_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class