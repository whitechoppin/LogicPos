Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fgudang
    Dim kodegudangedit As String
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean
    Private Sub fgudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtalamat.Clear()
        txtnama.Clear()
        txttelp.Clear()
        txtketerangan.Clear()

        Call koneksii()

        txtkode.Enabled = False
        btnauto.Enabled = False
        btngenerate.Enabled = False
        txtnama.Enabled = False
        txtalamat.Enabled = False
        txttelp.Enabled = False
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
        sql = "SELECT * FROM tb_gudang"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl.DataSource = Nothing
        GridControl.DataSource = ds.Tables(0)
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
        sql = "SELECT RIGHT(kode_gudang,3) FROM tb_gudang WHERE DATE_FORMAT(MID(`kode_gudang`, 3 , 6), ' %y ')+ MONTH(MID(`kode_gudang`,3 , 6)) + DAY(MID(`kode_gudang`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_gudang,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "GD" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "GD" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "GD" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "GD" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
    Sub enable_text()
        txtkode.Enabled = True
        btnauto.Enabled = True
        btngenerate.Enabled = True
        txtnama.Enabled = True
        txttelp.Enabled = True
        txtalamat.Enabled = True
        txtketerangan.Enabled = True
        txtnama.Focus()
    End Sub
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If tambahstatus.Equals(True) Then
            If btntambah.Text = "Tambah" Then
                btnbatal.Enabled = True
                btntambah.Text = "Simpan"
                Call enable_text()
                Call index()
                txtkode.Text = autonumber()
                GridControl.Enabled = False
            Else
                If txtkode.Text.Length = 0 Then
                    MsgBox("Kode belum terisi !")
                Else
                    If txtnama.Text.Length = 0 Then
                        MsgBox("Nama belum terisi !")
                    Else
                        If txtalamat.Text.Length = 0 Then
                            MsgBox("Alamat belum terisi !")
                        Else
                            If txttelp.Text.Length = 0 Then
                                MsgBox("Telepon belum terisi !")
                            Else
                                Call simpan()
                            End If
                        End If
                    End If
                End If
            End If
        Else
            MsgBox(True)
        End If
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_gudang WHERE kode_gudang  = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode Gudang Sudah ada dengan nama " + dr("nama_gudang"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_gudang (kode_gudang, nama_gudang, telepon_gudang, alamat_gudang, keterangan_gudang, created_by, updated_by,date_created, last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txttelp.Text & "', '" & txtalamat.Text & "','" & txtketerangan.Text & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If
        'Call koneksii()
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            Call enable_text()
            Call index()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("Kode belum terisi !")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama belum terisi !")
                Else
                    If txtalamat.Text.Length = 0 Then
                        MsgBox("Alamat belum terisi !")
                    Else
                        If txttelp.Text.Length = 0 Then
                            MsgBox("Telepon belum terisi !")
                        Else
                            Call edit()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Sub edit()
        Call koneksii()
        If txtkode.Text.Equals(kodegudangedit) Then
            sql = "UPDATE tb_gudang SET nama_gudang='" & txtnama.Text & "',alamat_gudang='" & txtalamat.Text & "', telepon_gudang='" & txttelp.Text & "',keterangan_gudang='" & txtketerangan.Text & "',updated_by='" & fmenu.statususer.Text & "',last_updated=now()  WHERE  kode_gudang='" & kodegudangedit & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"
            Me.Refresh()
            Call awal()
        Else
            sql = "SELECT * FROM tb_gudang WHERE kode_gudang  = '" + txtkode.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode Gudang Sudah ada dengan nama " + dr("nama_gudang"), MsgBoxStyle.Information, "Pemberitahuan")
            Else
                sql = "UPDATE tb_gudang SET  kode_gudang='" & txtkode.Text & "',nama_gudang='" & txtnama.Text & "',alamat_gudang='" & txtalamat.Text & "', telepon_gudang='" & txttelp.Text & "',keterangan_gudang='" & txtketerangan.Text & "',updated_by='" & fmenu.statususer.Text & "',last_updated=now()  WHERE  kode_gudang='" & kodegudangedit & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
                btnedit.Text = "Edit"
                Me.Refresh()
                Call awal()
            End If
        End If
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub
    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        txtkode.Text = GridView.GetFocusedRowCellValue("kode_gudang")
        kodegudangedit = GridView.GetFocusedRowCellValue("kode_gudang")
        txtnama.Text = GridView.GetFocusedRowCellValue("nama_gudang")
        txtalamat.Text = GridView.GetFocusedRowCellValue("alamat_gudang")
        txttelp.Text = GridView.GetFocusedRowCellValue("telepon_gudang")
        txtketerangan.Text = GridView.GetFocusedRowCellValue("keterangan_gudang")
        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub
    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If hapusstatus.Equals(True) Then
            Call koneksii()
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                sql = "DELETE FROM tb_gudang WHERE  kode_gudang='" & txtkode.Text & "'"
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
    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub fgudang_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnauto_Click(sender As Object, e As EventArgs) Handles btnauto.Click
        txtkode.Text = autonumber()
    End Sub

    Private Sub btngenerate_Click(sender As Object, e As EventArgs) Handles btngenerate.Click
        Dim r As New Random
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        txtkode.Text = sb.ToString()
    End Sub
End Class