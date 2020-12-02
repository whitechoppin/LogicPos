<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fchartpembelian
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fchartpembelian))
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.cmbsales = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbsupplier = New System.Windows.Forms.ComboBox()
        Me.btncarisupplier = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbgudang = New System.Windows.Forms.ComboBox()
        Me.btncarigudang = New System.Windows.Forms.Button()
        Me.btntabel = New System.Windows.Forms.Button()
        Me.btnexcel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbharian = New System.Windows.Forms.RadioButton()
        Me.rbbulanan = New System.Windows.Forms.RadioButton()
        Me.rbtahunan = New System.Windows.Forms.RadioButton()
        Me.btncari = New System.Windows.Forms.Button()
        Me.txtkodebarang = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
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
        Me.ChartControl1.Location = New System.Drawing.Point(13, 164)
        Me.ChartControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.ChartControl1.Name = "ChartControl1"
        Series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series1.Name = "Pembelian"
        Series1.Points.AddRange(New DevExpress.XtraCharts.SeriesPoint() {SeriesPoint1, SeriesPoint2, SeriesPoint3, SeriesPoint4, SeriesPoint5})
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1}
        Me.ChartControl1.Size = New System.Drawing.Size(1358, 384)
        Me.ChartControl1.TabIndex = 0
        ChartTitle1.Text = "Periode :"
        Me.ChartControl1.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle1})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(324, 61)
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
        Me.Label2.Size = New System.Drawing.Size(230, 31)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Chart Pembelian"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(451, 57)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(231, 24)
        Me.DateTimePicker2.TabIndex = 19
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(85, 57)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(231, 24)
        Me.DateTimePicker1.TabIndex = 18
        '
        'cmbsales
        '
        Me.cmbsales.FormattingEnabled = True
        Me.cmbsales.Location = New System.Drawing.Point(21, 126)
        Me.cmbsales.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbsales.MaxLength = 99
        Me.cmbsales.Name = "cmbsales"
        Me.cmbsales.Size = New System.Drawing.Size(268, 26)
        Me.cmbsales.TabIndex = 41
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(302, 102)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 18)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Kode Supplier"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(18, 102)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 18)
        Me.Label16.TabIndex = 42
        Me.Label16.Text = "Kode Sales"
        '
        'cmbsupplier
        '
        Me.cmbsupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsupplier.FormattingEnabled = True
        Me.cmbsupplier.Location = New System.Drawing.Point(305, 126)
        Me.cmbsupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbsupplier.MaxLength = 99
        Me.cmbsupplier.Name = "cmbsupplier"
        Me.cmbsupplier.Size = New System.Drawing.Size(239, 26)
        Me.cmbsupplier.TabIndex = 39
        '
        'btncarisupplier
        '
        Me.btncarisupplier.BackgroundImage = CType(resources.GetObject("btncarisupplier.BackgroundImage"), System.Drawing.Image)
        Me.btncarisupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarisupplier.ImageIndex = 0
        Me.btncarisupplier.Location = New System.Drawing.Point(544, 125)
        Me.btncarisupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.btncarisupplier.Name = "btncarisupplier"
        Me.btncarisupplier.Size = New System.Drawing.Size(29, 28)
        Me.btncarisupplier.TabIndex = 40
        Me.btncarisupplier.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(587, 102)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 18)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Kode Gudang"
        '
        'cmbgudang
        '
        Me.cmbgudang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbgudang.FormattingEnabled = True
        Me.cmbgudang.Location = New System.Drawing.Point(590, 125)
        Me.cmbgudang.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbgudang.MaxLength = 99
        Me.cmbgudang.Name = "cmbgudang"
        Me.cmbgudang.Size = New System.Drawing.Size(239, 26)
        Me.cmbgudang.TabIndex = 43
        '
        'btncarigudang
        '
        Me.btncarigudang.BackgroundImage = CType(resources.GetObject("btncarigudang.BackgroundImage"), System.Drawing.Image)
        Me.btncarigudang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncarigudang.ImageIndex = 0
        Me.btncarigudang.Location = New System.Drawing.Point(829, 124)
        Me.btncarigudang.Margin = New System.Windows.Forms.Padding(4)
        Me.btncarigudang.Name = "btncarigudang"
        Me.btncarigudang.Size = New System.Drawing.Size(29, 28)
        Me.btncarigudang.TabIndex = 44
        Me.btncarigudang.UseVisualStyleBackColor = True
        '
        'btntabel
        '
        Me.btntabel.Location = New System.Drawing.Point(1205, 56)
        Me.btntabel.Margin = New System.Windows.Forms.Padding(4)
        Me.btntabel.Name = "btntabel"
        Me.btntabel.Size = New System.Drawing.Size(166, 46)
        Me.btntabel.TabIndex = 46
        Me.btntabel.Text = "Refresh Tabel"
        Me.btntabel.UseVisualStyleBackColor = True
        '
        'btnexcel
        '
        Me.btnexcel.Location = New System.Drawing.Point(1205, 110)
        Me.btnexcel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Size = New System.Drawing.Size(166, 46)
        Me.btnexcel.TabIndex = 47
        Me.btnexcel.Text = "Export Excel"
        Me.btnexcel.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.Label3.Location = New System.Drawing.Point(15, 59)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 18)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Tanggal"
        '
        'rbharian
        '
        Me.rbharian.AutoSize = True
        Me.rbharian.Location = New System.Drawing.Point(726, 63)
        Me.rbharian.Name = "rbharian"
        Me.rbharian.Size = New System.Drawing.Size(69, 22)
        Me.rbharian.TabIndex = 52
        Me.rbharian.TabStop = True
        Me.rbharian.Text = "Harian"
        Me.rbharian.UseVisualStyleBackColor = True
        '
        'rbbulanan
        '
        Me.rbbulanan.AutoSize = True
        Me.rbbulanan.Location = New System.Drawing.Point(801, 63)
        Me.rbbulanan.Name = "rbbulanan"
        Me.rbbulanan.Size = New System.Drawing.Size(79, 22)
        Me.rbbulanan.TabIndex = 53
        Me.rbbulanan.TabStop = True
        Me.rbbulanan.Text = "Bulanan"
        Me.rbbulanan.UseVisualStyleBackColor = True
        '
        'rbtahunan
        '
        Me.rbtahunan.AutoSize = True
        Me.rbtahunan.Location = New System.Drawing.Point(886, 63)
        Me.rbtahunan.Name = "rbtahunan"
        Me.rbtahunan.Size = New System.Drawing.Size(83, 22)
        Me.rbtahunan.TabIndex = 54
        Me.rbtahunan.TabStop = True
        Me.rbtahunan.Text = "Tahunan"
        Me.rbtahunan.UseVisualStyleBackColor = True
        '
        'btncari
        '
        Me.btncari.BackgroundImage = CType(resources.GetObject("btncari.BackgroundImage"), System.Drawing.Image)
        Me.btncari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncari.ImageIndex = 0
        Me.btncari.Location = New System.Drawing.Point(1083, 124)
        Me.btncari.Margin = New System.Windows.Forms.Padding(4)
        Me.btncari.Name = "btncari"
        Me.btncari.Size = New System.Drawing.Size(29, 28)
        Me.btncari.TabIndex = 57
        Me.btncari.UseVisualStyleBackColor = True
        '
        'txtkodebarang
        '
        Me.txtkodebarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkodebarang.Location = New System.Drawing.Point(866, 126)
        Me.txtkodebarang.Margin = New System.Windows.Forms.Padding(4)
        Me.txtkodebarang.Name = "txtkodebarang"
        Me.txtkodebarang.Size = New System.Drawing.Size(213, 24)
        Me.txtkodebarang.TabIndex = 56
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(869, 100)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 18)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Kode Barang"
        '
        'fchartpembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1384, 561)
        Me.Controls.Add(Me.btncari)
        Me.Controls.Add(Me.txtkodebarang)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.rbtahunan)
        Me.Controls.Add(Me.rbbulanan)
        Me.Controls.Add(Me.rbharian)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btntabel)
        Me.Controls.Add(Me.btnexcel)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cmbgudang)
        Me.Controls.Add(Me.btncarigudang)
        Me.Controls.Add(Me.cmbsales)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cmbsupplier)
        Me.Controls.Add(Me.btncarisupplier)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ChartControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "fchartpembelian"
        Me.Text = "Chart Pembelian"
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
    Friend WithEvents cmbsales As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbsupplier As ComboBox
    Friend WithEvents btncarisupplier As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents cmbgudang As ComboBox
    Friend WithEvents btncarigudang As Button
    Friend WithEvents btntabel As Button
    Friend WithEvents btnexcel As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents rbharian As RadioButton
    Friend WithEvents rbbulanan As RadioButton
    Friend WithEvents rbtahunan As RadioButton
    Friend WithEvents btncari As Button
    Friend WithEvents txtkodebarang As TextBox
    Friend WithEvents Label4 As Label
End Class
