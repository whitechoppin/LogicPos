Imports System.Data.Odbc
Public Class flogin
    Sub login()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtusername.Text + "' AND password_user= '" + txtpassword.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            MsgBox("Username atau Password ada yang salah !", MsgBoxStyle.Exclamation, "Error Login")
        Else
            MsgBox("Selamat Datang " & txtusername.Text & " ! ", MsgBoxStyle.Information, "Successfull Login")
            Call offform()

            sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtusername.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()
            Dim master_barang, master_gudang, master_customer, master_supplier, master_user, master_kas, master_pricelist, master_rek_supplier, master_rek_cust As Integer
            master_barang = dr("master_barang")
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

            If master_customer > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(2).Visible = True 'master customer
            End If

            fmenu.MasterMenu.DropDownItems.Item(1).Visible = True 'master Gudang

            If master_supplier > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(3).Visible = True 'master supp
            End If

            If master_user > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(4).Visible = True 'master user
            End If

            If master_kas > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(5).Visible = True 'master kas
            End If

            If master_pricelist > 0 Then
                fmenu.MasterMenu.DropDownItems.Item(6).Visible = True 'master price
            End If

            Dim pembelian, penjualan, retur_beli, retur_jual, barang_masuk, barang_keluar, transfer_barang As Integer
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

            fmenu.AdministrasiMenu.DropDownItems.Item(0).Visible = True 'pelunasan utang
            fmenu.AdministrasiMenu.DropDownItems.Item(1).Visible = True 'pelunasan piutang
            fmenu.AdministrasiMenu.DropDownItems.Item(2).Visible = True 'kas masuk
            fmenu.AdministrasiMenu.DropDownItems.Item(3).Visible = True 'kas keluar
            fmenu.AdministrasiMenu.DropDownItems.Item(4).Visible = True 'transfer kas

            fmenu.LaporanMenu.DropDownItems.Item(0).Visible = True 'lap penjualan
            fmenu.LaporanMenu.DropDownItems.Item(1).Visible = True 'lap pembelian
            fmenu.LaporanMenu.DropDownItems.Item(2).Visible = True 'lap utang
            fmenu.LaporanMenu.DropDownItems.Item(3).Visible = True 'lap piutang
            fmenu.LaporanMenu.DropDownItems.Item(4).Visible = True 'lap akun masuk
            fmenu.LaporanMenu.DropDownItems.Item(5).Visible = True 'lap akun keluar
            fmenu.LaporanMenu.DropDownItems.Item(6).Visible = True 'lap transfer kas
            fmenu.LaporanMenu.DropDownItems.Item(7).Visible = True 'lap transfer brg
            fmenu.LaporanMenu.DropDownItems.Item(8).Visible = True 'lap stok barang
            fmenu.LaporanMenu.DropDownItems.Item(9).Visible = True 'lap transaksi kas 

            fmenu.SettingMenu.DropDownItems.Item(0).Visible = True 'set printer

            Me.Hide()
                fmenu.statususer.Text = txtusername.Text.ToUpper
                fmenu.Show()

            End If
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
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
        Call login()
    End Sub


    Private Sub flogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = False
        txtpassword.PasswordChar = "•"
    End Sub

    Private Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call login()
        End If
    End Sub
End Class