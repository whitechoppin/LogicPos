Imports System.Data.Odbc
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports ZXing

Public Class flaporanstokbarang
    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim pilih As String
    Dim kode As String
    Dim modalbarang As Double

    '==== autosize form ====
    Dim CuRWidth As Integer = Me.Width
    Dim CuRHeight As Integer = Me.Height

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim RatioHeight As Double = (Me.Height - CuRHeight) / CuRHeight
        Dim RatioWidth As Double = (Me.Width - CuRWidth) / CuRWidth

        For Each ctrl As Control In Controls
            ctrl.Width += ctrl.Width * RatioWidth
            ctrl.Left += ctrl.Left * RatioWidth
            ctrl.Top += ctrl.Top * RatioHeight
            ctrl.Height += ctrl.Height * RatioHeight
        Next

        CuRHeight = Me.Height
        CuRWidth = Me.Width
    End Sub

    '=======================

    Private Sub flaporanstokbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call tabel()
        LabelHarga.Visible = False
        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            .Columns("jumlah_stok").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "jumlah_stok", "{0:n0}")
        End With

        Select Case kodeakses
            Case 1
                printstatus = True
                exportstatus = False
            Case 3
                printstatus = False
                exportstatus = True
            Case 4
                printstatus = True
                exportstatus = True
        End Select

        Call historysave("Membuka Laporan Stok Barang", "N/A")
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_stok"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"
        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"
        GridColumn5.Caption = "Satuan"
        GridColumn5.FieldName = "satuan_barang"
        GridColumn6.Caption = "Jumlah Stok"
        GridColumn6.FieldName = "jumlah_stok"
        GridColumn7.Caption = "Kode Gudang"
        GridColumn7.FieldName = "kode_gudang"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        sql = "SELECT kode_stok, tb_stok.kode_barang, nama_barang, jenis_barang, satuan_barang, tb_stok.jumlah_stok, tb_stok.kode_gudang FROM tb_barang join tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang "
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call grid()
    End Sub

    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang = '" + kode + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            modalbarang = dr("modal_barang")
            LabelHarga.Text = Format(modalbarang, "##,##0")
            foto = dr("gambar_barang")
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
        End If
    End Sub
    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click
        Call ambil_gbr()
    End Sub

    Private Sub GridControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyDown
        Call ambil_gbr()
    End Sub

    Private Sub GridControl1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyUp
        Call ambil_gbr()
    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        If exportstatus.Equals(True) Then
            If GridView1.DataRowCount > 0 Then
                ExportToExcel()
                Call historysave("Mengexport Laporan Stok Barang", "N/A")
            Else
                MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub ExportToExcel()

        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            GridView1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Private Sub btnrekap_Click(sender As Object, e As EventArgs) Handles btnrekap.Click
        If printstatus.Equals(True) Then
            Dim rptstok As ReportDocument
            rptstok = New rptlaporstok

            flapstokbarang.CrystalReportViewer1.ReportSource = rptstok
            flapstokbarang.ShowDialog()
            flapstokbarang.WindowState = FormWindowState.Maximized

            Call historysave("Merekap Laporan Stok Barang", "N/A")
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
        Call historysave("Merefresh Laporan Stok Barang", "N/A")
    End Sub

    Private Sub flaporanstokbarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnbarcode_Click(sender As Object, e As EventArgs) Handles btnbarcode.Click
        Dim rptbarcodebarang As ReportDocument
        Dim tabel_barcode_barang As New DataTable
        Dim baris As DataRow


        Dim writer As New BarcodeWriter


        With tabel_barcode_barang
            .Columns.Add("kode_stok")
            .Columns.Add("nama_barang")
            .Columns.Add("jenis_barang")
            .Columns.Add("harga_satuan")
            .Columns.Add("satuan_barang")
            .Columns.Add("barcode_barang", GetType(Byte()))
        End With

        'GridColumn1.Caption = "Kode"
        'GridColumn1.FieldName = "kode_stok"
        'GridColumn2.Caption = "Kode Barang"
        'GridColumn2.FieldName = "kode_barang"
        'GridColumn3.Caption = "Nama Barang"
        'GridColumn3.FieldName = "nama_barang"
        'GridColumn4.Caption = "Jenis"
        'GridColumn4.FieldName = "jenis_barang"
        'GridColumn5.Caption = "Satuan"
        'GridColumn5.FieldName = "satuan_barang"
        'GridColumn6.Caption = "Jumlah Stok"
        'GridColumn6.FieldName = "jumlah_stok"
        'GridColumn7.Caption = "Kode Gudang"
        'GridColumn7.FieldName = "kode_gudang"

        For i As Integer = 0 To GridView1.RowCount - 1
            Dim barcode As Image
            Dim ms As MemoryStream = New MemoryStream


            baris = tabel_barcode_barang.NewRow
            baris("kode_stok") = GridView1.GetRowCellValue(i, "kode_stok")
            baris("nama_barang") = GridView1.GetRowCellValue(i, "nama_barang")
            baris("jenis_barang") = GridView1.GetRowCellValue(i, "jenis_barang")
            baris("harga_satuan") = GridView1.GetRowCellValue(i, "jumlah_stok")
            baris("satuan_barang") = GridView1.GetRowCellValue(i, "satuan_barang")

            writer.Options.Height = 100
            writer.Options.Width = 100
            writer.Format = BarcodeFormat.QR_CODE

            barcode = writer.Write(GridView1.GetRowCellValue(i, "kode_stok").ToString)
            barcode.Save(ms, Imaging.ImageFormat.Jpeg)
            ms.ToArray()
            baris("barcode_barang") = ms.ToArray

            tabel_barcode_barang.Rows.Add(baris)
        Next

        rptbarcodebarang = New rptlaporbarcode
        rptbarcodebarang.SetDataSource(tabel_barcode_barang)
        'rptbarcodebarang.SetParameterValue("tglawal", dtawal.Text)
        'rptbarcodebarang.SetParameterValue("tglakhir", dtakhir.Text)
        flapmutasibarang.CrystalReportViewer1.ReportSource = rptbarcodebarang
        flapmutasibarang.ShowDialog()
        flapmutasibarang.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        If LabelHarga.Visible = False Then
            passwordid = 2
            fpassword.Show()
            'LabelHarga.Visible = False
        ElseIf LabelHarga.Visible = True Then
            LabelHarga.Visible = False
        End If
    End Sub
End Class