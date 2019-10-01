Imports System.Data.Odbc
Public Class fmenu
    Private Sub exitStripMenuItem_Click(sender As Object, e As EventArgs) Handles exitStripMenuItem.Click
        Me.Close()
        Me.Dispose()
        Application.Exit()
    End Sub
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        statustgl.Text = Now.ToLongDateString
        Timer1.Start()
        'frmlogin.show()
        'frmlogin.mdiparent=me
        'Call koneksii()
        'statususer.Text = flogin.
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        statusjam.Text = TimeOfDay
    End Sub
    Private Sub DataBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataBarangToolStripMenuItem.Click
        fbarang.Show()
    End Sub
    Private Sub DataCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataCustomerToolStripMenuItem.Click
        'fcustomer.Show()
    End Sub
    Private Sub DataSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataSupplierToolStripMenuItem.Click
        'fsupplier.Show()
    End Sub
    Private Sub StokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles stokmenu.Click
        'fstok.Show()
    End Sub
    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem.Click
        'fpembelian.Show()
    End Sub
    Private Sub PenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem.Click
        'fpenjualan.Show()
        'fmodal.Show()
    End Sub
    Private Sub PembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem1.Click
        'flaporanpembelian.Show()
    End Sub
    Private Sub ReturPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'freturjual.Show()
    End Sub
    Private Sub NotaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'frmcarinota.Show()
    End Sub
    Private Sub PelunasanNotaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'fpelunasan.Show()
    End Sub
    Private Sub PenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem1.Click
        'flaporanpenjualan.Show()
    End Sub
    Private Sub InfoStokToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoStokToolStripMenuItem.Click
        'fminstok.Show()
    End Sub
    Private Sub ManageUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManageUserToolStripMenuItem.Click
        'fuser.Show()
    End Sub
    Private Sub fmenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
    Private Sub ReprintNotaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'freprint.Show()
    End Sub
    Private Sub PrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrinterToolStripMenuItem.Click
        'fsetprinter.Show()
    End Sub
    Private Sub PricelistGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PricelistGroupToolStripMenuItem.Click
        'fpricelist.Show()
    End Sub
    Private Sub ReprintFakturJualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReprintFakturJualToolStripMenuItem.Click
        'freprint.Show()
    End Sub
End Class
