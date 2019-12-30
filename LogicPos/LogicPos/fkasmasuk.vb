Imports System.Data.Odbc

Public Class fkasmasuk

    Dim kodemasuk As String
    Dim saldomasuk As Double

    Private Sub fkasmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub

    Sub comboboxuser()
        Call koneksii()
        cmbsales.Items.Clear()
        cmbsales.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_user", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbsales.AutoCompleteCustomSource.Add(dr("kode_user"))
                cmbsales.Items.Add(dr("kode_user"))
            End While
        End If
    End Sub

    Sub comboboxkas()
        Call koneksii()
        cmbkas.Items.Clear()
        cmbkas.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbkas.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbkas.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Sub carikas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbkas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamakas.Text = dr("nama_kas")
        Else
            txtnamakas.Text = ""
        End If
    End Sub

    Sub awal()
        'button 
        btntambah.Enabled = True
        btnbatal.Enabled = False
        btnedit.Enabled = False
        btnhapus.Enabled = False
        btnprint.Enabled = False

        btntambah.Text = "Tambah"
        btnedit.Text = "Edit"

        'bersihkan form
        txtkodemasuk.Clear()
        cmbsales.SelectedIndex = -1
        cmbkas.SelectedIndex = -1
        txtnamakas.Clear()

        txtsaldomasuk.Clear()
        txtketerangan.Clear()

        Call koneksii()

        cmbsales.Enabled = False
        cmbkas.Enabled = False
        txtsaldomasuk.Enabled = False
        txtketerangan.Enabled = False

        GridControl1.Enabled = True
        Call isitabel()

        Call comboboxuser()
        Call comboboxkas()
    End Sub
    Sub kolom()
        GridColumn1.Caption = "Kode Kas Masuk"
        GridColumn1.FieldName = "kode_kas_masuk"
        GridColumn1.Width = "40"

        GridColumn2.Caption = "Kode Kas"
        GridColumn2.FieldName = "kode_kas"
        GridColumn2.Width = "40"

        GridColumn3.Caption = "Tanggal Transaksi"
        GridColumn3.FieldName = "tanggal_transaksi"
        GridColumn3.Width = "40"

        GridColumn4.Caption = "Saldo Kas"
        GridColumn4.FieldName = "saldo_kas"
        GridColumn4.Width = "60"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "Rp ##,#0"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "SELECT * FROM tb_kas_masuk"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub

    Sub index()
        btntambah.TabIndex = 1
        btnedit.TabIndex = 2
        btnhapus.TabIndex = 3
        btnbatal.TabIndex = 4
        btnprint.TabIndex = 5
        cmbsales.TabIndex = 6
        cmbkas.TabIndex = 7
        txtsaldomasuk.TabIndex = 8
        txtketerangan.TabIndex = 9
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_kas_masuk,3) FROM tb_kas_masuk WHERE DATE_FORMAT(MID(`kode_kas_masuk`, 3 , 6), ' %y ')+ MONTH(MID(`kode_kas_masuk`,3 , 6)) + DAY(MID(`kode_kas_masuk`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_kas_masuk,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "KM" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "KM" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "KM" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "KM" + Format(Now.Date, "yyMMdd") + "001"
            End If

        Catch ex As Exception
            pesan = ex.Message.ToString
        Finally
            cnn.Close()
        End Try
        Return pesan
    End Function
    Sub enable_text()
        cmbsales.Enabled = True
        cmbkas.Enabled = True
        txtsaldomasuk.Enabled = True
        txtketerangan.Enabled = True
        cmbsales.Focus()
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btnbatal.Enabled = True
            btntambah.Text = "Simpan"
            Call enable_text()
            Call index()
            txtkodemasuk.Text = autonumber()
            GridControl1.Enabled = False
        Else
            If txtkodemasuk.Text.Length = 0 Then
                MsgBox("Kode belum terisi !")
            Else
                If cmbsales.SelectedIndex = -1 Then
                    MsgBox("Sales belum terisi !")
                Else
                    If cmbkas.SelectedIndex = -1 Then
                        MsgBox("Kas belum terisi !")
                    Else
                        If txtsaldomasuk.Text.Length = 0 Then
                            MsgBox("Saldo belum terisi !")
                        Else
                            Call simpan()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Sub simpan()
        kodemasuk = autonumber()

        Call koneksii()
        sql = "INSERT INTO tb_kas_masuk (kode_kas_masuk, kode_kas, kode_user, jenis_kas, tanggal_transaksi, keterangan_kas, saldo_kas, created_by, updated_by,date_created, last_updated) VALUES ('" & kodemasuk & "','" & cmbkas.Text & "','" & cmbsales.Text & "','MASUK', '" & Format(dttransaksi.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtketerangan.Text & "','" & saldomasuk & "','" & fmenu.statususer.Text & "','" & fmenu.statususer.Text & "',now(),now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        MsgBox("Data tersimpan", MsgBoxStyle.Information, "Berhasil")
        btntambah.Text = "Tambah"
        Me.Refresh()
        Call awal()

    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click

    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click

    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Sub cari()
        txtkodemasuk.Text = GridView1.GetFocusedRowCellValue("kode_kas_masuk")

        Using cnn As New OdbcConnection(strConn)
            sql = "SELECT * FROM tb_kas_masuk WHERE kode_kas_masuk  = '" + txtkodemasuk.Text + "'"
            cmmd = New OdbcCommand(sql, cnn)
            cnn.Open()
            dr = cmmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                cmbsales.Text = dr("kode_user")
                cmbkas.Text = dr("kode_kas")
                dttransaksi.Value = dr("tanggal_transaksi")
                saldomasuk = dr("saldo_kas")
                txtsaldomasuk.Text = Format(saldomasuk, "##,##0")
                txtketerangan.Text = dr("keterangan_kas")
                cnn.Close()

                btnedit.Enabled = True
                btnbatal.Enabled = True
                btnhapus.Enabled = True
                btntambah.Enabled = False
                btntambah.Text = "Tambah"
            End If
        End Using
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Call cari()
    End Sub

    Private Sub cmbkas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkas.SelectedIndexChanged
        Call carikas()
    End Sub

    Private Sub cmbkas_TextChanged(sender As Object, e As EventArgs) Handles cmbkas.TextChanged
        Call carikas()
    End Sub

    Private Sub txtsaldomasuk_TextChanged(sender As Object, e As EventArgs) Handles txtsaldomasuk.TextChanged
        If txtsaldomasuk.Text = "" Then
            txtsaldomasuk.Text = 0
        Else
            saldomasuk = txtsaldomasuk.Text
            txtsaldomasuk.Text = Format(saldomasuk, "##,##0")
            txtsaldomasuk.SelectionStart = Len(txtsaldomasuk.Text)
        End If
    End Sub

    Private Sub txtsaldomasuk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsaldomasuk.KeyPress
        e.Handled = ValidAngka(e)
    End Sub

End Class