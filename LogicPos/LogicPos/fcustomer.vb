Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fcustomer
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean
    Private Sub fcustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
        Select Case kodeakses
            Case 1
                tambahstatus = True
                editstatus = False
                hapusstatus = False
            Case 3
                tambahstatus = False
                editstatus = True
                hapusstatus = False
            Case 5
                tambahstatus = False
                editstatus = False
                hapusstatus = True
            Case 4
                tambahstatus = True
                editstatus = True
                hapusstatus = False
            Case 6
                tambahstatus = True
                editstatus = False
                hapusstatus = True
            Case 8
                tambahstatus = False
                editstatus = True
                hapusstatus = True
            Case 9
                tambahstatus = True
                editstatus = True
                hapusstatus = True
        End Select

        'history user =====
        Call koneksii()
        sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, created_by, date_created) VALUES ('Membuka Master Customer', 'N/A','" & fmenu.statususer.Text & "',now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        '==================
    End Sub
    Sub awal()
        txtkode.Clear()
        txtalamat.Clear()
        txtnama.Clear()
        txttelp.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtkode.Enabled = False
        txtnama.Enabled = False
        txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnrekening.Enabled = False
        btnupload.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        txtgbr.Text = ""
        PictureBox1.Image = ImageList1.Images(0)

        GridControl.Enabled = True
        Call isitabel()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode"
        GridColumn1.Width = 40
        GridColumn1.FieldName = "kode_pelanggan"
        GridColumn2.Caption = "Nama"
        GridColumn2.Width = 40
        GridColumn2.FieldName = "nama_pelanggan"
        GridColumn3.Caption = "Alamat"
        GridColumn3.Width = 65
        GridColumn3.FieldName = "alamat_pelanggan"
        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 40
        GridColumn4.FieldName = "telepon_pelanggan"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 40
        GridColumn5.FieldName = "keterangan_pelanggan"
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan"
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
        txtketerangan.TabIndex = 4
        btnupload.TabIndex = 5
    End Sub
    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_pelanggan,3) FROM tb_pelanggan WHERE DATE_FORMAT(MID(`kode_pelanggan`, 3 , 6), ' %y ')+ MONTH(MID(`kode_pelanggan`,3 , 6)) + DAY(MID(`kode_pelanggan`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_pelanggan,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "CS" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "CS" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "CS" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "CS" + Format(Now.Date, "yyMMdd") + "001"
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
        txtketerangan.Enabled = True
        txtnama.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
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
        Else
            MsgBox("Tidak ada akses")
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
                sql = "INSERT INTO tb_pelanggan (kode_pelanggan,nama_pelanggan,alamat_pelanggan,telepon_pelanggan,keterangan_pelanggan,foto_pelanggan,created_by,updated_by,date_created,last_updated) VALUES (?,?,?,?,?,?,?,?,?,?)"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@kode_pelanggan", txtkode.Text)
                cmmd.Parameters.AddWithValue("@nama_pelanggan", txtnama.Text)
                cmmd.Parameters.AddWithValue("@alamat_pelanggan", txtalamat.Text)
                cmmd.Parameters.AddWithValue("@telepon_pelanggan", txttelp.Text)
                cmmd.Parameters.AddWithValue("@keterangan_pelanggan", txtketerangan.Text)
                cmmd.Parameters.AddWithValue("@foto_pelanggan", ms.ToArray)
                cmmd.Parameters.AddWithValue("@created_by", fmenu.statususer.Text)
                cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
                cmmd.Parameters.AddWithValue("@date_created", Date.Now)
                cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                cnn.Open()
                cmmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")

                'history user ==========
                cnn.Open()
                sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, created_by, date_created) VALUES ('Menyimpan Customer kode " & txtkode.Text & "', '" & txtkode.Text & "','" & fmenu.statususer.Text & "',now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                cnn.Close()
                '========================

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
        If editstatus.Equals(True) Then
            If btnedit.Text = "Edit" Then
                btnedit.Text = "Simpan"
                btnhapus.Enabled = False
                btnupload.Enabled = True
                btnrekening.Enabled = False
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
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub edit()
        Dim ms As MemoryStream = New MemoryStream
        'menyimpan gambar ke dalam ms dengan format jpeg
        PictureBox1.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        'merubah gambar pada ms ke array
        ms.ToArray()
        Using cnn As New OdbcConnection(strConn)
            sql = "UPDATE tb_pelanggan SET kode_pelanggan=?, nama_pelanggan=?, alamat_pelanggan=?,  telepon_pelanggan=?, keterangan_pelanggan=?, foto_pelanggan=?, updated_by=?, last_updated=? WHERE  kode_pelanggan='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_pelanggan", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_pelanggan", txtnama.Text)
            cmmd.Parameters.AddWithValue("@alamat_pelanggan", txtalamat.Text)
            cmmd.Parameters.AddWithValue("@telepon_pelanggan", txttelp.Text)
            cmmd.Parameters.AddWithValue("@keterangan_pelanggan", txtketerangan.Text)
            cmmd.Parameters.AddWithValue("@foto_pelanggan", ms.ToArray)
            cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
            cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
            cnn.Open()
            cmmd.ExecuteNonQuery()
            cnn.Close()
            MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "&Edit"
            cnn.Close()

            'history user ==========
            cnn.Open()
            sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, created_by, date_created) VALUES ('Mengedit Customer kode " & txtkode.Text & "', '" & txtkode.Text & "','" & fmenu.statususer.Text & "',now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            cnn.Close()
            '========================

            Me.Refresh()
            Call awal()
        End Using
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            Call koneksii()
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_pelanggan WHERE  kode_pelanggan='" & txtkode.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'history user ==========
                Call koneksii()
                sql = "INSERT INTO tb_history_user (keterangan_history, kode_tabel, created_by, date_created) VALUES ('Menghapus Customer kode " & txtkode.Text & "', '" & txtkode.Text & "','" & fmenu.statususer.Text & "',now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                '========================

                Me.Refresh()
                Call awal()
            End If
        Else
            MsgBox("Tidak ada akses")
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
        btnrekening.Enabled = True
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
                txtketerangan.Text = dr("keterangan_pelanggan")
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

    Private Sub fcustomer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
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
            txtgbr.Text = oD.SafeFileName
        End If
    End Sub

    Private Sub btnrekening_Click(sender As Object, e As EventArgs) Handles btnrekening.Click
        Dim rekening As Integer
        rekening = flogin.rekeningcustomer
        If rekening > 0 Then
            frekeningcustomer.kode_customer = Me.txtkode.Text
            frekeningcustomer.kodeakses = rekening
            frekeningcustomer.ShowDialog()
        Else
            MsgBox("Anda tidak memiliki akses", MsgBoxStyle.Information, "Gagal")
        End If
    End Sub
End Class