Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine

Public Class flaporanpricelist
    Dim pilih As String
    Dim kode As String
    Dim modalbarang As Double
    Private Sub flaporbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call tabel()

    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode Customer"
        GridColumn1.FieldName = "kode_pelanggan"
        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"
        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"
        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"
        GridColumn5.Caption = "Kategori"
        GridColumn5.FieldName = "kategori_barang"
        GridColumn6.Caption = "Harga Jual"
        GridColumn6.FieldName = "harga_jual"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "Rp ##,##0"
        GridColumn7.Caption = "Kode Gudang"
        GridColumn7.FieldName = "kode_gudang"
        GridColumn7.Visible = False

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            'sql = "SELECT kode_stok, tb_stok.kode_barang, nama_barang, jenis_barang, satuan_barang, tb_stok.jumlah_stok, tb_stok.kode_gudang FROM tb_barang join tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang "
            sql = "SELECT tb_barang.kode_barang, nama_barang, tb_price_group.harga_jual, tb_price_group.kode_pelanggan, tb_barang.jenis_barang, tb_barang.kategori_barang FROM tb_barang JOIN tb_price_group ON tb_price_group.kode_barang=tb_barang.kode_barang WHERE tb_price_group.kode_pelanggan = '" & txtkodecust.Text & "' "
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            cnn.Close()
        End Using
    End Sub

    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_barang WHERE kode_barang = '" + kode + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                modalbarang = dr("modal_barang")

                foto = dr("gambar_barang")
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
                cnn.Close()
            End If
        End Using
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
        If GridView1.DataRowCount > 0 Then
            ExportToExcel()
        Else
            MsgBox("Export Gagal, Rekap Tabel terlebih dahulu  !", MsgBoxStyle.Information, "Gagal")
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
        Dim rptstok As ReportDocument
        rptstok = New rptlaporstok

        flappembelian.CrystalReportViewer1.ReportSource = rptstok
        flappembelian.Text = "Laporan Stok Barang"
        flappembelian.ShowDialog()
        flappembelian.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class