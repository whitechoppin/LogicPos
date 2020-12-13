<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fuser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fuser))
        Me.cmbjabatan = New System.Windows.Forms.ComboBox()
        Me.GridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtemail = New System.Windows.Forms.TextBox()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.txtalamat = New System.Windows.Forms.RichTextBox()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnhapus = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.txttelp = New System.Windows.Forms.TextBox()
        Me.txtnama = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtkode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.clblapmodalbarang = New System.Windows.Forms.CheckedListBox()
        Me.cblapmodalbarang = New System.Windows.Forms.CheckBox()
        Me.clblapbarangkeluar = New System.Windows.Forms.CheckedListBox()
        Me.cblapbarangkeluar = New System.Windows.Forms.CheckBox()
        Me.clblapbarangmasuk = New System.Windows.Forms.CheckedListBox()
        Me.cblapbarangmasuk = New System.Windows.Forms.CheckBox()
        Me.clblapreturjual = New System.Windows.Forms.CheckedListBox()
        Me.cblapreturjual = New System.Windows.Forms.CheckBox()
        Me.clblappricelist = New System.Windows.Forms.CheckedListBox()
        Me.cblappricelist = New System.Windows.Forms.CheckBox()
        Me.clblapreturbeli = New System.Windows.Forms.CheckedListBox()
        Me.cblapreturbeli = New System.Windows.Forms.CheckBox()
        Me.clbmasterkategori = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterkategori = New System.Windows.Forms.CheckBox()
        Me.clblaptransaksikas = New System.Windows.Forms.CheckedListBox()
        Me.cblaptransaksikas = New System.Windows.Forms.CheckBox()
        Me.clblaptransferbarang = New System.Windows.Forms.CheckedListBox()
        Me.cblaptransferbarang = New System.Windows.Forms.CheckBox()
        Me.clblaptransferkas = New System.Windows.Forms.CheckedListBox()
        Me.cblaptransferkas = New System.Windows.Forms.CheckBox()
        Me.clblapakunkeluar = New System.Windows.Forms.CheckedListBox()
        Me.cblapakunkeluar = New System.Windows.Forms.CheckBox()
        Me.clblapakunmasuk = New System.Windows.Forms.CheckedListBox()
        Me.cblapakunmasuk = New System.Windows.Forms.CheckBox()
        Me.clbakunkeluar = New System.Windows.Forms.CheckedListBox()
        Me.clbakunmasuk = New System.Windows.Forms.CheckedListBox()
        Me.cbakunkeluar = New System.Windows.Forms.CheckBox()
        Me.cbakunmasuk = New System.Windows.Forms.CheckBox()
        Me.clbtransferkas = New System.Windows.Forms.CheckedListBox()
        Me.clblunaspiutang = New System.Windows.Forms.CheckedListBox()
        Me.cbtransferkas = New System.Windows.Forms.CheckBox()
        Me.cblunaspiutang = New System.Windows.Forms.CheckBox()
        Me.clblunasutang = New System.Windows.Forms.CheckedListBox()
        Me.cblunasutang = New System.Windows.Forms.CheckBox()
        Me.clbtransferbarang = New System.Windows.Forms.CheckedListBox()
        Me.clbbarangkeluar = New System.Windows.Forms.CheckedListBox()
        Me.clbbarangmasuk = New System.Windows.Forms.CheckedListBox()
        Me.cbtransferbarang = New System.Windows.Forms.CheckBox()
        Me.cbbarangkeluar = New System.Windows.Forms.CheckBox()
        Me.cbbarangmasuk = New System.Windows.Forms.CheckBox()
        Me.clbmasterrekplng = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterrekplng = New System.Windows.Forms.CheckBox()
        Me.clbmasterreksupp = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterreksupp = New System.Windows.Forms.CheckBox()
        Me.clblapstokbarang = New System.Windows.Forms.CheckedListBox()
        Me.cblapstokbarang = New System.Windows.Forms.CheckBox()
        Me.clblappiutang = New System.Windows.Forms.CheckedListBox()
        Me.cblappiutang = New System.Windows.Forms.CheckBox()
        Me.clblaputang = New System.Windows.Forms.CheckedListBox()
        Me.cblaputang = New System.Windows.Forms.CheckBox()
        Me.clblappembelian = New System.Windows.Forms.CheckedListBox()
        Me.cblappembelian = New System.Windows.Forms.CheckBox()
        Me.clblappenjualan = New System.Windows.Forms.CheckedListBox()
        Me.cblappenjualan = New System.Windows.Forms.CheckBox()
        Me.clbreturjual = New System.Windows.Forms.CheckedListBox()
        Me.clbreturbeli = New System.Windows.Forms.CheckedListBox()
        Me.clbpenjualan = New System.Windows.Forms.CheckedListBox()
        Me.cbreturjual = New System.Windows.Forms.CheckBox()
        Me.cbreturbeli = New System.Windows.Forms.CheckBox()
        Me.cbpenjualan = New System.Windows.Forms.CheckBox()
        Me.clbpembelian = New System.Windows.Forms.CheckedListBox()
        Me.cbpembelian = New System.Windows.Forms.CheckBox()
        Me.clbmasteruser = New System.Windows.Forms.CheckedListBox()
        Me.cbmasteruser = New System.Windows.Forms.CheckBox()
        Me.clbmasterpricelist = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterpricelist = New System.Windows.Forms.CheckBox()
        Me.clbmasterkas = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterkas = New System.Windows.Forms.CheckBox()
        Me.clbmastersupplier = New System.Windows.Forms.CheckedListBox()
        Me.cbmastersupplier = New System.Windows.Forms.CheckBox()
        Me.clbmasterpelanggan = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterpelanggan = New System.Windows.Forms.CheckBox()
        Me.clbmastergudang = New System.Windows.Forms.CheckedListBox()
        Me.cbmastergudang = New System.Windows.Forms.CheckBox()
        Me.clbmasterbarang = New System.Windows.Forms.CheckedListBox()
        Me.cbmasterbarang = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cbpenyesuaianstok = New System.Windows.Forms.CheckBox()
        Me.clbpenyesuaianstok = New System.Windows.Forms.CheckedListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.clblaprekapanakhir = New System.Windows.Forms.CheckedListBox()
        Me.cblaprekapanakhir = New System.Windows.Forms.CheckBox()
        Me.clblaplabarugi = New System.Windows.Forms.CheckedListBox()
        Me.cblaplabarugi = New System.Windows.Forms.CheckBox()
        Me.clblappenjualanpajak = New System.Windows.Forms.CheckedListBox()
        Me.cblappenjualanpajak = New System.Windows.Forms.CheckBox()
        Me.clblapmutasibarang = New System.Windows.Forms.CheckedListBox()
        Me.cblapmutasibarang = New System.Windows.Forms.CheckBox()
        Me.clblappenyesuaianstok = New System.Windows.Forms.CheckedListBox()
        Me.cblappenyesuaianstok = New System.Windows.Forms.CheckBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.CheckedListBox5 = New System.Windows.Forms.CheckedListBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox6 = New System.Windows.Forms.CheckedListBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox3 = New System.Windows.Forms.CheckedListBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox4 = New System.Windows.Forms.CheckedListBox()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbauth = New System.Windows.Forms.CheckBox()
        Me.txtmaxprint = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnpassword = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbjabatan
        '
        Me.cmbjabatan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbjabatan.FormattingEnabled = True
        Me.cmbjabatan.Items.AddRange(New Object() {"Kasir", "Administrasi", "Supervisor", "Owner"})
        Me.cmbjabatan.Location = New System.Drawing.Point(108, 158)
        Me.cmbjabatan.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbjabatan.Name = "cmbjabatan"
        Me.cmbjabatan.Size = New System.Drawing.Size(168, 26)
        Me.cmbjabatan.TabIndex = 24
        '
        'GridControl
        '
        Me.GridControl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl.Location = New System.Drawing.Point(435, 66)
        Me.GridControl.MainView = Me.GridView
        Me.GridControl.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl.Name = "GridControl"
        Me.GridControl.Size = New System.Drawing.Size(588, 309)
        Me.GridControl.TabIndex = 17
        Me.GridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView})
        '
        'GridView
        '
        Me.GridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.GridView.GridControl = Me.GridControl
        Me.GridView.Name = "GridView"
        Me.GridView.OptionsBehavior.Editable = False
        Me.GridView.OptionsFind.AlwaysVisible = True
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
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
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'txtemail
        '
        Me.txtemail.Location = New System.Drawing.Point(108, 192)
        Me.txtemail.Margin = New System.Windows.Forms.Padding(4)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(308, 24)
        Me.txtemail.TabIndex = 23
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(108, 126)
        Me.txtpassword.Margin = New System.Windows.Forms.Padding(4)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtpassword.Size = New System.Drawing.Size(244, 24)
        Me.txtpassword.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 128)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 20)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Password"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(22, 160)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 20)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Jabatan"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(31, 194)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 20)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Email"
        '
        'txtketerangan
        '
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtketerangan.Location = New System.Drawing.Point(108, 319)
        Me.txtketerangan.Margin = New System.Windows.Forms.Padding(4)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(308, 56)
        Me.txtketerangan.TabIndex = 17
        Me.txtketerangan.Text = ""
        '
        'txtalamat
        '
        Me.txtalamat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtalamat.Location = New System.Drawing.Point(108, 255)
        Me.txtalamat.Margin = New System.Windows.Forms.Padding(4)
        Me.txtalamat.Name = "txtalamat"
        Me.txtalamat.Size = New System.Drawing.Size(308, 56)
        Me.txtalamat.TabIndex = 17
        Me.txtalamat.Text = ""
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(657, 9)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(4)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(149, 37)
        Me.btnbatal.TabIndex = 13
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnhapus
        '
        Me.btnhapus.Location = New System.Drawing.Point(500, 9)
        Me.btnhapus.Margin = New System.Windows.Forms.Padding(4)
        Me.btnhapus.Name = "btnhapus"
        Me.btnhapus.Size = New System.Drawing.Size(149, 37)
        Me.btnhapus.TabIndex = 14
        Me.btnhapus.Text = "Hapus"
        Me.btnhapus.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(343, 9)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(149, 37)
        Me.btnedit.TabIndex = 15
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.Location = New System.Drawing.Point(186, 9)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(4)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(149, 37)
        Me.btntambah.TabIndex = 16
        Me.btntambah.Text = "Tambah"
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txttelp
        '
        Me.txttelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttelp.Location = New System.Drawing.Point(108, 224)
        Me.txttelp.Margin = New System.Windows.Forms.Padding(4)
        Me.txttelp.Name = "txttelp"
        Me.txttelp.Size = New System.Drawing.Size(308, 22)
        Me.txttelp.TabIndex = 2
        '
        'txtnama
        '
        Me.txtnama.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnama.Location = New System.Drawing.Point(108, 96)
        Me.txtnama.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnama.Name = "txtnama"
        Me.txtnama.Size = New System.Drawing.Size(308, 22)
        Me.txtnama.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 320)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 20)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Keterangan"
        '
        'txtkode
        '
        Me.txtkode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtkode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkode.Location = New System.Drawing.Point(108, 66)
        Me.txtkode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtkode.Name = "txtkode"
        Me.txtkode.Size = New System.Drawing.Size(308, 22)
        Me.txtkode.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 255)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Alamat"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 223)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Telepon"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 96)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Nama User"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 66)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Kode User"
        '
        'clblapmodalbarang
        '
        Me.clblapmodalbarang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapmodalbarang.CheckOnClick = True
        Me.clblapmodalbarang.FormattingEnabled = True
        Me.clblapmodalbarang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapmodalbarang.Location = New System.Drawing.Point(678, 43)
        Me.clblapmodalbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapmodalbarang.Name = "clblapmodalbarang"
        Me.clblapmodalbarang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapmodalbarang.Size = New System.Drawing.Size(134, 42)
        Me.clblapmodalbarang.TabIndex = 115
        '
        'cblapmodalbarang
        '
        Me.cblapmodalbarang.AutoSize = True
        Me.cblapmodalbarang.Location = New System.Drawing.Point(664, 13)
        Me.cblapmodalbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapmodalbarang.Name = "cblapmodalbarang"
        Me.cblapmodalbarang.Size = New System.Drawing.Size(151, 22)
        Me.cblapmodalbarang.TabIndex = 114
        Me.cblapmodalbarang.Text = "Lap. Modal Barang"
        Me.cblapmodalbarang.UseVisualStyleBackColor = True
        '
        'clblapbarangkeluar
        '
        Me.clblapbarangkeluar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapbarangkeluar.CheckOnClick = True
        Me.clblapbarangkeluar.FormattingEnabled = True
        Me.clblapbarangkeluar.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapbarangkeluar.Location = New System.Drawing.Point(186, 283)
        Me.clblapbarangkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapbarangkeluar.Name = "clblapbarangkeluar"
        Me.clblapbarangkeluar.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapbarangkeluar.Size = New System.Drawing.Size(134, 42)
        Me.clblapbarangkeluar.TabIndex = 113
        '
        'cblapbarangkeluar
        '
        Me.cblapbarangkeluar.AutoSize = True
        Me.cblapbarangkeluar.Location = New System.Drawing.Point(176, 253)
        Me.cblapbarangkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapbarangkeluar.Name = "cblapbarangkeluar"
        Me.cblapbarangkeluar.Size = New System.Drawing.Size(152, 22)
        Me.cblapbarangkeluar.TabIndex = 112
        Me.cblapbarangkeluar.Text = "Lap. Barang Keluar"
        Me.cblapbarangkeluar.UseVisualStyleBackColor = True
        '
        'clblapbarangmasuk
        '
        Me.clblapbarangmasuk.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapbarangmasuk.CheckOnClick = True
        Me.clblapbarangmasuk.FormattingEnabled = True
        Me.clblapbarangmasuk.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapbarangmasuk.Location = New System.Drawing.Point(186, 203)
        Me.clblapbarangmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapbarangmasuk.Name = "clblapbarangmasuk"
        Me.clblapbarangmasuk.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapbarangmasuk.Size = New System.Drawing.Size(134, 42)
        Me.clblapbarangmasuk.TabIndex = 111
        '
        'cblapbarangmasuk
        '
        Me.cblapbarangmasuk.AutoSize = True
        Me.cblapbarangmasuk.Location = New System.Drawing.Point(176, 173)
        Me.cblapbarangmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapbarangmasuk.Name = "cblapbarangmasuk"
        Me.cblapbarangmasuk.Size = New System.Drawing.Size(155, 22)
        Me.cblapbarangmasuk.TabIndex = 110
        Me.cblapbarangmasuk.Text = "Lap. Barang Masuk"
        Me.cblapbarangmasuk.UseVisualStyleBackColor = True
        '
        'clblapreturjual
        '
        Me.clblapreturjual.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapreturjual.CheckOnClick = True
        Me.clblapreturjual.FormattingEnabled = True
        Me.clblapreturjual.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapreturjual.Location = New System.Drawing.Point(186, 123)
        Me.clblapreturjual.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapreturjual.Name = "clblapreturjual"
        Me.clblapreturjual.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapreturjual.Size = New System.Drawing.Size(134, 42)
        Me.clblapreturjual.TabIndex = 109
        '
        'cblapreturjual
        '
        Me.cblapreturjual.AutoSize = True
        Me.cblapreturjual.Location = New System.Drawing.Point(176, 93)
        Me.cblapreturjual.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapreturjual.Name = "cblapreturjual"
        Me.cblapreturjual.Size = New System.Drawing.Size(126, 22)
        Me.cblapreturjual.TabIndex = 108
        Me.cblapreturjual.Text = "Lap. Retur Jual"
        Me.cblapreturjual.UseVisualStyleBackColor = True
        '
        'clblappricelist
        '
        Me.clblappricelist.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblappricelist.CheckOnClick = True
        Me.clblappricelist.FormattingEnabled = True
        Me.clblappricelist.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblappricelist.Location = New System.Drawing.Point(18, 43)
        Me.clblappricelist.Margin = New System.Windows.Forms.Padding(4)
        Me.clblappricelist.Name = "clblappricelist"
        Me.clblappricelist.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblappricelist.Size = New System.Drawing.Size(134, 42)
        Me.clblappricelist.TabIndex = 107
        '
        'cblappricelist
        '
        Me.cblappricelist.AutoSize = True
        Me.cblappricelist.Location = New System.Drawing.Point(8, 13)
        Me.cblappricelist.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappricelist.Name = "cblappricelist"
        Me.cblappricelist.Size = New System.Drawing.Size(111, 22)
        Me.cblappricelist.TabIndex = 106
        Me.cblappricelist.Text = "Lap. Pricelist"
        Me.cblappricelist.UseVisualStyleBackColor = True
        '
        'clblapreturbeli
        '
        Me.clblapreturbeli.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapreturbeli.CheckOnClick = True
        Me.clblapreturbeli.FormattingEnabled = True
        Me.clblapreturbeli.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapreturbeli.Location = New System.Drawing.Point(186, 43)
        Me.clblapreturbeli.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapreturbeli.Name = "clblapreturbeli"
        Me.clblapreturbeli.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapreturbeli.Size = New System.Drawing.Size(134, 42)
        Me.clblapreturbeli.TabIndex = 105
        '
        'cblapreturbeli
        '
        Me.cblapreturbeli.AutoSize = True
        Me.cblapreturbeli.Location = New System.Drawing.Point(176, 13)
        Me.cblapreturbeli.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapreturbeli.Name = "cblapreturbeli"
        Me.cblapreturbeli.Size = New System.Drawing.Size(123, 22)
        Me.cblapreturbeli.TabIndex = 104
        Me.cblapreturbeli.Text = "Lap. Retur Beli"
        Me.cblapreturbeli.UseVisualStyleBackColor = True
        '
        'clbmasterkategori
        '
        Me.clbmasterkategori.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterkategori.CheckOnClick = True
        Me.clbmasterkategori.FormattingEnabled = True
        Me.clbmasterkategori.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterkategori.Location = New System.Drawing.Point(19, 132)
        Me.clbmasterkategori.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterkategori.Name = "clbmasterkategori"
        Me.clbmasterkategori.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterkategori.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterkategori.TabIndex = 103
        '
        'cbmasterkategori
        '
        Me.cbmasterkategori.AutoSize = True
        Me.cbmasterkategori.Location = New System.Drawing.Point(8, 102)
        Me.cbmasterkategori.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterkategori.Name = "cbmasterkategori"
        Me.cbmasterkategori.Size = New System.Drawing.Size(132, 22)
        Me.cbmasterkategori.TabIndex = 102
        Me.cbmasterkategori.Text = "Master Kategori"
        Me.cbmasterkategori.UseVisualStyleBackColor = True
        '
        'clblaptransaksikas
        '
        Me.clblaptransaksikas.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaptransaksikas.CheckOnClick = True
        Me.clblaptransaksikas.FormattingEnabled = True
        Me.clblaptransaksikas.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaptransaksikas.Location = New System.Drawing.Point(515, 283)
        Me.clblaptransaksikas.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaptransaksikas.Name = "clblaptransaksikas"
        Me.clblaptransaksikas.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaptransaksikas.Size = New System.Drawing.Size(134, 42)
        Me.clblaptransaksikas.TabIndex = 101
        '
        'cblaptransaksikas
        '
        Me.cblaptransaksikas.AutoSize = True
        Me.cblaptransaksikas.Location = New System.Drawing.Point(505, 253)
        Me.cblaptransaksikas.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaptransaksikas.Name = "cblaptransaksikas"
        Me.cblaptransaksikas.Size = New System.Drawing.Size(154, 22)
        Me.cblaptransaksikas.TabIndex = 100
        Me.cblaptransaksikas.Text = "Lap. Transaksi Kas"
        Me.cblaptransaksikas.UseVisualStyleBackColor = True
        '
        'clblaptransferbarang
        '
        Me.clblaptransferbarang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaptransferbarang.CheckOnClick = True
        Me.clblaptransferbarang.FormattingEnabled = True
        Me.clblaptransferbarang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaptransferbarang.Location = New System.Drawing.Point(342, 43)
        Me.clblaptransferbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaptransferbarang.Name = "clblaptransferbarang"
        Me.clblaptransferbarang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaptransferbarang.Size = New System.Drawing.Size(134, 42)
        Me.clblaptransferbarang.TabIndex = 99
        '
        'cblaptransferbarang
        '
        Me.cblaptransferbarang.AutoSize = True
        Me.cblaptransferbarang.Location = New System.Drawing.Point(332, 13)
        Me.cblaptransferbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaptransferbarang.Name = "cblaptransferbarang"
        Me.cblaptransferbarang.Size = New System.Drawing.Size(165, 22)
        Me.cblaptransferbarang.TabIndex = 98
        Me.cblaptransferbarang.Text = "Lap. Transfer Barang"
        Me.cblaptransferbarang.UseVisualStyleBackColor = True
        '
        'clblaptransferkas
        '
        Me.clblaptransferkas.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaptransferkas.CheckOnClick = True
        Me.clblaptransferkas.FormattingEnabled = True
        Me.clblaptransferkas.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaptransferkas.Location = New System.Drawing.Point(515, 203)
        Me.clblaptransferkas.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaptransferkas.Name = "clblaptransferkas"
        Me.clblaptransferkas.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaptransferkas.Size = New System.Drawing.Size(134, 42)
        Me.clblaptransferkas.TabIndex = 97
        '
        'cblaptransferkas
        '
        Me.cblaptransferkas.AutoSize = True
        Me.cblaptransferkas.Location = New System.Drawing.Point(505, 173)
        Me.cblaptransferkas.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaptransferkas.Name = "cblaptransferkas"
        Me.cblaptransferkas.Size = New System.Drawing.Size(144, 22)
        Me.cblaptransferkas.TabIndex = 96
        Me.cblaptransferkas.Text = "Lap. Transfer Kas"
        Me.cblaptransferkas.UseVisualStyleBackColor = True
        '
        'clblapakunkeluar
        '
        Me.clblapakunkeluar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapakunkeluar.CheckOnClick = True
        Me.clblapakunkeluar.FormattingEnabled = True
        Me.clblapakunkeluar.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapakunkeluar.Location = New System.Drawing.Point(515, 123)
        Me.clblapakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapakunkeluar.Name = "clblapakunkeluar"
        Me.clblapakunkeluar.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapakunkeluar.Size = New System.Drawing.Size(134, 42)
        Me.clblapakunkeluar.TabIndex = 95
        '
        'cblapakunkeluar
        '
        Me.cblapakunkeluar.AutoSize = True
        Me.cblapakunkeluar.Location = New System.Drawing.Point(505, 93)
        Me.cblapakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapakunkeluar.Name = "cblapakunkeluar"
        Me.cblapakunkeluar.Size = New System.Drawing.Size(138, 22)
        Me.cblapakunkeluar.TabIndex = 94
        Me.cblapakunkeluar.Text = "Lap. Akun Keluar"
        Me.cblapakunkeluar.UseVisualStyleBackColor = True
        '
        'clblapakunmasuk
        '
        Me.clblapakunmasuk.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapakunmasuk.CheckOnClick = True
        Me.clblapakunmasuk.FormattingEnabled = True
        Me.clblapakunmasuk.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapakunmasuk.Location = New System.Drawing.Point(515, 43)
        Me.clblapakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapakunmasuk.Name = "clblapakunmasuk"
        Me.clblapakunmasuk.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapakunmasuk.Size = New System.Drawing.Size(134, 42)
        Me.clblapakunmasuk.TabIndex = 93
        '
        'cblapakunmasuk
        '
        Me.cblapakunmasuk.AutoSize = True
        Me.cblapakunmasuk.Location = New System.Drawing.Point(505, 13)
        Me.cblapakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapakunmasuk.Name = "cblapakunmasuk"
        Me.cblapakunmasuk.Size = New System.Drawing.Size(141, 22)
        Me.cblapakunmasuk.TabIndex = 92
        Me.cblapakunmasuk.Text = "Lap. Akun Masuk"
        Me.cblapakunmasuk.UseVisualStyleBackColor = True
        '
        'clbakunkeluar
        '
        Me.clbakunkeluar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbakunkeluar.CheckOnClick = True
        Me.clbakunkeluar.FormattingEnabled = True
        Me.clbakunkeluar.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbakunkeluar.Location = New System.Drawing.Point(162, 141)
        Me.clbakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.clbakunkeluar.Name = "clbakunkeluar"
        Me.clbakunkeluar.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbakunkeluar.Size = New System.Drawing.Size(100, 61)
        Me.clbakunkeluar.TabIndex = 90
        '
        'clbakunmasuk
        '
        Me.clbakunmasuk.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbakunmasuk.CheckOnClick = True
        Me.clbakunmasuk.FormattingEnabled = True
        Me.clbakunmasuk.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbakunmasuk.Location = New System.Drawing.Point(162, 42)
        Me.clbakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.clbakunmasuk.Name = "clbakunmasuk"
        Me.clbakunmasuk.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbakunmasuk.Size = New System.Drawing.Size(100, 61)
        Me.clbakunmasuk.TabIndex = 89
        '
        'cbakunkeluar
        '
        Me.cbakunkeluar.AutoSize = True
        Me.cbakunkeluar.Location = New System.Drawing.Point(153, 111)
        Me.cbakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cbakunkeluar.Name = "cbakunkeluar"
        Me.cbakunkeluar.Size = New System.Drawing.Size(106, 22)
        Me.cbakunkeluar.TabIndex = 87
        Me.cbakunkeluar.Text = "Akun Keluar"
        Me.cbakunkeluar.UseVisualStyleBackColor = True
        '
        'cbakunmasuk
        '
        Me.cbakunmasuk.AutoSize = True
        Me.cbakunmasuk.Location = New System.Drawing.Point(153, 12)
        Me.cbakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cbakunmasuk.Name = "cbakunmasuk"
        Me.cbakunmasuk.Size = New System.Drawing.Size(109, 22)
        Me.cbakunmasuk.TabIndex = 86
        Me.cbakunmasuk.Text = "Akun Masuk"
        Me.cbakunmasuk.UseVisualStyleBackColor = True
        '
        'clbtransferkas
        '
        Me.clbtransferkas.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbtransferkas.CheckOnClick = True
        Me.clbtransferkas.FormattingEnabled = True
        Me.clbtransferkas.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbtransferkas.Location = New System.Drawing.Point(321, 42)
        Me.clbtransferkas.Margin = New System.Windows.Forms.Padding(4)
        Me.clbtransferkas.Name = "clbtransferkas"
        Me.clbtransferkas.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbtransferkas.Size = New System.Drawing.Size(100, 61)
        Me.clbtransferkas.TabIndex = 85
        '
        'clblunaspiutang
        '
        Me.clblunaspiutang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblunaspiutang.CheckOnClick = True
        Me.clblunaspiutang.FormattingEnabled = True
        Me.clblunaspiutang.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clblunaspiutang.Location = New System.Drawing.Point(21, 141)
        Me.clblunaspiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblunaspiutang.Name = "clblunaspiutang"
        Me.clblunaspiutang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblunaspiutang.Size = New System.Drawing.Size(100, 61)
        Me.clblunaspiutang.TabIndex = 84
        '
        'cbtransferkas
        '
        Me.cbtransferkas.AutoSize = True
        Me.cbtransferkas.Location = New System.Drawing.Point(309, 12)
        Me.cbtransferkas.Margin = New System.Windows.Forms.Padding(4)
        Me.cbtransferkas.Name = "cbtransferkas"
        Me.cbtransferkas.Size = New System.Drawing.Size(112, 22)
        Me.cbtransferkas.TabIndex = 83
        Me.cbtransferkas.Text = "Transfer Kas"
        Me.cbtransferkas.UseVisualStyleBackColor = True
        '
        'cblunaspiutang
        '
        Me.cblunaspiutang.AutoSize = True
        Me.cblunaspiutang.Location = New System.Drawing.Point(7, 111)
        Me.cblunaspiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblunaspiutang.Name = "cblunaspiutang"
        Me.cblunaspiutang.Size = New System.Drawing.Size(120, 22)
        Me.cblunaspiutang.TabIndex = 82
        Me.cblunaspiutang.Text = "Lunas Piutang"
        Me.cblunaspiutang.UseVisualStyleBackColor = True
        '
        'clblunasutang
        '
        Me.clblunasutang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblunasutang.CheckOnClick = True
        Me.clblunasutang.FormattingEnabled = True
        Me.clblunasutang.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clblunasutang.Location = New System.Drawing.Point(21, 42)
        Me.clblunasutang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblunasutang.Name = "clblunasutang"
        Me.clblunasutang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblunasutang.Size = New System.Drawing.Size(100, 61)
        Me.clblunasutang.TabIndex = 81
        '
        'cblunasutang
        '
        Me.cblunasutang.AutoSize = True
        Me.cblunasutang.Location = New System.Drawing.Point(9, 12)
        Me.cblunasutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblunasutang.Name = "cblunasutang"
        Me.cblunasutang.Size = New System.Drawing.Size(110, 22)
        Me.cblunasutang.TabIndex = 80
        Me.cblunasutang.Text = "Lunas Utang"
        Me.cblunasutang.UseVisualStyleBackColor = True
        '
        'clbtransferbarang
        '
        Me.clbtransferbarang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbtransferbarang.CheckOnClick = True
        Me.clbtransferbarang.FormattingEnabled = True
        Me.clbtransferbarang.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbtransferbarang.Location = New System.Drawing.Point(459, 42)
        Me.clbtransferbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.clbtransferbarang.Name = "clbtransferbarang"
        Me.clbtransferbarang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbtransferbarang.Size = New System.Drawing.Size(100, 61)
        Me.clbtransferbarang.TabIndex = 79
        '
        'clbbarangkeluar
        '
        Me.clbbarangkeluar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbbarangkeluar.CheckOnClick = True
        Me.clbbarangkeluar.FormattingEnabled = True
        Me.clbbarangkeluar.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbbarangkeluar.Location = New System.Drawing.Point(307, 141)
        Me.clbbarangkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.clbbarangkeluar.Name = "clbbarangkeluar"
        Me.clbbarangkeluar.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbbarangkeluar.Size = New System.Drawing.Size(100, 61)
        Me.clbbarangkeluar.TabIndex = 78
        '
        'clbbarangmasuk
        '
        Me.clbbarangmasuk.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbbarangmasuk.CheckOnClick = True
        Me.clbbarangmasuk.FormattingEnabled = True
        Me.clbbarangmasuk.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbbarangmasuk.Location = New System.Drawing.Point(307, 42)
        Me.clbbarangmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.clbbarangmasuk.Name = "clbbarangmasuk"
        Me.clbbarangmasuk.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbbarangmasuk.Size = New System.Drawing.Size(100, 61)
        Me.clbbarangmasuk.TabIndex = 77
        '
        'cbtransferbarang
        '
        Me.cbtransferbarang.AutoSize = True
        Me.cbtransferbarang.Location = New System.Drawing.Point(442, 12)
        Me.cbtransferbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbtransferbarang.Name = "cbtransferbarang"
        Me.cbtransferbarang.Size = New System.Drawing.Size(133, 22)
        Me.cbtransferbarang.TabIndex = 76
        Me.cbtransferbarang.Text = "Transfer Barang"
        Me.cbtransferbarang.UseVisualStyleBackColor = True
        '
        'cbbarangkeluar
        '
        Me.cbbarangkeluar.AutoSize = True
        Me.cbbarangkeluar.Location = New System.Drawing.Point(295, 111)
        Me.cbbarangkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cbbarangkeluar.Name = "cbbarangkeluar"
        Me.cbbarangkeluar.Size = New System.Drawing.Size(120, 22)
        Me.cbbarangkeluar.TabIndex = 75
        Me.cbbarangkeluar.Text = "Barang Keluar"
        Me.cbbarangkeluar.UseVisualStyleBackColor = True
        '
        'cbbarangmasuk
        '
        Me.cbbarangmasuk.AutoSize = True
        Me.cbbarangmasuk.Location = New System.Drawing.Point(295, 12)
        Me.cbbarangmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cbbarangmasuk.Name = "cbbarangmasuk"
        Me.cbbarangmasuk.Size = New System.Drawing.Size(123, 22)
        Me.cbbarangmasuk.TabIndex = 74
        Me.cbbarangmasuk.Text = "Barang Masuk"
        Me.cbbarangmasuk.UseVisualStyleBackColor = True
        '
        'clbmasterrekplng
        '
        Me.clbmasterrekplng.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterrekplng.CheckOnClick = True
        Me.clbmasterrekplng.FormattingEnabled = True
        Me.clbmasterrekplng.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterrekplng.Location = New System.Drawing.Point(455, 132)
        Me.clbmasterrekplng.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterrekplng.Name = "clbmasterrekplng"
        Me.clbmasterrekplng.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterrekplng.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterrekplng.TabIndex = 73
        '
        'cbmasterrekplng
        '
        Me.cbmasterrekplng.AutoSize = True
        Me.cbmasterrekplng.Location = New System.Drawing.Point(439, 102)
        Me.cbmasterrekplng.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterrekplng.Name = "cbmasterrekplng"
        Me.cbmasterrekplng.Size = New System.Drawing.Size(177, 22)
        Me.cbmasterrekplng.TabIndex = 72
        Me.cbmasterrekplng.Text = "Master Rek Pelanggan"
        Me.cbmasterrekplng.UseVisualStyleBackColor = True
        '
        'clbmasterreksupp
        '
        Me.clbmasterreksupp.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterreksupp.CheckOnClick = True
        Me.clbmasterreksupp.FormattingEnabled = True
        Me.clbmasterreksupp.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterreksupp.Location = New System.Drawing.Point(455, 33)
        Me.clbmasterreksupp.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterreksupp.Name = "clbmasterreksupp"
        Me.clbmasterreksupp.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterreksupp.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterreksupp.TabIndex = 71
        '
        'cbmasterreksupp
        '
        Me.cbmasterreksupp.AutoSize = True
        Me.cbmasterreksupp.Location = New System.Drawing.Point(439, 8)
        Me.cbmasterreksupp.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterreksupp.Name = "cbmasterreksupp"
        Me.cbmasterreksupp.Size = New System.Drawing.Size(161, 22)
        Me.cbmasterreksupp.TabIndex = 70
        Me.cbmasterreksupp.Text = "Master Rek Supplier"
        Me.cbmasterreksupp.UseVisualStyleBackColor = True
        '
        'clblapstokbarang
        '
        Me.clblapstokbarang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapstokbarang.CheckOnClick = True
        Me.clblapstokbarang.FormattingEnabled = True
        Me.clblapstokbarang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapstokbarang.Location = New System.Drawing.Point(342, 123)
        Me.clblapstokbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapstokbarang.Name = "clblapstokbarang"
        Me.clblapstokbarang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapstokbarang.Size = New System.Drawing.Size(134, 42)
        Me.clblapstokbarang.TabIndex = 69
        '
        'cblapstokbarang
        '
        Me.cblapstokbarang.AutoSize = True
        Me.cblapstokbarang.Location = New System.Drawing.Point(332, 93)
        Me.cblapstokbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapstokbarang.Name = "cblapstokbarang"
        Me.cblapstokbarang.Size = New System.Drawing.Size(141, 22)
        Me.cblapstokbarang.TabIndex = 68
        Me.cblapstokbarang.Text = "Lap. Stok Barang"
        Me.cblapstokbarang.UseVisualStyleBackColor = True
        '
        'clblappiutang
        '
        Me.clblappiutang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblappiutang.CheckOnClick = True
        Me.clblappiutang.FormattingEnabled = True
        Me.clblappiutang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblappiutang.Location = New System.Drawing.Point(342, 283)
        Me.clblappiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblappiutang.Name = "clblappiutang"
        Me.clblappiutang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblappiutang.Size = New System.Drawing.Size(134, 42)
        Me.clblappiutang.TabIndex = 59
        '
        'cblappiutang
        '
        Me.cblappiutang.AutoSize = True
        Me.cblappiutang.Location = New System.Drawing.Point(332, 253)
        Me.cblappiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappiutang.Name = "cblappiutang"
        Me.cblappiutang.Size = New System.Drawing.Size(152, 22)
        Me.cblappiutang.TabIndex = 58
        Me.cblappiutang.Text = "Lap. Lunas Piutang"
        Me.cblappiutang.UseVisualStyleBackColor = True
        '
        'clblaputang
        '
        Me.clblaputang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaputang.CheckOnClick = True
        Me.clblaputang.FormattingEnabled = True
        Me.clblaputang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaputang.Location = New System.Drawing.Point(342, 203)
        Me.clblaputang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaputang.Name = "clblaputang"
        Me.clblaputang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaputang.Size = New System.Drawing.Size(134, 42)
        Me.clblaputang.TabIndex = 57
        '
        'cblaputang
        '
        Me.cblaputang.AutoSize = True
        Me.cblaputang.Location = New System.Drawing.Point(332, 173)
        Me.cblaputang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaputang.Name = "cblaputang"
        Me.cblaputang.Size = New System.Drawing.Size(142, 22)
        Me.cblaputang.TabIndex = 56
        Me.cblaputang.Text = "Lap. Lunas Utang"
        Me.cblaputang.UseVisualStyleBackColor = True
        '
        'clblappembelian
        '
        Me.clblappembelian.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblappembelian.CheckOnClick = True
        Me.clblappembelian.FormattingEnabled = True
        Me.clblappembelian.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblappembelian.Location = New System.Drawing.Point(18, 123)
        Me.clblappembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.clblappembelian.Name = "clblappembelian"
        Me.clblappembelian.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblappembelian.Size = New System.Drawing.Size(134, 42)
        Me.clblappembelian.TabIndex = 55
        '
        'cblappembelian
        '
        Me.cblappembelian.AutoSize = True
        Me.cblappembelian.Location = New System.Drawing.Point(8, 93)
        Me.cblappembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappembelian.Name = "cblappembelian"
        Me.cblappembelian.Size = New System.Drawing.Size(128, 22)
        Me.cblappembelian.TabIndex = 54
        Me.cblappembelian.Text = "Lap. Pembelian"
        Me.cblappembelian.UseVisualStyleBackColor = True
        '
        'clblappenjualan
        '
        Me.clblappenjualan.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblappenjualan.CheckOnClick = True
        Me.clblappenjualan.FormattingEnabled = True
        Me.clblappenjualan.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblappenjualan.Location = New System.Drawing.Point(18, 203)
        Me.clblappenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.clblappenjualan.Name = "clblappenjualan"
        Me.clblappenjualan.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblappenjualan.Size = New System.Drawing.Size(134, 42)
        Me.clblappenjualan.TabIndex = 53
        '
        'cblappenjualan
        '
        Me.cblappenjualan.AutoSize = True
        Me.cblappenjualan.Location = New System.Drawing.Point(8, 173)
        Me.cblappenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappenjualan.Name = "cblappenjualan"
        Me.cblappenjualan.Size = New System.Drawing.Size(123, 22)
        Me.cblappenjualan.TabIndex = 52
        Me.cblappenjualan.Text = "Lap. Penjualan"
        Me.cblappenjualan.UseVisualStyleBackColor = True
        '
        'clbreturjual
        '
        Me.clbreturjual.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbreturjual.CheckOnClick = True
        Me.clbreturjual.FormattingEnabled = True
        Me.clbreturjual.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbreturjual.Location = New System.Drawing.Point(165, 143)
        Me.clbreturjual.Margin = New System.Windows.Forms.Padding(4)
        Me.clbreturjual.Name = "clbreturjual"
        Me.clbreturjual.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbreturjual.Size = New System.Drawing.Size(100, 61)
        Me.clbreturjual.TabIndex = 40
        '
        'clbreturbeli
        '
        Me.clbreturbeli.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbreturbeli.CheckOnClick = True
        Me.clbreturbeli.FormattingEnabled = True
        Me.clbreturbeli.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbreturbeli.Location = New System.Drawing.Point(165, 42)
        Me.clbreturbeli.Margin = New System.Windows.Forms.Padding(4)
        Me.clbreturbeli.Name = "clbreturbeli"
        Me.clbreturbeli.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbreturbeli.Size = New System.Drawing.Size(100, 61)
        Me.clbreturbeli.TabIndex = 38
        '
        'clbpenjualan
        '
        Me.clbpenjualan.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbpenjualan.CheckOnClick = True
        Me.clbpenjualan.FormattingEnabled = True
        Me.clbpenjualan.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbpenjualan.Location = New System.Drawing.Point(22, 141)
        Me.clbpenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.clbpenjualan.Name = "clbpenjualan"
        Me.clbpenjualan.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbpenjualan.Size = New System.Drawing.Size(100, 61)
        Me.clbpenjualan.TabIndex = 36
        '
        'cbreturjual
        '
        Me.cbreturjual.AutoSize = True
        Me.cbreturjual.Location = New System.Drawing.Point(155, 111)
        Me.cbreturjual.Margin = New System.Windows.Forms.Padding(4)
        Me.cbreturjual.Name = "cbreturjual"
        Me.cbreturjual.Size = New System.Drawing.Size(94, 22)
        Me.cbreturjual.TabIndex = 28
        Me.cbreturjual.Text = "Retur Jual"
        Me.cbreturjual.UseVisualStyleBackColor = True
        '
        'cbreturbeli
        '
        Me.cbreturbeli.AutoSize = True
        Me.cbreturbeli.Location = New System.Drawing.Point(155, 12)
        Me.cbreturbeli.Margin = New System.Windows.Forms.Padding(4)
        Me.cbreturbeli.Name = "cbreturbeli"
        Me.cbreturbeli.Size = New System.Drawing.Size(91, 22)
        Me.cbreturbeli.TabIndex = 26
        Me.cbreturbeli.Text = "Retur Beli"
        Me.cbreturbeli.UseVisualStyleBackColor = True
        '
        'cbpenjualan
        '
        Me.cbpenjualan.AutoSize = True
        Me.cbpenjualan.Location = New System.Drawing.Point(8, 111)
        Me.cbpenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.cbpenjualan.Name = "cbpenjualan"
        Me.cbpenjualan.Size = New System.Drawing.Size(91, 22)
        Me.cbpenjualan.TabIndex = 24
        Me.cbpenjualan.Text = "Penjualan"
        Me.cbpenjualan.UseVisualStyleBackColor = True
        '
        'clbpembelian
        '
        Me.clbpembelian.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbpembelian.CheckOnClick = True
        Me.clbpembelian.FormattingEnabled = True
        Me.clbpembelian.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbpembelian.Location = New System.Drawing.Point(22, 42)
        Me.clbpembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.clbpembelian.Name = "clbpembelian"
        Me.clbpembelian.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbpembelian.Size = New System.Drawing.Size(100, 61)
        Me.clbpembelian.TabIndex = 23
        '
        'cbpembelian
        '
        Me.cbpembelian.AutoSize = True
        Me.cbpembelian.Location = New System.Drawing.Point(8, 12)
        Me.cbpembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.cbpembelian.Name = "cbpembelian"
        Me.cbpembelian.Size = New System.Drawing.Size(96, 22)
        Me.cbpembelian.TabIndex = 22
        Me.cbpembelian.Text = "Pembelian"
        Me.cbpembelian.UseVisualStyleBackColor = True
        '
        'clbmasteruser
        '
        Me.clbmasteruser.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasteruser.CheckOnClick = True
        Me.clbmasteruser.FormattingEnabled = True
        Me.clbmasteruser.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasteruser.Location = New System.Drawing.Point(161, 231)
        Me.clbmasteruser.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasteruser.Name = "clbmasteruser"
        Me.clbmasteruser.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasteruser.Size = New System.Drawing.Size(100, 61)
        Me.clbmasteruser.TabIndex = 21
        '
        'cbmasteruser
        '
        Me.cbmasteruser.AutoSize = True
        Me.cbmasteruser.Location = New System.Drawing.Point(150, 201)
        Me.cbmasteruser.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasteruser.Name = "cbmasteruser"
        Me.cbmasteruser.Size = New System.Drawing.Size(109, 22)
        Me.cbmasteruser.TabIndex = 20
        Me.cbmasteruser.Text = "Master User"
        Me.cbmasteruser.UseVisualStyleBackColor = True
        '
        'clbmasterpricelist
        '
        Me.clbmasterpricelist.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterpricelist.CheckOnClick = True
        Me.clbmasterpricelist.FormattingEnabled = True
        Me.clbmasterpricelist.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterpricelist.Location = New System.Drawing.Point(312, 132)
        Me.clbmasterpricelist.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterpricelist.Name = "clbmasterpricelist"
        Me.clbmasterpricelist.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterpricelist.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterpricelist.TabIndex = 19
        '
        'cbmasterpricelist
        '
        Me.cbmasterpricelist.AutoSize = True
        Me.cbmasterpricelist.Location = New System.Drawing.Point(301, 102)
        Me.cbmasterpricelist.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterpricelist.Name = "cbmasterpricelist"
        Me.cbmasterpricelist.Size = New System.Drawing.Size(129, 22)
        Me.cbmasterpricelist.TabIndex = 18
        Me.cbmasterpricelist.Text = "Master Pricelist"
        Me.cbmasterpricelist.UseVisualStyleBackColor = True
        '
        'clbmasterkas
        '
        Me.clbmasterkas.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterkas.CheckOnClick = True
        Me.clbmasterkas.FormattingEnabled = True
        Me.clbmasterkas.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterkas.Location = New System.Drawing.Point(312, 33)
        Me.clbmasterkas.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterkas.Name = "clbmasterkas"
        Me.clbmasterkas.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterkas.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterkas.TabIndex = 17
        '
        'cbmasterkas
        '
        Me.cbmasterkas.AutoSize = True
        Me.cbmasterkas.Location = New System.Drawing.Point(301, 8)
        Me.cbmasterkas.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterkas.Name = "cbmasterkas"
        Me.cbmasterkas.Size = New System.Drawing.Size(103, 22)
        Me.cbmasterkas.TabIndex = 16
        Me.cbmasterkas.Text = "Master Kas"
        Me.cbmasterkas.UseVisualStyleBackColor = True
        '
        'clbmastersupplier
        '
        Me.clbmastersupplier.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmastersupplier.CheckOnClick = True
        Me.clbmastersupplier.FormattingEnabled = True
        Me.clbmastersupplier.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmastersupplier.Location = New System.Drawing.Point(161, 132)
        Me.clbmastersupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmastersupplier.Name = "clbmastersupplier"
        Me.clbmastersupplier.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmastersupplier.Size = New System.Drawing.Size(100, 61)
        Me.clbmastersupplier.TabIndex = 15
        '
        'cbmastersupplier
        '
        Me.cbmastersupplier.AutoSize = True
        Me.cbmastersupplier.Location = New System.Drawing.Point(150, 102)
        Me.cbmastersupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmastersupplier.Name = "cbmastersupplier"
        Me.cbmastersupplier.Size = New System.Drawing.Size(130, 22)
        Me.cbmastersupplier.TabIndex = 14
        Me.cbmastersupplier.Text = "Master Supplier"
        Me.cbmastersupplier.UseVisualStyleBackColor = True
        '
        'clbmasterpelanggan
        '
        Me.clbmasterpelanggan.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterpelanggan.CheckOnClick = True
        Me.clbmasterpelanggan.FormattingEnabled = True
        Me.clbmasterpelanggan.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterpelanggan.Location = New System.Drawing.Point(161, 33)
        Me.clbmasterpelanggan.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterpelanggan.Name = "clbmasterpelanggan"
        Me.clbmasterpelanggan.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterpelanggan.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterpelanggan.TabIndex = 13
        '
        'cbmasterpelanggan
        '
        Me.cbmasterpelanggan.AutoSize = True
        Me.cbmasterpelanggan.Location = New System.Drawing.Point(150, 8)
        Me.cbmasterpelanggan.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterpelanggan.Name = "cbmasterpelanggan"
        Me.cbmasterpelanggan.Size = New System.Drawing.Size(146, 22)
        Me.cbmasterpelanggan.TabIndex = 12
        Me.cbmasterpelanggan.Text = "Master Pelanggan"
        Me.cbmasterpelanggan.UseVisualStyleBackColor = True
        '
        'clbmastergudang
        '
        Me.clbmastergudang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmastergudang.CheckOnClick = True
        Me.clbmastergudang.FormattingEnabled = True
        Me.clbmastergudang.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmastergudang.Location = New System.Drawing.Point(19, 231)
        Me.clbmastergudang.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmastergudang.Name = "clbmastergudang"
        Me.clbmastergudang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmastergudang.Size = New System.Drawing.Size(100, 61)
        Me.clbmastergudang.TabIndex = 11
        '
        'cbmastergudang
        '
        Me.cbmastergudang.AutoSize = True
        Me.cbmastergudang.Location = New System.Drawing.Point(8, 201)
        Me.cbmastergudang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmastergudang.Name = "cbmastergudang"
        Me.cbmastergudang.Size = New System.Drawing.Size(129, 22)
        Me.cbmastergudang.TabIndex = 10
        Me.cbmastergudang.Text = "Master Gudang"
        Me.cbmastergudang.UseVisualStyleBackColor = True
        '
        'clbmasterbarang
        '
        Me.clbmasterbarang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbmasterbarang.CheckOnClick = True
        Me.clbmasterbarang.FormattingEnabled = True
        Me.clbmasterbarang.Items.AddRange(New Object() {"Tambah", "Edit", "Hapus"})
        Me.clbmasterbarang.Location = New System.Drawing.Point(19, 33)
        Me.clbmasterbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.clbmasterbarang.Name = "clbmasterbarang"
        Me.clbmasterbarang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbmasterbarang.Size = New System.Drawing.Size(100, 61)
        Me.clbmasterbarang.TabIndex = 9
        '
        'cbmasterbarang
        '
        Me.cbmasterbarang.AutoSize = True
        Me.cbmasterbarang.Location = New System.Drawing.Point(8, 8)
        Me.cbmasterbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterbarang.Name = "cbmasterbarang"
        Me.cbmasterbarang.Size = New System.Drawing.Size(124, 22)
        Me.cbmasterbarang.TabIndex = 8
        Me.cbmasterbarang.Text = "Master Barang"
        Me.cbmasterbarang.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(13, 387)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1010, 375)
        Me.TabControl1.TabIndex = 18
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage1.Controls.Add(Me.cbmasterbarang)
        Me.TabPage1.Controls.Add(Me.clbmasterbarang)
        Me.TabPage1.Controls.Add(Me.cbmasterkategori)
        Me.TabPage1.Controls.Add(Me.clbmasterkategori)
        Me.TabPage1.Controls.Add(Me.cbmastergudang)
        Me.TabPage1.Controls.Add(Me.clbmastergudang)
        Me.TabPage1.Controls.Add(Me.cbmasterpelanggan)
        Me.TabPage1.Controls.Add(Me.clbmasterpelanggan)
        Me.TabPage1.Controls.Add(Me.clbmasteruser)
        Me.TabPage1.Controls.Add(Me.cbmastersupplier)
        Me.TabPage1.Controls.Add(Me.clbmastersupplier)
        Me.TabPage1.Controls.Add(Me.cbmasteruser)
        Me.TabPage1.Controls.Add(Me.clbmasterpricelist)
        Me.TabPage1.Controls.Add(Me.cbmasterkas)
        Me.TabPage1.Controls.Add(Me.clbmasterkas)
        Me.TabPage1.Controls.Add(Me.cbmasterpricelist)
        Me.TabPage1.Controls.Add(Me.clbmasterrekplng)
        Me.TabPage1.Controls.Add(Me.cbmasterreksupp)
        Me.TabPage1.Controls.Add(Me.clbmasterreksupp)
        Me.TabPage1.Controls.Add(Me.cbmasterrekplng)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1002, 344)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Master"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Controls.Add(Me.cbpenyesuaianstok)
        Me.TabPage2.Controls.Add(Me.clbpenyesuaianstok)
        Me.TabPage2.Controls.Add(Me.clbpenjualan)
        Me.TabPage2.Controls.Add(Me.cbpembelian)
        Me.TabPage2.Controls.Add(Me.clbpembelian)
        Me.TabPage2.Controls.Add(Me.cbpenjualan)
        Me.TabPage2.Controls.Add(Me.clbreturjual)
        Me.TabPage2.Controls.Add(Me.cbreturbeli)
        Me.TabPage2.Controls.Add(Me.cbreturjual)
        Me.TabPage2.Controls.Add(Me.clbreturbeli)
        Me.TabPage2.Controls.Add(Me.clbbarangkeluar)
        Me.TabPage2.Controls.Add(Me.cbbarangmasuk)
        Me.TabPage2.Controls.Add(Me.cbbarangkeluar)
        Me.TabPage2.Controls.Add(Me.clbbarangmasuk)
        Me.TabPage2.Controls.Add(Me.cbtransferbarang)
        Me.TabPage2.Controls.Add(Me.clbtransferbarang)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Size = New System.Drawing.Size(1002, 344)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Transaksi"
        '
        'cbpenyesuaianstok
        '
        Me.cbpenyesuaianstok.AutoSize = True
        Me.cbpenyesuaianstok.Location = New System.Drawing.Point(442, 111)
        Me.cbpenyesuaianstok.Margin = New System.Windows.Forms.Padding(4)
        Me.cbpenyesuaianstok.Name = "cbpenyesuaianstok"
        Me.cbpenyesuaianstok.Size = New System.Drawing.Size(146, 22)
        Me.cbpenyesuaianstok.TabIndex = 80
        Me.cbpenyesuaianstok.Text = "Penyesuaian Stok"
        Me.cbpenyesuaianstok.UseVisualStyleBackColor = True
        '
        'clbpenyesuaianstok
        '
        Me.clbpenyesuaianstok.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbpenyesuaianstok.CheckOnClick = True
        Me.clbpenyesuaianstok.FormattingEnabled = True
        Me.clbpenyesuaianstok.Items.AddRange(New Object() {"Tambah", "Edit", "Print"})
        Me.clbpenyesuaianstok.Location = New System.Drawing.Point(459, 141)
        Me.clbpenyesuaianstok.Margin = New System.Windows.Forms.Padding(4)
        Me.clbpenyesuaianstok.Name = "clbpenyesuaianstok"
        Me.clbpenyesuaianstok.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbpenyesuaianstok.Size = New System.Drawing.Size(100, 61)
        Me.clbpenyesuaianstok.TabIndex = 81
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage3.Controls.Add(Me.clbtransferkas)
        Me.TabPage3.Controls.Add(Me.clbakunkeluar)
        Me.TabPage3.Controls.Add(Me.clblunaspiutang)
        Me.TabPage3.Controls.Add(Me.clbakunmasuk)
        Me.TabPage3.Controls.Add(Me.cbtransferkas)
        Me.TabPage3.Controls.Add(Me.cbakunmasuk)
        Me.TabPage3.Controls.Add(Me.cblunaspiutang)
        Me.TabPage3.Controls.Add(Me.cbakunkeluar)
        Me.TabPage3.Controls.Add(Me.clblunasutang)
        Me.TabPage3.Controls.Add(Me.cblunasutang)
        Me.TabPage3.Location = New System.Drawing.Point(4, 27)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1002, 344)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Administrasi"
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage4.Controls.Add(Me.clblaprekapanakhir)
        Me.TabPage4.Controls.Add(Me.cblaprekapanakhir)
        Me.TabPage4.Controls.Add(Me.clblaplabarugi)
        Me.TabPage4.Controls.Add(Me.cblaplabarugi)
        Me.TabPage4.Controls.Add(Me.clblappenjualanpajak)
        Me.TabPage4.Controls.Add(Me.cblappenjualanpajak)
        Me.TabPage4.Controls.Add(Me.clblapmutasibarang)
        Me.TabPage4.Controls.Add(Me.cblapmutasibarang)
        Me.TabPage4.Controls.Add(Me.clblappenyesuaianstok)
        Me.TabPage4.Controls.Add(Me.cblappenyesuaianstok)
        Me.TabPage4.Controls.Add(Me.clblappricelist)
        Me.TabPage4.Controls.Add(Me.cblappricelist)
        Me.TabPage4.Controls.Add(Me.clblapmodalbarang)
        Me.TabPage4.Controls.Add(Me.clblappenjualan)
        Me.TabPage4.Controls.Add(Me.cblapmodalbarang)
        Me.TabPage4.Controls.Add(Me.cblappenjualan)
        Me.TabPage4.Controls.Add(Me.clblapbarangkeluar)
        Me.TabPage4.Controls.Add(Me.clblapstokbarang)
        Me.TabPage4.Controls.Add(Me.cblapbarangkeluar)
        Me.TabPage4.Controls.Add(Me.cblapstokbarang)
        Me.TabPage4.Controls.Add(Me.clblaptransaksikas)
        Me.TabPage4.Controls.Add(Me.cblappembelian)
        Me.TabPage4.Controls.Add(Me.cblaptransaksikas)
        Me.TabPage4.Controls.Add(Me.clblapbarangmasuk)
        Me.TabPage4.Controls.Add(Me.clblaptransferbarang)
        Me.TabPage4.Controls.Add(Me.cblaptransferbarang)
        Me.TabPage4.Controls.Add(Me.clblappembelian)
        Me.TabPage4.Controls.Add(Me.clblaptransferkas)
        Me.TabPage4.Controls.Add(Me.cblapbarangmasuk)
        Me.TabPage4.Controls.Add(Me.cblaptransferkas)
        Me.TabPage4.Controls.Add(Me.clblapreturjual)
        Me.TabPage4.Controls.Add(Me.cblapreturbeli)
        Me.TabPage4.Controls.Add(Me.clblapreturbeli)
        Me.TabPage4.Controls.Add(Me.cblapreturjual)
        Me.TabPage4.Controls.Add(Me.clblapakunkeluar)
        Me.TabPage4.Controls.Add(Me.cblapakunmasuk)
        Me.TabPage4.Controls.Add(Me.clblapakunmasuk)
        Me.TabPage4.Controls.Add(Me.cblapakunkeluar)
        Me.TabPage4.Controls.Add(Me.cblaputang)
        Me.TabPage4.Controls.Add(Me.clblaputang)
        Me.TabPage4.Controls.Add(Me.cblappiutang)
        Me.TabPage4.Controls.Add(Me.clblappiutang)
        Me.TabPage4.Location = New System.Drawing.Point(4, 27)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1002, 344)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Laporan"
        '
        'clblaprekapanakhir
        '
        Me.clblaprekapanakhir.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaprekapanakhir.CheckOnClick = True
        Me.clblaprekapanakhir.FormattingEnabled = True
        Me.clblaprekapanakhir.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaprekapanakhir.Location = New System.Drawing.Point(846, 43)
        Me.clblaprekapanakhir.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaprekapanakhir.Name = "clblaprekapanakhir"
        Me.clblaprekapanakhir.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaprekapanakhir.Size = New System.Drawing.Size(134, 42)
        Me.clblaprekapanakhir.TabIndex = 125
        '
        'cblaprekapanakhir
        '
        Me.cblaprekapanakhir.AutoSize = True
        Me.cblaprekapanakhir.Location = New System.Drawing.Point(832, 13)
        Me.cblaprekapanakhir.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaprekapanakhir.Name = "cblaprekapanakhir"
        Me.cblaprekapanakhir.Size = New System.Drawing.Size(155, 22)
        Me.cblaprekapanakhir.TabIndex = 124
        Me.cblaprekapanakhir.Text = "Lap. Rekapan Akhir"
        Me.cblaprekapanakhir.UseVisualStyleBackColor = True
        '
        'clblaplabarugi
        '
        Me.clblaplabarugi.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaplabarugi.CheckOnClick = True
        Me.clblaplabarugi.FormattingEnabled = True
        Me.clblaplabarugi.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaplabarugi.Location = New System.Drawing.Point(678, 283)
        Me.clblaplabarugi.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaplabarugi.Name = "clblaplabarugi"
        Me.clblaplabarugi.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaplabarugi.Size = New System.Drawing.Size(134, 42)
        Me.clblaplabarugi.TabIndex = 123
        '
        'cblaplabarugi
        '
        Me.cblaplabarugi.AutoSize = True
        Me.cblaplabarugi.Location = New System.Drawing.Point(664, 253)
        Me.cblaplabarugi.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaplabarugi.Name = "cblaplabarugi"
        Me.cblaplabarugi.Size = New System.Drawing.Size(125, 22)
        Me.cblaplabarugi.TabIndex = 122
        Me.cblaplabarugi.Text = "Lap. Laba Rugi"
        Me.cblaplabarugi.UseVisualStyleBackColor = True
        '
        'clblappenjualanpajak
        '
        Me.clblappenjualanpajak.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblappenjualanpajak.CheckOnClick = True
        Me.clblappenjualanpajak.FormattingEnabled = True
        Me.clblappenjualanpajak.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblappenjualanpajak.Location = New System.Drawing.Point(18, 283)
        Me.clblappenjualanpajak.Margin = New System.Windows.Forms.Padding(4)
        Me.clblappenjualanpajak.Name = "clblappenjualanpajak"
        Me.clblappenjualanpajak.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblappenjualanpajak.Size = New System.Drawing.Size(134, 42)
        Me.clblappenjualanpajak.TabIndex = 121
        '
        'cblappenjualanpajak
        '
        Me.cblappenjualanpajak.AutoSize = True
        Me.cblappenjualanpajak.Location = New System.Drawing.Point(8, 253)
        Me.cblappenjualanpajak.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappenjualanpajak.Name = "cblappenjualanpajak"
        Me.cblappenjualanpajak.Size = New System.Drawing.Size(164, 22)
        Me.cblappenjualanpajak.TabIndex = 120
        Me.cblappenjualanpajak.Text = "Lap. Penjualan Pajak"
        Me.cblappenjualanpajak.UseVisualStyleBackColor = True
        '
        'clblapmutasibarang
        '
        Me.clblapmutasibarang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblapmutasibarang.CheckOnClick = True
        Me.clblapmutasibarang.FormattingEnabled = True
        Me.clblapmutasibarang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblapmutasibarang.Location = New System.Drawing.Point(678, 123)
        Me.clblapmutasibarang.Margin = New System.Windows.Forms.Padding(4)
        Me.clblapmutasibarang.Name = "clblapmutasibarang"
        Me.clblapmutasibarang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblapmutasibarang.Size = New System.Drawing.Size(134, 42)
        Me.clblapmutasibarang.TabIndex = 119
        '
        'cblapmutasibarang
        '
        Me.cblapmutasibarang.AutoSize = True
        Me.cblapmutasibarang.Location = New System.Drawing.Point(664, 93)
        Me.cblapmutasibarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapmutasibarang.Name = "cblapmutasibarang"
        Me.cblapmutasibarang.Size = New System.Drawing.Size(154, 22)
        Me.cblapmutasibarang.TabIndex = 118
        Me.cblapmutasibarang.Text = "Lap. Mutasi Barang"
        Me.cblapmutasibarang.UseVisualStyleBackColor = True
        '
        'clblappenyesuaianstok
        '
        Me.clblappenyesuaianstok.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblappenyesuaianstok.CheckOnClick = True
        Me.clblappenyesuaianstok.FormattingEnabled = True
        Me.clblappenyesuaianstok.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblappenyesuaianstok.Location = New System.Drawing.Point(678, 203)
        Me.clblappenyesuaianstok.Margin = New System.Windows.Forms.Padding(4)
        Me.clblappenyesuaianstok.Name = "clblappenyesuaianstok"
        Me.clblappenyesuaianstok.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblappenyesuaianstok.Size = New System.Drawing.Size(134, 42)
        Me.clblappenyesuaianstok.TabIndex = 117
        '
        'cblappenyesuaianstok
        '
        Me.cblappenyesuaianstok.AutoSize = True
        Me.cblappenyesuaianstok.Location = New System.Drawing.Point(664, 173)
        Me.cblappenyesuaianstok.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappenyesuaianstok.Name = "cblappenyesuaianstok"
        Me.cblappenyesuaianstok.Size = New System.Drawing.Size(178, 22)
        Me.cblappenyesuaianstok.TabIndex = 116
        Me.cblappenyesuaianstok.Text = "Lap. Penyesuaian Stok"
        Me.cblappenyesuaianstok.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage5.Controls.Add(Me.CheckedListBox5)
        Me.TabPage5.Controls.Add(Me.CheckBox5)
        Me.TabPage5.Controls.Add(Me.CheckedListBox6)
        Me.TabPage5.Controls.Add(Me.CheckBox6)
        Me.TabPage5.Controls.Add(Me.CheckBox3)
        Me.TabPage5.Controls.Add(Me.CheckedListBox3)
        Me.TabPage5.Controls.Add(Me.CheckBox4)
        Me.TabPage5.Controls.Add(Me.CheckedListBox4)
        Me.TabPage5.Controls.Add(Me.CheckedListBox1)
        Me.TabPage5.Controls.Add(Me.CheckBox1)
        Me.TabPage5.Controls.Add(Me.CheckBox2)
        Me.TabPage5.Controls.Add(Me.CheckedListBox2)
        Me.TabPage5.Location = New System.Drawing.Point(4, 27)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1002, 344)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Chart"
        '
        'CheckedListBox5
        '
        Me.CheckedListBox5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.CheckedListBox5.CheckOnClick = True
        Me.CheckedListBox5.FormattingEnabled = True
        Me.CheckedListBox5.Items.AddRange(New Object() {"Print", "Export"})
        Me.CheckedListBox5.Location = New System.Drawing.Point(187, 129)
        Me.CheckedListBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox5.Name = "CheckedListBox5"
        Me.CheckedListBox5.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.CheckedListBox5.Size = New System.Drawing.Size(134, 42)
        Me.CheckedListBox5.TabIndex = 99
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(177, 19)
        Me.CheckBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(149, 22)
        Me.CheckBox5.TabIndex = 96
        Me.CheckBox5.Text = "Chart Akun Masuk"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckedListBox6
        '
        Me.CheckedListBox6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.CheckedListBox6.CheckOnClick = True
        Me.CheckedListBox6.FormattingEnabled = True
        Me.CheckedListBox6.Items.AddRange(New Object() {"Print", "Export"})
        Me.CheckedListBox6.Location = New System.Drawing.Point(187, 49)
        Me.CheckedListBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox6.Name = "CheckedListBox6"
        Me.CheckedListBox6.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.CheckedListBox6.Size = New System.Drawing.Size(134, 42)
        Me.CheckedListBox6.TabIndex = 97
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(177, 99)
        Me.CheckBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(146, 22)
        Me.CheckBox6.TabIndex = 98
        Me.CheckBox6.Text = "Chart Akun Keluar"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(18, 180)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(150, 22)
        Me.CheckBox3.TabIndex = 60
        Me.CheckBox3.Text = "Chart Lunas Utang"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckedListBox3
        '
        Me.CheckedListBox3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.CheckedListBox3.CheckOnClick = True
        Me.CheckedListBox3.FormattingEnabled = True
        Me.CheckedListBox3.Items.AddRange(New Object() {"Print", "Export"})
        Me.CheckedListBox3.Location = New System.Drawing.Point(28, 210)
        Me.CheckedListBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox3.Name = "CheckedListBox3"
        Me.CheckedListBox3.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.CheckedListBox3.Size = New System.Drawing.Size(134, 42)
        Me.CheckedListBox3.TabIndex = 61
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(18, 260)
        Me.CheckBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(160, 22)
        Me.CheckBox4.TabIndex = 62
        Me.CheckBox4.Text = "Chart Lunas Piutang"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckedListBox4
        '
        Me.CheckedListBox4.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.CheckedListBox4.CheckOnClick = True
        Me.CheckedListBox4.FormattingEnabled = True
        Me.CheckedListBox4.Items.AddRange(New Object() {"Print", "Export"})
        Me.CheckedListBox4.Location = New System.Drawing.Point(28, 290)
        Me.CheckedListBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox4.Name = "CheckedListBox4"
        Me.CheckedListBox4.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.CheckedListBox4.Size = New System.Drawing.Size(134, 42)
        Me.CheckedListBox4.TabIndex = 63
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"Print", "Export"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(28, 129)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.CheckedListBox1.Size = New System.Drawing.Size(134, 42)
        Me.CheckedListBox1.TabIndex = 57
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(18, 99)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(131, 22)
        Me.CheckBox1.TabIndex = 56
        Me.CheckBox1.Text = "Chart Penjualan"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(18, 19)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(136, 22)
        Me.CheckBox2.TabIndex = 58
        Me.CheckBox2.Text = "Chart Pembelian"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.CheckedListBox2.CheckOnClick = True
        Me.CheckedListBox2.FormattingEnabled = True
        Me.CheckedListBox2.Items.AddRange(New Object() {"Print", "Export"})
        Me.CheckedListBox2.Location = New System.Drawing.Point(28, 49)
        Me.CheckedListBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.CheckedListBox2.Size = New System.Drawing.Size(134, 42)
        Me.CheckedListBox2.TabIndex = 59
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 9)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 29)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Data User"
        '
        'cbauth
        '
        Me.cbauth.AutoSize = True
        Me.cbauth.Location = New System.Drawing.Point(363, 128)
        Me.cbauth.Margin = New System.Windows.Forms.Padding(4)
        Me.cbauth.Name = "cbauth"
        Me.cbauth.Size = New System.Drawing.Size(56, 22)
        Me.cbauth.TabIndex = 104
        Me.cbauth.Text = "Auth"
        Me.cbauth.UseVisualStyleBackColor = True
        '
        'txtmaxprint
        '
        Me.txtmaxprint.Location = New System.Drawing.Point(365, 159)
        Me.txtmaxprint.Margin = New System.Windows.Forms.Padding(4)
        Me.txtmaxprint.MaxLength = 3
        Me.txtmaxprint.Name = "txtmaxprint"
        Me.txtmaxprint.Size = New System.Drawing.Size(51, 24)
        Me.txtmaxprint.TabIndex = 105
        Me.txtmaxprint.Text = "1"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(284, 161)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 20)
        Me.Label10.TabIndex = 106
        Me.Label10.Text = "Max Print"
        '
        'btnpassword
        '
        Me.btnpassword.Location = New System.Drawing.Point(814, 9)
        Me.btnpassword.Margin = New System.Windows.Forms.Padding(4)
        Me.btnpassword.Name = "btnpassword"
        Me.btnpassword.Size = New System.Drawing.Size(149, 37)
        Me.btnpassword.TabIndex = 107
        Me.btnpassword.Text = "Generate Pass"
        Me.btnpassword.UseVisualStyleBackColor = True
        '
        'btnrefresh
        '
        Me.btnrefresh.BackgroundImage = CType(resources.GetObject("btnrefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnrefresh.ImageIndex = 0
        Me.btnrefresh.Location = New System.Drawing.Point(982, 9)
        Me.btnrefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(41, 37)
        Me.btnrefresh.TabIndex = 108
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage6.Location = New System.Drawing.Point(4, 27)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(1002, 344)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Setting"
        '
        'fuser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1039, 775)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.btnpassword)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtmaxprint)
        Me.Controls.Add(Me.cbauth)
        Me.Controls.Add(Me.txtketerangan)
        Me.Controls.Add(Me.cmbjabatan)
        Me.Controls.Add(Me.txtalamat)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txttelp)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtemail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtpassword)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GridControl)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnhapus)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.txtnama)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtkode)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fuser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data User"
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents txtalamat As RichTextBox
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnhapus As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btntambah As Button
    Friend WithEvents txttelp As TextBox
    Friend WithEvents txtnama As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtkode As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtemail As TextBox
    Friend WithEvents txtpassword As TextBox
    Friend WithEvents cmbjabatan As ComboBox
    Friend WithEvents clbmasterbarang As CheckedListBox
    Friend WithEvents cbmasterbarang As CheckBox
    Friend WithEvents clbmastersupplier As CheckedListBox
    Friend WithEvents cbmastersupplier As CheckBox
    Friend WithEvents clbmasterpelanggan As CheckedListBox
    Friend WithEvents cbmasterpelanggan As CheckBox
    Friend WithEvents clbmastergudang As CheckedListBox
    Friend WithEvents cbmastergudang As CheckBox
    Friend WithEvents clbmasteruser As CheckedListBox
    Friend WithEvents cbmasteruser As CheckBox
    Friend WithEvents clbmasterpricelist As CheckedListBox
    Friend WithEvents cbmasterpricelist As CheckBox
    Friend WithEvents clbmasterkas As CheckedListBox
    Friend WithEvents cbmasterkas As CheckBox
    Friend WithEvents clbreturjual As CheckedListBox
    Friend WithEvents clbreturbeli As CheckedListBox
    Friend WithEvents clbpenjualan As CheckedListBox
    Friend WithEvents cbreturjual As CheckBox
    Friend WithEvents cbreturbeli As CheckBox
    Friend WithEvents cbpenjualan As CheckBox
    Friend WithEvents clbpembelian As CheckedListBox
    Friend WithEvents cbpembelian As CheckBox
    Friend WithEvents clblappenjualan As CheckedListBox
    Friend WithEvents cblappenjualan As CheckBox
    Friend WithEvents clblapstokbarang As CheckedListBox
    Friend WithEvents cblapstokbarang As CheckBox
    Friend WithEvents clblappiutang As CheckedListBox
    Friend WithEvents cblappiutang As CheckBox
    Friend WithEvents clblaputang As CheckedListBox
    Friend WithEvents cblaputang As CheckBox
    Friend WithEvents clblappembelian As CheckedListBox
    Friend WithEvents cblappembelian As CheckBox
    Friend WithEvents clbmasterrekplng As CheckedListBox
    Friend WithEvents cbmasterrekplng As CheckBox
    Friend WithEvents clbmasterreksupp As CheckedListBox
    Friend WithEvents cbmasterreksupp As CheckBox
    Friend WithEvents clbtransferbarang As CheckedListBox
    Friend WithEvents clbbarangkeluar As CheckedListBox
    Friend WithEvents clbbarangmasuk As CheckedListBox
    Friend WithEvents cbtransferbarang As CheckBox
    Friend WithEvents cbbarangkeluar As CheckBox
    Friend WithEvents cbbarangmasuk As CheckBox
    Friend WithEvents clbakunkeluar As CheckedListBox
    Friend WithEvents clbakunmasuk As CheckedListBox
    Friend WithEvents cbakunkeluar As CheckBox
    Friend WithEvents cbakunmasuk As CheckBox
    Friend WithEvents clbtransferkas As CheckedListBox
    Friend WithEvents clblunaspiutang As CheckedListBox
    Friend WithEvents cbtransferkas As CheckBox
    Friend WithEvents cblunaspiutang As CheckBox
    Friend WithEvents clblunasutang As CheckedListBox
    Friend WithEvents cblunasutang As CheckBox
    Friend WithEvents clblaptransferbarang As CheckedListBox
    Friend WithEvents cblaptransferbarang As CheckBox
    Friend WithEvents clblaptransferkas As CheckedListBox
    Friend WithEvents cblaptransferkas As CheckBox
    Friend WithEvents clblapakunkeluar As CheckedListBox
    Friend WithEvents cblapakunkeluar As CheckBox
    Friend WithEvents clblapakunmasuk As CheckedListBox
    Friend WithEvents cblapakunmasuk As CheckBox
    Friend WithEvents clblaptransaksikas As CheckedListBox
    Friend WithEvents cblaptransaksikas As CheckBox
    Friend WithEvents clbmasterkategori As CheckedListBox
    Friend WithEvents cbmasterkategori As CheckBox
    Friend WithEvents clblapbarangkeluar As CheckedListBox
    Friend WithEvents cblapbarangkeluar As CheckBox
    Friend WithEvents clblapbarangmasuk As CheckedListBox
    Friend WithEvents cblapbarangmasuk As CheckBox
    Friend WithEvents clblapreturjual As CheckedListBox
    Friend WithEvents cblapreturjual As CheckBox
    Friend WithEvents clblappricelist As CheckedListBox
    Friend WithEvents cblappricelist As CheckBox
    Friend WithEvents clblapreturbeli As CheckedListBox
    Friend WithEvents cblapreturbeli As CheckBox
    Friend WithEvents clblapmodalbarang As CheckedListBox
    Friend WithEvents cblapmodalbarang As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label9 As Label
    Friend WithEvents cbauth As CheckBox
    Friend WithEvents txtmaxprint As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btnpassword As Button
    Friend WithEvents cbpenyesuaianstok As CheckBox
    Friend WithEvents clbpenyesuaianstok As CheckedListBox
    Friend WithEvents clblappenyesuaianstok As CheckedListBox
    Friend WithEvents cblappenyesuaianstok As CheckBox
    Friend WithEvents clblapmutasibarang As CheckedListBox
    Friend WithEvents cblapmutasibarang As CheckBox
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents clblappenjualanpajak As CheckedListBox
    Friend WithEvents cblappenjualanpajak As CheckBox
    Friend WithEvents clblaplabarugi As CheckedListBox
    Friend WithEvents cblaplabarugi As CheckBox
    Friend WithEvents clblaprekapanakhir As CheckedListBox
    Friend WithEvents cblaprekapanakhir As CheckBox
    Friend WithEvents btnrefresh As Button
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckedListBox2 As CheckedListBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckedListBox3 As CheckedListBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckedListBox4 As CheckedListBox
    Friend WithEvents CheckedListBox5 As CheckedListBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckedListBox6 As CheckedListBox
    Friend WithEvents CheckBox6 As CheckBox
    Friend WithEvents TabPage6 As TabPage
End Class
