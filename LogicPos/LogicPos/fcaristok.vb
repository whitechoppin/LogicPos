Imports System.Data.Odbc
Imports DevExpress.Utils

Public Class fcaristok

    Dim pilih As String
    Dim kode As String
    Dim modalbarang As Double

    Private Sub fcaristok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tabel()
        LabelHarga.Visible = False
    End Sub
    Sub grid()
        GridColumn1.Caption = "Kode Stok"
        GridColumn1.FieldName = "kode_stok"
        GridColumn1.Width = 30

        GridColumn2.Caption = "Kode Barang"
        GridColumn2.FieldName = "kode_barang"
        GridColumn2.Width = 30

        GridColumn3.Caption = "Nama Barang"
        GridColumn3.FieldName = "nama_barang"
        GridColumn3.Width = 50

        GridColumn4.Caption = "Jenis"
        GridColumn4.FieldName = "jenis_barang"
        GridColumn4.Width = 15

        GridColumn5.Caption = "Satuan"
        GridColumn5.FieldName = "satuan_barang"
        GridColumn5.Width = 15

        GridColumn6.Caption = "Jumlah Stok"
        GridColumn6.FieldName = "jumlah_stok"
        GridColumn6.Width = 10
        GridColumn6.DisplayFormat.FormatType = FormatType.Numeric
        GridColumn6.DisplayFormat.FormatString = "{0:n0}"

        GridControl1.Visible = True
    End Sub
    Sub tabel()
        'Call koneksii()
        If tutupstok > 0 Then
            Call koneksii()
            'Using cnn As New OdbcConnection(strConn)
            sql = "SELECT kode_stok, tb_stok.kode_barang, nama_barang, jenis_barang, satuan_barang, tb_stok.jumlah_stok FROM tb_barang JOIN tb_stok ON tb_barang.kode_barang = tb_stok.kode_barang WHERE tb_stok.kode_gudang ='" & kodegudangcari & "' AND tb_stok.jumlah_stok > 0"
            da = New OdbcDataAdapter(sql, cnn)
            'cnn.Open()
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid()
            'cnn.Close()
            'End Using
        End If

    End Sub

    Sub ambil_gbr()
        kode = Me.GridView1.GetFocusedRowCellValue("kode_barang")
        Dim foto As Byte()
        'menyiapkan koneksi database
        Call koneksii()

        'Using cnn As New OdbcConnection(strConn)
        sql = "SELECT * FROM tb_barang WHERE kode_barang = '" + kode + "'"
        cmmd = New OdbcCommand(sql, cnn)
        'cnn.Open()
        dr = cmmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            modalbarang = dr("modal_barang")
            LabelHarga.Text = Format(modalbarang, "##,##0")
            foto = dr("gambar_barang")
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
            'cnn.Close()
        End If
        'End Using
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If tutupstok = 1 Then
            fpenjualan.txtkodestok.Text = Me.GridView1.GetFocusedRowCellValue("kode_stok")
        ElseIf tutupstok = 2 Then
            fbarangkeluar.txtkodestok.Text = Me.GridView1.GetFocusedRowCellValue("kode_stok")
        ElseIf tutupstok = 3 Then
            ftransferbarang.txtkodestok.Text = Me.GridView1.GetFocusedRowCellValue("kode_stok")
        End If
        Me.Hide()
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
            If tutupstok = 1 Then
                fpenjualan.txtkodestok.Text = Me.GridView1.GetFocusedRowCellValue("kode_stok")
            ElseIf tutupstok = 2 Then
                fbarangkeluar.txtkodestok.Text = Me.GridView1.GetFocusedRowCellValue("kode_stok")
            ElseIf tutupstok = 3 Then
                ftransferbarang.txtkodestok.Text = Me.GridView1.GetFocusedRowCellValue("kode_stok")
            End If
            Me.Hide()
        End If
    End Sub

    Private Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        If LabelHarga.Visible = False Then
            passwordid = 3
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