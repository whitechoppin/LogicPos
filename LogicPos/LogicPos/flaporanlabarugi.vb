Imports System.Data.Odbc
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Columns

Public Class flaporanlabarugi
    Public namaform As String = "laporan-laba_rugi"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
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

    Private Sub flaporanlabarugi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        cmbmonth.DataSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList()
        cmbyear.DataSource = Enumerable.Range(2019, DateTime.Now.Year - 2000 + 1).ToList()

        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("saldo").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "saldo", "{0:n0}")
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

        Call historysave("Membuka Laporan Laba Rugi", "N/A", namaform)
    End Sub
    Sub grid()
        GridColumn1.Caption = "Tipe"
        GridColumn1.FieldName = "tipe"

        GridColumn2.Caption = "Jenis"
        GridColumn2.FieldName = "jenis"

        GridColumn3.Caption = "Saldo"
        GridColumn3.FieldName = "saldo"
        GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn3.DisplayFormat.FormatString = "##,##0"

        GridColumn4.Caption = "id baris"
        GridColumn4.FieldName = "idbaris"
        GridColumn4.Visible = False

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()

        sql = "(SELECT 'PENDAPATAN' AS tipe, 'Penjualan' AS jenis, IFNULL(SUM(total_penjualan), 0) AS saldo, '1' AS idbaris FROM tb_penjualan WHERE MONTHNAME(tgl_penjualan) = '" & cmbmonth.Text & "' AND YEAR(tgl_penjualan) = '" & cmbyear.Text & "')
                UNION 
                (SELECT 'PENDAPATAN' AS tipe, 'Kas Masuk' AS jenis, IFNULL(SUM(saldo_kas), 0) AS saldo, '2' AS idbaris FROM tb_kas_masuk WHERE MONTHNAME(tanggal) = '" & cmbmonth.Text & "' AND YEAR(tanggal) = '" & cmbyear.Text & "')
                UNION 
                (SELECT 'PENGELUARAN' AS tipe, 'Harga Pokok Penjualan' AS jenis, (IFNULL(SUM(modal * qty), 0) * -1) AS saldo, '3' AS idbaris FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE MONTHNAME(tgl_penjualan) = '" & cmbmonth.Text & "' AND YEAR(tgl_penjualan) = '" & cmbyear.Text & "')
                UNION 
                (SELECT 'PENGELUARAN' AS tipe, 'Kas Keluar' AS jenis, (IFNULL(SUM(saldo_kas), 0) * -1) AS saldo, '4' AS idbaris FROM tb_kas_keluar WHERE MONTHNAME(tanggal) = '" & cmbmonth.Text & "' AND YEAR(tanggal) = '" & cmbyear.Text & "')
                ORDER BY idbaris ASC"

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabel()
        Call historysave("Merefresh Laporan Laba Rugi", "N/A", namaform)
    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click
        Dim rptlabarugi As ReportDocument
        Dim tabel_laba_rugi As New DataTable
        Dim baris As DataRow

        With tabel_laba_rugi
            .Columns.Add("tipe")
            .Columns.Add("jenis")
            .Columns.Add("saldo", GetType(Double))
        End With


        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_laba_rugi.NewRow
            baris("tipe") = GridView1.GetRowCellValue(i, "tipe")
            baris("jenis") = GridView1.GetRowCellValue(i, "jenis")
            baris("saldo") = GridView1.GetRowCellValue(i, "saldo")
            tabel_laba_rugi.Rows.Add(baris)
        Next

        rptlabarugi = New rptrekaplabarugi
        rptlabarugi.Database.Tables(1).SetDataSource(tabel_laba_rugi)

        rptlabarugi.SetParameterValue("bulan", cmbmonth.Text)
        rptlabarugi.SetParameterValue("tahun", cmbyear.Text)
        flaplabarugi.CrystalReportViewer1.ReportSource = rptlabarugi
        flaplabarugi.ShowDialog()
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

                Call historysave("Mengexport Laporan Laba Rugi", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporanlabarugi_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class