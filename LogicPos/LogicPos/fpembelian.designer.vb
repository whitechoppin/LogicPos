<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpembelian
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fpembelian))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblsatuanbeli = New System.Windows.Forms.Label()
        Me.lblsatuan = New System.Windows.Forms.Label()
        Me.btncari = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.txtbanyak = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtharga = New System.Windows.Forms.TextBox()
        Me.txtnama = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtkodeitem = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbsupplier = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnproses = New System.Windows.Forms.Button()
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtnonota = New System.Windows.Forms.TextBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtongkir = New System.Windows.Forms.TextBox()
        Me.cbongkir = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbbayar = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cblunas = New System.Windows.Forms.CheckBox()
        Me.cbvoid = New System.Windows.Forms.CheckBox()
        Me.cbppn = New System.Windows.Forms.CheckBox()
        Me.cbdiskon = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblsatuanbeli)
        Me.GroupBox1.Controls.Add(Me.lblsatuan)
        Me.GroupBox1.Controls.Add(Me.btncari)
        Me.GroupBox1.Controls.Add(Me.btntambah)
        Me.GroupBox1.Controls.Add(Me.txtbanyak)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtharga)
        Me.GroupBox1.Controls.Add(Me.txtnama)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtkodeitem)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 162)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1025, 70)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'lblsatuanbeli
        '
        Me.lblsatuanbeli.AutoSize = True
        Me.lblsatuanbeli.Location = New System.Drawing.Point(865, 16)
        Me.lblsatuanbeli.Name = "lblsatuanbeli"
        Me.lblsatuanbeli.Size = New System.Drawing.Size(41, 13)
        Me.lblsatuanbeli.TabIndex = 26
        Me.lblsatuanbeli.Text = "Satuan"
        '
        'lblsatuan
        '
        Me.lblsatuan.AutoSize = True
        Me.lblsatuan.Location = New System.Drawing.Point(629, 40)
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
        Me.btncari.Location = New System.Drawing.Point(234, 32)
        Me.btncari.Name = "btncari"
        Me.btncari.Size = New System.Drawing.Size(29, 26)
        Me.btncari.TabIndex = 5
        Me.btncari.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.BackgroundImage = CType(resources.GetObject("btntambah.BackgroundImage"), System.Drawing.Image)
        Me.btntambah.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambah.ImageIndex = 0
        Me.btntambah.Location = New System.Drawing.Point(959, 12)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(60, 52)
        Me.btntambah.TabIndex = 5
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txtbanyak
        '
        Me.txtbanyak.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbanyak.Location = New System.Drawing.Point(564, 33)
        Me.txtbanyak.Name = "txtbanyak"
        Me.txtbanyak.Size = New System.Drawing.Size(59, 24)
        Me.txtbanyak.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(564, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Qty"
        '
        'txtharga
        '
        Me.txtharga.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtharga.Location = New System.Drawing.Point(785, 34)
        Me.txtharga.Name = "txtharga"
        Me.txtharga.Size = New System.Drawing.Size(168, 24)
        Me.txtharga.TabIndex = 10
        '
        'txtnama
        '
        Me.txtnama.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnama.Location = New System.Drawing.Point(274, 33)
        Me.txtnama.Name = "txtnama"
        Me.txtnama.Size = New System.Drawing.Size(284, 24)
        Me.txtnama.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(752, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 18)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Rp"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(782, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 18)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Harga Beli / "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(271, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Nama Barang"
        '
        'txtkodeitem
        '
        Me.txtkodeitem.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodeitem.Location = New System.Drawing.Point(11, 33)
        Me.txtkodeitem.Name = "txtkodeitem"
        Me.txtkodeitem.Size = New System.Drawing.Size(226, 24)
        Me.txtkodeitem.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kode Barang"
        '
        'cmbsupplier
        '
        Me.cmbsupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsupplier.FormattingEnabled = True
        Me.cmbsupplier.Location = New System.Drawing.Point(130, 51)
        Me.cmbsupplier.Name = "cmbsupplier"
        Me.cmbsupplier.Size = New System.Drawing.Size(252, 26)
        Me.cmbsupplier.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 18)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Supplier"
        '
        'btnproses
        '
        Me.btnproses.BackgroundImage = CType(resources.GetObject("btnproses.BackgroundImage"), System.Drawing.Image)
        Me.btnproses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnproses.Location = New System.Drawing.Point(431, 12)
        Me.btnproses.Name = "btnproses"
        Me.btnproses.Size = New System.Drawing.Size(75, 28)
        Me.btnproses.TabIndex = 5
        Me.btnproses.UseVisualStyleBackColor = True
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(12, 247)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1028, 209)
        Me.GridControl1.TabIndex = 13
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Kode Stok"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Kode Barang"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nama Barang"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Banyak"
        Me.GridColumn4.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Satuan barang"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Jenis barang"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Harga Satuan"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Sub Total"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(605, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 18)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Tanggal Jatuh Tempo"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = ""
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.DateTimePicker1.Location = New System.Drawing.Point(764, 52)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(252, 23)
        Me.DateTimePicker1.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(251, 31)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Pembelian Barang"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 18)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "No. Faktur"
        '
        'txtnonota
        '
        Me.txtnonota.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnonota.Location = New System.Drawing.Point(130, 23)
        Me.txtnonota.Name = "txtnonota"
        Me.txtnonota.Size = New System.Drawing.Size(252, 24)
        Me.txtnonota.TabIndex = 11
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 651)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(449, 59)
        Me.RichTextBox1.TabIndex = 18
        Me.RichTextBox1.Text = "Fungsi Tombol" & Global.Microsoft.VisualBasic.ChrW(10) & "- F2 pada kolom Kode Item untuk membatalkan Transaksi"
        '
        'txtketerangan
        '
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtketerangan.Location = New System.Drawing.Point(12, 488)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(598, 139)
        Me.txtketerangan.TabIndex = 19
        Me.txtketerangan.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(12, 461)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 18)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Keterangan"
        '
        'txtongkir
        '
        Me.txtongkir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.txtongkir.Location = New System.Drawing.Point(843, 521)
        Me.txtongkir.Name = "txtongkir"
        Me.txtongkir.Size = New System.Drawing.Size(198, 24)
        Me.txtongkir.TabIndex = 22
        '
        'cbongkir
        '
        Me.cbongkir.AutoSize = True
        Me.cbongkir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbongkir.Location = New System.Drawing.Point(739, 519)
        Me.cbongkir.Name = "cbongkir"
        Me.cbongkir.Size = New System.Drawing.Size(98, 22)
        Me.cbongkir.TabIndex = 23
        Me.cbongkir.Text = "Ongkir Rp."
        Me.cbongkir.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.cmbsupplier)
        Me.GroupBox2.Controls.Add(Me.txtnonota)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 43)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1026, 113)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Pembelian"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(130, 81)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(252, 26)
        Me.ComboBox1.TabIndex = 19
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(11, 83)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 18)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Gudang"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.DateTimePicker2.Location = New System.Drawing.Point(764, 20)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(252, 23)
        Me.DateTimePicker2.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label12.Location = New System.Drawing.Point(606, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(133, 18)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Tanggal Pembelian"
        '
        'cmbbayar
        '
        Me.cmbbayar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbayar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cmbbayar.FormattingEnabled = True
        Me.cmbbayar.Location = New System.Drawing.Point(802, 556)
        Me.cmbbayar.Name = "cmbbayar"
        Me.cmbbayar.Size = New System.Drawing.Size(239, 26)
        Me.cmbbayar.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label11.Location = New System.Drawing.Point(706, 559)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 18)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Pembayaran"
        '
        'cblunas
        '
        Me.cblunas.AutoSize = True
        Me.cblunas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cblunas.Location = New System.Drawing.Point(784, 20)
        Me.cblunas.Name = "cblunas"
        Me.cblunas.Size = New System.Drawing.Size(67, 22)
        Me.cblunas.TabIndex = 27
        Me.cblunas.Text = "Lunas"
        Me.cblunas.UseVisualStyleBackColor = True
        '
        'cbvoid
        '
        Me.cbvoid.AutoSize = True
        Me.cbvoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbvoid.Location = New System.Drawing.Point(884, 20)
        Me.cbvoid.Name = "cbvoid"
        Me.cbvoid.Size = New System.Drawing.Size(56, 22)
        Me.cbvoid.TabIndex = 28
        Me.cbvoid.Text = "Void"
        Me.cbvoid.UseVisualStyleBackColor = True
        '
        'cbppn
        '
        Me.cbppn.AutoSize = True
        Me.cbppn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbppn.Location = New System.Drawing.Point(739, 492)
        Me.cbppn.Name = "cbppn"
        Me.cbppn.Size = New System.Drawing.Size(58, 22)
        Me.cbppn.TabIndex = 29
        Me.cbppn.Text = "PPN"
        Me.cbppn.UseVisualStyleBackColor = True
        '
        'cbdiskon
        '
        Me.cbdiskon.AutoSize = True
        Me.cbdiskon.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbdiskon.Location = New System.Drawing.Point(739, 464)
        Me.cbdiskon.Name = "cbdiskon"
        Me.cbdiskon.Size = New System.Drawing.Size(74, 22)
        Me.cbdiskon.TabIndex = 30
        Me.cbdiskon.Text = "Diskon"
        Me.cbdiskon.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.TextBox1.Location = New System.Drawing.Point(843, 464)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(197, 24)
        Me.TextBox1.TabIndex = 31
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.TextBox2.Location = New System.Drawing.Point(843, 492)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(197, 24)
        Me.TextBox2.TabIndex = 32
        '
        'fpembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1052, 744)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.cbdiskon)
        Me.Controls.Add(Me.cbppn)
        Me.Controls.Add(Me.cbvoid)
        Me.Controls.Add(Me.cblunas)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmbbayar)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cbongkir)
        Me.Controls.Add(Me.txtongkir)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtketerangan)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.btnproses)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "fpembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transaksi Pembelian Barang"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbsupplier As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtbanyak As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtnama As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtkodeitem As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btntambah As System.Windows.Forms.Button
    Friend WithEvents btnproses As System.Windows.Forms.Button
    Friend WithEvents txtharga As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btncari As System.Windows.Forms.Button
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtnonota As TextBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtongkir As TextBox
    Friend WithEvents cbongkir As CheckBox
    Friend WithEvents lblsatuan As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cmbbayar As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblsatuanbeli As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cblunas As CheckBox
    Friend WithEvents cbvoid As CheckBox
    Friend WithEvents cbppn As CheckBox
    Friend WithEvents cbdiskon As CheckBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
End Class
