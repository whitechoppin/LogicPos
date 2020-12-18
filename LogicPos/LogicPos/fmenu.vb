Imports System.Data.Odbc
Public Class fmenu
    Public namaform As String = "menu"
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        statustgl.Text = Now.ToLongDateString
        Timer.Start()
        fjatuhtempopembelian.Show()
        fjatuhtempopenjualan.Show()
        fnotifikasistok.Show()

        Call historysave("User Login Program", Me.namauser.Text, namaform)
    End Sub
    Public Sub ActiveMdiChild_FormClosed(sender As Object)
        TryCast(TryCast(sender, Form).Tag, TabPage).Dispose()
    End Sub
    Private Sub tabform_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabform.SelectedIndexChanged
        If tabform.TabCount.Equals(0) Then
        Else
            If (tabform.SelectedTab.Equals(Nothing)) Or (tabform.SelectedTab.Tag.Equals(Nothing)) Then
            Else
                TryCast(tabform.SelectedTab.Tag, Form).Select()
            End If
        End If
    End Sub
    Private Sub fmenu_MdiChildActivate(sender As Object, e As EventArgs) Handles MyBase.MdiChildActivate
        If Me.ActiveMdiChild Is Nothing Then
            tabform.Visible = False
        Else
            If Me.ActiveMdiChild.Tag Is Nothing Then
                Dim tp As New TabPage(Me.ActiveMdiChild.Text)
                tp.Tag = Me.ActiveMdiChild
                tp.Parent = tabform
                tabform.SelectedTab = tp

                Me.ActiveMdiChild.Tag = tp
            End If

            If Not tabform.Visible Then
                tabform.Visible = True
            End If
        End If
    End Sub
    Private Sub fmenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Call koneksii()
        sql = "DELETE FROM tb_status_user WHERE user_id='" & flogin.USERID & "'AND computer_id='" & flogin.CPUIDPOS & "'AND status_user='" & flogin.STATUSPOS & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        Call diskoneksii()
        Application.Exit()
    End Sub
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        statusjam.Text = TimeOfDay
    End Sub
    'master data ==========================================================================================================================
    Private Sub DataBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_barang
        If masterstatus > 0 Then
            fbarang.kodeakses = masterstatus
            fbarang.Show()
        End If
    End Sub
    Private Sub DataKategoriBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataKategoriBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_kategori
        If masterstatus > 0 Then
            fkategoribarang.kodeakses = masterstatus
            fkategoribarang.Show()
        End If
    End Sub
    Private Sub DataGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataGudangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_gudang
        If masterstatus > 0 Then
            fgudang.kodeakses = masterstatus
            fgudang.Show()
        End If
    End Sub
    Private Sub DataPelangganToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataPelangganToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_pelanggan
        If masterstatus > 0 Then
            fpelanggan.kodeakses = masterstatus
            fpelanggan.Show()
        End If
    End Sub
    Private Sub DataSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataSupplierToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_supplier
        If masterstatus > 0 Then
            fsupplier.kodeakses = masterstatus
            fsupplier.Show()
        End If
    End Sub
    Private Sub DataUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataUserToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_user
        If masterstatus > 0 Then
            fuser.kodeakses = masterstatus
            fuser.Show()
        End If
        fuser.Show()
    End Sub
    Private Sub DataKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataKasToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_kas
        If masterstatus > 0 Then
            fkas.kodeakses = masterstatus
            fkas.Show()
        End If
    End Sub
    Private Sub PricelistGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PricelistGroupToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.master_pricelist
        If masterstatus > 0 Then
            fpricelist.kodeakses = masterstatus
            fpricelist.Show()
        End If
    End Sub
    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Call historysave("User Log Out Program", "N/A", namaform)

        Me.Hide()

        flogin.Show()
        flogin.txtusername.Text = ""
        flogin.txtpassword.Text = ""
    End Sub

    Private Sub ExitStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitStripMenuItem.Click
        Call historysave("User Close Program", Me.namauser.Text, namaform)
        Me.Close()
        Me.Dispose()
        Application.Exit()
    End Sub

    'transaksi ============================================================================================================================
    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.pembelian
        If masterstatus > 0 Then
            fpembelian.kodeakses = masterstatus
            fpembelian.Show()
        End If
    End Sub
    Private Sub PenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.penjualan
        If masterstatus > 0 Then
            fpenjualan.kodeakses = masterstatus
            fpenjualan.Show()
        End If
    End Sub
    Private Sub ReturPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPembelianToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.retur_beli
        If masterstatus > 0 Then
            freturbeli.kodeakses = masterstatus
            freturbeli.Show()
        End If
    End Sub
    Private Sub ReturPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPenjualanToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.retur_jual
        If masterstatus > 0 Then
            freturjual.kodeakses = masterstatus
            freturjual.Show()
        End If
    End Sub
    Private Sub BarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangMasukToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.barang_masuk
        If masterstatus > 0 Then
            fbarangmasuk.kodeakses = masterstatus
            fbarangmasuk.Show()
        End If
    End Sub
    Private Sub BarangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangKeluarToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.barang_keluar
        If masterstatus > 0 Then
            fbarangkeluar.kodeakses = masterstatus
            fbarangkeluar.Show()
        End If
    End Sub
    Private Sub TransferBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.transfer_barang
        If masterstatus > 0 Then
            ftransferbarang.kodeakses = masterstatus
            ftransferbarang.Show()
        End If
    End Sub
    Private Sub PenyesuaianStokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenyesuaianStokToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.penyesuaian_stok
        If masterstatus > 0 Then
            fpenyesuaianstok.kodeakses = masterstatus
            fpenyesuaianstok.Show()
        End If
    End Sub
    'administrasi =========================================================================================================================
    Private Sub PreviewUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviewUtangToolStripMenuItem.Click
        fpreviewutang.Show()
    End Sub
    Private Sub PreviewPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviewPiutangToolStripMenuItem.Click
        fpreviewpiutang.Show()
    End Sub
    Private Sub PelunasanUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanUtangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lunas_utang
        If masterstatus > 0 Then
            flunasutang.kodeakses = masterstatus
            flunasutang.Show()
        End If
    End Sub
    Private Sub PelunasanUtangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PelunasanUtangToolStripMenuItem1.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lunas_utang
        If masterstatus > 0 Then
            flunasutang.kodeakses = masterstatus
            flunasutang.Show()
        End If
    End Sub
    Private Sub PelunasanPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanPiutangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lunas_piutang
        If masterstatus > 0 Then
            flunaspiutang.kodeakses = masterstatus
            flunaspiutang.Show()
        End If
    End Sub
    Private Sub PelunasanPiutangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PelunasanPiutangToolStripMenuItem1.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lunas_piutang
        If masterstatus > 0 Then
            flunaspiutang.kodeakses = masterstatus
            flunaspiutang.Show()
        End If
    End Sub
    Private Sub KasMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KasMasukToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.akun_masuk
        If masterstatus > 0 Then
            fkasmasuk.kodeakses = masterstatus
            fkasmasuk.Show()
        End If
    End Sub
    Private Sub KasKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KasKeluarToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.akun_keluar
        If masterstatus > 0 Then
            fkaskeluar.kodeakses = masterstatus
            fkaskeluar.Show()
        End If
    End Sub
    Private Sub TransferKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferKasToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.transfer_kas
        If masterstatus > 0 Then
            ftransferkas.kodeakses = masterstatus
            ftransferkas.Show()
        End If
    End Sub
    'laporan ==============================================================================================================================
    Private Sub LPricelistBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LPricelistBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_pricelist
        If masterstatus > 0 Then
            flaporanpricelist.kodeakses = masterstatus
            flaporanpricelist.Show()
        End If
    End Sub

    Private Sub LPembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LPembelianToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_pembelian
        If masterstatus > 0 Then
            flaporanpembelian.kodeakses = masterstatus
            flaporanpembelian.Show()
        End If
    End Sub
    Private Sub LPenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LPenjualanToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_penjualan
        If masterstatus > 0 Then
            flaporanpenjualan.kodeakses = masterstatus
            flaporanpenjualan.Show()
        End If
    End Sub
    Private Sub LPenjualanPajakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LPenjualanPajakToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_penjualan_pajak
        If masterstatus > 0 Then
            flaporanpenjualanpajak.kodeakses = masterstatus
            flaporanpenjualanpajak.Show()
        End If
    End Sub
    Private Sub LReturBeliToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LReturBeliToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_returbeli
        If masterstatus > 0 Then
            flaporanreturbeli.kodeakses = masterstatus
            flaporanreturbeli.Show()
        End If
    End Sub
    Private Sub LReturJualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LReturJualToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_returjual
        If masterstatus > 0 Then
            flaporanreturjual.kodeakses = masterstatus
            flaporanreturjual.Show()
        End If
    End Sub
    Private Sub LBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LBarangMasukToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_barangmasuk
        If masterstatus > 0 Then
            flaporanbarangmasuk.kodeakses = masterstatus
            flaporanbarangmasuk.Show()
        End If
    End Sub
    Private Sub LBarangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LBarangKeluarToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_barangkeluar
        If masterstatus > 0 Then
            flaporanbarangkeluar.kodeakses = masterstatus
            flaporanbarangkeluar.Show()
        End If
    End Sub
    Private Sub LTransferBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransferBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_transfer_barang
        If masterstatus > 0 Then
            flaporantransferbarang.kodeakses = masterstatus
            flaporantransferbarang.Show()
        End If
    End Sub
    Private Sub LStokBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LStokBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_stok_barang
        If masterstatus > 0 Then
            flaporanstokbarang.kodeakses = masterstatus
            flaporanstokbarang.Show()
        End If
    End Sub
    Private Sub LUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LUtangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_lunas_utang
        If masterstatus > 0 Then
            flaporanlunasutang.kodeakses = masterstatus
            flaporanlunasutang.Show()
        End If
    End Sub
    Private Sub LPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LPiutangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_lunas_piutang
        If masterstatus > 0 Then
            flaporanlunaspiutang.kodeakses = masterstatus
            flaporanlunaspiutang.Show()
        End If
    End Sub
    Private Sub LAkunMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LAkunMasukToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_akun_masuk
        If masterstatus > 0 Then
            flaporankasmasuk.kodeakses = masterstatus
            flaporankasmasuk.Show()
        End If
    End Sub
    Private Sub LAkunKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LAkunKeluarToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_akun_keluar
        If masterstatus > 0 Then
            flaporankaskeluar.kodeakses = masterstatus
            flaporankaskeluar.Show()
        End If
    End Sub
    Private Sub LTransferKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransferKasToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_transfer_kas
        If masterstatus > 0 Then
            flaporantransferkas.kodeakses = masterstatus
            flaporantransferkas.Show()
        End If
    End Sub
    Private Sub LTransaksiKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransaksiKasToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_transaksi_kas
        If masterstatus > 0 Then
            flaporantransaksikas.kodeakses = masterstatus
            flaporantransaksikas.Show()
        End If
    End Sub
    Private Sub LModalBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LModalBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_modal_barang
        If masterstatus > 0 Then
            flaporanmodalbarang.kodeakses = masterstatus
            flaporanmodalbarang.Show()
        End If
    End Sub
    Private Sub LMutasiBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LMutasiBarangToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_mutasi_barang
        If masterstatus > 0 Then
            flaporanmutasibarang.kodeakses = masterstatus
            flaporanmutasibarang.Show()
        End If
    End Sub
    Private Sub LPenyesuaianStokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LPenyesuaianStokToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_penyesuaian_stok
        If masterstatus > 0 Then
            flaporanpenyesuaianstok.kodeakses = masterstatus
            flaporanpenyesuaianstok.Show()
        End If
    End Sub
    Private Sub LLabaRugiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LLabaRugiToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_laba_rugi
        If masterstatus > 0 Then
            flaporanlabarugi.kodeakses = masterstatus
            flaporanlabarugi.Show()
        End If
    End Sub
    Private Sub LRekapanAkhirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LRekapanAkhirToolStripMenuItem.Click
        Dim masterstatus As Integer = 0
        masterstatus = flogin.lap_rekapan_akhir
        If masterstatus > 0 Then
            flaporanrekapanakhir.kodeakses = masterstatus
            flaporanrekapanakhir.Show()
        End If
    End Sub

    'chart =============================================================================================================================

    Private Sub ChartPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChartPembelianToolStripMenuItem.Click
        fchartpembelian.Show()
    End Sub

    Private Sub ChartPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChartPenjualanToolStripMenuItem.Click
        fchartpenjualan.Show()
    End Sub

    Private Sub ChartPelunasanUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChartPelunasanUtangToolStripMenuItem.Click
        fchartpelunasanutang.Show()
    End Sub

    Private Sub ChartPelunasanPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChartPelunasanPiutangToolStripMenuItem.Click
        fchartpelunasanpiutang.Show()
    End Sub

    Private Sub ChartKasMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChartKasMasukToolStripMenuItem.Click

    End Sub

    Private Sub ChartKasKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChartKasKeluarToolStripMenuItem.Click

    End Sub

    'feature ==============================================================================================================================

    Private Sub ContainerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContainerToolStripMenuItem.Click
        fkalkulasiexpedisi.Show()
    End Sub

    Private Sub PengirimanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengirimanToolStripMenuItem.Click
        fkalkulasipengiriman.Show()
    End Sub

    Private Sub LaporanContainerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanContainerToolStripMenuItem.Click
        flaporankalkulasiexpedisi.Show()
    End Sub

    Private Sub LaporanPengirimanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPengirimanToolStripMenuItem.Click
        flaporankalkulasipengiriman.Show()
    End Sub

    Private Sub BarcodeGeneratorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeGeneratorToolStripMenuItem.Click
        fgeneratebarcode.Show()
    End Sub

    'settings =============================================================================================================================
    Private Sub InfoPerusahaanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoPerusahaanToolStripMenuItem.Click
        finfoperusahaan.Show()
    End Sub

    Private Sub PrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrinterToolStripMenuItem.Click
        fprinter.Show()
    End Sub

    Private Sub JatuhTempoPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JatuhTempoPembelianToolStripMenuItem.Click
        fjatuhtempopembelian.Show()
    End Sub

    Private Sub JatuhTempoPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JatuhTempoPenjualanToolStripMenuItem.Click
        fjatuhtempopenjualan.Show()
    End Sub

    Private Sub NotifikasiStokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NotifikasiStokToolStripMenuItem.Click
        fnotifikasistok.Show()
    End Sub

    Private Sub HistoryUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistoryUserToolStripMenuItem.Click
        fhistoryuser.Show()
    End Sub

    Private Sub KonfigurasiDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KonfigurasiDatabaseToolStripMenuItem.Click

    End Sub

    Private Sub PengaturanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengaturanToolStripMenuItem.Click

    End Sub


    'about ================================================================================================================================
    Private Sub LogicHouseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogicHouseToolStripMenuItem.Click
        flogichouse.Show()
    End Sub

End Class
