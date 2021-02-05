Imports System.Data.Odbc
Imports System.Text.RegularExpressions

Public Class fuser
    Public namaform As String = "master-user"

    Dim kodeuser, iduser As String
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean

    Dim cekauthuser As Integer
    Dim aksesauthuser As Integer

    Dim maxprint As Integer

    'master
    Dim cekmasterbarang, cekmasterkategori, cekmastergudang, cekmasterpelanggan, cekmastersupplier, cekmasteruser, cekmasterkas, cekmasterpricelist, cekmasterreksupp, cekmasterrekplng As Integer
    Dim aksesbarang, akseskategori, aksesgudang, aksespelanggan, aksessupplier, aksesuser, akseskas, aksespricelist, aksesreksupp, aksesrekplng As Integer
    'transaksi
    Dim cekpembelian, cekpenjualan, cekreturbeli, cekreturjual, cekbarangmasuk, cekbarangkeluar, cektransferbarang, cekpenyesuaianstok As Integer
    Dim aksespembelian, aksespenjualan, aksesreturbeli, aksesreturjual, aksesbarangmasuk, aksesbarangkeluar, aksestransferbarang, aksespenyesuaianstok As Integer
    'administrasi
    Dim ceklunasutang, ceklunaspiutang, cektransferkas, cekakunmasuk, cekakunkeluar As Integer
    Dim akseslunasutang, akseslunaspiutang, aksestransferkas, aksesakunmasuk, aksesakunkeluar As Integer
    'laporan
    Dim ceklappricelist, ceklappembelian, ceklappenjualan, ceklappenjualanpajak, ceklapreturbeli, ceklapreturjual, ceklapbarangmasuk, ceklapbarangkeluar, ceklaptransferbarang, ceklapstokbarang, ceklaputang, ceklappiutang, ceklapakunmasuk, ceklapakunkeluar, ceklaptransferkas, ceklaptransaksikas, ceklapmodalbarang, ceklapmutasibarang, ceklappenyesuaianstok, ceklaplabarugi, ceklaprekapanharian As Integer
    Dim akseslappricelist, akseslappembelian, akseslappenjualan, akseslappenjualanpajak, akseslapreturbeli, akseslapreturjual, akseslapbarangmasuk, akseslapbarangkeluar, akseslaptransferbarang, akseslapstokbarang, akseslaputang, akseslappiutang, akseslapakunmasuk, akseslapakunkeluar, akseslaptransferkas, akseslaptransaksikas, akseslapmodalbarang, akseslapmutasibarang, akseslappenyesuaianstok, akseslaplabarugi, akseslaprekapanharian As Integer
    'chart
    Dim cekchartpembelian, cekchartpenjualan, cekchartpelunasanutang, cekchartpelunasanpiutang, cekchartakunmasuk, cekchartakunkeluar As Integer
    Dim akseschartpembelian, akseschartpenjualan, akseschartpelunasanutang, akseschartpelunasanpiutang, akseschartakunmasuk, akseschartakunkeluar As Integer
    'feature
    Dim cekfeaturekalkulasi, cekfeaturebarcode As Integer
    Dim aksesfeaturekalkulasi, aksesfeaturebarcode As Integer
    'setting
    Dim ceksettinginfoperusahaan, ceksettingprinter, ceksettingbackupdatabase, ceksettingpengaturan As Integer
    Dim aksessettinginfoperusahaan, aksessettingprinter, aksessettingbackupdatabase, aksessettingpengaturan As Integer

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

    Private Sub fuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        Call awal()
        Select Case kodeakses
            Case 1
                tambahstatus = True
                editstatus = False
                hapusstatus = False
            Case 3
                tambahstatus = False
                editstatus = True
                hapusstatus = False
            Case 5
                tambahstatus = False
                editstatus = False
                hapusstatus = True
            Case 4
                tambahstatus = True
                editstatus = True
                hapusstatus = False
            Case 6
                tambahstatus = True
                editstatus = False
                hapusstatus = True
            Case 8
                tambahstatus = False
                editstatus = True
                hapusstatus = True
            Case 9
                tambahstatus = True
                editstatus = True
                hapusstatus = True
        End Select
        Call historysave("Membuka Master User", "N/A", namaform)
    End Sub

    Private Sub fuser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub awal()
        txtkode.Clear()
        txtnama.Clear()
        txtpassword.Clear()
        cbauth.Checked = False

        cmbjabatan.SelectedIndex = -1
        txtmaxprint.Clear()

        txtemail.Clear()
        txttelp.Clear()
        txtalamat.Clear()
        txtketerangan.Clear()

        'feature
        '===========================================
        'checkbox and checkboxlist
        '===========================================

        'master
        If flogin.xmaster_barang > 0 Then
            cbmasterbarang.Visible = True
            clbmasterbarang.Visible = True
        Else
            cbmasterbarang.Visible = False
            clbmasterbarang.Visible = False
        End If

        If flogin.xmaster_kategori > 0 Then
            cbmasterkategori.Visible = True
            clbmasterkategori.Visible = True
        Else
            cbmasterkategori.Visible = False
            clbmasterkategori.Visible = False
        End If

        If flogin.xmaster_gudang > 0 Then
            cbmastergudang.Visible = True
            clbmastergudang.Visible = True
        Else
            cbmastergudang.Visible = False
            clbmastergudang.Visible = False
        End If

        If flogin.xmaster_pelanggan > 0 Then
            cbmasterpelanggan.Visible = True
            clbmasterpelanggan.Visible = True
        Else
            cbmasterpelanggan.Visible = False
            clbmasterpelanggan.Visible = False
        End If

        If flogin.xmaster_supplier > 0 Then
            cbmastersupplier.Visible = True
            clbmastersupplier.Visible = True
        Else
            cbmastersupplier.Visible = False
            clbmastersupplier.Visible = False
        End If

        If flogin.xmaster_user > 0 Then
            cbmasteruser.Visible = True
            clbmasteruser.Visible = True
        Else
            cbmasteruser.Visible = False
            clbmasteruser.Visible = False
        End If

        If flogin.xmaster_kas > 0 Then
            cbmasterkas.Visible = True
            clbmasterkas.Visible = True
        Else
            cbmasterkas.Visible = False
            clbmasterkas.Visible = False
        End If

        If flogin.xmaster_pricelist > 0 Then
            cbmasterpricelist.Visible = True
            clbmasterpricelist.Visible = True
        Else
            cbmasterpricelist.Visible = False
            clbmasterpricelist.Visible = False
        End If

        If flogin.xmaster_rek_supplier > 0 Then
            cbmasterreksupp.Visible = True
            clbmasterreksupp.Visible = True
        Else
            cbmasterreksupp.Visible = False
            clbmasterreksupp.Visible = False
        End If

        If flogin.xmaster_rek_pelanggan > 0 Then
            cbmasterrekplng.Visible = True
            clbmasterrekplng.Visible = True
        Else
            cbmasterrekplng.Visible = False
            clbmasterrekplng.Visible = False
        End If

        'transaksi

        If flogin.xpembelian > 0 Then
            cbpembelian.Visible = True
            clbpembelian.Visible = True
        Else
            cbpembelian.Visible = False
            clbpembelian.Visible = False
        End If

        If flogin.xpenjualan > 0 Then
            cbpenjualan.Visible = True
            clbpenjualan.Visible = True
        Else
            cbpenjualan.Visible = False
            clbpenjualan.Visible = False
        End If

        If flogin.xretur_beli > 0 Then
            cbreturbeli.Visible = True
            clbreturbeli.Visible = True
        Else
            cbreturbeli.Visible = False
            clbreturbeli.Visible = False
        End If

        If flogin.xretur_jual > 0 Then
            cbreturjual.Visible = True
            clbreturjual.Visible = True
        Else
            cbreturjual.Visible = False
            clbreturjual.Visible = False
        End If

        If flogin.xbarang_masuk > 0 Then
            cbbarangmasuk.Visible = True
            clbbarangmasuk.Visible = True
        Else
            cbbarangmasuk.Visible = False
            clbbarangmasuk.Visible = False
        End If

        If flogin.xbarang_keluar > 0 Then
            cbbarangkeluar.Visible = True
            clbbarangkeluar.Visible = True
        Else
            cbbarangkeluar.Visible = False
            clbbarangkeluar.Visible = False
        End If

        If flogin.xtransfer_barang > 0 Then
            cbtransferbarang.Visible = True
            clbtransferbarang.Visible = True
        Else
            cbtransferbarang.Visible = False
            clbtransferbarang.Visible = False
        End If

        If flogin.xpenyesuaian_stok > 0 Then
            cbpenyesuaianstok.Visible = True
            clbpenyesuaianstok.Visible = True
        Else
            cbpenyesuaianstok.Visible = False
            clbpenyesuaianstok.Visible = False
        End If

        'administrasi

        If flogin.xlunas_utang > 0 Then
            cblunasutang.Visible = True
            clblunasutang.Visible = True
        Else
            cblunasutang.Visible = False
            clblunasutang.Visible = False
        End If

        If flogin.xlunas_piutang > 0 Then
            cblunaspiutang.Visible = True
            clblunaspiutang.Visible = True
        Else
            cblunaspiutang.Visible = False
            clblunaspiutang.Visible = False
        End If

        If flogin.xakun_masuk > 0 Then
            cbakunmasuk.Visible = True
            clbakunmasuk.Visible = True
        Else
            cbakunmasuk.Visible = False
            clbakunmasuk.Visible = False
        End If

        If flogin.xakun_keluar > 0 Then
            cbakunkeluar.Visible = True
            clbakunkeluar.Visible = True
        Else
            cbakunkeluar.Visible = False
            clbakunkeluar.Visible = False
        End If

        If flogin.xtransfer_kas > 0 Then
            cbtransferkas.Visible = True
            clbtransferkas.Visible = True
        Else
            cbtransferkas.Visible = False
            clbtransferkas.Visible = False
        End If

        'laporan

        If flogin.xlap_pricelist > 0 Then
            cblappricelist.Visible = True
            clblappricelist.Visible = True
        Else
            cblappricelist.Visible = False
            clblappricelist.Visible = False
        End If

        If flogin.xlap_pembelian > 0 Then
            cblappembelian.Visible = True
            clblappembelian.Visible = True
        Else
            cblappembelian.Visible = False
            clblappembelian.Visible = False
        End If

        If flogin.xlap_penjualan > 0 Then
            cblappenjualan.Visible = True
            clblappenjualan.Visible = True
        Else
            cblappenjualan.Visible = False
            clblappenjualan.Visible = False
        End If

        If flogin.xlap_penjualan_pajak > 0 Then
            cblappenjualanpajak.Visible = True
            clblappenjualanpajak.Visible = True
        Else
            cblappenjualanpajak.Visible = False
            clblappenjualanpajak.Visible = False
        End If

        '===

        If flogin.xlap_returbeli > 0 Then
            cblapreturbeli.Visible = True
            clblapreturbeli.Visible = True
        Else
            cblapreturbeli.Visible = False
            clblapreturbeli.Visible = False
        End If

        If flogin.xlap_returjual > 0 Then
            cblapreturjual.Visible = True
            clblapreturjual.Visible = True
        Else
            cblapreturjual.Visible = False
            clblapreturjual.Visible = False
        End If

        If flogin.xlap_barangmasuk > 0 Then
            cblapbarangmasuk.Visible = True
            clblapbarangmasuk.Visible = True
        Else
            cblapbarangmasuk.Visible = False
            clblapbarangmasuk.Visible = False
        End If

        If flogin.xlap_barangkeluar > 0 Then
            cblapbarangkeluar.Visible = True
            clblapbarangkeluar.Visible = True
        Else
            cblapbarangkeluar.Visible = False
            clblapbarangkeluar.Visible = False
        End If

        '===

        If flogin.xlap_transfer_barang > 0 Then
            cblaptransferbarang.Visible = True
            clblaptransferbarang.Visible = True
        Else
            cblaptransferbarang.Visible = False
            clblaptransferbarang.Visible = False
        End If

        If flogin.xlap_stok_barang > 0 Then
            cblapstokbarang.Visible = True
            clblapstokbarang.Visible = True
        Else
            cblapstokbarang.Visible = False
            clblapstokbarang.Visible = False
        End If

        If flogin.xlap_lunas_utang > 0 Then
            cblaputang.Visible = True
            clblaputang.Visible = True
        Else
            cblaputang.Visible = False
            clblaputang.Visible = False
        End If

        If flogin.xlap_lunas_piutang > 0 Then
            cblappiutang.Visible = True
            clblappiutang.Visible = True
        Else
            cblappiutang.Visible = False
            clblappiutang.Visible = False
        End If

        '===

        If flogin.xlap_akun_masuk > 0 Then
            cblapakunmasuk.Visible = True
            clblapakunmasuk.Visible = True
        Else
            cblapakunmasuk.Visible = False
            clblapakunmasuk.Visible = False
        End If

        If flogin.xlap_akun_keluar > 0 Then
            cblapakunkeluar.Visible = True
            clblapakunkeluar.Visible = True
        Else
            cblapakunkeluar.Visible = False
            clblapakunkeluar.Visible = False
        End If

        If flogin.xlap_transfer_kas > 0 Then
            cblaptransferkas.Visible = True
            clblaptransferkas.Visible = True
        Else
            cblaptransferkas.Visible = False
            clblaptransferkas.Visible = False
        End If

        If flogin.xlap_transaksi_kas > 0 Then
            cblaptransaksikas.Visible = True
            clblaptransaksikas.Visible = True
        Else
            cblaptransaksikas.Visible = False
            clblaptransaksikas.Visible = False
        End If

        '===

        If flogin.xlap_modal_barang > 0 Then
            cblapmodalbarang.Visible = True
            clblapmodalbarang.Visible = True
        Else
            cblapmodalbarang.Visible = False
            clblapmodalbarang.Visible = False
        End If

        If flogin.xlap_mutasi_barang > 0 Then
            cblapmutasibarang.Visible = True
            clblapmutasibarang.Visible = True
        Else
            cblapmutasibarang.Visible = False
            clblapmutasibarang.Visible = False
        End If

        If flogin.xlap_penyesuaian_stok > 0 Then
            cblappenyesuaianstok.Visible = True
            clblappenyesuaianstok.Visible = True
        Else
            cblappenyesuaianstok.Visible = False
            clblappenyesuaianstok.Visible = False
        End If

        If flogin.xlap_laba_rugi > 0 Then
            cblaplabarugi.Visible = True
            clblaplabarugi.Visible = True
        Else
            cblaplabarugi.Visible = False
            clblaplabarugi.Visible = False
        End If

        If flogin.xlap_rekapan_harian > 0 Then
            cblaprekapanharian.Visible = True
            clblaprekapanharian.Visible = True
        Else
            cblaprekapanharian.Visible = False
            clblaprekapanharian.Visible = False
        End If

        'chart

        If flogin.xchart_pembelian > 0 Then
            cbchartpembelian.Visible = True
            clbchartpembelian.Visible = True
        Else
            cbchartpembelian.Visible = False
            clbchartpembelian.Visible = False
        End If

        If flogin.xchart_penjualan > 0 Then
            cbchartpenjualan.Visible = True
            clbchartpenjualan.Visible = True
        Else
            cbchartpenjualan.Visible = False
            clbchartpenjualan.Visible = False
        End If

        If flogin.xchart_lunas_utang > 0 Then
            cbchartlunasutang.Visible = True
            clbchartlunasutang.Visible = True
        Else
            cbchartlunasutang.Visible = False
            clbchartlunasutang.Visible = False
        End If

        If flogin.xchart_lunas_piutang > 0 Then
            cbchartlunaspiutang.Visible = True
            clbchartlunaspiutang.Visible = True
        Else
            cbchartlunaspiutang.Visible = False
            clbchartlunaspiutang.Visible = False
        End If

        If flogin.xchart_kas_masuk > 0 Then
            cbchartakunmasuk.Visible = True
            clbchartakunmasuk.Visible = True
        Else
            cbchartakunmasuk.Visible = False
            clbchartakunmasuk.Visible = False
        End If

        If flogin.xchart_kas_keluar > 0 Then
            cbchartakunkeluar.Visible = True
            clbchartakunkeluar.Visible = True
        Else
            cbchartakunkeluar.Visible = False
            clbchartakunkeluar.Visible = False
        End If

        '===

        If flogin.xfeature_kalkulasi > 0 Then
            cbkalkulasiexpedisi.Visible = True
        Else
            cbkalkulasiexpedisi.Visible = False
        End If

        If flogin.xfeature_barcode > 0 Then
            cbbarcodegenerator.Visible = True
        Else
            cbbarcodegenerator.Visible = False
        End If

        '==========================================================

        'akses user
        'master
        cbmasterbarang.Checked = False
        cbmasterkategori.Checked = False
        cbmastergudang.Checked = False
        cbmasterpelanggan.Checked = False
        cbmastersupplier.Checked = False
        cbmasteruser.Checked = False
        cbmasterkas.Checked = False
        cbmasterpricelist.Checked = False
        cbmasterreksupp.Checked = False
        cbmasterrekplng.Checked = False

        'transaksi
        cbpembelian.Checked = False
        cbpenjualan.Checked = False
        cbreturbeli.Checked = False
        cbreturjual.Checked = False
        cbbarangmasuk.Checked = False
        cbbarangkeluar.Checked = False
        cbtransferbarang.Checked = False
        cbpenyesuaianstok.Checked = False

        'administrasi
        cblunasutang.Checked = False
        cblunaspiutang.Checked = False
        cbtransferkas.Checked = False
        cbakunmasuk.Checked = False
        cbakunkeluar.Checked = False

        'laporan
        cblappricelist.Checked = False
        cblappembelian.Checked = False
        cblappenjualan.Checked = False
        cblappenjualanpajak.Checked = False

        cblapreturbeli.Checked = False
        cblapreturjual.Checked = False
        cblapbarangmasuk.Checked = False
        cblapbarangkeluar.Checked = False

        cblaptransferbarang.Checked = False
        cblapstokbarang.Checked = False
        cblaputang.Checked = False
        cblappiutang.Checked = False

        cblapakunmasuk.Checked = False
        cblapakunkeluar.Checked = False
        cblaptransferkas.Checked = False
        cblaptransaksikas.Checked = False

        cblapmodalbarang.Checked = False
        cblapmutasibarang.Checked = False
        cblappenyesuaianstok.Checked = False
        cblaplabarugi.Checked = False
        cblaprekapanharian.Checked = False

        'chart
        cbchartpembelian.Checked = False
        cbchartpenjualan.Checked = False
        cbchartlunasutang.Checked = False
        cbchartlunaspiutang.Checked = False
        cbchartakunmasuk.Checked = False
        cbchartakunkeluar.Checked = False

        'feature
        cbkalkulasiexpedisi.Checked = False
        cbbarcodegenerator.Checked = False

        'setting
        cbinfoperusahaan.Checked = False
        cbprinter.Checked = False
        cbbackupdatabase.Checked = False
        cbpengaturan.Checked = False



        For id As Integer = 0 To 2
            'master
            clbmasterkategori.SetItemChecked(id, False)
            clbmasterbarang.SetItemChecked(id, False)
            clbmastergudang.SetItemChecked(id, False)
            clbmasterpelanggan.SetItemChecked(id, False)
            clbmastersupplier.SetItemChecked(id, False)
            clbmasteruser.SetItemChecked(id, False)
            clbmasterkas.SetItemChecked(id, False)
            clbmasterpricelist.SetItemChecked(id, False)
            clbmasterreksupp.SetItemChecked(id, False)
            clbmasterrekplng.SetItemChecked(id, False)

            'transaksi
            clbpembelian.SetItemChecked(id, False)
            clbpenjualan.SetItemChecked(id, False)
            clbreturbeli.SetItemChecked(id, False)
            clbreturjual.SetItemChecked(id, False)
            clbbarangmasuk.SetItemChecked(id, False)
            clbbarangkeluar.SetItemChecked(id, False)
            clbtransferbarang.SetItemChecked(id, False)
            clbpenyesuaianstok.SetItemChecked(id, False)

            'administrasi
            clblunasutang.SetItemChecked(id, False)
            clblunaspiutang.SetItemChecked(id, False)
            clbtransferkas.SetItemChecked(id, False)
            clbakunmasuk.SetItemChecked(id, False)
            clbakunkeluar.SetItemChecked(id, False)

            'laporan dan chart
            If id <= 1 Then
                'laporan
                clblappricelist.SetItemChecked(id, False)
                clblappembelian.SetItemChecked(id, False)
                clblappenjualan.SetItemChecked(id, False)
                clblappenjualanpajak.SetItemChecked(id, False)

                clblapreturbeli.SetItemChecked(id, False)
                clblapreturjual.SetItemChecked(id, False)
                clblapbarangmasuk.SetItemChecked(id, False)
                clblapbarangkeluar.SetItemChecked(id, False)

                clblaptransferbarang.SetItemChecked(id, False)
                clblapstokbarang.SetItemChecked(id, False)
                clblaputang.SetItemChecked(id, False)
                clblappiutang.SetItemChecked(id, False)

                clblapakunmasuk.SetItemChecked(id, False)
                clblapakunkeluar.SetItemChecked(id, False)
                clblaptransferkas.SetItemChecked(id, False)
                clblaptransaksikas.SetItemChecked(id, False)

                clblapmodalbarang.SetItemChecked(id, False)
                clblapmutasibarang.SetItemChecked(id, False)
                clblappenyesuaianstok.SetItemChecked(id, False)
                clblaplabarugi.SetItemChecked(id, False)

                clblaprekapanharian.SetItemChecked(id, False)

                'chart
                clbchartpenjualan.SetItemChecked(id, False)
                clbchartpembelian.SetItemChecked(id, False)
                clbchartlunasutang.SetItemChecked(id, False)
                clbchartlunaspiutang.SetItemChecked(id, False)
                clbchartakunmasuk.SetItemChecked(id, False)
                clbchartakunkeluar.SetItemChecked(id, False)

            End If
        Next

        'combo box 
        'master
        cbmasterbarang.Enabled = False
        cbmasterkategori.Enabled = False
        cbmastergudang.Enabled = False
        cbmasterpelanggan.Enabled = False
        cbmastersupplier.Enabled = False
        cbmasteruser.Enabled = False
        cbmasterkas.Enabled = False
        cbmasterpricelist.Enabled = False
        cbmasterreksupp.Enabled = False
        cbmasterrekplng.Enabled = False

        'transaksi
        cbpembelian.Enabled = False
        cbpenjualan.Enabled = False
        cbreturbeli.Enabled = False
        cbreturjual.Enabled = False
        cbbarangmasuk.Enabled = False
        cbbarangkeluar.Enabled = False
        cbtransferbarang.Enabled = False
        cbpenyesuaianstok.Enabled = False

        'administrasi
        cblunasutang.Enabled = False
        cblunaspiutang.Enabled = False
        cbtransferkas.Enabled = False
        cbakunmasuk.Enabled = False
        cbakunkeluar.Enabled = False

        'laporan
        cblappricelist.Enabled = False
        cblappembelian.Enabled = False
        cblappenjualan.Enabled = False
        cblappenjualanpajak.Enabled = False

        cblapreturbeli.Enabled = False
        cblapreturjual.Enabled = False
        cblapbarangmasuk.Enabled = False
        cblapbarangkeluar.Enabled = False

        cblaptransferbarang.Enabled = False
        cblapstokbarang.Enabled = False
        cblaputang.Enabled = False
        cblappiutang.Enabled = False

        cblapakunmasuk.Enabled = False
        cblapakunkeluar.Enabled = False
        cblaptransferkas.Enabled = False
        cblaptransaksikas.Enabled = False

        cblapmodalbarang.Enabled = False
        cblapmutasibarang.Enabled = False
        cblappenyesuaianstok.Enabled = False
        cblaplabarugi.Enabled = False
        cblaprekapanharian.Enabled = False

        'chart
        cbchartpembelian.Enabled = False
        cbchartpenjualan.Enabled = False
        cbchartlunasutang.Enabled = False
        cbchartlunaspiutang.Enabled = False
        cbchartakunmasuk.Enabled = False
        cbchartakunkeluar.Enabled = False

        'feature
        cbkalkulasiexpedisi.Enabled = False
        cbbarcodegenerator.Enabled = False

        'setting
        cbinfoperusahaan.Enabled = False
        cbprinter.Enabled = False
        cbbackupdatabase.Enabled = False
        cbpengaturan.Enabled = False

        '=======================================================================

        'combo box list
        clbmasterbarang.Enabled = False
        clbmasterkategori.Enabled = False
        clbmastergudang.Enabled = False
        clbmasterpelanggan.Enabled = False
        clbmastersupplier.Enabled = False
        clbmasteruser.Enabled = False
        clbmasterkas.Enabled = False
        clbmasterpricelist.Enabled = False
        clbmasterreksupp.Enabled = False
        clbmasterrekplng.Enabled = False

        'transaksi
        clbpembelian.Enabled = False
        clbpenjualan.Enabled = False
        clbreturbeli.Enabled = False
        clbreturjual.Enabled = False
        clbbarangmasuk.Enabled = False
        clbbarangkeluar.Enabled = False
        clbtransferbarang.Enabled = False
        clbpenyesuaianstok.Enabled = False

        'administrasi
        clblunasutang.Enabled = False
        clblunaspiutang.Enabled = False
        clbtransferkas.Enabled = False
        clbakunmasuk.Enabled = False
        clbakunkeluar.Enabled = False

        'laporan
        clblappricelist.Enabled = False
        clblappembelian.Enabled = False
        clblappenjualan.Enabled = False
        clblappenjualanpajak.Enabled = False

        clblapreturbeli.Enabled = False
        clblapreturjual.Enabled = False
        clblapbarangmasuk.Enabled = False
        clblapbarangkeluar.Enabled = False

        clblaptransferbarang.Enabled = False
        clblapstokbarang.Enabled = False
        clblaputang.Enabled = False
        clblappiutang.Enabled = False

        clblapakunmasuk.Enabled = False
        clblapakunkeluar.Enabled = False
        clblaptransferkas.Enabled = False
        clblaptransaksikas.Enabled = False

        clblapmodalbarang.Enabled = False
        clblapmutasibarang.Enabled = False
        clblappenyesuaianstok.Enabled = False
        clblaplabarugi.Enabled = False
        clblaprekapanharian.Enabled = False

        'chart
        clbchartpenjualan.Enabled = False
        clbchartpembelian.Enabled = False
        clbchartlunasutang.Enabled = False
        clbchartlunaspiutang.Enabled = False
        clbchartakunmasuk.Enabled = False
        clbchartakunkeluar.Enabled = False

        txtkode.Enabled = False
        txtnama.Enabled = False
        txtpassword.Enabled = False
        cbauth.Enabled = False

        cmbjabatan.Enabled = False
        txtmaxprint.Enabled = False

        txtemail.Enabled = False
        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl.Enabled = True
        Call isitabel()
    End Sub
    Sub isitabel()
        Call koneksi()
        sql = "SELECT * FROM tb_user ORDER BY nama_user ASC"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)

        GridColumn1.Caption = "Kode User"
        GridColumn1.Width = 40
        GridColumn1.FieldName = "kode_user"

        GridColumn2.Caption = "Nama user"
        GridColumn2.Width = 60
        GridColumn2.FieldName = "nama_user"

        GridColumn3.Caption = "Alamat"
        GridColumn3.Width = 80
        GridColumn3.FieldName = "alamat_user"

        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 40
        GridColumn4.FieldName = "telepon_user"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 70
        GridColumn5.FieldName = "keterangan_user"

        GridColumn6.Caption = "id"
        GridColumn6.Width = 10
        GridColumn6.FieldName = "id"
        GridColumn6.Visible = False
    End Sub
    Sub index()
        txtkode.TabIndex = 1
        txtnama.TabIndex = 2
        txtpassword.TabIndex = 3
        cbauth.TabIndex = 4
        cmbjabatan.TabIndex = 5
        txtmaxprint.TabIndex = 6
        txtemail.TabIndex = 7
        txttelp.TabIndex = 8
        txtalamat.TabIndex = 9
        txtketerangan.TabIndex = 10
    End Sub
    Sub enable_text()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtpassword.Enabled = True
        cbauth.Enabled = True

        cmbjabatan.Enabled = True
        txtmaxprint.Enabled = True

        txtemail.Enabled = True
        txtalamat.Enabled = True
        txttelp.Enabled = True
        txtketerangan.Enabled = True

        'akses user
        'master
        cbmasterbarang.Enabled = True
        cbmasterkategori.Enabled = True
        cbmastergudang.Enabled = True
        cbmasterpelanggan.Enabled = True
        cbmastersupplier.Enabled = True
        cbmasteruser.Enabled = True
        cbmasterkas.Enabled = True
        cbmasterpricelist.Enabled = True
        cbmasterreksupp.Enabled = True
        cbmasterrekplng.Enabled = True

        'transaksi
        cbpembelian.Enabled = True
        cbpenjualan.Enabled = True
        cbreturbeli.Enabled = True
        cbreturjual.Enabled = True
        cbbarangmasuk.Enabled = True
        cbbarangkeluar.Enabled = True
        cbtransferbarang.Enabled = True
        cbpenyesuaianstok.Enabled = True

        'administrasi
        cblunasutang.Enabled = True
        cblunaspiutang.Enabled = True
        cbtransferkas.Enabled = True
        cbakunmasuk.Enabled = True
        cbakunkeluar.Enabled = True

        'laporan
        cblappricelist.Enabled = True
        cblappembelian.Enabled = True
        cblappenjualan.Enabled = True
        cblappenjualanpajak.Enabled = True

        cblapreturbeli.Enabled = True
        cblapreturjual.Enabled = True
        cblapbarangmasuk.Enabled = True
        cblapbarangkeluar.Enabled = True

        cblaptransferbarang.Enabled = True
        cblapstokbarang.Enabled = True
        cblaputang.Enabled = True
        cblappiutang.Enabled = True

        cblapakunmasuk.Enabled = True
        cblapakunkeluar.Enabled = True
        cblaptransferkas.Enabled = True
        cblaptransaksikas.Enabled = True

        cblapmodalbarang.Enabled = True
        cblapmutasibarang.Enabled = True
        cblappenyesuaianstok.Enabled = True
        cblaplabarugi.Enabled = True
        cblaprekapanharian.Enabled = True

        'chart
        cbchartpembelian.Enabled = True
        cbchartpenjualan.Enabled = True
        cbchartlunasutang.Enabled = True
        cbchartlunaspiutang.Enabled = True
        cbchartakunmasuk.Enabled = True
        cbchartakunkeluar.Enabled = True

        'feature
        cbkalkulasiexpedisi.Enabled = True
        cbbarcodegenerator.Enabled = True

        'setting
        cbinfoperusahaan.Enabled = True
        cbprinter.Enabled = True
        cbbackupdatabase.Enabled = True
        cbpengaturan.Enabled = True

        'batas akses user

        txtkode.Focus()
    End Sub

    Sub enable_text_edit()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtpassword.Enabled = True
        cbauth.Enabled = True

        cmbjabatan.Enabled = True
        txtmaxprint.Enabled = True

        txtemail.Enabled = True
        txtalamat.Enabled = True
        txttelp.Enabled = True
        txtketerangan.Enabled = True

        'akses user
        'master
        cbmasterbarang.Enabled = True
        cbmasterkategori.Enabled = True
        cbmastergudang.Enabled = True
        cbmasterpelanggan.Enabled = True
        cbmastersupplier.Enabled = True
        cbmasteruser.Enabled = True
        cbmasterkas.Enabled = True
        cbmasterpricelist.Enabled = True
        cbmasterreksupp.Enabled = True
        cbmasterrekplng.Enabled = True

        'transaksi
        cbpembelian.Enabled = True
        cbpenjualan.Enabled = True
        cbreturbeli.Enabled = True
        cbreturjual.Enabled = True
        cbbarangmasuk.Enabled = True
        cbbarangkeluar.Enabled = True
        cbtransferbarang.Enabled = True
        cbpenyesuaianstok.Enabled = True

        'administrasi
        cblunasutang.Enabled = True
        cblunaspiutang.Enabled = True
        cbtransferkas.Enabled = True
        cbakunmasuk.Enabled = True
        cbakunkeluar.Enabled = True

        'laporan
        cblappricelist.Enabled = True
        cblappembelian.Enabled = True
        cblappenjualan.Enabled = True
        cblappenjualanpajak.Enabled = True

        cblapreturbeli.Enabled = True
        cblapreturjual.Enabled = True
        cblapbarangmasuk.Enabled = True
        cblapbarangkeluar.Enabled = True

        cblaptransferbarang.Enabled = True
        cblapstokbarang.Enabled = True
        cblaputang.Enabled = True
        cblappiutang.Enabled = True

        cblapakunmasuk.Enabled = True
        cblapakunkeluar.Enabled = True
        cblaptransferkas.Enabled = True
        cblaptransaksikas.Enabled = True

        cblapmodalbarang.Enabled = True
        cblapmutasibarang.Enabled = True
        cblappenyesuaianstok.Enabled = True
        cblaplabarugi.Enabled = True

        'chart
        cbchartpembelian.Enabled = True
        cbchartpenjualan.Enabled = True
        cbchartlunasutang.Enabled = True
        cbchartlunaspiutang.Enabled = True
        cbchartakunmasuk.Enabled = True
        cbchartakunkeluar.Enabled = True

        'feature
        cbkalkulasiexpedisi.Enabled = True
        cbbarcodegenerator.Enabled = True

        'setting
        cbinfoperusahaan.Enabled = True
        cbprinter.Enabled = True
        cbbackupdatabase.Enabled = True
        cbpengaturan.Enabled = True

        cblaprekapanharian.Enabled = True

        'combo box list
        'master
        If aksesbarang > 0 Then
            clbmasterbarang.Enabled = True
        Else
            clbmasterbarang.Enabled = False
        End If

        If akseskategori > 0 Then
            clbmasterkategori.Enabled = True
        Else
            clbmasterkategori.Enabled = False
        End If

        If aksesgudang > 0 Then
            clbmastergudang.Enabled = True
        Else
            clbmastergudang.Enabled = False
        End If

        If aksespelanggan > 0 Then
            clbmasterpelanggan.Enabled = True
        Else
            clbmasterpelanggan.Enabled = False
        End If

        If aksessupplier > 0 Then
            clbmastersupplier.Enabled = True
        Else
            clbmastersupplier.Enabled = False
        End If

        If aksesuser > 0 Then
            clbmasteruser.Enabled = True
        Else
            clbmasteruser.Enabled = False
        End If

        If akseskas > 0 Then
            clbmasterkas.Enabled = True
        Else
            clbmasterkas.Enabled = False
        End If

        If aksespricelist > 0 Then
            clbmasterpricelist.Enabled = True
        Else
            clbmasterpricelist.Enabled = False
        End If

        If aksesreksupp > 0 Then
            clbmasterreksupp.Enabled = True
        Else
            clbmasterreksupp.Enabled = False
        End If

        If aksesrekplng > 0 Then
            clbmasterrekplng.Enabled = True
        Else
            clbmasterrekplng.Enabled = False
        End If

        'transaksi
        If aksespembelian > 0 Then
            clbpembelian.Enabled = True
        Else
            clbpembelian.Enabled = False
        End If

        If aksespenjualan > 0 Then
            clbpenjualan.Enabled = True
        Else
            clbpenjualan.Enabled = False
        End If

        If aksesreturbeli > 0 Then
            clbreturbeli.Enabled = True
        Else
            clbreturbeli.Enabled = False
        End If

        If aksesreturjual > 0 Then
            clbreturjual.Enabled = True
        Else
            clbreturjual.Enabled = False
        End If

        If aksesbarangmasuk > 0 Then
            clbbarangmasuk.Enabled = True
        Else
            clbbarangmasuk.Enabled = False
        End If

        If aksesbarangkeluar > 0 Then
            clbbarangkeluar.Enabled = True
        Else
            clbbarangkeluar.Enabled = False
        End If

        If aksestransferbarang > 0 Then
            clbtransferbarang.Enabled = True
        Else
            clbtransferbarang.Enabled = False
        End If

        If aksespenyesuaianstok > 0 Then
            clbpenyesuaianstok.Enabled = True
        Else
            clbpenyesuaianstok.Enabled = False
        End If

        'administrasi
        If akseslunasutang > 0 Then
            clblunasutang.Enabled = True
        Else
            clblunasutang.Enabled = False
        End If

        If akseslunaspiutang > 0 Then
            clblunaspiutang.Enabled = True
        Else
            clblunaspiutang.Enabled = False
        End If

        If aksesakunmasuk > 0 Then
            clbakunmasuk.Enabled = True
        Else
            clbakunmasuk.Enabled = False
        End If

        If aksesakunkeluar > 0 Then
            clbakunkeluar.Enabled = True
        Else
            clbakunkeluar.Enabled = False
        End If

        If aksestransferkas > 0 Then
            clbtransferkas.Enabled = True
        Else
            clbtransferkas.Enabled = False
        End If

        'laporan
        If akseslappricelist > 0 Then
            clblappricelist.Enabled = True
        Else
            clblappricelist.Enabled = False
        End If

        If akseslappembelian > 0 Then
            clblappembelian.Enabled = True
        Else
            clblappembelian.Enabled = False
        End If

        If akseslappenjualan > 0 Then
            clblappenjualan.Enabled = True
        Else
            clblappenjualan.Enabled = False
        End If

        If akseslappenjualanpajak > 0 Then
            clblappenjualanpajak.Enabled = True
        Else
            clblappenjualanpajak.Enabled = False
        End If

        '======================================


        If akseslapreturbeli > 0 Then
            clblapreturbeli.Enabled = True
        Else
            clblapreturbeli.Enabled = False
        End If

        If akseslapreturjual > 0 Then
            clblapreturjual.Enabled = True
        Else
            clblapreturjual.Enabled = False
        End If

        If akseslapbarangmasuk > 0 Then
            clblapbarangmasuk.Enabled = True
        Else
            clblapbarangmasuk.Enabled = False
        End If

        If akseslapbarangkeluar > 0 Then
            clblapbarangkeluar.Enabled = True
        Else
            clblapbarangkeluar.Enabled = False
        End If

        '=======================================

        If akseslaptransferbarang > 0 Then
            clblaptransferbarang.Enabled = True
        Else
            clblaptransferbarang.Enabled = False
        End If

        If akseslapstokbarang > 0 Then
            clblapstokbarang.Enabled = True
        Else
            clblapstokbarang.Enabled = False
        End If

        If akseslaputang > 0 Then
            clblaputang.Enabled = True
        Else
            clblaputang.Enabled = False
        End If

        If akseslappiutang > 0 Then
            clblappiutang.Enabled = True
        Else
            clblappiutang.Enabled = False
        End If

        '========================================

        If akseslapakunmasuk > 0 Then
            clblapakunmasuk.Enabled = True
        Else
            clblapakunmasuk.Enabled = False
        End If

        If akseslapakunkeluar > 0 Then
            clblapakunkeluar.Enabled = True
        Else
            clblapakunkeluar.Enabled = False
        End If

        If akseslaptransferkas > 0 Then
            clblaptransferkas.Enabled = True
        Else
            clblaptransferkas.Enabled = False
        End If

        If akseslaptransaksikas > 0 Then
            clblaptransaksikas.Enabled = True
        Else
            clblaptransaksikas.Enabled = False
        End If

        '==========================================

        If akseslapmodalbarang > 0 Then
            clblapmodalbarang.Enabled = True
        Else
            clblapmodalbarang.Enabled = False
        End If

        If akseslapmutasibarang > 0 Then
            clblapmutasibarang.Enabled = True
        Else
            clblapmutasibarang.Enabled = False
        End If

        If akseslappenyesuaianstok > 0 Then
            clblappenyesuaianstok.Enabled = True
        Else
            clblappenyesuaianstok.Enabled = False
        End If

        If akseslaplabarugi > 0 Then
            clblaplabarugi.Enabled = True
        Else
            clblaplabarugi.Enabled = False
        End If

        If akseslaprekapanharian > 0 Then
            clblaprekapanharian.Enabled = True
        Else
            clblaprekapanharian.Enabled = False
        End If
        '==========================================

        If akseschartpembelian > 0 Then
            clbchartpembelian.Enabled = True
        Else
            clbchartpembelian.Enabled = False
        End If

        If akseschartpenjualan > 0 Then
            clbchartpenjualan.Enabled = True
        Else
            clbchartpenjualan.Enabled = False
        End If

        If akseschartpelunasanutang > 0 Then
            clbchartlunasutang.Enabled = True
        Else
            clbchartlunasutang.Enabled = False
        End If

        If akseschartpelunasanpiutang > 0 Then
            clbchartlunaspiutang.Enabled = True
        Else
            clbchartlunaspiutang.Enabled = False
        End If

        If akseschartakunmasuk > 0 Then
            clbchartakunmasuk.Enabled = True
        Else
            clbchartakunmasuk.Enabled = False
        End If

        If akseschartakunkeluar > 0 Then
            clbchartakunkeluar.Enabled = True
        Else
            clbchartakunkeluar.Enabled = False
        End If

        '==========================================

        'batas akses user

        txtkode.Focus()
    End Sub

    Private Sub txtmaxprint_TextChanged(sender As Object, e As EventArgs) Handles txtmaxprint.TextChanged
        If txtmaxprint.Text = "" Or txtmaxprint.Text = "0" Then
            txtmaxprint.Text = 1
        Else
            maxprint = txtmaxprint.Text
            txtmaxprint.Text = Format(maxprint, "##,##0")
            txtmaxprint.SelectionStart = Len(txtmaxprint.Text)
        End If
    End Sub

    Private Sub txtmaxprint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmaxprint.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
                Call enable_text()
                Call index()
                GridControl.Enabled = False
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtpassword.Text.Length = 0 Then
                            MsgBox("Password belum terisi !")
                        Else
                            If cmbjabatan.Text.Length = 0 Then
                                MsgBox("Jabatan belum terisi !")
                            Else
                                If txtemail.Text.Length = 0 Then
                                    MsgBox("Email belum terisi !")
                                Else
                                    If txttelp.Text.Length = 0 Then
                                        MsgBox("Telepon belum terisi !")
                                    Else
                                        If txtalamat.Text.Length = 0 Then
                                            MsgBox("Alamat belum terisi !")
                                        Else
                                            Call simpan()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub aksesadmin()
        cekauthuser = 0
        'master
        cekmasterbarang = 0
        cekmasterkategori = 0
        cekmastergudang = 0
        cekmasterpelanggan = 0
        cekmastersupplier = 0
        cekmasteruser = 0
        cekmasterkas = 0
        cekmasterpricelist = 0
        cekmasterreksupp = 0
        cekmasterrekplng = 0

        'transaksi
        cekpembelian = 0
        cekpenjualan = 0
        cekreturbeli = 0
        cekreturjual = 0
        cekbarangmasuk = 0
        cekbarangkeluar = 0
        cektransferbarang = 0
        cekpenyesuaianstok = 0

        'administrasi
        ceklunasutang = 0
        ceklunaspiutang = 0
        cektransferkas = 0
        cekakunmasuk = 0
        cekakunkeluar = 0

        'laporan
        ceklappricelist = 0
        ceklappembelian = 0
        ceklappenjualan = 0
        ceklappenjualanpajak = 0

        ceklapreturbeli = 0
        ceklapreturjual = 0
        ceklapbarangmasuk = 0
        ceklapbarangkeluar = 0

        ceklaptransferbarang = 0
        ceklapstokbarang = 0
        ceklaputang = 0
        ceklappiutang = 0

        ceklapakunmasuk = 0
        ceklapakunkeluar = 0
        ceklaptransferkas = 0
        ceklaptransaksikas = 0

        ceklapmodalbarang = 0
        ceklapmutasibarang = 0
        ceklappenyesuaianstok = 0
        ceklaplabarugi = 0
        ceklaprekapanharian = 0

        'chart
        cekchartpembelian = 0
        cekchartpenjualan = 0
        cekchartpelunasanutang = 0
        cekchartpelunasanpiutang = 0
        cekchartakunmasuk = 0
        cekchartakunkeluar = 0

        'feature
        cekfeaturekalkulasi = 0
        cekfeaturebarcode = 0

        'setting
        ceksettinginfoperusahaan = 0
        ceksettingprinter = 0
        ceksettingbackupdatabase = 0
        ceksettingpengaturan = 0

        'We will run through each indice

        If cbauth.Checked = True Then
            cekauthuser = 1
        Else
            cekauthuser = 0
        End If

        For i = 0 To 2
            'master =======================================================================
            'barang
            If clbmasterbarang.GetItemChecked(i) Then
                If clbmasterbarang.Items(i).Equals("Tambah") Then
                    cekmasterbarang = cekmasterbarang + 1
                ElseIf clbmasterbarang.Items(i).Equals("Edit") Then
                    cekmasterbarang = cekmasterbarang + 3
                ElseIf clbmasterbarang.Items(i).Equals("Hapus") Then
                    cekmasterbarang = cekmasterbarang + 5
                End If
            Else
                cekmasterbarang = cekmasterbarang + 0
            End If
            'kategori
            If clbmasterkategori.GetItemChecked(i) Then
                If clbmasterkategori.Items(i).Equals("Tambah") Then
                    cekmasterkategori = cekmasterkategori + 1
                ElseIf clbmasterkategori.Items(i).Equals("Edit") Then
                    cekmasterkategori = cekmasterkategori + 3
                ElseIf clbmasterkategori.Items(i).Equals("Hapus") Then
                    cekmasterkategori = cekmasterkategori + 5
                End If
            Else
                cekmasterkategori = cekmasterkategori + 0
            End If
            'gudang
            If clbmastergudang.GetItemChecked(i) Then
                If clbmastergudang.Items(i).Equals("Tambah") Then
                    cekmastergudang = cekmastergudang + 1
                ElseIf clbmastergudang.Items(i).Equals("Edit") Then
                    cekmastergudang = cekmastergudang + 3
                ElseIf clbmastergudang.Items(i).Equals("Hapus") Then
                    cekmastergudang = cekmastergudang + 5
                End If
            Else
                cekmastergudang = cekmastergudang + 0
            End If
            'pelanggan
            If clbmasterpelanggan.GetItemChecked(i) Then
                If clbmasterpelanggan.Items(i).Equals("Tambah") Then
                    cekmasterpelanggan = cekmasterpelanggan + 1
                ElseIf clbmasterpelanggan.Items(i).Equals("Edit") Then
                    cekmasterpelanggan = cekmasterpelanggan + 3
                ElseIf clbmasterpelanggan.Items(i).Equals("Hapus") Then
                    cekmasterpelanggan = cekmasterpelanggan + 5
                End If
            Else
                cekmasterpelanggan = cekmasterpelanggan + 0
            End If
            'supplier
            If clbmastersupplier.GetItemChecked(i) Then
                If clbmastersupplier.Items(i).Equals("Tambah") Then
                    cekmastersupplier = cekmastersupplier + 1
                ElseIf clbmastersupplier.Items(i).Equals("Edit") Then
                    cekmastersupplier = cekmastersupplier + 3
                ElseIf clbmastersupplier.Items(i).Equals("Hapus") Then
                    cekmastersupplier = cekmastersupplier + 5
                End If
            Else
                cekmastersupplier = cekmastersupplier + 0
            End If
            'user
            If clbmasteruser.GetItemChecked(i) Then
                If clbmasteruser.Items(i).Equals("Tambah") Then
                    cekmasteruser = cekmasteruser + 1
                ElseIf clbmasteruser.Items(i).Equals("Edit") Then
                    cekmasteruser = cekmasteruser + 3
                ElseIf clbmasteruser.Items(i).Equals("Hapus") Then
                    cekmasteruser = cekmasteruser + 5
                End If
            Else
                cekmasteruser = cekmasteruser + 0
            End If
            'kas
            If clbmasterkas.GetItemChecked(i) Then
                If clbmasterkas.Items(i).Equals("Tambah") Then
                    cekmasterkas = cekmasterkas + 1
                ElseIf clbmasterkas.Items(i).Equals("Edit") Then
                    cekmasterkas = cekmasterkas + 3
                ElseIf clbmasterkas.Items(i).Equals("Hapus") Then
                    cekmasterkas = cekmasterkas + 5
                End If
            Else
                cekmasterkas = cekmasterkas + 0
            End If
            'pricelist
            If clbmasterpricelist.GetItemChecked(i) Then
                If clbmasterpricelist.Items(i).Equals("Tambah") Then
                    cekmasterpricelist = cekmasterpricelist + 1
                ElseIf clbmasterpricelist.Items(i).Equals("Edit") Then
                    cekmasterpricelist = cekmasterpricelist + 3
                ElseIf clbmasterpricelist.Items(i).Equals("Hapus") Then
                    cekmasterpricelist = cekmasterpricelist + 5
                End If
            Else
                cekmasterpricelist = cekmasterpricelist + 0
            End If
            'rekening supp
            If clbmasterreksupp.GetItemChecked(i) Then
                If clbmasterreksupp.Items(i).Equals("Tambah") Then
                    cekmasterreksupp = cekmasterreksupp + 1
                ElseIf clbmasterreksupp.Items(i).Equals("Edit") Then
                    cekmasterreksupp = cekmasterreksupp + 3
                ElseIf clbmasterreksupp.Items(i).Equals("Hapus") Then
                    cekmasterreksupp = cekmasterreksupp + 5
                End If
            Else
                cekmasterreksupp = cekmasterreksupp + 0
            End If
            'rekening cust
            If clbmasterrekplng.GetItemChecked(i) Then
                If clbmasterrekplng.Items(i).Equals("Tambah") Then
                    cekmasterrekplng = cekmasterrekplng + 1
                ElseIf clbmasterrekplng.Items(i).Equals("Edit") Then
                    cekmasterrekplng = cekmasterrekplng + 3
                ElseIf clbmasterrekplng.Items(i).Equals("Hapus") Then
                    cekmasterrekplng = cekmasterrekplng + 5
                End If
            Else
                cekmasterrekplng = cekmasterrekplng + 0
            End If

            'transaksi ===================================================================
            'pembelian
            If clbpembelian.GetItemChecked(i) Then
                If clbpembelian.Items(i).Equals("Tambah") Then
                    cekpembelian = cekpembelian + 1
                ElseIf clbpembelian.Items(i).Equals("Edit") Then
                    cekpembelian = cekpembelian + 3
                ElseIf clbpembelian.Items(i).Equals("Print") Then
                    cekpembelian = cekpembelian + 5
                End If
            Else
                cekpembelian = cekpembelian + 0
            End If

            'penjualan
            If clbpenjualan.GetItemChecked(i) Then
                If clbpenjualan.Items(i).Equals("Tambah") Then
                    cekpenjualan = cekpenjualan + 1
                ElseIf clbpenjualan.Items(i).Equals("Edit") Then
                    cekpenjualan = cekpenjualan + 3
                ElseIf clbpenjualan.Items(i).Equals("Print") Then
                    cekpenjualan = cekpenjualan + 5
                End If
            Else
                cekpenjualan = cekpenjualan + 0
            End If

            'returbeli
            If clbreturbeli.GetItemChecked(i) Then
                If clbreturbeli.Items(i).Equals("Tambah") Then
                    cekreturbeli = cekreturbeli + 1
                ElseIf clbreturbeli.Items(i).Equals("Edit") Then
                    cekreturbeli = cekreturbeli + 3
                ElseIf clbreturbeli.Items(i).Equals("Print") Then
                    cekreturbeli = cekreturbeli + 5
                End If
            Else
                cekreturbeli = cekreturbeli + 0
            End If

            'returjual
            If clbreturjual.GetItemChecked(i) Then
                If clbreturjual.Items(i).Equals("Tambah") Then
                    cekreturjual = cekreturjual + 1
                ElseIf clbreturjual.Items(i).Equals("Edit") Then
                    cekreturjual = cekreturjual + 3
                ElseIf clbreturjual.Items(i).Equals("Print") Then
                    cekreturjual = cekreturjual + 5
                End If
            Else
                cekreturjual = cekreturjual + 0
            End If

            'barangmasuk
            If clbbarangmasuk.GetItemChecked(i) Then
                If clbbarangmasuk.Items(i).Equals("Tambah") Then
                    cekbarangmasuk = cekbarangmasuk + 1
                ElseIf clbbarangmasuk.Items(i).Equals("Edit") Then
                    cekbarangmasuk = cekbarangmasuk + 3
                ElseIf clbbarangmasuk.Items(i).Equals("Print") Then
                    cekbarangmasuk = cekbarangmasuk + 5
                End If
            Else
                cekbarangmasuk = cekbarangmasuk + 0
            End If

            'barangkeluar
            If clbbarangkeluar.GetItemChecked(i) Then
                If clbbarangkeluar.Items(i).Equals("Tambah") Then
                    cekbarangkeluar = cekbarangkeluar + 1
                ElseIf clbbarangkeluar.Items(i).Equals("Edit") Then
                    cekbarangkeluar = cekbarangkeluar + 3
                ElseIf clbbarangkeluar.Items(i).Equals("Print") Then
                    cekbarangkeluar = cekbarangkeluar + 5
                End If
            Else
                cekbarangkeluar = cekbarangkeluar + 0
            End If

            'transferbarang
            If clbtransferbarang.GetItemChecked(i) Then
                If clbtransferbarang.Items(i).Equals("Tambah") Then
                    cektransferbarang = cektransferbarang + 1
                ElseIf clbtransferbarang.Items(i).Equals("Edit") Then
                    cektransferbarang = cektransferbarang + 3
                ElseIf clbtransferbarang.Items(i).Equals("Print") Then
                    cektransferbarang = cektransferbarang + 5
                End If
            Else
                cektransferbarang = cektransferbarang + 0
            End If

            'penyesuaianstok
            If clbpenyesuaianstok.GetItemChecked(i) Then
                If clbpenyesuaianstok.Items(i).Equals("Tambah") Then
                    cekpenyesuaianstok = cekpenyesuaianstok + 1
                ElseIf clbpenyesuaianstok.Items(i).Equals("Edit") Then
                    cekpenyesuaianstok = cekpenyesuaianstok + 3
                ElseIf clbpenyesuaianstok.Items(i).Equals("Print") Then
                    cekpenyesuaianstok = cekpenyesuaianstok + 5
                End If
            Else
                cekpenyesuaianstok = cekpenyesuaianstok + 0
            End If

            'administrasi ================================================================
            'lunasutang
            If clblunasutang.GetItemChecked(i) Then
                If clblunasutang.Items(i).Equals("Tambah") Then
                    ceklunasutang = ceklunasutang + 1
                ElseIf clblunasutang.Items(i).Equals("Edit") Then
                    ceklunasutang = ceklunasutang + 3
                ElseIf clblunasutang.Items(i).Equals("Print") Then
                    ceklunasutang = ceklunasutang + 5
                End If
            Else
                ceklunasutang = ceklunasutang + 0
            End If

            'lunaspiutang
            If clblunaspiutang.GetItemChecked(i) Then
                If clblunaspiutang.Items(i).Equals("Tambah") Then
                    ceklunaspiutang = ceklunaspiutang + 1
                ElseIf clblunaspiutang.Items(i).Equals("Edit") Then
                    ceklunaspiutang = ceklunaspiutang + 3
                ElseIf clblunaspiutang.Items(i).Equals("Print") Then
                    ceklunaspiutang = ceklunaspiutang + 5
                End If
            Else
                ceklunaspiutang = ceklunaspiutang + 0
            End If

            'transferkas
            If clbtransferkas.GetItemChecked(i) Then
                If clbtransferkas.Items(i).Equals("Tambah") Then
                    cektransferkas = cektransferkas + 1
                ElseIf clbtransferkas.Items(i).Equals("Edit") Then
                    cektransferkas = cektransferkas + 3
                ElseIf clbtransferkas.Items(i).Equals("Print") Then
                    cektransferkas = cektransferkas + 5
                End If
            Else
                cektransferkas = cektransferkas + 0
            End If

            'akunmasuk
            If clbakunmasuk.GetItemChecked(i) Then
                If clbakunmasuk.Items(i).Equals("Tambah") Then
                    cekakunmasuk = cekakunmasuk + 1
                ElseIf clbakunmasuk.Items(i).Equals("Edit") Then
                    cekakunmasuk = cekakunmasuk + 3
                ElseIf clbakunmasuk.Items(i).Equals("Print") Then
                    cekakunmasuk = cekakunmasuk + 5
                End If
            Else
                cekakunmasuk = cekakunmasuk + 0
            End If

            'akunkeluar
            If clbakunkeluar.GetItemChecked(i) Then
                If clbakunkeluar.Items(i).Equals("Tambah") Then
                    cekakunkeluar = cekakunkeluar + 1
                ElseIf clbakunkeluar.Items(i).Equals("Edit") Then
                    cekakunkeluar = cekakunkeluar + 3
                ElseIf clbakunkeluar.Items(i).Equals("Print") Then
                    cekakunkeluar = cekakunkeluar + 5
                End If
            Else
                cekakunkeluar = cekakunkeluar + 0
            End If

            'laporan =====================================================================
            If i <= 1 Then
                'lappricelist
                If clblappricelist.GetItemChecked(i) Then
                    If clblappricelist.Items(i).Equals("Print") Then
                        ceklappricelist = ceklappricelist + 1
                    ElseIf clblappricelist.Items(i).Equals("Export") Then
                        ceklappricelist = ceklappricelist + 3
                    End If
                Else
                    ceklappricelist = ceklappricelist + 0
                End If
                'lappembelian
                If clblappembelian.GetItemChecked(i) Then
                    If clblappembelian.Items(i).Equals("Print") Then
                        ceklappembelian = ceklappembelian + 1
                    ElseIf clblappembelian.Items(i).Equals("Export") Then
                        ceklappembelian = ceklappembelian + 3
                    End If
                Else
                    ceklappembelian = ceklappembelian + 0
                End If
                'lappenjualan
                If clblappenjualan.GetItemChecked(i) Then
                    If clblappenjualan.Items(i).Equals("Print") Then
                        ceklappenjualan = ceklappenjualan + 1
                    ElseIf clblappenjualan.Items(i).Equals("Export") Then
                        ceklappenjualan = ceklappenjualan + 3
                    End If
                Else
                    ceklappenjualan = ceklappenjualan + 0
                End If

                'lappenjualanpajak
                If clblappenjualanpajak.GetItemChecked(i) Then
                    If clblappenjualanpajak.Items(i).Equals("Print") Then
                        ceklappenjualanpajak = ceklappenjualanpajak + 1
                    ElseIf clblappenjualanpajak.Items(i).Equals("Export") Then
                        ceklappenjualanpajak = ceklappenjualanpajak + 3
                    End If
                Else
                    ceklappenjualanpajak = ceklappenjualanpajak + 0
                End If

                '=========================================================================

                'lapreturbeli
                If clblapreturbeli.GetItemChecked(i) Then
                    If clblapreturbeli.Items(i).Equals("Print") Then
                        ceklapreturbeli = ceklapreturbeli + 1
                    ElseIf clblapreturbeli.Items(i).Equals("Export") Then
                        ceklapreturbeli = ceklapreturbeli + 3
                    End If
                Else
                    ceklapreturbeli = ceklapreturbeli + 0
                End If

                'lapreturjual
                If clblapreturjual.GetItemChecked(i) Then
                    If clblapreturjual.Items(i).Equals("Print") Then
                        ceklapreturjual = ceklapreturjual + 1
                    ElseIf clblapreturjual.Items(i).Equals("Export") Then
                        ceklapreturjual = ceklapreturjual + 3
                    End If
                Else
                    ceklapreturjual = ceklapreturjual + 0
                End If

                'lapbarangmasuk
                If clblapbarangmasuk.GetItemChecked(i) Then
                    If clblapbarangmasuk.Items(i).Equals("Print") Then
                        ceklapbarangmasuk = ceklapbarangmasuk + 1
                    ElseIf clblapbarangmasuk.Items(i).Equals("Export") Then
                        ceklapbarangmasuk = ceklapbarangmasuk + 3
                    End If
                Else
                    ceklapbarangmasuk = ceklapbarangmasuk + 0
                End If

                'lapbarangkeluar
                If clblapbarangkeluar.GetItemChecked(i) Then
                    If clblapbarangkeluar.Items(i).Equals("Print") Then
                        ceklapbarangkeluar = ceklapbarangkeluar + 1
                    ElseIf clblapbarangkeluar.Items(i).Equals("Export") Then
                        ceklapbarangkeluar = ceklapbarangkeluar + 3
                    End If
                Else
                    ceklapbarangkeluar = ceklapbarangkeluar + 0
                End If

                '=========================================================================

                'laptransferbarang
                If clblaptransferbarang.GetItemChecked(i) Then
                    If clblaptransferbarang.Items(i).Equals("Print") Then
                        ceklaptransferbarang = ceklaptransferbarang + 1
                    ElseIf clblaptransferbarang.Items(i).Equals("Export") Then
                        ceklaptransferbarang = ceklaptransferbarang + 3
                    End If
                Else
                    ceklaptransferbarang = ceklaptransferbarang + 0
                End If

                'lapstokbarang
                If clblapstokbarang.GetItemChecked(i) Then
                    If clblapstokbarang.Items(i).Equals("Print") Then
                        ceklapstokbarang = ceklapstokbarang + 1
                    ElseIf clblapstokbarang.Items(i).Equals("Export") Then
                        ceklapstokbarang = ceklapstokbarang + 3
                    End If
                Else
                    ceklapstokbarang = ceklapstokbarang + 0
                End If

                'laputang
                If clblaputang.GetItemChecked(i) Then
                    If clblaputang.Items(i).Equals("Print") Then
                        ceklaputang = ceklaputang + 1
                    ElseIf clblaputang.Items(i).Equals("Export") Then
                        ceklaputang = ceklaputang + 3
                    End If
                Else
                    ceklaputang = ceklaputang + 0
                End If

                'lappiutang
                If clblappiutang.GetItemChecked(i) Then
                    If clblappiutang.Items(i).Equals("Print") Then
                        ceklappiutang = ceklappiutang + 1
                    ElseIf clblappiutang.Items(i).Equals("Export") Then
                        ceklappiutang = ceklappiutang + 3
                    End If
                Else
                    ceklappiutang = ceklappiutang + 0
                End If

                '=========================================================================

                'lapakunmasuk
                If clblapakunmasuk.GetItemChecked(i) Then
                    If clblapakunmasuk.Items(i).Equals("Print") Then
                        ceklapakunmasuk = ceklapakunmasuk + 1
                    ElseIf clblapakunmasuk.Items(i).Equals("Export") Then
                        ceklapakunmasuk = ceklapakunmasuk + 3
                    End If
                Else
                    ceklapakunmasuk = ceklapakunmasuk + 0
                End If

                'lapakunkeluar
                If clblapakunkeluar.GetItemChecked(i) Then
                    If clblapakunkeluar.Items(i).Equals("Print") Then
                        ceklapakunkeluar = ceklapakunkeluar + 1
                    ElseIf clblapakunkeluar.Items(i).Equals("Export") Then
                        ceklapakunkeluar = ceklapakunkeluar + 3
                    End If
                Else
                    ceklapakunkeluar = ceklapakunkeluar + 0
                End If

                'laptransferkas
                If clblaptransferkas.GetItemChecked(i) Then
                    If clblaptransferkas.Items(i).Equals("Print") Then
                        ceklaptransferkas = ceklaptransferkas + 1
                    ElseIf clblaptransferkas.Items(i).Equals("Export") Then
                        ceklaptransferkas = ceklaptransferkas + 3
                    End If
                Else
                    ceklaptransferkas = ceklaptransferkas + 0
                End If

                'laptransaksikas
                If clblaptransaksikas.GetItemChecked(i) Then
                    If clblaptransaksikas.Items(i).Equals("Print") Then
                        ceklaptransaksikas = ceklaptransaksikas + 1
                    ElseIf clblaptransaksikas.Items(i).Equals("Export") Then
                        ceklaptransaksikas = ceklaptransaksikas + 3
                    End If
                Else
                    ceklaptransaksikas = ceklaptransaksikas + 0
                End If

                '=========================================================================

                'lapmodalbarang
                If clblapmodalbarang.GetItemChecked(i) Then
                    If clblapmodalbarang.Items(i).Equals("Print") Then
                        ceklapmodalbarang = ceklapmodalbarang + 1
                    ElseIf clblapmodalbarang.Items(i).Equals("Export") Then
                        ceklapmodalbarang = ceklapmodalbarang + 3
                    End If
                Else
                    ceklapmodalbarang = ceklapmodalbarang + 0
                End If

                'lapmutasibarang
                If clblapmutasibarang.GetItemChecked(i) Then
                    If clblapmutasibarang.Items(i).Equals("Print") Then
                        ceklapmutasibarang = ceklapmutasibarang + 1
                    ElseIf clblapmutasibarang.Items(i).Equals("Export") Then
                        ceklapmutasibarang = ceklapmutasibarang + 3
                    End If
                Else
                    ceklapmutasibarang = ceklapmutasibarang + 0
                End If

                'lappenyesuaianstok
                If clblappenyesuaianstok.GetItemChecked(i) Then
                    If clblappenyesuaianstok.Items(i).Equals("Print") Then
                        ceklappenyesuaianstok = ceklappenyesuaianstok + 1
                    ElseIf clblappenyesuaianstok.Items(i).Equals("Export") Then
                        ceklappenyesuaianstok = ceklappenyesuaianstok + 3
                    End If
                Else
                    ceklappenyesuaianstok = ceklappenyesuaianstok + 0
                End If

                'laplabarugi
                If clblaplabarugi.GetItemChecked(i) Then
                    If clblaplabarugi.Items(i).Equals("Print") Then
                        ceklaplabarugi = ceklaplabarugi + 1
                    ElseIf clblaplabarugi.Items(i).Equals("Export") Then
                        ceklaplabarugi = ceklaplabarugi + 3
                    End If
                Else
                    ceklaplabarugi = ceklaplabarugi + 0
                End If

                '=========================================================================

                'laprekapanakhir
                If clblaprekapanharian.GetItemChecked(i) Then
                    If clblaprekapanharian.Items(i).Equals("Print") Then
                        ceklaprekapanharian = ceklaprekapanharian + 1
                    ElseIf clblaprekapanharian.Items(i).Equals("Export") Then
                        ceklaprekapanharian = ceklaprekapanharian + 3
                    End If
                Else
                    ceklaprekapanharian = ceklaprekapanharian + 0
                End If

                '=========================================================================

                'chartpembelian
                If clbchartpembelian.GetItemChecked(i) Then
                    If clbchartpembelian.Items(i).Equals("Print") Then
                        cekchartpembelian = cekchartpembelian + 1
                    ElseIf clbchartpembelian.Items(i).Equals("Export") Then
                        cekchartpembelian = cekchartpembelian + 3
                    End If
                Else
                    cekchartpembelian = cekchartpembelian + 0
                End If

                'chartpenjualan
                If clbchartpenjualan.GetItemChecked(i) Then
                    If clbchartpenjualan.Items(i).Equals("Print") Then
                        cekchartpenjualan = cekchartpenjualan + 1
                    ElseIf clbchartpenjualan.Items(i).Equals("Export") Then
                        cekchartpenjualan = cekchartpenjualan + 3
                    End If
                Else
                    cekchartpenjualan = cekchartpenjualan + 0
                End If

                'chartpelunasanutang
                If clbchartlunasutang.GetItemChecked(i) Then
                    If clbchartlunasutang.Items(i).Equals("Print") Then
                        cekchartpelunasanutang = cekchartpelunasanutang + 1
                    ElseIf clbchartlunasutang.Items(i).Equals("Export") Then
                        cekchartpelunasanutang = cekchartpelunasanutang + 3
                    End If
                Else
                    cekchartpelunasanutang = cekchartpelunasanutang + 0
                End If

                'chartpelunasanpiutang
                If clbchartlunaspiutang.GetItemChecked(i) Then
                    If clbchartlunaspiutang.Items(i).Equals("Print") Then
                        cekchartpelunasanpiutang = cekchartpelunasanpiutang + 1
                    ElseIf clbchartlunaspiutang.Items(i).Equals("Export") Then
                        cekchartpelunasanpiutang = cekchartpelunasanpiutang + 3
                    End If
                Else
                    cekchartpelunasanpiutang = cekchartpelunasanpiutang + 0
                End If

                'chartakunmasuk
                If clbchartakunmasuk.GetItemChecked(i) Then
                    If clbchartakunmasuk.Items(i).Equals("Print") Then
                        cekchartakunmasuk = cekchartakunmasuk + 1
                    ElseIf clbchartakunmasuk.Items(i).Equals("Export") Then
                        cekchartakunmasuk = cekchartakunmasuk + 3
                    End If
                Else
                    cekchartakunmasuk = cekchartakunmasuk + 0
                End If

                'chartakunkeluar
                If clbchartakunkeluar.GetItemChecked(i) Then
                    If clbchartakunkeluar.Items(i).Equals("Print") Then
                        cekchartakunkeluar = cekchartakunkeluar + 1
                    ElseIf clbchartakunkeluar.Items(i).Equals("Export") Then
                        cekchartakunkeluar = cekchartakunkeluar + 3
                    End If
                Else
                    cekchartakunkeluar = cekchartakunkeluar + 0
                End If
            End If
        Next

        '===========================================================================
        If cbkalkulasiexpedisi.Checked = True Then
            cekfeaturekalkulasi = 1
        Else
            cekfeaturekalkulasi = 0
        End If

        If cbbarcodegenerator.Checked = True Then
            cekfeaturebarcode = 1
        Else
            cekfeaturebarcode = 0
        End If
        '============================================================================
        If cbinfoperusahaan.Checked = True Then
            ceksettinginfoperusahaan = 1
        Else
            ceksettinginfoperusahaan = 0
        End If

        If cbprinter.Checked = True Then
            ceksettingprinter = 1
        Else
            ceksettingprinter = 0
        End If
        If cbbackupdatabase.Checked = True Then
            ceksettingbackupdatabase = 1
        Else
            ceksettingbackupdatabase = 0
        End If

        If cbpengaturan.Checked = True Then
            ceksettingpengaturan = 1
        Else
            ceksettingpengaturan = 0
        End If
    End Sub
    Sub simpan()
        Call koneksi()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" & txtkode.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode User Sudah ada dengan nama " & dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            Try
                Call aksesadmin()
                Call koneksi()
                sql = "INSERT INTO tb_user (kode_user, nama_user, password_user, jabatan_user, email_user, telepon_user, alamat_user, keterangan_user, auth_user, max_print,
                    master_barang, master_kategori, master_gudang, master_pelanggan, master_supplier, master_user, master_kas, master_pricelist, master_rek_supp, master_rek_plng, 
                    pembelian, penjualan, retur_beli, retur_jual, barang_masuk, barang_keluar, transfer_barang, penyesuaian_stok,
                    lunas_utang, lunas_piutang, transfer_kas, akun_masuk, akun_keluar, 
                    lap_pricelist, lap_pembelian, lap_penjualan, lap_penjualan_pajak, 
                    lap_returbeli, lap_returjual, lap_barang_masuk, lap_barang_keluar, 
                    lap_transfer_barang, lap_stok_barang, lap_utang, lap_piutang, 
                    lap_akun_masuk, lap_akun_keluar, lap_transfer_kas, lap_transaksi_kas, 
                    lap_modal_barang, lap_mutasi_barang, lap_penyesuaian_stok, lap_laba_rugi, lap_rekapan_harian,
                    chart_pembelian, chart_penjualan, chart_lunas_utang, chart_lunas_piutang, chart_kas_masuk, chart_kas_keluar,
                    feature_kalkulasi_expedisi, feature_barcode_generator,
                    setting_info_perusahaan, setting_printer, setting_backup_database, setting_pengaturan,
                    created_by, updated_by, date_created, last_updated) 
                    VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtpassword.Text & "', '" & cmbjabatan.Text & "', '" & txtemail.Text & "', '" & txttelp.Text & "','" & txtalamat.Text & "','" & txtketerangan.Text & "','" & cekauthuser & "','" & maxprint & "',
                    '" & cekmasterbarang & "','" & cekmasterkategori & "','" & cekmastergudang & "','" & cekmasterpelanggan & "','" & cekmastersupplier & "','" & cekmasteruser & "','" & cekmasterkas & "','" & cekmasterpricelist & "','" & cekmasterreksupp & "','" & cekmasterrekplng & "',
                    '" & cekpembelian & "','" & cekpenjualan & "','" & cekreturbeli & "','" & cekreturjual & "','" & cekbarangmasuk & "','" & cekbarangkeluar & "','" & cektransferbarang & "','" & cekpenyesuaianstok & "',
                    '" & ceklunasutang & "','" & ceklunaspiutang & "','" & cektransferkas & "','" & cekakunmasuk & "','" & cekakunkeluar & "',
                    '" & ceklappricelist & "','" & ceklappembelian & "','" & ceklappenjualan & "','" & ceklappenjualanpajak & "',
                    '" & ceklapreturbeli & "','" & ceklapreturjual & "','" & ceklapbarangmasuk & "','" & ceklapbarangkeluar & "',
                    '" & ceklaptransferbarang & "','" & ceklapstokbarang & "','" & ceklaputang & "','" & ceklappiutang & "',
                    '" & ceklapakunmasuk & "','" & ceklapakunkeluar & "','" & ceklaptransferkas & "','" & ceklaptransaksikas & "',
                    '" & ceklapmodalbarang & "','" & ceklapmutasibarang & "','" & ceklappenyesuaianstok & "','" & ceklaplabarugi & "','" & ceklaprekapanharian & "',
                    '" & cekchartpembelian & "','" & cekchartpenjualan & "','" & cekchartpelunasanutang & "','" & cekchartpelunasanpiutang & "','" & cekchartakunmasuk & "','" & cekchartakunkeluar & "',
                    '" & cekfeaturekalkulasi & "','" & cekfeaturebarcode & "',
                    '" & ceksettinginfoperusahaan & "','" & ceksettingprinter & "','" & ceksettingbackupdatabase & "','" & ceksettingpengaturan & "',
                    '" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Berhasil")
                btntambah.Text = "Tambah"

                Call historysave("Menyimpan Data User kode " & txtkode.Text, txtkode.Text, namaform)

                Me.Refresh()
                Call awal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
            If btnedit.Text = "Edit" Then
                btnedit.Text = "Simpan"
                btnhapus.Enabled = False
                Call enable_text_edit()
                Call index()
                GridControl.Enabled = False
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtpassword.Text.Length = 0 Then
                            MsgBox("Password belum terisi !")
                        Else
                            If cmbjabatan.Text.Length = 0 Then
                                MsgBox("Jabatan belum terisi !")
                            Else
                                If txtemail.Text.Length = 0 Then
                                    MsgBox("Email belum terisi !")
                                Else
                                    If txttelp.Text.Length = 0 Then
                                        MsgBox("Telepon belum terisi !")
                                    Else
                                        If txtalamat.Text.Length = 0 Then
                                            MsgBox("Alamat belum terisi !")
                                        Else
                                            Call edit()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub


    Sub edit()
        If txtkode.Text.Equals(kodeuser) Then
            Call perbaharui()
        Else
            Call koneksi()
            sql = "SELECT * FROM tb_user WHERE kode_user ='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode User Sudah ada dengan nama " & dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
            Else
                Call perbaharui()
            End If
        End If
    End Sub

    Sub perbaharui()
        Try
            Call aksesadmin()
            Call koneksi()
            sql = "UPDATE tb_user SET kode_user=?, nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, auth_user=?, max_print=?, master_barang=?, master_kategori=?, master_gudang=?, master_pelanggan=?, master_supplier=?, master_user=?, master_kas=?, master_pricelist=?, master_rek_supp=?, master_rek_plng=?, 
                pembelian=? ,penjualan=?, retur_beli=?, retur_jual=?, barang_masuk=?, barang_keluar=?, transfer_barang=?, penyesuaian_stok=?,
                lunas_utang=?, lunas_piutang=?, transfer_kas=?, akun_masuk=?, akun_keluar=?, 
                lap_pricelist=?, lap_pembelian=?, lap_penjualan=?, lap_penjualan_pajak=?, lap_returbeli=?, lap_returjual=?, lap_barang_masuk=?, lap_barang_keluar=?, lap_transfer_barang=?, lap_stok_barang=?, lap_utang=?, lap_piutang=?, lap_akun_masuk=?, lap_akun_keluar=?, lap_transfer_kas=?, lap_transaksi_kas=?, lap_modal_barang=?, lap_mutasi_barang=?, lap_penyesuaian_stok=?, lap_laba_rugi=?, lap_rekapan_harian=?,
                chart_pembelian=?, chart_penjualan=?, chart_lunas_utang=?, chart_lunas_piutang=?, chart_kas_masuk=?, chart_kas_keluar=?,
                feature_kalkulasi_expedisi=?, feature_barcode_generator=?,
                setting_info_perusahaan=?, setting_printer=?, setting_backup_database=?, setting_pengaturan=?,
                updated_by=?, last_updated=? WHERE id='" & iduser & "'"

            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_user", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
            cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
            cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
            cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
            cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
            cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
            cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
            cmmd.Parameters.AddWithValue("@auth_user", cekauthuser)
            cmmd.Parameters.AddWithValue("@max_print", maxprint)
            'akses 
            'master
            cmmd.Parameters.AddWithValue("@master_barang", cekmasterbarang)
            cmmd.Parameters.AddWithValue("@master_kategori", cekmasterkategori)
            cmmd.Parameters.AddWithValue("@master_gudang", cekmastergudang)
            cmmd.Parameters.AddWithValue("@master_pelanggan", cekmasterpelanggan)
            cmmd.Parameters.AddWithValue("@master_supplier", cekmastersupplier)
            cmmd.Parameters.AddWithValue("@master_user", cekmasteruser)
            cmmd.Parameters.AddWithValue("@master_kas", cekmasterkas)
            cmmd.Parameters.AddWithValue("@master_pricelist", cekmasterpricelist)
            cmmd.Parameters.AddWithValue("@master_rek_supp", cekmasterreksupp)
            cmmd.Parameters.AddWithValue("@master_rek_plng", cekmasterrekplng)
            'transaksi
            cmmd.Parameters.AddWithValue("@pembelian", cekpembelian)
            cmmd.Parameters.AddWithValue("@penjualan", cekpenjualan)
            cmmd.Parameters.AddWithValue("@retur_beli", cekreturbeli)
            cmmd.Parameters.AddWithValue("@retur_jual", cekreturjual)
            cmmd.Parameters.AddWithValue("@barang_masuk", cekbarangmasuk)
            cmmd.Parameters.AddWithValue("@barang_keluar", cekbarangkeluar)
            cmmd.Parameters.AddWithValue("@transfer_barang", cektransferbarang)
            cmmd.Parameters.AddWithValue("@penyesuaian_stok", cekpenyesuaianstok)
            'administrasi
            cmmd.Parameters.AddWithValue("@lunas_utang", ceklunasutang)
            cmmd.Parameters.AddWithValue("@lunas_piutang", ceklunaspiutang)
            cmmd.Parameters.AddWithValue("@transfer_kas", cektransferkas)
            cmmd.Parameters.AddWithValue("@akun_masuk", cekakunmasuk)
            cmmd.Parameters.AddWithValue("@akun_keluar", cekakunkeluar)
            'laporan
            cmmd.Parameters.AddWithValue("@lap_pricelist", ceklappricelist)
            cmmd.Parameters.AddWithValue("@lap_pembelian", ceklappembelian)
            cmmd.Parameters.AddWithValue("@lap_penjualan", ceklappenjualan)
            cmmd.Parameters.AddWithValue("@lap_penjualan_pajak", ceklappenjualanpajak)

            cmmd.Parameters.AddWithValue("@lap_returbeli", ceklapreturbeli)
            cmmd.Parameters.AddWithValue("@lap_returjual", ceklapreturjual)
            cmmd.Parameters.AddWithValue("@lap_barang_masuk", ceklapbarangmasuk)
            cmmd.Parameters.AddWithValue("@lap_barang_keluar", ceklapbarangkeluar)

            cmmd.Parameters.AddWithValue("@lap_transfer_barang", ceklaptransferbarang)
            cmmd.Parameters.AddWithValue("@lap_stok_barang", ceklapstokbarang)
            cmmd.Parameters.AddWithValue("@lap_utang", ceklaputang)
            cmmd.Parameters.AddWithValue("@lap_piutang", ceklappiutang)

            cmmd.Parameters.AddWithValue("@lap_akun_masuk", ceklapakunmasuk)
            cmmd.Parameters.AddWithValue("@lap_akun_keluar", ceklapakunkeluar)
            cmmd.Parameters.AddWithValue("@lap_transfer_kas", ceklaptransferkas)
            cmmd.Parameters.AddWithValue("@lap_transaksi_kas", ceklaptransaksikas)

            cmmd.Parameters.AddWithValue("@lap_modal_barang", ceklapmodalbarang)
            cmmd.Parameters.AddWithValue("@lap_mutasi_barang", ceklapmutasibarang)
            cmmd.Parameters.AddWithValue("@lap_penyesuaian_stok", ceklappenyesuaianstok)
            cmmd.Parameters.AddWithValue("@lap_laba_rugi", ceklaplabarugi)
            cmmd.Parameters.AddWithValue("@lap_rekapan_harian", ceklaprekapanharian)
            'chart
            cmmd.Parameters.AddWithValue("@chart_pembelian", cekchartpembelian)
            cmmd.Parameters.AddWithValue("@chart_penjualan", cekchartpenjualan)
            cmmd.Parameters.AddWithValue("@chart_lunas_utang", cekchartpelunasanutang)
            cmmd.Parameters.AddWithValue("@chart_lunas_piutang", cekchartpelunasanpiutang)
            cmmd.Parameters.AddWithValue("@chart_kas_masuk", cekchartakunmasuk)
            cmmd.Parameters.AddWithValue("@chart_kas_keluar", cekchartakunkeluar)
            'feature
            cmmd.Parameters.AddWithValue("@feature_kalkulasi_expedisi", cekfeaturekalkulasi)
            cmmd.Parameters.AddWithValue("@feature_barcode_generator", cekfeaturebarcode)
            'setting
            cmmd.Parameters.AddWithValue("@setting_info_perusahaan", ceksettinginfoperusahaan)
            cmmd.Parameters.AddWithValue("@setting_printer", ceksettingprinter)
            cmmd.Parameters.AddWithValue("@setting_backup_database", ceksettingbackupdatabase)
            cmmd.Parameters.AddWithValue("@setting_pengaturan", ceksettingpengaturan)

            ' end akses
            cmmd.Parameters.AddWithValue("@updated_by", fmenu.kodeuser.Text)
            cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
            cmmd.ExecuteNonQuery()

            MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            Call historysave("Mengedit Data User kode " & txtkode.Text, txtkode.Text, namaform)

            Me.Refresh()
            Call awal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkode.KeyPress
        e.Handled = ValidAngkaHuruf(e)
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    Call koneksi()
                    sql = "DELETE FROM tb_user WHERE id='" & iduser & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    MessageBox.Show(txtnama.Text & " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Call historysave("Menghapus Data User kode " & txtkode.Text, txtkode.Text, namaform)

                    Me.Refresh()
                    Call awal()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Private Sub btnpassword_Click(sender As Object, e As EventArgs) Handles btnpassword.Click
        fgeneratepassword.ShowDialog()
    End Sub

    Sub cari()
        iduser = GridView.GetFocusedRowCellValue("id")

        sql = "SELECT * FROM tb_user WHERE id ='" & iduser & "'"
        cmmd = New OdbcCommand(sql, cnn)

        dr = cmmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            kodeuser = dr("kode_user")
            txtkode.Text = kodeuser
            txtnama.Text = dr("nama_user")

            txtpassword.Text = dr("password_user")
            aksesauthuser = dr("auth_user")

            cmbjabatan.Text = dr("jabatan_user")
            txtmaxprint.Text = dr("max_print")

            txtemail.Text = dr("email_user")
            txttelp.Text = dr("telepon_user")
            txtalamat.Text = dr("alamat_user")
            txtketerangan.Text = dr("keterangan_user")


            'akses user
            'master
            aksesbarang = Val(dr("master_barang"))
            akseskategori = Val(dr("master_kategori"))
            aksesgudang = Val(dr("master_gudang"))
            aksespelanggan = Val(dr("master_pelanggan"))
            aksessupplier = Val(dr("master_supplier"))
            aksesuser = Val(dr("master_user"))
            akseskas = Val(dr("master_kas"))
            aksespricelist = Val(dr("master_pricelist"))
            aksesreksupp = Val(dr("master_rek_supp"))
            aksesrekplng = Val(dr("master_rek_plng"))

            'transaksi
            aksespembelian = Val(dr("pembelian"))
            aksespenjualan = Val(dr("penjualan"))
            aksesreturbeli = Val(dr("retur_beli"))
            aksesreturjual = Val(dr("retur_jual"))
            aksesbarangmasuk = Val(dr("barang_masuk"))
            aksesbarangkeluar = Val(dr("barang_keluar"))
            aksestransferbarang = Val(dr("transfer_barang"))
            aksespenyesuaianstok = Val(dr("penyesuaian_stok"))

            'administrasi
            akseslunasutang = Val(dr("lunas_utang"))
            akseslunaspiutang = Val(dr("lunas_piutang"))
            aksestransferkas = Val(dr("transfer_kas"))
            aksesakunmasuk = Val(dr("akun_masuk"))
            aksesakunkeluar = Val(dr("akun_keluar"))

            'laporan
            akseslappricelist = Val(dr("lap_pricelist"))
            akseslappembelian = Val(dr("lap_pembelian"))
            akseslappenjualan = Val(dr("lap_penjualan"))
            akseslappenjualanpajak = Val(dr("lap_penjualan_pajak"))

            akseslapreturbeli = Val(dr("lap_returbeli"))
            akseslapreturjual = Val(dr("lap_returjual"))
            akseslapbarangmasuk = Val(dr("lap_barang_masuk"))
            akseslapbarangkeluar = Val(dr("lap_barang_keluar"))

            akseslaptransferbarang = Val(dr("lap_transfer_barang"))
            akseslapstokbarang = Val(dr("lap_stok_barang"))
            akseslaputang = Val(dr("lap_utang"))
            akseslappiutang = Val(dr("lap_piutang"))

            akseslapakunmasuk = Val(dr("lap_akun_masuk"))
            akseslapakunkeluar = Val(dr("lap_akun_keluar"))
            akseslaptransferkas = Val(dr("lap_transfer_kas"))
            akseslaptransaksikas = Val(dr("lap_transaksi_kas"))

            akseslapmodalbarang = Val(dr("lap_modal_barang"))
            akseslapmutasibarang = Val(dr("lap_mutasi_barang"))
            akseslappenyesuaianstok = Val(dr("lap_penyesuaian_stok"))
            akseslaplabarugi = Val(dr("lap_laba_rugi"))
            akseslaprekapanharian = Val(dr("lap_rekapan_harian"))

            akseschartpembelian = dr("chart_pembelian")
            akseschartpenjualan = dr("chart_Penjualan")
            akseschartpelunasanutang = dr("chart_lunas_utang")
            akseschartpelunasanpiutang = dr("chart_lunas_piutang")
            akseschartakunmasuk = dr("chart_kas_masuk")
            akseschartakunkeluar = dr("chart_kas_keluar")

            aksesfeaturekalkulasi = dr("feature_kalkulasi_expedisi")
            aksesfeaturebarcode = dr("feature_barcode_generator")

            aksessettinginfoperusahaan = dr("setting_info_perusahaan")
            aksessettingprinter = dr("setting_printer")
            aksessettingbackupdatabase = dr("setting_backup_database")
            aksessettingpengaturan = dr("setting_pengaturan")

            Select Case aksesauthuser
                Case 0
                    cbauth.Checked = False
                Case 1
                    cbauth.Checked = True
            End Select

            '== mulai case ==
            'master=======================================================================
            Select Case akseskategori
                Case 0
                    cbmasterkategori.Checked = False
                    clbmasterkategori.SetItemChecked(0, False)
                    clbmasterkategori.SetItemChecked(1, False)
                    clbmasterkategori.SetItemChecked(2, False)
                Case 1
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, True)
                    clbmasterkategori.SetItemChecked(1, False)
                    clbmasterkategori.SetItemChecked(2, False)
                Case 3
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, False)
                    clbmasterkategori.SetItemChecked(1, True)
                    clbmasterkategori.SetItemChecked(2, False)
                Case 5
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, False)
                    clbmasterkategori.SetItemChecked(1, False)
                    clbmasterkategori.SetItemChecked(2, True)
                Case 4
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, True)
                    clbmasterkategori.SetItemChecked(1, True)
                    clbmasterkategori.SetItemChecked(2, False)
                Case 6
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, True)
                    clbmasterkategori.SetItemChecked(1, False)
                    clbmasterkategori.SetItemChecked(2, True)
                Case 8
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, False)
                    clbmasterkategori.SetItemChecked(1, True)
                    clbmasterkategori.SetItemChecked(2, True)
                Case 9
                    cbmasterkategori.Checked = True
                    clbmasterkategori.SetItemChecked(0, True)
                    clbmasterkategori.SetItemChecked(1, True)
                    clbmasterkategori.SetItemChecked(2, True)
            End Select

            Select Case aksesbarang
                Case 0
                    cbmasterbarang.Checked = False
                    clbmasterbarang.SetItemChecked(0, False)
                    clbmasterbarang.SetItemChecked(1, False)
                    clbmasterbarang.SetItemChecked(2, False)
                Case 1
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, True)
                    clbmasterbarang.SetItemChecked(1, False)
                    clbmasterbarang.SetItemChecked(2, False)
                Case 3
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, False)
                    clbmasterbarang.SetItemChecked(1, True)
                    clbmasterbarang.SetItemChecked(2, False)
                Case 5
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, False)
                    clbmasterbarang.SetItemChecked(1, False)
                    clbmasterbarang.SetItemChecked(2, True)
                Case 4
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, True)
                    clbmasterbarang.SetItemChecked(1, True)
                    clbmasterbarang.SetItemChecked(2, False)
                Case 6
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, True)
                    clbmasterbarang.SetItemChecked(1, False)
                    clbmasterbarang.SetItemChecked(2, True)
                Case 8
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, False)
                    clbmasterbarang.SetItemChecked(1, True)
                    clbmasterbarang.SetItemChecked(2, True)
                Case 9
                    cbmasterbarang.Checked = True
                    clbmasterbarang.SetItemChecked(0, True)
                    clbmasterbarang.SetItemChecked(1, True)
                    clbmasterbarang.SetItemChecked(2, True)
            End Select

            Select Case aksesgudang
                Case 0
                    cbmastergudang.Checked = False
                    clbmastergudang.SetItemChecked(0, False)
                    clbmastergudang.SetItemChecked(1, False)
                    clbmastergudang.SetItemChecked(2, False)
                Case 1
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, True)
                    clbmastergudang.SetItemChecked(1, False)
                    clbmastergudang.SetItemChecked(2, False)
                Case 3
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, False)
                    clbmastergudang.SetItemChecked(1, True)
                    clbmastergudang.SetItemChecked(2, False)
                Case 5
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, False)
                    clbmastergudang.SetItemChecked(1, False)
                    clbmastergudang.SetItemChecked(2, True)
                Case 4
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, True)
                    clbmastergudang.SetItemChecked(1, True)
                    clbmastergudang.SetItemChecked(2, False)
                Case 6
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, True)
                    clbmastergudang.SetItemChecked(1, False)
                    clbmastergudang.SetItemChecked(2, True)
                Case 8
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, False)
                    clbmastergudang.SetItemChecked(1, True)
                    clbmastergudang.SetItemChecked(2, True)
                Case 9
                    cbmastergudang.Checked = True
                    clbmastergudang.SetItemChecked(0, True)
                    clbmastergudang.SetItemChecked(1, True)
                    clbmastergudang.SetItemChecked(2, True)
            End Select

            Select Case aksespelanggan
                Case 0
                    cbmasterpelanggan.Checked = False
                    clbmasterpelanggan.SetItemChecked(0, False)
                    clbmasterpelanggan.SetItemChecked(1, False)
                    clbmasterpelanggan.SetItemChecked(2, False)
                Case 1
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, True)
                    clbmasterpelanggan.SetItemChecked(1, False)
                    clbmasterpelanggan.SetItemChecked(2, False)
                Case 3
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, False)
                    clbmasterpelanggan.SetItemChecked(1, True)
                    clbmasterpelanggan.SetItemChecked(2, False)
                Case 5
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, False)
                    clbmasterpelanggan.SetItemChecked(1, False)
                    clbmasterpelanggan.SetItemChecked(2, True)
                Case 4
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, True)
                    clbmasterpelanggan.SetItemChecked(1, True)
                    clbmasterpelanggan.SetItemChecked(2, False)
                Case 6
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, True)
                    clbmasterpelanggan.SetItemChecked(1, False)
                    clbmasterpelanggan.SetItemChecked(2, True)
                Case 8
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, False)
                    clbmasterpelanggan.SetItemChecked(1, True)
                    clbmasterpelanggan.SetItemChecked(2, True)
                Case 9
                    cbmasterpelanggan.Checked = True
                    clbmasterpelanggan.SetItemChecked(0, True)
                    clbmasterpelanggan.SetItemChecked(1, True)
                    clbmasterpelanggan.SetItemChecked(2, True)
            End Select

            Select Case aksessupplier
                Case 0
                    cbmastersupplier.Checked = False
                    clbmastersupplier.SetItemChecked(0, False)
                    clbmastersupplier.SetItemChecked(1, False)
                    clbmastersupplier.SetItemChecked(2, False)
                Case 1
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, True)
                    clbmastersupplier.SetItemChecked(1, False)
                    clbmastersupplier.SetItemChecked(2, False)
                Case 3
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, False)
                    clbmastersupplier.SetItemChecked(1, True)
                    clbmastersupplier.SetItemChecked(2, False)
                Case 5
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, False)
                    clbmastersupplier.SetItemChecked(1, False)
                    clbmastersupplier.SetItemChecked(2, True)
                Case 4
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, True)
                    clbmastersupplier.SetItemChecked(1, True)
                    clbmastersupplier.SetItemChecked(2, False)
                Case 6
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, True)
                    clbmastersupplier.SetItemChecked(1, False)
                    clbmastersupplier.SetItemChecked(2, True)
                Case 8
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, False)
                    clbmastersupplier.SetItemChecked(1, True)
                    clbmastersupplier.SetItemChecked(2, True)
                Case 9
                    cbmastersupplier.Checked = True
                    clbmastersupplier.SetItemChecked(0, True)
                    clbmastersupplier.SetItemChecked(1, True)
                    clbmastersupplier.SetItemChecked(2, True)
            End Select

            Select Case aksesuser
                Case 0
                    cbmasteruser.Checked = False
                    clbmasteruser.SetItemChecked(0, False)
                    clbmasteruser.SetItemChecked(1, False)
                    clbmasteruser.SetItemChecked(2, False)
                Case 1
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, True)
                    clbmasteruser.SetItemChecked(1, False)
                    clbmasteruser.SetItemChecked(2, False)
                Case 3
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, False)
                    clbmasteruser.SetItemChecked(1, True)
                    clbmasteruser.SetItemChecked(2, False)
                Case 5
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, False)
                    clbmasteruser.SetItemChecked(1, False)
                    clbmasteruser.SetItemChecked(2, True)
                Case 4
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, True)
                    clbmasteruser.SetItemChecked(1, True)
                    clbmasteruser.SetItemChecked(2, False)
                Case 6
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, True)
                    clbmasteruser.SetItemChecked(1, False)
                    clbmasteruser.SetItemChecked(2, True)
                Case 8
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, False)
                    clbmasteruser.SetItemChecked(1, True)
                    clbmasteruser.SetItemChecked(2, True)
                Case 9
                    cbmasteruser.Checked = True
                    clbmasteruser.SetItemChecked(0, True)
                    clbmasteruser.SetItemChecked(1, True)
                    clbmasteruser.SetItemChecked(2, True)
            End Select

            Select Case akseskas
                Case 0
                    cbmasterkas.Checked = False
                    clbmasterkas.SetItemChecked(0, False)
                    clbmasterkas.SetItemChecked(1, False)
                    clbmasterkas.SetItemChecked(2, False)
                Case 1
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, True)
                    clbmasterkas.SetItemChecked(1, False)
                    clbmasterkas.SetItemChecked(2, False)
                Case 3
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, False)
                    clbmasterkas.SetItemChecked(1, True)
                    clbmasterkas.SetItemChecked(2, False)
                Case 5
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, False)
                    clbmasterkas.SetItemChecked(1, False)
                    clbmasterkas.SetItemChecked(2, True)
                Case 4
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, True)
                    clbmasterkas.SetItemChecked(1, True)
                    clbmasterkas.SetItemChecked(2, False)
                Case 6
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, True)
                    clbmasterkas.SetItemChecked(1, False)
                    clbmasterkas.SetItemChecked(2, True)
                Case 8
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, False)
                    clbmasterkas.SetItemChecked(1, True)
                    clbmasterkas.SetItemChecked(2, True)
                Case 9
                    cbmasterkas.Checked = True
                    clbmasterkas.SetItemChecked(0, True)
                    clbmasterkas.SetItemChecked(1, True)
                    clbmasterkas.SetItemChecked(2, True)
            End Select

            Select Case aksespricelist
                Case 0
                    cbmasterpricelist.Checked = False
                    clbmasterpricelist.SetItemChecked(0, False)
                    clbmasterpricelist.SetItemChecked(1, False)
                    clbmasterpricelist.SetItemChecked(2, False)
                Case 1
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, True)
                    clbmasterpricelist.SetItemChecked(1, False)
                    clbmasterpricelist.SetItemChecked(2, False)
                Case 3
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, False)
                    clbmasterpricelist.SetItemChecked(1, True)
                    clbmasterpricelist.SetItemChecked(2, False)
                Case 5
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, False)
                    clbmasterpricelist.SetItemChecked(1, False)
                    clbmasterpricelist.SetItemChecked(2, True)
                Case 4
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, True)
                    clbmasterpricelist.SetItemChecked(1, True)
                    clbmasterpricelist.SetItemChecked(2, False)
                Case 6
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, True)
                    clbmasterpricelist.SetItemChecked(1, False)
                    clbmasterpricelist.SetItemChecked(2, True)
                Case 8
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, False)
                    clbmasterpricelist.SetItemChecked(1, True)
                    clbmasterpricelist.SetItemChecked(2, True)
                Case 9
                    cbmasterpricelist.Checked = True
                    clbmasterpricelist.SetItemChecked(0, True)
                    clbmasterpricelist.SetItemChecked(1, True)
                    clbmasterpricelist.SetItemChecked(2, True)
            End Select

            Select Case aksesreksupp
                Case 0
                    cbmasterreksupp.Checked = False
                    clbmasterreksupp.SetItemChecked(0, False)
                    clbmasterreksupp.SetItemChecked(1, False)
                    clbmasterreksupp.SetItemChecked(2, False)
                Case 1
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, True)
                    clbmasterreksupp.SetItemChecked(1, False)
                    clbmasterreksupp.SetItemChecked(2, False)
                Case 3
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, False)
                    clbmasterreksupp.SetItemChecked(1, True)
                    clbmasterreksupp.SetItemChecked(2, False)
                Case 5
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, False)
                    clbmasterreksupp.SetItemChecked(1, False)
                    clbmasterreksupp.SetItemChecked(2, True)
                Case 4
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, True)
                    clbmasterreksupp.SetItemChecked(1, True)
                    clbmasterreksupp.SetItemChecked(2, False)
                Case 6
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, True)
                    clbmasterreksupp.SetItemChecked(1, False)
                    clbmasterreksupp.SetItemChecked(2, True)
                Case 8
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, False)
                    clbmasterreksupp.SetItemChecked(1, True)
                    clbmasterreksupp.SetItemChecked(2, True)
                Case 9
                    cbmasterreksupp.Checked = True
                    clbmasterreksupp.SetItemChecked(0, True)
                    clbmasterreksupp.SetItemChecked(1, True)
                    clbmasterreksupp.SetItemChecked(2, True)
            End Select

            Select Case aksesrekplng
                Case 0
                    cbmasterrekplng.Checked = False
                    clbmasterrekplng.SetItemChecked(0, False)
                    clbmasterrekplng.SetItemChecked(1, False)
                    clbmasterrekplng.SetItemChecked(2, False)
                Case 1
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, True)
                    clbmasterrekplng.SetItemChecked(1, False)
                    clbmasterrekplng.SetItemChecked(2, False)
                Case 3
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, False)
                    clbmasterrekplng.SetItemChecked(1, True)
                    clbmasterrekplng.SetItemChecked(2, False)
                Case 5
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, False)
                    clbmasterrekplng.SetItemChecked(1, False)
                    clbmasterrekplng.SetItemChecked(2, True)
                Case 4
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, True)
                    clbmasterrekplng.SetItemChecked(1, True)
                    clbmasterrekplng.SetItemChecked(2, False)
                Case 6
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, True)
                    clbmasterrekplng.SetItemChecked(1, False)
                    clbmasterrekplng.SetItemChecked(2, True)
                Case 8
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, False)
                    clbmasterrekplng.SetItemChecked(1, True)
                    clbmasterrekplng.SetItemChecked(2, True)
                Case 9
                    cbmasterrekplng.Checked = True
                    clbmasterrekplng.SetItemChecked(0, True)
                    clbmasterrekplng.SetItemChecked(1, True)
                    clbmasterrekplng.SetItemChecked(2, True)
            End Select

            'transaksi====================================================================
            Select Case aksespembelian
                Case 0
                    cbpembelian.Checked = False
                    clbpembelian.SetItemChecked(0, False)
                    clbpembelian.SetItemChecked(1, False)
                    clbpembelian.SetItemChecked(2, False)
                Case 1
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, True)
                    clbpembelian.SetItemChecked(1, False)
                    clbpembelian.SetItemChecked(2, False)
                Case 3
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, False)
                    clbpembelian.SetItemChecked(1, True)
                    clbpembelian.SetItemChecked(2, False)
                Case 5
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, False)
                    clbpembelian.SetItemChecked(1, False)
                    clbpembelian.SetItemChecked(2, True)
                Case 4
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, True)
                    clbpembelian.SetItemChecked(1, True)
                    clbpembelian.SetItemChecked(2, False)
                Case 6
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, True)
                    clbpembelian.SetItemChecked(1, False)
                    clbpembelian.SetItemChecked(2, True)
                Case 8
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, False)
                    clbpembelian.SetItemChecked(1, True)
                    clbpembelian.SetItemChecked(2, True)
                Case 9
                    cbpembelian.Checked = True
                    clbpembelian.SetItemChecked(0, True)
                    clbpembelian.SetItemChecked(1, True)
                    clbpembelian.SetItemChecked(2, True)
            End Select

            Select Case aksespenjualan
                Case 0
                    cbpenjualan.Checked = False
                    clbpenjualan.SetItemChecked(0, False)
                    clbpenjualan.SetItemChecked(1, False)
                    clbpenjualan.SetItemChecked(2, False)
                Case 1
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, True)
                    clbpenjualan.SetItemChecked(1, False)
                    clbpenjualan.SetItemChecked(2, False)
                Case 3
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, False)
                    clbpenjualan.SetItemChecked(1, True)
                    clbpenjualan.SetItemChecked(2, False)
                Case 5
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, False)
                    clbpenjualan.SetItemChecked(1, False)
                    clbpenjualan.SetItemChecked(2, True)
                Case 4
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, True)
                    clbpenjualan.SetItemChecked(1, True)
                    clbpenjualan.SetItemChecked(2, False)
                Case 6
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, True)
                    clbpenjualan.SetItemChecked(1, False)
                    clbpenjualan.SetItemChecked(2, True)
                Case 8
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, False)
                    clbpenjualan.SetItemChecked(1, True)
                    clbpenjualan.SetItemChecked(2, True)
                Case 9
                    cbpenjualan.Checked = True
                    clbpenjualan.SetItemChecked(0, True)
                    clbpenjualan.SetItemChecked(1, True)
                    clbpenjualan.SetItemChecked(2, True)
            End Select

            Select Case aksesreturbeli
                Case 0
                    cbreturbeli.Checked = False
                    clbreturbeli.SetItemChecked(0, False)
                    clbreturbeli.SetItemChecked(1, False)
                    clbreturbeli.SetItemChecked(2, False)
                Case 1
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, True)
                    clbreturbeli.SetItemChecked(1, False)
                    clbreturbeli.SetItemChecked(2, False)
                Case 3
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, False)
                    clbreturbeli.SetItemChecked(1, True)
                    clbreturbeli.SetItemChecked(2, False)
                Case 5
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, False)
                    clbreturbeli.SetItemChecked(1, False)
                    clbreturbeli.SetItemChecked(2, True)
                Case 4
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, True)
                    clbreturbeli.SetItemChecked(1, True)
                    clbreturbeli.SetItemChecked(2, False)
                Case 6
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, True)
                    clbreturbeli.SetItemChecked(1, False)
                    clbreturbeli.SetItemChecked(2, True)
                Case 8
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, False)
                    clbreturbeli.SetItemChecked(1, True)
                    clbreturbeli.SetItemChecked(2, True)
                Case 9
                    cbreturbeli.Checked = True
                    clbreturbeli.SetItemChecked(0, True)
                    clbreturbeli.SetItemChecked(1, True)
                    clbreturbeli.SetItemChecked(2, True)
            End Select

            Select Case aksesreturjual
                Case 0
                    cbreturjual.Checked = False
                    clbreturjual.SetItemChecked(0, False)
                    clbreturjual.SetItemChecked(1, False)
                    clbreturjual.SetItemChecked(2, False)
                Case 1
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, True)
                    clbreturjual.SetItemChecked(1, False)
                    clbreturjual.SetItemChecked(2, False)
                Case 3
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, False)
                    clbreturjual.SetItemChecked(1, True)
                    clbreturjual.SetItemChecked(2, False)
                Case 5
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, False)
                    clbreturjual.SetItemChecked(1, False)
                    clbreturjual.SetItemChecked(2, True)
                Case 4
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, True)
                    clbreturjual.SetItemChecked(1, True)
                    clbreturjual.SetItemChecked(2, False)
                Case 6
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, True)
                    clbreturjual.SetItemChecked(1, False)
                    clbreturjual.SetItemChecked(2, True)
                Case 8
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, False)
                    clbreturjual.SetItemChecked(1, True)
                    clbreturjual.SetItemChecked(2, True)
                Case 9
                    cbreturjual.Checked = True
                    clbreturjual.SetItemChecked(0, True)
                    clbreturjual.SetItemChecked(1, True)
                    clbreturjual.SetItemChecked(2, True)
            End Select

            Select Case aksesbarangmasuk
                Case 0
                    cbbarangmasuk.Checked = False
                    clbbarangmasuk.SetItemChecked(0, False)
                    clbbarangmasuk.SetItemChecked(1, False)
                    clbbarangmasuk.SetItemChecked(2, False)
                Case 1
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, True)
                    clbbarangmasuk.SetItemChecked(1, False)
                    clbbarangmasuk.SetItemChecked(2, False)
                Case 3
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, False)
                    clbbarangmasuk.SetItemChecked(1, True)
                    clbbarangmasuk.SetItemChecked(2, False)
                Case 5
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, False)
                    clbbarangmasuk.SetItemChecked(1, False)
                    clbbarangmasuk.SetItemChecked(2, True)
                Case 4
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, True)
                    clbbarangmasuk.SetItemChecked(1, True)
                    clbbarangmasuk.SetItemChecked(2, False)
                Case 6
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, True)
                    clbbarangmasuk.SetItemChecked(1, False)
                    clbbarangmasuk.SetItemChecked(2, True)
                Case 8
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, False)
                    clbbarangmasuk.SetItemChecked(1, True)
                    clbbarangmasuk.SetItemChecked(2, True)
                Case 9
                    cbbarangmasuk.Checked = True
                    clbbarangmasuk.SetItemChecked(0, True)
                    clbbarangmasuk.SetItemChecked(1, True)
                    clbbarangmasuk.SetItemChecked(2, True)
            End Select

            Select Case aksesbarangkeluar
                Case 0
                    cbbarangkeluar.Checked = False
                    clbbarangkeluar.SetItemChecked(0, False)
                    clbbarangkeluar.SetItemChecked(1, False)
                    clbbarangkeluar.SetItemChecked(2, False)
                Case 1
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, True)
                    clbbarangkeluar.SetItemChecked(1, False)
                    clbbarangkeluar.SetItemChecked(2, False)
                Case 3
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, False)
                    clbbarangkeluar.SetItemChecked(1, True)
                    clbbarangkeluar.SetItemChecked(2, False)
                Case 5
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, False)
                    clbbarangkeluar.SetItemChecked(1, False)
                    clbbarangkeluar.SetItemChecked(2, True)
                Case 4
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, True)
                    clbbarangkeluar.SetItemChecked(1, True)
                    clbbarangkeluar.SetItemChecked(2, False)
                Case 6
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, True)
                    clbbarangkeluar.SetItemChecked(1, False)
                    clbbarangkeluar.SetItemChecked(2, True)
                Case 8
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, False)
                    clbbarangkeluar.SetItemChecked(1, True)
                    clbbarangkeluar.SetItemChecked(2, True)
                Case 9
                    cbbarangkeluar.Checked = True
                    clbbarangkeluar.SetItemChecked(0, True)
                    clbbarangkeluar.SetItemChecked(1, True)
                    clbbarangkeluar.SetItemChecked(2, True)
            End Select

            Select Case aksestransferbarang
                Case 0
                    cbtransferbarang.Checked = False
                    clbtransferbarang.SetItemChecked(0, False)
                    clbtransferbarang.SetItemChecked(1, False)
                    clbtransferbarang.SetItemChecked(2, False)
                Case 1
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, True)
                    clbtransferbarang.SetItemChecked(1, False)
                    clbtransferbarang.SetItemChecked(2, False)
                Case 3
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, False)
                    clbtransferbarang.SetItemChecked(1, True)
                    clbtransferbarang.SetItemChecked(2, False)
                Case 5
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, False)
                    clbtransferbarang.SetItemChecked(1, False)
                    clbtransferbarang.SetItemChecked(2, True)
                Case 4
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, True)
                    clbtransferbarang.SetItemChecked(1, True)
                    clbtransferbarang.SetItemChecked(2, False)
                Case 6
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, True)
                    clbtransferbarang.SetItemChecked(1, False)
                    clbtransferbarang.SetItemChecked(2, True)
                Case 8
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, False)
                    clbtransferbarang.SetItemChecked(1, True)
                    clbtransferbarang.SetItemChecked(2, True)
                Case 9
                    cbtransferbarang.Checked = True
                    clbtransferbarang.SetItemChecked(0, True)
                    clbtransferbarang.SetItemChecked(1, True)
                    clbtransferbarang.SetItemChecked(2, True)
            End Select

            Select Case aksespenyesuaianstok
                Case 0
                    cbpenyesuaianstok.Checked = False
                    clbpenyesuaianstok.SetItemChecked(0, False)
                    clbpenyesuaianstok.SetItemChecked(1, False)
                    clbpenyesuaianstok.SetItemChecked(2, False)
                Case 1
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, True)
                    clbpenyesuaianstok.SetItemChecked(1, False)
                    clbpenyesuaianstok.SetItemChecked(2, False)
                Case 3
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, False)
                    clbpenyesuaianstok.SetItemChecked(1, True)
                    clbpenyesuaianstok.SetItemChecked(2, False)
                Case 5
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, False)
                    clbpenyesuaianstok.SetItemChecked(1, False)
                    clbpenyesuaianstok.SetItemChecked(2, True)
                Case 4
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, True)
                    clbpenyesuaianstok.SetItemChecked(1, True)
                    clbpenyesuaianstok.SetItemChecked(2, False)
                Case 6
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, True)
                    clbpenyesuaianstok.SetItemChecked(1, False)
                    clbpenyesuaianstok.SetItemChecked(2, True)
                Case 8
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, False)
                    clbpenyesuaianstok.SetItemChecked(1, True)
                    clbpenyesuaianstok.SetItemChecked(2, True)
                Case 9
                    cbpenyesuaianstok.Checked = True
                    clbpenyesuaianstok.SetItemChecked(0, True)
                    clbpenyesuaianstok.SetItemChecked(1, True)
                    clbpenyesuaianstok.SetItemChecked(2, True)
            End Select

            'administrasi=================================================================

            Select Case akseslunasutang
                Case 0
                    cblunasutang.Checked = False
                    clblunasutang.SetItemChecked(0, False)
                    clblunasutang.SetItemChecked(1, False)
                    clblunasutang.SetItemChecked(2, False)
                Case 1
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, True)
                    clblunasutang.SetItemChecked(1, False)
                    clblunasutang.SetItemChecked(2, False)
                Case 3
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, False)
                    clblunasutang.SetItemChecked(1, True)
                    clblunasutang.SetItemChecked(2, False)
                Case 5
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, False)
                    clblunasutang.SetItemChecked(1, False)
                    clblunasutang.SetItemChecked(2, True)
                Case 4
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, True)
                    clblunasutang.SetItemChecked(1, True)
                    clblunasutang.SetItemChecked(2, False)
                Case 6
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, True)
                    clblunasutang.SetItemChecked(1, False)
                    clblunasutang.SetItemChecked(2, True)
                Case 8
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, False)
                    clblunasutang.SetItemChecked(1, True)
                    clblunasutang.SetItemChecked(2, True)
                Case 9
                    cblunasutang.Checked = True
                    clblunasutang.SetItemChecked(0, True)
                    clblunasutang.SetItemChecked(1, True)
                    clblunasutang.SetItemChecked(2, True)
            End Select

            Select Case akseslunaspiutang
                Case 0
                    cblunaspiutang.Checked = False
                    clblunaspiutang.SetItemChecked(0, False)
                    clblunaspiutang.SetItemChecked(1, False)
                    clblunaspiutang.SetItemChecked(2, False)
                Case 1
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, True)
                    clblunaspiutang.SetItemChecked(1, False)
                    clblunaspiutang.SetItemChecked(2, False)
                Case 3
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, False)
                    clblunaspiutang.SetItemChecked(1, True)
                    clblunaspiutang.SetItemChecked(2, False)
                Case 5
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, False)
                    clblunaspiutang.SetItemChecked(1, False)
                    clblunaspiutang.SetItemChecked(2, True)
                Case 4
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, True)
                    clblunaspiutang.SetItemChecked(1, True)
                    clblunaspiutang.SetItemChecked(2, False)
                Case 6
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, True)
                    clblunaspiutang.SetItemChecked(1, False)
                    clblunaspiutang.SetItemChecked(2, True)
                Case 8
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, False)
                    clblunaspiutang.SetItemChecked(1, True)
                    clblunaspiutang.SetItemChecked(2, True)
                Case 9
                    cblunaspiutang.Checked = True
                    clblunaspiutang.SetItemChecked(0, True)
                    clblunaspiutang.SetItemChecked(1, True)
                    clblunaspiutang.SetItemChecked(2, True)
            End Select

            Select Case aksestransferkas
                Case 0
                    cbtransferkas.Checked = False
                    clbtransferkas.SetItemChecked(0, False)
                    clbtransferkas.SetItemChecked(1, False)
                    clbtransferkas.SetItemChecked(2, False)
                Case 1
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, True)
                    clbtransferkas.SetItemChecked(1, False)
                    clbtransferkas.SetItemChecked(2, False)
                Case 3
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, False)
                    clbtransferkas.SetItemChecked(1, True)
                    clbtransferkas.SetItemChecked(2, False)
                Case 5
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, False)
                    clbtransferkas.SetItemChecked(1, False)
                    clbtransferkas.SetItemChecked(2, True)
                Case 4
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, True)
                    clbtransferkas.SetItemChecked(1, True)
                    clbtransferkas.SetItemChecked(2, False)
                Case 6
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, True)
                    clbtransferkas.SetItemChecked(1, False)
                    clbtransferkas.SetItemChecked(2, True)
                Case 8
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, False)
                    clbtransferkas.SetItemChecked(1, True)
                    clbtransferkas.SetItemChecked(2, True)
                Case 9
                    cbtransferkas.Checked = True
                    clbtransferkas.SetItemChecked(0, True)
                    clbtransferkas.SetItemChecked(1, True)
                    clbtransferkas.SetItemChecked(2, True)
            End Select

            Select Case aksesakunmasuk
                Case 0
                    cbakunmasuk.Checked = False
                    clbakunmasuk.SetItemChecked(0, False)
                    clbakunmasuk.SetItemChecked(1, False)
                    clbakunmasuk.SetItemChecked(2, False)
                Case 1
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, True)
                    clbakunmasuk.SetItemChecked(1, False)
                    clbakunmasuk.SetItemChecked(2, False)
                Case 3
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, False)
                    clbakunmasuk.SetItemChecked(1, True)
                    clbakunmasuk.SetItemChecked(2, False)
                Case 5
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, False)
                    clbakunmasuk.SetItemChecked(1, False)
                    clbakunmasuk.SetItemChecked(2, True)
                Case 4
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, True)
                    clbakunmasuk.SetItemChecked(1, True)
                    clbakunmasuk.SetItemChecked(2, False)
                Case 6
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, True)
                    clbakunmasuk.SetItemChecked(1, False)
                    clbakunmasuk.SetItemChecked(2, True)
                Case 8
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, False)
                    clbakunmasuk.SetItemChecked(1, True)
                    clbakunmasuk.SetItemChecked(2, True)
                Case 9
                    cbakunmasuk.Checked = True
                    clbakunmasuk.SetItemChecked(0, True)
                    clbakunmasuk.SetItemChecked(1, True)
                    clbakunmasuk.SetItemChecked(2, True)
            End Select

            Select Case aksesakunkeluar
                Case 0
                    cbakunkeluar.Checked = False
                    clbakunkeluar.SetItemChecked(0, False)
                    clbakunkeluar.SetItemChecked(1, False)
                    clbakunkeluar.SetItemChecked(2, False)
                Case 1
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, True)
                    clbakunkeluar.SetItemChecked(1, False)
                    clbakunkeluar.SetItemChecked(2, False)
                Case 3
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, False)
                    clbakunkeluar.SetItemChecked(1, True)
                    clbakunkeluar.SetItemChecked(2, False)
                Case 5
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, False)
                    clbakunkeluar.SetItemChecked(1, False)
                    clbakunkeluar.SetItemChecked(2, True)
                Case 4
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, True)
                    clbakunkeluar.SetItemChecked(1, True)
                    clbakunkeluar.SetItemChecked(2, False)
                Case 6
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, True)
                    clbakunkeluar.SetItemChecked(1, False)
                    clbakunkeluar.SetItemChecked(2, True)
                Case 8
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, False)
                    clbakunkeluar.SetItemChecked(1, True)
                    clbakunkeluar.SetItemChecked(2, True)
                Case 9
                    cbakunkeluar.Checked = True
                    clbakunkeluar.SetItemChecked(0, True)
                    clbakunkeluar.SetItemChecked(1, True)
                    clbakunkeluar.SetItemChecked(2, True)
            End Select

            'laporan======================================================================
            Select Case akseslappricelist
                Case 0
                    cblappricelist.Checked = False
                    clblappricelist.SetItemChecked(0, False)
                    clblappricelist.SetItemChecked(1, False)
                Case 1
                    cblappricelist.Checked = True
                    clblappricelist.SetItemChecked(0, True)
                    clblappricelist.SetItemChecked(1, False)
                Case 3
                    cblappricelist.Checked = True
                    clblappricelist.SetItemChecked(0, False)
                    clblappricelist.SetItemChecked(1, True)
                Case 4
                    cblappricelist.Checked = True
                    clblappricelist.SetItemChecked(0, True)
                    clblappricelist.SetItemChecked(1, True)
            End Select

            Select Case akseslappembelian
                Case 0
                    cblappembelian.Checked = False
                    clblappembelian.SetItemChecked(0, False)
                    clblappembelian.SetItemChecked(1, False)
                Case 1
                    cblappembelian.Checked = True
                    clblappembelian.SetItemChecked(0, True)
                    clblappembelian.SetItemChecked(1, False)
                Case 3
                    cblappembelian.Checked = True
                    clblappembelian.SetItemChecked(0, False)
                    clblappembelian.SetItemChecked(1, True)
                Case 4
                    cblappembelian.Checked = True
                    clblappembelian.SetItemChecked(0, True)
                    clblappembelian.SetItemChecked(1, True)
            End Select

            Select Case akseslappenjualan
                Case 0
                    cblappenjualan.Checked = False
                    clblappenjualan.SetItemChecked(0, False)
                    clblappenjualan.SetItemChecked(1, False)
                Case 1
                    cblappenjualan.Checked = True
                    clblappenjualan.SetItemChecked(0, True)
                    clblappenjualan.SetItemChecked(1, False)
                Case 3
                    cblappenjualan.Checked = True
                    clblappenjualan.SetItemChecked(0, False)
                    clblappenjualan.SetItemChecked(1, True)
                Case 4
                    cblappenjualan.Checked = True
                    clblappenjualan.SetItemChecked(0, True)
                    clblappenjualan.SetItemChecked(1, True)
            End Select

            Select Case akseslappenjualanpajak
                Case 0
                    cblappenjualanpajak.Checked = False
                    clblappenjualanpajak.SetItemChecked(0, False)
                    clblappenjualanpajak.SetItemChecked(1, False)
                Case 1
                    cblappenjualanpajak.Checked = True
                    clblappenjualanpajak.SetItemChecked(0, True)
                    clblappenjualanpajak.SetItemChecked(1, False)
                Case 3
                    cblappenjualanpajak.Checked = True
                    clblappenjualanpajak.SetItemChecked(0, False)
                    clblappenjualanpajak.SetItemChecked(1, True)
                Case 4
                    cblappenjualanpajak.Checked = True
                    clblappenjualanpajak.SetItemChecked(0, True)
                    clblappenjualanpajak.SetItemChecked(1, True)
            End Select

            '=============================================================================

            Select Case akseslapreturbeli
                Case 0
                    cblapreturbeli.Checked = False
                    clblapreturbeli.SetItemChecked(0, False)
                    clblapreturbeli.SetItemChecked(1, False)
                Case 1
                    cblapreturbeli.Checked = True
                    clblapreturbeli.SetItemChecked(0, True)
                    clblapreturbeli.SetItemChecked(1, False)
                Case 3
                    cblapreturbeli.Checked = True
                    clblapreturbeli.SetItemChecked(0, False)
                    clblapreturbeli.SetItemChecked(1, True)
                Case 4
                    cblapreturbeli.Checked = True
                    clblapreturbeli.SetItemChecked(0, True)
                    clblapreturbeli.SetItemChecked(1, True)
            End Select

            Select Case akseslapreturjual
                Case 0
                    cblapreturjual.Checked = False
                    clblapreturjual.SetItemChecked(0, False)
                    clblapreturjual.SetItemChecked(1, False)
                Case 1
                    cblapreturjual.Checked = True
                    clblapreturjual.SetItemChecked(0, True)
                    clblapreturjual.SetItemChecked(1, False)
                Case 3
                    cblapreturjual.Checked = True
                    clblapreturjual.SetItemChecked(0, False)
                    clblapreturjual.SetItemChecked(1, True)
                Case 4
                    cblapreturjual.Checked = True
                    clblapreturjual.SetItemChecked(0, True)
                    clblapreturjual.SetItemChecked(1, True)
            End Select

            Select Case akseslapbarangmasuk
                Case 0
                    cblapbarangmasuk.Checked = False
                    clblapbarangmasuk.SetItemChecked(0, False)
                    clblapbarangmasuk.SetItemChecked(1, False)
                Case 1
                    cblapbarangmasuk.Checked = True
                    clblapbarangmasuk.SetItemChecked(0, True)
                    clblapbarangmasuk.SetItemChecked(1, False)
                Case 3
                    cblapbarangmasuk.Checked = True
                    clblapbarangmasuk.SetItemChecked(0, False)
                    clblapbarangmasuk.SetItemChecked(1, True)
                Case 4
                    cblapbarangmasuk.Checked = True
                    clblapbarangmasuk.SetItemChecked(0, True)
                    clblapbarangmasuk.SetItemChecked(1, True)
            End Select

            Select Case akseslapbarangkeluar
                Case 0
                    cblapbarangkeluar.Checked = False
                    clblapbarangkeluar.SetItemChecked(0, False)
                    clblapbarangkeluar.SetItemChecked(1, False)
                Case 1
                    cblapbarangkeluar.Checked = True
                    clblapbarangkeluar.SetItemChecked(0, True)
                    clblapbarangkeluar.SetItemChecked(1, False)
                Case 3
                    cblapbarangkeluar.Checked = True
                    clblapbarangkeluar.SetItemChecked(0, False)
                    clblapbarangkeluar.SetItemChecked(1, True)
                Case 4
                    cblapbarangkeluar.Checked = True
                    clblapbarangkeluar.SetItemChecked(0, True)
                    clblapbarangkeluar.SetItemChecked(1, True)
            End Select

            '=============================================================================

            Select Case akseslaptransferbarang
                Case 0
                    cblaptransferbarang.Checked = False
                    clblaptransferbarang.SetItemChecked(0, False)
                    clblaptransferbarang.SetItemChecked(1, False)
                Case 1
                    cblaptransferbarang.Checked = True
                    clblaptransferbarang.SetItemChecked(0, True)
                    clblaptransferbarang.SetItemChecked(1, False)
                Case 3
                    cblaptransferbarang.Checked = True
                    clblaptransferbarang.SetItemChecked(0, False)
                    clblaptransferbarang.SetItemChecked(1, True)
                Case 4
                    cblaptransferbarang.Checked = True
                    clblaptransferbarang.SetItemChecked(0, True)
                    clblaptransferbarang.SetItemChecked(1, True)
            End Select

            Select Case akseslapstokbarang
                Case 0
                    cblapstokbarang.Checked = False
                    clblapstokbarang.SetItemChecked(0, False)
                    clblapstokbarang.SetItemChecked(1, False)
                Case 1
                    cblapstokbarang.Checked = True
                    clblapstokbarang.SetItemChecked(0, True)
                    clblapstokbarang.SetItemChecked(1, False)
                Case 3
                    cblapstokbarang.Checked = True
                    clblapstokbarang.SetItemChecked(0, False)
                    clblapstokbarang.SetItemChecked(1, True)
                Case 4
                    cblapstokbarang.Checked = True
                    clblapstokbarang.SetItemChecked(0, True)
                    clblapstokbarang.SetItemChecked(1, True)
            End Select

            Select Case akseslaputang
                Case 0
                    cblaputang.Checked = False
                    clblaputang.SetItemChecked(0, False)
                    clblaputang.SetItemChecked(1, False)
                Case 1
                    cblaputang.Checked = True
                    clblaputang.SetItemChecked(0, True)
                    clblaputang.SetItemChecked(1, False)
                Case 3
                    cblaputang.Checked = True
                    clblaputang.SetItemChecked(0, False)
                    clblaputang.SetItemChecked(1, True)
                Case 4
                    cblaputang.Checked = True
                    clblaputang.SetItemChecked(0, True)
                    clblaputang.SetItemChecked(1, True)
            End Select

            Select Case akseslappiutang
                Case 0
                    cblappiutang.Checked = False
                    clblappiutang.SetItemChecked(0, False)
                    clblappiutang.SetItemChecked(1, False)
                Case 1
                    cblappiutang.Checked = True
                    clblappiutang.SetItemChecked(0, True)
                    clblappiutang.SetItemChecked(1, False)
                Case 3
                    cblappiutang.Checked = True
                    clblappiutang.SetItemChecked(0, False)
                    clblappiutang.SetItemChecked(1, True)
                Case 4
                    cblappiutang.Checked = True
                    clblappiutang.SetItemChecked(0, True)
                    clblappiutang.SetItemChecked(1, True)
            End Select

            '=============================================================================

            Select Case akseslapakunmasuk
                Case 0
                    cblapakunmasuk.Checked = False
                    clblapakunmasuk.SetItemChecked(0, False)
                    clblapakunmasuk.SetItemChecked(1, False)
                Case 1
                    cblapakunmasuk.Checked = True
                    clblapakunmasuk.SetItemChecked(0, True)
                    clblapakunmasuk.SetItemChecked(1, False)
                Case 3
                    cblapakunmasuk.Checked = True
                    clblapakunmasuk.SetItemChecked(0, False)
                    clblapakunmasuk.SetItemChecked(1, True)
                Case 4
                    cblapakunmasuk.Checked = True
                    clblapakunmasuk.SetItemChecked(0, True)
                    clblapakunmasuk.SetItemChecked(1, True)
            End Select

            Select Case akseslapakunkeluar
                Case 0
                    cblapakunkeluar.Checked = False
                    clblapakunkeluar.SetItemChecked(0, False)
                    clblapakunkeluar.SetItemChecked(1, False)
                Case 1
                    cblapakunkeluar.Checked = True
                    clblapakunkeluar.SetItemChecked(0, True)
                    clblapakunkeluar.SetItemChecked(1, False)
                Case 3
                    cblapakunkeluar.Checked = True
                    clblapakunkeluar.SetItemChecked(0, False)
                    clblapakunkeluar.SetItemChecked(1, True)
                Case 4
                    cblapakunkeluar.Checked = True
                    clblapakunkeluar.SetItemChecked(0, True)
                    clblapakunkeluar.SetItemChecked(1, True)
            End Select

            Select Case akseslaptransferkas
                Case 0
                    cblaptransferkas.Checked = False
                    clblaptransferkas.SetItemChecked(0, False)
                    clblaptransferkas.SetItemChecked(1, False)
                Case 1
                    cblaptransferkas.Checked = True
                    clblaptransferkas.SetItemChecked(0, True)
                    clblaptransferkas.SetItemChecked(1, False)
                Case 3
                    cblaptransferkas.Checked = True
                    clblaptransferkas.SetItemChecked(0, False)
                    clblaptransferkas.SetItemChecked(1, True)
                Case 4
                    cblaptransferkas.Checked = True
                    clblaptransferkas.SetItemChecked(0, True)
                    clblaptransferkas.SetItemChecked(1, True)
            End Select

            Select Case akseslaptransaksikas
                Case 0
                    cblaptransaksikas.Checked = False
                    clblaptransaksikas.SetItemChecked(0, False)
                    clblaptransaksikas.SetItemChecked(1, False)
                Case 1
                    cblaptransaksikas.Checked = True
                    clblaptransaksikas.SetItemChecked(0, True)
                    clblaptransaksikas.SetItemChecked(1, False)
                Case 3
                    cblaptransaksikas.Checked = True
                    clblaptransaksikas.SetItemChecked(0, False)
                    clblaptransaksikas.SetItemChecked(1, True)
                Case 4
                    cblaptransaksikas.Checked = True
                    clblaptransaksikas.SetItemChecked(0, True)
                    clblaptransaksikas.SetItemChecked(1, True)
            End Select

            '=============================================================================

            Select Case akseslapmodalbarang
                Case 0
                    cblapmodalbarang.Checked = False
                    clblapmodalbarang.SetItemChecked(0, False)
                    clblapmodalbarang.SetItemChecked(1, False)
                Case 1
                    cblapmodalbarang.Checked = True
                    clblapmodalbarang.SetItemChecked(0, True)
                    clblapmodalbarang.SetItemChecked(1, False)
                Case 3
                    cblapmodalbarang.Checked = True
                    clblapmodalbarang.SetItemChecked(0, False)
                    clblapmodalbarang.SetItemChecked(1, True)
                Case 4
                    cblapmodalbarang.Checked = True
                    clblapmodalbarang.SetItemChecked(0, True)
                    clblapmodalbarang.SetItemChecked(1, True)
            End Select

            Select Case akseslapmutasibarang
                Case 0
                    cblapmutasibarang.Checked = False
                    clblapmutasibarang.SetItemChecked(0, False)
                    clblapmutasibarang.SetItemChecked(1, False)
                Case 1
                    cblapmutasibarang.Checked = True
                    clblapmutasibarang.SetItemChecked(0, True)
                    clblapmutasibarang.SetItemChecked(1, False)
                Case 3
                    cblapmutasibarang.Checked = True
                    clblapmutasibarang.SetItemChecked(0, False)
                    clblapmutasibarang.SetItemChecked(1, True)
                Case 4
                    cblapmutasibarang.Checked = True
                    clblapmutasibarang.SetItemChecked(0, True)
                    clblapmutasibarang.SetItemChecked(1, True)
            End Select

            Select Case akseslappenyesuaianstok
                Case 0
                    cblappenyesuaianstok.Checked = False
                    clblappenyesuaianstok.SetItemChecked(0, False)
                    clblappenyesuaianstok.SetItemChecked(1, False)
                Case 1
                    cblappenyesuaianstok.Checked = True
                    clblappenyesuaianstok.SetItemChecked(0, True)
                    clblappenyesuaianstok.SetItemChecked(1, False)
                Case 3
                    cblappenyesuaianstok.Checked = True
                    clblappenyesuaianstok.SetItemChecked(0, False)
                    clblappenyesuaianstok.SetItemChecked(1, True)
                Case 4
                    cblappenyesuaianstok.Checked = True
                    clblappenyesuaianstok.SetItemChecked(0, True)
                    clblappenyesuaianstok.SetItemChecked(1, True)
            End Select

            Select Case akseslaplabarugi
                Case 0
                    cblaplabarugi.Checked = False
                    clblaplabarugi.SetItemChecked(0, False)
                    clblaplabarugi.SetItemChecked(1, False)
                Case 1
                    cblaplabarugi.Checked = True
                    clblaplabarugi.SetItemChecked(0, True)
                    clblaplabarugi.SetItemChecked(1, False)
                Case 3
                    cblaplabarugi.Checked = True
                    clblaplabarugi.SetItemChecked(0, False)
                    clblaplabarugi.SetItemChecked(1, True)
                Case 4
                    cblaplabarugi.Checked = True
                    clblaplabarugi.SetItemChecked(0, True)
                    clblaplabarugi.SetItemChecked(1, True)
            End Select

            Select Case akseslaprekapanharian
                Case 0
                    cblaprekapanharian.Checked = False
                    clblaprekapanharian.SetItemChecked(0, False)
                    clblaprekapanharian.SetItemChecked(1, False)
                Case 1
                    cblaprekapanharian.Checked = True
                    clblaprekapanharian.SetItemChecked(0, True)
                    clblaprekapanharian.SetItemChecked(1, False)
                Case 3
                    cblaprekapanharian.Checked = True
                    clblaprekapanharian.SetItemChecked(0, False)
                    clblaprekapanharian.SetItemChecked(1, True)
                Case 4
                    cblaprekapanharian.Checked = True
                    clblaprekapanharian.SetItemChecked(0, True)
                    clblaprekapanharian.SetItemChecked(1, True)
            End Select

            '=============================================================================

            Select Case akseschartpembelian
                Case 0
                    cbchartpembelian.Checked = False
                    clbchartpembelian.SetItemChecked(0, False)
                    clbchartpembelian.SetItemChecked(1, False)
                Case 1
                    cbchartpembelian.Checked = True
                    clbchartpembelian.SetItemChecked(0, True)
                    clbchartpembelian.SetItemChecked(1, False)
                Case 3
                    cbchartpembelian.Checked = True
                    clbchartpembelian.SetItemChecked(0, False)
                    clbchartpembelian.SetItemChecked(1, True)
                Case 4
                    cbchartpembelian.Checked = True
                    clbchartpembelian.SetItemChecked(0, True)
                    clbchartpembelian.SetItemChecked(1, True)
            End Select

            Select Case akseschartpenjualan
                Case 0
                    cbchartpenjualan.Checked = False
                    clbchartpenjualan.SetItemChecked(0, False)
                    clbchartpenjualan.SetItemChecked(1, False)
                Case 1
                    cbchartpenjualan.Checked = True
                    clbchartpenjualan.SetItemChecked(0, True)
                    clbchartpenjualan.SetItemChecked(1, False)
                Case 3
                    cbchartpenjualan.Checked = True
                    clbchartpenjualan.SetItemChecked(0, False)
                    clbchartpenjualan.SetItemChecked(1, True)
                Case 4
                    cbchartpenjualan.Checked = True
                    clbchartpenjualan.SetItemChecked(0, True)
                    clbchartpenjualan.SetItemChecked(1, True)
            End Select

            Select Case akseschartpelunasanutang
                Case 0
                    cbchartlunasutang.Checked = False
                    clbchartlunasutang.SetItemChecked(0, False)
                    clbchartlunasutang.SetItemChecked(1, False)
                Case 1
                    cbchartlunasutang.Checked = True
                    clbchartlunasutang.SetItemChecked(0, True)
                    clbchartlunasutang.SetItemChecked(1, False)
                Case 3
                    cbchartlunasutang.Checked = True
                    clbchartlunasutang.SetItemChecked(0, False)
                    clbchartlunasutang.SetItemChecked(1, True)
                Case 4
                    cbchartlunasutang.Checked = True
                    clbchartlunasutang.SetItemChecked(0, True)
                    clbchartlunasutang.SetItemChecked(1, True)
            End Select

            Select Case akseschartpelunasanpiutang
                Case 0
                    cbchartlunaspiutang.Checked = False
                    clbchartlunaspiutang.SetItemChecked(0, False)
                    clbchartlunaspiutang.SetItemChecked(1, False)
                Case 1
                    cbchartlunaspiutang.Checked = True
                    clbchartlunaspiutang.SetItemChecked(0, True)
                    clbchartlunaspiutang.SetItemChecked(1, False)
                Case 3
                    cbchartlunaspiutang.Checked = True
                    clbchartlunaspiutang.SetItemChecked(0, False)
                    clbchartlunaspiutang.SetItemChecked(1, True)
                Case 4
                    cbchartlunaspiutang.Checked = True
                    clbchartlunaspiutang.SetItemChecked(0, True)
                    clbchartlunaspiutang.SetItemChecked(1, True)
            End Select

            Select Case akseschartakunmasuk
                Case 0
                    cbchartakunmasuk.Checked = False
                    clbchartakunmasuk.SetItemChecked(0, False)
                    clbchartakunmasuk.SetItemChecked(1, False)
                Case 1
                    cbchartakunmasuk.Checked = True
                    clbchartakunmasuk.SetItemChecked(0, True)
                    clbchartakunmasuk.SetItemChecked(1, False)
                Case 3
                    cbchartakunmasuk.Checked = True
                    clbchartakunmasuk.SetItemChecked(0, False)
                    clbchartakunmasuk.SetItemChecked(1, True)
                Case 4
                    cbchartakunmasuk.Checked = True
                    clbchartakunmasuk.SetItemChecked(0, True)
                    clbchartakunmasuk.SetItemChecked(1, True)
            End Select

            Select Case akseschartakunkeluar
                Case 0
                    cbchartakunkeluar.Checked = False
                    clbchartakunkeluar.SetItemChecked(0, False)
                    clbchartakunkeluar.SetItemChecked(1, False)
                Case 1
                    cbchartakunkeluar.Checked = True
                    clbchartakunkeluar.SetItemChecked(0, True)
                    clbchartakunkeluar.SetItemChecked(1, False)
                Case 3
                    cbchartakunkeluar.Checked = True
                    clbchartakunkeluar.SetItemChecked(0, False)
                    clbchartakunkeluar.SetItemChecked(1, True)
                Case 4
                    cbchartakunkeluar.Checked = True
                    clbchartakunkeluar.SetItemChecked(0, True)
                    clbchartakunkeluar.SetItemChecked(1, True)
            End Select

            '=============================================================================

            Select Case aksesfeaturekalkulasi
                Case 0
                    cbkalkulasiexpedisi.Checked = False
                Case 1
                    cbkalkulasiexpedisi.Checked = True
            End Select

            Select Case aksesfeaturebarcode
                Case 0
                    cbbarcodegenerator.Checked = False
                Case 1
                    cbbarcodegenerator.Checked = True
            End Select

            '=============================================================================

            Select Case aksessettinginfoperusahaan
                Case 0
                    cbinfoperusahaan.Checked = False
                Case 1
                    cbinfoperusahaan.Checked = True
            End Select


            Select Case aksessettingprinter
                Case 0
                    cbprinter.Checked = False
                Case 1
                    cbprinter.Checked = True
            End Select

            Select Case aksessettingbackupdatabase
                Case 0
                    cbbackupdatabase.Checked = False
                Case 1
                    cbbackupdatabase.Checked = True
            End Select


            Select Case aksessettingpengaturan
                Case 0
                    cbpengaturan.Checked = False
                Case 1
                    cbpengaturan.Checked = True
            End Select

            '=============================================================================

            '==batas case==

            'master
            clbmasterbarang.Enabled = False
            clbmasterkategori.Enabled = False
            clbmastergudang.Enabled = False
            clbmasterpelanggan.Enabled = False
            clbmastersupplier.Enabled = False
            clbmasteruser.Enabled = False
            clbmasterkas.Enabled = False
            clbmasterpricelist.Enabled = False
            clbmasterreksupp.Enabled = False
            clbmasterrekplng.Enabled = False

            'transaksi
            clbpembelian.Enabled = False
            clbpenjualan.Enabled = False
            clbreturbeli.Enabled = False
            clbreturjual.Enabled = False
            clbbarangmasuk.Enabled = False
            clbbarangkeluar.Enabled = False
            clbtransferbarang.Enabled = False
            clbpenyesuaianstok.Enabled = False

            'administrasi
            clblunasutang.Enabled = False
            clblunaspiutang.Enabled = False
            clbtransferkas.Enabled = False
            clbakunmasuk.Enabled = False
            clbakunkeluar.Enabled = False

            'laporan
            clblappricelist.Enabled = False
            clblappembelian.Enabled = False
            clblappenjualan.Enabled = False
            clblappenjualanpajak.Enabled = False

            clblapreturbeli.Enabled = False
            clblapreturjual.Enabled = False
            clblapbarangmasuk.Enabled = False
            clblapbarangkeluar.Enabled = False

            clblaptransferbarang.Enabled = False
            clblapstokbarang.Enabled = False
            clblaputang.Enabled = False
            clblappiutang.Enabled = False

            clblapakunmasuk.Enabled = False
            clblapakunkeluar.Enabled = False
            clblaptransferkas.Enabled = False
            clblaptransaksikas.Enabled = False

            clblapmodalbarang.Enabled = False
            clblapmutasibarang.Enabled = False
            clblappenyesuaianstok.Enabled = False
            clblaplabarugi.Enabled = False
            clblaprekapanharian.Enabled = False

            'chart
            clbchartpenjualan.Enabled = False
            clbchartpembelian.Enabled = False
            clbchartlunasutang.Enabled = False
            clbchartlunaspiutang.Enabled = False
            clbchartakunmasuk.Enabled = False
            clbchartakunkeluar.Enabled = False
            'end

            btnedit.Enabled = True
            btnbatal.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
            btntambah.Text = "Tambah"
        End If
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        Call cari()
    End Sub

    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    'master menu ==========================================================================================================================
    Private Sub cbmasterbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterbarang.CheckedChanged
        If cbmasterbarang.Checked = True Then
            clbmasterbarang.Enabled = True
            For id As Integer = 0 To clbmasterbarang.Items.Count - 1
                Me.clbmasterbarang.SetItemChecked(id, True)
            Next
        ElseIf cbmasterbarang.Checked = False Then
            clbmasterbarang.Enabled = False
            For id As Integer = 0 To clbmasterbarang.Items.Count - 1
                Me.clbmasterbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterkategori_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterkategori.CheckedChanged
        If cbmasterkategori.Checked = True Then
            clbmasterkategori.Enabled = True
            For id As Integer = 0 To clbmasterkategori.Items.Count - 1
                Me.clbmasterkategori.SetItemChecked(id, True)
            Next
        ElseIf cbmasterkategori.Checked = False Then
            clbmasterkategori.Enabled = False
            For id As Integer = 0 To clbmasterkategori.Items.Count - 1
                Me.clbmasterkategori.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastergudang_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastergudang.CheckedChanged
        If cbmastergudang.Checked = True Then
            clbmastergudang.Enabled = True
            For id As Integer = 0 To clbmastergudang.Items.Count - 1
                Me.clbmastergudang.SetItemChecked(id, True)
            Next
        ElseIf cbmastergudang.Checked = False Then
            clbmastergudang.Enabled = False
            For id As Integer = 0 To clbmastergudang.Items.Count - 1
                Me.clbmastergudang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterpelanggan_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterpelanggan.CheckedChanged
        If cbmasterpelanggan.Checked = True Then
            clbmasterpelanggan.Enabled = True
            For id As Integer = 0 To clbmasterpelanggan.Items.Count - 1
                Me.clbmasterpelanggan.SetItemChecked(id, True)
            Next
        ElseIf cbmasterpelanggan.Checked = False Then
            clbmasterpelanggan.Enabled = False
            For id As Integer = 0 To clbmasterpelanggan.Items.Count - 1
                Me.clbmasterpelanggan.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastersupplier_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastersupplier.CheckedChanged
        If cbmastersupplier.Checked = True Then
            clbmastersupplier.Enabled = True
            For id As Integer = 0 To clbmastersupplier.Items.Count - 1
                Me.clbmastersupplier.SetItemChecked(id, True)
            Next
        ElseIf cbmastersupplier.Checked = False Then
            clbmastersupplier.Enabled = False
            For id As Integer = 0 To clbmastersupplier.Items.Count - 1
                Me.clbmastersupplier.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call isitabel()
    End Sub

    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]" &
        "*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)

        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Private Sub txtemail_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtemail.Validating
        Dim email As String = txtemail.Text
        If EmailAddressCheck(email) = False Then
            txtemail.BackColor = Color.Red
        Else
            txtemail.BackColor = Color.White
        End If
    End Sub

    Private Sub cbmasteruser_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasteruser.CheckedChanged
        If cbmasteruser.Checked = True Then
            clbmasteruser.Enabled = True
            For id As Integer = 0 To clbmasteruser.Items.Count - 1
                Me.clbmasteruser.SetItemChecked(id, True)
            Next
        ElseIf cbmasteruser.Checked = False Then
            clbmasteruser.Enabled = False
            For id As Integer = 0 To clbmasteruser.Items.Count - 1
                Me.clbmasteruser.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterkas_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterkas.CheckedChanged
        If cbmasterkas.Checked = True Then
            clbmasterkas.Enabled = True
            For id As Integer = 0 To clbmasterkas.Items.Count - 1
                Me.clbmasterkas.SetItemChecked(id, True)
            Next
        ElseIf cbmasterkas.Checked = False Then
            clbmasterkas.Enabled = False
            For id As Integer = 0 To clbmasterkas.Items.Count - 1
                Me.clbmasterkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterpricelist_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterpricelist.CheckedChanged
        If cbmasterpricelist.Checked = True Then
            clbmasterpricelist.Enabled = True
            For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
                Me.clbmasterpricelist.SetItemChecked(id, True)
            Next
        ElseIf cbmasterpricelist.Checked = False Then
            clbmasterpricelist.Enabled = False
            For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
                Me.clbmasterpricelist.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterreksupp_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterreksupp.CheckedChanged
        If cbmasterreksupp.Checked = True Then
            clbmasterreksupp.Enabled = True
            For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
                Me.clbmasterreksupp.SetItemChecked(id, True)
            Next
        ElseIf cbmasterreksupp.Checked = False Then
            clbmasterreksupp.Enabled = False
            For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
                Me.clbmasterreksupp.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterrekplng_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterrekplng.CheckedChanged
        If cbmasterrekplng.Checked = True Then
            clbmasterrekplng.Enabled = True
            For id As Integer = 0 To clbmasterrekplng.Items.Count - 1
                Me.clbmasterrekplng.SetItemChecked(id, True)
            Next
        ElseIf cbmasterrekplng.Checked = False Then
            clbmasterrekplng.Enabled = False
            For id As Integer = 0 To clbmasterrekplng.Items.Count - 1
                Me.clbmasterrekplng.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'transaksi ============================================================================================================================
    Private Sub cbpembelian_CheckedChanged(sender As Object, e As EventArgs) Handles cbpembelian.CheckedChanged
        If cbpembelian.Checked = True Then
            clbpembelian.Enabled = True
            For id As Integer = 0 To clbpembelian.Items.Count - 1
                Me.clbpembelian.SetItemChecked(id, True)
            Next
        ElseIf cbpembelian.Checked = False Then
            clbpembelian.Enabled = False
            For id As Integer = 0 To clbpembelian.Items.Count - 1
                Me.clbpembelian.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cbpenjualan_CheckedChanged(sender As Object, e As EventArgs) Handles cbpenjualan.CheckedChanged
        If cbpenjualan.Checked = True Then
            clbpenjualan.Enabled = True
            For id As Integer = 0 To clbpenjualan.Items.Count - 1
                Me.clbpenjualan.SetItemChecked(id, True)
            Next
        ElseIf cbpenjualan.Checked = False Then
            clbpenjualan.Enabled = False
            For id As Integer = 0 To clbpenjualan.Items.Count - 1
                Me.clbpenjualan.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cbreturbeli_CheckedChanged(sender As Object, e As EventArgs) Handles cbreturbeli.CheckedChanged
        If cbreturbeli.Checked = True Then
            clbreturbeli.Enabled = True
            For id As Integer = 0 To clbreturbeli.Items.Count - 1
                Me.clbreturbeli.SetItemChecked(id, True)
            Next
        ElseIf cbreturbeli.Checked = False Then
            clbreturbeli.Enabled = False
            For id As Integer = 0 To clbreturbeli.Items.Count - 1
                Me.clbreturbeli.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbreturjual_CheckedChanged(sender As Object, e As EventArgs) Handles cbreturjual.CheckedChanged
        If cbreturjual.Checked = True Then
            clbreturjual.Enabled = True
            For id As Integer = 0 To clbreturjual.Items.Count - 1
                Me.clbreturjual.SetItemChecked(id, True)
            Next
        ElseIf cbreturjual.Checked = False Then
            clbreturjual.Enabled = False
            For id As Integer = 0 To clbreturjual.Items.Count - 1
                Me.clbreturjual.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbbarangmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cbbarangmasuk.CheckedChanged
        If cbbarangmasuk.Checked = True Then
            clbbarangmasuk.Enabled = True
            For id As Integer = 0 To clbbarangmasuk.Items.Count - 1
                Me.clbbarangmasuk.SetItemChecked(id, True)
            Next
        ElseIf cbbarangmasuk.Checked = False Then
            clbbarangmasuk.Enabled = False
            For id As Integer = 0 To clbbarangmasuk.Items.Count - 1
                Me.clbbarangmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbbarangkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cbbarangkeluar.CheckedChanged
        If cbbarangkeluar.Checked = True Then
            clbbarangkeluar.Enabled = True
            For id As Integer = 0 To clbbarangkeluar.Items.Count - 1
                Me.clbbarangkeluar.SetItemChecked(id, True)
            Next
        ElseIf cbbarangkeluar.Checked = False Then
            clbbarangkeluar.Enabled = False
            For id As Integer = 0 To clbbarangkeluar.Items.Count - 1
                Me.clbbarangkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbtransferbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cbtransferbarang.CheckedChanged
        If cbtransferbarang.Checked = True Then
            clbtransferbarang.Enabled = True
            For id As Integer = 0 To clbtransferbarang.Items.Count - 1
                Me.clbtransferbarang.SetItemChecked(id, True)
            Next
        ElseIf cbtransferbarang.Checked = False Then
            clbtransferbarang.Enabled = False
            For id As Integer = 0 To clbtransferbarang.Items.Count - 1
                Me.clbtransferbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbpenyesuaianstok_CheckedChanged(sender As Object, e As EventArgs) Handles cbpenyesuaianstok.CheckedChanged
        If cbpenyesuaianstok.Checked = True Then
            clbpenyesuaianstok.Enabled = True
            For id As Integer = 0 To clbpenyesuaianstok.Items.Count - 1
                Me.clbpenyesuaianstok.SetItemChecked(id, True)
            Next
        ElseIf cbpenyesuaianstok.Checked = False Then
            clbpenyesuaianstok.Enabled = False
            For id As Integer = 0 To clbpenyesuaianstok.Items.Count - 1
                Me.clbpenyesuaianstok.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'administrasi ==========================================================================================================================
    Private Sub cblunasutang_CheckedChanged(sender As Object, e As EventArgs) Handles cblunasutang.CheckedChanged
        If cblunasutang.Checked = True Then
            clblunasutang.Enabled = True
            For id As Integer = 0 To clblunasutang.Items.Count - 1
                Me.clblunasutang.SetItemChecked(id, True)
            Next
        ElseIf cblunasutang.Checked = False Then
            clblunasutang.Enabled = False
            For id As Integer = 0 To clblunasutang.Items.Count - 1
                Me.clblunasutang.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblunaspiutang_CheckedChanged(sender As Object, e As EventArgs) Handles cblunaspiutang.CheckedChanged
        If cblunaspiutang.Checked = True Then
            clblunaspiutang.Enabled = True
            For id As Integer = 0 To clblunaspiutang.Items.Count - 1
                Me.clblunaspiutang.SetItemChecked(id, True)
            Next
        ElseIf cblunaspiutang.Checked = False Then
            clblunaspiutang.Enabled = False
            For id As Integer = 0 To clblunaspiutang.Items.Count - 1
                Me.clblunaspiutang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbakunmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cbakunmasuk.CheckedChanged
        If cbakunmasuk.Checked = True Then
            clbakunmasuk.Enabled = True
            For id As Integer = 0 To clbakunmasuk.Items.Count - 1
                Me.clbakunmasuk.SetItemChecked(id, True)
            Next
        ElseIf cbakunmasuk.Checked = False Then
            clbakunmasuk.Enabled = False
            For id As Integer = 0 To clbakunmasuk.Items.Count - 1
                Me.clbakunmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbakunkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cbakunkeluar.CheckedChanged
        If cbakunkeluar.Checked = True Then
            clbakunkeluar.Enabled = True
            For id As Integer = 0 To clbakunkeluar.Items.Count - 1
                Me.clbakunkeluar.SetItemChecked(id, True)
            Next
        ElseIf cbakunkeluar.Checked = False Then
            clbakunkeluar.Enabled = False
            For id As Integer = 0 To clbakunkeluar.Items.Count - 1
                Me.clbakunkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cbtransferkas_CheckedChanged(sender As Object, e As EventArgs) Handles cbtransferkas.CheckedChanged
        If cbtransferkas.Checked = True Then
            clbtransferkas.Enabled = True
            For id As Integer = 0 To clbtransferkas.Items.Count - 1
                Me.clbtransferkas.SetItemChecked(id, True)
            Next
        ElseIf cbtransferkas.Checked = False Then
            clbtransferkas.Enabled = False
            For id As Integer = 0 To clbtransferkas.Items.Count - 1
                Me.clbtransferkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'laporan ==============================================================================================================================
    Private Sub cblappricelist_CheckedChanged(sender As Object, e As EventArgs) Handles cblappricelist.CheckedChanged
        If cblappricelist.Checked = True Then
            clblappricelist.Enabled = True
            For id As Integer = 0 To clblappricelist.Items.Count - 1
                Me.clblappricelist.SetItemChecked(id, True)
            Next
        ElseIf cblappricelist.Checked = False Then
            clblappricelist.Enabled = False
            For id As Integer = 0 To clblappricelist.Items.Count - 1
                Me.clblappricelist.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblappembelian_CheckedChanged(sender As Object, e As EventArgs) Handles cblappembelian.CheckedChanged
        If cblappembelian.Checked = True Then
            clblappembelian.Enabled = True
            For id As Integer = 0 To clblappembelian.Items.Count - 1
                Me.clblappembelian.SetItemChecked(id, True)
            Next
        ElseIf cblappembelian.Checked = False Then
            clblappembelian.Enabled = False
            For id As Integer = 0 To clblappembelian.Items.Count - 1
                Me.clblappembelian.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblappenjualan_CheckedChanged(sender As Object, e As EventArgs) Handles cblappenjualan.CheckedChanged
        If cblappenjualan.Checked = True Then
            clblappenjualan.Enabled = True
            For id As Integer = 0 To clblappenjualan.Items.Count - 1
                Me.clblappenjualan.SetItemChecked(id, True)
            Next
        ElseIf cblappenjualan.Checked = False Then
            clblappenjualan.Enabled = False
            For id As Integer = 0 To clblappenjualan.Items.Count - 1
                Me.clblappenjualan.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblappenjualanpajak_CheckedChanged(sender As Object, e As EventArgs) Handles cblappenjualanpajak.CheckedChanged
        If cblappenjualanpajak.Checked = True Then
            clblappenjualanpajak.Enabled = True
            For id As Integer = 0 To clblappenjualanpajak.Items.Count - 1
                Me.clblappenjualanpajak.SetItemChecked(id, True)
            Next
        ElseIf cblappenjualanpajak.Checked = False Then
            clblappenjualanpajak.Enabled = False
            For id As Integer = 0 To clblappenjualanpajak.Items.Count - 1
                Me.clblappenjualanpajak.SetItemChecked(id, False)
            Next
        End If
    End Sub

    '======================================================================================================================================

    Private Sub cblapreturbeli_CheckedChanged(sender As Object, e As EventArgs) Handles cblapreturbeli.CheckedChanged
        If cblapreturbeli.Checked = True Then
            clblapreturbeli.Enabled = True
            For id As Integer = 0 To clblapreturbeli.Items.Count - 1
                Me.clblapreturbeli.SetItemChecked(id, True)
            Next
        ElseIf cblapreturbeli.Checked = False Then
            clblapreturbeli.Enabled = False
            For id As Integer = 0 To clblapreturbeli.Items.Count - 1
                Me.clblapreturbeli.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapreturjual_CheckedChanged(sender As Object, e As EventArgs) Handles cblapreturjual.CheckedChanged
        If cblapreturjual.Checked = True Then
            clblapreturjual.Enabled = True
            For id As Integer = 0 To clblapreturjual.Items.Count - 1
                Me.clblapreturjual.SetItemChecked(id, True)
            Next
        ElseIf cblapreturjual.Checked = False Then
            clblapreturjual.Enabled = False
            For id As Integer = 0 To clblapreturjual.Items.Count - 1
                Me.clblapreturjual.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapbarangmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cblapbarangmasuk.CheckedChanged
        If cblapbarangmasuk.Checked = True Then
            clblapbarangmasuk.Enabled = True
            For id As Integer = 0 To clblapbarangmasuk.Items.Count - 1
                Me.clblapbarangmasuk.SetItemChecked(id, True)
            Next
        ElseIf cblapbarangmasuk.Checked = False Then
            clblapbarangmasuk.Enabled = False
            For id As Integer = 0 To clblapbarangmasuk.Items.Count - 1
                Me.clblapbarangmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapbarangkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cblapbarangkeluar.CheckedChanged
        If cblapbarangkeluar.Checked = True Then
            clblapbarangkeluar.Enabled = True
            For id As Integer = 0 To clblapbarangkeluar.Items.Count - 1
                Me.clblapbarangkeluar.SetItemChecked(id, True)
            Next
        ElseIf cblapbarangkeluar.Checked = False Then
            clblapbarangkeluar.Enabled = False
            For id As Integer = 0 To clblapbarangkeluar.Items.Count - 1
                Me.clblapbarangkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub

    '======================================================================================================================================
    Private Sub cblaptransferbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cblaptransferbarang.CheckedChanged
        If cblaptransferbarang.Checked = True Then
            clblaptransferbarang.Enabled = True
            For id As Integer = 0 To clblaptransferbarang.Items.Count - 1
                Me.clblaptransferbarang.SetItemChecked(id, True)
            Next
        ElseIf cblaptransferbarang.Checked = False Then
            clblaptransferbarang.Enabled = False
            For id As Integer = 0 To clblaptransferbarang.Items.Count - 1
                Me.clblaptransferbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapstokbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cblapstokbarang.CheckedChanged
        If cblapstokbarang.Checked = True Then
            clblapstokbarang.Enabled = True
            For id As Integer = 0 To clblapstokbarang.Items.Count - 1
                Me.clblapstokbarang.SetItemChecked(id, True)
            Next
        ElseIf cblapstokbarang.Checked = False Then
            clblapstokbarang.Enabled = False
            For id As Integer = 0 To clblapstokbarang.Items.Count - 1
                Me.clblapstokbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaputang_CheckedChanged(sender As Object, e As EventArgs) Handles cblaputang.CheckedChanged
        If cblaputang.Checked = True Then
            clblaputang.Enabled = True
            For id As Integer = 0 To clblaputang.Items.Count - 1
                Me.clblaputang.SetItemChecked(id, True)
            Next
        ElseIf cblaputang.Checked = False Then
            clblaputang.Enabled = False
            For id As Integer = 0 To clblaputang.Items.Count - 1
                Me.clblaputang.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblappiutang_CheckedChanged(sender As Object, e As EventArgs) Handles cblappiutang.CheckedChanged
        If cblappiutang.Checked = True Then
            clblappiutang.Enabled = True
            For id As Integer = 0 To clblappiutang.Items.Count - 1
                Me.clblappiutang.SetItemChecked(id, True)
            Next
        ElseIf cblappiutang.Checked = False Then
            clblappiutang.Enabled = False
            For id As Integer = 0 To clblappiutang.Items.Count - 1
                Me.clblappiutang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    '======================================================================================================================================

    Private Sub cblapakunmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cblapakunmasuk.CheckedChanged
        If cblapakunmasuk.Checked = True Then
            clblapakunmasuk.Enabled = True
            For id As Integer = 0 To clblapakunmasuk.Items.Count - 1
                Me.clblapakunmasuk.SetItemChecked(id, True)
            Next
        ElseIf cblapakunmasuk.Checked = False Then
            clblapakunmasuk.Enabled = False
            For id As Integer = 0 To clblapakunmasuk.Items.Count - 1
                Me.clblapakunmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblapakunkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cblapakunkeluar.CheckedChanged
        If cblapakunkeluar.Checked = True Then
            clblapakunkeluar.Enabled = True
            For id As Integer = 0 To clblapakunkeluar.Items.Count - 1
                Me.clblapakunkeluar.SetItemChecked(id, True)
            Next
        ElseIf cblapakunkeluar.Checked = False Then
            clblapakunkeluar.Enabled = False
            For id As Integer = 0 To clblapakunkeluar.Items.Count - 1
                Me.clblapakunkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub
    Private Sub cblaptransferkas_CheckedChanged(sender As Object, e As EventArgs) Handles cblaptransferkas.CheckedChanged
        If cblaptransferkas.Checked = True Then
            clblaptransferkas.Enabled = True
            For id As Integer = 0 To clblaptransferkas.Items.Count - 1
                Me.clblaptransferkas.SetItemChecked(id, True)
            Next
        ElseIf cblaptransferkas.Checked = False Then
            clblaptransferkas.Enabled = False
            For id As Integer = 0 To clblaptransferkas.Items.Count - 1
                Me.clblaptransferkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaptransaksikas_CheckedChanged(sender As Object, e As EventArgs) Handles cblaptransaksikas.CheckedChanged
        If cblaptransaksikas.Checked = True Then
            clblaptransaksikas.Enabled = True
            For id As Integer = 0 To clblaptransaksikas.Items.Count - 1
                Me.clblaptransaksikas.SetItemChecked(id, True)
            Next
        ElseIf cblaptransaksikas.Checked = False Then
            clblaptransaksikas.Enabled = False
            For id As Integer = 0 To clblaptransaksikas.Items.Count - 1
                Me.clblaptransaksikas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    '======================================================================================================================================
    Private Sub cblapmodalbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cblapmodalbarang.CheckedChanged
        If cblapmodalbarang.Checked = True Then
            clblapmodalbarang.Enabled = True
            For id As Integer = 0 To clblapmodalbarang.Items.Count - 1
                Me.clblapmodalbarang.SetItemChecked(id, True)
            Next
        ElseIf cblapmodalbarang.Checked = False Then
            clblapmodalbarang.Enabled = False
            For id As Integer = 0 To clblapmodalbarang.Items.Count - 1
                Me.clblapmodalbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblapmutasibarang_CheckedChanged(sender As Object, e As EventArgs) Handles cblapmutasibarang.CheckedChanged
        If cblapmutasibarang.Checked = True Then
            clblapmutasibarang.Enabled = True
            For id As Integer = 0 To clblapmutasibarang.Items.Count - 1
                Me.clblapmutasibarang.SetItemChecked(id, True)
            Next
        ElseIf cblapmutasibarang.Checked = False Then
            clblapmutasibarang.Enabled = False
            For id As Integer = 0 To clblapmutasibarang.Items.Count - 1
                Me.clblapmutasibarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblappenyesuaianstok_CheckedChanged(sender As Object, e As EventArgs) Handles cblappenyesuaianstok.CheckedChanged
        If cblappenyesuaianstok.Checked = True Then
            clblappenyesuaianstok.Enabled = True
            For id As Integer = 0 To clblappenyesuaianstok.Items.Count - 1
                Me.clblappenyesuaianstok.SetItemChecked(id, True)
            Next
        ElseIf cblappenyesuaianstok.Checked = False Then
            clblappenyesuaianstok.Enabled = False
            For id As Integer = 0 To clblappenyesuaianstok.Items.Count - 1
                Me.clblappenyesuaianstok.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaplabarugi_CheckedChanged(sender As Object, e As EventArgs) Handles cblaplabarugi.CheckedChanged
        If cblaplabarugi.Checked = True Then
            clblaplabarugi.Enabled = True
            For id As Integer = 0 To clblaplabarugi.Items.Count - 1
                Me.clblaplabarugi.SetItemChecked(id, True)
            Next
        ElseIf cblaplabarugi.Checked = False Then
            clblaplabarugi.Enabled = False
            For id As Integer = 0 To clblaplabarugi.Items.Count - 1
                Me.clblaplabarugi.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cblaprekapanharian_CheckedChanged(sender As Object, e As EventArgs) Handles cblaprekapanharian.CheckedChanged
        If cblaprekapanharian.Checked = True Then
            clblaprekapanharian.Enabled = True
            For id As Integer = 0 To clblaprekapanharian.Items.Count - 1
                Me.clblaprekapanharian.SetItemChecked(id, True)
            Next
        ElseIf cblaprekapanharian.Checked = False Then
            clblaprekapanharian.Enabled = False
            For id As Integer = 0 To clblaprekapanharian.Items.Count - 1
                Me.clblaprekapanharian.SetItemChecked(id, False)
            Next
        End If
    End Sub

    '======================================================================================================================================

    Private Sub cbchartpembelian_CheckedChanged(sender As Object, e As EventArgs) Handles cbchartpembelian.CheckedChanged
        If cbchartpembelian.Checked = True Then
            clbchartpembelian.Enabled = True
            For id As Integer = 0 To clbchartpembelian.Items.Count - 1
                Me.clbchartpembelian.SetItemChecked(id, True)
            Next
        ElseIf cbchartpembelian.Checked = False Then
            clbchartpembelian.Enabled = False
            For id As Integer = 0 To clbchartpembelian.Items.Count - 1
                Me.clbchartpembelian.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbchartpenjualan_CheckedChanged(sender As Object, e As EventArgs) Handles cbchartpenjualan.CheckedChanged
        If cbchartpenjualan.Checked = True Then
            clbchartpenjualan.Enabled = True
            For id As Integer = 0 To clbchartpenjualan.Items.Count - 1
                Me.clbchartpenjualan.SetItemChecked(id, True)
            Next
        ElseIf cbchartpenjualan.Checked = False Then
            clbchartpenjualan.Enabled = False
            For id As Integer = 0 To clbchartpenjualan.Items.Count - 1
                Me.clbchartpenjualan.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbchartlunasutang_CheckedChanged(sender As Object, e As EventArgs) Handles cbchartlunasutang.CheckedChanged
        If cbchartlunasutang.Checked = True Then
            clbchartlunasutang.Enabled = True
            For id As Integer = 0 To clbchartlunasutang.Items.Count - 1
                Me.clbchartlunasutang.SetItemChecked(id, True)
            Next
        ElseIf cbchartlunasutang.Checked = False Then
            clbchartlunasutang.Enabled = False
            For id As Integer = 0 To clbchartlunasutang.Items.Count - 1
                Me.clbchartlunasutang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbchartlunaspiutang_CheckedChanged(sender As Object, e As EventArgs) Handles cbchartlunaspiutang.CheckedChanged
        If cbchartlunaspiutang.Checked = True Then
            clbchartlunaspiutang.Enabled = True
            For id As Integer = 0 To clbchartlunaspiutang.Items.Count - 1
                Me.clbchartlunaspiutang.SetItemChecked(id, True)
            Next
        ElseIf cbchartlunaspiutang.Checked = False Then
            clbchartlunaspiutang.Enabled = False
            For id As Integer = 0 To clbchartlunaspiutang.Items.Count - 1
                Me.clbchartlunaspiutang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbchartakunmasuk_CheckedChanged(sender As Object, e As EventArgs) Handles cbchartakunmasuk.CheckedChanged
        If cbchartakunmasuk.Checked = True Then
            clbchartakunmasuk.Enabled = True
            For id As Integer = 0 To clbchartakunmasuk.Items.Count - 1
                Me.clbchartakunmasuk.SetItemChecked(id, True)
            Next
        ElseIf cbchartakunmasuk.Checked = False Then
            clbchartakunmasuk.Enabled = False
            For id As Integer = 0 To clbchartakunmasuk.Items.Count - 1
                Me.clbchartakunmasuk.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbchartakunkeluar_CheckedChanged(sender As Object, e As EventArgs) Handles cbchartakunkeluar.CheckedChanged
        If cbchartakunkeluar.Checked = True Then
            clbchartakunkeluar.Enabled = True
            For id As Integer = 0 To clbchartakunkeluar.Items.Count - 1
                Me.clbchartakunkeluar.SetItemChecked(id, True)
            Next
        ElseIf cbchartakunkeluar.Checked = False Then
            clbchartakunkeluar.Enabled = False
            For id As Integer = 0 To clbchartakunkeluar.Items.Count - 1
                Me.clbchartakunkeluar.SetItemChecked(id, False)
            Next
        End If
    End Sub

    'combobox list
    'master ===============================================================================================================================
    Private Sub clbmasterbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterbarang.MouseDown
        Dim Index As Integer = clbmasterbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterbarang.SetItemChecked(Index, Not clbmasterbarang.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterbarang.Items.Count - 1
            If clbmasterbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterbarang.Enabled = False
            cbmasterbarang.Checked = False
        End If
    End Sub

    Private Sub clbmasterkategori_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterkategori.MouseDown
        Dim Index As Integer = clbmasterkategori.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterkategori.SetItemChecked(Index, Not clbmasterkategori.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterkategori.Items.Count - 1
            If clbmasterkategori.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterkategori.Enabled = False
            cbmasterkategori.Checked = False
        End If
    End Sub

    Private Sub clbmastergudang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastergudang.MouseDown
        Dim Index As Integer = clbmastergudang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastergudang.SetItemChecked(Index, Not clbmastergudang.GetItemChecked(Index))

        For id As Integer = 0 To clbmastergudang.Items.Count - 1
            If clbmastergudang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastergudang.Enabled = False
            cbmastergudang.Checked = False
        End If
    End Sub

    Private Sub clbmasterpelanggan_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterpelanggan.MouseDown
        Dim Index As Integer = clbmasterpelanggan.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterpelanggan.SetItemChecked(Index, Not clbmasterpelanggan.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterpelanggan.Items.Count - 1
            If clbmasterpelanggan.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterpelanggan.Enabled = False
            cbmasterpelanggan.Checked = False
        End If
    End Sub

    Private Sub clbmastersupplier_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastersupplier.MouseDown
        Dim Index As Integer = clbmastersupplier.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastersupplier.SetItemChecked(Index, Not clbmastersupplier.GetItemChecked(Index))

        For id As Integer = 0 To clbmastersupplier.Items.Count - 1
            If clbmastersupplier.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastersupplier.Enabled = False
            cbmastersupplier.Checked = False
        End If
    End Sub

    Private Sub clbmasteruser_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasteruser.MouseDown
        Dim Index As Integer = clbmasteruser.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasteruser.SetItemChecked(Index, Not clbmasteruser.GetItemChecked(Index))

        For id As Integer = 0 To clbmasteruser.Items.Count - 1
            If clbmasteruser.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasteruser.Enabled = False
            cbmasteruser.Checked = False
        End If
    End Sub

    Private Sub clbmasterkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterkas.MouseDown
        Dim Index As Integer = clbmasterkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterkas.SetItemChecked(Index, Not clbmasterkas.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterkas.Items.Count - 1
            If clbmasterkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterkas.Enabled = False
            cbmasterkas.Checked = False
        End If
    End Sub

    Private Sub clbmasterpricelist_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterpricelist.MouseDown
        Dim Index As Integer = clbmasterpricelist.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterpricelist.SetItemChecked(Index, Not clbmasterpricelist.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
            If clbmasterpricelist.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterpricelist.Enabled = False
            cbmasterpricelist.Checked = False
        End If
    End Sub

    Private Sub clbmasterreksupp_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterreksupp.MouseDown
        Dim Index As Integer = clbmasterreksupp.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterreksupp.SetItemChecked(Index, Not clbmasterreksupp.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
            If clbmasterreksupp.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterreksupp.Enabled = False
            cbmasterreksupp.Checked = False
        End If
    End Sub

    Private Sub clbmasterrekcust_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterrekplng.MouseDown
        Dim Index As Integer = clbmasterrekplng.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterrekplng.SetItemChecked(Index, Not clbmasterrekplng.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterrekplng.Items.Count - 1
            If clbmasterrekplng.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterrekplng.Enabled = False
            cbmasterrekplng.Checked = False
        End If
    End Sub

    'transaksi ============================================================================================================================
    Private Sub clbpembelian_MouseDown(sender As Object, e As MouseEventArgs) Handles clbpembelian.MouseDown
        Dim Index As Integer = clbpembelian.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbpembelian.SetItemChecked(Index, Not clbpembelian.GetItemChecked(Index))

        For id As Integer = 0 To clbpembelian.Items.Count - 1
            If clbpembelian.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbpembelian.Enabled = False
            cbpembelian.Checked = False
        End If
    End Sub

    Private Sub clbpenjualan_MouseDown(sender As Object, e As MouseEventArgs) Handles clbpenjualan.MouseDown
        Dim Index As Integer = clbpenjualan.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbpenjualan.SetItemChecked(Index, Not clbpenjualan.GetItemChecked(Index))

        For id As Integer = 0 To clbpenjualan.Items.Count - 1
            If clbpenjualan.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbpenjualan.Enabled = False
            cbpenjualan.Checked = False
        End If
    End Sub

    Private Sub clbreturbeli_MouseDown(sender As Object, e As MouseEventArgs) Handles clbreturbeli.MouseDown
        Dim Index As Integer = clbreturbeli.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbreturbeli.SetItemChecked(Index, Not clbreturbeli.GetItemChecked(Index))

        For id As Integer = 0 To clbreturbeli.Items.Count - 1
            If clbreturbeli.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbreturbeli.Enabled = False
            cbreturbeli.Checked = False
        End If
    End Sub

    Private Sub clbreturjual_MouseDown(sender As Object, e As MouseEventArgs) Handles clbreturjual.MouseDown
        Dim Index As Integer = clbreturjual.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbreturjual.SetItemChecked(Index, Not clbreturjual.GetItemChecked(Index))

        For id As Integer = 0 To clbreturjual.Items.Count - 1
            If clbreturjual.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbreturjual.Enabled = False
            cbreturjual.Checked = False
        End If
    End Sub

    Private Sub clbbarangmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clbbarangmasuk.MouseDown
        Dim Index As Integer = clbbarangmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbbarangmasuk.SetItemChecked(Index, Not clbbarangmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clbbarangmasuk.Items.Count - 1
            If clbbarangmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbbarangmasuk.Enabled = False
            cbbarangmasuk.Checked = False
        End If
    End Sub

    Private Sub clbbarangkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clbbarangkeluar.MouseDown
        Dim Index As Integer = clbbarangkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbbarangkeluar.SetItemChecked(Index, Not clbbarangkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clbbarangkeluar.Items.Count - 1
            If clbbarangkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbbarangkeluar.Enabled = False
            cbbarangkeluar.Checked = False
        End If
    End Sub

    Private Sub clbtransferbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbtransferbarang.MouseDown
        Dim Index As Integer = clbtransferbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbtransferbarang.SetItemChecked(Index, Not clbtransferbarang.GetItemChecked(Index))

        For id As Integer = 0 To clbtransferbarang.Items.Count - 1
            If clbtransferbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbtransferbarang.Enabled = False
            cbtransferbarang.Checked = False
        End If
    End Sub

    Private Sub clbpenyesuaianstok_MouseDown(sender As Object, e As MouseEventArgs) Handles clbpenyesuaianstok.MouseDown
        Dim Index As Integer = clbpenyesuaianstok.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbpenyesuaianstok.SetItemChecked(Index, Not clbpenyesuaianstok.GetItemChecked(Index))

        For id As Integer = 0 To clbpenyesuaianstok.Items.Count - 1
            If clbpenyesuaianstok.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbpenyesuaianstok.Enabled = False
            cbpenyesuaianstok.Checked = False
        End If
    End Sub

    'administrasi =========================================================================================================================

    Private Sub clblunasutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblunasutang.MouseDown
        Dim Index As Integer = clblunasutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblunasutang.SetItemChecked(Index, Not clblunasutang.GetItemChecked(Index))

        For id As Integer = 0 To clblunasutang.Items.Count - 1
            If clblunasutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblunasutang.Enabled = False
            cblunasutang.Checked = False
        End If
    End Sub

    Private Sub clblunaspiutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblunaspiutang.MouseDown
        Dim Index As Integer = clblunaspiutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblunaspiutang.SetItemChecked(Index, Not clblunaspiutang.GetItemChecked(Index))

        For id As Integer = 0 To clblunaspiutang.Items.Count - 1
            If clblunaspiutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblunaspiutang.Enabled = False
            cblunaspiutang.Checked = False
        End If
    End Sub

    Private Sub clbtransferkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clbtransferkas.MouseDown
        Dim Index As Integer = clbtransferkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbtransferkas.SetItemChecked(Index, Not clbtransferkas.GetItemChecked(Index))

        For id As Integer = 0 To clbtransferkas.Items.Count - 1
            If clbtransferkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbtransferkas.Enabled = False
            cbtransferkas.Checked = False
        End If
    End Sub

    Private Sub clbakunmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clbakunmasuk.MouseDown
        Dim Index As Integer = clbakunmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbakunmasuk.SetItemChecked(Index, Not clbakunmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clbakunmasuk.Items.Count - 1
            If clbakunmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbakunmasuk.Enabled = False
            cbakunmasuk.Checked = False
        End If
    End Sub

    Private Sub clbakunkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clbakunkeluar.MouseDown
        Dim Index As Integer = clbakunkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbakunkeluar.SetItemChecked(Index, Not clbakunkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clbakunkeluar.Items.Count - 1
            If clbakunkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbakunkeluar.Enabled = False
            cbakunkeluar.Checked = False
        End If
    End Sub

    'laporan ==============================================================================================================================
    Private Sub clblappricelist_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappricelist.MouseDown
        Dim Index As Integer = clblappricelist.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappricelist.SetItemChecked(Index, Not clblappricelist.GetItemChecked(Index))

        For id As Integer = 0 To clblappricelist.Items.Count - 1
            If clblappricelist.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappricelist.Enabled = False
            cblappricelist.Checked = False
        End If
    End Sub
    Private Sub clblappembelian_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappembelian.MouseDown
        Dim Index As Integer = clblappembelian.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappembelian.SetItemChecked(Index, Not clblappembelian.GetItemChecked(Index))

        For id As Integer = 0 To clblappembelian.Items.Count - 1
            If clblappembelian.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappembelian.Enabled = False
            cblappembelian.Checked = False
        End If
    End Sub

    Private Sub clblappenjualan_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappenjualan.MouseDown
        Dim Index As Integer = clblappenjualan.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappenjualan.SetItemChecked(Index, Not clblappenjualan.GetItemChecked(Index))

        For id As Integer = 0 To clblappenjualan.Items.Count - 1
            If clblappenjualan.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappenjualan.Enabled = False
            cblappenjualan.Checked = False
        End If
    End Sub

    Private Sub clblappenjualanpajak_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappenjualanpajak.MouseDown
        Dim Index As Integer = clblappenjualanpajak.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappenjualanpajak.SetItemChecked(Index, Not clblappenjualanpajak.GetItemChecked(Index))

        For id As Integer = 0 To clblappenjualanpajak.Items.Count - 1
            If clblappenjualanpajak.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappenjualanpajak.Enabled = False
            cblappenjualanpajak.Checked = False
        End If
    End Sub

    '======================================================================================================================================

    Private Sub clblapreturbeli_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapreturbeli.MouseDown
        Dim Index As Integer = clblapreturbeli.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapreturbeli.SetItemChecked(Index, Not clblapreturbeli.GetItemChecked(Index))

        For id As Integer = 0 To clblapreturbeli.Items.Count - 1
            If clblapreturbeli.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapreturbeli.Enabled = False
            cblapreturbeli.Checked = False
        End If
    End Sub

    Private Sub clblapreturjual_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapreturjual.MouseDown
        Dim Index As Integer = clblapreturjual.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapreturjual.SetItemChecked(Index, Not clblapreturjual.GetItemChecked(Index))

        For id As Integer = 0 To clblapreturjual.Items.Count - 1
            If clblapreturjual.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapreturjual.Enabled = False
            cblapreturjual.Checked = False
        End If
    End Sub

    Private Sub clblapbarangmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapbarangmasuk.MouseDown
        Dim Index As Integer = clblapbarangmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapbarangmasuk.SetItemChecked(Index, Not clblapbarangmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clblapbarangmasuk.Items.Count - 1
            If clblapbarangmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapbarangmasuk.Enabled = False
            cblapbarangmasuk.Checked = False
        End If
    End Sub

    Private Sub clblapbarangkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapbarangkeluar.MouseDown
        Dim Index As Integer = clblapbarangkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapbarangkeluar.SetItemChecked(Index, Not clblapbarangkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clblapbarangkeluar.Items.Count - 1
            If clblapbarangkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapbarangkeluar.Enabled = False
            cblapbarangkeluar.Checked = False
        End If
    End Sub

    '======================================================================================================================================

    Private Sub clblaptransferbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaptransferbarang.MouseDown
        Dim Index As Integer = clblaptransferbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaptransferbarang.SetItemChecked(Index, Not clblaptransferbarang.GetItemChecked(Index))

        For id As Integer = 0 To clblaptransferbarang.Items.Count - 1
            If clblaptransferbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaptransferbarang.Enabled = False
            cblaptransferbarang.Checked = False
        End If
    End Sub
    Private Sub clblapstokbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapstokbarang.MouseDown
        Dim Index As Integer = clblapstokbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapstokbarang.SetItemChecked(Index, Not clblapstokbarang.GetItemChecked(Index))

        For id As Integer = 0 To clblapstokbarang.Items.Count - 1
            If clblapstokbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapstokbarang.Enabled = False
            cblapstokbarang.Checked = False
        End If
    End Sub
    Private Sub clblaputang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaputang.MouseDown
        Dim Index As Integer = clblaputang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaputang.SetItemChecked(Index, Not clblaputang.GetItemChecked(Index))

        For id As Integer = 0 To clblaputang.Items.Count - 1
            If clblaputang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaputang.Enabled = False
            cblaputang.Checked = False
        End If
    End Sub

    Private Sub clblappiutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappiutang.MouseDown
        Dim Index As Integer = clblappiutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappiutang.SetItemChecked(Index, Not clblappiutang.GetItemChecked(Index))

        For id As Integer = 0 To clblappiutang.Items.Count - 1
            If clblappiutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappiutang.Enabled = False
            cblappiutang.Checked = False
        End If
    End Sub

    '======================================================================================================================================

    Private Sub clblapakunmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapakunmasuk.MouseDown
        Dim Index As Integer = clblapakunmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapakunmasuk.SetItemChecked(Index, Not clblapakunmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clblapakunmasuk.Items.Count - 1
            If clblapakunmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapakunmasuk.Enabled = False
            cblapakunmasuk.Checked = False
        End If
    End Sub

    Private Sub clblapakunkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapakunkeluar.MouseDown
        Dim Index As Integer = clblapakunkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapakunkeluar.SetItemChecked(Index, Not clblapakunkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clblapakunkeluar.Items.Count - 1
            If clblapakunkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapakunkeluar.Enabled = False
            cblapakunkeluar.Checked = False
        End If
    End Sub

    Private Sub clblaptransferkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaptransferkas.MouseDown
        Dim Index As Integer = clblaptransferkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaptransferkas.SetItemChecked(Index, Not clblaptransferkas.GetItemChecked(Index))

        For id As Integer = 0 To clblaptransferkas.Items.Count - 1
            If clblaptransferkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaptransferkas.Enabled = False
            cblaptransferkas.Checked = False
        End If
    End Sub

    Private Sub clblaptransaksikas_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaptransaksikas.MouseDown
        Dim Index As Integer = clblaptransaksikas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaptransaksikas.SetItemChecked(Index, Not clblaptransaksikas.GetItemChecked(Index))

        For id As Integer = 0 To clblaptransaksikas.Items.Count - 1
            If clblaptransaksikas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaptransaksikas.Enabled = False
            cblaptransaksikas.Checked = False
        End If
    End Sub

    '======================================================================================================================================

    Private Sub clblapmodalbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapmodalbarang.MouseDown
        Dim Index As Integer = clblapmodalbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapmodalbarang.SetItemChecked(Index, Not clblapmodalbarang.GetItemChecked(Index))

        For id As Integer = 0 To clblapmodalbarang.Items.Count - 1
            If clblapmodalbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapmodalbarang.Enabled = False
            cblapmodalbarang.Checked = False
        End If
    End Sub

    Private Sub clblapmutasibarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clblapmutasibarang.MouseDown
        Dim Index As Integer = clblapmutasibarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblapmutasibarang.SetItemChecked(Index, Not clblapmutasibarang.GetItemChecked(Index))

        For id As Integer = 0 To clblapmutasibarang.Items.Count - 1
            If clblapmutasibarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblapmutasibarang.Enabled = False
            cblapmutasibarang.Checked = False
        End If
    End Sub

    Private Sub clblappenyesuaianstok_MouseDown(sender As Object, e As MouseEventArgs) Handles clblappenyesuaianstok.MouseDown
        Dim Index As Integer = clblappenyesuaianstok.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblappenyesuaianstok.SetItemChecked(Index, Not clblappenyesuaianstok.GetItemChecked(Index))

        For id As Integer = 0 To clblappenyesuaianstok.Items.Count - 1
            If clblappenyesuaianstok.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblappenyesuaianstok.Enabled = False
            cblappenyesuaianstok.Checked = False
        End If
    End Sub

    Private Sub clblaplabarugi_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaplabarugi.MouseDown
        Dim Index As Integer = clblaplabarugi.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaplabarugi.SetItemChecked(Index, Not clblaplabarugi.GetItemChecked(Index))

        For id As Integer = 0 To clblaplabarugi.Items.Count - 1
            If clblaplabarugi.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaplabarugi.Enabled = False
            cblaplabarugi.Checked = False
        End If
    End Sub

    Private Sub clblaprekapanharian_MouseDown(sender As Object, e As MouseEventArgs) Handles clblaprekapanharian.MouseDown
        Dim Index As Integer = clblaprekapanharian.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clblaprekapanharian.SetItemChecked(Index, Not clblaprekapanharian.GetItemChecked(Index))

        For id As Integer = 0 To clblaprekapanharian.Items.Count - 1
            If clblaprekapanharian.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clblaprekapanharian.Enabled = False
            cblaprekapanharian.Checked = False
        End If
    End Sub

    Private Sub clbchartpembelian_MouseDown(sender As Object, e As MouseEventArgs) Handles clbchartpembelian.MouseDown
        Dim Index As Integer = clbchartpembelian.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbchartpembelian.SetItemChecked(Index, Not clbchartpembelian.GetItemChecked(Index))

        For id As Integer = 0 To clbchartpembelian.Items.Count - 1
            If clbchartpembelian.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbchartpembelian.Enabled = False
            cbchartpembelian.Checked = False
        End If
    End Sub

    Private Sub clbchartpenjualan_MouseDown(sender As Object, e As MouseEventArgs) Handles clbchartpenjualan.MouseDown
        Dim Index As Integer = clbchartpenjualan.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbchartpenjualan.SetItemChecked(Index, Not clbchartpenjualan.GetItemChecked(Index))

        For id As Integer = 0 To clbchartpenjualan.Items.Count - 1
            If clbchartpenjualan.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbchartpenjualan.Enabled = False
            cbchartpenjualan.Checked = False
        End If
    End Sub

    Private Sub clbchartlunasutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbchartlunasutang.MouseDown
        Dim Index As Integer = clbchartlunasutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbchartlunasutang.SetItemChecked(Index, Not clbchartlunasutang.GetItemChecked(Index))

        For id As Integer = 0 To clbchartlunasutang.Items.Count - 1
            If clbchartlunasutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbchartlunasutang.Enabled = False
            cbchartlunasutang.Checked = False
        End If
    End Sub

    Private Sub clbchartlunaspiutang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbchartlunaspiutang.MouseDown
        Dim Index As Integer = clbchartlunaspiutang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbchartlunaspiutang.SetItemChecked(Index, Not clbchartlunaspiutang.GetItemChecked(Index))

        For id As Integer = 0 To clbchartlunaspiutang.Items.Count - 1
            If clbchartlunaspiutang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbchartlunaspiutang.Enabled = False
            cbchartlunaspiutang.Checked = False
        End If
    End Sub

    Private Sub clbchartakunmasuk_MouseDown(sender As Object, e As MouseEventArgs) Handles clbchartakunmasuk.MouseDown
        Dim Index As Integer = clbchartakunmasuk.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbchartakunmasuk.SetItemChecked(Index, Not clbchartakunmasuk.GetItemChecked(Index))

        For id As Integer = 0 To clbchartakunmasuk.Items.Count - 1
            If clbchartakunmasuk.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbchartakunmasuk.Enabled = False
            cbchartakunmasuk.Checked = False
        End If
    End Sub

    Private Sub clbchartakunkeluar_MouseDown(sender As Object, e As MouseEventArgs) Handles clbchartakunkeluar.MouseDown
        Dim Index As Integer = clbchartakunkeluar.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbchartakunkeluar.SetItemChecked(Index, Not clbchartakunkeluar.GetItemChecked(Index))

        For id As Integer = 0 To clbchartakunkeluar.Items.Count - 1
            If clbchartakunkeluar.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbchartakunkeluar.Enabled = False
            cbchartakunkeluar.Checked = False
        End If
    End Sub
End Class