<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frekeningsupplier
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtnamarek = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtnorek = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtnamabank = New System.Windows.Forms.TextBox()
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.btnhapus = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btntambah = New System.Windows.Forms.Button()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridControl1.Location = New System.Drawing.Point(13, 28)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(507, 218)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "GridColumn3"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GridControl1)
        Me.GroupControl1.Location = New System.Drawing.Point(271, 82)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(527, 252)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "List Rekening"
        '
        'txtnamarek
        '
        Me.txtnamarek.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamarek.Location = New System.Drawing.Point(16, 113)
        Me.txtnamarek.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtnamarek.Name = "txtnamarek"
        Me.txtnamarek.Size = New System.Drawing.Size(245, 29)
        Me.txtnamarek.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 87)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(159, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nama Rekening"
        '
        'txtnorek
        '
        Me.txtnorek.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnorek.Location = New System.Drawing.Point(16, 183)
        Me.txtnorek.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtnorek.Name = "txtnorek"
        Me.txtnorek.Size = New System.Drawing.Size(245, 29)
        Me.txtnorek.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 158)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "No. Rekening"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(16, 230)
        Me.label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(116, 24)
        Me.label3.TabIndex = 3
        Me.label3.Text = "Nama Bank"
        '
        'txtnamabank
        '
        Me.txtnamabank.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabank.Location = New System.Drawing.Point(16, 256)
        Me.txtnamabank.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtnamabank.Name = "txtnamabank"
        Me.txtnamabank.Size = New System.Drawing.Size(245, 29)
        Me.txtnamabank.TabIndex = 2
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(411, 15)
        Me.btnbatal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(189, 60)
        Me.btnbatal.TabIndex = 17
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'btnhapus
        '
        Me.btnhapus.Location = New System.Drawing.Point(608, 15)
        Me.btnhapus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnhapus.Name = "btnhapus"
        Me.btnhapus.Size = New System.Drawing.Size(189, 60)
        Me.btnhapus.TabIndex = 18
        Me.btnhapus.Text = "Hapus"
        Me.btnhapus.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(213, 15)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(189, 60)
        Me.btnedit.TabIndex = 19
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btntambah
        '
        Me.btntambah.Location = New System.Drawing.Point(16, 15)
        Me.btntambah.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btntambah.Name = "btntambah"
        Me.btntambah.Size = New System.Drawing.Size(189, 60)
        Me.btntambah.TabIndex = 20
        Me.btntambah.Text = "Tambah"
        Me.btntambah.UseVisualStyleBackColor = True
        '
        'frekeningsupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(808, 347)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnhapus)
        Me.Controls.Add(Me.txtnamabank)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.txtnorek)
        Me.Controls.Add(Me.btntambah)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtnamarek)
        Me.Controls.Add(Me.GroupControl1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frekeningsupplier"
        Me.Text = "Data Rekening Supplier"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtnamarek As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtnorek As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents label3 As Label
    Friend WithEvents txtnamabank As TextBox
    Friend WithEvents btnbatal As Button
    Friend WithEvents btnhapus As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btntambah As Button
End Class
