Imports System.Data.Odbc

Public Class fuser
    Dim kode As String
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

    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE kode_user = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode User Sudah ada dengan nama " + dr("nama_user"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_user (kode_user, nama_user, password_user, jabatan_user, email_user, telepon_user, alamat_user, keterangan_user, created_by, updated_by,date_created, last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtpassword.Text & "', '" & cmbjabatan.Text & "', '" & txtemail.Text & "', '" & txttelp.Text & "','" & txtalamat.Text & "','" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
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
        Using cnn As New OdbcConnection(strConn)
            sql = "UPDATE tb_user SET kode_user=?, nama_user=?, password_user=?,  jabatan_user=?, email_user=?, telepon_user=?, alamat_user=?, keterangan_user=?, updated_by=?, last_updated=? WHERE  kode_user='" & kode & "'"
            cmmd = New OdbcCommand(sql, cnn)
            cmmd.Parameters.AddWithValue("@kode_user", txtkode.Text)
            cmmd.Parameters.AddWithValue("@nama_user", txtnama.Text)
            cmmd.Parameters.AddWithValue("@password_user", txtpassword.Text)
            cmmd.Parameters.AddWithValue("@jabatan_user", cmbjabatan.Text)
            cmmd.Parameters.AddWithValue("@email_user", txtemail.Text)
            cmmd.Parameters.AddWithValue("@telepon_user", txttelp.Text)
            cmmd.Parameters.AddWithValue("@alamat_user", txtalamat.Text)
            cmmd.Parameters.AddWithValue("@keterangan_user", txtketerangan.Text)
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
End Class