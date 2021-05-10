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
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.MasterTab = New System.Windows.Forms.TabPage()
        Me.TransaksiTab = New System.Windows.Forms.TabPage()
        Me.cbpenyesuaianstok = New System.Windows.Forms.CheckBox()
        Me.clbpenyesuaianstok = New System.Windows.Forms.CheckedListBox()
        Me.AdministrasiTab = New System.Windows.Forms.TabPage()
        Me.LaporanTab = New System.Windows.Forms.TabPage()
        Me.clblaprekapanharian = New System.Windows.Forms.CheckedListBox()
        Me.cblaprekapanharian = New System.Windows.Forms.CheckBox()
        Me.clblaplabarugi = New System.Windows.Forms.CheckedListBox()
        Me.cblaplabarugi = New System.Windows.Forms.CheckBox()
        Me.clblappenjualanpajak = New System.Windows.Forms.CheckedListBox()
        Me.cblappenjualanpajak = New System.Windows.Forms.CheckBox()
        Me.clblapmutasibarang = New System.Windows.Forms.CheckedListBox()
        Me.cblapmutasibarang = New System.Windows.Forms.CheckBox()
        Me.clblappenyesuaianstok = New System.Windows.Forms.CheckedListBox()
        Me.cblappenyesuaianstok = New System.Windows.Forms.CheckBox()
        Me.ChartTab = New System.Windows.Forms.TabPage()
        Me.clbchartakunkeluar = New System.Windows.Forms.CheckedListBox()
        Me.cbchartakunmasuk = New System.Windows.Forms.CheckBox()
        Me.clbchartakunmasuk = New System.Windows.Forms.CheckedListBox()
        Me.cbchartakunkeluar = New System.Windows.Forms.CheckBox()
        Me.cbchartlunasutang = New System.Windows.Forms.CheckBox()
        Me.clbchartlunasutang = New System.Windows.Forms.CheckedListBox()
        Me.cbchartlunaspiutang = New System.Windows.Forms.CheckBox()
        Me.clbchartlunaspiutang = New System.Windows.Forms.CheckedListBox()
        Me.clbchartpenjualan = New System.Windows.Forms.CheckedListBox()
        Me.cbchartpenjualan = New System.Windows.Forms.CheckBox()
        Me.cbchartpembelian = New System.Windows.Forms.CheckBox()
        Me.clbchartpembelian = New System.Windows.Forms.CheckedListBox()
        Me.FeatureTab = New System.Windows.Forms.TabPage()
        Me.cbbarcodegenerator = New System.Windows.Forms.CheckBox()
        Me.cbkalkulasiexpedisi = New System.Windows.Forms.CheckBox()
        Me.SettingTab = New System.Windows.Forms.TabPage()
        Me.cbbackupdatabase = New System.Windows.Forms.CheckBox()
        Me.cbpengaturan = New System.Windows.Forms.CheckBox()
        Me.cbprinter = New System.Windows.Forms.CheckBox()
        Me.cbinfoperusahaan = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbauth = New System.Windows.Forms.CheckBox()
        Me.txtmaxprint = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnpassword = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.MasterTab.SuspendLayout()
        Me.TransaksiTab.SuspendLayout()
        Me.AdministrasiTab.SuspendLayout()
        Me.LaporanTab.SuspendLayout()
        Me.ChartTab.SuspendLayout()
        Me.FeatureTab.SuspendLayout()
        Me.SettingTab.SuspendLayout()
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
        Me.cmbjabatan.Size = New System.Drawing.Size(168, 32)
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
        Me.txtemail.Size = New System.Drawing.Size(308, 29)
        Me.txtemail.TabIndex = 23
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(108, 126)
        Me.txtpassword.Margin = New System.Windows.Forms.Padding(4)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtpassword.Size = New System.Drawing.Size(244, 29)
        Me.txtpassword.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 128)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 25)
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
        Me.Label7.Size = New System.Drawing.Size(83, 25)
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
        Me.Label6.Size = New System.Drawing.Size(60, 25)
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
        Me.txttelp.Size = New System.Drawing.Size(308, 26)
        Me.txttelp.TabIndex = 2
        '
        'txtnama
        '
        Me.txtnama.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnama.Location = New System.Drawing.Point(108, 96)
        Me.txtnama.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnama.Name = "txtnama"
        Me.txtnama.Size = New System.Drawing.Size(308, 26)
        Me.txtnama.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 320)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 25)
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
        Me.txtkode.Size = New System.Drawing.Size(308, 26)
        Me.txtkode.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 255)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 25)
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
        Me.Label3.Size = New System.Drawing.Size(84, 25)
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
        Me.Label2.Size = New System.Drawing.Size(110, 25)
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
        Me.Label1.Size = New System.Drawing.Size(105, 25)
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
        Me.clblapmodalbarang.Size = New System.Drawing.Size(134, 28)
        Me.clblapmodalbarang.TabIndex = 115
        '
        'cblapmodalbarang
        '
        Me.cblapmodalbarang.AutoSize = True
        Me.cblapmodalbarang.Location = New System.Drawing.Point(664, 13)
        Me.cblapmodalbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapmodalbarang.Name = "cblapmodalbarang"
        Me.cblapmodalbarang.Size = New System.Drawing.Size(190, 28)
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
        Me.clblapbarangkeluar.Size = New System.Drawing.Size(134, 28)
        Me.clblapbarangkeluar.TabIndex = 113
        '
        'cblapbarangkeluar
        '
        Me.cblapbarangkeluar.AutoSize = True
        Me.cblapbarangkeluar.Location = New System.Drawing.Point(176, 253)
        Me.cblapbarangkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapbarangkeluar.Name = "cblapbarangkeluar"
        Me.cblapbarangkeluar.Size = New System.Drawing.Size(192, 28)
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
        Me.clblapbarangmasuk.Size = New System.Drawing.Size(134, 28)
        Me.clblapbarangmasuk.TabIndex = 111
        '
        'cblapbarangmasuk
        '
        Me.cblapbarangmasuk.AutoSize = True
        Me.cblapbarangmasuk.Location = New System.Drawing.Point(176, 173)
        Me.cblapbarangmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapbarangmasuk.Name = "cblapbarangmasuk"
        Me.cblapbarangmasuk.Size = New System.Drawing.Size(193, 28)
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
        Me.clblapreturjual.Size = New System.Drawing.Size(134, 28)
        Me.clblapreturjual.TabIndex = 109
        '
        'cblapreturjual
        '
        Me.cblapreturjual.AutoSize = True
        Me.cblapreturjual.Location = New System.Drawing.Point(176, 93)
        Me.cblapreturjual.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapreturjual.Name = "cblapreturjual"
        Me.cblapreturjual.Size = New System.Drawing.Size(157, 28)
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
        Me.clblappricelist.Size = New System.Drawing.Size(134, 28)
        Me.clblappricelist.TabIndex = 107
        '
        'cblappricelist
        '
        Me.cblappricelist.AutoSize = True
        Me.cblappricelist.Location = New System.Drawing.Point(8, 13)
        Me.cblappricelist.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappricelist.Name = "cblappricelist"
        Me.cblappricelist.Size = New System.Drawing.Size(137, 28)
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
        Me.clblapreturbeli.Size = New System.Drawing.Size(134, 28)
        Me.clblapreturbeli.TabIndex = 105
        '
        'cblapreturbeli
        '
        Me.cblapreturbeli.AutoSize = True
        Me.cblapreturbeli.Location = New System.Drawing.Point(176, 13)
        Me.cblapreturbeli.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapreturbeli.Name = "cblapreturbeli"
        Me.cblapreturbeli.Size = New System.Drawing.Size(154, 28)
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
        Me.clbmasterkategori.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterkategori.TabIndex = 103
        '
        'cbmasterkategori
        '
        Me.cbmasterkategori.AutoSize = True
        Me.cbmasterkategori.Location = New System.Drawing.Point(8, 102)
        Me.cbmasterkategori.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterkategori.Name = "cbmasterkategori"
        Me.cbmasterkategori.Size = New System.Drawing.Size(162, 28)
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
        Me.clblaptransaksikas.Size = New System.Drawing.Size(134, 28)
        Me.clblaptransaksikas.TabIndex = 101
        '
        'cblaptransaksikas
        '
        Me.cblaptransaksikas.AutoSize = True
        Me.cblaptransaksikas.Location = New System.Drawing.Point(505, 253)
        Me.cblaptransaksikas.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaptransaksikas.Name = "cblaptransaksikas"
        Me.cblaptransaksikas.Size = New System.Drawing.Size(189, 28)
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
        Me.clblaptransferbarang.Size = New System.Drawing.Size(134, 28)
        Me.clblaptransferbarang.TabIndex = 99
        '
        'cblaptransferbarang
        '
        Me.cblaptransferbarang.AutoSize = True
        Me.cblaptransferbarang.Location = New System.Drawing.Point(332, 13)
        Me.cblaptransferbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaptransferbarang.Name = "cblaptransferbarang"
        Me.cblaptransferbarang.Size = New System.Drawing.Size(207, 28)
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
        Me.clblaptransferkas.Size = New System.Drawing.Size(134, 28)
        Me.clblaptransferkas.TabIndex = 97
        '
        'cblaptransferkas
        '
        Me.cblaptransferkas.AutoSize = True
        Me.cblaptransferkas.Location = New System.Drawing.Point(505, 173)
        Me.cblaptransferkas.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaptransferkas.Name = "cblaptransferkas"
        Me.cblaptransferkas.Size = New System.Drawing.Size(178, 28)
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
        Me.clblapakunkeluar.Size = New System.Drawing.Size(134, 28)
        Me.clblapakunkeluar.TabIndex = 95
        '
        'cblapakunkeluar
        '
        Me.cblapakunkeluar.AutoSize = True
        Me.cblapakunkeluar.Location = New System.Drawing.Point(505, 93)
        Me.cblapakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapakunkeluar.Name = "cblapakunkeluar"
        Me.cblapakunkeluar.Size = New System.Drawing.Size(176, 28)
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
        Me.clblapakunmasuk.Size = New System.Drawing.Size(134, 28)
        Me.clblapakunmasuk.TabIndex = 93
        '
        'cblapakunmasuk
        '
        Me.cblapakunmasuk.AutoSize = True
        Me.cblapakunmasuk.Location = New System.Drawing.Point(505, 13)
        Me.cblapakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapakunmasuk.Name = "cblapakunmasuk"
        Me.cblapakunmasuk.Size = New System.Drawing.Size(177, 28)
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
        Me.clbakunkeluar.Size = New System.Drawing.Size(100, 52)
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
        Me.clbakunmasuk.Size = New System.Drawing.Size(100, 52)
        Me.clbakunmasuk.TabIndex = 89
        '
        'cbakunkeluar
        '
        Me.cbakunkeluar.AutoSize = True
        Me.cbakunkeluar.Location = New System.Drawing.Point(153, 111)
        Me.cbakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cbakunkeluar.Name = "cbakunkeluar"
        Me.cbakunkeluar.Size = New System.Drawing.Size(135, 28)
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
        Me.cbakunmasuk.Size = New System.Drawing.Size(136, 28)
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
        Me.clbtransferkas.Size = New System.Drawing.Size(100, 52)
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
        Me.clblunaspiutang.Size = New System.Drawing.Size(100, 52)
        Me.clblunaspiutang.TabIndex = 84
        '
        'cbtransferkas
        '
        Me.cbtransferkas.AutoSize = True
        Me.cbtransferkas.Location = New System.Drawing.Point(309, 12)
        Me.cbtransferkas.Margin = New System.Windows.Forms.Padding(4)
        Me.cbtransferkas.Name = "cbtransferkas"
        Me.cbtransferkas.Size = New System.Drawing.Size(137, 28)
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
        Me.cblunaspiutang.Size = New System.Drawing.Size(151, 28)
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
        Me.clblunasutang.Size = New System.Drawing.Size(100, 52)
        Me.clblunasutang.TabIndex = 81
        '
        'cblunasutang
        '
        Me.cblunasutang.AutoSize = True
        Me.cblunasutang.Location = New System.Drawing.Point(9, 12)
        Me.cblunasutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblunasutang.Name = "cblunasutang"
        Me.cblunasutang.Size = New System.Drawing.Size(137, 28)
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
        Me.clbtransferbarang.Size = New System.Drawing.Size(100, 52)
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
        Me.clbbarangkeluar.Size = New System.Drawing.Size(100, 52)
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
        Me.clbbarangmasuk.Size = New System.Drawing.Size(100, 52)
        Me.clbbarangmasuk.TabIndex = 77
        '
        'cbtransferbarang
        '
        Me.cbtransferbarang.AutoSize = True
        Me.cbtransferbarang.Location = New System.Drawing.Point(442, 12)
        Me.cbtransferbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbtransferbarang.Name = "cbtransferbarang"
        Me.cbtransferbarang.Size = New System.Drawing.Size(166, 28)
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
        Me.cbbarangkeluar.Size = New System.Drawing.Size(151, 28)
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
        Me.cbbarangmasuk.Size = New System.Drawing.Size(152, 28)
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
        Me.clbmasterrekplng.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterrekplng.TabIndex = 73
        '
        'cbmasterrekplng
        '
        Me.cbmasterrekplng.AutoSize = True
        Me.cbmasterrekplng.Location = New System.Drawing.Point(439, 102)
        Me.cbmasterrekplng.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterrekplng.Name = "cbmasterrekplng"
        Me.cbmasterrekplng.Size = New System.Drawing.Size(222, 28)
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
        Me.clbmasterreksupp.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterreksupp.TabIndex = 71
        '
        'cbmasterreksupp
        '
        Me.cbmasterreksupp.AutoSize = True
        Me.cbmasterreksupp.Location = New System.Drawing.Point(439, 8)
        Me.cbmasterreksupp.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterreksupp.Name = "cbmasterreksupp"
        Me.cbmasterreksupp.Size = New System.Drawing.Size(201, 28)
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
        Me.clblapstokbarang.Size = New System.Drawing.Size(134, 28)
        Me.clblapstokbarang.TabIndex = 69
        '
        'cblapstokbarang
        '
        Me.cblapstokbarang.AutoSize = True
        Me.cblapstokbarang.Location = New System.Drawing.Point(332, 93)
        Me.cblapstokbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapstokbarang.Name = "cblapstokbarang"
        Me.cblapstokbarang.Size = New System.Drawing.Size(174, 28)
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
        Me.clblappiutang.Size = New System.Drawing.Size(134, 28)
        Me.clblappiutang.TabIndex = 59
        '
        'cblappiutang
        '
        Me.cblappiutang.AutoSize = True
        Me.cblappiutang.Location = New System.Drawing.Point(332, 253)
        Me.cblappiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappiutang.Name = "cblappiutang"
        Me.cblappiutang.Size = New System.Drawing.Size(192, 28)
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
        Me.clblaputang.Size = New System.Drawing.Size(134, 28)
        Me.clblaputang.TabIndex = 57
        '
        'cblaputang
        '
        Me.cblaputang.AutoSize = True
        Me.cblaputang.Location = New System.Drawing.Point(332, 173)
        Me.cblaputang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaputang.Name = "cblaputang"
        Me.cblaputang.Size = New System.Drawing.Size(178, 28)
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
        Me.clblappembelian.Size = New System.Drawing.Size(134, 28)
        Me.clblappembelian.TabIndex = 55
        '
        'cblappembelian
        '
        Me.cblappembelian.AutoSize = True
        Me.cblappembelian.Location = New System.Drawing.Point(8, 93)
        Me.cblappembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappembelian.Name = "cblappembelian"
        Me.cblappembelian.Size = New System.Drawing.Size(163, 28)
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
        Me.clblappenjualan.Size = New System.Drawing.Size(134, 28)
        Me.clblappenjualan.TabIndex = 53
        '
        'cblappenjualan
        '
        Me.cblappenjualan.AutoSize = True
        Me.cblappenjualan.Location = New System.Drawing.Point(8, 173)
        Me.cblappenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappenjualan.Name = "cblappenjualan"
        Me.cblappenjualan.Size = New System.Drawing.Size(157, 28)
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
        Me.clbreturjual.Size = New System.Drawing.Size(100, 52)
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
        Me.clbreturbeli.Size = New System.Drawing.Size(100, 52)
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
        Me.clbpenjualan.Size = New System.Drawing.Size(100, 52)
        Me.clbpenjualan.TabIndex = 36
        '
        'cbreturjual
        '
        Me.cbreturjual.AutoSize = True
        Me.cbreturjual.Location = New System.Drawing.Point(155, 111)
        Me.cbreturjual.Margin = New System.Windows.Forms.Padding(4)
        Me.cbreturjual.Name = "cbreturjual"
        Me.cbreturjual.Size = New System.Drawing.Size(116, 28)
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
        Me.cbreturbeli.Size = New System.Drawing.Size(113, 28)
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
        Me.cbpenjualan.Size = New System.Drawing.Size(116, 28)
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
        Me.clbpembelian.Size = New System.Drawing.Size(100, 52)
        Me.clbpembelian.TabIndex = 23
        '
        'cbpembelian
        '
        Me.cbpembelian.AutoSize = True
        Me.cbpembelian.Location = New System.Drawing.Point(8, 12)
        Me.cbpembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.cbpembelian.Name = "cbpembelian"
        Me.cbpembelian.Size = New System.Drawing.Size(122, 28)
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
        Me.clbmasteruser.Size = New System.Drawing.Size(100, 52)
        Me.clbmasteruser.TabIndex = 21
        '
        'cbmasteruser
        '
        Me.cbmasteruser.AutoSize = True
        Me.cbmasteruser.Location = New System.Drawing.Point(150, 201)
        Me.cbmasteruser.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasteruser.Name = "cbmasteruser"
        Me.cbmasteruser.Size = New System.Drawing.Size(132, 28)
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
        Me.clbmasterpricelist.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterpricelist.TabIndex = 19
        '
        'cbmasterpricelist
        '
        Me.cbmasterpricelist.AutoSize = True
        Me.cbmasterpricelist.Location = New System.Drawing.Point(301, 102)
        Me.cbmasterpricelist.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterpricelist.Name = "cbmasterpricelist"
        Me.cbmasterpricelist.Size = New System.Drawing.Size(157, 28)
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
        Me.clbmasterkas.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterkas.TabIndex = 17
        '
        'cbmasterkas
        '
        Me.cbmasterkas.AutoSize = True
        Me.cbmasterkas.Location = New System.Drawing.Point(301, 8)
        Me.cbmasterkas.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterkas.Name = "cbmasterkas"
        Me.cbmasterkas.Size = New System.Drawing.Size(124, 28)
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
        Me.clbmastersupplier.Size = New System.Drawing.Size(100, 52)
        Me.clbmastersupplier.TabIndex = 15
        '
        'cbmastersupplier
        '
        Me.cbmastersupplier.AutoSize = True
        Me.cbmastersupplier.Location = New System.Drawing.Point(150, 102)
        Me.cbmastersupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmastersupplier.Name = "cbmastersupplier"
        Me.cbmastersupplier.Size = New System.Drawing.Size(163, 28)
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
        Me.clbmasterpelanggan.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterpelanggan.TabIndex = 13
        '
        'cbmasterpelanggan
        '
        Me.cbmasterpelanggan.AutoSize = True
        Me.cbmasterpelanggan.Location = New System.Drawing.Point(150, 8)
        Me.cbmasterpelanggan.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterpelanggan.Name = "cbmasterpelanggan"
        Me.cbmasterpelanggan.Size = New System.Drawing.Size(184, 28)
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
        Me.clbmastergudang.Size = New System.Drawing.Size(100, 52)
        Me.clbmastergudang.TabIndex = 11
        '
        'cbmastergudang
        '
        Me.cbmastergudang.AutoSize = True
        Me.cbmastergudang.Location = New System.Drawing.Point(8, 201)
        Me.cbmastergudang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmastergudang.Name = "cbmastergudang"
        Me.cbmastergudang.Size = New System.Drawing.Size(161, 28)
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
        Me.clbmasterbarang.Size = New System.Drawing.Size(100, 52)
        Me.clbmasterbarang.TabIndex = 9
        '
        'cbmasterbarang
        '
        Me.cbmasterbarang.AutoSize = True
        Me.cbmasterbarang.Location = New System.Drawing.Point(8, 8)
        Me.cbmasterbarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmasterbarang.Name = "cbmasterbarang"
        Me.cbmasterbarang.Size = New System.Drawing.Size(153, 28)
        Me.cbmasterbarang.TabIndex = 8
        Me.cbmasterbarang.Text = "Master Barang"
        Me.cbmasterbarang.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.MasterTab)
        Me.TabControl.Controls.Add(Me.TransaksiTab)
        Me.TabControl.Controls.Add(Me.AdministrasiTab)
        Me.TabControl.Controls.Add(Me.LaporanTab)
        Me.TabControl.Controls.Add(Me.ChartTab)
        Me.TabControl.Controls.Add(Me.FeatureTab)
        Me.TabControl.Controls.Add(Me.SettingTab)
        Me.TabControl.Location = New System.Drawing.Point(14, 387)
        Me.TabControl.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(1010, 375)
        Me.TabControl.TabIndex = 18
        '
        'MasterTab
        '
        Me.MasterTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.MasterTab.Controls.Add(Me.cbmasterbarang)
        Me.MasterTab.Controls.Add(Me.clbmasterbarang)
        Me.MasterTab.Controls.Add(Me.cbmasterkategori)
        Me.MasterTab.Controls.Add(Me.clbmasterkategori)
        Me.MasterTab.Controls.Add(Me.cbmastergudang)
        Me.MasterTab.Controls.Add(Me.clbmastergudang)
        Me.MasterTab.Controls.Add(Me.cbmasterpelanggan)
        Me.MasterTab.Controls.Add(Me.clbmasterpelanggan)
        Me.MasterTab.Controls.Add(Me.clbmasteruser)
        Me.MasterTab.Controls.Add(Me.cbmastersupplier)
        Me.MasterTab.Controls.Add(Me.clbmastersupplier)
        Me.MasterTab.Controls.Add(Me.cbmasteruser)
        Me.MasterTab.Controls.Add(Me.clbmasterpricelist)
        Me.MasterTab.Controls.Add(Me.cbmasterkas)
        Me.MasterTab.Controls.Add(Me.clbmasterkas)
        Me.MasterTab.Controls.Add(Me.cbmasterpricelist)
        Me.MasterTab.Controls.Add(Me.clbmasterrekplng)
        Me.MasterTab.Controls.Add(Me.cbmasterreksupp)
        Me.MasterTab.Controls.Add(Me.clbmasterreksupp)
        Me.MasterTab.Controls.Add(Me.cbmasterrekplng)
        Me.MasterTab.Location = New System.Drawing.Point(4, 33)
        Me.MasterTab.Margin = New System.Windows.Forms.Padding(4)
        Me.MasterTab.Name = "MasterTab"
        Me.MasterTab.Padding = New System.Windows.Forms.Padding(4)
        Me.MasterTab.Size = New System.Drawing.Size(1002, 338)
        Me.MasterTab.TabIndex = 0
        Me.MasterTab.Text = "Master"
        '
        'TransaksiTab
        '
        Me.TransaksiTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TransaksiTab.Controls.Add(Me.cbpenyesuaianstok)
        Me.TransaksiTab.Controls.Add(Me.clbpenyesuaianstok)
        Me.TransaksiTab.Controls.Add(Me.clbpenjualan)
        Me.TransaksiTab.Controls.Add(Me.cbpembelian)
        Me.TransaksiTab.Controls.Add(Me.clbpembelian)
        Me.TransaksiTab.Controls.Add(Me.cbpenjualan)
        Me.TransaksiTab.Controls.Add(Me.clbreturjual)
        Me.TransaksiTab.Controls.Add(Me.cbreturbeli)
        Me.TransaksiTab.Controls.Add(Me.cbreturjual)
        Me.TransaksiTab.Controls.Add(Me.clbreturbeli)
        Me.TransaksiTab.Controls.Add(Me.clbbarangkeluar)
        Me.TransaksiTab.Controls.Add(Me.cbbarangmasuk)
        Me.TransaksiTab.Controls.Add(Me.cbbarangkeluar)
        Me.TransaksiTab.Controls.Add(Me.clbbarangmasuk)
        Me.TransaksiTab.Controls.Add(Me.cbtransferbarang)
        Me.TransaksiTab.Controls.Add(Me.clbtransferbarang)
        Me.TransaksiTab.Location = New System.Drawing.Point(4, 33)
        Me.TransaksiTab.Margin = New System.Windows.Forms.Padding(4)
        Me.TransaksiTab.Name = "TransaksiTab"
        Me.TransaksiTab.Padding = New System.Windows.Forms.Padding(4)
        Me.TransaksiTab.Size = New System.Drawing.Size(1002, 338)
        Me.TransaksiTab.TabIndex = 1
        Me.TransaksiTab.Text = "Transaksi"
        '
        'cbpenyesuaianstok
        '
        Me.cbpenyesuaianstok.AutoSize = True
        Me.cbpenyesuaianstok.Location = New System.Drawing.Point(442, 111)
        Me.cbpenyesuaianstok.Margin = New System.Windows.Forms.Padding(4)
        Me.cbpenyesuaianstok.Name = "cbpenyesuaianstok"
        Me.cbpenyesuaianstok.Size = New System.Drawing.Size(182, 28)
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
        Me.clbpenyesuaianstok.Size = New System.Drawing.Size(100, 52)
        Me.clbpenyesuaianstok.TabIndex = 81
        '
        'AdministrasiTab
        '
        Me.AdministrasiTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.AdministrasiTab.Controls.Add(Me.clbtransferkas)
        Me.AdministrasiTab.Controls.Add(Me.clbakunkeluar)
        Me.AdministrasiTab.Controls.Add(Me.clblunaspiutang)
        Me.AdministrasiTab.Controls.Add(Me.clbakunmasuk)
        Me.AdministrasiTab.Controls.Add(Me.cbtransferkas)
        Me.AdministrasiTab.Controls.Add(Me.cbakunmasuk)
        Me.AdministrasiTab.Controls.Add(Me.cblunaspiutang)
        Me.AdministrasiTab.Controls.Add(Me.cbakunkeluar)
        Me.AdministrasiTab.Controls.Add(Me.clblunasutang)
        Me.AdministrasiTab.Controls.Add(Me.cblunasutang)
        Me.AdministrasiTab.Location = New System.Drawing.Point(4, 33)
        Me.AdministrasiTab.Name = "AdministrasiTab"
        Me.AdministrasiTab.Size = New System.Drawing.Size(1002, 338)
        Me.AdministrasiTab.TabIndex = 2
        Me.AdministrasiTab.Text = "Administrasi"
        '
        'LaporanTab
        '
        Me.LaporanTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.LaporanTab.Controls.Add(Me.clblaprekapanharian)
        Me.LaporanTab.Controls.Add(Me.cblaprekapanharian)
        Me.LaporanTab.Controls.Add(Me.clblaplabarugi)
        Me.LaporanTab.Controls.Add(Me.cblaplabarugi)
        Me.LaporanTab.Controls.Add(Me.clblappenjualanpajak)
        Me.LaporanTab.Controls.Add(Me.cblappenjualanpajak)
        Me.LaporanTab.Controls.Add(Me.clblapmutasibarang)
        Me.LaporanTab.Controls.Add(Me.cblapmutasibarang)
        Me.LaporanTab.Controls.Add(Me.clblappenyesuaianstok)
        Me.LaporanTab.Controls.Add(Me.cblappenyesuaianstok)
        Me.LaporanTab.Controls.Add(Me.clblappricelist)
        Me.LaporanTab.Controls.Add(Me.cblappricelist)
        Me.LaporanTab.Controls.Add(Me.clblapmodalbarang)
        Me.LaporanTab.Controls.Add(Me.clblappenjualan)
        Me.LaporanTab.Controls.Add(Me.cblapmodalbarang)
        Me.LaporanTab.Controls.Add(Me.cblappenjualan)
        Me.LaporanTab.Controls.Add(Me.clblapbarangkeluar)
        Me.LaporanTab.Controls.Add(Me.clblapstokbarang)
        Me.LaporanTab.Controls.Add(Me.cblapbarangkeluar)
        Me.LaporanTab.Controls.Add(Me.cblapstokbarang)
        Me.LaporanTab.Controls.Add(Me.clblaptransaksikas)
        Me.LaporanTab.Controls.Add(Me.cblappembelian)
        Me.LaporanTab.Controls.Add(Me.cblaptransaksikas)
        Me.LaporanTab.Controls.Add(Me.clblapbarangmasuk)
        Me.LaporanTab.Controls.Add(Me.clblaptransferbarang)
        Me.LaporanTab.Controls.Add(Me.cblaptransferbarang)
        Me.LaporanTab.Controls.Add(Me.clblappembelian)
        Me.LaporanTab.Controls.Add(Me.clblaptransferkas)
        Me.LaporanTab.Controls.Add(Me.cblapbarangmasuk)
        Me.LaporanTab.Controls.Add(Me.cblaptransferkas)
        Me.LaporanTab.Controls.Add(Me.clblapreturjual)
        Me.LaporanTab.Controls.Add(Me.cblapreturbeli)
        Me.LaporanTab.Controls.Add(Me.clblapreturbeli)
        Me.LaporanTab.Controls.Add(Me.cblapreturjual)
        Me.LaporanTab.Controls.Add(Me.clblapakunkeluar)
        Me.LaporanTab.Controls.Add(Me.cblapakunmasuk)
        Me.LaporanTab.Controls.Add(Me.clblapakunmasuk)
        Me.LaporanTab.Controls.Add(Me.cblapakunkeluar)
        Me.LaporanTab.Controls.Add(Me.cblaputang)
        Me.LaporanTab.Controls.Add(Me.clblaputang)
        Me.LaporanTab.Controls.Add(Me.cblappiutang)
        Me.LaporanTab.Controls.Add(Me.clblappiutang)
        Me.LaporanTab.Location = New System.Drawing.Point(4, 33)
        Me.LaporanTab.Name = "LaporanTab"
        Me.LaporanTab.Size = New System.Drawing.Size(1002, 338)
        Me.LaporanTab.TabIndex = 3
        Me.LaporanTab.Text = "Laporan"
        '
        'clblaprekapanharian
        '
        Me.clblaprekapanharian.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clblaprekapanharian.CheckOnClick = True
        Me.clblaprekapanharian.FormattingEnabled = True
        Me.clblaprekapanharian.Items.AddRange(New Object() {"Print", "Export"})
        Me.clblaprekapanharian.Location = New System.Drawing.Point(846, 43)
        Me.clblaprekapanharian.Margin = New System.Windows.Forms.Padding(4)
        Me.clblaprekapanharian.Name = "clblaprekapanharian"
        Me.clblaprekapanharian.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clblaprekapanharian.Size = New System.Drawing.Size(134, 28)
        Me.clblaprekapanharian.TabIndex = 125
        '
        'cblaprekapanharian
        '
        Me.cblaprekapanharian.AutoSize = True
        Me.cblaprekapanharian.Location = New System.Drawing.Point(832, 13)
        Me.cblaprekapanharian.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaprekapanharian.Name = "cblaprekapanharian"
        Me.cblaprekapanharian.Size = New System.Drawing.Size(208, 28)
        Me.cblaprekapanharian.TabIndex = 124
        Me.cblaprekapanharian.Text = "Lap. Rekapan Harian"
        Me.cblaprekapanharian.UseVisualStyleBackColor = True
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
        Me.clblaplabarugi.Size = New System.Drawing.Size(134, 28)
        Me.clblaplabarugi.TabIndex = 123
        '
        'cblaplabarugi
        '
        Me.cblaplabarugi.AutoSize = True
        Me.cblaplabarugi.Location = New System.Drawing.Point(664, 253)
        Me.cblaplabarugi.Margin = New System.Windows.Forms.Padding(4)
        Me.cblaplabarugi.Name = "cblaplabarugi"
        Me.cblaplabarugi.Size = New System.Drawing.Size(158, 28)
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
        Me.clblappenjualanpajak.Size = New System.Drawing.Size(134, 28)
        Me.clblappenjualanpajak.TabIndex = 121
        '
        'cblappenjualanpajak
        '
        Me.cblappenjualanpajak.AutoSize = True
        Me.cblappenjualanpajak.Location = New System.Drawing.Point(8, 253)
        Me.cblappenjualanpajak.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappenjualanpajak.Name = "cblappenjualanpajak"
        Me.cblappenjualanpajak.Size = New System.Drawing.Size(207, 28)
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
        Me.clblapmutasibarang.Size = New System.Drawing.Size(134, 28)
        Me.clblapmutasibarang.TabIndex = 119
        '
        'cblapmutasibarang
        '
        Me.cblapmutasibarang.AutoSize = True
        Me.cblapmutasibarang.Location = New System.Drawing.Point(664, 93)
        Me.cblapmutasibarang.Margin = New System.Windows.Forms.Padding(4)
        Me.cblapmutasibarang.Name = "cblapmutasibarang"
        Me.cblapmutasibarang.Size = New System.Drawing.Size(192, 28)
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
        Me.clblappenyesuaianstok.Size = New System.Drawing.Size(134, 28)
        Me.clblappenyesuaianstok.TabIndex = 117
        '
        'cblappenyesuaianstok
        '
        Me.cblappenyesuaianstok.AutoSize = True
        Me.cblappenyesuaianstok.Location = New System.Drawing.Point(664, 173)
        Me.cblappenyesuaianstok.Margin = New System.Windows.Forms.Padding(4)
        Me.cblappenyesuaianstok.Name = "cblappenyesuaianstok"
        Me.cblappenyesuaianstok.Size = New System.Drawing.Size(223, 28)
        Me.cblappenyesuaianstok.TabIndex = 116
        Me.cblappenyesuaianstok.Text = "Lap. Penyesuaian Stok"
        Me.cblappenyesuaianstok.UseVisualStyleBackColor = True
        '
        'ChartTab
        '
        Me.ChartTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ChartTab.Controls.Add(Me.clbchartakunkeluar)
        Me.ChartTab.Controls.Add(Me.cbchartakunmasuk)
        Me.ChartTab.Controls.Add(Me.clbchartakunmasuk)
        Me.ChartTab.Controls.Add(Me.cbchartakunkeluar)
        Me.ChartTab.Controls.Add(Me.cbchartlunasutang)
        Me.ChartTab.Controls.Add(Me.clbchartlunasutang)
        Me.ChartTab.Controls.Add(Me.cbchartlunaspiutang)
        Me.ChartTab.Controls.Add(Me.clbchartlunaspiutang)
        Me.ChartTab.Controls.Add(Me.clbchartpenjualan)
        Me.ChartTab.Controls.Add(Me.cbchartpenjualan)
        Me.ChartTab.Controls.Add(Me.cbchartpembelian)
        Me.ChartTab.Controls.Add(Me.clbchartpembelian)
        Me.ChartTab.Location = New System.Drawing.Point(4, 33)
        Me.ChartTab.Name = "ChartTab"
        Me.ChartTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ChartTab.Size = New System.Drawing.Size(1002, 338)
        Me.ChartTab.TabIndex = 4
        Me.ChartTab.Text = "Chart"
        '
        'clbchartakunkeluar
        '
        Me.clbchartakunkeluar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbchartakunkeluar.CheckOnClick = True
        Me.clbchartakunkeluar.FormattingEnabled = True
        Me.clbchartakunkeluar.Items.AddRange(New Object() {"Print", "Export"})
        Me.clbchartakunkeluar.Location = New System.Drawing.Point(192, 126)
        Me.clbchartakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.clbchartakunkeluar.Name = "clbchartakunkeluar"
        Me.clbchartakunkeluar.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbchartakunkeluar.Size = New System.Drawing.Size(134, 28)
        Me.clbchartakunkeluar.TabIndex = 99
        '
        'cbchartakunmasuk
        '
        Me.cbchartakunmasuk.AutoSize = True
        Me.cbchartakunmasuk.Location = New System.Drawing.Point(180, 16)
        Me.cbchartakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.cbchartakunmasuk.Name = "cbchartakunmasuk"
        Me.cbchartakunmasuk.Size = New System.Drawing.Size(185, 28)
        Me.cbchartakunmasuk.TabIndex = 96
        Me.cbchartakunmasuk.Text = "Chart Akun Masuk"
        Me.cbchartakunmasuk.UseVisualStyleBackColor = True
        '
        'clbchartakunmasuk
        '
        Me.clbchartakunmasuk.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbchartakunmasuk.CheckOnClick = True
        Me.clbchartakunmasuk.FormattingEnabled = True
        Me.clbchartakunmasuk.Items.AddRange(New Object() {"Print", "Export"})
        Me.clbchartakunmasuk.Location = New System.Drawing.Point(192, 46)
        Me.clbchartakunmasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.clbchartakunmasuk.Name = "clbchartakunmasuk"
        Me.clbchartakunmasuk.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbchartakunmasuk.Size = New System.Drawing.Size(134, 28)
        Me.clbchartakunmasuk.TabIndex = 97
        '
        'cbchartakunkeluar
        '
        Me.cbchartakunkeluar.AutoSize = True
        Me.cbchartakunkeluar.Location = New System.Drawing.Point(180, 96)
        Me.cbchartakunkeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.cbchartakunkeluar.Name = "cbchartakunkeluar"
        Me.cbchartakunkeluar.Size = New System.Drawing.Size(184, 28)
        Me.cbchartakunkeluar.TabIndex = 98
        Me.cbchartakunkeluar.Text = "Chart Akun Keluar"
        Me.cbchartakunkeluar.UseVisualStyleBackColor = True
        '
        'cbchartlunasutang
        '
        Me.cbchartlunasutang.AutoSize = True
        Me.cbchartlunasutang.Location = New System.Drawing.Point(18, 176)
        Me.cbchartlunasutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbchartlunasutang.Name = "cbchartlunasutang"
        Me.cbchartlunasutang.Size = New System.Drawing.Size(186, 28)
        Me.cbchartlunasutang.TabIndex = 60
        Me.cbchartlunasutang.Text = "Chart Lunas Utang"
        Me.cbchartlunasutang.UseVisualStyleBackColor = True
        '
        'clbchartlunasutang
        '
        Me.clbchartlunasutang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbchartlunasutang.CheckOnClick = True
        Me.clbchartlunasutang.FormattingEnabled = True
        Me.clbchartlunasutang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clbchartlunasutang.Location = New System.Drawing.Point(29, 206)
        Me.clbchartlunasutang.Margin = New System.Windows.Forms.Padding(4)
        Me.clbchartlunasutang.Name = "clbchartlunasutang"
        Me.clbchartlunasutang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbchartlunasutang.Size = New System.Drawing.Size(134, 28)
        Me.clbchartlunasutang.TabIndex = 61
        '
        'cbchartlunaspiutang
        '
        Me.cbchartlunaspiutang.AutoSize = True
        Me.cbchartlunaspiutang.Location = New System.Drawing.Point(18, 256)
        Me.cbchartlunaspiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.cbchartlunaspiutang.Name = "cbchartlunaspiutang"
        Me.cbchartlunaspiutang.Size = New System.Drawing.Size(200, 28)
        Me.cbchartlunaspiutang.TabIndex = 62
        Me.cbchartlunaspiutang.Text = "Chart Lunas Piutang"
        Me.cbchartlunaspiutang.UseVisualStyleBackColor = True
        '
        'clbchartlunaspiutang
        '
        Me.clbchartlunaspiutang.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbchartlunaspiutang.CheckOnClick = True
        Me.clbchartlunaspiutang.FormattingEnabled = True
        Me.clbchartlunaspiutang.Items.AddRange(New Object() {"Print", "Export"})
        Me.clbchartlunaspiutang.Location = New System.Drawing.Point(29, 286)
        Me.clbchartlunaspiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.clbchartlunaspiutang.Name = "clbchartlunaspiutang"
        Me.clbchartlunaspiutang.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbchartlunaspiutang.Size = New System.Drawing.Size(134, 28)
        Me.clbchartlunaspiutang.TabIndex = 63
        '
        'clbchartpenjualan
        '
        Me.clbchartpenjualan.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbchartpenjualan.CheckOnClick = True
        Me.clbchartpenjualan.FormattingEnabled = True
        Me.clbchartpenjualan.Items.AddRange(New Object() {"Print", "Export"})
        Me.clbchartpenjualan.Location = New System.Drawing.Point(29, 126)
        Me.clbchartpenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.clbchartpenjualan.Name = "clbchartpenjualan"
        Me.clbchartpenjualan.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbchartpenjualan.Size = New System.Drawing.Size(134, 28)
        Me.clbchartpenjualan.TabIndex = 57
        '
        'cbchartpenjualan
        '
        Me.cbchartpenjualan.AutoSize = True
        Me.cbchartpenjualan.Location = New System.Drawing.Point(18, 96)
        Me.cbchartpenjualan.Margin = New System.Windows.Forms.Padding(4)
        Me.cbchartpenjualan.Name = "cbchartpenjualan"
        Me.cbchartpenjualan.Size = New System.Drawing.Size(165, 28)
        Me.cbchartpenjualan.TabIndex = 56
        Me.cbchartpenjualan.Text = "Chart Penjualan"
        Me.cbchartpenjualan.UseVisualStyleBackColor = True
        '
        'cbchartpembelian
        '
        Me.cbchartpembelian.AutoSize = True
        Me.cbchartpembelian.Location = New System.Drawing.Point(18, 16)
        Me.cbchartpembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.cbchartpembelian.Name = "cbchartpembelian"
        Me.cbchartpembelian.Size = New System.Drawing.Size(171, 28)
        Me.cbchartpembelian.TabIndex = 58
        Me.cbchartpembelian.Text = "Chart Pembelian"
        Me.cbchartpembelian.UseVisualStyleBackColor = True
        '
        'clbchartpembelian
        '
        Me.clbchartpembelian.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.clbchartpembelian.CheckOnClick = True
        Me.clbchartpembelian.FormattingEnabled = True
        Me.clbchartpembelian.Items.AddRange(New Object() {"Print", "Export"})
        Me.clbchartpembelian.Location = New System.Drawing.Point(29, 46)
        Me.clbchartpembelian.Margin = New System.Windows.Forms.Padding(4)
        Me.clbchartpembelian.Name = "clbchartpembelian"
        Me.clbchartpembelian.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.clbchartpembelian.Size = New System.Drawing.Size(134, 28)
        Me.clbchartpembelian.TabIndex = 59
        '
        'FeatureTab
        '
        Me.FeatureTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.FeatureTab.Controls.Add(Me.cbbarcodegenerator)
        Me.FeatureTab.Controls.Add(Me.cbkalkulasiexpedisi)
        Me.FeatureTab.Location = New System.Drawing.Point(4, 33)
        Me.FeatureTab.Name = "FeatureTab"
        Me.FeatureTab.Padding = New System.Windows.Forms.Padding(3)
        Me.FeatureTab.Size = New System.Drawing.Size(1002, 338)
        Me.FeatureTab.TabIndex = 6
        Me.FeatureTab.Text = "Feature"
        '
        'cbbarcodegenerator
        '
        Me.cbbarcodegenerator.AutoSize = True
        Me.cbbarcodegenerator.Location = New System.Drawing.Point(11, 48)
        Me.cbbarcodegenerator.Margin = New System.Windows.Forms.Padding(4)
        Me.cbbarcodegenerator.Name = "cbbarcodegenerator"
        Me.cbbarcodegenerator.Size = New System.Drawing.Size(192, 28)
        Me.cbbarcodegenerator.TabIndex = 103
        Me.cbbarcodegenerator.Text = "Barcode Generator"
        Me.cbbarcodegenerator.UseVisualStyleBackColor = True
        '
        'cbkalkulasiexpedisi
        '
        Me.cbkalkulasiexpedisi.AutoSize = True
        Me.cbkalkulasiexpedisi.Location = New System.Drawing.Point(11, 18)
        Me.cbkalkulasiexpedisi.Margin = New System.Windows.Forms.Padding(4)
        Me.cbkalkulasiexpedisi.Name = "cbkalkulasiexpedisi"
        Me.cbkalkulasiexpedisi.Size = New System.Drawing.Size(183, 28)
        Me.cbkalkulasiexpedisi.TabIndex = 104
        Me.cbkalkulasiexpedisi.Text = "Kalkulasi Expedisi"
        Me.cbkalkulasiexpedisi.UseVisualStyleBackColor = True
        '
        'SettingTab
        '
        Me.SettingTab.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SettingTab.Controls.Add(Me.cbbackupdatabase)
        Me.SettingTab.Controls.Add(Me.cbpengaturan)
        Me.SettingTab.Controls.Add(Me.cbprinter)
        Me.SettingTab.Controls.Add(Me.cbinfoperusahaan)
        Me.SettingTab.Location = New System.Drawing.Point(4, 33)
        Me.SettingTab.Name = "SettingTab"
        Me.SettingTab.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTab.Size = New System.Drawing.Size(1002, 338)
        Me.SettingTab.TabIndex = 5
        Me.SettingTab.Text = "Setting"
        '
        'cbbackupdatabase
        '
        Me.cbbackupdatabase.AutoSize = True
        Me.cbbackupdatabase.Location = New System.Drawing.Point(17, 77)
        Me.cbbackupdatabase.Margin = New System.Windows.Forms.Padding(4)
        Me.cbbackupdatabase.Name = "cbbackupdatabase"
        Me.cbbackupdatabase.Size = New System.Drawing.Size(178, 28)
        Me.cbbackupdatabase.TabIndex = 104
        Me.cbbackupdatabase.Text = "Backup Database"
        Me.cbbackupdatabase.UseVisualStyleBackColor = True
        '
        'cbpengaturan
        '
        Me.cbpengaturan.AutoSize = True
        Me.cbpengaturan.Location = New System.Drawing.Point(18, 107)
        Me.cbpengaturan.Margin = New System.Windows.Forms.Padding(4)
        Me.cbpengaturan.Name = "cbpengaturan"
        Me.cbpengaturan.Size = New System.Drawing.Size(129, 28)
        Me.cbpengaturan.TabIndex = 106
        Me.cbpengaturan.Text = "Pengaturan"
        Me.cbpengaturan.UseVisualStyleBackColor = True
        '
        'cbprinter
        '
        Me.cbprinter.AutoSize = True
        Me.cbprinter.Location = New System.Drawing.Point(17, 47)
        Me.cbprinter.Margin = New System.Windows.Forms.Padding(4)
        Me.cbprinter.Name = "cbprinter"
        Me.cbprinter.Size = New System.Drawing.Size(86, 28)
        Me.cbprinter.TabIndex = 100
        Me.cbprinter.Text = "Printer"
        Me.cbprinter.UseVisualStyleBackColor = True
        '
        'cbinfoperusahaan
        '
        Me.cbinfoperusahaan.AutoSize = True
        Me.cbinfoperusahaan.Location = New System.Drawing.Point(17, 17)
        Me.cbinfoperusahaan.Margin = New System.Windows.Forms.Padding(4)
        Me.cbinfoperusahaan.Name = "cbinfoperusahaan"
        Me.cbinfoperusahaan.Size = New System.Drawing.Size(168, 28)
        Me.cbinfoperusahaan.TabIndex = 102
        Me.cbinfoperusahaan.Text = "Info Perusahaan"
        Me.cbinfoperusahaan.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 9)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(156, 36)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Data User"
        '
        'cbauth
        '
        Me.cbauth.AutoSize = True
        Me.cbauth.Location = New System.Drawing.Point(363, 128)
        Me.cbauth.Margin = New System.Windows.Forms.Padding(4)
        Me.cbauth.Name = "cbauth"
        Me.cbauth.Size = New System.Drawing.Size(71, 28)
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
        Me.txtmaxprint.Size = New System.Drawing.Size(51, 29)
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
        Me.Label10.Size = New System.Drawing.Size(94, 25)
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
        'fuser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
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
        Me.Controls.Add(Me.TabControl)
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
        Me.TabControl.ResumeLayout(False)
        Me.MasterTab.ResumeLayout(False)
        Me.MasterTab.PerformLayout()
        Me.TransaksiTab.ResumeLayout(False)
        Me.TransaksiTab.PerformLayout()
        Me.AdministrasiTab.ResumeLayout(False)
        Me.AdministrasiTab.PerformLayout()
        Me.LaporanTab.ResumeLayout(False)
        Me.LaporanTab.PerformLayout()
        Me.ChartTab.ResumeLayout(False)
        Me.ChartTab.PerformLayout()
        Me.FeatureTab.ResumeLayout(False)
        Me.FeatureTab.PerformLayout()
        Me.SettingTab.ResumeLayout(False)
        Me.SettingTab.PerformLayout()
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
    Friend WithEvents TabControl As TabControl
    Friend WithEvents MasterTab As TabPage
    Friend WithEvents TransaksiTab As TabPage
    Friend WithEvents AdministrasiTab As TabPage
    Friend WithEvents LaporanTab As TabPage
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
    Friend WithEvents clblaprekapanharian As CheckedListBox
    Friend WithEvents cblaprekapanharian As CheckBox
    Friend WithEvents btnrefresh As Button
    Friend WithEvents ChartTab As TabPage
    Friend WithEvents clbchartpenjualan As CheckedListBox
    Friend WithEvents cbchartpenjualan As CheckBox
    Friend WithEvents cbchartpembelian As CheckBox
    Friend WithEvents clbchartpembelian As CheckedListBox
    Friend WithEvents cbchartlunasutang As CheckBox
    Friend WithEvents clbchartlunasutang As CheckedListBox
    Friend WithEvents cbchartlunaspiutang As CheckBox
    Friend WithEvents clbchartlunaspiutang As CheckedListBox
    Friend WithEvents clbchartakunkeluar As CheckedListBox
    Friend WithEvents cbchartakunmasuk As CheckBox
    Friend WithEvents clbchartakunmasuk As CheckedListBox
    Friend WithEvents cbchartakunkeluar As CheckBox
    Friend WithEvents SettingTab As TabPage
    Friend WithEvents cbbackupdatabase As CheckBox
    Friend WithEvents cbpengaturan As CheckBox
    Friend WithEvents cbprinter As CheckBox
    Friend WithEvents cbinfoperusahaan As CheckBox
    Friend WithEvents FeatureTab As TabPage
    Friend WithEvents cbbarcodegenerator As CheckBox
    Friend WithEvents cbkalkulasiexpedisi As CheckBox
End Class
