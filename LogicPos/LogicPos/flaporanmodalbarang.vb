Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.Utils

Public Class flaporanmodalbarang
    Public namaform As String = "laporan-modal_barang"

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
    Private Sub flaporanmodalbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call grid()
        Call tabel()

        With GridView1
            'agar muncul footer untuk sum/avg/count
            .OptionsView.ShowFooter = True
            'buat sum harga
            '.Columns("jumlah_stok").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "jumlah_stok", "{0:n0}")
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

        Call historysave("Membuka Laporan Modal Barang", "N/A", namaform)
    End Sub
    Sub grid()
        GridColumn1.Caption = "id"
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"

        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"

        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"

        GridColumn5.Caption = "Satuan"
        GridColumn5.FieldName = "satuan_barang"

        GridColumn6.Caption = "Kategori"
        GridColumn6.FieldName = "nama_kategori"

        GridColumn7.Caption = "Harga Modal"
        GridColumn7.FieldName = "modal_barang"
        GridColumn7.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn7.DisplayFormat.FormatString = "{0:n0}"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksi()
        sql = "SELECT * FROM tb_barang JOIN tb_kategori_barang ON tb_kategori_barang.id = tb_barang.kategori_barang_id"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
    End Sub
    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Call koneksi()
        sql = "SELECT * FROM tb_barang WHERE kode_barang = '" & kode & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            modalbarang = dr("modal_barang")
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
                Call historysave("Mengexport Laporan Modal Barang", "N/A", namaform)
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
            Dim rptmodal As ReportDocument
            rptmodal = New rptlapormodal

            flapmodalbarang.CrystalReportViewer1.ReportSource = rptmodal
            flapmodalbarang.ShowDialog()
            flapmodalbarang.WindowState = FormWindowState.Maximized

            Call historysave("Merekap Laporan Modal Barang", "N/A", namaform)
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
        Call historysave("Merefresh Laporan Modal Barang", "N/A", namaform)
    End Sub
    Private Sub flaporanmodalbarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class