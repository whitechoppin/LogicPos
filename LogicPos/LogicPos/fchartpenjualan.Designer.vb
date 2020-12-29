<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fchartpenjualan
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
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim SeriesPoint1 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("0", New Object() {CType(6.9R, Object)})
        Dim SeriesPoint2 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("1", New Object() {CType(2.6R, Object)})
        Dim SeriesPoint3 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("2", New Object() {CType(7.9R, Object)})
        Dim SeriesPoint4 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("3", New Object() {CType(7.2R, Object)})
        Dim SeriesPoint5 As DevExpress.XtraCharts.SeriesPoint = New DevExpress.XtraCharts.SeriesPoint("4", New Object() {CType(6.3R, Object)})
        Dim ChartTitle1 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fchartpenjualan))
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.cmbpelanggan = New System.Windows.Forms.ComboBox()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btncaricustomer = New System.Windows.Forms.Button()
        Me.btncarigudang = New System.Windows.Forms.Button()
        Me.cmbgudang = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.btnexcel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbtahunan = New System.Windows.Forms.RadioButton()
        Me.rbbulanan = New System.Windows.Forms.RadioButton()
        Me.rbharian = New System.Windows.Forms.RadioButton()
        Me.btncaribarang = New System.Windows.Forms.Button()
        Me.txtkodestok = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtnamabarang = New System.Windows.Forms.TextBox()
        Me.txtnamapelanggan = New System.Windows.Forms.TextBox()
        Me.txtnamagudang = New System.Windows.Forms.TextBox()
        Me.txtnamasales = New System.Windows.Forms.TextBox()
        Me.btnprint = New System.Windows.Forms.Button()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChartControl1
        '
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.ChartControl1.Diagram = XyDiagram1
        Me.ChartControl1.Location = New System.Drawing.Point(21, 201)
        Me.ChartControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.ChartControl1.Name = "ChartControl1"
        Series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series1.Name = "Penjualan"
        Series1.Points.AddRange(New DevExpress.XtraCharts.SeriesPoint() {SeriesPoint1, SeriesPoint2, SeriesPoint3, SeriesPoint4, SeriesPoint5})
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1}
        Me.ChartControl1.Size = New System.Drawing.Size(1358, 347)
        Me.ChartControl1.TabIndex = 1
        ChartTitle1.Text = "Periode :"
        Me.ChartControl1.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle1})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(327, 63)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 16)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Sampai Dengan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(223, 31)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Chart Penjualan"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(454, 59)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(231, 24)
        Me.DateTimePicker2.TabIndex = 19
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(88, 59)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(231, 24)
        Me.DateTimePicker1.TabIndex = 18
        '
        'cmbpelanggan
        '
        Me.cmbpelanggan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbpelanggan.FormattingEnabled = True
        Me.cmbpelanggan.Location = New System.Drawing.Point(587, 127)
        Me.cmbpelanggan.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbpelanggan.MaxLength = 99
        Me.cmbpelanggan.Name = "cmbpelanggan"
        Me.cmbpelanggan.Size = New System.Drawing.Size(239, 26)
        Me.cmbpelanggan.TabIndex = 39
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(21, 126)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(268, 26)
        Me.cmbsales.TabIndex = 40
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label16.Location = New System.Drawing.Point(18, 102)
        Me.Label16.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 18)
        Me.Label16.TabIndex = 42
        Me.Label16.Text = "Kode Sales"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label6.Location = New System.Drawing.Point(584, 103)
        Me.Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 18)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Kode Pelanggan"
        '
        'btncaricustomer
        '
        Me.btncaricustomer.BackgroundImage = CType(resources.GetObject("btncaricustomer.BackgroundImage"), System.Drawing.Image)
        Me.btncaricustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaricustomer.ImageIndex = 0
        Me.btncaricustomer.Location = New System.Drawing.Point(826, 126)
        Me.btncaricustomer.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaricustomer.Name = "btncaricustomer"
        Me.btncaricustomer.Size = New System.Drawing.Size(29, 28)
        Me.btncaricustomer.TabIndex = 38
        Me.btncaricustomer.UseVisualStyleBackColor = True
        '
        'btncarigudang
        '
        Me.btncarigudang.BackgroundImage = CType(resources.GetObject("btncarigudang.BackgroundImage"), System.Drawing.Image)
        Me.btncarigudang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarigudang.ImageIndex = 0
        Me.btncarigudang.Location = New System.Drawing.Point(545, 125)
        Me.btncarigudang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncarigudang.Name = "btncarigudang"
        Me.btncarigudang.Size = New System.Drawing.Size(29, 28)
        Me.btncarigudang.TabIndex = 44
        Me.btncarigudang.UseVisualStyleBackColor = True
        '
        'cmbgudang
        '
        Me.cmbgudang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbgudang.FormattingEnabled = True
        Me.cmbgudang.Location = New System.Drawing.Point(306, 126)
        Me.cmbgudang.Margin = New System.Windows.Forms.Padding(6)
        Me.cmbgudang.MaxLength = 99
        Me.cmbgudang.Name = "cmbgudang"
        Me.cmbgudang.Size = New System.Drawing.Size(236, 26)
        Me.cmbgudang.TabIndex = 43
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label14.Location = New System.Drawing.Point(303, 102)
        Me.Label14.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 18)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "Kode Gudang"
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(1213, 39)
        Me.btnrefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(166, 46)
        Me.btnrefresh.TabIndex = 48
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'btnexcel
        '
        Me.btnexcel.Location = New System.Drawing.Point(1213, 93)
        Me.btnexcel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Size = New System.Drawing.Size(166, 46)
        Me.btnexcel.TabIndex = 49
        Me.btnexcel.Text = "Export Excel"
        Me.btnexcel.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(18, 61)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 18)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Tanggal"
        '
        'rbtahunan
        '
        Me.rbtahunan.AutoSize = True
        Me.rbtahunan.Location = New System.Drawing.Point(861, 59)
        Me.rbtahunan.Name = "rbtahunan"
        Me.rbtahunan.Size = New System.Drawing.Size(83, 22)
        Me.rbtahunan.TabIndex = 57
        Me.rbtahunan.TabStop = True
        Me.rbtahunan.Text = "Tahunan"
        Me.rbtahunan.UseVisualStyleBackColor = True
        '
        'rbbulanan
        '
        Me.rbbulanan.AutoSize = True
        Me.rbbulanan.Location = New System.Drawing.Point(776, 59)
        Me.rbbulanan.Name = "rbbulanan"
        Me.rbbulanan.Size = New System.Drawing.Size(79, 22)
        Me.rbbulanan.TabIndex = 56
        Me.rbbulanan.TabStop = True
        Me.rbbulanan.Text = "Bulanan"
        Me.rbbulanan.UseVisualStyleBackColor = True
        '
        'rbharian
        '
        Me.rbharian.AutoSize = True
        Me.rbharian.Location = New System.Drawing.Point(701, 59)
        Me.rbharian.Name = "rbharian"
        Me.rbharian.Size = New System.Drawing.Size(69, 22)
        Me.rbharian.TabIndex = 55
        Me.rbharian.TabStop = True
        Me.rbharian.Text = "Harian"
        Me.rbharian.UseVisualStyleBackColor = True
        '
        'btncaribarang
        '
        Me.btncaribarang.BackgroundImage = CType(resources.GetObject("btncaribarang.BackgroundImage"), System.Drawing.Image)
        Me.btncaribarang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncaribarang.ImageIndex = 0
        Me.btncaribarang.Location = New System.Drawing.Point(1063, 126)
        Me.btncaribarang.Margin = New System.Windows.Forms.Padding(6)
        Me.btncaribarang.Name = "btncaribarang"
        Me.btncaribarang.Size = New System.Drawing.Size(29, 28)
        Me.btncaribarang.TabIndex = 58
        Me.btncaribarang.UseVisualStyleBackColor = True
        '
        'txtkodestok
        '
        Me.txtkodestok.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtkodestok.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodestok.Location = New System.Drawing.Point(861, 128)
        Me.txtkodestok.Margin = New System.Windows.Forms.Padding(6)
        Me.txtkodestok.Name = "txtkodestok"
        Me.txtkodestok.Size = New System.Drawing.Size(200, 24)
        Me.txtkodestok.TabIndex = 60
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label4.Location = New System.Drawing.Point(864, 104)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 18)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Kode Stok"
        '
        'txtnamabarang
        '
        Me.txtnamabarang.Enabled = False
        Me.txtnamabarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnamabarang.Location = New System.Drawing.Point(861, 164)
        Me.txtnamabarang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnamabarang.Name = "txtnamabarang"
        Me.txtnamabarang.Size = New System.Drawing.Size(231, 24)
        Me.txtnamabarang.TabIndex = 61
        '
        'txtnamapelanggan
        '
        Me.txtnamapelanggan.Enabled = False
        Me.txtnamapelanggan.Location = New System.Drawing.Point(587, 164)
        Me.txtnamapelanggan.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnamapelanggan.Name = "txtnamapelanggan"
        Me.txtnamapelanggan.Size = New System.Drawing.Size(268, 24)
        Me.txtnamapelanggan.TabIndex = 62
        '
        'txtnamagudang
        '
        Me.txtnamagudang.Enabled = False
        Me.txtnamagudang.Location = New System.Drawing.Point(306, 164)
        Me.txtnamagudang.Margin = New System.Windows.Forms.Padding(6)
        Me.txtnamagudang.Name = "txtnamagudang"
        Me.txtnamagudang.Size = New System.Drawing.Size(268, 24)
        Me.txtnamagudang.TabIndex = 63
        '
        'txtnamasales
        '
        Me.txtnamasales.Enabled = False
        Me.txtnamasales.Location = New System.Drawing.Point(21, 164)
        Me.txtnamasales.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnamasales.Name = "txtnamasales"
        Me.txtnamasales.Size = New System.Drawing.Size(268, 24)
        Me.txtnamasales.TabIndex = 64
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(1213, 147)
        Me.btnprint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(166, 46)
        Me.btnprint.TabIndex = 65
        Me.btnprint.Text = "Print Chart"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'fchartpenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1396, 561)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.txtnamasales)
        Me.Controls.Add(Me.txtnamagudang)
        Me.Controls.Add(Me.txtnamapelanggan)
        Me.Controls.Add(Me.txtnamabarang)
        Me.Controls.Add(Me.btncaribarang)
        Me.Controls.Add(Me.txtkodestok)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.rbtahunan)
        Me.Controls.Add(Me.rbbulanan)
        Me.Controls.Add(Me.rbharian)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.btnexcel)
        Me.Controls.Add(Me.btncarigudang)
        Me.Controls.Add(Me.cmbgudang)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cmbpelanggan)
        Me.Controls.Add(Me.cmbsales)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btncaricustomer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ChartControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fchartpenjualan"
        Me.Text = "Chart Penjualan"
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChartControl1 As DevExpress.XtraCharts.ChartControl
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents cmbpelanggan As ComboBox
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btncaricustomer As Button
    Friend WithEvents btncarigudang As Button
    Friend WithEvents cmbgudang As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents btnrefresh As Button
    Friend WithEvents btnexcel As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents rbtahunan As RadioButton
    Friend WithEvents rbbulanan As RadioButton
    Friend WithEvents rbharian As RadioButton
    Friend WithEvents btncaribarang As Button
    Friend WithEvents txtkodestok As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtnamabarang As TextBox
    Friend WithEvents txtnamapelanggan As TextBox
    Friend WithEvents txtnamagudang As TextBox
    Friend WithEvents txtnamasales As TextBox
    Friend WithEvents btnprint As Button
End Class
