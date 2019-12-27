Imports System.Data.Odbc

Public Class fkasmasuk
    Private Sub fkasmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Sub awal()
        'txtkodemasuk.Clear()
        'cmbsales.SelectedIndex = -1
        'cmbkas.SelectedIndex = -1
        'txtalamat.Clear()
        'txtnama.Clear()
        'txttelp.Clear()
        'txtketerangan.Clear()

        'Call koneksii()

        'txtalamat.Enabled = False
        'txttelp.Enabled = False
        'txtkodemasuk.Enabled = False
        'txtnama.Enabled = False
        'txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnprint.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        Call isitabel()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode Gudang"
        GridColumn1.Width = 40
        GridColumn1.FieldName = "kode_gudang"

        GridColumn2.Caption = "Nama Gudang"
        GridColumn2.Width = 60
        GridColumn2.FieldName = "nama_gudang"

        GridColumn3.Caption = "Alamat"
        GridColumn3.Width = 80
        GridColumn3.FieldName = "alamat_gudang"

        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 40
        GridColumn4.FieldName = "telepon_gudang"

        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 70
        GridColumn5.FieldName = "keterangan_gudang"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "Select * from tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick

    End Sub


End Class