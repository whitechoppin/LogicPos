<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class flaporanpricelist
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(flaporanpricelist))
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.btnexcel = New System.Windows.Forms.Button()
        Me.btnrekap = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbpelanggan = New System.Windows.Forms.ComboBox()
        Me.txttelp = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtcustomer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btncaricustomer = New System.Windows.Forms.Button()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsFind.AlwaysVisible = True
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.Caption = "GridColumn1"
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
        Me.GridColumn2.Caption = "GridColumn2"
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
        Me.GridColumn3.Caption = "GridColumn3"
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
        Me.GridColumn4.Caption = "GridColumn4"
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
        Me.GridColumn5.Caption = "GridColumn5"
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
        Me.GridColumn6.Caption = "GridColumn6"
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
        Me.GridColumn7.Caption = "GridColumn7"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Location = New System.Drawing.Point(16, 57)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1035, 518)
        Me.GridControl1.TabIndex = 18
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(1068, 369)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(258, 203)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(334, 31)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Laporan Pricelist Barang"
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(1068, 227)
        Me.btnrefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(258, 37)
        Me.btnrefresh.TabIndex = 18
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'btnexcel
        '
        Me.btnexcel.Location = New System.Drawing.Point(1068, 272)
        Me.btnexcel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Size = New System.Drawing.Size(258, 37)
        Me.btnexcel.TabIndex = 17
        Me.btnexcel.Text = "Convert Excel"
        Me.btnexcel.UseVisualStyleBackColor = True
        '
        'btnrekap
        '
        Me.btnrekap.Location = New System.Drawing.Point(1068, 317)
        Me.btnrekap.Margin = New System.Windows.Forms.Padding(4)
        Me.btnrekap.Name = "btnrekap"
        Me.btnrekap.Size = New System.Drawing.Size(258, 37)
        Me.btnrekap.TabIndex = 10
        Me.btnrekap.Text = "Report"
        Me.btnrekap.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1134, 57)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 20)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Kode Pelanggan"
        '
        'cmbpelanggan
        '
        Me.cmbpelanggan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbpelanggan.FormattingEnabled = True
        Me.cmbpelanggan.Location = New System.Drawing.Point(1068, 83)
        Me.cmbpelanggan.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbpelanggan.MaxLength = 99
        Me.cmbpelanggan.Name = "cmbpelanggan"
        Me.cmbpelanggan.Size = New System.Drawing.Size(214, 26)
        Me.cmbpelanggan.TabIndex = 22
        '
        'txttelp
        '
        Me.txttelp.Enabled = False
        Me.txttelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttelp.Location = New System.Drawing.Point(1068, 193)
        Me.txttelp.Margin = New System.Windows.Forms.Padding(6)
        Me.txttelp.Name = "txttelp"
        Me.txttelp.Size = New System.Drawing.Size(258, 24)
        Me.txttelp.TabIndex = 60
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label22.Location = New System.Drawing.Point(1135, 169)
        Me.Label22.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(134, 18)
        Me.Label22.TabIndex = 59
        Me.Label22.Text = "Telepon Pelanggan"
        '
        'txtcustomer
        '
        Me.txtcustomer.Enabled = False
        Me.txtcustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.Location = New System.Drawing.Point(1068, 139)
        Me.txtcustomer.Margin = New System.Windows.Forms.Padding(6)
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.Size = New System.Drawing.Size(258, 24)
        Me.txtcustomer.TabIndex = 58
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label2.Location = New System.Drawing.Point(1135, 115)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 18)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Nama Pelanggan"
        '
        'btncaricustomer
        '
        Me.btncaricustomer.BackgroundImage = CType(resources.GetObject("btncaricustomer.BackgroundImage"), System.Drawing.Image)
        Me.btncaricustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaricustomer.ImageIndex = 0
        Me.btncaricustomer.Location = New System.Drawing.Point(1287, 82)
        Me.btncaricustomer.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaricustomer.Name = "btncaricustomer"
        Me.btncaricustomer.Size = New System.Drawing.Size(39, 26)
        Me.btncaricustomer.TabIndex = 61
        Me.btncaricustomer.UseVisualStyleBackColor = True
        '
        'flaporanpricelist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1341, 588)
        Me.Controls.Add(Me.btncaricustomer)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnexcel)
        Me.Controls.Add(Me.btnrekap)
        Me.Controls.Add(Me.txttelp)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtcustomer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbpelanggan)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "flaporanpricelist"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Laporan Pricelist"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents Label1 As Label
    Friend WithEvents btnexcel As Button
    Friend WithEvents btnrekap As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnrefresh As Button
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbpelanggan As ComboBox
    Friend WithEvents txttelp As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtcustomer As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btncaricustomer As Button
End Class
