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
        sql = "SELECT * FROM tb_user"
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
            sql = "INSERT INTO tb_user (kode_user, nama_user, password_user, jabatan_user, email_user, telepon_user, alamat_user, keterangan_user, master_barang, master_gudang, master_customer, master_supplier, master_user, master_kas, master_pricelist, master_rek_supp, master_rek_cust, created_by, updated_by,date_created, last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtpassword.Text & "', '" & cmbjabatan.Text & "', '" & txtemail.Text & "', '" & txttelp.Text & "','" & txtalamat.Text & "','" & txtketerangan.Text & "','" & cekmasterbarang & "','" & cekmastergudang & "','" & cekmastercustomer & "','" & cekmastersupplier & "','" & cekmasteruser & "','" & cekmasterkas & "','" & cekmasterpricelist & "','" & cekmasterreksupp & "','" & cekmasterrekcust & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
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
                sql = "UPDATE tb_user SET nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, master_barang=?, master_gudang=?, master_customer=?, master_supplier=?, master_user=?, master_kas=?, master_pricelist=?, master_rek_supp=?, master_rek_cust=?, updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
                cmmd = New OdbcCommand(sql, cnn)
                cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
                cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
                cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
                cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
                cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
                cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
                cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
                cmmd.Parameters.AddWithValue("@master_barang", cekmasterbarang)
                cmmd.Parameters.AddWithValue("@master_gudang", cekmastergudang)
                cmmd.Parameters.AddWithValue("@master_customer", cekmastercustomer)
                cmmd.Parameters.AddWithValue("@master_supplier", cekmastersupplier)
                cmmd.Parameters.AddWithValue("@master_user", cekmasteruser)
                cmmd.Parameters.AddWithValue("@master_kas", cekmasterkas)
                cmmd.Parameters.AddWithValue("@master_pricelist", cekmasterpricelist)
                cmmd.Parameters.AddWithValue("@master_rek_supp", cekmasterreksupp)
                cmmd.Parameters.AddWithValue("@master_rek_cust", cekmastercustomer)
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
                    sql = "UPDATE tb_user SET kode_user=?, nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, master_barang=?, master_gudang=?, master_customer=?, master_supplier=?, master_user=?, master_kas=?, master_pricelist=?, master_rek_supp=?, master_rek_cust=?, updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
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
                    cmmd.Parameters.AddWithValue("@master_gudang", cekmastergudang)
                    cmmd.Parameters.AddWithValue("@master_customer", cekmastercustomer)
                    cmmd.Parameters.AddWithValue("@master_supplier", cekmastersupplier)
                    cmmd.Parameters.AddWithValue("@master_user", cekmasteruser)
                    cmmd.Parameters.AddWithValue("@master_kas", cekmasterkas)
                    cmmd.Parameters.AddWithValue("@master_pricelist", cekmasterpricelist)
                    cmmd.Parameters.AddWithValue("@master_rek_supp", cekmasterreksupp)
                    cmmd.Parameters.AddWithValue("@master_rek_cust", cekmastercustomer)
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
                aksesgudang = Val(dr("master_gudang"))
                aksescustomer = Val(dr("master_customer"))
                aksessupplier = Val(dr("master_supplier"))
                aksesuser = Val(dr("master_user"))
                akseskas = Val(dr("master_kas"))
                aksespricelist = Val(dr("master_pricelist"))
                aksesreksupp = Val(dr("master_rek_supp"))
                aksesrekcust = Val(dr("master_rek_cust"))

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

                Select Case aksesgudang
                    Case 0
                        cbmastergudang.Checked = False
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 1
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 3
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 5
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, True)
                    Case 4
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, False)
                    Case 6
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, False)
                        clbmastergudang.SetItemChecked(2, True)
                    Case 8
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, False)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, True)
                    Case 9
                        cbmastergudang.Checked = True
                        clbmastergudang.SetItemChecked(0, True)
                        clbmastergudang.SetItemChecked(1, True)
                        clbmastergudang.SetItemChecked(2, True)
                End Select

                Select Case aksescustomer
                    Case 0
                        cbmastercustomer.Checked = False
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 1
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 3
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 5
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, True)
                    Case 4
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, False)
                    Case 6
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, False)
                        clbmastercustomer.SetItemChecked(2, True)
                    Case 8
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, False)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, True)
                    Case 9
                        cbmastercustomer.Checked = True
                        clbmastercustomer.SetItemChecked(0, True)
                        clbmastercustomer.SetItemChecked(1, True)
                        clbmastercustomer.SetItemChecked(2, True)
                End Select

                Select Case aksessupplier
                    Case 0
                        cbmastersupplier.Checked = False
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 1
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 3
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 5
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, True)
                    Case 4
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, False)
                    Case 6
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, False)
                        clbmastersupplier.SetItemChecked(2, True)
                    Case 8
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, False)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, True)
                    Case 9
                        cbmastersupplier.Checked = True
                        clbmastersupplier.SetItemChecked(0, True)
                        clbmastersupplier.SetItemChecked(1, True)
                        clbmastersupplier.SetItemChecked(2, True)
                End Select

                Select Case aksesuser
                    Case 0
                        cbmasteruser.Checked = False
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 1
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 3
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 5
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, True)
                    Case 4
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, False)
                    Case 6
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, False)
                        clbmasteruser.SetItemChecked(2, True)
                    Case 8
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, False)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, True)
                    Case 9
                        cbmasteruser.Checked = True
                        clbmasteruser.SetItemChecked(0, True)
                        clbmasteruser.SetItemChecked(1, True)
                        clbmasteruser.SetItemChecked(2, True)
                End Select

                Select Case akseskas
                    Case 0
                        cbmasterkas.Checked = False
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 1
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 3
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 5
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, True)
                    Case 4
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, False)
                    Case 6
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, False)
                        clbmasterkas.SetItemChecked(2, True)
                    Case 8
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, False)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, True)
                    Case 9
                        cbmasterkas.Checked = True
                        clbmasterkas.SetItemChecked(0, True)
                        clbmasterkas.SetItemChecked(1, True)
                        clbmasterkas.SetItemChecked(2, True)
                End Select

                Select Case aksespricelist
                    Case 0
                        cbmasterpricelist.Checked = False
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 1
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 3
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 5
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, True)
                    Case 4
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, False)
                    Case 6
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, False)
                        clbmasterpricelist.SetItemChecked(2, True)
                    Case 8
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, False)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, True)
                    Case 9
                        cbmasterpricelist.Checked = True
                        clbmasterpricelist.SetItemChecked(0, True)
                        clbmasterpricelist.SetItemChecked(1, True)
                        clbmasterpricelist.SetItemChecked(2, True)
                End Select

                Select Case aksesreksupp
                    Case 0
                        cbmasterreksupp.Checked = False
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 1
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 3
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 5
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, True)
                    Case 4
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, False)
                    Case 6
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, False)
                        clbmasterreksupp.SetItemChecked(2, True)
                    Case 8
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, False)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, True)
                    Case 9
                        cbmasterreksupp.Checked = True
                        clbmasterreksupp.SetItemChecked(0, True)
                        clbmasterreksupp.SetItemChecked(1, True)
                        clbmasterreksupp.SetItemChecked(2, True)
                End Select

                Select Case aksesrekcust
                    Case 0
                        cbmasterrekcust.Checked = False
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 1
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 3
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 5
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, True)
                    Case 4
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, False)
                    Case 6
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, False)
                        clbmasterrekcust.SetItemChecked(2, True)
                    Case 8
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, False)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, True)
                    Case 9
                        cbmasterrekcust.Checked = True
                        clbmasterrekcust.SetItemChecked(0, True)
                        clbmasterrekcust.SetItemChecked(1, True)
                        clbmasterrekcust.SetItemChecked(2, True)
                End Select

                clbmasterbarang.Enabled = False
                clbmastergudang.Enabled = False
                clbmastercustomer.Enabled = False
                clbmastersupplier.Enabled = False
                clbmasteruser.Enabled = False
                clbmasterkas.Enabled = False
                clbmasterpricelist.Enabled = False
                clbmasterreksupp.Enabled = False
                clbmasterrekcust.Enabled = False

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

    Private Sub cbmastergudang_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastergudang.CheckedChanged
        If cbmastergudang.Checked = True Then
            clbmastergudang.Enabled = True
            For id As Integer = 0 To clbmastergudang.Items.Count - 1
                Me.clbmastergudang.SetItemChecked(id, True)
            Next
        ElseIf cbmastergudang.Checked = False Then
            clbmastergudang.Enabled = False
            For id As Integer = 0 To clbmastergudang.Items.Count - 1
                Me.clbmastergudang.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastercustomer_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastercustomer.CheckedChanged
        If cbmastercustomer.Checked = True Then
            clbmastercustomer.Enabled = True
            For id As Integer = 0 To clbmastercustomer.Items.Count - 1
                Me.clbmastercustomer.SetItemChecked(id, True)
            Next
        ElseIf cbmastercustomer.Checked = False Then
            clbmastercustomer.Enabled = False
            For id As Integer = 0 To clbmastercustomer.Items.Count - 1
                Me.clbmastercustomer.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmastersupplier_CheckedChanged(sender As Object, e As EventArgs) Handles cbmastersupplier.CheckedChanged
        If cbmastersupplier.Checked = True Then
            clbmastersupplier.Enabled = True
            For id As Integer = 0 To clbmastersupplier.Items.Count - 1
                Me.clbmastersupplier.SetItemChecked(id, True)
            Next
        ElseIf cbmastersupplier.Checked = False Then
            clbmastersupplier.Enabled = False
            For id As Integer = 0 To clbmastersupplier.Items.Count - 1
                Me.clbmastersupplier.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasteruser_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasteruser.CheckedChanged
        If cbmasteruser.Checked = True Then
            clbmasteruser.Enabled = True
            For id As Integer = 0 To clbmasteruser.Items.Count - 1
                Me.clbmasteruser.SetItemChecked(id, True)
            Next
        ElseIf cbmasteruser.Checked = False Then
            clbmasteruser.Enabled = False
            For id As Integer = 0 To clbmasteruser.Items.Count - 1
                Me.clbmasteruser.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterkas_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterkas.CheckedChanged
        If cbmasterkas.Checked = True Then
            clbmasterkas.Enabled = True
            For id As Integer = 0 To clbmasterkas.Items.Count - 1
                Me.clbmasterkas.SetItemChecked(id, True)
            Next
        ElseIf cbmasterkas.Checked = False Then
            clbmasterkas.Enabled = False
            For id As Integer = 0 To clbmasterkas.Items.Count - 1
                Me.clbmasterkas.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterpricelist_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterpricelist.CheckedChanged
        If cbmasterpricelist.Checked = True Then
            clbmasterpricelist.Enabled = True
            For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
                Me.clbmasterpricelist.SetItemChecked(id, True)
            Next
        ElseIf cbmasterpricelist.Checked = False Then
            clbmasterpricelist.Enabled = False
            For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
                Me.clbmasterpricelist.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterreksupp_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterreksupp.CheckedChanged
        If cbmasterreksupp.Checked = True Then
            clbmasterreksupp.Enabled = True
            For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
                Me.clbmasterreksupp.SetItemChecked(id, True)
            Next
        ElseIf cbmasterreksupp.Checked = False Then
            clbmasterreksupp.Enabled = False
            For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
                Me.clbmasterreksupp.SetItemChecked(id, False)
            Next
        End If
    End Sub

    Private Sub cbmasterrekcust_CheckedChanged(sender As Object, e As EventArgs) Handles cbmasterrekcust.CheckedChanged
        If cbmasterrekcust.Checked = True Then
            clbmasterrekcust.Enabled = True
            For id As Integer = 0 To clbmasterrekcust.Items.Count - 1
                Me.clbmasterrekcust.SetItemChecked(id, True)
            Next
        ElseIf cbmasterrekcust.Checked = False Then
            clbmasterrekcust.Enabled = False
            For id As Integer = 0 To clbmasterrekcust.Items.Count - 1
                Me.clbmasterrekcust.SetItemChecked(id, False)
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

    Private Sub clbmastergudang_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastergudang.MouseDown
        Dim Index As Integer = clbmastergudang.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastergudang.SetItemChecked(Index, Not clbmastergudang.GetItemChecked(Index))

        For id As Integer = 0 To clbmastergudang.Items.Count - 1
            If clbmastergudang.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastergudang.Enabled = False
            cbmastergudang.Checked = False
        End If
    End Sub

    Private Sub clbmastercustomer_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastercustomer.MouseDown
        Dim Index As Integer = clbmastercustomer.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastercustomer.SetItemChecked(Index, Not clbmastercustomer.GetItemChecked(Index))

        For id As Integer = 0 To clbmastercustomer.Items.Count - 1
            If clbmastercustomer.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastercustomer.Enabled = False
            cbmastercustomer.Checked = False
        End If
    End Sub

    Private Sub clbmastersupplier_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmastersupplier.MouseDown
        Dim Index As Integer = clbmastersupplier.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmastersupplier.SetItemChecked(Index, Not clbmastersupplier.GetItemChecked(Index))

        For id As Integer = 0 To clbmastersupplier.Items.Count - 1
            If clbmastersupplier.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmastersupplier.Enabled = False
            cbmastersupplier.Checked = False
        End If
    End Sub

    Private Sub clbmasteruser_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasteruser.MouseDown
        Dim Index As Integer = clbmasteruser.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasteruser.SetItemChecked(Index, Not clbmasteruser.GetItemChecked(Index))

        For id As Integer = 0 To clbmasteruser.Items.Count - 1
            If clbmasteruser.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasteruser.Enabled = False
            cbmasteruser.Checked = False
        End If
    End Sub

    Private Sub clbmasterkas_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterkas.MouseDown
        Dim Index As Integer = clbmasterkas.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterkas.SetItemChecked(Index, Not clbmasterkas.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterkas.Items.Count - 1
            If clbmasterkas.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterkas.Enabled = False
            cbmasterkas.Checked = False
        End If
    End Sub

    Private Sub clbmasterpricelist_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterpricelist.MouseDown
        Dim Index As Integer = clbmasterpricelist.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterpricelist.SetItemChecked(Index, Not clbmasterpricelist.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterpricelist.Items.Count - 1
            If clbmasterpricelist.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterpricelist.Enabled = False
            cbmasterpricelist.Checked = False
        End If
    End Sub

    Private Sub clbmasterreksupp_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterreksupp.MouseDown
        Dim Index As Integer = clbmasterreksupp.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterreksupp.SetItemChecked(Index, Not clbmasterreksupp.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterreksupp.Items.Count - 1
            If clbmasterreksupp.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterreksupp.Enabled = False
            cbmasterreksupp.Checked = False
        End If
    End Sub

    Private Sub clbmasterrekcust_MouseDown(sender As Object, e As MouseEventArgs) Handles clbmasterrekcust.MouseDown
        Dim Index As Integer = clbmasterrekcust.IndexFromPoint(e.Location)
        Dim Counter As Integer = 0

        clbmasterrekcust.SetItemChecked(Index, Not clbmasterrekcust.GetItemChecked(Index))

        For id As Integer = 0 To clbmasterrekcust.Items.Count - 1
            If clbmasterrekcust.GetItemChecked(id) = True Then
                Counter = Counter + 1
            End If
        Next

        If Counter.Equals(0) Then
            clbmasterrekcust.Enabled = False
            cbmasterrekcust.Checked = False
        End If
    End Sub
End Class