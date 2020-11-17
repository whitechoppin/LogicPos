Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class fsupplier
    Public namaform As String = "master-supplier"

    Dim idsupplieredit, kodesupplieredit As String
    Public kodeakses As Integer
    Dim tambahstatus, editstatus, hapusstatus As Boolean

    '==== autosize form ====
    Dim CuRWidth As Integer = Me.Width
    Dim CuRHeight As Integer = Me.Height

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim RatioHeight As Double = (Me.Height - CuRHeight) / CuRHeight
        Dim RatioWidth As Double = (Me.Width - CuRWidth) / CuRWidth

        For Each ctrl As Control In Controls
            ctrl.Width += ctrl.Width * RatioWidth
            ctrl.Left += ctrl.Left * RatioWidth
            ctrl.Top += ctrl.Top * RatioHeight
            ctrl.Height += ctrl.Height * RatioHeight
        Next

        CuRHeight = Me.Height
        CuRWidth = Me.Width
    End Sub

    '=======================

    Private Sub fsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Call historysave("Membuka Master Supplier", "N/A", namaform)
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
        btnrekening.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        GridControl1.Enabled = True
        Call isitabel()
    End Sub
    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_supplier ORDER BY nama_supplier ASC"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)

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

        GridColumn6.Caption = "id"
        GridColumn6.Width = 20
        GridColumn6.FieldName = "id"
        GridColumn6.Visible = False
    End Sub
    Sub index()
        txtkode.TabIndex = 1
        btnauto.TabIndex = 2
        btngenerate.TabIndex = 3
        txtnama.TabIndex = 4
        txtalamat.TabIndex = 5
        txttelp.TabIndex = 6
        txtketerangan.TabIndex = 7
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
            'cnn.Close()
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
        Else
            MsgBox("Tidak ada akses")
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
            Try
                Call koneksii()
                sql = "INSERT INTO tb_supplier (kode_supplier, nama_supplier, telepon_supplier, alamat_supplier, keterangan_supplier, created_by, updated_by,date_created,last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txttelp.Text & "', '" & txtalamat.Text & "','" & txtketerangan.Text & "','" & fmenu.kodeuser.Text & "','" & fmenu.kodeuser.Text & "',now(),now())"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()
                MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
                btntambah.Text = "Tambah"

                'history user ==========
                Call historysave("Menyimpan Data Supplier Kode " + txtkode.Text, txtkode.Text, namaform)
                '========================
                Me.Refresh()
                Call awal()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If editstatus.Equals(True) Then
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
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub
    Sub edit()
        If txtkode.Text.Equals(kodesupplieredit) Then
            Call perbaharui()
        Else
            Call koneksii()
            sql = "SELECT * FROM tb_supplier WHERE kode_supplier  = '" & txtkode.Text & "' LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                MsgBox("Kode supplier sudah ada dengan nama " + dr("nama_supplier"), MsgBoxStyle.Information, "Pemberitahuan")
                txtkode.Focus()
            Else
                Call perbaharui()
            End If
        End If
    End Sub

    Sub perbaharui()
        Try
            Call koneksii()
            sql = "UPDATE tb_supplier SET  kode_supplier='" & txtkode.Text & "',nama_supplier='" & txtnama.Text & "',alamat_supplier='" & txtalamat.Text & "', telepon_supplier='" & txttelp.Text & "',keterangan_supplier='" & txtketerangan.Text & "',updated_by='" & fmenu.kodeuser.Text & "',last_updated= now()  WHERE  id='" & idsupplieredit & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
            btnedit.Text = "Edit"

            'history user ==========
            Call historysave("Mengedit Data Supplier Kode " + txtkode.Text, txtkode.Text, namaform)
            '=======================
            Me.Refresh()
            Call awal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
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
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        kodesupplieredit = GridView1.GetFocusedRowCellValue("kode_supplier")
        txtkode.Text = kodesupplieredit
        idsupplieredit = GridView1.GetFocusedRowCellValue("id")
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
        If hapusstatus.Equals(True) Then
            If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Try
                    Call koneksii()
                    sql = "DELETE FROM tb_supplier WHERE id='" & idsupplieredit & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader
                    MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'history user ==========
                    Call historysave("Menghapus Data Supplier Kode" + txtkode.Text, txtkode.Text, namaform)
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

    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkode.KeyPress
        e.Handled = ValidAngkaHuruf(e)
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call isitabel()
    End Sub

    Private Sub btnrekening_Click(sender As Object, e As EventArgs) Handles btnrekening.Click
        Dim rekening As Integer
        rekening = flogin.rekeningsupplier
        If rekening > 0 Then
            frekeningsupplier.idsupplier = idsupplieredit
            frekeningsupplier.kodeakses = rekening
            frekeningsupplier.ShowDialog()
        Else
            MsgBox("Anda tidak memiliki akses", MsgBoxStyle.Information, "Gagal")
        End If
    End Sub

    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub fsupplier_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class