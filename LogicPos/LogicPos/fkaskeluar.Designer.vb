<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkaskeluar
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
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dttransaksi = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbposted = New System.Windows.Forms.CheckBox()
        Me.cbprinted = New System.Windows.Forms.CheckBox()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtsaldokeluar = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtnamakas = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbkas = New System.Windows.Forms.ComboBox()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnhapus = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        Me.txtkodekeluar = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Location = New System.Drawing.Point(13, 80)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1001, 277)
        Me.GridControl1.TabIndex = 16
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.Caption = "Kode Kas Keluar"
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
        Me.GridColumn2.Caption = "Kode Kas"
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
        Me.GridColumn3.Caption = "Tanggal Transaksi"
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
        Me.GridColumn4.Caption = "Saldo Kas"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'dttransaksi
        '
        Me.dttransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dttransaksi.Location = New System.Drawing.Point(587, 369)
        Me.dttransaksi.Margin = New System.Windows.Forms.Padding(6)
        Me.dttransaksi.Name = "dttransaksi"
        Me.dttransaksi.Size = New System.Drawing.Size(284, 24)
        Me.dttransaksi.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(439, 406)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 20)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Saldo Keluar"
        '
        'cbposted
        '
        Me.cbposted.AutoSize = True
        Me.cbposted.Enabled = False
        Me.cbposted.Location = New System.Drawing.Point(915, 462)
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
        Me.cbprinted.Location = New System.Drawing.Point(915, 419)
        Me.cbprinted.Margin = New System.Windows.Forms.Padding(6)
        Me.cbprinted.Name = "cbprinted"
        Me.cbprinted.Size = New System.Drawing.Size(73, 22)
        Me.cbprinted.TabIndex = 40
        Me.cbprinted.Text = "Printed"
        Me.cbprinted.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(864, 13)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(150, 31)
        Me.btnprint.TabIndex = 31
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'txtketerangan
        '
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtketerangan.Location = New System.Drawing.Point(587, 443)
        Me.txtketerangan.Margin = New System.Windows.Forms.Padding(4)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(284, 58)
        Me.txtketerangan.TabIndex = 29
        Me.txtketerangan.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(439, 445)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 20)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Keterangan"
        '
        'txtsaldokeluar
        '
        Me.txtsaldokeluar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsaldokeluar.Location = New System.Drawing.Point(587, 405)
        Me.txtsaldokeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtsaldokeluar.Name = "txtsaldokeluar"
        Me.txtsaldokeluar.Size = New System.Drawing.Size(284, 24)
        Me.txtsaldokeluar.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(439, 372)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(138, 20)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Tanggal Transaksi"
        '
        'txtnamakas
        '
        Me.txtnamakas.Enabled = False
        Me.txtnamakas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamakas.Location = New System.Drawing.Point(156, 477)
        Me.txtnamakas.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnamakas.Name = "txtnamakas"
        Me.txtnamakas.Size = New System.Drawing.Size(265, 24)
        Me.txtnamakas.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 479)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 20)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Nama Kas"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 443)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 20)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Kode Kas"
        '
        'cmbkas
        '
        Me.cmbkas.FormattingEnabled = True
        Me.cmbkas.Location = New System.Drawing.Point(156, 441)
        Me.cmbkas.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbkas.MaxLength = 99
        Me.cmbkas.Name = "cmbkas"
        Me.cmbkas.Size = New System.Drawing.Size(265, 26)
        Me.cmbkas.TabIndex = 20
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(156, 403)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(265, 26)
        Me.cmbsales.TabIndex = 19
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(703, 13)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(4)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(150, 31)
        Me.btnbatal.TabIndex = 13
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnhapus
        '
        Me.btnhapus.Location = New System.Drawing.Point(544, 13)
        Me.btnhapus.Margin = New System.Windows.Forms.Padding(4)
        Me.btnhapus.Name = "btnhapus"
        Me.btnhapus.Size = New System.Drawing.Size(150, 31)
        Me.btnhapus.TabIndex = 14
        Me.btnhapus.Text = "Hapus"
        Me.btnhapus.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(385, 13)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(150, 31)
        Me.btnedit.TabIndex = 15
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.Location = New System.Drawing.Point(226, 13)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(4)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(150, 31)
        Me.btntambah.TabIndex = 16
        Me.btntambah.Text = "Tambah"
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txtkodekeluar
        '
        Me.txtkodekeluar.Enabled = False
        Me.txtkodekeluar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodekeluar.Location = New System.Drawing.Point(156, 369)
        Me.txtkodekeluar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtkodekeluar.Name = "txtkodekeluar"
        Me.txtkodekeluar.Size = New System.Drawing.Size(265, 24)
        Me.txtkodekeluar.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 406)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kode User"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 371)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Kode Kas Keluar"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(-205, -167)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(363, 31)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Akun Keluar (Pembayaran)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(911, 379)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 20)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "Status"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(15, 13)
        Me.Label10.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(156, 31)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "Kas Keluar"
        '
        'fkaskeluar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1027, 522)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cbposted)
        Me.Controls.Add(Me.dttransaksi)
        Me.Controls.Add(Me.cbprinted)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.txtketerangan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtsaldokeluar)
        Me.Controls.Add(Me.txtkodekeluar)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.txtnamakas)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnhapus)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbsales)
        Me.Controls.Add(Me.cmbkas)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fkaskeluar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Akun Kas Keluar"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dttransaksi As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents cbposted As CheckBox
    Friend WithEvents cbprinted As CheckBox
    Friend WithEvents btnprint As Button
    Friend WithEvents txtketerangan As RichTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtsaldokeluar As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtnamakas As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbkas As ComboBox
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnhapus As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btntambah As Button
    Friend WithEvents txtkodekeluar As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label10 As Label
End Class
