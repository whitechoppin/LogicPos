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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mastermenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGudangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PricelistGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataSupplierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManageUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.exitStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.stokmenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.posmenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenjualanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.transaksimenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembelianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReprintFakturJualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransferGudangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.laporanmenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenjualanToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembelianToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoStokToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.settingmenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrinterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.statususer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusjam = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statustgl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PelunasanNotaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mastermenu, Me.stokmenu, Me.posmenu, Me.transaksimenu, Me.laporanmenu, Me.settingmenu})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(969, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mastermenu
        '
        Me.mastermenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataBarangToolStripMenuItem, Me.DataGudangToolStripMenuItem, Me.PricelistGroupToolStripMenuItem, Me.DataCustomerToolStripMenuItem, Me.DataSupplierToolStripMenuItem, Me.ManageUserToolStripMenuItem, Me.ToolStripSeparator1, Me.exitStripMenuItem})
        Me.mastermenu.Name = "mastermenu"
        Me.mastermenu.Size = New System.Drawing.Size(55, 20)
        Me.mastermenu.Text = "&Master"
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
        Me.DataGudangToolStripMenuItem.Text = "Data Gudang"
        '
        'PricelistGroupToolStripMenuItem
        '
        Me.PricelistGroupToolStripMenuItem.Name = "PricelistGroupToolStripMenuItem"
        Me.PricelistGroupToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.PricelistGroupToolStripMenuItem.Text = "Pricelist Group"
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
        Me.DataSupplierToolStripMenuItem.Text = "Data Supplier"
        '
        'ManageUserToolStripMenuItem
        '
        Me.ManageUserToolStripMenuItem.Name = "ManageUserToolStripMenuItem"
        Me.ManageUserToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ManageUserToolStripMenuItem.Text = "Manage &User"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(150, 6)
        '
        'exitStripMenuItem
        '
        Me.exitStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.exitStripMenuItem.Name = "exitStripMenuItem"
        Me.exitStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.exitStripMenuItem.Text = "E&xit"
        '
        'stokmenu
        '
        Me.stokmenu.Checked = True
        Me.stokmenu.CheckState = System.Windows.Forms.CheckState.Checked
        Me.stokmenu.Name = "stokmenu"
        Me.stokmenu.Size = New System.Drawing.Size(42, 20)
        Me.stokmenu.Text = "Stok"
        '
        'posmenu
        '
        Me.posmenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PenjualanToolStripMenuItem})
        Me.posmenu.Name = "posmenu"
        Me.posmenu.Size = New System.Drawing.Size(41, 20)
        Me.posmenu.Text = "&POS"
        '
        'PenjualanToolStripMenuItem
        '
        Me.PenjualanToolStripMenuItem.Name = "PenjualanToolStripMenuItem"
        Me.PenjualanToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PenjualanToolStripMenuItem.Text = "Pen&jualan"
        '
        'transaksimenu
        '
        Me.transaksimenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PembelianToolStripMenuItem, Me.ReprintFakturJualToolStripMenuItem, Me.TransferGudangToolStripMenuItem, Me.PelunasanNotaToolStripMenuItem})
        Me.transaksimenu.Name = "transaksimenu"
        Me.transaksimenu.Size = New System.Drawing.Size(66, 20)
        Me.transaksimenu.Text = "&Transaksi"
        Me.transaksimenu.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'PembelianToolStripMenuItem
        '
        Me.PembelianToolStripMenuItem.Name = "PembelianToolStripMenuItem"
        Me.PembelianToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.PembelianToolStripMenuItem.Text = "Pembelian"
        '
        'ReprintFakturJualToolStripMenuItem
        '
        Me.ReprintFakturJualToolStripMenuItem.Name = "ReprintFakturJualToolStripMenuItem"
        Me.ReprintFakturJualToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ReprintFakturJualToolStripMenuItem.Text = "Reprint Faktur Jual"
        '
        'TransferGudangToolStripMenuItem
        '
        Me.TransferGudangToolStripMenuItem.Name = "TransferGudangToolStripMenuItem"
        Me.TransferGudangToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.TransferGudangToolStripMenuItem.Text = "Transfer Gudang"
        '
        'laporanmenu
        '
        Me.laporanmenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PenjualanToolStripMenuItem1, Me.PembelianToolStripMenuItem1, Me.InfoStokToolStripMenuItem})
        Me.laporanmenu.Name = "laporanmenu"
        Me.laporanmenu.Size = New System.Drawing.Size(62, 20)
        Me.laporanmenu.Text = "Laporan"
        '
        'PenjualanToolStripMenuItem1
        '
        Me.PenjualanToolStripMenuItem1.Name = "PenjualanToolStripMenuItem1"
        Me.PenjualanToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.PenjualanToolStripMenuItem1.Text = "Penjualan"
        '
        'PembelianToolStripMenuItem1
        '
        Me.PembelianToolStripMenuItem1.Name = "PembelianToolStripMenuItem1"
        Me.PembelianToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.PembelianToolStripMenuItem1.Text = "Pembelian"
        '
        'InfoStokToolStripMenuItem
        '
        Me.InfoStokToolStripMenuItem.Name = "InfoStokToolStripMenuItem"
        Me.InfoStokToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.InfoStokToolStripMenuItem.Text = "Info Stok"
        '
        'settingmenu
        '
        Me.settingmenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrinterToolStripMenuItem})
        Me.settingmenu.Name = "settingmenu"
        Me.settingmenu.Size = New System.Drawing.Size(56, 20)
        Me.settingmenu.Text = "Setting"
        '
        'PrinterToolStripMenuItem
        '
        Me.PrinterToolStripMenuItem.Name = "PrinterToolStripMenuItem"
        Me.PrinterToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PrinterToolStripMenuItem.Text = "Printer"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statususer, Me.statusjam, Me.statustgl})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 304)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip1.Size = New System.Drawing.Size(969, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
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
        Me.statusjam.Size = New System.Drawing.Size(127, 17)
        Me.statusjam.Text = "jam"
        Me.statusjam.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'statustgl
        '
        Me.statustgl.Name = "statustgl"
        Me.statustgl.Padding = New System.Windows.Forms.Padding(50, 0, 0, 0)
        Me.statustgl.Size = New System.Drawing.Size(71, 17)
        Me.statustgl.Text = "tgl"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'PelunasanNotaToolStripMenuItem
        '
        Me.PelunasanNotaToolStripMenuItem.Name = "PelunasanNotaToolStripMenuItem"
        Me.PelunasanNotaToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.PelunasanNotaToolStripMenuItem.Text = "Pelunasan Nota"
        '
        'fmenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClientSize = New System.Drawing.Size(969, 326)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "fmenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Logic POS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mastermenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataBarangToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataCustomerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManageUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents exitStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents posmenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PenjualanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents transaksimenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PembelianToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents laporanmenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PenjualanToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PembelianToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents statususer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents statustgl As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents statusjam As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents stokmenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataSupplierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoStokToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents settingmenu As ToolStripMenuItem
    Friend WithEvents PrinterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PricelistGroupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReprintFakturJualToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataGudangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransferGudangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PelunasanNotaToolStripMenuItem As ToolStripMenuItem
End Class
