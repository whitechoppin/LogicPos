Imports System.Data.Odbc
Public Class flogin
    Public CPUIDPOS As String
    Public rekeningsupplier, rekeningcustomer, maxprinting As Integer
    Public master_barang, master_kategori, master_gudang, master_customer, master_supplier, master_user, master_kas, master_pricelist, master_rek_supplier, master_rek_cust, master_max_print As Integer
    Public pembelian, penjualan, retur_beli, retur_jual, barang_masuk, barang_keluar, transfer_barang As Integer
    Public lunas_utang, lunas_piutang, transfer_kas, akun_masuk, akun_keluar As Integer
    Public lap_pricelist, lap_pembelian, lap_penjualan, lap_returbeli, lap_returjual, lap_barangmasuk, lap_barangkeluar, lap_lunas_utang, lap_lunas_piutang, lap_stok_barang, lap_akun_masuk, lap_akun_keluar, lap_transfer_kas, lap_transfer_barang, lap_transaksi_kas, lap_modal_barang As Integer

    Sub ProcessorID()
        ' shows the processor name and speed of the computer
        Dim MyOBJ As Object
        Dim cpu As Object

        MyOBJ = GetObject("WinMgmts:").instancesof("Win32_Processor")
        For Each cpu In MyOBJ
            CPUIDPOS = cpu.ProcessorId
        Next
    End Sub

    Sub reset()
        master_max_print = 0

        master_barang = 0
        master_kategori = 0
        master_gudang = 0
        master_customer = 0
        master_supplier = 0
        master_user = 0
        master_kas = 0
        master_pricelist = 0
        master_rek_supplier = 0
        master_rek_cust = 0

        pembelian = 0
        penjualan = 0
        retur_beli = 0
        retur_jual = 0
        barang_keluar = 0
        barang_masuk = 0
        transfer_barang = 0

        lunas_utang = 0
        lunas_piutang = 0
        transfer_kas = 0
        akun_masuk = 0
        akun_keluar = 0

        lap_pricelist = 0
        lap_pembelian = 0
        lap_penjualan = 0
        lap_returbeli = 0
        lap_returjual = 0
        lap_barangmasuk = 0
        lap_barangkeluar = 0
        lap_lunas_utang = 0
        lap_lunas_piutang = 0
        lap_akun_masuk = 0
        lap_akun_keluar = 0
        lap_transfer_kas = 0
        lap_transfer_barang = 0
        lap_stok_barang = 0
        lap_transaksi_kas = 0
        lap_modal_barang = 0
    End Sub
    Sub login()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtusername.Text + "' AND password_user= '" + txtpassword.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            MsgBox("Username atau Password ada yang salah !", MsgBoxStyle.Exclamation, "Error Login")
        Else
            sql = "SELECT * FROM tb_status_user WHERE kode_user = '" + txtusername.Text + "' AND computer_id='" + CPUIDPOS + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()

            If dr.HasRows = 0 Then
                MsgBox("Selamat Datang " & txtusername.Text & " ! ", MsgBoxStyle.Information, "Successfull Login")
                Call offform()

                sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtusername.Text + "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                dr.Read()
                master_max_print = dr("max_print")
                master_barang = dr("master_barang")
                master_kategori = dr("master_kategori")
                master_gudang = dr("master_gudang")
                master_customer = dr("master_customer")
                master_supplier = dr("master_supplier")
                master_user = dr("master_user")
                master_kas = dr("master_kas")
                master_pricelist = dr("master_pricelist")
                master_rek_supplier = dr("master_rek_supp")
                master_rek_cust = dr("master_rek_cust")

                If master_barang > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(0).Visible = True 'master barang
                End If

                If master_kategori > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(1).Visible = True 'master kategori barang
                End If

                If master_gudang > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(2).Visible = True 'master Gudang
                End If

                If master_customer > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(3).Visible = True 'master customer
                End If

                If master_supplier > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(4).Visible = True 'master supp
                End If

                If master_user > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(5).Visible = True 'master user
                End If

                If master_kas > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(6).Visible = True 'master kas
                End If

                If master_pricelist > 0 Then
                    fmenu.MasterMenu.DropDownItems.Item(7).Visible = True 'master price
                End If

                rekeningcustomer = master_rek_cust
                rekeningsupplier = master_rek_supplier
                maxprinting = master_max_print

                pembelian = dr("pembelian")
                penjualan = dr("penjualan")
                retur_beli = dr("retur_beli")
                retur_jual = dr("retur_jual")
                barang_keluar = dr("barang_keluar")
                barang_masuk = dr("barang_masuk")
                transfer_barang = dr("transfer_barang")

                If pembelian > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(0).Visible = True 'pembelian
                End If

                If penjualan > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(1).Visible = True 'penjualan 
                End If

                If retur_jual > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(3).Visible = True 'retur penjualan
                End If

                If retur_beli > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(2).Visible = True 'retur pembelian
                End If

                If barang_masuk > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(4).Visible = True 'barang masuk
                End If

                If barang_keluar > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(5).Visible = True 'barang keluar
                End If

                If transfer_barang > 0 Then
                    fmenu.TransaksiMenu.DropDownItems.Item(6).Visible = True 'transfer barang
                End If

                lunas_utang = dr("lunas_utang")
                lunas_piutang = dr("lunas_piutang")
                transfer_kas = dr("transfer_kas")
                akun_masuk = dr("akun_masuk")
                akun_keluar = dr("akun_keluar")

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

                lap_pricelist = dr("lap_pricelist")
                lap_pembelian = dr("lap_pembelian")
                lap_penjualan = dr("lap_penjualan")
                lap_returbeli = dr("lap_returbeli")
                lap_returjual = dr("lap_returjual")
                lap_barangmasuk = dr("lap_barang_masuk")
                lap_barangkeluar = dr("lap_barang_keluar")
                lap_lunas_utang = dr("lap_utang")
                lap_lunas_piutang = dr("lap_piutang")
                lap_akun_masuk = dr("lap_akun_masuk")
                lap_akun_keluar = dr("lap_akun_keluar")
                lap_transfer_kas = dr("lap_transfer_kas")
                lap_transfer_barang = dr("lap_transfer_barang")
                lap_stok_barang = dr("lap_stok_barang")
                lap_transaksi_kas = dr("lap_transaksi_kas")
                lap_modal_barang = dr("lap_modal_barang")

                If lap_pricelist > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(0).Visible = True 'lap pembelian
                End If

                If lap_pembelian > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(1).Visible = True 'lap pembelian
                End If

                If lap_penjualan > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(2).Visible = True 'lap penjualan
                End If

                If lap_returbeli > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(3).Visible = True 'lap retur beli
                End If

                If lap_returjual > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(4).Visible = True 'lap retur jual
                End If

                If lap_barangmasuk > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(5).Visible = True 'lap brg masuk
                End If

                If lap_barangkeluar > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(6).Visible = True 'lap brg keluar
                End If

                If lap_lunas_utang > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(7).Visible = True 'lap lunas utang
                End If

                If lap_lunas_piutang > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(8).Visible = True 'lap lunas piutang
                End If

                If lap_akun_masuk > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(9).Visible = True 'lap akun masuk
                End If

                If lap_akun_keluar > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(10).Visible = True 'lap akun keluar
                End If

                If lap_transfer_kas > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(11).Visible = True 'lap transfer kas
                End If

                If lap_transfer_barang > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(12).Visible = True 'lap transfer brg
                End If

                If lap_stok_barang > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(13).Visible = True 'lap stok barang
                End If

                If lap_transaksi_kas > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(14).Visible = True 'lap transaksi kas 
                End If

                If lap_modal_barang > 0 Then
                    fmenu.LaporanMenu.DropDownItems.Item(15).Visible = True 'lap modal barang
                End If

                fmenu.SettingMenu.DropDownItems.Item(0).Visible = True 'set printer

                Me.Hide()
                fmenu.statususer.Text = txtusername.Text.ToUpper
                fmenu.Show()
            Else
                MsgBox("Anda Sudah Login !!!", MsgBoxStyle.Exclamation, "Error Login")
            End If
        End If
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

            Call reset()
            Call login()
        End If
    End Sub
End Class