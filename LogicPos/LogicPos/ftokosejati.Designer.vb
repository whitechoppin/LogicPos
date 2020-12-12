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
        Me.DashboardDesigner1 = New DevExpress.DashboardWin.DashboardDesigner()
        Me.SuspendLayout()
        '
        'DashboardDesigner1
        '
        Me.DashboardDesigner1.CustomDBSchemaProvider = Nothing
        Me.DashboardDesigner1.Location = New System.Drawing.Point(570, 483)
        Me.DashboardDesigner1.Name = "DashboardDesigner1"
        Me.DashboardDesigner1.PrintingOptions.DocumentContentOptions.FilterState = DevExpress.DashboardWin.DashboardPrintingFilterState.SeparatePage
        Me.DashboardDesigner1.PrintingOptions.FontInfo.GdiCharSet = CType(0, Byte)
        Me.DashboardDesigner1.PrintingOptions.FontInfo.Name = Nothing
        Me.DashboardDesigner1.Size = New System.Drawing.Size(744, 131)
        Me.DashboardDesigner1.TabIndex = 8
        '
        'ftokosejati
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1326, 626)
        Me.Controls.Add(Me.DashboardDesigner1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ftokosejati"
        Me.Text = "ftokosejati"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DashboardDesigner1 As DevExpress.DashboardWin.DashboardDesigner
End Class
