<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkalkulasipengiriman
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fkalkulasipengiriman))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.txthargakubik = New System.Windows.Forms.TextBox()
        Me.txttelpexpedisi = New System.Windows.Forms.TextBox()
        Me.txtuser = New System.Windows.Forms.TextBox()
        Me.txtalamatexpedisi = New System.Windows.Forms.TextBox()
        Me.txtnonota = New System.Windows.Forms.TextBox()
        Me.txtnamaexpedisi = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpengiriman = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbuser = New System.Windows.Forms.ComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.btntambahbarang = New System.Windows.Forms.Button()
        Me.btncaridata = New System.Windows.Forms.Button()
        Me.txthargabarang = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnprev = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnnext = New System.Windows.Forms.Button()
        Me.txtgodata = New System.Windows.Forms.TextBox()
        Me.btngo = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnbaru = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtbanyakbarang = New System.Windows.Forms.TextBox()
        Me.btncaribarang = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.txtnamabarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ritenumber = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ritedesimal = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtkodebarang = New System.Windows.Forms.TextBox()
        Me.txtkubik = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ritenumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ritedesimal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(19, 74)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1041, 158)
        Me.TabControl1.TabIndex = 90
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.cbposted)
        Me.TabPage1.Controls.Add(Me.cbprinted)
        Me.TabPage1.Controls.Add(Me.txthargakubik)
        Me.TabPage1.Controls.Add(Me.txttelpexpedisi)
        Me.TabPage1.Controls.Add(Me.txtuser)
        Me.TabPage1.Controls.Add(Me.txtalamatexpedisi)
        Me.TabPage1.Controls.Add(Me.txtnonota)
        Me.TabPage1.Controls.Add(Me.txtnamaexpedisi)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.dtpengiriman)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.cmbuser)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1033, 127)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Pengiriman"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(713, 19)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 18)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Status"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(942, 14)
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
        Me.cbprinted.Location = New System.Drawing.Point(840, 14)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(4)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'txthargakubik
        '
        Me.txthargakubik.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.txthargakubik.Location = New System.Drawing.Point(821, 81)
        Me.txthargakubik.Margin = New System.Windows.Forms.Padding(4)
        Me.txthargakubik.MaxLength = 12
        Me.txthargakubik.Name = "txthargakubik"
        Me.txthargakubik.Size = New System.Drawing.Size(195, 24)
        Me.txthargakubik.TabIndex = 55
        '
        'txttelpexpedisi
        '
        Me.txttelpexpedisi.Location = New System.Drawing.Point(463, 82)
        Me.txttelpexpedisi.Margin = New System.Windows.Forms.Padding(4)
        Me.txttelpexpedisi.Name = "txttelpexpedisi"
        Me.txttelpexpedisi.Size = New System.Drawing.Size(198, 24)
        Me.txttelpexpedisi.TabIndex = 60
        '
        'txtuser
        '
        Me.txtuser.Enabled = False
        Me.txtuser.Location = New System.Drawing.Point(120, 81)
        Me.txtuser.Margin = New System.Windows.Forms.Padding(4)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.Size = New System.Drawing.Size(203, 24)
        Me.txtuser.TabIndex = 56
        '
        'txtalamatexpedisi
        '
        Me.txtalamatexpedisi.Location = New System.Drawing.Point(463, 49)
        Me.txtalamatexpedisi.Margin = New System.Windows.Forms.Padding(4)
        Me.txtalamatexpedisi.Name = "txtalamatexpedisi"
        Me.txtalamatexpedisi.Size = New System.Drawing.Size(198, 24)
        Me.txtalamatexpedisi.TabIndex = 58
        '
        'txtnonota
        '
        Me.txtnonota.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonota.Location = New System.Drawing.Point(120, 15)
        Me.txtnonota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnonota.Name = "txtnonota"
        Me.txtnonota.Size = New System.Drawing.Size(203, 24)
        Me.txtnonota.TabIndex = 9
        '
        'txtnamaexpedisi
        '
        Me.txtnamaexpedisi.Location = New System.Drawing.Point(463, 15)
        Me.txtnamaexpedisi.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnamaexpedisi.Name = "txtnamaexpedisi"
        Me.txtnamaexpedisi.Size = New System.Drawing.Size(198, 24)
        Me.txtnamaexpedisi.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(335, 85)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 18)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Telepon Expedisi"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label14.Location = New System.Drawing.Point(695, 85)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 18)
        Me.Label14.TabIndex = 53
        Me.Label14.Text = "Harga kubik"
        '
        'dtpengiriman
        '
        Me.dtpengiriman.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dtpengiriman.Location = New System.Drawing.Point(821, 49)
        Me.dtpengiriman.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpengiriman.Name = "dtpengiriman"
        Me.dtpengiriman.Size = New System.Drawing.Size(195, 23)
        Me.dtpengiriman.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label12.Location = New System.Drawing.Point(675, 52)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(138, 18)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Tanggal Pengiriman"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(342, 52)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 18)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Alamat Expedisi"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(24, 18)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 18)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "No. Nota"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(21, 50)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(79, 18)
        Me.Label16.TabIndex = 37
        Me.Label16.Text = "Kode User"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(21, 84)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 18)
        Me.Label18.TabIndex = 57
        Me.Label18.Text = "Nama User"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(343, 19)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(107, 18)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Nama Expedisi"
        '
        'cmbuser
        '
        Me.cmbuser.FormattingEnabled = True
        Me.cmbuser.Location = New System.Drawing.Point(120, 47)
        Me.cmbuser.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbuser.MaxLength = 99
        Me.cmbuser.Name = "cmbuser"
        Me.cmbuser.Size = New System.Drawing.Size(203, 26)
        Me.cmbuser.TabIndex = 13
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Controls.Add(Me.txtketerangan)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1033, 127)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Keterangan"
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
        Me.txtketerangan.Size = New System.Drawing.Size(1019, 113)
        Me.txtketerangan.TabIndex = 56
        Me.txtketerangan.Text = ""
        '
        'btntambahbarang
        '
        Me.btntambahbarang.BackgroundImage = CType(resources.GetObject("btntambahbarang.BackgroundImage"), System.Drawing.Image)
        Me.btntambahbarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambahbarang.ImageIndex = 0
        Me.btntambahbarang.Location = New System.Drawing.Point(975, 247)
        Me.btntambahbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.btntambahbarang.Name = "btntambahbarang"
        Me.btntambahbarang.Size = New System.Drawing.Size(41, 37)
        Me.btntambahbarang.TabIndex = 76
        Me.btntambahbarang.UseVisualStyleBackColor = True
        '
        'btncaridata
        '
        Me.btncaridata.BackgroundImage = CType(resources.GetObject("btncaridata.BackgroundImage"), System.Drawing.Image)
        Me.btncaridata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaridata.ImageIndex = 0
        Me.btncaridata.Location = New System.Drawing.Point(938, 19)
        Me.btncaridata.Margin = New System.Windows.Forms.Padding(4)
        Me.btncaridata.Name = "btncaridata"
        Me.btncaridata.Size = New System.Drawing.Size(30, 27)
        Me.btncaridata.TabIndex = 88
        Me.btncaridata.UseVisualStyleBackColor = True
        '
        'txthargabarang
        '
        Me.txthargabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txthargabarang.Location = New System.Drawing.Point(685, 260)
        Me.txthargabarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txthargabarang.MaxLength = 12
        Me.txthargabarang.Name = "txthargabarang"
        Me.txthargabarang.Size = New System.Drawing.Size(183, 24)
        Me.txthargabarang.TabIndex = 85
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(655, 263)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(27, 18)
        Me.Label13.TabIndex = 83
        Me.Label13.Text = "Rp"
        '
        'btnprev
        '
        Me.btnprev.Location = New System.Drawing.Point(745, 20)
        Me.btnprev.Margin = New System.Windows.Forms.Padding(4)
        Me.btnprev.Name = "btnprev"
        Me.btnprev.Size = New System.Drawing.Size(35, 27)
        Me.btnprev.TabIndex = 65
        Me.btnprev.Text = "<<"
        Me.btnprev.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(688, 237)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 18)
        Me.Label17.TabIndex = 84
        Me.Label17.Text = "Harga Barang"
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(657, 18)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(4)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(84, 30)
        Me.btnbatal.TabIndex = 63
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnnext
        '
        Me.btnnext.Location = New System.Drawing.Point(1025, 19)
        Me.btnnext.Margin = New System.Windows.Forms.Padding(4)
        Me.btnnext.Name = "btnnext"
        Me.btnnext.Size = New System.Drawing.Size(35, 27)
        Me.btnnext.TabIndex = 70
        Me.btnnext.Text = ">>"
        Me.btnnext.UseVisualStyleBackColor = True
        '
        'txtgodata
        '
        Me.txtgodata.Location = New System.Drawing.Point(784, 21)
        Me.txtgodata.Margin = New System.Windows.Forms.Padding(4)
        Me.txtgodata.Name = "txtgodata"
        Me.txtgodata.Size = New System.Drawing.Size(150, 24)
        Me.txtgodata.TabIndex = 69
        '
        'btngo
        '
        Me.btngo.Location = New System.Drawing.Point(972, 19)
        Me.btngo.Margin = New System.Windows.Forms.Padding(4)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(50, 27)
        Me.btngo.TabIndex = 66
        Me.btngo.Text = "Go"
        Me.btngo.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(569, 18)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(84, 30)
        Me.btnedit.TabIndex = 60
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnbaru
        '
        Me.btnbaru.Location = New System.Drawing.Point(307, 18)
        Me.btnbaru.Margin = New System.Windows.Forms.Padding(4)
        Me.btnbaru.Name = "btnbaru"
        Me.btnbaru.Size = New System.Drawing.Size(84, 30)
        Me.btnbaru.TabIndex = 59
        Me.btnbaru.Text = "Baru"
        Me.btnbaru.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(482, 18)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(84, 30)
        Me.btnprint.TabIndex = 62
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 19)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(286, 31)
        Me.Label7.TabIndex = 89
        Me.Label7.Text = "Kalkulasi Pengiriman"
        '
        'txtbanyakbarang
        '
        Me.txtbanyakbarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbanyakbarang.Location = New System.Drawing.Point(876, 260)
        Me.txtbanyakbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtbanyakbarang.MaxLength = 12
        Me.txtbanyakbarang.Name = "txtbanyakbarang"
        Me.txtbanyakbarang.Size = New System.Drawing.Size(37, 24)
        Me.txtbanyakbarang.TabIndex = 75
        '
        'btncaribarang
        '
        Me.btncaribarang.BackgroundImage = CType(resources.GetObject("btncaribarang.BackgroundImage"), System.Drawing.Image)
        Me.btncaribarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaribarang.ImageIndex = 0
        Me.btncaribarang.Location = New System.Drawing.Point(192, 259)
        Me.btncaribarang.Margin = New System.Windows.Forms.Padding(4)
        Me.btncaribarang.Name = "btncaribarang"
        Me.btncaribarang.Size = New System.Drawing.Size(30, 27)
        Me.btncaribarang.TabIndex = 72
        Me.btncaribarang.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(879, 237)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "Qty"
        '
        'btnsimpan
        '
        Me.btnsimpan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnsimpan.Location = New System.Drawing.Point(394, 18)
        Me.btnsimpan.Margin = New System.Windows.Forms.Padding(4)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(84, 30)
        Me.btnsimpan.TabIndex = 61
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'txtnamabarang
        '
        Me.txtnamabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabarang.Location = New System.Drawing.Point(228, 260)
        Me.txtnamabarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnamabarang.Name = "txtnamabarang"
        Me.txtnamabarang.Size = New System.Drawing.Size(425, 24)
        Me.txtnamabarang.TabIndex = 74
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(228, 237)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 18)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Nama Barang"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Location = New System.Drawing.Point(17, 305)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ritenumber, Me.ritedesimal})
        Me.GridControl1.Size = New System.Drawing.Size(1043, 336)
        Me.GridControl1.TabIndex = 73
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
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
        Me.GridColumn2.Caption = "Nama Barang"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn3.AppearanceCell.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.Caption = "Qty"
        Me.GridColumn3.ColumnEdit = Me.ritenumber
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'ritenumber
        '
        Me.ritenumber.AutoHeight = False
        Me.ritenumber.Name = "ritenumber"
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn4.AppearanceCell.Options.UseFont = True
        Me.GridColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn4.AppearanceHeader.Options.UseFont = True
        Me.GridColumn4.Caption = "Harga Barang"
        Me.GridColumn4.ColumnEdit = Me.ritenumber
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceCell.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.Caption = "Kubik"
        Me.GridColumn5.ColumnEdit = Me.ritedesimal
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'ritedesimal
        '
        Me.ritedesimal.AutoHeight = False
        Me.ritedesimal.Name = "ritedesimal"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn6.AppearanceCell.Options.UseFont = True
        Me.GridColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn6.AppearanceHeader.Options.UseFont = True
        Me.GridColumn6.Caption = "Ongkos Kirim"
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
        Me.GridColumn7.Caption = "Harga + Ongkir"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn8.AppearanceCell.Options.UseFont = True
        Me.GridColumn8.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn8.AppearanceHeader.Options.UseFont = True
        Me.GridColumn8.Caption = "Total Ongkos Kirim"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn9.AppearanceCell.Options.UseFont = True
        Me.GridColumn9.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn9.AppearanceHeader.Options.UseFont = True
        Me.GridColumn9.Caption = "Total Harga Barang"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn10.AppearanceCell.Options.UseFont = True
        Me.GridColumn10.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn10.AppearanceHeader.Options.UseFont = True
        Me.GridColumn10.Caption = "Grand Total Barang"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 237)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 18)
        Me.Label2.TabIndex = 68
        Me.Label2.Text = "Kode Barang"
        '
        'txtkodebarang
        '
        Me.txtkodebarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodebarang.Location = New System.Drawing.Point(17, 260)
        Me.txtkodebarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtkodebarang.Name = "txtkodebarang"
        Me.txtkodebarang.Size = New System.Drawing.Size(171, 24)
        Me.txtkodebarang.TabIndex = 71
        '
        'txtkubik
        '
        Me.txtkubik.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkubik.Location = New System.Drawing.Point(919, 260)
        Me.txtkubik.Margin = New System.Windows.Forms.Padding(4)
        Me.txtkubik.MaxLength = 12
        Me.txtkubik.Name = "txtkubik"
        Me.txtkubik.Size = New System.Drawing.Size(37, 24)
        Me.txtkubik.TabIndex = 96
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(922, 237)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 18)
        Me.Label8.TabIndex = 95
        Me.Label8.Text = "Kubik"
        '
        'btnrefresh
        '
        Me.btnrefresh.BackgroundImage = CType(resources.GetObject("btnrefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnrefresh.ImageIndex = 0
        Me.btnrefresh.Location = New System.Drawing.Point(1019, 247)
        Me.btnrefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(41, 37)
        Me.btnrefresh.TabIndex = 97
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn11.AppearanceCell.Options.UseFont = True
        Me.GridColumn11.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn11.AppearanceHeader.Options.UseFont = True
        Me.GridColumn11.Caption = "id barang"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 10
        '
        'fkalkulasipengiriman
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1076, 649)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.txtkubik)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btntambahbarang)
        Me.Controls.Add(Me.btncaridata)
        Me.Controls.Add(Me.txthargabarang)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnprev)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.btnnext)
        Me.Controls.Add(Me.txtgodata)
        Me.Controls.Add(Me.btngo)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnbaru)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtbanyakbarang)
        Me.Controls.Add(Me.btncaribarang)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.txtnamabarang)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtkodebarang)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fkalkulasipengiriman"
        Me.Text = "Kalkulasi Pengiriman"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ritenumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ritedesimal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents txthargakubik As TextBox
    Friend WithEvents txttelpexpedisi As TextBox
    Friend WithEvents txtuser As TextBox
    Friend WithEvents txtalamatexpedisi As TextBox
    Friend WithEvents txtnonota As TextBox
    Friend WithEvents txtnamaexpedisi As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents dtpengiriman As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmbuser As ComboBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents btntambahbarang As Button
    Friend WithEvents btncaridata As Button
    Friend WithEvents txthargabarang As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents btnprev As Button
    Friend WithEvents Label17 As Label
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnnext As Button
    Friend WithEvents txtgodata As TextBox
    Friend WithEvents btngo As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnbaru As Button
    Friend WithEvents btnprint As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents txtbanyakbarang As TextBox
    Friend WithEvents btncaribarang As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnsimpan As Button
    Friend WithEvents txtnamabarang As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ritenumber As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents txtkodebarang As TextBox
    Friend WithEvents txtkubik As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnrefresh As Button
    Friend WithEvents ritedesimal As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
End Class
