Imports System.Data.Odbc

Public Class frekeningsupplier
    Public idsupplier, idrekening, norekening As String
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean
    Private Sub frekening_supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MdiParent = fmenu
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
        txtnamabank.Enabled = False
        txtnamarekening.Enabled = False
        txtnorekening.Enabled = False
        txtketeranganrekening.Enabled = False

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
        txtnamabank.Enabled = True
        txtnorekening.Enabled = True
        txtnamarekening.Enabled = True
        txtketeranganrekening.Enabled = True
        txtnamabank.Focus()
    End Sub
    Sub index()
        txtnamabank.TabIndex = 1
        txtnorekening.TabIndex = 2
        txtnamarekening.TabIndex = 3
        txtketeranganrekening.TabIndex = 4
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
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
                            Call simpan()
                        End If

                    End If
                End If
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_rekening_supplier JOIN tb_supplier ON tb_rekening_supplier.supplier_id = tb_supplier.id WHERE nomor_rekening  ='" + txtnorekening.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Nomor Rekening Sudah ada dengan nama " + dr("nama_supplier"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            Try
                Call koneksii()
                sql = "INSERT INTO tb_rekening_supplier (nomor_rekening, nama_bank, nama_rekening, keterangan_rekening, supplier_id, created_by, update_by,date_created,last_updated) VALUES ('" & txtnorekening.Text & "', '" & txtnamabank.Text & "','" & txtnamarekening.Text & "','" & txtketeranganrekening.Text & "','" & idsupplier & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
                btntambah.Text = "Tambah"

                'history user ===========
                Call historysave("Menyimpan Data Rekening Supplier Nomor Rekening" + txtnorekening.Text, txtnorekening.Text)
                '========================
                Me.Refresh()
                Call awal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_rekening_supplier WHERE supplier_id = '" & idsupplier & "'"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

        GridColumn1.Caption = "id"
        GridColumn1.Width = 20
        GridColumn1.FieldName = "id"

        GridColumn2.Caption = "Nomor Rekening"
        GridColumn2.Width = 50
        GridColumn2.FieldName = "nomor_rekening"

        GridColumn3.Caption = "Nama Rekening"
        GridColumn3.Width = 50
        GridColumn3.FieldName = "nama_rekening"

        GridColumn4.Caption = "Nama Bank"
        GridColumn4.Width = 50
        GridColumn4.FieldName = "nama_bank"

        GridColumn5.Caption = "Keterangan Rekening"
        GridColumn5.Width = 75
        GridColumn5.FieldName = "keterangan_rekening"

        GridColumn6.Caption = "Created By"
        GridColumn6.Width = 30
        GridColumn6.FieldName = "created_by"
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub txtnorek_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnorekening.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            If MessageBox.Show("Hapus " & Me.txtnorekening.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    Call koneksii()
                    sql = "DELETE FROM tb_rekening_supplier WHERE  supplier_id='" & idsupplier & "' and id= '" & idrekening & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    MessageBox.Show("Rekening " + txtnorekening.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'history user ===========
                    Call historysave("Menghapus Data Rekening Supplier Nomor Rekening" + txtnorekening.Text, txtnorekening.Text)
                    '========================

                    Me.Refresh()
                    Call awal()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
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
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub edit()
        If txtnorekening.Text.Equals(norekening) Then
            Call perbaharui()
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_rekening_supplier JOIN tb_supplier ON tb_rekening_supplier.supplier_id = tb_supplier.id WHERE nomor_rekening ='" + norekening + "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Data Rekening sudah ada dengan nama " + dr("nama_supplier"), MsgBoxStyle.Information, "Pemberitahuan")
                txtnorekening.Focus()
            Else
                Call perbaharui()
            End If
        End If
    End Sub

    Sub perbaharui()
        Try
            Call koneksii()
            sql = "UPDATE tb_rekening_supplier SET nomor_rekening='" & txtnorekening.Text & "', nama_bank='" & txtnamabank.Text & "', nama_rekening='" & txtnamarekening.Text & "', keterangan_rekening='" & txtketeranganrekening.Text & "', update_by='" & fmenu.kodeuser.Text & "', last_updated= now()  WHERE supplier_id='" & idsupplier & "' AND id= '" & idrekening & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            'history user ==========
            Call historysave("Mengedit Data Rekening Supplier Nomor Rekening " + txtnorekening.Text, txtnorekening.Text)
            '=======================

            Me.Refresh()
            Call awal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles GridControl1.DoubleClick
        idrekening = GridView1.GetFocusedRowCellValue("id")
        norekening = GridView1.GetFocusedRowCellValue("nomor_rekening")

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

End Class