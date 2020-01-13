Imports System.Data.Odbc

Public Class fuser
    Dim kode As String
    Dim cekmasterbarang, cekmastergudang, cekmastercustomer, cekmastersupplier, cekmasteruser, cekmasterkas, cekmasterpricelist, cekmasterreksupp, cekmasterrekcust As Integer
    Dim aksesbarang, aksesgudang, aksescustomer, aksessupplier, aksesuser, akseskas, aksespricelist, aksesreksupp, aksesrekcust As Integer
    Private Sub fuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Sub awal()
        txtkode.Clear()
        txtnama.Clear()
        txtpassword.Clear()
        txtalamat.Clear()
        cmbjabatan.SelectedIndex = -1
        txtemail.Clear()
        txttelp.Clear()
        txtalamat.Clear()
        txtketerangan.Clear()

        'akses user
        cbmasterbarang.Checked = False
        cbmastergudang.Checked = False
        cbmastercustomer.Checked = False
        cbmastersupplier.Checked = False
        cbmasteruser.Checked = False
        cbmasterkas.Checked = False
        cbmasterpricelist.Checked = False
        cbmasterreksupp.Checked = False
        cbmasterrekcust.Checked = False

        For id As Integer = 0 To 2
            clbmasterbarang.SetItemChecked(id, False)
            clbmastergudang.SetItemChecked(id, False)
            clbmastercustomer.SetItemChecked(id, False)
            clbmastersupplier.SetItemChecked(id, False)
            clbmasteruser.SetItemChecked(id, False)
            clbmasterkas.SetItemChecked(id, False)
            clbmasterpricelist.SetItemChecked(id, False)
            clbmasterreksupp.SetItemChecked(id, False)
            clbmasterrekcust.SetItemChecked(id, False)
        Next

        cbmasterbarang.Enabled = False
        cbmastergudang.Enabled = False
        cbmastercustomer.Enabled = False
        cbmastersupplier.Enabled = False
        cbmasteruser.Enabled = False
        cbmasterkas.Enabled = False
        cbmasterpricelist.Enabled = False
        cbmasterreksupp.Enabled = False
        cbmasterrekcust.Enabled = False

        clbmasterbarang.Enabled = False
        clbmastergudang.Enabled = False
        clbmastercustomer.Enabled = False
        clbmastersupplier.Enabled = False
        clbmasteruser.Enabled = False
        clbmasterkas.Enabled = False
        clbmasterpricelist.Enabled = False
        clbmasterreksupp.Enabled = False
        clbmasterrekcust.Enabled = False

        Call koneksii()

        txtkode.Enabled = False
        txtnama.Enabled = False
        txtpassword.Enabled = False
        cmbjabatan.Enabled = False
        txtemail.Enabled = False
        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl.Enabled = True
        Call isitabel()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode User"
        GridColumn1.Width = 40
        GridColumn1.FieldName = "kode_user"
        GridColumn2.Caption = "Nama user"
        GridColumn2.Width = 60
        GridColumn2.FieldName = "nama_user"
        GridColumn3.Caption = "Alamat"
        GridColumn3.Width = 80
        GridColumn3.FieldName = "alamat_user"
        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 40
        GridColumn4.FieldName = "telepon_user"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 70
        GridColumn5.FieldName = "keterangan_user"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "Select * from tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)
        Call kolom()
    End Sub
    Sub index()
        txtkode.TabIndex = 1
        txtnama.TabIndex = 2
        txtpassword.TabIndex = 3
        cmbjabatan.TabIndex = 4
        txtemail.TabIndex = 5
        txttelp.TabIndex = 6
        txtalamat.TabIndex = 7
        txtketerangan.TabIndex = 8
    End Sub
    Sub enable_text()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtpassword.Enabled = True
        cmbjabatan.Enabled = True
        txtemail.Enabled = True
        txtalamat.Enabled = True
        txttelp.Enabled = True
        txtketerangan.Enabled = True

        'akses user
        cbmasterbarang.Enabled = True
        cbmastergudang.Enabled = True
        cbmastercustomer.Enabled = True
        cbmastersupplier.Enabled = True
        cbmasteruser.Enabled = True
        cbmasterkas.Enabled = True
        cbmasterpricelist.Enabled = True
        cbmasterreksupp.Enabled = True
        cbmasterrekcust.Enabled = True

        'batas akses user

        txtkode.Focus()
    End Sub

    Sub enable_text_edit()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtpassword.Enabled = True
        cmbjabatan.Enabled = True
        txtemail.Enabled = True
        txtalamat.Enabled = True
        txttelp.Enabled = True
        txtketerangan.Enabled = True


        'akses user
        cbmasterbarang.Enabled = True
        cbmastergudang.Enabled = True
        cbmastercustomer.Enabled = True
        cbmastersupplier.Enabled = True
        cbmasteruser.Enabled = True
        cbmasterkas.Enabled = True
        cbmasterpricelist.Enabled = True
        cbmasterreksupp.Enabled = True
        cbmasterrekcust.Enabled = True

        If aksesbarang > 0 Then
            clbmasterbarang.Enabled = True
        Else
            clbmasterbarang.Enabled = False
        End If

        If aksesgudang > 0 Then
            clbmastergudang.Enabled = True
        Else
            clbmastergudang.Enabled = False
        End If

        If aksescustomer > 0 Then
            clbmastercustomer.Enabled = True
        Else
            clbmastercustomer.Enabled = False
        End If

        If aksessupplier > 0 Then
            clbmastersupplier.Enabled = True
        Else
            clbmastersupplier.Enabled = False
        End If

        If aksesuser > 0 Then
            clbmasteruser.Enabled = True
        Else
            clbmasteruser.Enabled = False
        End If

        If akseskas > 0 Then
            clbmasterkas.Enabled = True
        Else
            clbmasterkas.Enabled = False
        End If

        If aksespricelist > 0 Then
            clbmasterpricelist.Enabled = True
        Else
            clbmasterpricelist.Enabled = False
        End If

        If aksesreksupp > 0 Then
            clbmasterreksupp.Enabled = True
        Else
            clbmasterreksupp.Enabled = False
        End If

        If aksesrekcust > 0 Then
            clbmasterrekcust.Enabled = True
        Else
            clbmasterrekcust.Enabled = False
        End If
        'batas akses user

        txtkode.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            Call index()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("Kode belum terisi !")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama belum terisi !")
                Else
                    If txtpassword.Text.Length = 0 Then
                        MsgBox("Password belum terisi !")
                    Else
                        If cmbjabatan.Text.Length = 0 Then
                            MsgBox("Jabatan belum terisi !")
                        Else
                            If txtemail.Text.Length = 0 Then
                                MsgBox("Email belum terisi !")
                            Else
                                If txttelp.Text.Length = 0 Then
                                    MsgBox("Telepon belum terisi !")
                                Else
                                    If txtalamat.Text.Length = 0 Then
                                        MsgBox("Alamat belum terisi !")
                                    Else
                                        Call simpan()
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Sub aksesadmin()
        cekmasterbarang = 0
        cekmastergudang = 0
        cekmastercustomer = 0
        cekmastersupplier = 0
        cekmasteruser = 0
        cekmasterkas = 0
        cekmasterpricelist = 0
        cekmasterreksupp = 0
        cekmasterrekcust = 0

        'We will run through each indice
        For i = 0 To 2
            'barang
            If clbmasterbarang.GetItemChecked(i) Then
                If clbmasterbarang.Items(i).Equals("Tambah") Then
                    cekmasterbarang = cekmasterbarang + 1
                ElseIf clbmasterbarang.Items(i).Equals("Edit") Then
                    cekmasterbarang = cekmasterbarang + 3
                ElseIf clbmasterbarang.Items(i).Equals("Hapus") Then
                    cekmasterbarang = cekmasterbarang + 5
                End If
            Else
                cekmasterbarang = cekmasterbarang + 0
            End If
            'gudang
            If clbmastergudang.GetItemChecked(i) Then
                If clbmastergudang.Items(i).Equals("Tambah") Then
                    cekmastergudang = cekmastergudang + 1
                ElseIf clbmastergudang.Items(i).Equals("Edit") Then
                    cekmastergudang = cekmastergudang + 3
                ElseIf clbmastergudang.Items(i).Equals("Hapus") Then
                    cekmastergudang = cekmastergudang + 5
                End If
            Else
                cekmastergudang = cekmastergudang + 0
            End If
            'customer
            If clbmastercustomer.GetItemChecked(i) Then
                If clbmastercustomer.Items(i).Equals("Tambah") Then
                    cekmastercustomer = cekmastercustomer + 1
                ElseIf clbmastercustomer.Items(i).Equals("Edit") Then
                    cekmastercustomer = cekmastercustomer + 3
                ElseIf clbmastercustomer.Items(i).Equals("Hapus") Then
                    cekmastercustomer = cekmastercustomer + 5
                End If
            Else
                cekmastercustomer = cekmastercustomer + 0
            End If
            'supplier
            If clbmastersupplier.GetItemChecked(i) Then
                If clbmastersupplier.Items(i).Equals("Tambah") Then
                    cekmastersupplier = cekmastersupplier + 1
                ElseIf clbmastersupplier.Items(i).Equals("Edit") Then
                    cekmastersupplier = cekmastersupplier + 3
                ElseIf clbmastersupplier.Items(i).Equals("Hapus") Then
                    cekmastersupplier = cekmastersupplier + 5
                End If
            Else
                cekmastersupplier = cekmastersupplier + 0
            End If
            'user
            If clbmasteruser.GetItemChecked(i) Then
                If clbmasteruser.Items(i).Equals("Tambah") Then
                    cekmasteruser = cekmasteruser + 1
                ElseIf clbmasteruser.Items(i).Equals("Edit") Then
                    cekmasteruser = cekmasteruser + 3
                ElseIf clbmasteruser.Items(i).Equals("Hapus") Then
                    cekmasteruser = cekmasteruser + 5
                End If
            Else
                cekmasteruser = cekmasteruser + 0
            End If
            'kas
            If clbmasterkas.GetItemChecked(i) Then
                If clbmasterkas.Items(i).Equals("Tambah") Then
                    cekmasterkas = cekmasterkas + 1
                ElseIf clbmasterkas.Items(i).Equals("Edit") Then
                    cekmasterkas = cekmasterkas + 3
                ElseIf clbmasterkas.Items(i).Equals("Hapus") Then
                    cekmasterkas = cekmasterkas + 5
                End If
            Else
                cekmasterkas = cekmasterkas + 0
            End If
            'pricelist
            If clbmasterpricelist.GetItemChecked(i) Then
                If clbmasterpricelist.Items(i).Equals("Tambah") Then
                    cekmasterpricelist = cekmasterpricelist + 1
                ElseIf clbmasterpricelist.Items(i).Equals("Edit") Then
                    cekmasterpricelist = cekmasterpricelist + 3
                ElseIf clbmasterpricelist.Items(i).Equals("Hapus") Then
                    cekmasterpricelist = cekmasterpricelist + 5
                End If
            Else
                cekmasterpricelist = cekmasterpricelist + 0
            End If
            'rekening supp
            If clbmasterreksupp.GetItemChecked(i) Then
                If clbmasterreksupp.Items(i).Equals("Tambah") Then
                    cekmasterreksupp = cekmasterreksupp + 1
                ElseIf clbmasterreksupp.Items(i).Equals("Edit") Then
                    cekmasterreksupp = cekmasterreksupp + 3
                ElseIf clbmasterreksupp.Items(i).Equals("Hapus") Then
                    cekmasterreksupp = cekmasterreksupp + 5
                End If
            Else
                cekmasterreksupp = cekmasterreksupp + 0
            End If
            'rekening cust
            If clbmasterrekcust.GetItemChecked(i) Then
                If clbmasterrekcust.Items(i).Equals("Tambah") Then
                    cekmasterrekcust = cekmasterrekcust + 1
                ElseIf clbmasterrekcust.Items(i).Equals("Edit") Then
                    cekmasterrekcust = cekmasterrekcust + 3
                ElseIf clbmasterrekcust.Items(i).Equals("Hapus") Then
                    cekmasterrekcust = cekmasterrekcust + 5
                End If
            Else
                cekmasterrekcust = cekmasterrekcust + 0
            End If
        Next
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode User Sudah ada dengan nama " + dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            Call aksesadmin()
            sql = "INSERT INTO tb_user (kode_user, nama_user, password_user, jabatan_user, email_user, telepon_user, alamat_user, keterangan_user, master_barang, created_by, updated_by,date_created, last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtpassword.Text & "', '" & cmbjabatan.Text & "', '" & txtemail.Text & "', '" & txttelp.Text & "','" & txtalamat.Text & "','" & txtketerangan.Text & "','" & cekmasterbarang & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If
        'Call koneksii()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            Call enable_text_edit()
            Call index()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("Kode belum terisi !")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama belum terisi !")
                Else
                    If txtpassword.Text.Length = 0 Then
                        MsgBox("Password belum terisi !")
                    Else
                        If cmbjabatan.Text.Length = 0 Then
                            MsgBox("Jabatan belum terisi !")
                        Else
                            If txtemail.Text.Length = 0 Then
                                MsgBox("Email belum terisi !")
                            Else
                                If txttelp.Text.Length = 0 Then
                                    MsgBox("Telepon belum terisi !")
                                Else
                                    If txtalamat.Text.Length = 0 Then
                                        MsgBox("Alamat belum terisi !")
                                    Else
                                        Call edit()
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Sub edit()
        If txtkode.Text.Equals(kode) Then
            Call aksesadmin()
            Using cnn As New OdbcConnection(strConn)
                sql = "UPDATE tb_user SET nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, master_barang=?, updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
                cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
                cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
                cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
                cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
                cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
                cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
                cmmd.Parameters.AddWithValue("@master_barang", cekmasterbarang)
                cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
                cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                cnn.Open()
                cmmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Data Terupdate", MsgBoxStyle.Information, "Berhasil")
                btnedit.Text = "Edit"
                cnn.Close()
                Me.Refresh()
                Call awal()
            End Using
        Else

            sql = "SELECT * FROM tb_user WHERE kode_user  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode User Sudah ada dengan nama " + dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
            Else
                Call aksesadmin()
                Using cnn As New OdbcConnection(strConn)
                    sql = "UPDATE tb_user SET kode_user=?, nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, master_barang=?, updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    cmmd.Parameters.AddWithValue("@kode_user", txtkode.Text)
                    cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
                    cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
                    cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
                    cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
                    cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
                    cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
                    cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
                    cmmd.Parameters.AddWithValue("@master_barang", cekmasterbarang)
                    cmmd.Parameters.AddWithValue("@updated_by", fmenu.statususer.Text)
                    cmmd.Parameters.AddWithValue("@last_updated", Date.Now)
                    cnn.Open()
                    cmmd.ExecuteNonQuery()
                    cnn.Close()
                    MsgBox("Data terupdate", MsgBoxStyle.Information, "Berhasil")
                    btnedit.Text = "Edit"
                    cnn.Close()
                    Me.Refresh()
                    Call awal()
                End Using
            End If
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_user WHERE  kode_user='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Refresh()
            Call awal()
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Sub cari()


        txtkode.Text = GridView.GetFocusedRowCellValue("kode_user")
        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_user WHERE kode_user  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                kode = dr("kode_user")
                txtnama.Text = dr("nama_user")
                txtpassword.Text = dr("password_user")
                cmbjabatan.Text = dr("jabatan_user")
                txtemail.Text = dr("email_user")
                txttelp.Text = dr("telepon_user")
                txtalamat.Text = dr("alamat_user")
                txtketerangan.Text = dr("keterangan_user")

                'akses user
                aksesbarang = Val(dr("master_barang"))



                Select Case aksesbarang
                    Case 0
                        cbmasterbarang.Checked = False
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 1
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 3
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 5
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, True)
                    Case 4
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, False)
                    Case 6
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, False)
                        clbmasterbarang.SetItemChecked(2, True)
                    Case 8
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, False)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, True)
                    Case 9
                        cbmasterbarang.Checked = True
                        clbmasterbarang.SetItemChecked(0, True)
                        clbmasterbarang.SetItemChecked(1, True)
                        clbmasterbarang.SetItemChecked(2, True)
                End Select
                clbmasterbarang.Enabled = False

                'end

                btnedit.Enabled = True
                btnbatal.Enabled = True
                btnhapus.Enabled = True
                btntambah.Enabled = False
                btntambah.Text = "Tambah"
                cnn.Close()
            End If
        End Using
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        Call cari()
    End Sub

    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub cbmasterbarang_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterbarang.CheckedChanged
        If cbmasterbarang.Checked = True Then
            clbmasterbarang.Enabled = True
            For id As Integer = 0 To clbmasterbarang.Items.Count - 1
                Me.clbmasterbarang.SetItemChecked(id, True)
            Next
        ElseIf cbmasterbarang.Checked = False Then
            clbmasterbarang.Enabled = False
            For id As Integer = 0 To clbmasterbarang.Items.Count - 1
                Me.clbmasterbarang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub clbmasterbarang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterbarang.MouseDown
        Dim Index As Integer = clbmasterbarang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterbarang.SetItemChecked(Index, Not clbmasterbarang.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterbarang.Items.Count - 1
            If clbmasterbarang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterbarang.Enabled = False
            cbmasterbarang.Checked = False
        End If
    End Sub
End Class