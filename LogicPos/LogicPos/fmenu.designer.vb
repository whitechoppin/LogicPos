<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fmenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fmenu))
        Me.MenuUtama = New System.Windows.Forms.MenuStrip()
        Me.MasterMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGudangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSupplierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataKasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PricelistGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransaksiMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembelianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransferBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LaporanMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenjualanToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembelianToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoStokToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrinterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.statususer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusjam = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statustgl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.PenjualanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturPenjualanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturPembelianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarangMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarangKeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdministrasiMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuUtama.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuUtama
        '
        Me.MenuUtama.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuUtama.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterMenu, Me.TransaksiMenu, Me.AdministrasiMenu, Me.LaporanMenu, Me.SettingMenu})
        Me.MenuUtama.Location = New System.Drawing.Point(0, 0)
        Me.MenuUtama.Name = "MenuUtama"
        Me.MenuUtama.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuUtama.Size = New System.Drawing.Size(1292, 28)
        Me.MenuUtama.TabIndex = 0
        Me.MenuUtama.Text = "MenuStrip"
        '
        'MasterMenu
        '
        Me.MasterMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataBarangToolStripMenuItem, Me.DataGudangToolStripMenuItem, Me.DataCustomerToolStripMenuItem, Me.DataSupplierToolStripMenuItem, Me.DataUserToolStripMenuItem, Me.DataKasToolStripMenuItem, Me.PricelistGroupToolStripMenuItem, Me.ToolStripSeparator, Me.ExitStripMenuItem})
        Me.MasterMenu.Name = "MasterMenu"
        Me.MasterMenu.Size = New System.Drawing.Size(66, 24)
        Me.MasterMenu.Text = "&Master"
        '
        'DataBarangToolStripMenuItem
        '
        Me.DataBarangToolStripMenuItem.Name = "DataBarangToolStripMenuItem"
        Me.DataBarangToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.DataBarangToolStripMenuItem.Text = "Data &Barang"
        '
        'DataGudangToolStripMenuItem
        '
        Me.DataGudangToolStripMenuItem.Name = "DataGudangToolStripMenuItem"
        Me.DataGudangToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.DataGudangToolStripMenuItem.Text = "Data &Gudang"
        '
        'DataCustomerToolStripMenuItem
        '
        Me.DataCustomerToolStripMenuItem.Name = "DataCustomerToolStripMenuItem"
        Me.DataCustomerToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.DataCustomerToolStripMenuItem.Text = "Data &Customer"
        '
        'DataSupplierToolStripMenuItem
        '
        Me.DataSupplierToolStripMenuItem.Name = "DataSupplierToolStripMenuItem"
        Me.DataSupplierToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.DataSupplierToolStripMenuItem.Text = "Data &Supplier"
        '
        'DataUserToolStripMenuItem
        '
        Me.DataUserToolStripMenuItem.Name = "DataUserToolStripMenuItem"
        Me.DataUserToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.DataUserToolStripMenuItem.Text = "Data &User"
        '
        'DataKasToolStripMenuItem
        '
        Me.DataKasToolStripMenuItem.Name = "DataKasToolStripMenuItem"
        Me.DataKasToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.DataKasToolStripMenuItem.Text = "Data &Kas"
        '
        'PricelistGroupToolStripMenuItem
        '
        Me.PricelistGroupToolStripMenuItem.Name = "PricelistGroupToolStripMenuItem"
        Me.PricelistGroupToolStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.PricelistGroupToolStripMenuItem.Text = "Pricelist Group"
        '
        'ToolStripSeparator
        '
        Me.ToolStripSeparator.Name = "ToolStripSeparator"
        Me.ToolStripSeparator.Size = New System.Drawing.Size(180, 6)
        '
        'ExitStripMenuItem
        '
        Me.ExitStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.ExitStripMenuItem.Name = "ExitStripMenuItem"
        Me.ExitStripMenuItem.Size = New System.Drawing.Size(183, 26)
        Me.ExitStripMenuItem.Text = "E&xit"
        '
        'TransaksiMenu
        '
        Me.TransaksiMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PembelianToolStripMenuItem, Me.PenjualanToolStripMenuItem, Me.ReturPembelianToolStripMenuItem, Me.ReturPenjualanToolStripMenuItem, Me.BarangMasukToolStripMenuItem, Me.BarangKeluarToolStripMenuItem, Me.TransferBarangToolStripMenuItem})
        Me.TransaksiMenu.Name = "TransaksiMenu"
        Me.TransaksiMenu.Size = New System.Drawing.Size(80, 24)
        Me.TransaksiMenu.Text = "&Transaksi"
        Me.TransaksiMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'PembelianToolStripMenuItem
        '
        Me.PembelianToolStripMenuItem.Name = "PembelianToolStripMenuItem"
        Me.PembelianToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.PembelianToolStripMenuItem.Text = "&Pembelian"
        '
        'TransferBarangToolStripMenuItem
        '
        Me.TransferBarangToolStripMenuItem.Name = "TransferBarangToolStripMenuItem"
        Me.TransferBarangToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.TransferBarangToolStripMenuItem.Text = "Transfer Barang"
        '
        'LaporanMenu
        '
        Me.LaporanMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PenjualanToolStripMenuItem1, Me.PembelianToolStripMenuItem1, Me.InfoStokToolStripMenuItem})
        Me.LaporanMenu.Name = "LaporanMenu"
        Me.LaporanMenu.Size = New System.Drawing.Size(75, 24)
        Me.LaporanMenu.Text = "Laporan"
        '
        'PenjualanToolStripMenuItem1
        '
        Me.PenjualanToolStripMenuItem1.Name = "PenjualanToolStripMenuItem1"
        Me.PenjualanToolStripMenuItem1.Size = New System.Drawing.Size(181, 26)
        Me.PenjualanToolStripMenuItem1.Text = "Penjualan"
        '
        'PembelianToolStripMenuItem1
        '
        Me.PembelianToolStripMenuItem1.Name = "PembelianToolStripMenuItem1"
        Me.PembelianToolStripMenuItem1.Size = New System.Drawing.Size(181, 26)
        Me.PembelianToolStripMenuItem1.Text = "Pembelian"
        '
        'InfoStokToolStripMenuItem
        '
        Me.InfoStokToolStripMenuItem.Name = "InfoStokToolStripMenuItem"
        Me.InfoStokToolStripMenuItem.Size = New System.Drawing.Size(181, 26)
        Me.InfoStokToolStripMenuItem.Text = "Info Stok"
        '
        'SettingMenu
        '
        Me.SettingMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrinterToolStripMenuItem})
        Me.SettingMenu.Name = "SettingMenu"
        Me.SettingMenu.Size = New System.Drawing.Size(68, 24)
        Me.SettingMenu.Text = "Setting"
        '
        'PrinterToolStripMenuItem
        '
        Me.PrinterToolStripMenuItem.Name = "PrinterToolStripMenuItem"
        Me.PrinterToolStripMenuItem.Size = New System.Drawing.Size(127, 26)
        Me.PrinterToolStripMenuItem.Text = "Printer"
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statususer, Me.statusjam, Me.statustgl})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 376)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip.Size = New System.Drawing.Size(1292, 25)
        Me.StatusStrip.TabIndex = 1
        Me.StatusStrip.Text = "StatusStrip"
        '
        'statususer
        '
        Me.statususer.Name = "statususer"
        Me.statususer.Padding = New System.Windows.Forms.Padding(100, 0, 0, 0)
        Me.statususer.Size = New System.Drawing.Size(185, 20)
        Me.statususer.Text = "Nama Kasir"
        '
        'statusjam
        '
        Me.statusjam.Name = "statusjam"
        Me.statusjam.Padding = New System.Windows.Forms.Padding(100, 0, 0, 0)
        Me.statusjam.Size = New System.Drawing.Size(135, 20)
        Me.statusjam.Text = "Jam"
        Me.statusjam.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'statustgl
        '
        Me.statustgl.Name = "statustgl"
        Me.statustgl.Padding = New System.Windows.Forms.Padding(50, 0, 0, 0)
        Me.statustgl.Size = New System.Drawing.Size(111, 20)
        Me.statustgl.Text = "Tanggal"
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'PenjualanToolStripMenuItem
        '
        Me.PenjualanToolStripMenuItem.Name = "PenjualanToolStripMenuItem"
        Me.PenjualanToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.PenjualanToolStripMenuItem.Text = "&Penjualan"
        '
        'ReturPenjualanToolStripMenuItem
        '
        Me.ReturPenjualanToolStripMenuItem.Name = "ReturPenjualanToolStripMenuItem"
        Me.ReturPenjualanToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.ReturPenjualanToolStripMenuItem.Text = "&Retur Penjualan"
        '
        'ReturPembelianToolStripMenuItem
        '
        Me.ReturPembelianToolStripMenuItem.Name = "ReturPembelianToolStripMenuItem"
        Me.ReturPembelianToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.ReturPembelianToolStripMenuItem.Text = "&ReturPembelian"
        '
        'BarangMasukToolStripMenuItem
        '
        Me.BarangMasukToolStripMenuItem.Name = "BarangMasukToolStripMenuItem"
        Me.BarangMasukToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.BarangMasukToolStripMenuItem.Text = "Barang Masuk"
        '
        'BarangKeluarToolStripMenuItem
        '
        Me.BarangKeluarToolStripMenuItem.Name = "BarangKeluarToolStripMenuItem"
        Me.BarangKeluarToolStripMenuItem.Size = New System.Drawing.Size(188, 26)
        Me.BarangKeluarToolStripMenuItem.Text = "Barang Keluar"
        '
        'AdministrasiMenu
        '
        Me.AdministrasiMenu.Name = "AdministrasiMenu"
        Me.AdministrasiMenu.Size = New System.Drawing.Size(103, 24)
        Me.AdministrasiMenu.Text = "&Administrasi"
        '
        'fmenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClientSize = New System.Drawing.Size(1292, 401)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuUtama)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuUtama
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fmenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Logic POS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuUtama.ResumeLayout(False)
        Me.MenuUtama.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuUtama As System.Windows.Forms.MenuStrip
    Friend WithEvents MasterMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataBarangToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataCustomerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TransaksiMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PembelianToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LaporanMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PenjualanToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PembelianToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents statususer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents statustgl As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents statusjam As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DataSupplierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoStokToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingMenu As ToolStripMenuItem
    Friend WithEvents PrinterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PricelistGroupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataGudangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransferBarangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataKasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PenjualanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReturPenjualanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReturPembelianToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BarangMasukToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BarangKeluarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdministrasiMenu As ToolStripMenuItem
End Class
