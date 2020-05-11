<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgudang
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
        Me.txtketerangan = New System.Windows.Forms.RichTextBox()
        Me.txtalamat = New System.Windows.Forms.RichTextBox()
        Me.btngenerate = New System.Windows.Forms.Button()
        Me.btnauto = New System.Windows.Forms.Button()
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
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtketerangan
        '
        Me.txtketerangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtketerangan.Location = New System.Drawing.Point(497, 146)
        Me.txtketerangan.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtketerangan.Name = "txtketerangan"
        Me.txtketerangan.Size = New System.Drawing.Size(277, 192)
        Me.txtketerangan.TabIndex = 17
        Me.txtketerangan.Text = ""
        '
        'txtalamat
        '
        Me.txtalamat.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtalamat.Location = New System.Drawing.Point(122, 222)
        Me.txtalamat.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtalamat.Name = "txtalamat"
        Me.txtalamat.Size = New System.Drawing.Size(270, 116)
        Me.txtalamat.TabIndex = 17
        Me.txtalamat.Text = ""
        '
        'btngenerate
        '
        Me.btngenerate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngenerate.Location = New System.Drawing.Point(263, 146)
        Me.btngenerate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btngenerate.Name = "btngenerate"
        Me.btngenerate.Size = New System.Drawing.Size(129, 37)
        Me.btngenerate.TabIndex = 28
        Me.btngenerate.Text = "Generate"
        Me.btngenerate.UseVisualStyleBackColor = True
        '
        'btnauto
        '
        Me.btnauto.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnauto.Location = New System.Drawing.Point(122, 146)
        Me.btnauto.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnauto.Name = "btnauto"
        Me.btnauto.Size = New System.Drawing.Size(129, 37)
        Me.btnauto.TabIndex = 27
        Me.btnauto.Text = "Auto"
        Me.btnauto.UseVisualStyleBackColor = True
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(591, 51)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(183, 55)
        Me.btnbatal.TabIndex = 13
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnhapus
        '
        Me.btnhapus.Location = New System.Drawing.Point(400, 51)
        Me.btnhapus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnhapus.Name = "btnhapus"
        Me.btnhapus.Size = New System.Drawing.Size(183, 55)
        Me.btnhapus.TabIndex = 14
        Me.btnhapus.Text = "Hapus"
        Me.btnhapus.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(209, 51)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(183, 55)
        Me.btnedit.TabIndex = 15
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.Location = New System.Drawing.Point(18, 51)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(183, 55)
        Me.btntambah.TabIndex = 16
        Me.btntambah.Text = "Tambah"
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'txttelp
        '
        Me.txttelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttelp.Location = New System.Drawing.Point(497, 114)
        Me.txttelp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txttelp.Name = "txttelp"
        Me.txttelp.Size = New System.Drawing.Size(277, 24)
        Me.txttelp.TabIndex = 2
        '
        'txtnama
        '
        Me.txtnama.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnama.Location = New System.Drawing.Point(122, 190)
        Me.txtnama.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtnama.Name = "txtnama"
        Me.txtnama.Size = New System.Drawing.Size(270, 24)
        Me.txtnama.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(407, 155)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 18)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Keterangan"
        '
        'txtkode
        '
        Me.txtkode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkode.Location = New System.Drawing.Point(122, 114)
        Me.txtkode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtkode.Name = "txtkode"
        Me.txtkode.Size = New System.Drawing.Size(270, 24)
        Me.txtkode.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 225)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 18)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Alamat"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(422, 117)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 18)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Telepon"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 193)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Nama Gudang"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 117)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 18)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Kode Gudang"
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
        'GridControl
        '
        Me.GridControl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridControl.Location = New System.Drawing.Point(13, 361)
        Me.GridControl.MainView = Me.GridView
        Me.GridControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridControl.Name = "GridControl"
        Me.GridControl.Size = New System.Drawing.Size(761, 374)
        Me.GridControl.TabIndex = 15
        Me.GridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView})
        '
        'GridView
        '
        Me.GridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.GridView.GridControl = Me.GridControl
        Me.GridView.Name = "GridView"
        Me.GridView.OptionsBehavior.Editable = False
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 9)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(164, 29)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Data Gudang"
        '
        'fgudang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(790, 751)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btngenerate)
        Me.Controls.Add(Me.btnauto)
        Me.Controls.Add(Me.GridControl)
        Me.Controls.Add(Me.txtalamat)
        Me.Controls.Add(Me.txtketerangan)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnhapus)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.txtnama)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtkode)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txttelp)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "fgudang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Gudang"
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btngenerate As Button
    Friend WithEvents btnauto As Button
    Friend WithEvents Label6 As Label
End Class
