Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fsupplier
    Sub awal()
        txtkode.Clear()
        txtalamat.Clear()
        txtnama.Clear()
        txttelp.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtcari.Enabled = True
        cmbpilih.Enabled = True

        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtkode.Enabled = False
        txtnama.Enabled = False
        txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        Call isitabel()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "ID"
        GridColumn1.Width = 65
        GridColumn1.FieldName = "id"
        GridColumn2.Caption = "Nama"
        GridColumn2.Width = 65
        GridColumn2.FieldName = "nama"
        GridColumn3.Caption = "Alamat"
        GridColumn3.FieldName = "alamat"
        GridColumn3.Width = 73
        GridColumn4.Caption = "Telepon"
        GridColumn4.FieldName = "telepon"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 75
        GridColumn5.FieldName = "keterangan"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "Select * from tb_supplier"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub
    Sub index()
        txtnama.TabIndex = 1
        txtalamat.TabIndex = 2
        txttelp.TabIndex = 3
        txtketerangan.TabIndex = 4
    End Sub
    Private Sub fsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
        cmbpilih.SelectedIndex = 1
    End Sub
    Function autonumber()
        Call koneksii()
        'sql = "SELECT RIGHT(`id`,3) FROM `tb_supplier` WHERE date_format(LEFT(`id`,2), ' %y ')= DATE_FORMAT(NOW(),' %y ') ORDER BY RIGHT(id,3) DESC"
        sql = "SELECT RIGHT(`id`,3) FROM `tb_supplier` WHERE LEFT(`id`,2)= DATE_FORMAT(now(),'%y') ORDER BY RIGHT(id,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return Format(Now.Date, "yy") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return Format(Now.Date, "yy") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return Format(Now.Date, "yy") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return Format(Now.Date, "yy") + "001"
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
        txtcari.Enabled = False
        cmbpilih.Enabled = False
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            Call index()
            txtkode.Text = autonumber()
            GridControl1.Enabled = False
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
        'Call koneksii()
        'sql = "SELECT * FROM tb_supplier WHERE id  = '" + txtkode.Text + "'"
        'cmmd = New OdbcCommand(sql, cnn)
        'dr = cmmd.ExecuteReader
        'If dr.HasRows Then
        '    MsgBox("ID Customer Sudah ada dengan nama " + dr("nama"), MsgBoxStyle.Information, "Pemberitahuan")
        'Else
        '    sql = "INSERT INTO tb_supplier (id, nama, telepon, alamat, keterangan) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txttelp.Text & "', '" & txtalamat.Text & "','" & txtketerangan.Text & "')"
        '    cmmd = New OdbcCommand(sql, cnn)
        '    dr = cmmd.ExecuteReader()

        '    MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        '    btntambah.Text = "Tambah"
        '    Me.Refresh()
        '    Call awal()
        'End If
        'Call koneksii()

    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            Call enable_text()
            Call index()
            GridControl1.Enabled = False
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
        Call koneksii()
        sql = "UPDATE tb_supplier SET  nama='" & txtnama.Text & "',alamat='" & txtalamat.Text & "', telepon='" & txttelp.Text & "',keterangan='" & txtketerangan.Text & "'  WHERE  id='" & txtkode.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"
        Me.Refresh()
        Call awal()
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        txtkode.Text = GridView1.GetFocusedRowCellValue("id")
        txtnama.Text = GridView1.GetFocusedRowCellValue("nama")
        txtalamat.Text = GridView1.GetFocusedRowCellValue("alamat")
        txttelp.Text = GridView1.GetFocusedRowCellValue("telepon")
        txtketerangan.Text = GridView1.GetFocusedRowCellValue("keterangan")
        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_supplier WHERE  id='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader

            MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Refresh()
            Call awal()
        End If
    End Sub
    Sub cari()
        Dim pilih As String
        If cmbpilih.SelectedIndex = 0 Then
            pilih = "id"
        Else
            If cmbpilih.SelectedIndex = 1 Then
                pilih = "nama"
            Else
                If cmbpilih.SelectedIndex = 2 Then
                    pilih = "alamat"
                Else
                    If cmbpilih.SelectedIndex = 3 Then
                        pilih = "telepon"
                    Else
                        If cmbpilih.SelectedIndex = 4 Then
                            pilih = "keterangan"
                        End If
                    End If
                End If
            End If
        End If


        sql = "SELECT * FROM tb_supplier WHERE `" & pilih & "` LIKE '%" & txtcari.Text & "%'"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub
    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub
End Class