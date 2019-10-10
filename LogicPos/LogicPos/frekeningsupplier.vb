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
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            'Call Index()
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
        MsgBox(kode_supplier)
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
End Class