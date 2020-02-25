Imports System.Data.Odbc

Public Class fkategoribarang
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean
    Dim kodekategoriedit As String
    Dim selisihharga As Double
    Private Sub fkategoribarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    End Sub

    Sub awal()
        txtkode.Clear()
        txtnama.Clear()
        txtselisih.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtkode.Enabled = False
        txtnama.Enabled = False
        txtselisih.Enabled = False
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
        GridColumn1.Caption = "Kode Kategori"
        GridColumn1.Width = 100
        GridColumn1.FieldName = "kode_kategori"

        GridColumn2.Caption = "Nama Kategori"
        GridColumn2.Width = 200
        GridColumn2.FieldName = "nama_kategori"

        GridColumn3.Caption = "Selisih Kategori"
        GridColumn3.FieldName = "selisih_kategori"
        GridColumn3.Width = 100

        GridColumn4.Caption = "Keterangan Kategori"
        GridColumn4.FieldName = "keterangan_kategori"
        GridColumn4.Width = 300
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_kategori_barang"
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
        txtselisih.TabIndex = 3
        txtketerangan.TabIndex = 4
    End Sub
    Sub enable_text()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtselisih.Enabled = True
        txtketerangan.Enabled = True
        txtkode.Focus()
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_kategori_barang WHERE kode_kategori  = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode Kategori Sudah ada dengan nama " + dr("nama_kategori"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_kategori_barang (kode_kategori, nama_kategori, selisih_kategori, keterangan_kategori, created_by, updated_by,date_created,last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "','" & txtselisih.Text & "', '" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If

    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
                Call enable_text()
                Call index()

                GridControl.Enabled = False
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("kode belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtselisih.Text.Length = 0 Then
                            MsgBox("Selisih belum terisi !")
                        Else
                            Call simpan()
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Sub edit()
        Call koneksii()
        If txtkode.Text.Equals(kodekategoriedit) Then
            sql = "UPDATE tb_kategori_barang SET  nama_kategori='" & txtnama.Text & "', selisih_kategori='" & selisihharga & "', keterangan_kategori='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated= now()  WHERE  kode_kategori='" & kodekategoriedit & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"
            Me.Refresh()
            Call awal()
        Else
            sql = "SELECT * FROM tb_kategori_barang WHERE kode_kategori  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode Kas Sudah ada dengan nama " + dr("nama_kategori"), MsgBoxStyle.Information, "Pemberitahuan")
            Else
                sql = "UPDATE tb_kategori_barang SET kode_kategori='" & txtkode.Text & "', nama_kategori='" & txtnama.Text & "', selisih_kategori='" & selisihharga & "', keterangan_kategori='" & txtketerangan.Text & "', updated_by='" & fmenu.statususer.Text & "', last_updated= now()  WHERE  kode_kategori='" & kodekategoriedit & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
                btnedit.Text = "Edit"
                Me.Refresh()
                Call awal()
            End If
        End If
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
                    MsgBox("ID belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtselisih.Text.Length = 0 Then
                            MsgBox("Selisih belum terisi !")
                        Else
                            Call edit()
                        End If
                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            Call koneksii()
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_kategori_barang WHERE  kode_kategori='" & txtkode.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader
                MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Refresh()
                Call awal()
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        txtkode.Text = GridView.GetFocusedRowCellValue("kode_kategori")
        kodekategoriedit = GridView.GetFocusedRowCellValue("kode_kategori")
        txtnama.Text = GridView.GetFocusedRowCellValue("nama_kategori")
        txtselisih.Text = GridView.GetFocusedRowCellValue("selisih_kategori")
        txtketerangan.Text = GridView.GetFocusedRowCellValue("keterangan_kategori")

        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub

    Private Sub txtselisih_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtselisih.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub txtselisih_TextChanged(sender As Object, e As EventArgs) Handles txtselisih.TextChanged
        If txtselisih.Text = "" Then
            txtselisih.Text = 0
        Else
            selisihharga = txtselisih.Text
            txtselisih.Text = Format(selisihharga, "##,##0")
            txtselisih.SelectionStart = Len(txtselisih.Text)
        End If
    End Sub
End Class