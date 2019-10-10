
Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class fbarang
    Dim harga, modal As Double
    Dim kode As String
    Dim minstok As Integer
    Private PathFile As String = Nothing
    Sub awal()
        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        btnauto.Enabled = False
        txtkode.Enabled = False
        txtkode.Clear()
        txtnama.Enabled = False
        txtnama.Clear()
        txtsatuan.Enabled = False
        txtsatuan.SelectedIndex = -1
        cmbjenis.Enabled = False
        cmbjenis.SelectedIndex = -1
        txtmodal.Text = 0
        txtmodal.Enabled = False

        btntambah.Enabled = True
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnbatal.Enabled = False

        txtkode.MaxLength = 20
        txtnama.MaxLength = 20

        GridControl.Enabled = True
        Call isitabel()
        txtgbr.Text = ""
        PictureBox1.Image = ImageList1.Images(0)
        btnupload.Enabled = False
        cnn.Close()
    End Sub
    Private Sub fbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub
    Sub index()
        txtkode.TabIndex = 1
        txtnama.TabIndex = 2
        txtsatuan.TabIndex = 3
    End Sub
    Sub isitabel()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "Select * from tb_barang"
            da = New OdbcDataAdapter(sql, cnn)
            cnn.Open()
            ds = New DataSet

            da.Fill(ds)
            GridControl.DataSource = Nothing

            GridControl.DataSource = ds.Tables(0)
            GridColumn1.Caption = "Kode"
            GridColumn1.FieldName = "kode_barang"
            GridColumn2.Caption = "Nama Barang"
            GridColumn2.FieldName = "nama_barang"
            GridColumn3.Caption = "Satuan"
            GridColumn3.FieldName = "satuan_barang"
            GridColumn3.Width = "60"
            GridColumn4.Caption = "Jenis"
            GridColumn4.FieldName = "jenis_barang"
            GridColumn5.Caption = "Modal"
            GridColumn5.FieldName = "modal_barang"
            GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            GridColumn5.DisplayFormat.FormatString = "Rp ##,#0"
            GridControl.Visible = True
            cnn.Close()
        End Using
    End Sub
    Sub enable_text()
        btnauto.Enabled = True
        txtkode.Enabled = True
        txtnama.Enabled = True
        cmbjenis.Enabled = True
        cmbjenis.SelectedIndex = 0
        txtsatuan.Enabled = True
        txtsatuan.SelectedIndex = 0
        btnupload.Enabled = True
        txtmodal.Enabled = True
        txtkode.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            btnupload.Enabled = True
            Call enable_text()
            Call index()

            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("Kode barang belum terisi !")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama barang belum terisi !")
                Else
                    If txtsatuan.SelectedIndex = -1 Then
                        MsgBox("Satuan belum terpilih !")
                    Else
                        If cmbjenis.SelectedIndex = -1 Then
                            MsgBox("Jenis belum dipilih !")
                        Else
                            Call simpan()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Sub simpan()
        'Call koneksii()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_barang WHERE kode_barang  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode barang Sudah ada dengan nama " + dr("nama_barang"), MsgBoxStyle.Critical, "Pemberitahuan")
                txtkode.Clear()
                txtnama.Clear()
                txtsatuan.SelectedIndex = -1
                txtkode.Focus()
                cnn.Close()
            Else
                cnn.Close()
                'menyiapkan MemoryStream
                Dim ms As MemoryStream = New MemoryStream
                'menyimpan gambar ke dalam ms dengan format jpeg
                PictureBox1.Image.Save(ms, Imaging.ImageFormat.Jpeg)
                'merubah gambar pada ms ke array
                ms.ToArray()
                sql = "INSERT INTO tb_barang (kode_barang,nama_barang,satuan_barang,jenis_barang,gambar_barang, modal_barang,created_by,updated_by,date_created,last_updated) VALUES ( ?,?,?,?,?,?,?,?,?,?)"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@kode_barang", txtkode.Text)
                cmmd.Parameters.AddWithValue("@nama_barang", txtnama.Text)
                cmmd.Parameters.AddWithValue("@satuan_barang", txtsatuan.Text)
                cmmd.Parameters.AddWithValue("@jenis_barang", cmbjenis.Text)
                cmmd.Parameters.AddWithValue("@gambar_barang", ms.ToArray)
                cmmd.Parameters.AddWithValue("@modal_barang", harga)
                cmmd.Parameters.AddWithValue("@created_by", fmenu.statususer.Text)
                cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
                cmmd.Parameters.AddWithValue("@date_created", Date.Now)
                cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                cnn.Open()
                cmmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
                btntambah.Text = "Tambah"
                Me.Refresh()
                Call awal()
            End If
        End Using
    End Sub
    Sub cari()
        txtkode.Text = GridView.GetFocusedRowCellValue("kode_barang")
        'menyiapkan variable byte() untuk menampung byte() dari foto yang ada di database
        Dim foto As Byte()
        'menyiapkan koneksi database
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_barang WHERE kode_barang  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtnama.Text = dr("nama_barang")
                txtsatuan.Text = dr("satuan_barang")
                cmbjenis.Text = dr("jenis_barang")
                kode = dr("kode_barang")
                foto = dr("gambar_barang")
                modal = dr("modal_barang")
                txtmodal.Text = Format(modal, "##,##0")
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                PictureBox1.Image = Image.FromStream(New IO.MemoryStream(foto))
                btnedit.Enabled = True
                btnbatal.Enabled = True
                btnhapus.Enabled = True
                btntambah.Enabled = False
                cnn.Close()
            End If
        End Using
    End Sub
    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        Call cari()
    End Sub
    Private Sub GridView_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call cari()
        End If
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            Call enable_text()
            Call index()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("Kode barang belum terisi!!!")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama barang belum terisi!!!")
                Else
                    If txtsatuan.SelectedIndex = -1 Then
                        MsgBox("Satuan belum terpilih!!!")
                    Else
                        If cmbjenis.SelectedIndex = -1 Then
                            MsgBox("Kategori belum terisi!!!")
                        Else
                            Call edit()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Sub update()
        'Call koneksii()
        Dim ms As MemoryStream = New MemoryStream
        'menyimpan gambar ke dalam ms dengan format jpeg
        PictureBox1.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        'merubah gambar pada ms ke array
        ms.ToArray()
        Using cnn As New OdbcConnection(strConn)
            sql = "UPDATE tb_barang SET kode_barang=?, nama_barang=?, satuan_barang=?,  jenis_barang=?, gambar_barang=?, modal_barang=?, updated_by=?, last_updated=? WHERE  kode_barang='" & kode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_barang", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_barang", txtnama.Text)
            cmmd.Parameters.AddWithValue("@satuan_barang", txtsatuan.Text)
            cmmd.Parameters.AddWithValue("@jenis_barang", cmbjenis.Text)
            cmmd.Parameters.AddWithValue("@gambar_barang", ms.ToArray)
            cmmd.Parameters.AddWithValue("@modal_barang", harga)
            cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
            cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
            cnn.Open()
            cmmd.ExecuteNonQuery()
            cnn.Close()
            MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "&Edit"
            cnn.Close()
            Me.Refresh()
            Call awal()
        End Using
    End Sub
    Sub edit()
        If txtkode.Text = kode Then
            Call update()
        Else
            Using cnn As New OdbcConnection(strConn)
                sql = "SELECT * FROM tb_barang WHERE kode_barang  = '" + txtkode.Text + "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                If dr.HasRows Then
                    MsgBox("Kode barang Sudah ada dengan nama " + dr("nama_barang"), MsgBoxStyle.Exclamation, "Pemberitahuan")
                    cnn.Close()
                Else
                    cnn.Close()
                    Call update()
                End If
            End Using
        End If

    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        'Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Using cnn As New OdbcConnection(strConn)
                sql = "DELETE FROM tb_barang WHERE  `kode_barang`='" & txtkode.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                cnn.Open()
                dr = cmmd.ExecuteReader
                cnn.Close()
                'sql = "DELETE FROM tb_stok WHERE  `kode`='" & txtkode.Text & "'"
                'cmmd = New OdbcCommand(sql, cnn)
                'cnn.Open()
                'dr = cmmd.ExecuteReader
                'cnn.Close()
                MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Refresh()
                Call awal()
            End Using
        End If
    End Sub

    Private Sub btnupload_Click(sender As Object, e As EventArgs) Handles btnupload.Click
        Dim oD As New OpenFileDialog
        oD.Multiselect = False
        Dim filena As String
        oD.Filter = "Image Files (*.jpg, *.png)|*.jpg; *.png"
        If oD.ShowDialog(Me) = DialogResult.OK Then
            filena = oD.FileName
            Dim fs As FileStream = New FileStream(filena, FileMode.OpenOrCreate, FileAccess.Read)
            Dim rawData() As Byte = New Byte(CType(fs.Length, Integer)) {}
            fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length))
            fs.Close()
            Dim resized As Image = ResizeGambar(Image.FromFile(filena), New Size(260, 260))
            Dim memStream As MemoryStream = New MemoryStream()
            resized.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg)
            PictureBox1.Image = resized
            txtgbr.Text = oD.SafeFileName
        End If
    End Sub
    Private Function ResizeGambar(ByVal gmb As Image,
    ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = gmb.Width
            Dim originalHeight As Integer = gmb.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(gmb, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function
    Private Sub btnauto_Click(sender As Object, e As EventArgs) Handles btnauto.Click
        Dim r As New Random
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        txtkode.Text = sb.ToString()
    End Sub
    Private Sub txtmodal_TextChanged(sender As Object, e As EventArgs) Handles txtmodal.TextChanged
        If txtmodal.Text = "" Then
            txtmodal.Text = 0
        Else
            harga = txtmodal.Text
            txtmodal.Text = Format(harga, "##,##0")
            txtmodal.SelectionStart = Len(txtmodal.Text)
        End If
    End Sub

    Private Sub txtmodal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmodal.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
End Class