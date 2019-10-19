Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fsupplier
    Private Sub fsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub
    Sub awal()
        txtkode.Clear()
        txtalamat.Clear()
        txtnama.Clear()
        txttelp.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtalamat.Enabled = False
        txttelp.Enabled = False
        txtkode.Enabled = False
        txtnama.Enabled = False
        txtketerangan.Enabled = False

        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnrekening.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        Call isitabel()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode Supplier"
        GridColumn1.Width = 30
        GridColumn1.FieldName = "kode_supplier"
        GridColumn2.Caption = "Nama Supplier"
        GridColumn2.Width = 50
        GridColumn2.FieldName = "nama_supplier"
        GridColumn3.Caption = "Alamat"
        GridColumn3.Width = 75
        GridColumn3.FieldName = "alamat_supplier"
        GridColumn4.Caption = "Telepon"
        GridColumn4.Width = 30
        GridColumn4.FieldName = "telepon_supplier"
        GridColumn5.Caption = "Keterangan"
        GridColumn5.Width = 75
        GridColumn5.FieldName = "keterangan_supplier"
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
    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_supplier,3) FROM tb_supplier WHERE DATE_FORMAT(MID(`kode_supplier`, 3 , 6), ' %y ')+ MONTH(MID(`kode_supplier`,3 , 6)) + DAY(MID(`kode_supplier`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_supplier,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "SP" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "SP" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "SP" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "SP" + Format(Now.Date, "yyMMdd") + "001"
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
        Call koneksii()
        sql = "SELECT * FROM tb_supplier WHERE kode_supplier  = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode Supplier Sudah ada dengan nama " + dr("nama_supplier"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_supplier (kode_supplier, nama_supplier, telepon_supplier, alamat_supplier, keterangan_supplier, created_by, updated_by,date_created,last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txttelp.Text & "', '" & txtalamat.Text & "','" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If

    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnrekening.Enabled = True
            btnhapus.Enabled = False
            btnrekening.Enabled = False
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
        sql = "UPDATE tb_supplier SET  nama_supplier='" & txtnama.Text & "',alamat_supplier='" & txtalamat.Text & "', telepon_supplier='" & txttelp.Text & "',keterangan_supplier='" & txtketerangan.Text & "',updated_by='" & fmenu.statususer.Text & "',last_updated= now()  WHERE  kode_supplier='" & txtkode.Text & "'"
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
        txtkode.Text = GridView1.GetFocusedRowCellValue("kode_supplier")
        txtnama.Text = GridView1.GetFocusedRowCellValue("nama_supplier")
        txtalamat.Text = GridView1.GetFocusedRowCellValue("alamat_supplier")
        txttelp.Text = GridView1.GetFocusedRowCellValue("telepon_supplier")
        txtketerangan.Text = GridView1.GetFocusedRowCellValue("keterangan_supplier")
        btnrekening.Enabled = True
        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_supplier WHERE  kode_supplier='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Refresh()
            Call awal()
        End If
    End Sub
    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub btnrekening_Click(sender As Object, e As EventArgs) Handles btnrekening.Click
        frekeningsupplier.kode_supplier = Me.txtkode.Text
        frekeningsupplier.ShowDialog()
    End Sub

End Class