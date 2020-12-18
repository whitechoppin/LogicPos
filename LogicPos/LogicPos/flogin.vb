Imports System.Data.Odbc
Public Class flogin
    Public namaform As String = "login"

    Public CPUIDPOS, STATUSPOS As String
    Public USERID As Integer

    'feature variabel ===========================================================================================================================================================================================

    Public xmaster_barang, xmaster_kategori, xmaster_gudang, xmaster_pelanggan, xmaster_supplier, xmaster_user, xmaster_kas, xmaster_pricelist, xmaster_rek_supplier, xmaster_rek_pelanggan As Integer
    Public xpembelian, xpenjualan, xretur_beli, xretur_jual, xbarang_masuk, xbarang_keluar, xtransfer_barang, xpenyesuaian_stok As Integer
    Public xlunas_utang, xlunas_piutang, xtransfer_kas, xakun_masuk, xakun_keluar As Integer
    Public xlap_pricelist, xlap_pembelian, xlap_penjualan, xlap_penjualan_pajak, xlap_returbeli, xlap_returjual, xlap_barangmasuk, xlap_barangkeluar,
            xlap_transfer_barang, xlap_stok_barang, xlap_lunas_utang, xlap_lunas_piutang, xlap_akun_masuk, xlap_akun_keluar, xlap_transfer_kas, xlap_transaksi_kas,
            xlap_modal_barang, xlap_mutasi_barang, xlap_penyesuaian_stok, xlap_laba_rugi, xlap_rekapan_akhir As Integer
    Public xchart_pembelian, xchart_penjualan, xchart_lunas_utang, xchart_lunas_piutang, xchart_kas_masuk, xchart_kas_keluar As Integer
    Public xfeature_kalkulasi, xfeature_barcode As Integer

    'hak akses variabel ===========================================================================================================================================================================================
    Public rekeningsupplier, rekeningpelanggan, maxprinting As Integer
    '==================================================================
    Public master_barang, master_kategori, master_gudang, master_pelanggan, master_supplier, master_user, master_kas, master_pricelist, master_rek_supplier, master_rek_pelanggan, master_max_print As Integer
    Public pembelian, penjualan, retur_beli, retur_jual, barang_masuk, barang_keluar, transfer_barang, penyesuaian_stok As Integer
    Public lunas_utang, lunas_piutang, transfer_kas, akun_masuk, akun_keluar As Integer
    Public lap_pricelist, lap_pembelian, lap_penjualan, lap_penjualan_pajak, lap_returbeli, lap_returjual, lap_barangmasuk, lap_barangkeluar,
            lap_transfer_barang, lap_stok_barang, lap_lunas_utang, lap_lunas_piutang, lap_akun_masuk, lap_akun_keluar, lap_transfer_kas, lap_transaksi_kas,
            lap_modal_barang, lap_mutasi_barang, lap_penyesuaian_stok, lap_laba_rugi, lap_rekapan_akhir As Integer
    Public chart_pembelian, chart_penjualan, chart_lunas_utang, chart_lunas_piutang, chart_kas_masuk, chart_kas_keluar As Integer
    Public feature_kalkulasi, feature_barcode As Integer

    Public setting_info, setting_printer, setting_backup_database, setting_pengaturan As Integer

    Dim kodeuser, namauser, emailuser, jabatanuser As String

    Sub ProcessorID()
        'shows the processor name and speed of the computer
        Dim MyOBJ As Object
        Dim cpu As Object

        MyOBJ = GetObject("WinMgmts:").instancesof("Win32_Processor")
        For Each cpu In MyOBJ
            CPUIDPOS = cpu.ProcessorId
        Next
    End Sub

    Sub resetfeature()
        xmaster_barang = 0
        xmaster_kategori = 0
        xmaster_gudang = 0
        xmaster_pelanggan = 0
        xmaster_supplier = 0
        xmaster_user = 0
        xmaster_kas = 0
        xmaster_pricelist = 0
        xmaster_rek_supplier = 0
        xmaster_rek_pelanggan = 0

        xpembelian = 0
        xpenjualan = 0
        xretur_beli = 0
        xretur_jual = 0
        xbarang_keluar = 0
        xbarang_masuk = 0
        xtransfer_barang = 0
        xpenyesuaian_stok = 0

        xlunas_utang = 0
        xlunas_piutang = 0
        xtransfer_kas = 0
        xakun_masuk = 0
        xakun_keluar = 0

        xlap_pricelist = 0
        xlap_pembelian = 0
        xlap_penjualan = 0
        xlap_penjualan_pajak = 0

        xlap_returbeli = 0
        xlap_returjual = 0
        xlap_barangmasuk = 0
        xlap_barangkeluar = 0

        xlap_transfer_barang = 0
        xlap_stok_barang = 0
        xlap_lunas_utang = 0
        xlap_lunas_piutang = 0

        xlap_akun_masuk = 0
        xlap_akun_keluar = 0
        xlap_transfer_kas = 0
        xlap_transaksi_kas = 0

        xlap_modal_barang = 0
        xlap_mutasi_barang = 0
        xlap_penyesuaian_stok = 0
        xlap_laba_rugi = 0

        xlap_rekapan_akhir = 0

        xchart_pembelian = 0
        xchart_penjualan = 0
        xchart_lunas_utang = 0
        xchart_lunas_piutang = 0
        xchart_kas_masuk = 0
        xchart_kas_keluar = 0

        xfeature_kalkulasi = 0
        xfeature_barcode = 0
    End Sub

    Sub reset()
        master_max_print = 0

        master_barang = 0
        master_kategori = 0
        master_gudang = 0
        master_pelanggan = 0
        master_supplier = 0
        master_user = 0
        master_kas = 0
        master_pricelist = 0
        master_rek_supplier = 0
        master_rek_pelanggan = 0

        pembelian = 0
        penjualan = 0
        retur_beli = 0
        retur_jual = 0
        barang_keluar = 0
        barang_masuk = 0
        transfer_barang = 0
        penyesuaian_stok = 0

        lunas_utang = 0
        lunas_piutang = 0
        transfer_kas = 0
        akun_masuk = 0
        akun_keluar = 0

        lap_pricelist = 0
        lap_pembelian = 0
        lap_penjualan = 0
        lap_penjualan_pajak = 0

        lap_returbeli = 0
        lap_returjual = 0
        lap_barangmasuk = 0
        lap_barangkeluar = 0

        lap_transfer_barang = 0
        lap_stok_barang = 0
        lap_lunas_utang = 0
        lap_lunas_piutang = 0

        lap_akun_masuk = 0
        lap_akun_keluar = 0
        lap_transfer_kas = 0
        lap_transaksi_kas = 0

        lap_modal_barang = 0
        lap_mutasi_barang = 0
        lap_penyesuaian_stok = 0
        lap_laba_rugi = 0

        lap_rekapan_akhir = 0

        chart_pembelian = 0
        chart_penjualan = 0
        chart_lunas_utang = 0
        chart_lunas_piutang = 0
        chart_kas_masuk = 0
        chart_kas_keluar = 0

        feature_kalkulasi = 0
        feature_barcode = 0

        setting_info = 0
        setting_printer = 0
        setting_backup_database = 0
        setting_pengaturan = 0
    End Sub
    Sub login()
        Dim counteruser As Integer

        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" & txtusername.Text & "' AND password_user= '" & txtpassword.Text & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            MsgBox("Username atau Password anda salah !", MsgBoxStyle.Exclamation, "Error Login")
        Else
            USERID = dr("id")

            Call koneksii()
            sql = "SELECT COUNT(user_id) AS total_user FROM tb_status_user WHERE user_id='" & USERID & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()

            counteruser = Val(dr("total_user"))

            If counteruser = 0 Then
                MsgBox("Selamat Datang " & txtusername.Text & " ! ", MsgBoxStyle.Information, "Successfull Login")
                Call offform()
                Call feature()
                Call procced()
            Else
                sql = "SELECT * FROM tb_status_user WHERE user_id = '" & USERID & "' AND computer_id='" & CPUIDPOS & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                dr.Read()

                If dr.HasRows = 0 Then
                    MsgBox("Anda Sudah Login !", MsgBoxStyle.Exclamation, "Error Login")
                Else
                    MsgBox("Selamat Datang " & txtusername.Text & " ! ", MsgBoxStyle.Information, "Successfull Login")
                    Call offform()
                    Call feature()
                    Call procced()
                End If
            End If
        End If
    End Sub

    Sub feature()
        Call koneksii()
        sql = "SELECT * FROM tb_feature LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        xmaster_barang = Val(dr("master_barang"))
        xmaster_kategori = Val(dr("master_kategori"))
        xmaster_gudang = Val(dr("master_gudang"))
        xmaster_pelanggan = Val(dr("master_pelanggan"))
        xmaster_supplier = Val(dr("master_supplier"))
        xmaster_user = Val(dr("master_user"))
        xmaster_kas = Val(dr("master_kas"))
        xmaster_pricelist = Val(dr("master_pricelist"))
        xmaster_rek_supplier = Val(dr("master_rek_supp"))
        xmaster_rek_pelanggan = Val(dr("master_rek_plng"))

        xpembelian = Val(dr("pembelian"))
        xpenjualan = Val(dr("penjualan"))
        xretur_beli = Val(dr("retur_beli"))
        xretur_jual = Val(dr("retur_jual"))
        xbarang_keluar = Val(dr("barang_keluar"))
        xbarang_masuk = Val(dr("barang_masuk"))
        xtransfer_barang = Val(dr("transfer_barang"))
        xpenyesuaian_stok = Val(dr("penyesuaian_stok"))

        xlunas_utang = Val(dr("lunas_utang"))
        xlunas_piutang = Val(dr("lunas_piutang"))
        xtransfer_kas = Val(dr("transfer_kas"))
        xakun_masuk = Val(dr("akun_masuk"))
        xakun_keluar = Val(dr("akun_keluar"))

        xlap_pricelist = Val(dr("lap_pricelist"))
        xlap_pembelian = Val(dr("lap_pembelian"))
        xlap_penjualan = Val(dr("lap_penjualan"))
        xlap_penjualan_pajak = Val(dr("lap_penjualan_pajak"))

        xlap_returbeli = Val(dr("lap_returbeli"))
        xlap_returjual = Val(dr("lap_returjual"))
        xlap_barangmasuk = Val(dr("lap_barang_masuk"))
        xlap_barangkeluar = Val(dr("lap_barang_keluar"))

        xlap_transfer_barang = Val(dr("lap_transfer_barang"))
        xlap_stok_barang = Val(dr("lap_stok_barang"))
        xlap_lunas_utang = Val(dr("lap_utang"))
        xlap_lunas_piutang = Val(dr("lap_piutang"))

        xlap_akun_masuk = Val(dr("lap_akun_masuk"))
        xlap_akun_keluar = Val(dr("lap_akun_keluar"))
        xlap_transfer_kas = Val(dr("lap_transfer_kas"))
        xlap_transaksi_kas = Val(dr("lap_transaksi_kas"))

        xlap_modal_barang = Val(dr("lap_modal_barang"))
        xlap_mutasi_barang = Val(dr("lap_mutasi_barang"))
        xlap_penyesuaian_stok = Val(dr("lap_penyesuaian_stok"))
        xlap_laba_rugi = Val(dr("lap_laba_rugi"))

        xlap_rekapan_akhir = Val(dr("lap_rekapan_akhir"))

        xchart_pembelian = Val(dr("chart_pembelian"))
        xchart_penjualan = Val(dr("chart_penjualan"))
        xchart_lunas_utang = Val(dr("chart_lunas_utang"))
        xchart_lunas_piutang = Val(dr("chart_lunas_piutang"))
        xchart_kas_masuk = Val(dr("chart_kas_masuk"))
        xchart_kas_keluar = Val(dr("chart_kas_keluar"))

        xfeature_kalkulasi = Val(dr("feature_kalkulasi_expedisi"))
        xfeature_barcode = Val(dr("feature_barcode_generator"))
    End Sub

    Sub procced()
        Call koneksii()
        sql = "DELETE FROM tb_status_user WHERE user_id='" & USERID & "' AND computer_id='" & CPUIDPOS & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE id = '" & USERID & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodeuser = dr("kode_user")
        namauser = dr("nama_user")
        emailuser = dr("email_user")
        jabatanuser = dr("jabatan_user")

        master_max_print = Val(dr("max_print"))
        '========================================================================
        If xmaster_barang > 0 Then
            master_barang = Val(dr("master_barang")) 'master barang

            If master_barang > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(0).Visible = True 'master barang
            End If
        End If

        If xmaster_kategori > 0 Then
            master_kategori = Val(dr("master_kategori")) 'master kategori

            If master_kategori > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(1).Visible = True 'master kategori barang
            End If
        End If

        If xmaster_gudang > 0 Then
            master_gudang = Val(dr("master_gudang")) 'master gudang

            If master_gudang > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(2).Visible = True 'master Gudang
            End If
        End If

        If xmaster_pelanggan > 0 Then
            master_pelanggan = Val(dr("master_pelanggan")) 'master pelanggan

            If master_pelanggan > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(3).Visible = True 'master pelanggan
            End If
        End If

        If xmaster_supplier > 0 Then
            master_supplier = Val(dr("master_supplier")) 'master supplier

            If master_supplier > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(4).Visible = True 'master supp
            End If
        End If

        If xmaster_user > 0 Then
            master_user = Val(dr("master_user")) 'master user

            If master_user > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(5).Visible = True 'master user
            End If
        End If

        If xmaster_kas > 0 Then
            master_kas = Val(dr("master_kas")) 'master kas

            If master_kas > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(6).Visible = True 'master kas
            End If
        End If

        If xmaster_pricelist > 0 Then
            master_pricelist = Val(dr("master_pricelist")) 'master pricelist

            If master_pricelist > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(7).Visible = True 'master price
            End If
        End If

        If xmaster_rek_supplier > 0 Then
            master_rek_supplier = Val(dr("master_rek_supp")) 'master rek supplier

            If master_rek_pelanggan > 0 Then
                rekeningpelanggan = master_rek_pelanggan
            Else
                rekeningpelanggan = 0
            End If

        End If

        If xmaster_rek_pelanggan > 0 Then
            master_rek_pelanggan = Val(dr("master_rek_plng")) 'master rek pelanggan

            If master_rek_supplier > 0 Then
                rekeningsupplier = master_rek_supplier
            Else
                rekeningsupplier = 0
            End If
        End If

        '===========================================================================================================================
        maxprinting = master_max_print

        If xpembelian > 0 Then
            pembelian = Val(dr("pembelian")) 'pembelian

            If pembelian > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(0).Visible = True 'pembelian
            End If
        End If

        If xpenjualan > 0 Then
            penjualan = Val(dr("penjualan")) 'penjualan

            If penjualan > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(1).Visible = True 'penjualan 
            End If
        End If

        If xretur_beli > 0 Then
            retur_beli = Val(dr("retur_beli"))

            If retur_beli > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(2).Visible = True 'retur pembelian
            End If
        End If

        If xretur_jual > 0 Then
            retur_jual = Val(dr("retur_jual"))

            If retur_jual > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(3).Visible = True 'retur penjualan
            End If
        End If

        If xbarang_masuk > 0 Then
            barang_masuk = Val(dr("barang_masuk"))

            If barang_masuk > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(4).Visible = True 'barang masuk
            End If
        End If

        If xbarang_keluar > 0 Then
            barang_keluar = Val(dr("barang_keluar"))

            If barang_keluar > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(5).Visible = True 'barang keluar
            End If
        End If

        If xtransfer_barang > 0 Then
            transfer_barang = Val(dr("transfer_barang"))

            If transfer_barang > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(6).Visible = True 'transfer barang
            End If
        End If

        If xpenyesuaian_stok > 0 Then
            penyesuaian_stok = Val(dr("penyesuaian_stok"))

            If penyesuaian_stok > 0 Then
                fmenu.TransaksiMenu.DropDownItems.Item(7).Visible = True 'penyesuaian stok
            End If
        End If

        '===========================================================================================================================


        lunas_utang = Val(dr("lunas_utang"))
        lunas_piutang = Val(dr("lunas_piutang"))
        transfer_kas = Val(dr("transfer_kas"))
        akun_masuk = Val(dr("akun_masuk"))
        akun_keluar = Val(dr("akun_keluar"))

        If lunas_utang > 0 Then
            fmenu.AdministrasiMenu.DropDownItems.Item(0).Visible = True 'pelunasan utang
        End If

        If lunas_piutang > 0 Then
            fmenu.AdministrasiMenu.DropDownItems.Item(1).Visible = True 'pelunasan piutang
        End If

        If transfer_kas > 0 Then
            fmenu.AdministrasiMenu.DropDownItems.Item(4).Visible = True 'transfer kas
        End If

        If akun_masuk > 0 Then
            fmenu.AdministrasiMenu.DropDownItems.Item(2).Visible = True 'kas masuk
        End If

        If akun_keluar > 0 Then
            fmenu.AdministrasiMenu.DropDownItems.Item(3).Visible = True 'kas keluar
        End If

        '===========================================================================================================================

        lap_pricelist = Val(dr("lap_pricelist"))
        lap_pembelian = Val(dr("lap_pembelian"))
        lap_penjualan = Val(dr("lap_penjualan"))
        lap_penjualan_pajak = Val(dr("lap_penjualan_pajak"))

        lap_returbeli = Val(dr("lap_returbeli"))
        lap_returjual = Val(dr("lap_returjual"))
        lap_barangmasuk = Val(dr("lap_barang_masuk"))
        lap_barangkeluar = Val(dr("lap_barang_keluar"))

        lap_transfer_barang = Val(dr("lap_transfer_barang"))
        lap_stok_barang = Val(dr("lap_stok_barang"))
        lap_lunas_utang = Val(dr("lap_utang"))
        lap_lunas_piutang = Val(dr("lap_piutang"))

        lap_akun_masuk = Val(dr("lap_akun_masuk"))
        lap_akun_keluar = Val(dr("lap_akun_keluar"))
        lap_transfer_kas = Val(dr("lap_transfer_kas"))
        lap_transaksi_kas = Val(dr("lap_transaksi_kas"))

        lap_modal_barang = Val(dr("lap_modal_barang"))
        lap_mutasi_barang = Val(dr("lap_mutasi_barang"))
        lap_penyesuaian_stok = Val(dr("lap_penyesuaian_stok"))
        lap_laba_rugi = Val(dr("lap_laba_rugi"))

        lap_rekapan_akhir = Val(dr("lap_rekapan_akhir"))

        If lap_pricelist > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(0).Visible = True 'lap pembelian
        End If

        If lap_pembelian > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(1).Visible = True 'lap pembelian
        End If

        If lap_penjualan > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(2).Visible = True 'lap penjualan
        End If

        If lap_penjualan_pajak > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(3).Visible = True 'lap penjualan pajak
        End If

        '=================================================================================

        If lap_returbeli > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(4).Visible = True 'lap retur beli
        End If

        If lap_returjual > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(5).Visible = True 'lap retur jual
        End If

        If lap_barangmasuk > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(6).Visible = True 'lap brg masuk
        End If

        If lap_barangkeluar > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(7).Visible = True 'lap brg keluar
        End If

        '=================================================================================

        If lap_transfer_barang > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(8).Visible = True 'lap transfer brg
        End If

        If lap_stok_barang > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(9).Visible = True 'lap stok barang
        End If

        If lap_lunas_utang > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(10).Visible = True 'lap lunas utang
        End If

        If lap_lunas_piutang > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(11).Visible = True 'lap lunas piutang
        End If

        '=================================================================================

        If lap_akun_masuk > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(12).Visible = True 'lap akun masuk
        End If

        If lap_akun_keluar > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(13).Visible = True 'lap akun keluar
        End If

        If lap_transfer_kas > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(14).Visible = True 'lap transfer kas
        End If

        If lap_transaksi_kas > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(15).Visible = True 'lap transaksi kas 
        End If

        '===================================================================================

        If lap_modal_barang > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(16).Visible = True 'lap modal barang
        End If

        If lap_mutasi_barang > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(17).Visible = True 'lap mutasi barang
        End If

        If lap_penyesuaian_stok > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(18).Visible = True 'lap penyesuaian stok
        End If

        If lap_laba_rugi > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(19).Visible = True 'lap laba rugi
        End If

        '=====================================================================================

        If lap_rekapan_akhir > 0 Then
            fmenu.LaporanMenu.DropDownItems.Item(20).Visible = True 'lap rekapan akhir
        End If

        '=====================================================================================

        chart_pembelian = Val(dr("chart_pembelian"))
        chart_penjualan = Val(dr("chart_penjualan"))
        chart_lunas_utang = Val(dr("chart_lunas_utang"))
        chart_lunas_piutang = Val(dr("chart_lunas_piutang"))
        chart_kas_masuk = Val(dr("chart_kas_masuk"))
        chart_kas_keluar = Val(dr("chart_kas_keluar"))

        If chart_pembelian > 0 Then
            fmenu.ChartMenu.DropDownItems.Item(0).Visible = True 'chart pembelian
        End If

        If chart_penjualan > 0 Then
            fmenu.ChartMenu.DropDownItems.Item(1).Visible = True 'chart penjualan
        End If

        If chart_lunas_utang > 0 Then
            fmenu.ChartMenu.DropDownItems.Item(2).Visible = True 'chart lunas utang
        End If

        If chart_lunas_piutang > 0 Then
            fmenu.ChartMenu.DropDownItems.Item(3).Visible = True 'chart lunas piutang
        End If

        If chart_kas_masuk > 0 Then
            fmenu.ChartMenu.DropDownItems.Item(4).Visible = True 'chart kas masuk
        End If

        If chart_kas_keluar > 0 Then
            fmenu.ChartMenu.DropDownItems.Item(5).Visible = True 'chart kas keluar
        End If

        feature_kalkulasi = Val(dr("feature_kalkulasi_expedisi"))
        feature_barcode = Val(dr("feature_barcode_generator"))

        If feature_kalkulasi > 0 Then
            fmenu.FeatureMenu.DropDownItems.Item(0).Visible = True 'feature kalkulasi
        End If

        If feature_barcode > 0 Then
            fmenu.FeatureMenu.DropDownItems.Item(1).Visible = True 'feature barcode
        End If

        '===========================================================================================================================

        setting_info = Val(dr("setting_info_perusahaan"))
        setting_printer = Val(dr("setting_printer"))
        setting_backup_database = Val(dr("setting_backup_database"))
        setting_pengaturan = Val(dr("setting_pengaturan"))

        If setting_info > 0 Then
            fmenu.SettingMenu.DropDownItems.Item(0).Visible = True 'setting info
        End If

        If setting_printer > 0 Then
            fmenu.SettingMenu.DropDownItems.Item(1).Visible = True 'setting printer
        End If

        If setting_backup_database > 0 Then
            fmenu.SettingMenu.DropDownItems.Item(6).Visible = True 'setting backup database
        End If

        If setting_pengaturan > 0 Then
            fmenu.SettingMenu.DropDownItems.Item(7).Visible = True 'setting pengaturan
        End If

        sql = "INSERT INTO tb_status_user(user_id, computer_id, status_user, created_by, date_created) VALUES ('" & USERID & "','" & CPUIDPOS & "','" & STATUSPOS & "','" & txtusername.Text & "',now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        Me.Hide()

        fmenu.kodeuser.Text = kodeuser
        fmenu.namauser.Text = namauser
        fmenu.emailuser.Text = emailuser
        fmenu.jabatanuser.Text = jabatanuser
        fmenu.Show()
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Call reset()
        If txtusername.Text = "" Then
            MessageBox.Show("User masih kosong", "username", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            txtusername.Focus()
        ElseIf txtpassword.Text = "" Then
            MessageBox.Show("Password masih kosong", "password", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            txtpassword.Focus()
        Else
            Call login()
        End If
    End Sub


    Private Sub flogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = False
        txtpassword.PasswordChar = "•"
        Call ProcessorID()

        STATUSPOS = GenerateRandomString(10)
    End Sub

    Private Sub txtusername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtusername.KeyPress
        e.Handled = ValidAngkaHuruf(e)
    End Sub

    Private Sub txtpassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpassword.KeyPress
        e.Handled = ValidAngkaHuruf(e)
    End Sub


    Private Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtusername.Text = "" Then
                MessageBox.Show("User masih kosong", "username", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                txtusername.Focus()
                Exit Sub
            End If

            If txtpassword.Text = "" Then
                MessageBox.Show("Password masih kosong", "password", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                txtpassword.Focus()
                Exit Sub
            End If

            Call resetfeature()
            Call reset()
            Call login()
        End If
    End Sub
End Class