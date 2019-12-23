<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fretbeli
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
        Me.btnok = New System.Windows.Forms.Button()
        Me.txtretur = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblnamaibarang = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnok
        '
        Me.btnok.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Location = New System.Drawing.Point(118, 107)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(100, 26)
        Me.btnok.TabIndex = 6
        Me.btnok.Text = "OK"
        Me.btnok.UseVisualStyleBackColor = True
        '
        'txtretur
        '
        Me.txtretur.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtretur.Location = New System.Drawing.Point(118, 77)
        Me.txtretur.MaxLength = 12
        Me.txtretur.Name = "txtretur"
        Me.txtretur.Size = New System.Drawing.Size(100, 24)
        Me.txtretur.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Jumlah Retur"
        '
        'lblnamaibarang
        '
        Me.lblnamaibarang.AutoSize = True
        Me.lblnamaibarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnamaibarang.Location = New System.Drawing.Point(39, 28)
        Me.lblnamaibarang.Name = "lblnamaibarang"
        Me.lblnamaibarang.Size = New System.Drawing.Size(51, 18)
        Me.lblnamaibarang.TabIndex = 4
        Me.lblnamaibarang.Text = "Label1"
        '
        'fretbeli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(250, 160)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.txtretur)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblnamaibarang)
        Me.Name = "fretbeli"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jumlah Retur"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnok As Button
    Friend WithEvents txtretur As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblnamaibarang As Label
End Class
