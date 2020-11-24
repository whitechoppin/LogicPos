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
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim Series3 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim ChartTitle1 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle()
        Me.btnsend = New System.Windows.Forms.Button()
        Me.txtemailto = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl()
        Me.DashboardDesigner1 = New DevExpress.DashboardWin.DashboardDesigner()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnsend
        '
        Me.btnsend.Location = New System.Drawing.Point(62, 59)
        Me.btnsend.Margin = New System.Windows.Forms.Padding(4)
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Size = New System.Drawing.Size(112, 32)
        Me.btnsend.TabIndex = 0
        Me.btnsend.Text = "Send Email"
        Me.btnsend.UseVisualStyleBackColor = True
        '
        'txtemailto
        '
        Me.txtemailto.Location = New System.Drawing.Point(232, 59)
        Me.txtemailto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtemailto.Name = "txtemailto"
        Me.txtemailto.Size = New System.Drawing.Size(148, 24)
        Me.txtemailto.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1095, 334)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(211, 96)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "proses chart"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(416, 59)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(148, 24)
        Me.TextBox1.TabIndex = 3
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1095, 13)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(207, 96)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "test transactional"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(1095, 230)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(207, 96)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "region separator"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(1095, 117)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(207, 96)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "CPU ID"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ChartControl1
        '
        XyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.[True]
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.ChartControl1.Diagram = XyDiagram1
        Me.ChartControl1.Location = New System.Drawing.Point(17, 98)
        Me.ChartControl1.Name = "ChartControl1"
        Series1.Name = "Series 1"
        Series2.Name = "Series 2"
        Series3.Name = "Series 3"
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1, Series2, Series3}
        Me.ChartControl1.Size = New System.Drawing.Size(1070, 363)
        Me.ChartControl1.TabIndex = 7
        ChartTitle1.Text = "Laporan Penjualan"
        Me.ChartControl1.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle1})
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
        Me.Controls.Add(Me.ChartControl1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtemailto)
        Me.Controls.Add(Me.btnsend)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ftokosejati"
        Me.Text = "ftokosejati"
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnsend As Button
    Friend WithEvents txtemailto As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents ChartControl1 As DevExpress.XtraCharts.ChartControl
    Friend WithEvents DashboardDesigner1 As DevExpress.DashboardWin.DashboardDesigner
End Class
