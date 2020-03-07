Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporankaskeluar
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
    Sub grid()
        GridColumn1.Caption = "kode Kas Masuk"
        GridColumn1.FieldName = "kode_kas_keluar"

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

        GridColumn8.Caption = "Subtotal"
        GridColumn8.FieldName = "subtotal"
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "##,##0"
        GridColumn8.Visible = False

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
    Sub tabel()
        'For x As Integer = 0 To listkasir.Items.Count - 1
        '    If x = 0 Then
        '        user = def + "'" + listkasir.Items(x).ToString + "'"
        '    Else
        '        user = user + "or" + def + "'" + listkasir.Items(x).ToString + "'"

        '    End If
        '    'MsgBox(listkasir.Items(x).ToString)
        'Next
        'MsgBox(user)
        Using cnn As New OdbcConnection(strConn)
            'sql = "SELECT tb_barang.kode, tb_barang.nama, tb_barang.satuan, tb_barang.kategori, tb_barang.stok, tb_price.harga as harga from tb_barang join tb_price on tb_barang.kode = tb_price.idbrg where tb_price.idcustomer='" & Strings.Right(fpenjualan.cmbcustomer.Text, 8) & "'"
            'If listkasir.Items.Count = 0 Then
            '    sql = "select * from tb_penjualan_detail join tb_penjualan on tb_penjualan.idjual=tb_penjualan_detail.idjual join tb_barang on tb_barang.kode=tb_penjualan_detail.idbarang where  tgljual between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' "
            'Else
            'sql = "select * from tb_penjualan_detail join tb_penjualan on tb_penjualan.kode_penjualan=tb_penjualan_detail.kode_penjualan JOIN tb_pelanggan ON tb_pelanggan.kode_pelanggan=tb_penjualan.kode_pelanggan where  tgl_penjualan between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
            '            End If
            'MsgBox(sql)
            sql = "SELECT * FROM tb_kas_keluar WHERE  tb_kas_keluar.tanggal_transaksi between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
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
    Sub cari()
        'Call koneksii()
        'sql = "SELECT * FROM tb_barang WHERE kode='" & txtkode.Text & "'"
        'cmmd = New OdbcCommand(sql, cnn)
        'dr = cmmd.ExecuteReader
        'If dr.HasRows Then
        '    txtnama.Text = dr("nama")

        'Else
        '    txtnama.Text = ""

        'End If
    End Sub
    Sub carikasir()
        'Call koneksii()
        'sql = "SELECT * FROM tb_user WHERE username='" & txtkodekasir.Text & "'"
        'cmmd = New OdbcCommand(sql, cnn)
        'dr = cmmd.ExecuteReader
        'If dr.HasRows Then
        '    txtnamakasir.Text = dr("username")

        'Else
        '    txtnama.Text = ""

        'End If
    End Sub
    Sub search()
        'tutup = 4
        'Dim panjang As Integer = txtkode.Text.Length
        'fcaribarang.Show()
        'fcaribarang.txtcari.Focus()
        'fcaribarang.txtcari.DeselectAll()
        'fcaribarang.txtcari.SelectionStart = panjang
        'Me.txtkode.Clear()
    End Sub
    Sub searchcust()
        'Dim panjang As Integer = txtkode.Text.Length
        'fcaricust.Show()
        'fcaricust.txtcari.Focus()
        'fcaricust.txtcari.DeselectAll()
        'fcaricust.txtcari.SelectionStart = panjang
        'Me.txtkode.Clear()
    End Sub
    Private Sub txtkode_TextChanged(sender As Object, e As EventArgs)
        'isi = txtkode.Text
        'isicari = isi
        'If Strings.Left(txtkode.Text, 1) Like "[A-Z, a-z]" Then

        '    Call search()
        '    'fcaribarang.txtcari.Focus()
        '    'fcaribarang.txtcari.DeselectAll()
        'Else
        '    Call cari()
        'End If
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs)
        'If txtnama.Text.Length = 0 Then
        '    Exit Sub
        'Else
        '    listitem.Items.Add(txtnama.Text)
        '    txtkode.Clear()
        '    txtnama.Clear()
        'End If

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
    Private Sub btnrekap_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btncariitem_Click(sender As Object, e As EventArgs)
        'isi = txtkode.Text
        'Call search()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs)
        'listitem.Items.Clear()
    End Sub
    Private Sub btncaricust_Click(sender As Object, e As EventArgs)
        'isi = txtkode.Text
        'Call searchcust()
    End Sub
    Private Sub btntambahcust_Click(sender As Object, e As EventArgs)
        'If txtnamakasir.Text.Length = 0 Then
        '    Exit Sub
        'Else
        '    listkasir.Items.Add(txtnamakasir.Text)
        '    txtkodekasir.Clear()
        '    txtnamakasir.Clear()
        'End If
    End Sub
    Private Sub btnhapuscust_Click(sender As Object, e As EventArgs)
        'listkasir.Items.Clear()
    End Sub
    Private Sub txtkodecust_TextChanged(sender As Object, e As EventArgs)
        'isi2 = txtkodekasir.Text
        'isicari2 = isi2
        'If Strings.Left(txtkode.Text, 1) Like "[A-Z, a-z]" Then
        '    Call searchcust()
        '    'fcaribarang.txtcari.Focus()
        '    'fcaribarang.txtcari.DeselectAll()
        'Else
        Call carikasir()

        ' End If
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

    Private Sub btnrekap_Click_1(sender As Object, e As EventArgs) Handles btnrekap.Click
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
            sql = "SELECT * FROM tb_kas_keluar WHERE DATE(tanggal_transaksi) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Else
            sql = "SELECT * FROM tb_kas_keluar WHERE tanggal_transaksi BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
        End If

        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        If dr.HasRows Then
            rptrekap = New rptrekapkaskeluar

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

    Private Sub flaporankaskeluar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class