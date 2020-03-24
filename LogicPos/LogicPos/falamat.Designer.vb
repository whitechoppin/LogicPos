<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class falamat
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtalamat = New System.Windows.Forms.RichTextBox()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.txttelepon = New System.Windows.Forms.TextBox()
        Me.txtrekening = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label7.Location = New System.Drawing.Point(27, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 18)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Alamat :"
        '
        'txtalamat
        '
        Me.txtalamat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtalamat.Location = New System.Drawing.Point(97, 21)
        Me.txtalamat.Name = "txtalamat"
        Me.txtalamat.Size = New System.Drawing.Size(529, 57)
        Me.txtalamat.TabIndex = 40
        Me.txtalamat.Text = ""
        '
        'btnsimpan
        '
        Me.btnsimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsimpan.Location = New System.Drawing.Point(635, 21)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(115, 57)
        Me.btnsimpan.TabIndex = 42
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'txttelepon
        '
        Me.txttelepon.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttelepon.Location = New System.Drawing.Point(97, 85)
        Me.txttelepon.Margin = New System.Windows.Forms.Padding(4)
        Me.txttelepon.Name = "txttelepon"
        Me.txttelepon.Size = New System.Drawing.Size(529, 24)
        Me.txttelepon.TabIndex = 43
        '
        'txtrekening
        '
        Me.txtrekening.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrekening.Location = New System.Drawing.Point(97, 117)
        Me.txtrekening.Margin = New System.Windows.Forms.Padding(4)
        Me.txtrekening.Name = "txtrekening"
        Me.txtrekening.Size = New System.Drawing.Size(529, 24)
        Me.txtrekening.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label1.Location = New System.Drawing.Point(19, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 18)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Telepon :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label2.Location = New System.Drawing.Point(11, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 18)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Rekening :"
        '
        'btncancel
        '
        Me.btncancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Location = New System.Drawing.Point(635, 84)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(115, 57)
        Me.btncancel.TabIndex = 47
        Me.btncancel.Text = "Cancel"
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'falamat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(767, 165)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtrekening)
        Me.Controls.Add(Me.txttelepon)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtalamat)
        Me.Name = "falamat"
        Me.Text = "Alamat Perusahaan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label7 As Label
    Friend WithEvents txtalamat As RichTextBox
    Friend WithEvents btnsimpan As Button
    Friend WithEvents txttelepon As TextBox
    Friend WithEvents txtrekening As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btncancel As Button
End Class
