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
        Me.PenjualanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturPembelianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturPenjualanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarangMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarangKeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransferBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdministrasiMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PelunasanUtangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PelunasanPiutangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KasMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KasKeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransferKasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LaporanMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.LPenjualanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LPembelianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LUtangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LPiutangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LAkunMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LAkunKeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LTransferKasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LTransferBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LStokBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrinterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogicHouseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TokoSejatiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.statususer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusjam = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statustgl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuUtama.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuUtama
        '
        Me.MenuUtama.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuUtama.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterMenu, Me.TransaksiMenu, Me.AdministrasiMenu, Me.LaporanMenu, Me.SettingMenu, Me.AboutMenu})
        Me.MenuUtama.Location = New System.Drawing.Point(0, 0)
        Me.MenuUtama.Name = "MenuUtama"
        Me.MenuUtama.Size = New System.Drawing.Size(1050, 24)
        Me.MenuUtama.TabIndex = 0
        Me.MenuUtama.Text = "MenuStrip"
        '
        'MasterMenu
        '
        Me.MasterMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataBarangToolStripMenuItem, Me.DataGudangToolStripMenuItem, Me.DataCustomerToolStripMenuItem, Me.DataSupplierToolStripMenuItem, Me.DataUserToolStripMenuItem, Me.DataKasToolStripMenuItem, Me.PricelistGroupToolStripMenuItem, Me.ToolStripSeparator, Me.ExitStripMenuItem})
        Me.MasterMenu.Name = "MasterMenu"
        Me.MasterMenu.Size = New System.Drawing.Size(55, 20)
        Me.MasterMenu.Text = "&Master"
        '
        'DataBarangToolStripMenuItem
        '
        Me.DataBarangToolStripMenuItem.Name = "DataBarangToolStripMenuItem"
        Me.DataBarangToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DataBarangToolStripMenuItem.Text = "Data &Barang"
        '
        'DataGudangToolStripMenuItem
        '
        Me.DataGudangToolStripMenuItem.Name = "DataGudangToolStripMenuItem"
        Me.DataGudangToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DataGudangToolStripMenuItem.Text = "Data &Gudang"
        '
        'DataCustomerToolStripMenuItem
        '
        Me.DataCustomerToolStripMenuItem.Name = "DataCustomerToolStripMenuItem"
        Me.DataCustomerToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DataCustomerToolStripMenuItem.Text = "Data &Customer"
        '
        'DataSupplierToolStripMenuItem
        '
        Me.DataSupplierToolStripMenuItem.Name = "DataSupplierToolStripMenuItem"
        Me.DataSupplierToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DataSupplierToolStripMenuItem.Text = "Data &Supplier"
        '
        'DataUserToolStripMenuItem
        '
        Me.DataUserToolStripMenuItem.Name = "DataUserToolStripMenuItem"
        Me.DataUserToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DataUserToolStripMenuItem.Text = "Data &User"
        '
        'DataKasToolStripMenuItem
        '
        Me.DataKasToolStripMenuItem.Name = "DataKasToolStripMenuItem"
        Me.DataKasToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DataKasToolStripMenuItem.Text = "Data &Kas"
        '
        'PricelistGroupToolStripMenuItem
        '
        Me.PricelistGroupToolStripMenuItem.Name = "PricelistGroupToolStripMenuItem"
        Me.PricelistGroupToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.PricelistGroupToolStripMenuItem.Text = "Pricelist Group"
        '
        'ToolStripSeparator
        '
        Me.ToolStripSeparator.Name = "ToolStripSeparator"
        Me.ToolStripSeparator.Size = New System.Drawing.Size(150, 6)
        '
        'ExitStripMenuItem
        '
        Me.ExitStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.ExitStripMenuItem.Name = "ExitStripMenuItem"
        Me.ExitStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ExitStripMenuItem.Text = "E&xit"
        '
        'TransaksiMenu
        '
        Me.TransaksiMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PembelianToolStripMenuItem, Me.PenjualanToolStripMenuItem, Me.ReturPembelianToolStripMenuItem, Me.ReturPenjualanToolStripMenuItem, Me.BarangMasukToolStripMenuItem, Me.BarangKeluarToolStripMenuItem, Me.TransferBarangToolStripMenuItem})
        Me.TransaksiMenu.Name = "TransaksiMenu"
        Me.TransaksiMenu.Size = New System.Drawing.Size(66, 20)
        Me.TransaksiMenu.Text = "&Transaksi"
        Me.TransaksiMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'PembelianToolStripMenuItem
        '
        Me.PembelianToolStripMenuItem.Name = "PembelianToolStripMenuItem"
        Me.PembelianToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.PembelianToolStripMenuItem.Text = "&Pembelian"
        '
        'PenjualanToolStripMenuItem
        '
        Me.PenjualanToolStripMenuItem.Name = "PenjualanToolStripMenuItem"
        Me.PenjualanToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.PenjualanToolStripMenuItem.Text = "&Penjualan"
        '
        'ReturPembelianToolStripMenuItem
        '
        Me.ReturPembelianToolStripMenuItem.Name = "ReturPembelianToolStripMenuItem"
        Me.ReturPembelianToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ReturPembelianToolStripMenuItem.Text = "&Retur Pembelian"
        '
        'ReturPenjualanToolStripMenuItem
        '
        Me.ReturPenjualanToolStripMenuItem.Name = "ReturPenjualanToolStripMenuItem"
        Me.ReturPenjualanToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ReturPenjualanToolStripMenuItem.Text = "&Retur Penjualan"
        '
        'BarangMasukToolStripMenuItem
        '
        Me.BarangMasukToolStripMenuItem.Name = "BarangMasukToolStripMenuItem"
        Me.BarangMasukToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.BarangMasukToolStripMenuItem.Text = "Barang Masuk"
        '
        'BarangKeluarToolStripMenuItem
        '
        Me.BarangKeluarToolStripMenuItem.Name = "BarangKeluarToolStripMenuItem"
        Me.BarangKeluarToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.BarangKeluarToolStripMenuItem.Text = "Barang Keluar"
        '
        'TransferBarangToolStripMenuItem
        '
        Me.TransferBarangToolStripMenuItem.Name = "TransferBarangToolStripMenuItem"
        Me.TransferBarangToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.TransferBarangToolStripMenuItem.Text = "Transfer Barang"
        '
        'AdministrasiMenu
        '
        Me.AdministrasiMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PelunasanUtangToolStripMenuItem, Me.PelunasanPiutangToolStripMenuItem, Me.KasMasukToolStripMenuItem, Me.KasKeluarToolStripMenuItem, Me.TransferKasToolStripMenuItem})
        Me.AdministrasiMenu.Name = "AdministrasiMenu"
        Me.AdministrasiMenu.Size = New System.Drawing.Size(85, 20)
        Me.AdministrasiMenu.Text = "&Administrasi"
        '
        'PelunasanUtangToolStripMenuItem
        '
        Me.PelunasanUtangToolStripMenuItem.Name = "PelunasanUtangToolStripMenuItem"
        Me.PelunasanUtangToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.PelunasanUtangToolStripMenuItem.Text = "Pelunasan &Utang"
        '
        'PelunasanPiutangToolStripMenuItem
        '
        Me.PelunasanPiutangToolStripMenuItem.Name = "PelunasanPiutangToolStripMenuItem"
        Me.PelunasanPiutangToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.PelunasanPiutangToolStripMenuItem.Text = "Pelunasan Piutang"
        '
        'KasMasukToolStripMenuItem
        '
        Me.KasMasukToolStripMenuItem.Name = "KasMasukToolStripMenuItem"
        Me.KasMasukToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.KasMasukToolStripMenuItem.Text = "Kas Masuk"
        '
        'KasKeluarToolStripMenuItem
        '
        Me.KasKeluarToolStripMenuItem.Name = "KasKeluarToolStripMenuItem"
        Me.KasKeluarToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.KasKeluarToolStripMenuItem.Text = "Kas Keluar"
        '
        'TransferKasToolStripMenuItem
        '
        Me.TransferKasToolStripMenuItem.Name = "TransferKasToolStripMenuItem"
        Me.TransferKasToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.TransferKasToolStripMenuItem.Text = "Transfer Kas"
        '
        'LaporanMenu
        '
        Me.LaporanMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LPenjualanToolStripMenuItem, Me.LPembelianToolStripMenuItem, Me.LUtangToolStripMenuItem, Me.LPiutangToolStripMenuItem, Me.LAkunMasukToolStripMenuItem, Me.LAkunKeluarToolStripMenuItem, Me.LTransferKasToolStripMenuItem, Me.LTransferBarangToolStripMenuItem, Me.LStokBarangToolStripMenuItem})
        Me.LaporanMenu.Name = "LaporanMenu"
        Me.LaporanMenu.Size = New System.Drawing.Size(62, 20)
        Me.LaporanMenu.Text = "&Laporan"
        '
        'LPenjualanToolStripMenuItem
        '
        Me.LPenjualanToolStripMenuItem.Name = "LPenjualanToolStripMenuItem"
        Me.LPenjualanToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LPenjualanToolStripMenuItem.Text = "Laporan Penjualan"
        '
        'LPembelianToolStripMenuItem
        '
        Me.LPembelianToolStripMenuItem.Name = "LPembelianToolStripMenuItem"
        Me.LPembelianToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LPembelianToolStripMenuItem.Text = "Laporan Pembelian"
        '
        'LUtangToolStripMenuItem
        '
        Me.LUtangToolStripMenuItem.Name = "LUtangToolStripMenuItem"
        Me.LUtangToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LUtangToolStripMenuItem.Text = "Laporan Utang"
        '
        'LPiutangToolStripMenuItem
        '
        Me.LPiutangToolStripMenuItem.Name = "LPiutangToolStripMenuItem"
        Me.LPiutangToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LPiutangToolStripMenuItem.Text = "Laporan Piutang"
        '
        'LAkunMasukToolStripMenuItem
        '
        Me.LAkunMasukToolStripMenuItem.Name = "LAkunMasukToolStripMenuItem"
        Me.LAkunMasukToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LAkunMasukToolStripMenuItem.Text = "Laporan Akun Masuk"
        '
        'LAkunKeluarToolStripMenuItem
        '
        Me.LAkunKeluarToolStripMenuItem.Name = "LAkunKeluarToolStripMenuItem"
        Me.LAkunKeluarToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LAkunKeluarToolStripMenuItem.Text = "Laporan Akun Keluar"
        '
        'LTransferKasToolStripMenuItem
        '
        Me.LTransferKasToolStripMenuItem.Name = "LTransferKasToolStripMenuItem"
        Me.LTransferKasToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LTransferKasToolStripMenuItem.Text = "Laporan Transfer Kas"
        '
        'LTransferBarangToolStripMenuItem
        '
        Me.LTransferBarangToolStripMenuItem.Name = "LTransferBarangToolStripMenuItem"
        Me.LTransferBarangToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LTransferBarangToolStripMenuItem.Text = "Laporan Transfer Barang"
        '
        'LStokBarangToolStripMenuItem
        '
        Me.LStokBarangToolStripMenuItem.Name = "LStokBarangToolStripMenuItem"
        Me.LStokBarangToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.LStokBarangToolStripMenuItem.Text = "Laporan Stok Barang"
        '
        'SettingMenu
        '
        Me.SettingMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrinterToolStripMenuItem})
        Me.SettingMenu.Name = "SettingMenu"
        Me.SettingMenu.Size = New System.Drawing.Size(56, 20)
        Me.SettingMenu.Text = "&Setting"
        '
        'PrinterToolStripMenuItem
        '
        Me.PrinterToolStripMenuItem.Name = "PrinterToolStripMenuItem"
        Me.PrinterToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.PrinterToolStripMenuItem.Text = "Printer"
        '
        'AboutMenu
        '
        Me.AboutMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogicHouseToolStripMenuItem, Me.TokoSejatiToolStripMenuItem})
        Me.AboutMenu.Name = "AboutMenu"
        Me.AboutMenu.Size = New System.Drawing.Size(52, 20)
        Me.AboutMenu.Text = "Abou&t"
        '
        'LogicHouseToolStripMenuItem
        '
        Me.LogicHouseToolStripMenuItem.Name = "LogicHouseToolStripMenuItem"
        Me.LogicHouseToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.LogicHouseToolStripMenuItem.Text = "Logic House"
        '
        'TokoSejatiToolStripMenuItem
        '
        Me.TokoSejatiToolStripMenuItem.Name = "TokoSejatiToolStripMenuItem"
        Me.TokoSejatiToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.TokoSejatiToolStripMenuItem.Text = "Toko Sejati"
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statususer, Me.statusjam, Me.statustgl})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 581)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip.Size = New System.Drawing.Size(1050, 22)
        Me.StatusStrip.TabIndex = 1
        Me.StatusStrip.Text = "StatusStrip"
        '
        'statususer
        '
        Me.statususer.Name = "statususer"
        Me.statususer.Padding = New System.Windows.Forms.Padding(100, 0, 0, 0)
        Me.statususer.Size = New System.Drawing.Size(167, 17)
        Me.statususer.Text = "Nama Kasir"
        '
        'statusjam
        '
        Me.statusjam.Name = "statusjam"
        Me.statusjam.Padding = New System.Windows.Forms.Padding(100, 0, 0, 0)
        Me.statusjam.Size = New System.Drawing.Size(128, 17)
        Me.statusjam.Text = "Jam"
        Me.statusjam.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'statustgl
        '
        Me.statustgl.Name = "statustgl"
        Me.statustgl.Padding = New System.Windows.Forms.Padding(50, 0, 0, 0)
        Me.statustgl.Size = New System.Drawing.Size(98, 17)
        Me.statustgl.Text = "Tanggal"
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'fmenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClientSize = New System.Drawing.Size(1050, 603)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuUtama)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuUtama
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
    Friend WithEvents LPenjualanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LPembelianToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents statususer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents statustgl As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents statusjam As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DataSupplierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LStokBarangToolStripMenuItem As ToolStripMenuItem
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
    Friend WithEvents PelunasanUtangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PelunasanPiutangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KasMasukToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KasKeluarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransferKasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LUtangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LPiutangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LAkunMasukToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LAkunKeluarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LTransferKasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LTransferBarangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutMenu As ToolStripMenuItem
    Friend WithEvents LogicHouseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TokoSejatiToolStripMenuItem As ToolStripMenuItem
End Class
