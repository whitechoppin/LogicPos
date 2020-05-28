<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fbarangmasuk
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fbarangmasuk))
        Me.txttelp = New System.Windows.Forms.TextBox()
        Me.txtalamat = New System.Windows.Forms.RichTextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btncarisupplier = New System.Windows.Forms.Button()
        Me.cmbsupplier = New System.Windows.Forms.ComboBox()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.txtgudang = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.btncarigudang = New System.Windows.Forms.Button()
        Me.cmbgudang = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtbarangmasuk = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtnonota = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riteqty = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblsatuan = New System.Windows.Forms.Label()
        Me.btncari = New System.Windows.Forms.Button()
        Me.btntambahbarang = New System.Windows.Forms.Button()
        Me.txtbanyakbarang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnamabarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtkodebarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnprev = New System.Windows.Forms.Button()
        Me.btnnext = New System.Windows.Forms.Button()
        Me.txtgobarangmasuk = New System.Windows.Forms.TextBox()
        Me.btngo = New System.Windows.Forms.Button()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnbaru = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.btncarimasuk = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riteqty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txttelp
        '
        Me.txttelp.Location = New System.Drawing.Point(415, 88)
        Me.txttelp.Margin = New System.Windows.Forms.Padding(6)
        Me.txttelp.Name = "txttelp"
        Me.txttelp.Size = New System.Drawing.Size(252, 24)
        Me.txttelp.TabIndex = 60
        '
        'txtalamat
        '
        Me.txtalamat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtalamat.Location = New System.Drawing.Point(415, 14)
        Me.txtalamat.Margin = New System.Windows.Forms.Padding(6)
        Me.txtalamat.Name = "txtalamat"
        Me.txtalamat.Size = New System.Drawing.Size(252, 63)
        Me.txtalamat.TabIndex = 59
        Me.txtalamat.Text = ""
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label22.Location = New System.Drawing.Point(350, 91)
        Me.Label22.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(61, 18)
        Me.Label22.TabIndex = 58
        Me.Label22.Text = "Telepon"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label20.Location = New System.Drawing.Point(353, 17)
        Me.Label20.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 18)
        Me.Label20.TabIndex = 57
        Me.Label20.Text = "Alamat"
        '
        'btncarisupplier
        '
        Me.btncarisupplier.BackgroundImage = CType(resources.GetObject("btncarisupplier.BackgroundImage"), System.Drawing.Image)
        Me.btncarisupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarisupplier.ImageIndex = 0
        Me.btncarisupplier.Location = New System.Drawing.Point(309, 88)
        Me.btncarisupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.btncarisupplier.Name = "btncarisupplier"
        Me.btncarisupplier.Size = New System.Drawing.Size(34, 28)
        Me.btncarisupplier.TabIndex = 42
        Me.btncarisupplier.UseVisualStyleBackColor = True
        '
        'cmbsupplier
        '
        Me.cmbsupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsupplier.FormattingEnabled = True
        Me.cmbsupplier.Location = New System.Drawing.Point(114, 89)
        Me.cmbsupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbsupplier.MaxLength = 99
        Me.cmbsupplier.Name = "cmbsupplier"
        Me.cmbsupplier.Size = New System.Drawing.Size(191, 26)
        Me.cmbsupplier.TabIndex = 41
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(114, 51)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(229, 26)
        Me.cmbsales.TabIndex = 13
        '
        'txtgudang
        '
        Me.txtgudang.Location = New System.Drawing.Point(791, 89)
        Me.txtgudang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtgudang.Name = "txtgudang"
        Me.txtgudang.Size = New System.Drawing.Size(226, 24)
        Me.txtgudang.TabIndex = 16
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(677, 92)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 18)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Nama Gudang"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 55)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 18)
        Me.Label16.TabIndex = 37
        Me.Label16.Text = "Kode Sales"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(27, 93)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 18)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Supplier"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(1038, 83)
        Me.cbposted.Margin = New System.Windows.Forms.Padding(4)
        Me.cbposted.Name = "cbposted"
        Me.cbposted.Size = New System.Drawing.Size(74, 22)
        Me.cbposted.TabIndex = 41
        Me.cbposted.Text = "Posted"
        Me.cbposted.UseVisualStyleBackColor = True
        '
        'cbprinted
        '
        Me.cbprinted.AutoSize = True
        Me.cbprinted.Enabled = False
        Me.cbprinted.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbprinted.Location = New System.Drawing.Point(1038, 47)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(4)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'btncarigudang
        '
        Me.btncarigudang.BackgroundImage = CType(resources.GetObject("btncarigudang.BackgroundImage"), System.Drawing.Image)
        Me.btncarigudang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarigudang.ImageIndex = 0
        Me.btncarigudang.Location = New System.Drawing.Point(983, 51)
        Me.btncarigudang.Margin = New System.Windows.Forms.Padding(4)
        Me.btncarigudang.Name = "btncarigudang"
        Me.btncarigudang.Size = New System.Drawing.Size(34, 28)
        Me.btncarigudang.TabIndex = 15
        Me.btncarigudang.UseVisualStyleBackColor = True
        '
        'cmbgudang
        '
        Me.cmbgudang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbgudang.FormattingEnabled = True
        Me.cmbgudang.Location = New System.Drawing.Point(791, 52)
        Me.cmbgudang.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbgudang.MaxLength = 99
        Me.cmbgudang.Name = "cmbgudang"
        Me.cmbgudang.Size = New System.Drawing.Size(187, 26)
        Me.cmbgudang.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(680, 56)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 18)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Kode Gudang"
        '
        'dtbarangmasuk
        '
        Me.dtbarangmasuk.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dtbarangmasuk.Location = New System.Drawing.Point(791, 16)
        Me.dtbarangmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.dtbarangmasuk.Name = "dtbarangmasuk"
        Me.dtbarangmasuk.Size = New System.Drawing.Size(226, 23)
        Me.dtbarangmasuk.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label12.Location = New System.Drawing.Point(677, 19)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(109, 18)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Tanggal Masuk"
        '
        'txtnonota
        '
        Me.txtnonota.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonota.Location = New System.Drawing.Point(114, 14)
        Me.txtnonota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnonota.Name = "txtnonota"
        Me.txtnonota.Size = New System.Drawing.Size(229, 24)
        Me.txtnonota.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 16)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 18)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "No. Faktur"
        '
        'txtketerangan
        '
        Me.txtketerangan.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtketerangan.Location = New System.Drawing.Point(7, 7)
        Me.txtketerangan.Margin = New System.Windows.Forms.Padding(4)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(1128, 120)
        Me.txtketerangan.TabIndex = 46
        Me.txtketerangan.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 17)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(200, 31)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Barang Masuk"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Location = New System.Drawing.Point(13, 304)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riteqty})
        Me.GridControl1.Size = New System.Drawing.Size(1151, 356)
        Me.GridControl1.TabIndex = 45
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.Caption = "Kode Stok"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn2.AppearanceCell.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn2.AppearanceHeader.Options.UseFont = True
        Me.GridColumn2.Caption = "Kode Barang"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn3.AppearanceCell.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.Caption = "Nama Barang"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn4.AppearanceCell.Options.UseFont = True
        Me.GridColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn4.AppearanceHeader.Options.UseFont = True
        Me.GridColumn4.Caption = "Qty"
        Me.GridColumn4.ColumnEdit = Me.riteqty
        Me.GridColumn4.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'riteqty
        '
        Me.riteqty.AutoHeight = False
        Me.riteqty.MaxLength = 12
        Me.riteqty.Name = "riteqty"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceCell.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.Caption = "Satuan barang"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn6.AppearanceCell.Options.UseFont = True
        Me.GridColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn6.AppearanceHeader.Options.UseFont = True
        Me.GridColumn6.Caption = "Jenis barang"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'lblsatuan
        '
        Me.lblsatuan.AutoSize = True
        Me.lblsatuan.Location = New System.Drawing.Point(1026, 268)
        Me.lblsatuan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblsatuan.Name = "lblsatuan"
        Me.lblsatuan.Size = New System.Drawing.Size(54, 18)
        Me.lblsatuan.TabIndex = 25
        Me.lblsatuan.Text = "Satuan"
        '
        'btncari
        '
        Me.btncari.BackgroundImage = CType(resources.GetObject("btncari.BackgroundImage"), System.Drawing.Image)
        Me.btncari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncari.ImageIndex = 0
        Me.btncari.Location = New System.Drawing.Point(252, 263)
        Me.btncari.Margin = New System.Windows.Forms.Padding(4)
        Me.btncari.Name = "btncari"
        Me.btncari.Size = New System.Drawing.Size(34, 28)
        Me.btncari.TabIndex = 20
        Me.btncari.UseVisualStyleBackColor = True
        '
        'btntambahbarang
        '
        Me.btntambahbarang.BackgroundImage = CType(resources.GetObject("btntambahbarang.BackgroundImage"), System.Drawing.Image)
        Me.btntambahbarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambahbarang.ImageIndex = 0
        Me.btntambahbarang.Location = New System.Drawing.Point(1102, 247)
        Me.btntambahbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.btntambahbarang.Name = "btntambahbarang"
        Me.btntambahbarang.Size = New System.Drawing.Size(52, 44)
        Me.btntambahbarang.TabIndex = 24
        Me.btntambahbarang.UseVisualStyleBackColor = True
        '
        'txtbanyakbarang
        '
        Me.txtbanyakbarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbanyakbarang.Location = New System.Drawing.Point(960, 265)
        Me.txtbanyakbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtbanyakbarang.MaxLength = 12
        Me.txtbanyakbarang.Name = "txtbanyakbarang"
        Me.txtbanyakbarang.Size = New System.Drawing.Size(58, 24)
        Me.txtbanyakbarang.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(960, 242)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Qty"
        '
        'txtnamabarang
        '
        Me.txtnamabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabarang.Location = New System.Drawing.Point(298, 265)
        Me.txtnamabarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnamabarang.Name = "txtnamabarang"
        Me.txtnamabarang.Size = New System.Drawing.Size(640, 24)
        Me.txtnamabarang.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(299, 242)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Nama Barang"
        '
        'txtkodebarang
        '
        Me.txtkodebarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodebarang.Location = New System.Drawing.Point(14, 265)
        Me.txtkodebarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtkodebarang.Name = "txtkodebarang"
        Me.txtkodebarang.Size = New System.Drawing.Size(234, 24)
        Me.txtkodebarang.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 242)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kode Barang"
        '
        'btnprev
        '
        Me.btnprev.Location = New System.Drawing.Point(826, 24)
        Me.btnprev.Margin = New System.Windows.Forms.Padding(6)
        Me.btnprev.Name = "btnprev"
        Me.btnprev.Size = New System.Drawing.Size(35, 27)
        Me.btnprev.TabIndex = 5
        Me.btnprev.Text = "<<"
        Me.btnprev.UseVisualStyleBackColor = True
        '
        'btnnext
        '
        Me.btnnext.Location = New System.Drawing.Point(1129, 24)
        Me.btnnext.Margin = New System.Windows.Forms.Padding(6)
        Me.btnnext.Name = "btnnext"
        Me.btnnext.Size = New System.Drawing.Size(35, 27)
        Me.btnnext.TabIndex = 8
        Me.btnnext.Text = ">>"
        Me.btnnext.UseVisualStyleBackColor = True
        '
        'txtgobarangmasuk
        '
        Me.txtgobarangmasuk.Location = New System.Drawing.Point(865, 26)
        Me.txtgobarangmasuk.Margin = New System.Windows.Forms.Padding(6)
        Me.txtgobarangmasuk.Name = "txtgobarangmasuk"
        Me.txtgobarangmasuk.Size = New System.Drawing.Size(174, 24)
        Me.txtgobarangmasuk.TabIndex = 7
        '
        'btngo
        '
        Me.btngo.Location = New System.Drawing.Point(1076, 24)
        Me.btngo.Margin = New System.Windows.Forms.Padding(6)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(50, 27)
        Me.btngo.TabIndex = 6
        Me.btngo.Text = "Go"
        Me.btngo.UseVisualStyleBackColor = True
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(709, 21)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(6)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(110, 30)
        Me.btnbatal.TabIndex = 4
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(587, 21)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(6)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(110, 30)
        Me.btnedit.TabIndex = 1
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnbaru
        '
        Me.btnbaru.Location = New System.Drawing.Point(221, 21)
        Me.btnbaru.Margin = New System.Windows.Forms.Padding(6)
        Me.btnbaru.Name = "btnbaru"
        Me.btnbaru.Size = New System.Drawing.Size(110, 30)
        Me.btnbaru.TabIndex = 1
        Me.btnbaru.Text = "Baru"
        Me.btnbaru.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(465, 21)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(110, 30)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'btnsimpan
        '
        Me.btnsimpan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnsimpan.Location = New System.Drawing.Point(343, 21)
        Me.btnsimpan.Margin = New System.Windows.Forms.Padding(6)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(110, 30)
        Me.btnsimpan.TabIndex = 2
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'btncarimasuk
        '
        Me.btncarimasuk.BackgroundImage = CType(resources.GetObject("btncarimasuk.BackgroundImage"), System.Drawing.Image)
        Me.btncarimasuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarimasuk.ImageIndex = 0
        Me.btncarimasuk.Location = New System.Drawing.Point(1043, 24)
        Me.btncarimasuk.Margin = New System.Windows.Forms.Padding(6)
        Me.btncarimasuk.Name = "btncarimasuk"
        Me.btncarimasuk.Size = New System.Drawing.Size(30, 27)
        Me.btncarimasuk.TabIndex = 78
        Me.btncarimasuk.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(14, 73)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1150, 165)
        Me.TabControl1.TabIndex = 79
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage1.Controls.Add(Me.Label30)
        Me.TabPage1.Controls.Add(Me.cbposted)
        Me.TabPage1.Controls.Add(Me.cbprinted)
        Me.TabPage1.Controls.Add(Me.txtgudang)
        Me.TabPage1.Controls.Add(Me.txttelp)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.btncarisupplier)
        Me.TabPage1.Controls.Add(Me.btncarigudang)
        Me.TabPage1.Controls.Add(Me.txtalamat)
        Me.TabPage1.Controls.Add(Me.cmbgudang)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.dtbarangmasuk)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.txtnonota)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.cmbsupplier)
        Me.TabPage1.Controls.Add(Me.cmbsales)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1142, 134)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Barang Masuk"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label30.Location = New System.Drawing.Point(1048, 17)
        Me.Label30.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(50, 18)
        Me.Label30.TabIndex = 86
        Me.Label30.Text = "Status"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Controls.Add(Me.txtketerangan)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1142, 134)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Keterangan"
        '
        'fbarangmasuk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1176, 673)
        Me.Controls.Add(Me.lblsatuan)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btntambahbarang)
        Me.Controls.Add(Me.txtbanyakbarang)
        Me.Controls.Add(Me.btncari)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btncarimasuk)
        Me.Controls.Add(Me.txtgobarangmasuk)
        Me.Controls.Add(Me.btnprev)
        Me.Controls.Add(Me.btnnext)
        Me.Controls.Add(Me.txtnamabarang)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.txtkodebarang)
        Me.Controls.Add(Me.btngo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnbaru)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.GridControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fbarangmasuk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Barang Masuk"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riteqty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents txtgudang As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents btncarigudang As Button
    Friend WithEvents cmbgudang As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents dtbarangmasuk As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents txtnonota As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblsatuan As Label
    Friend WithEvents btncari As Button
    Friend WithEvents btntambahbarang As Button
    Friend WithEvents txtbanyakbarang As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtnamabarang As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtkodebarang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnprev As Button
    Friend WithEvents btnnext As Button
    Friend WithEvents txtgobarangmasuk As TextBox
    Friend WithEvents btngo As Button
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnbaru As Button
    Friend WithEvents btnprint As Button
    Friend WithEvents btnsimpan As Button
    Friend WithEvents btncarisupplier As Button
    Friend WithEvents cmbsupplier As ComboBox
    Friend WithEvents riteqty As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents txttelp As TextBox
    Friend WithEvents txtalamat As RichTextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents btncarimasuk As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label30 As Label
End Class
