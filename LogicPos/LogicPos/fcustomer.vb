Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fcustomer
    Private Sub fcustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()

    End Sub
    Sub awal()
        txtkode.Clear()
        txtalamat.Clear()
        txtnama.Clear()
        txttelp.Clear()

        Call koneksii()

        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtkode.Enabled = False
        txtnama.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        txtgbr.Text = ""
        PictureBox1.Image = ImageList1.Images(0)
        btnupload.Enabled = False

        GridControl.Enabled = True
        Call isitabel()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode"
        GridColumn1.Width = 65
        GridColumn1.FieldName = "kode_pelanggan"
        GridColumn2.Caption = "Nama"
        GridColumn2.Width = 65
        GridColumn2.FieldName = "nama_pelanggan"
        GridColumn3.Caption = "Alamat"
        GridColumn3.FieldName = "alamat_pelanggan"
        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 73
        GridColumn4.FieldName = "telepon_pelanggan"
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "Select * from tb_pelanggan"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl.DataSource = Nothing
        'GridView1.Columns.Clear()
        GridControl.DataSource = ds.Tables(0)
        Call kolom()
    End Sub
    Sub index()
        txtnama.TabIndex = 1
        txtalamat.TabIndex = 2
        txttelp.TabIndex = 3
    End Sub
    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(`kode_pelanggan`,2) FROM `tb_pelanggan` WHERE date_format(LEFT(`kode_pelanggan`,6), ' %y ')+ MONTH(LEFT(`kode_pelanggan`,6)) + DAY(LEFT(`kode_pelanggan`,6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_pelanggan,2) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    End If
                End If
            Else
                Return Format(Now.Date, "yyMMdd") + "01"
            End If
        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
    Sub enable_text()
        txtkode.Enabled = False
        txtnama.Enabled = True
        txttelp.Enabled = True
        txtalamat.Enabled = True
        txtnama.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            btnupload.Enabled = True
            Call enable_text()
            Call index()
            txtkode.Text = autonumber()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("ID belum terisi!!!")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama belum terisi!!!")
                Else
                    Call simpan()
                End If
            End If
        End If
    End Sub
    Sub simpan()
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode Customer Sudah ada dengan nama " + dr("nama_pelanggan"), MsgBoxStyle.Information, "Pemberitahuan")
                txtkode.Clear()
                txtnama.Clear()
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
                sql = "INSERT INTO tb_pelanggan (kode_pelanggan,nama_pelanggan,alamat_pelanggan,telepon_pelanggan,foto_pelanggan,created_by,updated_by,date_created,last_updated) VALUES ( ?,?,?,?,?,?,?,?,?)"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@kode_pelanggan", txtkode.Text)
                cmmd.Parameters.AddWithValue("@nama_pelanggan", txtnama.Text)
                cmmd.Parameters.AddWithValue("@alamat_pelanggan", txtalamat.Text)
                cmmd.Parameters.AddWithValue("@telepon_pelanggan", txttelp.Text)
                cmmd.Parameters.AddWithValue("@foto_pelanggan", ms.ToArray)
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
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            btnupload.Enabled = True
            Call enable_text()
            Call index()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("ID belum terisi!!!")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama belum terisi!!!")
                Else
                    Call edit()
                End If
            End If
        End If
    End Sub
    Sub edit()
        Dim ms As MemoryStream = New MemoryStream
        'menyimpan gambar ke dalam ms dengan format jpeg
        PictureBox1.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        'merubah gambar pada ms ke array
        ms.ToArray()
        Using cnn As New OdbcConnection(strConn)
            sql = "UPDATE tb_pelanggan SET kode_pelanggan=?, nama_pelanggan=?, alamat_pelanggan=?,  telepon_pelanggan=?, foto_pelanggan=?, updated_by=?, last_updated=? WHERE  kode_pelanggan='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_pelanggan", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_pelanggan", txtnama.Text)
            cmmd.Parameters.AddWithValue("@alamat_pelanggan", txtalamat.Text)
            cmmd.Parameters.AddWithValue("@telepon_pelanggan", txttelp.Text)
            cmmd.Parameters.AddWithValue("@foto_pelanggan", ms.ToArray)
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
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_pelanggan WHERE  kode_pelanggan='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Refresh()
            Call awal()
        End If
    End Sub
    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        txtkode.Text = GridView.GetFocusedRowCellValue("kode_pelanggan")
        'txtnama.Text = GridView1.GetFocusedRowCellValue("nama_pelanggan")
        'txtalamat.Text = GridView1.GetFocusedRowCellValue("alamat_pelanggan")
        'txttelp.Text = GridView1.GetFocusedRowCellValue("telepon_pelanggan")
        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"

        'menyiapkan variable byte() untuk menampung byte() dari foto yang ada di database
        Dim foto As Byte()
        'menyiapkan koneksi database
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtnama.Text = dr("nama_pelanggan")
                txtalamat.Text = dr("alamat_pelanggan")
                txttelp.Text = dr("telepon_pelanggan")
                foto = dr("foto_pelanggan")
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

    Private Sub btnrekening_Click(sender As Object, e As EventArgs) Handles btnrekening.Click
        frekeningcustomer.kode_customer = Me.txtkode.Text
        frekeningcustomer.ShowDialog()
    End Sub
End Class