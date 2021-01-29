Imports System.Data.Odbc

Public Class flaporanrekapanakhir
    Public namaform As String = "laporan-rekapan_akhir"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean

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
    Private Sub flaporanrekapanakhir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        Call grid()

        With GridView
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

        GridControl.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()

        'sql = "(SELECT 'PENDAPATAN' AS tipe, 'Penjualan' AS jenis, IFNULL(SUM(total_penjualan), 0) AS saldo, '1' AS idbaris FROM tb_penjualan WHERE MONTHNAME(tgl_penjualan) = '" & cmbmonth.Text & "' AND YEAR(tgl_penjualan) = '" & cmbyear.Text & "')
        '        UNION 
        '        (SELECT 'PENDAPATAN' AS tipe, 'Kas Masuk' AS jenis, IFNULL(SUM(saldo_kas), 0) AS saldo, '2' AS idbaris FROM tb_kas_masuk WHERE MONTHNAME(tanggal) = '" & cmbmonth.Text & "' AND YEAR(tanggal) = '" & cmbyear.Text & "')
        '        UNION 
        '        (SELECT 'PENGELUARAN' AS tipe, 'Harga Pokok Penjualan' AS jenis, (IFNULL(SUM(modal * qty), 0) * -1) AS saldo, '3' AS idbaris FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id WHERE MONTHNAME(tgl_penjualan) = '" & cmbmonth.Text & "' AND YEAR(tgl_penjualan) = '" & cmbyear.Text & "')
        '        UNION 
        '        (SELECT 'PENGELUARAN' AS tipe, 'Kas Keluar' AS jenis, (IFNULL(SUM(saldo_kas), 0) * -1) AS saldo, '4' AS idbaris FROM tb_kas_keluar WHERE MONTHNAME(tanggal) = '" & cmbmonth.Text & "' AND YEAR(tanggal) = '" & cmbyear.Text & "')
        '        ORDER BY idbaris ASC"

        'da = New OdbcDataAdapter(sql, cnn)
        'ds = New DataSet
        'da.Fill(ds)

        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click

    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click

    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click

    End Sub

    Private Sub flaporanrekapanakhir_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class