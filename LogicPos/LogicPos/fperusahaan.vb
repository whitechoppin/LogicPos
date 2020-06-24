Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class fperusahaan
    Dim namaawal, namaakhir As String
    Dim alamatawal, alamatakhir As String
    Dim telpawal, telpakhir As String
    Dim rekawal, rekakhir As String
    Dim logoawal, logoakhir As Byte()

    Private Sub falamat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
        Call historysave("Membuka Setting Alamat", "N/A")
    End Sub

    Private Sub falamat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
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
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
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

    Sub awal()
        Call koneksii()

        sql = "SELECT * FROM tb_info_perusahaan WHERE nomor =1 LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            namaawal = dr("nama")
            alamatawal = dr("alamat")
            telpawal = dr("telepon")
            rekawal = dr("rekening")
            logoawal = dr("logo")

            txtnama.Text = namaawal
            txtalamat.Text = alamatawal
            txttelepon.Text = telpawal
            txtrekening.Text = rekawal

            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.Image = Image.FromStream(New IO.MemoryStream(logoawal))
        Else
            txtnama.Text = ""
            txtalamat.Text = ""
            txttelepon.Text = ""
            txtrekening.Text = ""
            PictureBox1.Image = ImageList1.Images(0)
        End If
    End Sub

    Sub simpan()
        Call koneksii()
        namaakhir = txtnama.Text
        alamatakhir = txtalamat.Text
        telpakhir = txttelepon.Text
        rekakhir = txtrekening.Text

        Dim ms As MemoryStream = New MemoryStream
        'menyimpan gambar ke dalam ms dengan format jpeg
        PictureBox1.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        'merubah gambar pada ms ke array
        ms.ToArray()

        sql = "SELECT * FROM tb_info_perusahaan WHERE nomor = 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "UPDATE tb_info_perusahaan SET nama=?,alamat=?, telepon=?, rekening=?, logo=? WHERE nomor = 1"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@nama", namaakhir)
            cmmd.Parameters.AddWithValue("@alamat", alamatakhir)
            cmmd.Parameters.AddWithValue("@telepon", telpakhir)
            cmmd.Parameters.AddWithValue("@rekening", rekakhir)
            cmmd.Parameters.AddWithValue("@logo", ms.ToArray)
            cmmd.ExecuteNonQuery()
        Else
            sql = "INSERT INTO tb_info_perusahaan (nomor, nama, alamat, telepon, rekening, logo) VALUES (?,?,?,?,?,?)"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@nomor", 1)
            cmmd.Parameters.AddWithValue("@nama", namaakhir)
            cmmd.Parameters.AddWithValue("@alamat", alamatakhir)
            cmmd.Parameters.AddWithValue("@telepon", telpakhir)
            cmmd.Parameters.AddWithValue("@rekening", rekakhir)
            cmmd.Parameters.AddWithValue("@logo", ms.ToArray)
            cmmd.ExecuteNonQuery()
        End If

        MsgBox("Pengaturan Alamat Di Simpan")
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Call awal()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call simpan()
        Call historysave("Mengupdate Setting Alamat", "N/A")
    End Sub
End Class