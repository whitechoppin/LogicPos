<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgeneratebarcode
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
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.picBarcode = New System.Windows.Forms.PictureBox()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.cbBarcodeType = New System.Windows.Forms.ComboBox()
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(306, 352)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 32)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(168, 320)
        Me.txtInput.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(228, 24)
        Me.txtInput.TabIndex = 17
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(208, 352)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(90, 32)
        Me.btnPrint.TabIndex = 16
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(110, 351)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(90, 32)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(12, 351)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(90, 32)
        Me.btnGenerate.TabIndex = 14
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'picBarcode
        '
        Me.picBarcode.BackColor = System.Drawing.SystemColors.Control
        Me.picBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picBarcode.Location = New System.Drawing.Point(12, 13)
        Me.picBarcode.Margin = New System.Windows.Forms.Padding(4)
        Me.picBarcode.Name = "picBarcode"
        Me.picBarcode.Size = New System.Drawing.Size(384, 290)
        Me.picBarcode.TabIndex = 13
        Me.picBarcode.TabStop = False
        '
        'cbBarcodeType
        '
        Me.cbBarcodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBarcodeType.FormattingEnabled = True
        Me.cbBarcodeType.Items.AddRange(New Object() {"Barcode", "QR Code"})
        Me.cbBarcodeType.Location = New System.Drawing.Point(12, 318)
        Me.cbBarcodeType.Name = "cbBarcodeType"
        Me.cbBarcodeType.Size = New System.Drawing.Size(149, 26)
        Me.cbBarcodeType.TabIndex = 19
        '
        'fgeneratebarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(405, 397)
        Me.Controls.Add(Me.cbBarcodeType)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtInput)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.picBarcode)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fgeneratebarcode"
        Me.Text = "Generate Barcode"
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents txtInput As TextBox
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnGenerate As Button
    Friend WithEvents picBarcode As PictureBox
    Friend WithEvents sfd As SaveFileDialog
    Friend WithEvents cbBarcodeType As ComboBox
End Class
