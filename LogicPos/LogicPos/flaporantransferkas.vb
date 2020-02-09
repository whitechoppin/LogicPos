Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporantransferkas
    Dim tabel1 As DataTable
    Public isi As String
    Public isi2 As String
    Private Sub flaporankaskeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
        Me.WindowState = WindowState.Maximized

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("saldo_kas").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "saldo_kas", "{0:n0}")

        End With
    End Sub
    Sub new_tabel()
        tabel1 = New DataTable
        With tabel1
            .Columns.Add("kode_kas")
            .Columns.Add("jenis_kas")
            .Columns.Add("tanggal_transaksi")
            .Columns.Add("kas_masuk", GetType(Double))
            .Columns.Add("kas_keluar", GetType(Double))

        End With
        GridControl1.DataSource = tabel1
        'readonly
        For i As Integer = 0 To GridView1.Columns.Count - 1
            GridView1.Columns(i).OptionsColumn.AllowEdit = False
        Next
    End Sub
    Sub grid()
        GridColumn1.Caption = "Tanggal Transfer Kas"
        GridColumn1.FieldName = "tanggal_transfer_kas"
        GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn1.DisplayFormat.FormatString = "dd/MM/yyy"

        GridColumn2.Caption = "Kode Kas Dari"
        GridColumn2.FieldName = "kode_dari_kas"

        GridColumn3.Caption = "Kode Kas Ke-"
        GridColumn3.FieldName = "kode_ke_kas"


        GridColumn4.Caption = "Jenis Kas"
        GridColumn4.FieldName = "jenis_kas"

        GridColumn5.Caption = "Saldo Kas"
        GridColumn5.FieldName = "saldo_kas"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"


        GridColumn6.Caption = "Debet"
        GridColumn6.FieldName = "debet_kas"

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

        '        GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        '       GridColumn7.DisplayFormat.FormatString = "##,##0"

        GridControl1.Visible = True
    End Sub
    Sub tabel_bantu()
        Call new_tabel()
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = tabel1

        Call koneksii()
        'sql = "select tb_item.nama_brg, tb_histori.orderid, tb_histori.tanggal,tb_histori.waktu, tb_histori.qtymasuk, tb_histori.qtykeluar,tb_histori.qty, tb_histori.hrgmasuk,tb_histori.hrgkeluar from tb_histori join tb_item on tb_histori.idbrg = tb_item.id_barang where tb_histori.idbrg= '" & txtid.Text & "'  and tb_histori.tanggal < '" & tgl1 & "' order by tb_histori.tanggal desc, tb_histori.waktu desc limit 1"
        'sql = "SELECT  tb_kas_masuk.kode_kas, tb_kas_masuk.jenis_kas, tb_kas_masuk.tanggal_transaksi AS tanggal, tb_kas_masuk.saldo_kas AS debet FROM tb_kas_masuk WHERE  tb_kas_masuk.tanggal_transaksi between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
        sql = "SELECT  tb_kas_masuk.kode_kas, tb_kas_masuk.jenis_kas, tb_kas_masuk.tanggal_transaksi AS tanggal, tb_kas_masuk.saldo_kas AS debet FROM tb_kas_masuk WHERE  (tb_kas_masuk.tanggal_transaksi >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "') AND (tb_kas_masuk.tanggal_transaksi <='" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "') "

        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        While dr.Read
            tabel1.Rows.Add(dr("kode_kas"), dr("jenis_kas"), Format(dr("tanggal"), "dd/MM/yyyy"), dr("debet"), 0)
        End While
        'tabel1.Rows.Add("*INITIAL*", Format(dr("tanggal"), "dd/MM/yyyy"), dr("waktu"), "0", dr("qty"), " ", dr("qty"), " ", " ")
        ' End If


        sql = "SELECT  tb_kas_keluar.kode_kas, tb_kas_keluar.jenis_kas, tb_kas_keluar.tanggal_transaksi AS tanggal, tb_kas_keluar.saldo_kas AS kredit FROM tb_kas_keluar WHERE  (tb_kas_keluar.tanggal_transaksi >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "') AND (tb_kas_keluar.tanggal_transaksi <='" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "'+ INTERVAL 1 day) "
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        While dr.Read
            'tabel1.Rows.Add("*INITIAL*", Format(dr("tanggal"), "dd/MM/yyyy"), dr("waktu"), "0", dr("qty"), " ", dr("qty"), " ", " ")
            tabel1.Rows.Add(dr("kode_kas"), dr("jenis_kas"), Format(dr("tanggal"), "dd/MM/yyyy"), 0, dr("kredit"))

        End While
        GridControl1.RefreshDataSource()
        GridColumn5.SortOrder = DevExpress.Data.ColumnSortOrder.Descending

    End Sub
    Sub tabel()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_transfer_kas where tanggal_transfer_kas between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using
    End Sub
    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabel()
    End Sub
    Sub ExportToExcel()
        'https://www.devexpress.com/Support/Center/Question/Details/Q430217
        'https://documentation.devexpress.com/#WindowsForms/CustomDocument1874
        'storing current layout
        'GridView1.SaveLayoutToXml("C:\data\tempLayout.xml")
        'For Each column As GridColumn In GridView1.Columns
        'make all export columns visible
        'column.Visible = True
        'Next
        Dim a As String = InputBox("Nama File", "Input Nama file ")
        Dim b As String = "C:\data\" + a + ".xls"
        GridView1.ExportToXls(b)
        MsgBox("Data tersimpan di " + b, MsgBoxStyle.Information, "Success")
        'restoring the layout, the layout file needs to be deleted manually
        'GridView1.RestoreLayoutFromXml("C:\data\tempLayout.xml")
    End Sub
    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click

        ExportToExcel()
        MsgBox("Export successfull!")

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

        If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
            sql = "SELECT * FROM tb_transfer_kas WHERE DATE(tanggal_transfer_kas) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_transfer_kas WHERE tanggal_transfer_kas BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
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

            flappembelian.CrystalReportViewer1.ReportSource = rptrekap
            flappembelian.ShowDialog()
            flappembelian.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        End If
    End Sub
End Class