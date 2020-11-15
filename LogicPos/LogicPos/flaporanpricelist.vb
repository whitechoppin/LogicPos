Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class flaporanpricelist
    Public namaform As String = "laporan-pricelist"

    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Dim pilih As String
    Dim kode As String
    Dim modalbarang As Double
    Dim idpelanggan As String

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

    Private Sub flaporanpricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call tabel()
        Call comboboxpelanggan()

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

        Call historysave("Membuka Laporan Pricelist", "N/A", namaform)
    End Sub

    Sub comboboxpelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbpelanggan.DataSource = ds.Tables(0)
        cmbpelanggan.ValueMember = "id"
        cmbpelanggan.DisplayMember = "kode_pelanggan"
    End Sub
    Sub grid()
        GridColumn1.Caption = "id pelanggan"
        GridColumn1.FieldName = "pelanggan_id"
        GridColumn1.Visible = False

        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"

        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"

        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"

        GridColumn5.Caption = "Kategori"
        GridColumn5.FieldName = "nama_kategori"

        GridColumn6.Caption = "Harga Jual"
        GridColumn6.FieldName = "harga_jual"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "Rp ##,##0"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        sql = "SELECT kode_barang, nama_barang, tb_price_group.harga_jual, tb_price_group.pelanggan_id, tb_barang.jenis_barang, tb_kategori_barang.nama_kategori FROM tb_barang JOIN tb_price_group ON tb_price_group.barang_id = tb_barang.id JOIN tb_kategori_barang ON tb_kategori_barang.id = tb_barang.kategori_barang_id WHERE tb_price_group.pelanggan_id = '" & idpelanggan & "' "
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

    Sub caripelanggan()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" & cmbpelanggan.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idpelanggan = dr("id")
            txtpelanggan.Text = dr("nama_pelanggan")
            txttelp.Text = dr("telepon_pelanggan")
        Else
            txtpelanggan.Text = ""
            txttelp.Text = ""
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
                Call historysave("Mengexport Pricelist", "N/A", namaform)
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
            If cmbpelanggan.Text = "" Then
                MsgBox("Kode pelanggan kosong !")
            Else
                Dim rptstok As ReportDocument

                Call historysave("Merekap Laporan Pricelist", "N/A", namaform)
                rptstok = New rptlaporprice
                rptstok.SetParameterValue("kode", idpelanggan)
                flappricelist.CrystalReportViewer1.ReportSource = rptstok
                flappricelist.ShowDialog()
                flappricelist.WindowState = FormWindowState.Maximized
            End If
        Else
            MsgBox("Tidak ada akses")
        End If

    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
        Call historysave("Merefresh Laporan Pricelist", "N/A", namaform)
    End Sub

    Private Sub cmbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.SelectedIndexChanged
        Call caripelanggan()
    End Sub

    Private Sub cmbcustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbpelanggan.TextChanged
        Call caripelanggan()
    End Sub

    Private Sub flaporanpricelist_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btncaricustomer_Click(sender As Object, e As EventArgs) Handles btncaricustomer.Click
        tutupcus = 4
        fcaripelanggan.ShowDialog()
    End Sub
End Class