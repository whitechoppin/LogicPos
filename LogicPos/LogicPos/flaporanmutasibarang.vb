Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class flaporanmutasibarang
    Public namaform As String = "laporan-mutasi_barang"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim kode As String
    Dim namabarang As String

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

    Private Sub flaporanmutasibarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksi()

        dtawal.Value = Now
        dtakhir.Value = Now
        Call grid()

        With GridView1
            .OptionsView.ShowFooter = True 'agar muncul footer untuk sum/avg/count
            'buat sum harga
            .Columns("qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", "{0:n0}")
            ' .Columns("keuntungan").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "keuntungan", "{0:n0}")
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

        Call comboboxbarang()
        Call comboboxstok()
        Call comboboxgudang()

        Call historysave("Membuka Laporan Mutasi Barang", "N/A", namaform)
    End Sub

    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Call koneksi()
        sql = "SELECT * FROM tb_barang WHERE kode_barang = '" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            namabarang = dr("nama_barang")
            txtgbr.Text = namabarang
            foto = dr("gambar_barang")
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
        End If
    End Sub

    Sub comboboxbarang()
        Call koneksi()
        sql = "SELECT * FROM tb_barang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbbarang.DataSource = ds.Tables(0)
        cmbbarang.ValueMember = "kode_barang"
        cmbbarang.DisplayMember = "kode_barang"
    End Sub

    Sub comboboxstok()
        Call koneksi()
        cmbstok.DataSource = Nothing
        If cmbbarang.Text.Length = 0 Then
            sql = "SELECT DISTINCT kode_stok FROM tb_stok"
        Else
            sql = "SELECT DISTINCT kode_stok FROM tb_stok WHERE kode_barang ='" & cmbbarang.Text & "'"
        End If
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbstok.DataSource = ds.Tables(0)
        cmbstok.ValueMember = "kode_stok"
        cmbstok.DisplayMember = "kode_stok"
    End Sub

    Sub comboboxgudang()
        Call koneksi()
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbgudang.DataSource = ds.Tables(0)
        cmbgudang.ValueMember = "id"
        cmbgudang.DisplayMember = "kode_gudang"
    End Sub

    Sub grid()
        GridColumn1.Caption = "Tabel"
        GridColumn1.FieldName = "tabel"
        GridColumn1.Width = 5

        GridColumn2.Caption = "id"
        GridColumn2.FieldName = "id"
        GridColumn2.Width = 5

        GridColumn3.Caption = "Kode Barang"
        GridColumn3.FieldName = "kode_barang"
        GridColumn3.Width = 15

        GridColumn4.Caption = "Kode Stok"
        GridColumn4.FieldName = "kode_stok"
        GridColumn4.Width = 15

        GridColumn5.Caption = "Nama Barang"
        GridColumn5.FieldName = "nama_barang"
        GridColumn5.Width = 50

        GridColumn6.Caption = "Banyak"
        GridColumn6.FieldName = "qty"
        GridColumn6.Width = 5
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "##,##0"

        GridColumn7.Caption = "Gudang"
        GridColumn7.FieldName = "gudang"
        GridColumn7.Width = 15
        GridColumn7.Visible = False

        GridColumn8.Caption = "Tanggal Transaksi"
        GridColumn8.FieldName = "tanggal"
        GridColumn8.Width = 30
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()

        If cbperiode.Checked = True Then
            If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                If cmbbarang.Text.Length > 0 Then
                    If cmbstok.Text.Length > 0 Then
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    Else
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    End If
                Else
                    If cmbstok.Text.Length > 0 Then
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    Else
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                ORDER BY tanggal ASC"
                        End If
                    End If
                End If

            Else
                '==========================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================

                If cmbbarang.Text.Length > 0 Then
                    If cmbstok.Text.Length > 0 Then
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    Else
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    End If
                Else
                    If cmbstok.Text.Length > 0 Then
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    Else
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' )
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' )
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' )
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.idWHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.idWHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "')
                                ORDER BY tanggal ASC"
                        End If
                    End If
                End If

            End If
        Else
            '=======================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================
            If cmbbarang.Text.Length > 0 Then
                If cmbstok.Text.Length > 0 Then
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                    End If
                Else
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "')
                                ORDER BY tanggal ASC"
                    End If
                End If
            Else
                If cmbstok.Text.Length > 0 Then
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                    End If
                Else
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id)
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id)
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id)
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id)
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id) 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id)
                                ORDER BY tanggal ASC"
                    End If
                End If
            End If
        End If


        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
    End Sub

    Private Sub cmbbarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbbarang.SelectedIndexChanged
        If cmbbarang.SelectedIndex > -1 Then
            Call comboboxstok()
        End If
    End Sub

    Private Sub cmbbarang_TextChanged(sender As Object, e As EventArgs) Handles cmbbarang.TextChanged
        If cmbbarang.Text.Length > 0 Then
            Call comboboxstok()
        End If

    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupcaribarang = 4
        fcaribarang.ShowDialog()
    End Sub

    Private Sub btncaristok_Click(sender As Object, e As EventArgs) Handles btncaristok.Click
        tutupcaristok = 4
        idgudangcari = cmbgudang.Text
        fcaristok.ShowDialog()
    End Sub

    Private Sub btncarigudang_Click(sender As Object, e As EventArgs) Handles btncarigudang.Click
        tutupgudang = 7
        fcarigudang.ShowDialog()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
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
        If exportstatus.Equals(True) Then
            If GridView1.DataRowCount > 0 Then
                ExportToExcel()

                Call historysave("Mengexport Laporan Mutasi Barang", "N/A", namaform)
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click
        If printstatus.Equals(True) Then
            Dim rptmutasibarang As ReportDocument

            If cbperiode.Checked = True Then
                If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
                    If cmbbarang.Text.Length > 0 Then
                        If cmbstok.Text.Length > 0 Then
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                            End If
                        Else
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                ORDER BY tanggal ASC"
                            End If
                        End If
                    Else
                        If cmbstok.Text.Length > 0 Then
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')                                 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                            End If
                        Else
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                ORDER BY tanggal ASC"
                            End If
                        End If
                    End If

                Else
                    '==========================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================

                    If cmbbarang.Text.Length > 0 Then
                        If cmbstok.Text.Length > 0 Then
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                            End If
                        Else
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_barang='" & cmbbarang.Text & "')
                                ORDER BY tanggal ASC"
                            End If
                        End If
                    Else
                        If cmbstok.Text.Length > 0 Then
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                            End If
                        Else
                            If cmbgudang.Text.Length > 0 Then
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                            Else
                                sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE DATE(tb_penjualan.tgl_penjualan) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' )
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE DATE(tb_pembelian.tgl_pembelian) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' )
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE DATE(tb_barang_masuk.tgl_barang_masuk) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' )
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.idWHERE DATE(tb_barang_keluar.tgl_barang_keluar) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.idWHERE DATE(tb_transfer_barang.tanggal_transfer_barang) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE DATE(tb_penyesuaian_stok.tanggal_penyesuaian_stok) BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "')
                                ORDER BY tanggal ASC"
                            End If
                        End If
                    End If

                End If
            Else
                '=======================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================
                If cmbbarang.Text.Length > 0 Then
                    If cmbstok.Text.Length > 0 Then
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    Else
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_barang='" & cmbbarang.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_barang='" & cmbbarang.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    End If
                Else
                    If cmbstok.Text.Length > 0 Then
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_stok='" & cmbstok.Text & "' AND gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE kode_stok='" & cmbstok.Text & "') 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE kode_stok='" & cmbstok.Text & "')
                                ORDER BY tanggal ASC"
                        End If
                    Else
                        If cmbgudang.Text.Length > 0 Then
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id WHERE gudang_id='" & cmbgudang.SelectedValue & "')
                                ORDER BY tanggal ASC"
                        Else
                            sql = "(SELECT 'JUAL' AS tabel, tb_penjualan.id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_penjualan.gudang_id AS gudang, tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.penjualan_id = tb_penjualan.id)
                                UNION 
                                (SELECT 'BELI' AS tabel, tb_pembelian.id as id, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.gudang_id AS gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.pembelian_id = tb_pembelian.id)
                                UNION 
                                (SELECT 'MASUK' AS tabel, tb_barang_masuk_detail.barang_masuk_id as id, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.gudang_id AS gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.barang_masuk_id = tb_barang_masuk.id)
                                UNION 
                                (SELECT 'KELUAR' AS tabel, tb_barang_keluar_detail.barang_keluar_id as id, kode_barang, kode_stok, nama_barang, (qty * -1) AS qty, tb_barang_keluar.gudang_id AS gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.barang_keluar_id = tb_barang_keluar.id)
                                UNION
                                (SELECT 'TRANSFER' AS tabel, tb_transfer_barang_detail_mutasi.transfer_barang_id as id, kode_barang, kode_stok, nama_barang, qty,gudang_id as gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail_mutasi JOIN tb_transfer_barang ON tb_transfer_barang_detail_mutasi.transfer_barang_id = tb_transfer_barang.id) 
                                UNION
                                (SELECT 'PENYESUAIAN' AS tabel, tb_penyesuaian_stok.id AS id, kode_barang, kode_stok, nama_barang, IF(STRCMP(status_stok,'MINUS') = 0, (qty * -1), qty * 1) AS qty, tb_penyesuaian_stok.gudang_id AS gudang, tb_penyesuaian_stok.tanggal_penyesuaian_stok AS tanggal FROM tb_penyesuaian_stok_detail JOIN tb_penyesuaian_stok ON tb_penyesuaian_stok_detail.penyesuaian_stok_id = tb_penyesuaian_stok.id)
                                ORDER BY tanggal ASC"
                        End If
                    End If
                End If
            End If

            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)

            rptmutasibarang = New rptrekapmutasibarang
            rptmutasibarang.SetDataSource(ds.Tables(0))

            rptmutasibarang.SetParameterValue("tglawal", dtawal.Text)
            rptmutasibarang.SetParameterValue("tglakhir", dtakhir.Text)

            flapmutasibarang.CrystalReportViewer.ReportSource = rptmutasibarang
            flapmutasibarang.ShowDialog()
        Else
            MsgBox("Tidak ada akses")
        End If

    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call ambil_gbr()
    End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        Call ambil_gbr()
    End Sub

    Private Sub GridControl1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyUp
        Call ambil_gbr()
    End Sub

    Private Sub cbperiode_CheckedChanged(sender As Object, e As EventArgs) Handles cbperiode.CheckedChanged
        If cbperiode.Checked = True Then
            dtawal.Enabled = True
            dtakhir.Enabled = True
        Else
            dtawal.Enabled = False
            dtakhir.Enabled = False
        End If
    End Sub

    Private Sub flaporanmutasibarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class