Imports System.Data.Odbc

Public Class frekeningsupplier
    Public kode_supplier As String
    Private Sub frekening_supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MdiParent = fmenu
        Call awal()
    End Sub
    Sub awal()
        txtnamabank.Enabled = False
        txtnamarek.Enabled = False
        txtnorek.Enabled = False

        txtnamabank.Clear()
        txtnamarek.Clear()
        txtnorek.Clear()

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"
        btntambah.Enabled = True
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnbatal.Enabled = False
        GridControl1.Enabled = True
        Call isitabel()
    End Sub
    Sub enable_text()
        txtnamabank.Enabled = True
        txtnorek.Enabled = True
        txtnamarek.Enabled = True
        txtnamarek.Focus()
    End Sub
    Sub index()
        txtnamarek.TabIndex = 0
        txtnorek.TabIndex = 1
        txtnamabank.TabIndex = 2
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            Call Index()
            'txtkode.Text = autonumber()
            GridControl1.Enabled = False
        Else
            If txtnamabank.Text.Trim.Length = 0 Then
                MsgBox("Nama Bank belum terisi!!!")
            Else
                If txtnamarek.Text.Trim.Length = 0 Then
                    MsgBox("Nama Rekening belum terisi!!!")
                Else
                    If txtnorek.Text.Trim.Length = 0 Then
                        MsgBox("Nomor Rekening belum terisi!!!")
                    Else
                        Call simpan()
                    End If

                End If
            End If
        End If
    End Sub
    Sub simpan()
        Call koneksii()
        'MsgBox(kode_supplier)
        sql = "SELECT * FROM tb_rekening_supplier WHERE nomor_rekening  = '" + txtnorek.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Nomor Rekening Sudah ada dengan nama " + dr("nama_supplier"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_rekening_supplier (kode_rekening, nomor_rekening, nama_bank, nama_rekening, kode_supplier, created_by, update_by,date_created,last_updated) VALUES ('" & txtnorek.Text & "', '" & txtnorek.Text & "', '" & txtnamabank.Text & "','" & txtnamarek.Text & "','" & kode_supplier & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Nomor Rekening"
        GridColumn1.Width = 65
        GridColumn1.FieldName = "nomor_rekening"
        GridColumn2.Caption = "Nama Rekening"
        GridColumn2.Width = 65
        GridColumn2.FieldName = "nama_rekening"
        GridColumn3.Caption = "Nama Bank"
        GridColumn3.FieldName = "nama_bank"
        GridColumn3.Width = 73
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "Select * from tb_rekening_supplier where kode_supplier = '" & kode_supplier & "'"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub txtnorek_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnorek.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnorek.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_rekening_supplier WHERE  kode_supplier='" & kode_supplier & "' and kode_rekening= '" & txtnorek.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MessageBox.Show("Rekening " + txtnorek.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Refresh()
            Call awal()
        End If
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            Call enable_text()
            Call index()
            GridControl1.Enabled = False
        Else
            If txtnamabank.Text.Trim.Length = 0 Then
                MsgBox("Nama Bank belum terisi!!!")
            Else
                If txtnamarek.Text.Trim.Length = 0 Then
                    MsgBox("Nama Rekening belum terisi!!!")
                Else
                    If txtnorek.Text.Trim.Length = 0 Then
                        MsgBox("Nomor Rekening belum terisi!!!")
                    Else
                        Call edit()
                    End If
                End If
            End If
        End If
    End Sub
    Sub edit()
        Dim norek As String
        norek = txtnorek.Text
        Call koneksii()
        sql = "UPDATE tb_rekening_supplier SET kode_rekening='" & txtnorek.Text & "', nomor_rekening='" & txtnorek.Text & "',nama_rekening='" & txtnamarek.Text & "', nama_bank='" & txtnamabank.Text & "',update_by='" & fmenu.statususer.Text & "',last_updated= now()  WHERE  kode_supplier='" & kode_supplier & "' and kode_rekening= '" & norek & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"
        Me.Refresh()
        Call awal()
    End Sub

    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles GridControl1.DoubleClick
        txtnamabank.Text = GridView1.GetFocusedRowCellValue("nama_bank")
        txtnamarek.Text = GridView1.GetFocusedRowCellValue("nama_rekening")
        txtnorek.Text = GridView1.GetFocusedRowCellValue("nomor_rekening")
        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub
End Class