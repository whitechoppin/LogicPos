<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fpenyesuaianstok
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fpenyesuaianstok))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtgudang = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btncarigudang = New System.Windows.Forms.Button()
        Me.cmbgudang = New System.Windows.Forms.ComboBox()
        Me.txtnonota = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dttransaksi = New System.Windows.Forms.DateTimePicker()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.btncaripenyesuaian = New System.Windows.Forms.Button()
        Me.btnprev = New System.Windows.Forms.Button()
        Me.btnnext = New System.Windows.Forms.Button()
        Me.txtgopenyesuaianstok = New System.Windows.Forms.TextBox()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btngo = New System.Windows.Forms.Button()
        Me.btnbaru = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.lblsatuan = New System.Windows.Forms.Label()
        Me.txtkodebarang = New System.Windows.Forms.TextBox()
        Me.btnkurang = New System.Windows.Forms.Button()
        Me.txtbanyak = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btncaribarang = New System.Windows.Forms.Button()
        Me.txtnamabarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtkodestok = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riteqtyplus = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riteqtyminus = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riteqtyplus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riteqtyminus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(22, 58)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1101, 131)
        Me.TabControl1.TabIndex = 81
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.cbposted)
        Me.TabPage1.Controls.Add(Me.cbprinted)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.txtgudang)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.btncarigudang)
        Me.TabPage1.Controls.Add(Me.cmbgudang)
        Me.TabPage1.Controls.Add(Me.txtnonota)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.dttransaksi)
        Me.TabPage1.Controls.Add(Me.cmbsales)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1093, 100)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Transfer Stok"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(739, 49)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(50, 18)
        Me.Label24.TabIndex = 91
        Me.Label24.Text = "Status"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(927, 48)
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
        Me.cbprinted.Location = New System.Drawing.Point(823, 48)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(6)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
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
        'txtgudang
        '
        Me.txtgudang.Enabled = False
        Me.txtgudang.Location = New System.Drawing.Point(452, 48)
        Me.txtgudang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtgudang.Name = "txtgudang"
        Me.txtgudang.Size = New System.Drawing.Size(265, 24)
        Me.txtgudang.TabIndex = 16
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label18.Location = New System.Drawing.Point(336, 51)
        Me.Label18.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 18)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Nama Gudang"
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
        'btncarigudang
        '
        Me.btncarigudang.BackgroundImage = CType(resources.GetObject("btncarigudang.BackgroundImage"), System.Drawing.Image)
        Me.btncarigudang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarigudang.ImageIndex = 0
        Me.btncarigudang.Location = New System.Drawing.Point(684, 11)
        Me.btncarigudang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncarigudang.Name = "btncarigudang"
        Me.btncarigudang.Size = New System.Drawing.Size(33, 27)
        Me.btncarigudang.TabIndex = 15
        Me.btncarigudang.UseVisualStyleBackColor = True
        '
        'cmbgudang
        '
        Me.cmbgudang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbgudang.FormattingEnabled = True
        Me.cmbgudang.Location = New System.Drawing.Point(452, 12)
        Me.cmbgudang.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbgudang.MaxLength = 99
        Me.cmbgudang.Name = "cmbgudang"
        Me.cmbgudang.Size = New System.Drawing.Size(230, 26)
        Me.cmbgudang.TabIndex = 14
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
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label14.Location = New System.Drawing.Point(341, 16)
        Me.Label14.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 18)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Kode Gudang"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label17.Location = New System.Drawing.Point(729, 15)
        Me.Label17.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(60, 18)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Tanggal"
        '
        'dttransaksi
        '
        Me.dttransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dttransaksi.Location = New System.Drawing.Point(801, 12)
        Me.dttransaksi.Margin = New System.Windows.Forms.Padding(6)
        Me.dttransaksi.Name = "dttransaksi"
        Me.dttransaksi.Size = New System.Drawing.Size(235, 23)
        Me.dttransaksi.TabIndex = 17
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
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Controls.Add(Me.txtketerangan)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1093, 100)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Keterangan"
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
        Me.txtketerangan.Size = New System.Drawing.Size(1075, 82)
        Me.txtketerangan.TabIndex = 67
        Me.txtketerangan.Text = ""
        '
        'btncaripenyesuaian
        '
        Me.btncaripenyesuaian.BackgroundImage = CType(resources.GetObject("btncaripenyesuaian.BackgroundImage"), System.Drawing.Image)
        Me.btncaripenyesuaian.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaripenyesuaian.ImageIndex = 0
        Me.btncaripenyesuaian.Location = New System.Drawing.Point(997, 11)
        Me.btncaripenyesuaian.Margin = New System.Windows.Forms.Padding(4)
        Me.btncaripenyesuaian.Name = "btncaripenyesuaian"
        Me.btncaripenyesuaian.Size = New System.Drawing.Size(30, 27)
        Me.btncaripenyesuaian.TabIndex = 80
        Me.btncaripenyesuaian.UseVisualStyleBackColor = True
        '
        'btnprev
        '
        Me.btnprev.Location = New System.Drawing.Point(780, 11)
        Me.btnprev.Margin = New System.Windows.Forms.Padding(6)
        Me.btnprev.Name = "btnprev"
        Me.btnprev.Size = New System.Drawing.Size(35, 27)
        Me.btnprev.TabIndex = 75
        Me.btnprev.Text = "<<"
        Me.btnprev.UseVisualStyleBackColor = True
        '
        'btnnext
        '
        Me.btnnext.Location = New System.Drawing.Point(1085, 11)
        Me.btnnext.Margin = New System.Windows.Forms.Padding(6)
        Me.btnnext.Name = "btnnext"
        Me.btnnext.Size = New System.Drawing.Size(35, 27)
        Me.btnnext.TabIndex = 78
        Me.btnnext.Text = ">>"
        Me.btnnext.UseVisualStyleBackColor = True
        '
        'txtgopenyesuaianstok
        '
        Me.txtgopenyesuaianstok.Location = New System.Drawing.Point(819, 13)
        Me.txtgopenyesuaianstok.Margin = New System.Windows.Forms.Padding(6)
        Me.txtgopenyesuaianstok.Name = "txtgopenyesuaianstok"
        Me.txtgopenyesuaianstok.Size = New System.Drawing.Size(174, 24)
        Me.txtgopenyesuaianstok.TabIndex = 77
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(615, 9)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(6)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(110, 30)
        Me.btnbatal.TabIndex = 74
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btngo
        '
        Me.btngo.Location = New System.Drawing.Point(1031, 11)
        Me.btngo.Margin = New System.Windows.Forms.Padding(6)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(50, 27)
        Me.btngo.TabIndex = 76
        Me.btngo.Text = "Go"
        Me.btngo.UseVisualStyleBackColor = True
        '
        'btnbaru
        '
        Me.btnbaru.Location = New System.Drawing.Point(276, 9)
        Me.btnbaru.Margin = New System.Windows.Forms.Padding(6)
        Me.btnbaru.Name = "btnbaru"
        Me.btnbaru.Size = New System.Drawing.Size(110, 30)
        Me.btnbaru.TabIndex = 71
        Me.btnbaru.Text = "Baru"
        Me.btnbaru.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(502, 9)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(110, 30)
        Me.btnprint.TabIndex = 73
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 9)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(249, 31)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "Penyesuaian Stok"
        '
        'btnsimpan
        '
        Me.btnsimpan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnsimpan.Location = New System.Drawing.Point(389, 9)
        Me.btnsimpan.Margin = New System.Windows.Forms.Padding(6)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(110, 30)
        Me.btnsimpan.TabIndex = 72
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'lblsatuan
        '
        Me.lblsatuan.AutoSize = True
        Me.lblsatuan.Location = New System.Drawing.Point(950, 225)
        Me.lblsatuan.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblsatuan.Name = "lblsatuan"
        Me.lblsatuan.Size = New System.Drawing.Size(54, 18)
        Me.lblsatuan.TabIndex = 90
        Me.lblsatuan.Text = "Satuan"
        '
        'txtkodebarang
        '
        Me.txtkodebarang.Enabled = False
        Me.txtkodebarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodebarang.Location = New System.Drawing.Point(233, 223)
        Me.txtkodebarang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkodebarang.Name = "txtkodebarang"
        Me.txtkodebarang.Size = New System.Drawing.Size(179, 24)
        Me.txtkodebarang.TabIndex = 92
        '
        'btnkurang
        '
        Me.btnkurang.BackgroundImage = CType(resources.GetObject("btnkurang.BackgroundImage"), System.Drawing.Image)
        Me.btnkurang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnkurang.ImageIndex = 0
        Me.btnkurang.Location = New System.Drawing.Point(1069, 207)
        Me.btnkurang.Margin = New System.Windows.Forms.Padding(6)
        Me.btnkurang.Name = "btnkurang"
        Me.btnkurang.Size = New System.Drawing.Size(50, 40)
        Me.btnkurang.TabIndex = 82
        Me.btnkurang.UseVisualStyleBackColor = True
        '
        'txtbanyak
        '
        Me.txtbanyak.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbanyak.Location = New System.Drawing.Point(889, 223)
        Me.txtbanyak.Margin = New System.Windows.Forms.Padding(6)
        Me.txtbanyak.Name = "txtbanyak"
        Me.txtbanyak.Size = New System.Drawing.Size(55, 24)
        Me.txtbanyak.TabIndex = 87
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(893, 199)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Qty"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label23.Location = New System.Drawing.Point(230, 198)
        Me.Label23.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 18)
        Me.Label23.TabIndex = 91
        Me.Label23.Text = "Kode Barang"
        '
        'btncaribarang
        '
        Me.btncaribarang.BackgroundImage = CType(resources.GetObject("btncaribarang.BackgroundImage"), System.Drawing.Image)
        Me.btncaribarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaribarang.ImageIndex = 0
        Me.btncaribarang.Location = New System.Drawing.Point(192, 221)
        Me.btncaribarang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaribarang.Name = "btncaribarang"
        Me.btncaribarang.Size = New System.Drawing.Size(37, 26)
        Me.btncaribarang.TabIndex = 84
        Me.btncaribarang.UseVisualStyleBackColor = True
        '
        'txtnamabarang
        '
        Me.txtnamabarang.Enabled = False
        Me.txtnamabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabarang.Location = New System.Drawing.Point(419, 223)
        Me.txtnamabarang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnamabarang.Name = "txtnamabarang"
        Me.txtnamabarang.Size = New System.Drawing.Size(466, 24)
        Me.txtnamabarang.TabIndex = 88
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label5.Location = New System.Drawing.Point(416, 199)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 18)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "Nama Item"
        '
        'txtkodestok
        '
        Me.txtkodestok.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtkodestok.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodestok.Location = New System.Drawing.Point(21, 222)
        Me.txtkodestok.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkodestok.Name = "txtkodestok"
        Me.txtkodestok.Size = New System.Drawing.Size(167, 24)
        Me.txtkodestok.TabIndex = 89
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label2.Location = New System.Drawing.Point(18, 198)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 18)
        Me.Label2.TabIndex = 86
        Me.Label2.Text = "Kode Stok"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6)
        Me.GridControl1.Location = New System.Drawing.Point(22, 281)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(6)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riteqtyplus})
        Me.GridControl1.Size = New System.Drawing.Size(1101, 201)
        Me.GridControl1.TabIndex = 93
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
        Me.GridColumn4.Caption = "Qty"
        Me.GridColumn4.ColumnEdit = Me.riteqtyplus
        Me.GridColumn4.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'riteqtyplus
        '
        Me.riteqtyplus.AutoHeight = False
        Me.riteqtyplus.MaxLength = 12
        Me.riteqtyplus.Name = "riteqtyplus"
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
        Me.GridColumn8.Caption = "id stok"
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
        Me.GridColumn9.Caption = "Status Stok"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        '
        'btntambah
        '
        Me.btntambah.BackgroundImage = CType(resources.GetObject("btntambah.BackgroundImage"), System.Drawing.Image)
        Me.btntambah.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambah.ImageIndex = 0
        Me.btntambah.Location = New System.Drawing.Point(1012, 206)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(6)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(50, 41)
        Me.btntambah.TabIndex = 94
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(19, 257)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 18)
        Me.Label1.TabIndex = 92
        Me.Label1.Text = "Penambahan"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(19, 488)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 18)
        Me.Label4.TabIndex = 96
        Me.Label4.Text = "Pengurangan"
        '
        'GridControl2
        '
        Me.GridControl2.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6)
        Me.GridControl2.Location = New System.Drawing.Point(22, 512)
        Me.GridControl2.MainView = Me.GridView2
        Me.GridControl2.Margin = New System.Windows.Forms.Padding(6)
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riteqtyminus})
        Me.GridControl2.Size = New System.Drawing.Size(1101, 201)
        Me.GridControl2.TabIndex = 97
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18})
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.Name = "GridView2"
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn10.AppearanceCell.Options.UseFont = True
        Me.GridColumn10.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn10.AppearanceHeader.Options.UseFont = True
        Me.GridColumn10.Caption = "Kode Barang"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn11.AppearanceCell.Options.UseFont = True
        Me.GridColumn11.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn11.AppearanceHeader.Options.UseFont = True
        Me.GridColumn11.Caption = "Kode Stok"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn12.AppearanceCell.Options.UseFont = True
        Me.GridColumn12.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn12.AppearanceHeader.Options.UseFont = True
        Me.GridColumn12.Caption = "Nama Barang"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 2
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn13.AppearanceCell.Options.UseFont = True
        Me.GridColumn13.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn13.AppearanceHeader.Options.UseFont = True
        Me.GridColumn13.Caption = "Qty"
        Me.GridColumn13.ColumnEdit = Me.riteqtyminus
        Me.GridColumn13.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 3
        '
        'riteqtyminus
        '
        Me.riteqtyminus.AutoHeight = False
        Me.riteqtyminus.MaxLength = 12
        Me.riteqtyminus.Name = "riteqtyminus"
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn14.AppearanceCell.Options.UseFont = True
        Me.GridColumn14.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn14.AppearanceHeader.Options.UseFont = True
        Me.GridColumn14.Caption = "Satuan Barang"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 4
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn15.AppearanceCell.Options.UseFont = True
        Me.GridColumn15.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn15.AppearanceHeader.Options.UseFont = True
        Me.GridColumn15.Caption = "Jenis Barang"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 5
        '
        'GridColumn16
        '
        Me.GridColumn16.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn16.AppearanceCell.Options.UseFont = True
        Me.GridColumn16.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn16.AppearanceHeader.Options.UseFont = True
        Me.GridColumn16.Caption = "id barang"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 6
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn17.AppearanceCell.Options.UseFont = True
        Me.GridColumn17.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn17.AppearanceHeader.Options.UseFont = True
        Me.GridColumn17.Caption = "id stok"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 7
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn18.AppearanceCell.Options.UseFont = True
        Me.GridColumn18.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn18.AppearanceHeader.Options.UseFont = True
        Me.GridColumn18.Caption = "Status Stok"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 8
        '
        'fpenyesuaianstok
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1138, 725)
        Me.Controls.Add(Me.GridControl2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.lblsatuan)
        Me.Controls.Add(Me.txtkodebarang)
        Me.Controls.Add(Me.btnkurang)
        Me.Controls.Add(Me.txtbanyak)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.btncaribarang)
        Me.Controls.Add(Me.txtnamabarang)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtkodestok)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btncaripenyesuaian)
        Me.Controls.Add(Me.btnprev)
        Me.Controls.Add(Me.btnnext)
        Me.Controls.Add(Me.txtgopenyesuaianstok)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.btngo)
        Me.Controls.Add(Me.btnbaru)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnsimpan)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fpenyesuaianstok"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Penyesuaian Stok"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riteqtyplus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riteqtyminus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label24 As Label
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtgudang As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents btncarigudang As Button
    Friend WithEvents cmbgudang As ComboBox
    Friend WithEvents txtnonota As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents dttransaksi As DateTimePicker
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents btncaripenyesuaian As Button
    Friend WithEvents btnprev As Button
    Friend WithEvents btnnext As Button
    Friend WithEvents txtgopenyesuaianstok As TextBox
    Friend WithEvents btnbatal As Button
    Friend WithEvents btngo As Button
    Friend WithEvents btnbaru As Button
    Friend WithEvents btnprint As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents btnsimpan As Button
    Friend WithEvents lblsatuan As Label
    Friend WithEvents txtkodebarang As TextBox
    Friend WithEvents btnkurang As Button
    Friend WithEvents txtbanyak As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents btncaribarang As Button
    Friend WithEvents txtnamabarang As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtkodestok As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents riteqtyplus As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btntambah As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents riteqtyminus As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
End Class
