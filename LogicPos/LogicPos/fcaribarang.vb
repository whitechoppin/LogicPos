Imports System.Data.Odbc
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class fcaribarang
    Dim pilih As String
    Dim kode As String
    Dim modalbarang As Double

    Private Sub fcaribarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
        LabelHarga.Visible = False
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Width = 15
        GridColumn2.Caption = "Nama Barang"
        GridColumn2.FieldName = "nama_barang"
        GridColumn2.Width = 40
        GridColumn3.Caption = "Jenis"
        GridColumn3.FieldName = "jenis_barang"
        GridColumn3.Width = 15
        GridColumn4.Caption = "Satuan"
        GridColumn4.FieldName = "satuan_barang"
        GridColumn4.Width = 10

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        If tutup > 0 Then
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT tb_barang.kode_barang, tb_barang.nama_barang, tb_barang.satuan_barang, tb_barang.jenis_barang from tb_barang "
                da = New OdbcDataAdapter(sql, cnn)
                cnn.Open()
                ds = New DataSet
                da.Fill(ds)
                GridControl1.DataSource = Nothing
                GridControl1.DataSource = ds.Tables(0)
                Call grid()
                cnn.Close()
            End Using
        End If

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
                LabelHarga.Text = Format(modalbarang, "##,##0")
                foto = dr("gambar_barang")
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
                cnn.Close()
            End If
        End Using
    End Sub

    Private Sub Gridview1_KeyUp(sender As Object, e As KeyEventArgs)
        Call ambil_gbr()
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs)
        Call ambil_gbr()
    End Sub

    Sub ExportToExcel()
        'https://www.devexpress.com/Support/Center/Question/Details/Q430217
        'https://documentation.devexpress.com/#WindowsForms/CustomDocument1874
        'storing current layout
        GridView1.SaveLayoutToXml("C:\data\tempLayout.xml")
        For Each column As GridColumn In GridView1.Columns
            'make all export columns visible
            column.Visible = True
        Next
        Dim a As String = InputBox("Nama File", "Input Nama file ")
        GridView1.ExportToXls("C:\data\" + a + ".xls")
        'restoring the layout, the layout file needs to be deleted manually
        GridView1.RestoreLayoutFromXml("C:\data\tempLayout.xml")
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutup = 1 Then
            fpricelist.txtkode.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
            Me.Hide()
        ElseIf tutup = 2 Then
            fpembelian.txtkodebarang.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
            Me.Hide()
        ElseIf tutup = 3 Then
            fbarangmasuk.txtkodebarang.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
            Me.Hide()
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

    Private Sub GridControl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridControl1.KeyPress
        If e.KeyChar = Strings.Chr(Keys.Enter) Then
            If tutup = 1 Then
                fpricelist.txtkode.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
                Me.Hide()
            ElseIf tutup = 2 Then
                fpembelian.txtkodebarang.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
                Me.Hide()
            ElseIf tutup = 3 Then
                fbarangmasuk.txtkodebarang.Text = Me.GridView1.GetFocusedRowCellValue("kode_barang")
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        If LabelHarga.Visible = False Then
            passwordid = 4
            fpassword.ShowDialog()
            'LabelHarga.Visible = False
        ElseIf LabelHarga.Visible = True Then
            LabelHarga.Visible = False
        End If
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel()
    End Sub
End Class