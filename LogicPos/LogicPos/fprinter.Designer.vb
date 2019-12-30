<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fprinter
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbstruk = New System.Windows.Forms.ComboBox()
        Me.cmbfaktur = New System.Windows.Forms.ComboBox()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Printer Struk"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Printer Faktur"
        '
        'cmbstruk
        '
        Me.cmbstruk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbstruk.FormattingEnabled = True
        Me.cmbstruk.Location = New System.Drawing.Point(134, 19)
        Me.cmbstruk.Name = "cmbstruk"
        Me.cmbstruk.Size = New System.Drawing.Size(195, 21)
        Me.cmbstruk.TabIndex = 2
        '
        'cmbfaktur
        '
        Me.cmbfaktur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbfaktur.FormattingEnabled = True
        Me.cmbfaktur.Location = New System.Drawing.Point(134, 46)
        Me.cmbfaktur.Name = "cmbfaktur"
        Me.cmbfaktur.Size = New System.Drawing.Size(195, 21)
        Me.cmbfaktur.TabIndex = 2
        '
        'btnsimpan
        '
        Me.btnsimpan.Location = New System.Drawing.Point(344, 44)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btnsimpan.TabIndex = 3
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'fprinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(455, 90)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.cmbfaktur)
        Me.Controls.Add(Me.cmbstruk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "fprinter"
        Me.Text = "Printer Setting"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbstruk As ComboBox
    Friend WithEvents cmbfaktur As ComboBox
    Friend WithEvents btnsimpan As Button
End Class
