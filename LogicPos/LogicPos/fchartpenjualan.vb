Imports System.Data.Odbc

Public Class fchartpenjualan
    Public namaform As String = "chart-penjualan"

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
    Private Sub fchartpenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now

        Call historysave("Membuka Chart Penjualan", "N/A", namaform)
    End Sub

    Sub LoadChart()
        Me.Cursor = Cursors.WaitCursor

        Call koneksii()
        sql = "SELECT SUM(total_penjualan) AS total, MONTH(tgl_penjualan) as bulan FROM tb_penjualan GROUP BY MONTH(tgl_penjualan)"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        Dim dtTemp As New DataTable

        Try
            'dtTemp = FillDataTable("SELECT SUM(NET) AS NETT, TGL2 FROM DAT2 WHERE year(TGL2)=" & tgl.Year & " AND month(TGL2)=" & tgl.Month & " AND PROD LIKE 'c%' GROUP BY TGL2 ORDER BY TGL2")

            ChartControl1.Series("Series 1").Visible = True
            ChartControl1.Series("Series 1").DataSource = ds.Tables(0)
            ChartControl1.Series("Series 1").ValueDataMembersSerializable = "total"
            ChartControl1.Series("Series 1").ArgumentDataMember = "bulan"

            'dtTemp = FillDataTable("SELECT SUM(NET) AS NETT, TGL2 FROM DAT2 WHERE year(TGL2)=" & tgl.Year & " AND month(TGL2)=" & tgl.Month & " AND PROD LIKE 't%' GROUP BY TGL2 ORDER BY TGL2")

            'ChartControl1.Series("Series 2").Visible = True
            'ChartControl1.Series("Series 2").DataSource = ds.Tables(0)
            'ChartControl1.Series("Series 2").ValueDataMembersSerializable = "NETT"
            'ChartControl1.Series("Series 2").ArgumentDataMember = "TGL2"


            'dtTemp = FillDataTable("SELECT SUM(NET) AS NETT, TGL2 FROM DAT2 WHERE year(TGL2)=" & tgl.Year & " AND month(TGL2)=" & tgl.Month & " AND PROD LIKE 'p%' GROUP BY TGL2 ORDER BY TGL2")

            'ChartControl1.Series("Series 3").Visible = True
            'ChartControl1.Series("Series 3").DataSource = ds.Tables(0)
            'ChartControl1.Series("Series 3").ValueDataMembersSerializable = "NETT"
            'ChartControl1.Series("Series 3").ArgumentDataMember = "TGL2"

            'ChartControl1.Titles(1).Text = "PERIODE : " & Format(tgl, "MMMM yyyy")

        Catch ex As Exception
            Dim strErr As String

            strErr = "Stack trace = " & ex.StackTrace & vbCrLf & "Soure = " & ex.Source & "Error = " & ex.Message
            'WriteToEventLog(strErr, EventLogEntryType.Error, "LoadChart")
            MsgBox("Ada kesalahan, silahkan tutup form lalu coba buka kembali." & vbCrLf & "Error : " & ex.Message, vbInformation)
        End Try
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click

    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click

    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click

    End Sub

    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click

    End Sub
End Class