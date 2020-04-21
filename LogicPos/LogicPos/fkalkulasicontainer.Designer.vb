<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkalkulasicontainer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fkalkulasicontainer))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btncaripembelian = New System.Windows.Forms.Button()
        Me.btnprev = New System.Windows.Forms.Button()
        Me.btnnext = New System.Windows.Forms.Button()
        Me.txtgopembelian = New System.Windows.Forms.TextBox()
        Me.btngo = New System.Windows.Forms.Button()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnbaru = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.cblunas = New System.Windows.Forms.CheckBox()
        Me.cbvoid = New System.Windows.Forms.CheckBox()
        Me.txtsupplier = New System.Windows.Forms.TextBox()
        Me.btncarisupplier = New System.Windows.Forms.Button()
        Me.dtpembelian = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbsupplier = New System.Windows.Forms.ComboBox()
        Me.txtnonota = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtjatuhtempo = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblsatuanbeli = New System.Windows.Forms.Label()
        Me.lblsatuan = New System.Windows.Forms.Label()
        Me.btncari = New System.Windows.Forms.Button()
        Me.btntambahbarang = New System.Windows.Forms.Button()
        Me.txtbanyakbarang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txthargabarang = New System.Windows.Forms.TextBox()
        Me.txtnamabarang = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtkodebarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txttotal = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(10, 272)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1156, 243)
        Me.GridControl1.TabIndex = 21
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
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
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn3.AppearanceCell.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.Caption = "Panjang Barang"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn4.AppearanceCell.Options.UseFont = True
        Me.GridColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn4.AppearanceHeader.Options.UseFont = True
        Me.GridColumn4.Caption = "Lebar Barang"
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
        Me.GridColumn5.Caption = "Tinggi Barang"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn6.AppearanceCell.Options.UseFont = True
        Me.GridColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn6.AppearanceHeader.Options.UseFont = True
        Me.GridColumn6.Caption = "Qty"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn7.AppearanceCell.Options.UseFont = True
        Me.GridColumn7.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn7.AppearanceHeader.Options.UseFont = True
        Me.GridColumn7.Caption = "Harga Barang"
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
        Me.GridColumn8.Caption = "Ongkos Kirim"
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
        Me.GridColumn9.Caption = "Total Modal Barang"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.btnbatal)
        Me.GroupBox4.Controls.Add(Me.btnedit)
        Me.GroupBox4.Controls.Add(Me.btnbaru)
        Me.GroupBox4.Controls.Add(Me.btnprint)
        Me.GroupBox4.Controls.Add(Me.btnsimpan)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.GroupBox4.Location = New System.Drawing.Point(271, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(895, 72)
        Me.GroupBox4.TabIndex = 44
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Tools"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btncaripembelian)
        Me.GroupBox5.Controls.Add(Me.btnprev)
        Me.GroupBox5.Controls.Add(Me.btnnext)
        Me.GroupBox5.Controls.Add(Me.txtgopembelian)
        Me.GroupBox5.Controls.Add(Me.btngo)
        Me.GroupBox5.Location = New System.Drawing.Point(515, 10)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(369, 52)
        Me.GroupBox5.TabIndex = 37
        Me.GroupBox5.TabStop = False
        '
        'btncaripembelian
        '
        Me.btncaripembelian.BackgroundImage = CType(resources.GetObject("btncaripembelian.BackgroundImage"), System.Drawing.Image)
        Me.btncaripembelian.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaripembelian.ImageIndex = 0
        Me.btncaripembelian.Location = New System.Drawing.Point(251, 17)
        Me.btncaripembelian.Name = "btncaripembelian"
        Me.btncaripembelian.Size = New System.Drawing.Size(31, 26)
        Me.btncaripembelian.TabIndex = 41
        Me.btncaripembelian.UseVisualStyleBackColor = True
        '
        'btnprev
        '
        Me.btnprev.Location = New System.Drawing.Point(7, 17)
        Me.btnprev.Name = "btnprev"
        Me.btnprev.Size = New System.Drawing.Size(75, 26)
        Me.btnprev.TabIndex = 5
        Me.btnprev.Text = "<< Prev"
        Me.btnprev.UseVisualStyleBackColor = True
        '
        'btnnext
        '
        Me.btnnext.Location = New System.Drawing.Point(288, 17)
        Me.btnnext.Name = "btnnext"
        Me.btnnext.Size = New System.Drawing.Size(75, 26)
        Me.btnnext.TabIndex = 8
        Me.btnnext.Text = "Next >>"
        Me.btnnext.UseVisualStyleBackColor = True
        '
        'txtgopembelian
        '
        Me.txtgopembelian.Location = New System.Drawing.Point(137, 18)
        Me.txtgopembelian.Name = "txtgopembelian"
        Me.txtgopembelian.Size = New System.Drawing.Size(115, 24)
        Me.txtgopembelian.TabIndex = 7
        '
        'btngo
        '
        Me.btngo.Location = New System.Drawing.Point(85, 17)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(52, 26)
        Me.btngo.TabIndex = 6
        Me.btngo.Text = "Go"
        Me.btngo.UseVisualStyleBackColor = True
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(412, 23)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(93, 35)
        Me.btnbatal.TabIndex = 4
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(313, 23)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(93, 35)
        Me.btnedit.TabIndex = 1
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnbaru
        '
        Me.btnbaru.Location = New System.Drawing.Point(16, 23)
        Me.btnbaru.Name = "btnbaru"
        Me.btnbaru.Size = New System.Drawing.Size(93, 35)
        Me.btnbaru.TabIndex = 1
        Me.btnbaru.Text = "Baru"
        Me.btnbaru.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(214, 23)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(92, 35)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'btnsimpan
        '
        Me.btnsimpan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnsimpan.Location = New System.Drawing.Point(115, 23)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(93, 35)
        Me.btnsimpan.TabIndex = 2
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbsales)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.txtsupplier)
        Me.GroupBox2.Controls.Add(Me.btncarisupplier)
        Me.GroupBox2.Controls.Add(Me.dtpembelian)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.cmbsupplier)
        Me.GroupBox2.Controls.Add(Me.txtnonota)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.dtjatuhtempo)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1153, 126)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Expedisi"
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(470, 27)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(257, 26)
        Me.cmbsales.TabIndex = 13
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(359, 30)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 18)
        Me.Label16.TabIndex = 37
        Me.Label16.Text = "Kode Sales"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(11, 92)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(107, 18)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Nama Expedisi"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbposted)
        Me.GroupBox3.Controls.Add(Me.cbprinted)
        Me.GroupBox3.Controls.Add(Me.cblunas)
        Me.GroupBox3.Controls.Add(Me.cbvoid)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.GroupBox3.Location = New System.Drawing.Point(736, 9)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(406, 45)
        Me.GroupBox3.TabIndex = 36
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Status"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(309, 16)
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
        Me.cbprinted.Location = New System.Drawing.Point(214, 16)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'cblunas
        '
        Me.cblunas.AutoSize = True
        Me.cblunas.Enabled = False
        Me.cblunas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cblunas.Location = New System.Drawing.Point(30, 16)
        Me.cblunas.Name = "cblunas"
        Me.cblunas.Size = New System.Drawing.Size(67, 22)
        Me.cblunas.TabIndex = 38
        Me.cblunas.Text = "Lunas"
        Me.cblunas.UseVisualStyleBackColor = True
        '
        'cbvoid
        '
        Me.cbvoid.AutoSize = True
        Me.cbvoid.Enabled = False
        Me.cbvoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbvoid.Location = New System.Drawing.Point(123, 16)
        Me.cbvoid.Name = "cbvoid"
        Me.cbvoid.Size = New System.Drawing.Size(56, 22)
        Me.cbvoid.TabIndex = 39
        Me.cbvoid.Text = "Void"
        Me.cbvoid.UseVisualStyleBackColor = True
        '
        'txtsupplier
        '
        Me.txtsupplier.Location = New System.Drawing.Point(122, 89)
        Me.txtsupplier.Name = "txtsupplier"
        Me.txtsupplier.Size = New System.Drawing.Size(231, 24)
        Me.txtsupplier.TabIndex = 12
        '
        'btncarisupplier
        '
        Me.btncarisupplier.BackgroundImage = CType(resources.GetObject("btncarisupplier.BackgroundImage"), System.Drawing.Image)
        Me.btncarisupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarisupplier.ImageIndex = 0
        Me.btncarisupplier.Location = New System.Drawing.Point(320, 59)
        Me.btncarisupplier.Name = "btncarisupplier"
        Me.btncarisupplier.Size = New System.Drawing.Size(32, 27)
        Me.btncarisupplier.TabIndex = 11
        Me.btncarisupplier.UseVisualStyleBackColor = True
        '
        'dtpembelian
        '
        Me.dtpembelian.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dtpembelian.Location = New System.Drawing.Point(891, 58)
        Me.dtpembelian.Name = "dtpembelian"
        Me.dtpembelian.Size = New System.Drawing.Size(252, 23)
        Me.dtpembelian.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label12.Location = New System.Drawing.Point(733, 63)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(133, 18)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Tanggal Pembelian"
        '
        'cmbsupplier
        '
        Me.cmbsupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsupplier.FormattingEnabled = True
        Me.cmbsupplier.Location = New System.Drawing.Point(122, 59)
        Me.cmbsupplier.MaxLength = 99
        Me.cmbsupplier.Name = "cmbsupplier"
        Me.cmbsupplier.Size = New System.Drawing.Size(199, 26)
        Me.cmbsupplier.TabIndex = 10
        '
        'txtnonota
        '
        Me.txtnonota.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonota.Location = New System.Drawing.Point(122, 30)
        Me.txtnonota.Name = "txtnonota"
        Me.txtnonota.Size = New System.Drawing.Size(231, 24)
        Me.txtnonota.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 18)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "No. Aplikasi"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 18)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Kode Expedisi"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(733, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 18)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Tanggal Jatuh Tempo"
        '
        'dtjatuhtempo
        '
        Me.dtjatuhtempo.CustomFormat = ""
        Me.dtjatuhtempo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dtjatuhtempo.Location = New System.Drawing.Point(891, 90)
        Me.dtjatuhtempo.Name = "dtjatuhtempo"
        Me.dtjatuhtempo.Size = New System.Drawing.Size(252, 23)
        Me.dtjatuhtempo.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(250, 31)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Kalkulasi Expedisi"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblsatuanbeli)
        Me.GroupBox1.Controls.Add(Me.lblsatuan)
        Me.GroupBox1.Controls.Add(Me.btncari)
        Me.GroupBox1.Controls.Add(Me.btntambahbarang)
        Me.GroupBox1.Controls.Add(Me.txtbanyakbarang)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txthargabarang)
        Me.GroupBox1.Controls.Add(Me.txtnamabarang)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtkodebarang)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 203)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1153, 63)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        '
        'lblsatuanbeli
        '
        Me.lblsatuanbeli.AutoSize = True
        Me.lblsatuanbeli.Location = New System.Drawing.Point(946, 16)
        Me.lblsatuanbeli.Name = "lblsatuanbeli"
        Me.lblsatuanbeli.Size = New System.Drawing.Size(41, 13)
        Me.lblsatuanbeli.TabIndex = 26
        Me.lblsatuanbeli.Text = "Satuan"
        '
        'lblsatuan
        '
        Me.lblsatuan.AutoSize = True
        Me.lblsatuan.Location = New System.Drawing.Point(784, 40)
        Me.lblsatuan.Name = "lblsatuan"
        Me.lblsatuan.Size = New System.Drawing.Size(41, 13)
        Me.lblsatuan.TabIndex = 25
        Me.lblsatuan.Text = "Satuan"
        '
        'btncari
        '
        Me.btncari.BackgroundImage = CType(resources.GetObject("btncari.BackgroundImage"), System.Drawing.Image)
        Me.btncari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncari.ImageIndex = 0
        Me.btncari.Location = New System.Drawing.Point(238, 32)
        Me.btncari.Name = "btncari"
        Me.btncari.Size = New System.Drawing.Size(29, 26)
        Me.btncari.TabIndex = 20
        Me.btncari.UseVisualStyleBackColor = True
        '
        'btntambahbarang
        '
        Me.btntambahbarang.BackgroundImage = CType(resources.GetObject("btntambahbarang.BackgroundImage"), System.Drawing.Image)
        Me.btntambahbarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambahbarang.ImageIndex = 0
        Me.btntambahbarang.Location = New System.Drawing.Point(1092, 12)
        Me.btntambahbarang.Name = "btntambahbarang"
        Me.btntambahbarang.Size = New System.Drawing.Size(55, 46)
        Me.btntambahbarang.TabIndex = 24
        Me.btntambahbarang.UseVisualStyleBackColor = True
        '
        'txtbanyakbarang
        '
        Me.txtbanyakbarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbanyakbarang.Location = New System.Drawing.Point(719, 33)
        Me.txtbanyakbarang.MaxLength = 12
        Me.txtbanyakbarang.Name = "txtbanyakbarang"
        Me.txtbanyakbarang.Size = New System.Drawing.Size(59, 24)
        Me.txtbanyakbarang.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(719, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Qty"
        '
        'txthargabarang
        '
        Me.txthargabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txthargabarang.Location = New System.Drawing.Point(866, 34)
        Me.txthargabarang.MaxLength = 12
        Me.txthargabarang.Name = "txthargabarang"
        Me.txthargabarang.Size = New System.Drawing.Size(222, 24)
        Me.txthargabarang.TabIndex = 23
        '
        'txtnamabarang
        '
        Me.txtnamabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabarang.Location = New System.Drawing.Point(278, 33)
        Me.txtnamabarang.Name = "txtnamabarang"
        Me.txtnamabarang.Size = New System.Drawing.Size(435, 24)
        Me.txtnamabarang.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(833, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 18)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Rp"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(863, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 18)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Harga Beli / "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(275, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Nama Barang"
        '
        'txtkodebarang
        '
        Me.txtkodebarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodebarang.Location = New System.Drawing.Point(15, 33)
        Me.txtkodebarang.Name = "txtkodebarang"
        Me.txtkodebarang.Size = New System.Drawing.Size(226, 24)
        Me.txtkodebarang.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kode Barang"
        '
        'txttotal
        '
        Me.txttotal.Enabled = False
        Me.txttotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.txttotal.Location = New System.Drawing.Point(936, 539)
        Me.txttotal.MaxLength = 12
        Me.txttotal.Name = "txttotal"
        Me.txttotal.Size = New System.Drawing.Size(230, 24)
        Me.txttotal.TabIndex = 55
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label14.Location = New System.Drawing.Point(874, 542)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 18)
        Me.Label14.TabIndex = 53
        Me.Label14.Text = "Total"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(10, 522)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 18)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Keterangan"
        '
        'txtketerangan
        '
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtketerangan.Location = New System.Drawing.Point(10, 541)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(598, 92)
        Me.txtketerangan.TabIndex = 56
        Me.txtketerangan.Text = ""
        '
        'fkalkulasicontainer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1180, 735)
        Me.Controls.Add(Me.txttotal)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtketerangan)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GridControl1)
        Me.Name = "fkalkulasicontainer"
        Me.Text = "fkalkulasicontainer"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btncaripembelian As Button
    Friend WithEvents btnprev As Button
    Friend WithEvents btnnext As Button
    Friend WithEvents txtgopembelian As TextBox
    Friend WithEvents btngo As Button
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnbaru As Button
    Friend WithEvents btnprint As Button
    Friend WithEvents btnsimpan As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents cblunas As CheckBox
    Friend WithEvents cbvoid As CheckBox
    Friend WithEvents txtsupplier As TextBox
    Friend WithEvents btncarisupplier As Button
    Friend WithEvents dtpembelian As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbsupplier As ComboBox
    Friend WithEvents txtnonota As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtjatuhtempo As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblsatuanbeli As Label
    Friend WithEvents lblsatuan As Label
    Friend WithEvents btncari As Button
    Friend WithEvents btntambahbarang As Button
    Friend WithEvents txtbanyakbarang As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txthargabarang As TextBox
    Friend WithEvents txtnamabarang As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtkodebarang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txttotal As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtketerangan As RichTextBox
End Class
