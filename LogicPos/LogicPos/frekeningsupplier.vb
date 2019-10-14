Imports System.Data.Odbc

Public Class frekeningsupplier
    Public kode_supplier As String
    Private Sub frekening_supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MdiParent = fmenu
        Call awal()
    End Sub
    Sub awal()
        txtkoderekening.Enabled = False
        txtnamabank.Enabled = False
        txtnamarekening.Enabled = False
        txtnorekening.Enabled = False
        txtketeranganrekening.Enabled = False

        txtkoderekening.Clear()
        txtnamabank.Clear()
        txtnamarekening.Clear()
        txtnorekening.Clear()
        txtketeranganrekening.Clear()

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
        txtkoderekening.Enabled = False
        txtnamabank.Enabled = True
        txtnorekening.Enabled = True
        txtnamarekening.Enabled = True
        txtketeranganrekening.Enabled = True
        txtnamarekening.Focus()
    End Sub
    Sub index()
        txtnamarekening.TabIndex = 0
        txtnorekening.TabIndex = 1
        txtnamabank.TabIndex = 2
        txtketeranganrekening.TabIndex = 3
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            Call index()
            txtkoderekening.Text = autonumber()
            GridControl1.Enabled = False
        Else
            If txtnamabank.Text.Trim.Length = 0 Then
                MsgBox("Nama Bank belum terisi!!!")
            Else
                If txtnamarekening.Text.Trim.Length = 0 Then
                    MsgBox("Nama Rekening belum terisi!!!")
                Else
                    If txtnorekening.Text.Trim.Length = 0 Then
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
        sql = "SELECT * FROM tb_rekening_supplier WHERE nomor_rekening  = '" + txtnorekening.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Nomor Rekening Sudah ada dengan nama " + dr("nama_supplier"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_rekening_supplier (kode_rekening, nomor_rekening, nama_bank, nama_rekening, keterangan_rekening, kode_supplier, created_by, update_by,date_created,last_updated) VALUES ('" & txtkoderekening.Text & "', '" & txtnorekening.Text & "', '" & txtnamabank.Text & "','" & txtnamarekening.Text & "','" & txtketeranganrekening.Text & "','" & kode_supplier & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode Rekening"
        GridColumn1.Width = 30
        GridColumn1.FieldName = "kode_rekening"
        GridColumn2.Caption = "Nomor Rekening"
        GridColumn2.Width = 65
        GridColumn2.FieldName = "nomor_rekening"
        GridColumn3.Caption = "Nama Rekening"
        GridColumn3.Width = 65
        GridColumn3.FieldName = "nama_rekening"
        GridColumn4.Caption = "Nama Bank"
        GridColumn4.Width = 65
        GridColumn4.FieldName = "nama_bank"
        GridColumn5.Caption = "Keterangan Rekening"
        GridColumn5.Width = 75
        GridColumn5.FieldName = "keterangan_rekening"
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
    Private Sub txtnorek_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnorekening.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnorekening.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_rekening_supplier WHERE  kode_supplier='" & kode_supplier & "' and kode_rekening= '" & txtkoderekening.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MessageBox.Show("Rekening " + txtnorekening.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                If txtnamarekening.Text.Trim.Length = 0 Then
                    MsgBox("Nama Rekening belum terisi!!!")
                Else
                    If txtnorekening.Text.Trim.Length = 0 Then
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
        norek = txtkoderekening.Text
        Call koneksii()
        sql = "UPDATE tb_rekening_supplier SET nomor_rekening='" & txtnorekening.Text & "', nama_bank='" & txtnamabank.Text & "', nama_rekening='" & txtnamarekening.Text & "', keterangan_rekening='" & txtketeranganrekening.Text & "', update_by='" & fmenu.statususer.Text & "', last_updated= now()  WHERE  kode_supplier='" & kode_supplier & "' and kode_rekening= '" & norek & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"
        Me.Refresh()
        Call awal()
    End Sub

    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles GridControl1.DoubleClick
        txtkoderekening.Text = GridView1.GetFocusedRowCellValue("kode_rekening")
        txtnorekening.Text = GridView1.GetFocusedRowCellValue("nomor_rekening")
        txtnamarekening.Text = GridView1.GetFocusedRowCellValue("nama_rekening")
        txtnamabank.Text = GridView1.GetFocusedRowCellValue("nama_bank")
        txtketeranganrekening.Text = GridView1.GetFocusedRowCellValue("keterangan_rekening")

        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(`kode_rekening`,3) FROM `tb_rekening_supplier` WHERE LEFT(`kode_rekening`,2)= DATE_FORMAT(now(),'%y') ORDER BY RIGHT(kode_rekening,3) DESC"
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
End Class