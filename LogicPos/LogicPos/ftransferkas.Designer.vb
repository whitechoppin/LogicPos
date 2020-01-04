<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ftransferkas
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dttransaksi = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.txtnamakekas = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbkekas = New System.Windows.Forms.ComboBox()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.txtketerangantransfer = New System.Windows.Forms.RichTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtsaldotransfer = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtnamadarikas = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbdarikas = New System.Windows.Forms.ComboBox()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnhapus = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.txtkodetransfer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dttransaksi)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.txtnamakekas)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbkekas)
        Me.GroupBox1.Controls.Add(Me.btnprint)
        Me.GroupBox1.Controls.Add(Me.txtketerangantransfer)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtsaldotransfer)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtnamadarikas)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbdarikas)
        Me.GroupBox1.Controls.Add(Me.cmbsales)
        Me.GroupBox1.Controls.Add(Me.btnbatal)
        Me.GroupBox1.Controls.Add(Me.btnhapus)
        Me.GroupBox1.Controls.Add(Me.btnedit)
        Me.GroupBox1.Controls.Add(Me.btntambah)
        Me.GroupBox1.Controls.Add(Me.txtkodetransfer)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 218)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(925, 285)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input Transfer Kas"
        '
        'dttransaksi
        '
        Me.dttransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dttransaksi.Location = New System.Drawing.Point(11, 195)
        Me.dttransaksi.Margin = New System.Windows.Forms.Padding(4)
        Me.dttransaksi.Name = "dttransaksi"
        Me.dttransaksi.Size = New System.Drawing.Size(389, 24)
        Me.dttransaksi.TabIndex = 43
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 20)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Tanggal Transaksi"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cbposted)
        Me.GroupBox6.Controls.Add(Me.cbprinted)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.GroupBox6.Location = New System.Drawing.Point(725, 24)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Size = New System.Drawing.Size(188, 58)
        Me.GroupBox6.TabIndex = 37
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Status"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(92, 23)
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
        Me.cbprinted.Location = New System.Drawing.Point(11, 23)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(4)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'txtnamakekas
        '
        Me.txtnamakekas.Enabled = False
        Me.txtnamakekas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamakekas.Location = New System.Drawing.Point(727, 135)
        Me.txtnamakekas.Name = "txtnamakekas"
        Me.txtnamakekas.Size = New System.Drawing.Size(186, 24)
        Me.txtnamakekas.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(621, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 20)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Nama Ke Kas"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(621, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 20)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Kode Ke Kas"
        '
        'cmbkekas
        '
        Me.cmbkekas.FormattingEnabled = True
        Me.cmbkekas.Location = New System.Drawing.Point(727, 102)
        Me.cmbkekas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbkekas.MaxLength = 99
        Me.cmbkekas.Name = "cmbkekas"
        Me.cmbkekas.Size = New System.Drawing.Size(186, 26)
        Me.cmbkekas.TabIndex = 32
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(580, 32)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(135, 50)
        Me.btnprint.TabIndex = 31
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'txtketerangantransfer
        '
        Me.txtketerangantransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtketerangantransfer.Location = New System.Drawing.Point(424, 193)
        Me.txtketerangantransfer.Name = "txtketerangantransfer"
        Me.txtketerangantransfer.Size = New System.Drawing.Size(487, 79)
        Me.txtketerangantransfer.TabIndex = 29
        Me.txtketerangantransfer.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(424, 170)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(155, 20)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Keterangan Transfer"
        '
        'txtsaldotransfer
        '
        Me.txtsaldotransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsaldotransfer.Location = New System.Drawing.Point(11, 248)
        Me.txtsaldotransfer.Name = "txtsaldotransfer"
        Me.txtsaldotransfer.Size = New System.Drawing.Size(390, 24)
        Me.txtsaldotransfer.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 223)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 20)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Saldo Transfer"
        '
        'txtnamadarikas
        '
        Me.txtnamadarikas.Enabled = False
        Me.txtnamadarikas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamadarikas.Location = New System.Drawing.Point(431, 135)
        Me.txtnamadarikas.Name = "txtnamadarikas"
        Me.txtnamadarikas.Size = New System.Drawing.Size(186, 24)
        Me.txtnamadarikas.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(315, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 20)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Nama Dari Kas"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(315, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 20)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Kode Dari Kas"
        '
        'cmbdarikas
        '
        Me.cmbdarikas.FormattingEnabled = True
        Me.cmbdarikas.Location = New System.Drawing.Point(431, 102)
        Me.cmbdarikas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbdarikas.MaxLength = 99
        Me.cmbdarikas.Name = "cmbdarikas"
        Me.cmbdarikas.Size = New System.Drawing.Size(186, 26)
        Me.cmbdarikas.TabIndex = 20
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(126, 131)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(186, 26)
        Me.cmbsales.TabIndex = 19
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(438, 32)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(135, 50)
        Me.btnbatal.TabIndex = 13
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnhapus
        '
        Me.btnhapus.Location = New System.Drawing.Point(297, 32)
        Me.btnhapus.Name = "btnhapus"
        Me.btnhapus.Size = New System.Drawing.Size(135, 50)
        Me.btnhapus.TabIndex = 14
        Me.btnhapus.Text = "Hapus"
        Me.btnhapus.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(156, 32)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(135, 50)
        Me.btnedit.TabIndex = 15
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.Location = New System.Drawing.Point(15, 32)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(135, 50)
        Me.btntambah.TabIndex = 16
        Me.btntambah.Text = "Tambah"
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txtkodetransfer
        '
        Me.txtkodetransfer.Enabled = False
        Me.txtkodetransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodetransfer.Location = New System.Drawing.Point(126, 100)
        Me.txtkodetransfer.Name = "txtkodetransfer"
        Me.txtkodetransfer.Size = New System.Drawing.Size(186, 24)
        Me.txtkodetransfer.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kode User"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Kode Transfer"
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(12, 12)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(925, 200)
        Me.GridControl1.TabIndex = 16
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
        Me.GridColumn1.Caption = "Kode Transfer"
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
        Me.GridColumn2.Caption = "Kode Dari Kas"
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
        Me.GridColumn3.Caption = "Kode Ke Kas"
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
        Me.GridColumn4.Caption = "Kode User"
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
        Me.GridColumn5.Caption = "Tanggal Transaksi"
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
        Me.GridColumn6.Caption = "Saldo Kas"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'ftransferkas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(947, 515)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GridControl1)
        Me.MaximizeBox = False
        Me.Name = "ftransferkas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Akun Kas"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnprint As Button
    Friend WithEvents txtketerangantransfer As RichTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtsaldotransfer As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtnamadarikas As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbdarikas As ComboBox
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnhapus As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btntambah As Button
    Friend WithEvents txtkodetransfer As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtnamakekas As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbkekas As ComboBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dttransaksi As DateTimePicker
    Friend WithEvents Label7 As Label
End Class
