<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpersentasepajak
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
        Me.btnbatal = New System.Windows.Forms.Button()
        Me.txtpersen = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnbatal
        '
        Me.btnbatal.Location = New System.Drawing.Point(425, 14)
        Me.btnbatal.Name = "btnbatal"
        Me.btnbatal.Size = New System.Drawing.Size(77, 22)
        Me.btnbatal.TabIndex = 19
        Me.btnbatal.Text = "Batal"
        Me.btnbatal.UseVisualStyleBackColor = True
        '
        'txtpersen
        '
        Me.txtpersen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpersen.Location = New System.Drawing.Point(122, 14)
        Me.txtpersen.Name = "txtpersen"
        Me.txtpersen.Size = New System.Drawing.Size(217, 22)
        Me.txtpersen.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 18)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Persentase %"
        '
        'btnsimpan
        '
        Me.btnsimpan.Location = New System.Drawing.Point(345, 14)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(74, 22)
        Me.btnsimpan.TabIndex = 20
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'fpersentasepajak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(520, 54)
        Me.Controls.Add(Me.btnbatal)
        Me.Controls.Add(Me.txtpersen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnsimpan)
        Me.Name = "fpersentasepajak"
        Me.Text = "Persentase Pajak"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnbatal As Button
    Friend WithEvents txtpersen As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnsimpan As Button
End Class
