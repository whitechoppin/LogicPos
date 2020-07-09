<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ftokosejati
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
        Me.btnsend = New System.Windows.Forms.Button()
        Me.txtemailto = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnsend
        '
        Me.btnsend.Location = New System.Drawing.Point(163, 126)
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Size = New System.Drawing.Size(75, 23)
        Me.btnsend.TabIndex = 0
        Me.btnsend.Text = "Send Email"
        Me.btnsend.UseVisualStyleBackColor = True
        '
        'txtemailto
        '
        Me.txtemailto.Location = New System.Drawing.Point(276, 126)
        Me.txtemailto.Name = "txtemailto"
        Me.txtemailto.Size = New System.Drawing.Size(100, 20)
        Me.txtemailto.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(136, 236)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(273, 69)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "test transactional"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ftokosejati
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(940, 438)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtemailto)
        Me.Controls.Add(Me.btnsend)
        Me.KeyPreview = True
        Me.Name = "ftokosejati"
        Me.Text = "ftokosejati"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnsend As Button
    Friend WithEvents txtemailto As TextBox
    Friend WithEvents Button1 As Button
End Class
