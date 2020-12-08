<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fchartpelunasanutang
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
        Dim XyDiagram2 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim SeriesPoint6 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("0", New Object() {CType(6.9R, Object)})
        Dim SeriesPoint7 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("1", New Object() {CType(2.6R, Object)})
        Dim SeriesPoint8 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("2", New Object() {CType(7.9R, Object)})
        Dim SeriesPoint9 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("3", New Object() {CType(7.2R, Object)})
        Dim SeriesPoint10 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("4", New Object() {CType(6.3R, Object)})
        Dim ChartTitle2 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle()
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl()
        Me.rbtahunan = New System.Windows.Forms.RadioButton()
        Me.rbbulanan = New System.Windows.Forms.RadioButton()
        Me.rbharian = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChartControl1
        '
        XyDiagram2.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram2.AxisY.VisibleInPanesSerializable = "-1"
        Me.ChartControl1.Diagram = XyDiagram2
        Me.ChartControl1.Location = New System.Drawing.Point(14, 231)
        Me.ChartControl1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ChartControl1.Name = "ChartControl1"
        Series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series2.Name = "Pembelian"
        Series2.Points.AddRange(New DevExpress.XtraCharts.SeriesPoint() {SeriesPoint6, SeriesPoint7, SeriesPoint8, SeriesPoint9, SeriesPoint10})
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series2}
        Me.ChartControl1.Size = New System.Drawing.Size(1520, 532)
        Me.ChartControl1.TabIndex = 1
        ChartTitle2.Text = "Periode :"
        Me.ChartControl1.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle2})
        '
        'rbtahunan
        '
        Me.rbtahunan.AutoSize = True
        Me.rbtahunan.Location = New System.Drawing.Point(886, 63)
        Me.rbtahunan.Name = "rbtahunan"
        Me.rbtahunan.Size = New System.Drawing.Size(83, 22)
        Me.rbtahunan.TabIndex = 62
        Me.rbtahunan.TabStop = True
        Me.rbtahunan.Text = "Tahunan"
        Me.rbtahunan.UseVisualStyleBackColor = True
        '
        'rbbulanan
        '
        Me.rbbulanan.AutoSize = True
        Me.rbbulanan.Location = New System.Drawing.Point(801, 63)
        Me.rbbulanan.Name = "rbbulanan"
        Me.rbbulanan.Size = New System.Drawing.Size(79, 22)
        Me.rbbulanan.TabIndex = 61
        Me.rbbulanan.TabStop = True
        Me.rbbulanan.Text = "Bulanan"
        Me.rbbulanan.UseVisualStyleBackColor = True
        '
        'rbharian
        '
        Me.rbharian.AutoSize = True
        Me.rbharian.Location = New System.Drawing.Point(726, 63)
        Me.rbharian.Name = "rbharian"
        Me.rbharian.Size = New System.Drawing.Size(69, 22)
        Me.rbharian.TabIndex = 60
        Me.rbharian.TabStop = True
        Me.rbharian.Text = "Harian"
        Me.rbharian.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(15, 59)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 18)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Tanggal"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(324, 61)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 16)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Sampai Dengan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(230, 31)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Chart Pembelian"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(451, 57)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(231, 24)
        Me.DateTimePicker2.TabIndex = 56
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(85, 57)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(231, 24)
        Me.DateTimePicker1.TabIndex = 55
        '
        'fchartpelunasanutang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1554, 777)
        Me.Controls.Add(Me.rbtahunan)
        Me.Controls.Add(Me.rbbulanan)
        Me.Controls.Add(Me.rbharian)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ChartControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "fchartpelunasanutang"
        Me.Text = "fchartpelunasanutang"
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChartControl1 As DevExpress.XtraCharts.ChartControl
    Friend WithEvents rbtahunan As RadioButton
    Friend WithEvents rbbulanan As RadioButton
    Friend WithEvents rbharian As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
