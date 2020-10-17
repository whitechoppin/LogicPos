<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ftransferbarang
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ftransferbarang))
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.btnprev = New System.Windows.Forms.Button()
        Me.btnnext = New System.Windows.Forms.Button()
        Me.txtgotransferbarang = New System.Windows.Forms.TextBox()
        Me.btngo = New System.Windows.Forms.Button()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnbaru = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtdarigudang = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btncaridarigudang = New System.Windows.Forms.Button()
        Me.cmbdarigudang = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.txtkegudang = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.btncarikegudang = New System.Windows.Forms.Button()
        Me.cmbkegudang = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dttransferbarang = New System.Windows.Forms.DateTimePicker()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtnonota = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ritebanyak = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtkodebarang = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblsatuan = New System.Windows.Forms.Label()
        Me.btncaribarang = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.txtbanyak = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnamabarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtkodestok = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btncaritransfer = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ritebanyak, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtketerangan
        '
        Me.txtketerangan.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtketerangan.Location = New System.Drawing.Point(9, 9)
        Me.txtketerangan.Margin = New System.Windows.Forms.Padding(6)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(1119, 106)
        Me.txtketerangan.TabIndex = 67
        Me.txtketerangan.Text = ""
        '
        'btnprev
        '
        Me.btnprev.Location = New System.Drawing.Point(802, 19)
        Me.btnprev.Margin = New System.Windows.Forms.Padding(6)
        Me.btnprev.Name = "btnprev"
        Me.btnprev.Size = New System.Drawing.Size(35, 27)
        Me.btnprev.TabIndex = 5
        Me.btnprev.Text = "<<"
        Me.btnprev.UseVisualStyleBackColor = True
        '
        'btnnext
        '
        Me.btnnext.Location = New System.Drawing.Point(1107, 19)
        Me.btnnext.Margin = New System.Windows.Forms.Padding(6)
        Me.btnnext.Name = "btnnext"
        Me.btnnext.Size = New System.Drawing.Size(35, 27)
        Me.btnnext.TabIndex = 8
        Me.btnnext.Text = ">>"
        Me.btnnext.UseVisualStyleBackColor = True
        '
        'txtgotransferbarang
        '
        Me.txtgotransferbarang.Location = New System.Drawing.Point(841, 21)
        Me.txtgotransferbarang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtgotransferbarang.Name = "txtgotransferbarang"
        Me.txtgotransferbarang.Size = New System.Drawing.Size(174, 24)
        Me.txtgotransferbarang.TabIndex = 7
        '
        'btngo
        '
        Me.btngo.Location = New System.Drawing.Point(1053, 19)
        Me.btngo.Margin = New System.Windows.Forms.Padding(6)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(50, 27)
        Me.btngo.TabIndex = 6
        Me.btngo.Text = "Go"
        Me.btngo.UseVisualStyleBackColor = True
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(687, 17)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(6)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(110, 30)
        Me.btnbatal.TabIndex = 4
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(574, 17)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(6)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(110, 30)
        Me.btnedit.TabIndex = 1
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnbaru
        '
        Me.btnbaru.Location = New System.Drawing.Point(235, 17)
        Me.btnbaru.Margin = New System.Windows.Forms.Padding(6)
        Me.btnbaru.Name = "btnbaru"
        Me.btnbaru.Size = New System.Drawing.Size(110, 30)
        Me.btnbaru.TabIndex = 1
        Me.btnbaru.Text = "Baru"
        Me.btnbaru.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(461, 17)
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
        Me.btnsimpan.Location = New System.Drawing.Point(348, 17)
        Me.btnsimpan.Margin = New System.Windows.Forms.Padding(6)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(110, 30)
        Me.btnsimpan.TabIndex = 2
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label8.Location = New System.Drawing.Point(854, 15)
        Me.Label8.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 18)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Ke Gudang"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label6.Location = New System.Drawing.Point(477, 15)
        Me.Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 18)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "Dari Gudang"
        '
        'txtdarigudang
        '
        Me.txtdarigudang.Enabled = False
        Me.txtdarigudang.Location = New System.Drawing.Point(440, 86)
        Me.txtdarigudang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtdarigudang.Name = "txtdarigudang"
        Me.txtdarigudang.Size = New System.Drawing.Size(234, 24)
        Me.txtdarigudang.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(331, 89)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 18)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Nama Gudang"
        '
        'btncaridarigudang
        '
        Me.btncaridarigudang.BackgroundImage = CType(resources.GetObject("btncaridarigudang.BackgroundImage"), System.Drawing.Image)
        Me.btncaridarigudang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaridarigudang.ImageIndex = 0
        Me.btncaridarigudang.Location = New System.Drawing.Point(641, 49)
        Me.btncaridarigudang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaridarigudang.Name = "btncaridarigudang"
        Me.btncaridarigudang.Size = New System.Drawing.Size(33, 27)
        Me.btncaridarigudang.TabIndex = 42
        Me.btncaridarigudang.UseVisualStyleBackColor = True
        '
        'cmbdarigudang
        '
        Me.cmbdarigudang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbdarigudang.FormattingEnabled = True
        Me.cmbdarigudang.Location = New System.Drawing.Point(440, 50)
        Me.cmbdarigudang.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbdarigudang.MaxLength = 99
        Me.cmbdarigudang.Name = "cmbdarigudang"
        Me.cmbdarigudang.Size = New System.Drawing.Size(198, 26)
        Me.cmbdarigudang.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(331, 53)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 18)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Kode Gudang"
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(109, 48)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(215, 26)
        Me.cmbsales.TabIndex = 13
        '
        'txtkegudang
        '
        Me.txtkegudang.Enabled = False
        Me.txtkegudang.Location = New System.Drawing.Point(794, 86)
        Me.txtkegudang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkegudang.Name = "txtkegudang"
        Me.txtkegudang.Size = New System.Drawing.Size(235, 24)
        Me.txtkegudang.TabIndex = 16
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label18.Location = New System.Drawing.Point(685, 89)
        Me.Label18.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 18)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Nama Gudang"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label16.Location = New System.Drawing.Point(9, 51)
        Me.Label16.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 18)
        Me.Label16.TabIndex = 37
        Me.Label16.Text = " Kode Sales"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(1044, 86)
        Me.cbposted.Margin = New System.Windows.Forms.Padding(6)
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
        Me.cbprinted.Location = New System.Drawing.Point(1044, 51)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(6)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'btncarikegudang
        '
        Me.btncarikegudang.BackgroundImage = CType(resources.GetObject("btncarikegudang.BackgroundImage"), System.Drawing.Image)
        Me.btncarikegudang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarikegudang.ImageIndex = 0
        Me.btncarikegudang.Location = New System.Drawing.Point(996, 50)
        Me.btncarikegudang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncarikegudang.Name = "btncarikegudang"
        Me.btncarikegudang.Size = New System.Drawing.Size(33, 27)
        Me.btncarikegudang.TabIndex = 15
        Me.btncarikegudang.UseVisualStyleBackColor = True
        '
        'cmbkegudang
        '
        Me.cmbkegudang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbkegudang.FormattingEnabled = True
        Me.cmbkegudang.Location = New System.Drawing.Point(794, 51)
        Me.cmbkegudang.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbkegudang.MaxLength = 99
        Me.cmbkegudang.Name = "cmbkegudang"
        Me.cmbkegudang.Size = New System.Drawing.Size(199, 26)
        Me.cmbkegudang.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label14.Location = New System.Drawing.Point(685, 56)
        Me.Label14.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 18)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Kode Gudang"
        '
        'dttransferbarang
        '
        Me.dttransferbarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dttransferbarang.Location = New System.Drawing.Point(109, 86)
        Me.dttransferbarang.Margin = New System.Windows.Forms.Padding(6)
        Me.dttransferbarang.Name = "dttransferbarang"
        Me.dttransferbarang.Size = New System.Drawing.Size(215, 23)
        Me.dttransferbarang.TabIndex = 17
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label17.Location = New System.Drawing.Point(33, 89)
        Me.Label17.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(60, 18)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Tanggal"
        '
        'txtnonota
        '
        Me.txtnonota.Enabled = False
        Me.txtnonota.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonota.Location = New System.Drawing.Point(109, 12)
        Me.txtnonota.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnonota.Name = "txtnonota"
        Me.txtnonota.Size = New System.Drawing.Size(215, 24)
        Me.txtnonota.TabIndex = 9
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label19.Location = New System.Drawing.Point(21, 18)
        Me.Label19.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(78, 18)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "No. Faktur"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 17)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(225, 31)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "Transfer Barang"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6)
        Me.GridControl1.Location = New System.Drawing.Point(17, 286)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(6)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ritebanyak})
        Me.GridControl1.Size = New System.Drawing.Size(1145, 262)
        Me.GridControl1.TabIndex = 63
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.Caption = "Kode Barang"
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
        Me.GridColumn2.Caption = "Kode Stok"
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
        Me.GridColumn4.Caption = "Banyak"
        Me.GridColumn4.ColumnEdit = Me.ritebanyak
        Me.GridColumn4.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'ritebanyak
        '
        Me.ritebanyak.AutoHeight = False
        Me.ritebanyak.MaxLength = 12
        Me.ritebanyak.Name = "ritebanyak"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceCell.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.Caption = "Satuan Barang"
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
        Me.GridColumn6.Caption = "Jenis Barang"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn7.AppearanceCell.Options.UseFont = True
        Me.GridColumn7.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn7.AppearanceHeader.Options.UseFont = True
        Me.GridColumn7.Caption = "id barang"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn8.AppearanceCell.Options.UseFont = True
        Me.GridColumn8.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn8.AppearanceHeader.Options.UseFont = True
        Me.GridColumn8.Caption = "id dari stok"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn9.AppearanceCell.Options.UseFont = True
        Me.GridColumn9.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn9.AppearanceHeader.Options.UseFont = True
        Me.GridColumn9.Caption = "id ke stok"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        '
        'txtkodebarang
        '
        Me.txtkodebarang.Enabled = False
        Me.txtkodebarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodebarang.Location = New System.Drawing.Point(267, 250)
        Me.txtkodebarang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkodebarang.Name = "txtkodebarang"
        Me.txtkodebarang.Size = New System.Drawing.Size(198, 24)
        Me.txtkodebarang.TabIndex = 30
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label23.Location = New System.Drawing.Point(264, 225)
        Me.Label23.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 18)
        Me.Label23.TabIndex = 29
        Me.Label23.Text = "Kode Barang"
        '
        'lblsatuan
        '
        Me.lblsatuan.AutoSize = True
        Me.lblsatuan.Location = New System.Drawing.Point(1031, 253)
        Me.lblsatuan.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblsatuan.Name = "lblsatuan"
        Me.lblsatuan.Size = New System.Drawing.Size(54, 18)
        Me.lblsatuan.TabIndex = 27
        Me.lblsatuan.Text = "Satuan"
        '
        'btncaribarang
        '
        Me.btncaribarang.BackgroundImage = CType(resources.GetObject("btncaribarang.BackgroundImage"), System.Drawing.Image)
        Me.btncaribarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaribarang.ImageIndex = 0
        Me.btncaribarang.Location = New System.Drawing.Point(220, 248)
        Me.btncaribarang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaribarang.Name = "btncaribarang"
        Me.btncaribarang.Size = New System.Drawing.Size(37, 26)
        Me.btncaribarang.TabIndex = 5
        Me.btncaribarang.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.BackgroundImage = CType(resources.GetObject("btntambah.BackgroundImage"), System.Drawing.Image)
        Me.btntambah.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambah.ImageIndex = 0
        Me.btntambah.Location = New System.Drawing.Point(1107, 230)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(6)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(50, 40)
        Me.btntambah.TabIndex = 5
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txtbanyak
        '
        Me.txtbanyak.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbanyak.Location = New System.Drawing.Point(964, 250)
        Me.txtbanyak.Margin = New System.Windows.Forms.Padding(6)
        Me.txtbanyak.Name = "txtbanyak"
        Me.txtbanyak.Size = New System.Drawing.Size(60, 24)
        Me.txtbanyak.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(968, 226)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Qty"
        '
        'txtnamabarang
        '
        Me.txtnamabarang.Enabled = False
        Me.txtnamabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabarang.Location = New System.Drawing.Point(477, 250)
        Me.txtnamabarang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnamabarang.Name = "txtnamabarang"
        Me.txtnamabarang.Size = New System.Drawing.Size(475, 24)
        Me.txtnamabarang.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label5.Location = New System.Drawing.Point(474, 226)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Nama Item"
        '
        'txtkodestok
        '
        Me.txtkodestok.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtkodestok.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodestok.Location = New System.Drawing.Point(18, 249)
        Me.txtkodestok.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkodestok.Name = "txtkodestok"
        Me.txtkodestok.Size = New System.Drawing.Size(198, 24)
        Me.txtkodestok.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label2.Location = New System.Drawing.Point(18, 225)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kode Stok"
        '
        'btncaritransfer
        '
        Me.btncaritransfer.BackgroundImage = CType(resources.GetObject("btncaritransfer.BackgroundImage"), System.Drawing.Image)
        Me.btncaritransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaritransfer.ImageIndex = 0
        Me.btncaritransfer.Location = New System.Drawing.Point(1019, 19)
        Me.btncaritransfer.Margin = New System.Windows.Forms.Padding(4)
        Me.btncaritransfer.Name = "btncaritransfer"
        Me.btncaritransfer.Size = New System.Drawing.Size(30, 27)
        Me.btncaritransfer.TabIndex = 68
        Me.btncaritransfer.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(17, 66)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1145, 155)
        Me.TabControl1.TabIndex = 69
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.cbposted)
        Me.TabPage1.Controls.Add(Me.cbprinted)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.txtkegudang)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.btncarikegudang)
        Me.TabPage1.Controls.Add(Me.txtdarigudang)
        Me.TabPage1.Controls.Add(Me.cmbkegudang)
        Me.TabPage1.Controls.Add(Me.txtnonota)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.btncaridarigudang)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.cmbdarigudang)
        Me.TabPage1.Controls.Add(Me.dttransferbarang)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.cmbsales)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1137, 124)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Transfer Stok"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1054, 17)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(50, 18)
        Me.Label24.TabIndex = 91
        Me.Label24.Text = "Status"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Controls.Add(Me.txtketerangan)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1137, 124)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Keterangan"
        '
        'ftransferbarang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1178, 562)
        Me.Controls.Add(Me.lblsatuan)
        Me.Controls.Add(Me.txtkodebarang)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.txtbanyak)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.btncaritransfer)
        Me.Controls.Add(Me.btnprev)
        Me.Controls.Add(Me.btncaribarang)
        Me.Controls.Add(Me.btnnext)
        Me.Controls.Add(Me.txtgotransferbarang)
        Me.Controls.Add(Me.txtnamabarang)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.btngo)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.txtkodestok)
        Me.Controls.Add(Me.btnbaru)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.GridControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ftransferbarang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Barang"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ritebanyak, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents btnprev As Button
    Friend WithEvents btnnext As Button
    Friend WithEvents txtgotransferbarang As TextBox
    Friend WithEvents btngo As Button
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnbaru As Button
    Friend WithEvents btnprint As Button
    Friend WithEvents btnsimpan As Button
    Friend WithEvents txtdarigudang As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btncaridarigudang As Button
    Friend WithEvents cmbdarigudang As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents txtkegudang As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents btncarikegudang As Button
    Friend WithEvents cmbkegudang As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents dttransferbarang As DateTimePicker
    Friend WithEvents Label17 As Label
    Friend WithEvents txtnonota As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtkodebarang As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents lblsatuan As Label
    Friend WithEvents btncaribarang As Button
    Friend WithEvents btntambah As Button
    Friend WithEvents txtbanyak As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtnamabarang As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtkodestok As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ritebanyak As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents btncaritransfer As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label24 As Label
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
End Class
