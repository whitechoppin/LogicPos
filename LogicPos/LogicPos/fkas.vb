Imports System.Data.Odbc

Public Class fkas
    Dim saldoawal As Double
    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            Call index()
            txtkode.Text = autonumber()
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
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "Rp ##,#0"
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
        txtkode.Enabled = False
        txtnama.Enabled = True
        txtsaldo.Enabled = True
        txtketerangan.Enabled = True
        txtnama.Focus()
    End Sub
    Sub simpan()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas  = '" + txtkode.Text + "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            MsgBox("Kode Kas Sudah ada dengan nama " + dr("nama_kas"), MsgBoxStyle.Information, "Pemberitahuan")
        Else
            sql = "INSERT INTO tb_kas (kode_kas, nama_kas, keterangan_kas, saldo_awal, total_saldo, created_by, updated_by,date_created,last_updated) VALUES ('" & txtkode.Text & "', '" & txtnama.Text & "', '" & txtketerangan.Text & "','" & saldoawal & "','" & saldoawal & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
            btntambah.Text = "Tambah"
            Me.Refresh()
            Call awal()
        End If

    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        If MessageBox.Show("Hapus " & Me.txtnama.Text & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            sql = "DELETE FROM tb_kas WHERE  kode_kas='" & txtkode.Text & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            MessageBox.Show(txtnama.Text + " berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Refresh()
            Call awal()
        End If
    End Sub

    Private Sub txtsaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsaldo.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        txtkode.Text = GridView1.GetFocusedRowCellValue("kode_kas")
        txtnama.Text = GridView1.GetFocusedRowCellValue("nama_kas")
        txtsaldo.Text = GridView1.GetFocusedRowCellValue("saldo_awal")
        txtketerangan.Text = GridView1.GetFocusedRowCellValue("keterangan_kas")
        btnedit.Enabled = True
        btnbatal.Enabled = True
        btnhapus.Enabled = True
        btntambah.Enabled = False
        btntambah.Text = "Tambah"
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Sub edit()
        Call koneksii()
        sql = "UPDATE tb_kas SET  kode_kas='" & txtkode.Text & "', nama_kas='" & txtnama.Text & "', saldo_awal='" & saldoawal & "', total_saldo='" & saldoawal & "',keterangan_kas='" & txtketerangan.Text & "',updated_by='" & fmenu.statususer.Text & "',last_updated= now()  WHERE  kode_kas='" & txtkode.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        MsgBox("Data di Update", MsgBoxStyle.Information, "Berhasil")
        btnedit.Text = "Edit"
        Me.Refresh()
        Call awal()
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_kas,3) FROM tb_kas WHERE DATE_FORMAT(MID(`kode_kas`, 3 , 6), ' %y ')+ MONTH(MID(`kode_kas`,3 , 6)) + DAY(MID(`kode_kas`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_kas,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "KS" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "KS" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "KS" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "KS" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            btnedit.Text = "Simpan"
            btnhapus.Enabled = False
            Call enable_text()
            Call index()
            GridControl.Enabled = False
        Else
            If txtkode.Text.Length = 0 Then
                MsgBox("ID belum terisi!!!")
            Else
                If txtnama.Text.Length = 0 Then
                    MsgBox("Nama belum terisi!!!")
                Else
                    If txtsaldo.Text.Length = 0 Then
                        MsgBox("saldo belum terisi!!!")
                    Else
                        Call edit()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtsaldo_TextChanged(sender As Object, e As EventArgs) Handles txtsaldo.TextChanged
        If txtsaldo.Text = "" Then
            txtsaldo.Text = 0
        Else
            saldoawal = txtsaldo.Text
            txtsaldo.Text = Format(saldoawal, "##,##0")
            txtsaldo.SelectionStart = Len(txtsaldo.Text)
        End If
    End Sub
End Class