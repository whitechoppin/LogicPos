Imports System.Data.Odbc
Public Class fmenu
    Private Sub exitStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitStripMenuItem.Click
        Me.Close()
        Me.Dispose()
        Application.Exit()
    End Sub
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        statustgl.Text = Now.ToLongDateString
        Timer.Start()
        'frmlogin.show()
        'frmlogin.mdiparent=me
        'Call koneksii()
        'statususer.Text = flogin.

    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        statusjam.Text = TimeOfDay
    End Sub
    Private Sub DataBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataBarangToolStripMenuItem.Click
        fbarang.Show()
    End Sub
    Private Sub DataCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataCustomerToolStripMenuItem.Click
        fcustomer.Show()
    End Sub
    Private Sub DataSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataSupplierToolStripMenuItem.Click
        fsupplier.Show()
    End Sub
    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem.Click
        fpembelian.Show()
    End Sub
    Private Sub PenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem.Click
        fpenjualan.Show()
    End Sub
    Private Sub PembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LPembelianToolStripMenuItem.Click
        flaporanpembelian.Show()
    End Sub
    Private Sub PenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LPenjualanToolStripMenuItem.Click
        flaporanpenjualan.Show()
    End Sub
    Private Sub InfoStokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LStokBarangToolStripMenuItem.Click
        flaporstokbarang.Show()
    End Sub
    Private Sub ManageUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataUserToolStripMenuItem.Click
        fuser.Show()
    End Sub
    Private Sub fmenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
    Private Sub PrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrinterToolStripMenuItem.Click
        fprinter.Show()
    End Sub
    Private Sub PricelistGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PricelistGroupToolStripMenuItem.Click
        fpricelist.Show()
    End Sub
    Private Sub MenuStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuUtama.ItemClicked

    End Sub
    Private Sub DataGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataGudangToolStripMenuItem.Click
        fgudang.Show()
    End Sub

    Private Sub DataKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataKasToolStripMenuItem.Click
        fkas.Show()
    End Sub

    Private Sub BarangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangKeluarToolStripMenuItem.Click
        fbarangkeluar.Show()
    End Sub

    Private Sub BarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangMasukToolStripMenuItem.Click
        fbarangmasuk.Show()
    End Sub

    Private Sub ReturPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPenjualanToolStripMenuItem.Click
        freturjual.Show()
    End Sub

    Private Sub ReturPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPembelianToolStripMenuItem.Click
        freturbeli.Show()
    End Sub

    Private Sub TransferBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferBarangToolStripMenuItem.Click
        ftransferbarang.Show()
    End Sub

    Private Sub PelunasanPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanPiutangToolStripMenuItem.Click
        flunaspiutang.Show()
    End Sub

    Private Sub PelunasanUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanUtangToolStripMenuItem.Click
        flunasutang.Show()
    End Sub

    Private Sub KasMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KasMasukToolStripMenuItem.Click
        fkasmasuk.Show()
    End Sub

    Private Sub KasKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KasKeluarToolStripMenuItem.Click
        fkaskeluar.Show()
    End Sub

    Private Sub TransferKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferKasToolStripMenuItem.Click
        ftransferkas.Show()
    End Sub

    Private Sub LUtangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LUtangToolStripMenuItem.Click
        flaporanutang.Show()
    End Sub

    Private Sub fmenu_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        'Label2.Location = New System.Drawing.Point(Me.Size.Width - Label2.Size.Width - 20, 35)
        'GridControl1.Location = New System.Drawing.Point(Me.Size.Width - GridControl1.Size.Width - 20, 70)
    End Sub

    Private Sub LPiutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LPiutangToolStripMenuItem.Click
        flaporanpiutang.Show()
    End Sub

    Private Sub LaporanTransaksiKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanTransaksiKasToolStripMenuItem.Click
        flaporankas.Show()
    End Sub

    Private Sub LAkunMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LAkunMasukToolStripMenuItem.Click
        flaporankasmasuk.Show()
    End Sub

    Private Sub LAkunKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LAkunKeluarToolStripMenuItem.Click
        flaporankaskeluar.Show()
    End Sub

    Private Sub LTransferKasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransferKasToolStripMenuItem.Click
        flaporantransferkas.Show()
    End Sub

    Private Sub LTransferBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LTransferBarangToolStripMenuItem.Click

    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Hide()
        flogin.Show()
        flogin.txtusername.Text = ""
        flogin.txtpassword.Text = ""
    End Sub
End Class
