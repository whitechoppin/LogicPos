Imports System.Data.Odbc

Public Class fkategoribarang
    Dim kodekategoriedit As String
    Private Sub fkategoribarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
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
        txtnama.Focus()
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

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Private Sub txtselisih_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtselisih.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
End Class