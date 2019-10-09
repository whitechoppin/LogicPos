Imports System.Data.Odbc

Public Class fkas
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
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
                    If txtsaldo.Text.Length = 0 Then
                        MsgBox("Saldo belum terisi !")
                    Else
                        Call simpan()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub fkas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Sub awal()
        txtkode.Clear()
        txtnama.Clear()
        txtsaldo.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtsaldo.Enabled = False
        txtkode.Enabled = False
        txtnama.Enabled = False
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
        GridColumn1.Caption = "Kode Kas"
        GridColumn1.Width = 100
        GridColumn1.FieldName = "kode_kas"
        GridColumn2.Caption = "Nama Kas"
        GridColumn2.Width = 100
        GridColumn2.FieldName = "nama_kas"
        GridColumn3.Caption = "Keterangan"
        GridColumn3.FieldName = "keterangan_kas"
        GridColumn3.Width = 200
        GridColumn4.Caption = "Saldo Awal"
        GridColumn4.FieldName = "saldo_awal"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "Select * from tb_kas"
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
        txtsaldo.TabIndex = 3
        txtketerangan.TabIndex = 4
    End Sub
    Sub enable_text()
        txtkode.Enabled = True
        txtnama.Enabled = True
        txtsaldo.Enabled = True
        txtketerangan.Enabled = True
        txtkode.Focus()
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas  = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode Kas Sudah ada dengan nama " + dr("nama_kas"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_kas (kode_kas, nama_kas, keterangan_kas, saldo_awal, created_by, updated_by,date_created,last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtsaldo.Text & "','" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If

    End Sub
End Class