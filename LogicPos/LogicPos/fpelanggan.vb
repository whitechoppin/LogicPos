Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fpelanggan
    Dim idpelanggan, kodepelanggan, namapelanggan, alamatpelanggan, teleponpelanggan, keteranganpelanggan As String
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean

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

    Private Sub fpelanggan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Call historysave("Membuka Master Customer", "N/A")
    End Sub

    Private Sub fpelanggan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub awal()
        txtkode.Clear()
        txtalamat.Clear()
        txtnama.Clear()
        txttelp.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtkode.Enabled = False
        btnauto.Enabled = False
        btngenerate.Enabled = False
        txtnama.Enabled = False
        txtalamat.Enabled = False
        txttelp.Enabled = False
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
        PictureBox.Image = ImageList.Images(0)

        GridControl.Enabled = True
        Call isitabel()
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)

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
        GridColumn5.Width = 50
        GridColumn5.FieldName = "keterangan_pelanggan"

        GridColumn6.Caption = "id"
        GridColumn6.Width = 20
        GridColumn6.FieldName = "id"
        GridColumn6.Visible = False
    End Sub
    Sub index()
        txtkode.TabIndex = 1
        btnauto.TabIndex = 2
        btngenerate.TabIndex = 3
        txtnama.TabIndex = 4
        txtalamat.TabIndex = 5
        txttelp.TabIndex = 6
        txtketerangan.TabIndex = 7
        btnupload.TabIndex = 8
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
            'cnn.Close()
        End Try
        Return pesan
    End Function
    Sub enable_text()
        txtkode.Enabled = True
        btnauto.Enabled = True
        btngenerate.Enabled = True
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
        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan  = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode pelanggan sudah ada dengan nama " + dr("nama_pelanggan"), MsgBoxStyle.Information, "Pemberitahuan")
            txtkode.Focus()
        Else
            Dim ms As MemoryStream = New MemoryStream
            'menyimpan gambar ke dalam ms dengan format jpeg
            PictureBox.Image.Save(ms, Imaging.ImageFormat.Jpeg)
            ms.ToArray()

            Try
                Call koneksii()
                sql = "INSERT INTO tb_pelanggan (kode_pelanggan, nama_pelanggan, alamat_pelanggan, telepon_pelanggan, keterangan_pelanggan, foto_pelanggan, created_by, updated_by, date_created, last_updated) VALUES (?,?,?,?,?,?,?,?,?,?)"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@kode_pelanggan", txtkode.Text)
                cmmd.Parameters.AddWithValue("@nama_pelanggan", txtnama.Text)
                cmmd.Parameters.AddWithValue("@alamat_pelanggan", txtalamat.Text)
                cmmd.Parameters.AddWithValue("@telepon_pelanggan", txttelp.Text)
                cmmd.Parameters.AddWithValue("@keterangan_pelanggan", txtketerangan.Text)
                cmmd.Parameters.AddWithValue("@foto_pelanggan", ms.ToArray)
                cmmd.Parameters.AddWithValue("@created_by", fmenu.kodeuser.text)
                cmmd.Parameters.AddWithValue("@updated_by", fmenu.kodeuser.text)
                cmmd.Parameters.AddWithValue("@date_created", Date.Now)
                cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                cmmd.ExecuteNonQuery()

                MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")

                'history user ==========
                Call historysave("Menyimpan Data Customer Kode " + txtkode.Text, txtkode.Text)
                '========================
                btntambah.Text = "Tambah"
                Me.Refresh()
                Call awal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
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
                    MsgBox("Kode belum terisi!!!")
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
        If txtkode.Text.Equals(kodepelanggan) Then
            Call perbaharui()
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_pelanggan WHERE kode_pelanggan = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode pelanggan sudah ada dengan nama " + dr("nama_pelanggan"), MsgBoxStyle.Exclamation, "Pemberitahuan")
                txtkode.Focus()
            Else
                Call perbaharui()
            End If
        End If
    End Sub
    Sub perbaharui()
        Dim ms As MemoryStream = New MemoryStream
        'menyimpan gambar ke dalam ms dengan format jpeg
        PictureBox.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        ms.ToArray()

        Try
            Call koneksii()
            sql = "UPDATE tb_pelanggan SET kode_pelanggan=?, nama_pelanggan=?, alamat_pelanggan=?,  telepon_pelanggan=?, keterangan_pelanggan=?, foto_pelanggan=?, updated_by=?, last_updated=? WHERE  id='" & idpelanggan & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_pelanggan", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_pelanggan", txtnama.Text)
            cmmd.Parameters.AddWithValue("@alamat_pelanggan", txtalamat.Text)
            cmmd.Parameters.AddWithValue("@telepon_pelanggan", txttelp.Text)
            cmmd.Parameters.AddWithValue("@keterangan_pelanggan", txtketerangan.Text)
            cmmd.Parameters.AddWithValue("@foto_pelanggan", ms.ToArray)
            cmmd.Parameters.AddWithValue("@updated_by", fmenu.kodeuser.text)
            cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
            cmmd.ExecuteNonQuery()

            MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            'history user ===========
            Call historysave("Mengedit Data Pelanggan Kode " + txtkode.Text, txtkode.Text)
            '========================

            Me.Refresh()
            Call awal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then

            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    Call koneksii()
                    sql = "DELETE FROM tb_pelanggan WHERE id='" & idpelanggan & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'history user ===========
                    Call historysave("Menghapus Data Customer Kode" + txtkode.Text, txtkode.Text)
                    '========================
                    Me.Refresh()
                    Call awal()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkode.KeyPress
        e.Handled = ValidAngkaHuruf(e)
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call isitabel()
    End Sub

    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Sub cari()
        idpelanggan = GridView.GetFocusedRowCellValue("id")
        'menyiapkan variable byte() untuk menampung byte() dari foto yang ada di database
        Dim foto As Byte()

        Call koneksii()
        sql = "SELECT * FROM tb_pelanggan WHERE id  = '" + idpelanggan + "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            kodepelanggan = dr("kode_pelanggan")
            namapelanggan = dr("nama_pelanggan")
            alamatpelanggan = dr("alamat_pelanggan")
            teleponpelanggan = dr("telepon_pelanggan")
            keteranganpelanggan = dr("keterangan_pelanggan")
            foto = dr("foto_pelanggan")

            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox.Image = Image.FromStream(New IO.MemoryStream(foto))

            txtkode.Text = kodepelanggan
            txtnama.Text = namapelanggan
            txtalamat.Text = alamatpelanggan
            txttelp.Text = teleponpelanggan
            txtketerangan.Text = keteranganpelanggan
            txtgbr.Text = txtnama.Text

            'tombol
            btnrekening.Enabled = True

            btnedit.Enabled = True
            btnbatal.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
            btntambah.Text = "Tambah"
        End If
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        Call cari()
    End Sub

    Private Sub btnauto_Click(sender As Object, e As EventArgs) Handles btnauto.Click
        txtkode.Text = autonumber()
    End Sub

    Private Sub btngenerate_Click(sender As Object, e As EventArgs) Handles btngenerate.Click
        Dim r As New Random
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        txtkode.Text = sb.ToString()
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
            PictureBox.Image = resized
            txtgbr.Text = oD.SafeFileName
        End If
    End Sub

    Private Sub btnrekening_Click(sender As Object, e As EventArgs) Handles btnrekening.Click
        Dim rekening As Integer
        rekening = flogin.rekeningpelanggan
        If rekening > 0 Then
            frekeningpelanggan.idpelanggan = idpelanggan
            frekeningpelanggan.kodeakses = rekening
            frekeningpelanggan.ShowDialog()
        Else
            MsgBox("Anda tidak memiliki akses", MsgBoxStyle.Information, "Gagal")
        End If
    End Sub
End Class