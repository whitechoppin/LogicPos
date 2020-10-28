
Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class fbarang
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean
    Dim hargabarang, modalbarang As Double
    Public passwrodstatus As Boolean = False
    Dim idbarang, kodebarang, namabarang, satuanbarang, jenisbarang, kategoribarang, keteranganbarang As String

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

    Private Sub fbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Call historysave("Membuka Master Barang", "N/A")
    End Sub

    Sub awal()
        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        btnauto.Enabled = False
        txtkode.Enabled = False
        txtkode.Clear()
        txtnama.Enabled = False
        txtnama.Clear()
        cmbsatuan.Enabled = False
        cmbsatuan.SelectedIndex = -1
        cmbjenis.Enabled = False
        cmbjenis.SelectedIndex = -1
        cmbkategori.Enabled = False
        cmbkategori.SelectedIndex = -1
        txtmodal.Text = 0
        txtmodal.Enabled = False
        btnshow.Enabled = False
        txtketerangan.Enabled = False
        txtketerangan.Clear()


        btntambah.Enabled = True
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnbatal.Enabled = False

        'txtkode.MaxLength = 20
        'txtnama.MaxLength = 20

        GridControl.Enabled = True
        Call comboboxkategori()

        Call isitabel()
        txtgbr.Text = ""
        PictureBox.Image = ImageList.Images(0)
        btnupload.Enabled = False
    End Sub

    Sub comboboxkategori()
        Call koneksii()
        sql = "SELECT * FROM tb_kategori_barang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbkategori.DataSource = ds.Tables(0)
        cmbkategori.ValueMember = "id"
        cmbkategori.DisplayMember = "kode_kategori"
    End Sub

    Sub index()
        txtkode.TabIndex = 1
        btnauto.TabIndex = 2
        txtnama.TabIndex = 3
        cmbjenis.TabIndex = 4
        cmbsatuan.TabIndex = 5
        cmbkategori.TabIndex = 6
        txtketerangan.TabIndex = 7
        txtmodal.TabIndex = 8
        btnupload.TabIndex = 9
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_barang JOIN tb_kategori_barang ON tb_barang.kategori_barang_id = tb_kategori_barang.id"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)

        GridColumn1.Caption = "Kode"
        GridColumn1.FieldName = "kode_barang"
        GridColumn1.Width = 40

        GridColumn2.Caption = "Nama Barang"
        GridColumn2.FieldName = "nama_barang"
        GridColumn2.Width = 80

        GridColumn3.Caption = "Jenis"
        GridColumn3.FieldName = "jenis_barang"
        GridColumn3.Width = 40

        GridColumn4.Caption = "Satuan"
        GridColumn4.FieldName = "satuan_barang"
        GridColumn4.Width = 40

        GridColumn5.Caption = "Modal"
        GridColumn5.FieldName = "modal_barang"
        GridColumn5.Width = 60
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "Rp ##,#0"

        GridColumn6.Caption = "Kategori"
        GridColumn6.FieldName = "nama_kategori"
        GridColumn6.Width = 40

        GridColumn7.Caption = "id"
        GridColumn7.FieldName = "id"
        GridColumn7.Width = 30
        GridColumn7.Visible = False

        GridControl.Visible = True
    End Sub
    Sub enable_text()
        btnauto.Enabled = True
        txtkode.Enabled = True
        txtnama.Enabled = True
        cmbjenis.Enabled = True
        cmbsatuan.Enabled = True
        cmbkategori.Enabled = True
        txtketerangan.Enabled = True
        btnupload.Enabled = True
        txtmodal.Enabled = True
        btnshow.Enabled = True
        txtkode.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
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
                        If cmbsatuan.SelectedIndex = -1 Then
                            MsgBox("Satuan belum terpilih !")
                        Else
                            If cmbjenis.SelectedIndex = -1 Then
                                MsgBox("Jenis belum dipilih !")
                            Else
                                If cmbkategori.SelectedIndex = -1 Then
                                    MsgBox("Kategori belum dipilih !")
                                Else
                                    Call simpan()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak Ada Akses")
        End If
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE kode_barang = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode barang Sudah ada dengan nama " + dr("nama_barang"), MsgBoxStyle.Critical, "Pemberitahuan")
            txtkode.Focus()
        Else
            'menyiapkan MemoryStream
            Dim ms As MemoryStream = New MemoryStream
            'menyimpan gambar ke dalam ms dengan format jpeg
            PictureBox.Image.Save(ms, Imaging.ImageFormat.Jpeg)
            'merubah gambar pada ms ke array
            ms.ToArray()

            Try
                Call koneksii()
                sql = "INSERT INTO tb_barang (kode_barang, nama_barang, jenis_barang, kategori_barang_id, satuan_barang, keterangan_barang, modal_barang, gambar_barang, created_by, updated_by, date_created, last_updated) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@kode_barang", txtkode.Text)
                cmmd.Parameters.AddWithValue("@nama_barang", txtnama.Text)
                cmmd.Parameters.AddWithValue("@jenis_barang", cmbjenis.Text)
                cmmd.Parameters.AddWithValue("@kategori_barang_id", cmbkategori.SelectedValue)
                cmmd.Parameters.AddWithValue("@satuan_barang", cmbsatuan.Text)
                cmmd.Parameters.AddWithValue("@keterangan_barang", txtketerangan.Text)
                cmmd.Parameters.AddWithValue("@modal_barang", hargabarang)
                cmmd.Parameters.AddWithValue("@gambar_barang", ms.ToArray)
                cmmd.Parameters.AddWithValue("@created_by", fmenu.kodeuser.Text)
                cmmd.Parameters.AddWithValue("@updated_by", fmenu.kodeuser.Text)
                cmmd.Parameters.AddWithValue("@date_created", Date.Now)
                cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                cmmd.ExecuteNonQuery()
                MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")

                'history user ==========
                Call historysave("Menyimpan Data Barang Kode " + txtkode.Text, txtkode.Text)
                '========================

                btntambah.Text = "Tambah"
                Me.Refresh()
                Call awal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Sub isi_satuan()
        cmbsatuan.Items.Clear()
        cmbsatuan.AutoCompleteCustomSource.Clear()
        If cmbjenis.Text.Equals("Roll") Then
            cmbsatuan.Items.Clear()
            cmbsatuan.AutoCompleteCustomSource.Clear()
            cmbsatuan.AutoCompleteCustomSource.Add("Meter")
            cmbsatuan.Items.Add("Meter")
            cmbsatuan.AutoCompleteCustomSource.Add("Centimeter")
            cmbsatuan.Items.Add("Centimeter")
            cmbsatuan.SelectedIndex = 0
        ElseIf cmbjenis.Text.Equals("Satuan") Then
            cmbsatuan.Items.Clear()
            cmbsatuan.AutoCompleteCustomSource.Clear()
            cmbsatuan.AutoCompleteCustomSource.Add("Pcs")
            cmbsatuan.Items.Add("Pcs")
            cmbsatuan.SelectedIndex = 0
        End If
    End Sub
    Sub cari()
        idbarang = GridView.GetFocusedRowCellValue("id")
        'Menyiapkan variable byte() untuk menampung byte() dari foto yang ada di database
        Dim foto As Byte()

        Call koneksii()
        sql = "SELECT * FROM tb_barang WHERE id = '" & idbarang & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            kodebarang = dr("kode_barang")
            namabarang = dr("nama_barang")
            foto = dr("gambar_barang")
            modalbarang = dr("modal_barang")
            satuanbarang = dr("satuan_barang")
            jenisbarang = dr("jenis_barang")
            kategoribarang = dr("kategori_barang_id")
            keteranganbarang = dr("keterangan_barang")

            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox.Image = Image.FromStream(New IO.MemoryStream(foto))

            txtkode.Text = kodebarang
            txtnama.Text = namabarang
            cmbsatuan.Text = satuanbarang
            cmbjenis.Text = jenisbarang
            cmbkategori.SelectedValue = kategoribarang
            txtketerangan.Text = keteranganbarang
            txtgbr.Text = txtnama.Text
            txtmodal.Text = Format(modalbarang, "##,##0")

            'tombol
            btnshow.Enabled = True

            btnedit.Enabled = True
            btnbatal.Enabled = True
            btnhapus.Enabled = True
            btntambah.Enabled = False
        End If
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
        If editstatus.Equals(True) Then
            If btnedit.Text = "Edit" Then
                btnedit.Text = "Simpan"
                btnhapus.Enabled = False
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
                        If cmbsatuan.SelectedIndex = -1 Then
                            MsgBox("Satuan belum terpilih !")
                        Else
                            If cmbjenis.SelectedIndex = -1 Then
                                MsgBox("Kategori belum terisi !")
                            Else
                                If cmbjenis.SelectedIndex = -1 Then
                                    MsgBox("Jenis belum dipilih !")
                                Else
                                    Call edit()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub perbaharui()
        Dim ms As MemoryStream = New MemoryStream
        'menyimpan gambar ke dalam ms dengan format jpeg
        PictureBox.Image.Save(ms, Imaging.ImageFormat.Jpeg)
        ms.ToArray()

        Try
            Call koneksii()
            sql = "UPDATE tb_barang SET kode_barang=?, nama_barang=?, jenis_barang=?, kategori_barang_id=?, satuan_barang=?, keterangan_barang=?, modal_barang=?, gambar_barang=?, updated_by=?, last_updated=? WHERE  id='" & idbarang & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_barang", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_barang", txtnama.Text)
            cmmd.Parameters.AddWithValue("@jenis_barang", cmbjenis.Text)
            cmmd.Parameters.AddWithValue("@kategori_barang_id", cmbkategori.SelectedValue)
            cmmd.Parameters.AddWithValue("@satuan_barang", cmbsatuan.Text)
            cmmd.Parameters.AddWithValue("@keterangan_barang", txtketerangan.Text)
            cmmd.Parameters.AddWithValue("@modal_barang", hargabarang)
            cmmd.Parameters.AddWithValue("@gambar_barang", ms.ToArray)
            cmmd.Parameters.AddWithValue("@updated_by", fmenu.kodeuser.Text)
            cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
            cmmd.ExecuteNonQuery()

            MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            'history user ==========
            Call historysave("Mengedit Data Barang Kode " + txtkode.Text, txtkode.Text)
            '========================

            Me.Refresh()
            Call awal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub edit()
        If txtkode.Text.Equals(kodebarang) Then
            Call perbaharui()
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_barang WHERE kode_barang  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode barang Sudah ada dengan nama " + dr("nama_barang"), MsgBoxStyle.Exclamation, "Pemberitahuan")
                txtkode.Focus()
            Else
                Call perbaharui()
            End If
        End If
    End Sub

    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkode.KeyPress
        e.Handled = ValidAngkaHuruf(e)
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call isitabel()
    End Sub

    Private Sub fbarang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    Call koneksii()
                    sql = "DELETE FROM tb_barang WHERE id='" & idbarang & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'history user ===========
                    Call historysave("Menghapus Data Barang Kode" + txtkode.Text, txtkode.Text)
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
            hargabarang = txtmodal.Text
            txtmodal.Text = Format(hargabarang, "##,##0")
            txtmodal.SelectionStart = Len(txtmodal.Text)
        End If
    End Sub

    Private Sub cmbjenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbjenis.SelectedIndexChanged
        Call isi_satuan()
    End Sub

    Private Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        If txthidden.Visible = True Then
            passwordid = 1
            fpassword.ShowDialog()
        ElseIf txthidden.Visible = False Then
            txthidden.Visible = True
        End If
    End Sub

    Private Sub txtmodal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmodal.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
End Class