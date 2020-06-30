Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class flaporanmutasibarang
    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim kode As String
    Dim namabarang As String

    Private Sub flaporanmutasibarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()

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

        Call historysave("Membuka Laporan Mutasi Barang", "N/A")
    End Sub

    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang = '" + kode + "'"
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
        Call koneksii()
        cmbbarang.Items.Clear()
        cmbbarang.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_barang", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbbarang.AutoCompleteCustomSource.Add(dr("kode_barang"))
                cmbbarang.Items.Add(dr("kode_barang"))
            End While
        End If
    End Sub

    Sub comboboxstok()
        Call koneksii()
        cmbstok.Items.Clear()
        cmbstok.AutoCompleteCustomSource.Clear()
        If cmbbarang.Text.Length = 0 Then
            cmmd = New OdbcCommand("SELECT * FROM tb_stok", cnn)
        Else
            cmmd = New OdbcCommand("SELECT * FROM tb_stok WHERE kode_barang='" & cmbbarang.Text & "'", cnn)
        End If
        'cmmd = New OdbcCommand("SELECT * FROM tb_stok", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbstok.AutoCompleteCustomSource.Add(dr("kode_stok"))
                cmbstok.Items.Add(dr("kode_stok"))
            End While
        End If
    End Sub

    Sub comboboxgudang()
        Call koneksii()
        cmbgudang.Items.Clear()
        cmbgudang.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_gudang", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbgudang.AutoCompleteCustomSource.Add(dr("kode_gudang"))
                cmbgudang.Items.Add(dr("kode_gudang"))
            End While
        End If
    End Sub

    Sub grid()
        GridColumn1.Caption = "Kode Tabel"
        GridColumn1.FieldName = "kode_tabel"
        GridColumn1.Width = 15

        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Width = 15

        GridColumn3.Caption = "Kode Stok"
        GridColumn3.FieldName = "kode_stok"
        GridColumn3.Width = 15

        GridColumn4.Caption = "Nama Barang"
        GridColumn4.FieldName = "nama_barang"
        GridColumn4.Width = 35

        GridColumn5.Caption = "Banyak"
        GridColumn5.FieldName = "qty"
        GridColumn5.Width = 5
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "##,##0"

        GridColumn6.Caption = "Dari Gudang"
        GridColumn6.FieldName = "dari_gudang"
        GridColumn6.Width = 20

        GridColumn7.Caption = "Ke Gudang"
        GridColumn7.FieldName = "ke_gudang"
        GridColumn7.Width = 20

        GridColumn8.Caption = "Tanggal Transaksi"
        GridColumn8.FieldName = "tanggal"
        GridColumn8.Width = 20
        GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn8.DisplayFormat.FormatString = "dd/MM/yyyy"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        If Format(dtawal.Value, "yyyy-MM-dd").Equals(Format(dtakhir.Value, "yyyy-MM-dd")) Then
            If cmbbarang.Text.Length > 0 Then
                If cmbstok.Text.Length > 0 Then
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang, tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang, tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang, tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_dari_gudang='" & cmbgudang.Text & "') 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')                                 
                                ORDER BY tanggal ASC"
                    End If
                Else
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "' AND kode_dari_gudang='" & cmbgudang.Text & "')                                 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND Kode_barang='" & cmbbarang.Text & "')                                 
                                ORDER BY tanggal ASC"
                    End If
                End If
            Else
                If cmbstok.Text.Length > 0 Then
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "' AND kode_dari_gudang='" & cmbgudang.Text & "')                                 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_stok='" & cmbstok.Text & "')                                 
                                ORDER BY tanggal ASC"
                    End If
                Else
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_dari_gudang='" & cmbgudang.Text & "') 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE DATE(tb_penjualan.tgl_penjualan)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE DATE(tb_pembelian.tgl_pembelian)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE DATE(tb_barang_masuk.tgl_barang_masuk)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE DATE(tb_barang_keluar.tgl_barang_keluar)='" & Format(dtawal.Value, "yyyy-MM-dd") & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE(tb_transfer_barang.tanggal_transfer_barang)='" & Format(dtawal.Value, "yyyy-MM-dd") & "') 
                                ORDER BY tanggal ASC"
                    End If
                End If
            End If

        Else
            '==========================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================================

            If cmbbarang.Text.Length > 0 Then
                If cmbstok.Text.Length > 0 Then
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "') 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_stok='" & cmbstok.Text & "') 
                                ORDER BY tanggal ASC"
                    End If
                Else
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "' AND kode_gudang='" & cmbgudang.Text & "') 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND Kode_barang='" & cmbbarang.Text & "') 
                                ORDER BY tanggal ASC"
                    End If
                End If
            Else
                If cmbstok.Text.Length > 0 Then
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "' AND kode_gudang='" & cmbgudang.Text & "') 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_stok='" & cmbstok.Text & "') 
                                ORDER BY tanggal ASC"
                    End If
                Else
                    If cmbgudang.Text.Length > 0 Then
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_gudang='" & cmbgudang.Text & "')
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_gudang='" & cmbgudang.Text & "')
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE DATE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY AND kode_gudang='" & cmbgudang.Text & "') 
                                ORDER BY tanggal ASC"
                    Else
                        sql = "(SELECT tb_penjualan.kode_penjualan AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_penjualan.kode_gudang AS dari_gudang, tb_penjualan.kode_gudang AS ke_gudang , tb_penjualan.tgl_penjualan AS tanggal FROM tb_penjualan_detail JOIN tb_penjualan ON tb_penjualan_detail.kode_penjualan = tb_penjualan.kode_penjualan WHERE tb_penjualan.tgl_penjualan BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY )
                                UNION 
                                (SELECT tb_pembelian.kode_pembelian AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_pembelian.kode_gudang AS dari_gudang, tb_pembelian.kode_gudang AS ke_gudang , tb_pembelian.tgl_pembelian AS tanggal FROM tb_pembelian_detail JOIN tb_pembelian ON tb_pembelian_detail.kode_pembelian = tb_pembelian.kode_pembelian WHERE tb_pembelian.tgl_pembelian BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY )
                                UNION 
                                (SELECT tb_barang_masuk_detail.kode_barang_masuk AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_masuk.kode_gudang AS dari_gudang, tb_barang_masuk.kode_gudang AS ke_gudang , tb_barang_masuk.tgl_barang_masuk AS tanggal FROM tb_barang_masuk_detail JOIN tb_barang_masuk ON tb_barang_masuk_detail.kode_barang_masuk = tb_barang_masuk.kode_barang_masuk WHERE tb_barang_masuk.tgl_barang_masuk BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY )
                                UNION 
                                (SELECT tb_barang_keluar_detail.kode_barang_keluar AS kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_barang_keluar.kode_gudang AS dari_gudang, tb_barang_keluar.kode_gudang AS ke_gudang , tb_barang_keluar.tgl_barang_keluar AS tanggal FROM tb_barang_keluar_detail JOIN tb_barang_keluar ON tb_barang_keluar_detail.kode_barang_keluar = tb_barang_keluar.kode_barang_keluar WHERE tb_barang_keluar.tgl_barang_keluar BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY)
                                UNION
                                (SELECT tb_transfer_barang_detail.kode_transfer_barang as kode_tabel, kode_barang, kode_stok, nama_barang, qty, tb_transfer_barang.kode_dari_gudang as dari_gudang, tb_transfer_barang.kode_ke_gudang as ke_gudang, tb_transfer_barang.tanggal_transfer_barang as tanggal FROM tb_transfer_barang_detail JOIN tb_transfer_barang ON tb_transfer_barang_detail.kode_transfer_barang = tb_transfer_barang.kode_transfer_barang WHERE tb_transfer_barang.tanggal_transfer_barang BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY) 
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
        Call grid()
    End Sub

    Private Sub cmbbarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbbarang.SelectedIndexChanged
        Call comboboxstok()
    End Sub

    Private Sub cmbbarang_TextChanged(sender As Object, e As EventArgs) Handles cmbbarang.TextChanged
        Call comboboxstok()
    End Sub

    Private Sub btncaribarang_Click(sender As Object, e As EventArgs) Handles btncaribarang.Click
        tutupcaribarang = 4
        fcaribarang.ShowDialog()
    End Sub

    Private Sub btncaristok_Click(sender As Object, e As EventArgs) Handles btncaristok.Click
        tutupcaristok = 4
        kodegudangcari = cmbgudang.Text
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

                Call historysave("Mengexport Laporan Mutasi Barang", "N/A")
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click
        Dim rptmutasibarang As ReportDocument
        Dim tabel_mutasi_barang As New DataTable
        Dim baris As DataRow

        With tabel_mutasi_barang
            .Columns.Add("kode_tabel")
            .Columns.Add("kode_barang")
            .Columns.Add("kode_stok")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("dari_gudang")
            .Columns.Add("ke_gudang")
            .Columns.Add("tanggal")
        End With


        For i As Integer = 0 To GridView1.RowCount - 1
            baris = tabel_mutasi_barang.NewRow
            baris("kode_tabel") = GridView1.GetRowCellValue(i, "kode_tabel")
            baris("kode_barang") = GridView1.GetRowCellValue(i, "kode_barang")
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("qty") = GridView1.GetRowCellValue(i, "qty")
            baris("dari_gudang") = GridView1.GetRowCellValue(i, "dari_gudang")
            baris("ke_gudang") = GridView1.GetRowCellValue(i, "qty")
            baris("tanggal") = GridView1.GetRowCellValue(i, "tanggal")
            tabel_mutasi_barang.Rows.Add(baris)
        Next

        rptmutasibarang = New rptrekapmutasibarang
        rptmutasibarang.SetDataSource(tabel_mutasi_barang)

        flapmutasibarang.CrystalReportViewer1.ReportSource = rptmutasibarang
        flapmutasibarang.ShowDialog()
        flapmutasibarang.WindowState = FormWindowState.Maximized
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

    Private Sub flaporanmutasibarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class