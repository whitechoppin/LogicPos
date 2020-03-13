Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns
Public Class flaporankasmasuk
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
    End Sub
    Sub grid()
        GridColumn1.Caption = "kode Kas Masuk"
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
            sql = "SELECT * FROM tb_kas_masuk WHERE  tb_kas_masuk.tanggal_transaksi between '" & DateTimePicker1.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "' and '" & DateTimePicker2.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) & "'"
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

            flappembelian.CrystalReportViewer1.ReportSource = rptrekap
            flappembelian.ShowDialog()
            flappembelian.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Data pada tanggal tersebut tidak tersedia", MsgBoxStyle.Information, "Pemberitahuan")
        End If
    End Sub

    Private Sub flaporankasmasuk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class