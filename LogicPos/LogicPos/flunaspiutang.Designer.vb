<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class flunaspiutang
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(flunaspiutang))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riteterimapelunasan = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtbukti = New System.Windows.Forms.TextBox()
        Me.cmbcustomer = New System.Windows.Forms.ComboBox()
        Me.txtalamat = New System.Windows.Forms.RichTextBox()
        Me.txtcustomer = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbbayar = New System.Windows.Forms.ComboBox()
        Me.txttelp = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txttotalbayar = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dtpelunasan = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnolunaspiutang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.cbvoid = New System.Windows.Forms.CheckBox()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.btncaripelunasan = New System.Windows.Forms.Button()
        Me.btnprev = New System.Windows.Forms.Button()
        Me.btnnext = New System.Windows.Forms.Button()
        Me.txtgolunas = New System.Windows.Forms.TextBox()
        Me.btngolunas = New System.Windows.Forms.Button()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnbaru = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtsisajual = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtbayarjual = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txttotaljual = New System.Windows.Forms.TextBox()
        Me.btncarijual = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.txtkodepenjualan = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtselisih = New System.Windows.Forms.TextBox()
        Me.btnsesuaikan = New System.Windows.Forms.Button()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riteterimapelunasan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Location = New System.Drawing.Point(16, 324)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riteterimapelunasan})
        Me.GridControl1.Size = New System.Drawing.Size(1189, 347)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.Caption = "Kode Penjualan"
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
        Me.GridColumn2.Caption = "Kode Customer"
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
        Me.GridColumn3.Caption = "Tanggal Penjualan"
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
        Me.GridColumn4.Caption = "Tanggal Jatuh Tempo"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceCell.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.Caption = "Total Nota"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
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
        Me.GridColumn6.Caption = "Telah Dibayar"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
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
        Me.GridColumn7.Caption = "Terima"
        Me.GridColumn7.ColumnEdit = Me.riteterimapelunasan
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'riteterimapelunasan
        '
        Me.riteterimapelunasan.AutoHeight = False
        Me.riteterimapelunasan.Name = "riteterimapelunasan"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label9.Location = New System.Drawing.Point(816, 126)
        Me.Label9.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 18)
        Me.Label9.TabIndex = 83
        Me.Label9.Text = "No Bukti"
        '
        'txtbukti
        '
        Me.txtbukti.Enabled = False
        Me.txtbukti.Location = New System.Drawing.Point(906, 124)
        Me.txtbukti.Margin = New System.Windows.Forms.Padding(6)
        Me.txtbukti.MaxLength = 12
        Me.txtbukti.Name = "txtbukti"
        Me.txtbukti.Size = New System.Drawing.Size(245, 24)
        Me.txtbukti.TabIndex = 82
        '
        'cmbcustomer
        '
        Me.cmbcustomer.FormattingEnabled = True
        Me.cmbcustomer.Location = New System.Drawing.Point(152, 124)
        Me.cmbcustomer.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbcustomer.MaxLength = 99
        Me.cmbcustomer.Name = "cmbcustomer"
        Me.cmbcustomer.Size = New System.Drawing.Size(245, 26)
        Me.cmbcustomer.TabIndex = 81
        '
        'txtalamat
        '
        Me.txtalamat.Enabled = False
        Me.txtalamat.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtalamat.Location = New System.Drawing.Point(532, 89)
        Me.txtalamat.Margin = New System.Windows.Forms.Padding(6)
        Me.txtalamat.Name = "txtalamat"
        Me.txtalamat.Size = New System.Drawing.Size(245, 61)
        Me.txtalamat.TabIndex = 70
        Me.txtalamat.Text = ""
        '
        'txtcustomer
        '
        Me.txtcustomer.Enabled = False
        Me.txtcustomer.Location = New System.Drawing.Point(533, 15)
        Me.txtcustomer.Margin = New System.Windows.Forms.Padding(6)
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.Size = New System.Drawing.Size(245, 24)
        Me.txtcustomer.TabIndex = 46
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(408, 18)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 18)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Nama Customer"
        '
        'cmbbayar
        '
        Me.cmbbayar.FormattingEnabled = True
        Me.cmbbayar.Location = New System.Drawing.Point(906, 51)
        Me.cmbbayar.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbbayar.MaxLength = 99
        Me.cmbbayar.Name = "cmbbayar"
        Me.cmbbayar.Size = New System.Drawing.Size(245, 26)
        Me.cmbbayar.TabIndex = 79
        '
        'txttelp
        '
        Me.txttelp.Enabled = False
        Me.txttelp.Location = New System.Drawing.Point(533, 52)
        Me.txttelp.Margin = New System.Windows.Forms.Padding(6)
        Me.txttelp.Name = "txttelp"
        Me.txttelp.Size = New System.Drawing.Size(245, 24)
        Me.txttelp.TabIndex = 71
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label5.Location = New System.Drawing.Point(798, 55)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 18)
        Me.Label5.TabIndex = 80
        Me.Label5.Text = "Metode Bayar"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label20.Location = New System.Drawing.Point(406, 92)
        Me.Label20.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(123, 18)
        Me.Label20.TabIndex = 68
        Me.Label20.Text = "Alamat Customer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(805, 93)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 18)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Total Bayar"
        '
        'txttotalbayar
        '
        Me.txttotalbayar.Enabled = False
        Me.txttotalbayar.Location = New System.Drawing.Point(906, 89)
        Me.txttotalbayar.Margin = New System.Windows.Forms.Padding(6)
        Me.txttotalbayar.MaxLength = 12
        Me.txttotalbayar.Name = "txttotalbayar"
        Me.txttotalbayar.Size = New System.Drawing.Size(245, 24)
        Me.txttotalbayar.TabIndex = 77
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label22.Location = New System.Drawing.Point(414, 55)
        Me.Label22.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(98, 18)
        Me.Label22.TabIndex = 69
        Me.Label22.Text = "Tlp Customer"
        '
        'dtpelunasan
        '
        Me.dtpelunasan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.dtpelunasan.Location = New System.Drawing.Point(152, 89)
        Me.dtpelunasan.Margin = New System.Windows.Forms.Padding(6)
        Me.dtpelunasan.Name = "dtpelunasan"
        Me.dtpelunasan.Size = New System.Drawing.Size(245, 23)
        Me.dtpelunasan.TabIndex = 75
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(14, 92)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 18)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Tgl Lunas Piutang"
        '
        'txtnolunaspiutang
        '
        Me.txtnolunaspiutang.Enabled = False
        Me.txtnolunaspiutang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnolunaspiutang.Location = New System.Drawing.Point(152, 15)
        Me.txtnolunaspiutang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnolunaspiutang.Name = "txtnolunaspiutang"
        Me.txtnolunaspiutang.Size = New System.Drawing.Size(245, 24)
        Me.txtnolunaspiutang.TabIndex = 73
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label2.Location = New System.Drawing.Point(13, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 18)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "No. Lunas Piutang"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(1073, 17)
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
        Me.cbprinted.Location = New System.Drawing.Point(986, 17)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(4)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'cbvoid
        '
        Me.cbvoid.AutoSize = True
        Me.cbvoid.Enabled = False
        Me.cbvoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbvoid.Location = New System.Drawing.Point(909, 17)
        Me.cbvoid.Margin = New System.Windows.Forms.Padding(4)
        Me.cbvoid.Name = "cbvoid"
        Me.cbvoid.Size = New System.Drawing.Size(56, 22)
        Me.cbvoid.TabIndex = 39
        Me.cbvoid.Text = "Void"
        Me.cbvoid.UseVisualStyleBackColor = True
        Me.cbvoid.Visible = False
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(152, 51)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(245, 26)
        Me.cmbsales.TabIndex = 57
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label16.Location = New System.Drawing.Point(33, 54)
        Me.Label16.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 18)
        Me.Label16.TabIndex = 66
        Me.Label16.Text = "Kode Sales"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label19.Location = New System.Drawing.Point(21, 127)
        Me.Label19.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(113, 18)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "Kode Customer"
        '
        'txtketerangan
        '
        Me.txtketerangan.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtketerangan.Enabled = False
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtketerangan.Location = New System.Drawing.Point(9, 9)
        Me.txtketerangan.Margin = New System.Windows.Forms.Padding(6)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(1163, 145)
        Me.txtketerangan.TabIndex = 73
        Me.txtketerangan.Text = ""
        '
        'btncaripelunasan
        '
        Me.btncaripelunasan.BackgroundImage = CType(resources.GetObject("btncaripelunasan.BackgroundImage"), System.Drawing.Image)
        Me.btncaripelunasan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaripelunasan.ImageIndex = 0
        Me.btncaripelunasan.Location = New System.Drawing.Point(1074, 19)
        Me.btncaripelunasan.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaripelunasan.Name = "btncaripelunasan"
        Me.btncaripelunasan.Size = New System.Drawing.Size(30, 27)
        Me.btncaripelunasan.TabIndex = 87
        Me.btncaripelunasan.UseVisualStyleBackColor = True
        '
        'btnprev
        '
        Me.btnprev.Location = New System.Drawing.Point(859, 19)
        Me.btnprev.Margin = New System.Windows.Forms.Padding(4)
        Me.btnprev.Name = "btnprev"
        Me.btnprev.Size = New System.Drawing.Size(35, 27)
        Me.btnprev.TabIndex = 5
        Me.btnprev.Text = "<<"
        Me.btnprev.UseVisualStyleBackColor = True
        '
        'btnnext
        '
        Me.btnnext.Location = New System.Drawing.Point(1159, 19)
        Me.btnnext.Margin = New System.Windows.Forms.Padding(4)
        Me.btnnext.Name = "btnnext"
        Me.btnnext.Size = New System.Drawing.Size(35, 27)
        Me.btnnext.TabIndex = 8
        Me.btnnext.Text = ">>"
        Me.btnnext.UseVisualStyleBackColor = True
        '
        'txtgolunas
        '
        Me.txtgolunas.Location = New System.Drawing.Point(897, 21)
        Me.txtgolunas.Margin = New System.Windows.Forms.Padding(4)
        Me.txtgolunas.Name = "txtgolunas"
        Me.txtgolunas.Size = New System.Drawing.Size(174, 24)
        Me.txtgolunas.TabIndex = 7
        '
        'btngolunas
        '
        Me.btngolunas.Location = New System.Drawing.Point(1106, 19)
        Me.btngolunas.Margin = New System.Windows.Forms.Padding(4)
        Me.btngolunas.Name = "btngolunas"
        Me.btngolunas.Size = New System.Drawing.Size(50, 27)
        Me.btngolunas.TabIndex = 6
        Me.btngolunas.Text = "Go"
        Me.btngolunas.UseVisualStyleBackColor = True
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(745, 17)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(4)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(110, 30)
        Me.btnbatal.TabIndex = 4
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(632, 17)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(110, 30)
        Me.btnedit.TabIndex = 1
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnbaru
        '
        Me.btnbaru.Location = New System.Drawing.Point(293, 17)
        Me.btnbaru.Margin = New System.Windows.Forms.Padding(4)
        Me.btnbaru.Name = "btnbaru"
        Me.btnbaru.Size = New System.Drawing.Size(110, 30)
        Me.btnbaru.TabIndex = 1
        Me.btnbaru.Text = "Baru"
        Me.btnbaru.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(519, 17)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(110, 30)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'btnsimpan
        '
        Me.btnsimpan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnsimpan.Location = New System.Drawing.Point(406, 17)
        Me.btnsimpan.Margin = New System.Windows.Forms.Padding(4)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(110, 30)
        Me.btnsimpan.TabIndex = 2
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 19)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(258, 31)
        Me.Label7.TabIndex = 77
        Me.Label7.Text = "Pelunasan Piutang"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label11.Location = New System.Drawing.Point(942, 264)
        Me.Label11.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 18)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Sisa Nota"
        '
        'txtsisajual
        '
        Me.txtsisajual.Enabled = False
        Me.txtsisajual.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsisajual.Location = New System.Drawing.Point(939, 287)
        Me.txtsisajual.Margin = New System.Windows.Forms.Padding(6)
        Me.txtsisajual.Name = "txtsisajual"
        Me.txtsisajual.Size = New System.Drawing.Size(210, 24)
        Me.txtsisajual.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label10.Location = New System.Drawing.Point(725, 264)
        Me.Label10.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 18)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Telah Dibayar"
        '
        'txtbayarjual
        '
        Me.txtbayarjual.Enabled = False
        Me.txtbayarjual.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbayarjual.Location = New System.Drawing.Point(725, 287)
        Me.txtbayarjual.Margin = New System.Windows.Forms.Padding(6)
        Me.txtbayarjual.Name = "txtbayarjual"
        Me.txtbayarjual.Size = New System.Drawing.Size(210, 24)
        Me.txtbayarjual.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label8.Location = New System.Drawing.Point(512, 264)
        Me.Label8.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 18)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Total Nota"
        '
        'txttotaljual
        '
        Me.txttotaljual.Enabled = False
        Me.txttotaljual.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotaljual.Location = New System.Drawing.Point(511, 287)
        Me.txttotaljual.Margin = New System.Windows.Forms.Padding(6)
        Me.txttotaljual.Name = "txttotaljual"
        Me.txttotaljual.Size = New System.Drawing.Size(210, 24)
        Me.txttotaljual.TabIndex = 13
        '
        'btncarijual
        '
        Me.btncarijual.BackgroundImage = CType(resources.GetObject("btncarijual.BackgroundImage"), System.Drawing.Image)
        Me.btncarijual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarijual.ImageIndex = 0
        Me.btncarijual.Location = New System.Drawing.Point(237, 286)
        Me.btncarijual.Margin = New System.Windows.Forms.Padding(6)
        Me.btncarijual.Name = "btncarijual"
        Me.btncarijual.Size = New System.Drawing.Size(29, 28)
        Me.btncarijual.TabIndex = 5
        Me.btncarijual.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.BackgroundImage = CType(resources.GetObject("btntambah.BackgroundImage"), System.Drawing.Image)
        Me.btntambah.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btntambah.ImageIndex = 0
        Me.btntambah.Location = New System.Drawing.Point(1152, 268)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(6)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(50, 46)
        Me.btntambah.TabIndex = 5
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txtkodepenjualan
        '
        Me.txtkodepenjualan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodepenjualan.Location = New System.Drawing.Point(17, 287)
        Me.txtkodepenjualan.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkodepenjualan.Name = "txtkodepenjualan"
        Me.txtkodepenjualan.Size = New System.Drawing.Size(216, 24)
        Me.txtkodepenjualan.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label12.Location = New System.Drawing.Point(19, 264)
        Me.Label12.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(111, 18)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "Kode Penjualan"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(17, 66)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1189, 194)
        Me.TabControl1.TabIndex = 88
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.cbposted)
        Me.TabPage1.Controls.Add(Me.txtbukti)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.cmbbayar)
        Me.TabPage1.Controls.Add(Me.cbprinted)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.cbvoid)
        Me.TabPage1.Controls.Add(Me.txttotalbayar)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.txtalamat)
        Me.TabPage1.Controls.Add(Me.cmbcustomer)
        Me.TabPage1.Controls.Add(Me.txtcustomer)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.cmbsales)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txttelp)
        Me.TabPage1.Controls.Add(Me.txtnolunaspiutang)
        Me.TabPage1.Controls.Add(Me.dtpelunasan)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1181, 163)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Pelunasan Piutang"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label6.Location = New System.Drawing.Point(816, 19)
        Me.Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 18)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Status"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.TabPage2.Controls.Add(Me.txtketerangan)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1181, 163)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Keterangan"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label13.Location = New System.Drawing.Point(885, 684)
        Me.Label13.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 18)
        Me.Label13.TabIndex = 90
        Me.Label13.Text = "Selisih"
        '
        'txtselisih
        '
        Me.txtselisih.Enabled = False
        Me.txtselisih.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtselisih.Location = New System.Drawing.Point(939, 681)
        Me.txtselisih.Margin = New System.Windows.Forms.Padding(6)
        Me.txtselisih.Name = "txtselisih"
        Me.txtselisih.Size = New System.Drawing.Size(210, 24)
        Me.txtselisih.TabIndex = 89
        '
        'btnsesuaikan
        '
        Me.btnsesuaikan.Location = New System.Drawing.Point(1155, 680)
        Me.btnsesuaikan.Margin = New System.Windows.Forms.Padding(4)
        Me.btnsesuaikan.Name = "btnsesuaikan"
        Me.btnsesuaikan.Size = New System.Drawing.Size(50, 27)
        Me.btnsesuaikan.TabIndex = 91
        Me.btnsesuaikan.Text = "-/+"
        Me.btnsesuaikan.UseVisualStyleBackColor = True
        '
        'flunaspiutang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1223, 727)
        Me.Controls.Add(Me.btnsesuaikan)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtselisih)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtsisajual)
        Me.Controls.Add(Me.btncaripelunasan)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtbayarjual)
        Me.Controls.Add(Me.btnprev)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.txttotaljual)
        Me.Controls.Add(Me.btnnext)
        Me.Controls.Add(Me.btncarijual)
        Me.Controls.Add(Me.txtgolunas)
        Me.Controls.Add(Me.txtkodepenjualan)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btngolunas)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnbaru)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.GridControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "flunaspiutang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Pelunasan Piutang"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riteterimapelunasan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtpelunasan As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents txtnolunaspiutang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents cbvoid As CheckBox
    Friend WithEvents txttelp As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtcustomer As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnprev As Button
    Friend WithEvents btnnext As Button
    Friend WithEvents txtgolunas As TextBox
    Friend WithEvents btngolunas As Button
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnbaru As Button
    Friend WithEvents btnprint As Button
    Friend WithEvents btnsimpan As Button
    Friend WithEvents cmbbayar As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txttotalbayar As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txtalamat As RichTextBox
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbcustomer As ComboBox
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btncarijual As Button
    Friend WithEvents btntambah As Button
    Friend WithEvents txtkodepenjualan As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtbukti As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtbayarjual As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txttotaljual As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtsisajual As TextBox
    Friend WithEvents riteterimapelunasan As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents btncaripelunasan As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label6 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtselisih As TextBox
    Friend WithEvents btnsesuaikan As Button
End Class
