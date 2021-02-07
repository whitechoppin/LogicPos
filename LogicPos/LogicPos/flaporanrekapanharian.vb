Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class flaporanrekapanharian
    Public namaform As String = "laporan-rekapan_harian"

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
    Private Sub flaporanrekapanharian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            '.Columns("saldo").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total", "{0:n0}")
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

        Call historysave("Membuka Laporan Rekapan Harian", "N/A", namaform)
    End Sub

    Sub grid()
        GridColumn1.Caption = "tipe"
        GridColumn1.FieldName = "tipe"

        GridColumn2.Caption = "no"
        GridColumn2.FieldName = "no_transaksi"

        GridColumn3.Caption = "Barang"
        GridColumn3.FieldName = "nama_barang"

        GridColumn4.Caption = "qty"
        GridColumn4.FieldName = "qty"

        GridColumn5.Caption = "harga"
        GridColumn5.FieldName = "harga"
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

        GridColumn6.Caption = "total"
        GridColumn6.FieldName = "total"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "gudang"
        GridColumn7.FieldName = "gudang"

        GridControl1.Visible = True

        '========================================================================================

        GridColumn8.Caption = "tipe"
        GridColumn8.FieldName = "tipe"

        GridColumn9.Caption = "no"
        GridColumn9.FieldName = "no_transaksi"

        GridColumn10.Caption = "keterangan"
        GridColumn10.FieldName = "keterangan"

        GridColumn11.Caption = "saldo"
        GridColumn11.FieldName = "saldo"
        GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn11.DisplayFormat.FormatString = "##,##0"

        GridControl2.Visible = True
    End Sub
    Sub tabeltransaksi()
        Call koneksi()

        sql = "(SELECT 'PEMBELIAN' AS tipe, pembelian_id AS no_transaksi, nama_barang, qty, harga_beli AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id JOIN tb_gudang ON tb_gudang.id = tb_pembelian.gudang_id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'PENJUALAN' AS tipe, penjualan_id AS no_transaksi, nama_barang, qty, harga_jual AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id JOIN tb_gudang ON tb_gudang.id = tb_penjualan.gudang_id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'BARANG MASUK' AS tipe, barang_masuk_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk.id = tb_barang_masuk_detail.barang_masuk_id JOIN tb_gudang ON tb_gudang.id = tb_barang_masuk.gudang_id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'BARANG KELUAR' AS tipe, barang_keluar_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar.id = tb_barang_keluar_detail.barang_keluar_id JOIN tb_gudang ON tb_gudang.id = tb_barang_keluar.gudang_id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'RETUR PEMBELIAN' AS tipe, retur_pembelian_id AS no_transaksi, nama_barang, qty, harga_beli AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_retur_pembelian_detail JOIN tb_retur_pembelian ON tb_retur_pembelian.id = tb_retur_pembelian_detail.retur_pembelian_id JOIN tb_pembelian ON tb_pembelian.id = tb_retur_pembelian.pembelian_id JOIN tb_gudang ON tb_gudang.id = tb_pembelian.gudang_id WHERE DATE(tb_retur_pembelian.tgl_returbeli)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'RETUR PENJUALAN' AS tipe, retur_penjualan_id AS no_transaksi, nama_barang, qty, harga_jual AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_retur_penjualan_detail JOIN tb_retur_penjualan ON tb_retur_penjualan.id = tb_retur_penjualan_detail.retur_penjualan_id JOIN tb_penjualan ON tb_penjualan.id = tb_retur_penjualan.penjualan_id JOIN tb_gudang ON tb_gudang.id = tb_penjualan.gudang_id WHERE DATE(tb_retur_penjualan.tgl_returjual)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'TRANSFER BARANG' AS tipe, transfer_barang_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang.id = tb_transfer_barang_detail.transfer_barang_id JOIN tb_gudang ON tb_gudang.id = tb_transfer_barang.dari_gudang_id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'PENYESUAIAN STOK' AS tipe, penyesuaian_stok_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok.id = tb_penyesuaian_stok_detail.penyesuaian_stok_id JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')"

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
    End Sub

    Sub tabeladministrasi()
        Call koneksi()

        sql = "(SELECT 'PELUNASAN UTANG' AS tipe, pelunasan_utang_id AS no_transaksi, CONCAT('Pelunasan Nota Pembelian no : ', pembelian_id) AS keterangan, terima_utang AS saldo FROM tb_pelunasan_utang_detail JOIN tb_pelunasan_utang ON tb_pelunasan_utang.id = tb_pelunasan_utang_detail.pelunasan_utang_id WHERE DATE(tb_pelunasan_utang.tanggal_transaksi)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'PELUNASAN PIUTANG' AS tipe, pelunasan_piutang_id AS no_transaksi, CONCAT('Pelunasan Nota Penjualan no : ', penjualan_id) AS keterangan, terima_piutang AS saldo FROM tb_pelunasan_piutang_detail JOIN tb_pelunasan_piutang ON tb_pelunasan_piutang.id = tb_pelunasan_piutang_detail.pelunasan_piutang_id WHERE DATE(tb_pelunasan_piutang.tanggal_transaksi)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'KAS MASUK' AS tipe, id AS no_transaksi, keterangan_kas AS keterangan, saldo_kas AS saldo FROM tb_kas_masuk WHERE DATE(tb_kas_masuk.tanggal)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'KAS KELUAR' AS tipe, id AS no_transaksi, keterangan_kas AS keterangan, saldo_kas AS saldo FROM tb_kas_keluar WHERE DATE(tb_kas_keluar.tanggal)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'TRANSFER KAS' AS tipe, id AS no_transaksi, CONCAT('Transfer Kas dari : ', dari_kas_id,' ke ', ke_kas_id,' Keterangan :', keterangan_transfer_kas) AS keterangan, saldo_transfer_kas AS saldo FROM tb_transfer_kas WHERE DATE(tb_transfer_kas.tanggal)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')"

        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl2.DataSource = Nothing
        GridControl2.DataSource = ds.Tables(0)
    End Sub

    Private Sub btntabel_Click(sender As Object, e As EventArgs) Handles btntabel.Click
        Call tabeltransaksi()
        Call tabeladministrasi()
        Call historysave("Merefresh Laporan Rekapan Harian", "N/A", namaform)
    End Sub

    Private Sub btnrekaptransaksi_Click(sender As Object, e As EventArgs) Handles btnrekaptransaksi.Click
        If printstatus.Equals(True) Then
            Dim rptrekaphariantransaksi As ReportDocument

            Call koneksi()

            sql = "(SELECT 'PEMBELIAN' AS tipe, pembelian_id AS no_transaksi, nama_barang, qty, harga_beli AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian.id = tb_pembelian_detail.pembelian_id JOIN tb_gudang ON tb_gudang.id = tb_pembelian.gudang_id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'PENJUALAN' AS tipe, penjualan_id AS no_transaksi, nama_barang, qty, harga_jual AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan.id = tb_penjualan_detail.penjualan_id JOIN tb_gudang ON tb_gudang.id = tb_penjualan.gudang_id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'BARANG MASUK' AS tipe, barang_masuk_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk.id = tb_barang_masuk_detail.barang_masuk_id JOIN tb_gudang ON tb_gudang.id = tb_barang_masuk.gudang_id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'BARANG KELUAR' AS tipe, barang_keluar_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar.id = tb_barang_keluar_detail.barang_keluar_id JOIN tb_gudang ON tb_gudang.id = tb_barang_keluar.gudang_id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'RETUR PEMBELIAN' AS tipe, retur_pembelian_id AS no_transaksi, nama_barang, qty, harga_beli AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_retur_pembelian_detail JOIN tb_retur_pembelian ON tb_retur_pembelian.id = tb_retur_pembelian_detail.retur_pembelian_id JOIN tb_pembelian ON tb_pembelian.id = tb_retur_pembelian.pembelian_id JOIN tb_gudang ON tb_gudang.id = tb_pembelian.gudang_id WHERE DATE(tb_retur_pembelian.tgl_returbeli)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'RETUR PENJUALAN' AS tipe, retur_penjualan_id AS no_transaksi, nama_barang, qty, harga_jual AS harga, subtotal AS total, nama_gudang AS gudang FROM tb_retur_penjualan_detail JOIN tb_retur_penjualan ON tb_retur_penjualan.id = tb_retur_penjualan_detail.retur_penjualan_id JOIN tb_penjualan ON tb_penjualan.id = tb_retur_penjualan.penjualan_id JOIN tb_gudang ON tb_gudang.id = tb_penjualan.gudang_id WHERE DATE(tb_retur_penjualan.tgl_returjual)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'TRANSFER BARANG' AS tipe, transfer_barang_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang.id = tb_transfer_barang_detail.transfer_barang_id JOIN tb_gudang ON tb_gudang.id = tb_transfer_barang.dari_gudang_id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'PENYESUAIAN STOK' AS tipe, penyesuaian_stok_id AS no_transaksi, nama_barang, qty, 0 AS harga, 0 AS total, nama_gudang AS gudang FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok.id = tb_penyesuaian_stok_detail.penyesuaian_stok_id JOIN tb_gudang ON tb_gudang.id = tb_penyesuaian_stok.gudang_id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')"

            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)

            rptrekaphariantransaksi = New rptrekaplaporantransaksi
            rptrekaphariantransaksi.Database.Tables(1).SetDataSource(ds.Tables(0))

            rptrekaphariantransaksi.SetParameterValue("tanggal", dttanggal.Text)

            flaprekapanharian.CrystalReportViewer.ReportSource = rptrekaphariantransaksi
            flaprekapanharian.ShowDialog()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub ExportToExcel1()
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

    Private Sub btnexporttransaksi_Click(sender As Object, e As EventArgs) Handles btnexporttransaksi.Click
        If exportstatus.Equals(True) Then
            If GridView1.DataRowCount > 0 Then
                ExportToExcel1()
                Call historysave("Mengexport Laporan Rekapan Akhir", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnrekapadmin_Click(sender As Object, e As EventArgs) Handles btnrekapadmin.Click
        If printstatus.Equals(True) Then
            Dim rptrekapharianadmin As ReportDocument

            Call koneksi()

            sql = "(SELECT 'PELUNASAN UTANG' AS tipe, pelunasan_utang_id AS no_transaksi, CONCAT('Pelunasan Nota Pembelian no : ', pembelian_id) AS keterangan, terima_utang AS saldo FROM tb_pelunasan_utang_detail JOIN tb_pelunasan_utang ON tb_pelunasan_utang.id = tb_pelunasan_utang_detail.pelunasan_utang_id WHERE DATE(tb_pelunasan_utang.tanggal_transaksi)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'PELUNASAN PIUTANG' AS tipe, pelunasan_piutang_id AS no_transaksi, CONCAT('Pelunasan Nota Penjualan no : ', penjualan_id) AS keterangan, terima_piutang AS saldo FROM tb_pelunasan_piutang_detail JOIN tb_pelunasan_piutang ON tb_pelunasan_piutang.id = tb_pelunasan_piutang_detail.pelunasan_piutang_id WHERE DATE(tb_pelunasan_piutang.tanggal_transaksi)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'KAS MASUK' AS tipe, id AS no_transaksi, keterangan_kas AS keterangan, saldo_kas AS saldo FROM tb_kas_masuk WHERE DATE(tb_kas_masuk.tanggal)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'KAS KELUAR' AS tipe, id AS no_transaksi, keterangan_kas AS keterangan, saldo_kas AS saldo FROM tb_kas_keluar WHERE DATE(tb_kas_keluar.tanggal)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')
                UNION
                (SELECT 'TRANSFER KAS' AS tipe, id AS no_transaksi, CONCAT('Transfer Kas dari : ', dari_kas_id,' ke ', ke_kas_id,' Keterangan :', keterangan_transfer_kas) AS keterangan, saldo_transfer_kas AS saldo FROM tb_transfer_kas WHERE DATE(tb_transfer_kas.tanggal)='" & Format(dttanggal.Value, "yyyy-MM-dd") & "')"

            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)

            rptrekapharianadmin = New rptrekaplaporanadmin
            rptrekapharianadmin.Database.Tables(1).SetDataSource(ds.Tables(0))

            rptrekapharianadmin.SetParameterValue("tanggal", dttanggal.Text)

            flaprekapanharian.CrystalReportViewer.ReportSource = rptrekapharianadmin
            flaprekapanharian.ShowDialog()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub ExportToExcel2()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            GridView2.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Private Sub btnexportadmin_Click(sender As Object, e As EventArgs) Handles btnexportadmin.Click
        If exportstatus.Equals(True) Then
            If GridView2.DataRowCount > 0 Then
                ExportToExcel2()
                Call historysave("Mengexport Laporan Rekapan Akhir", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub flaporanrekapanharian_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class