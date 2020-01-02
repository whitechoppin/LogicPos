Imports System.Data.Odbc

Public Class ftransferkas
    Dim kodetransfer, kodedarikas, kodekekas, viewketerangan, viewkodekekas, viewkodedarikas, viewkodesales As String
    Dim saldotransfer As Double
    Dim viewtglkas As DateTime
    Dim statusprint, statusposted As Boolean

    Private Sub ftransferkas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Sub comboboxdarikas()
        Call koneksii()
        cmbdarikas.Items.Clear()
        cmbdarikas.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbdarikas.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbdarikas.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Sub comboboxkekas()
        Call koneksii()
        cmbkekas.Items.Clear()
        cmbkekas.AutoCompleteCustomSource.Clear()
        cmmd = New OdbcCommand("SELECT * FROM tb_kas", cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows = True Then
            While dr.Read()
                cmbkekas.AutoCompleteCustomSource.Add(dr("kode_kas"))
                cmbkekas.Items.Add(dr("kode_kas"))
            End While
        End If
    End Sub

    Sub caridarikas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbdarikas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamadarikas.Text = dr("nama_kas")
        Else
            txtnamadarikas.Text = ""
        End If
    End Sub

    Sub carikekas()
        Call koneksii()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbkekas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            txtnamakekas.Text = dr("nama_kas")
        Else
            txtnamakekas.Text = ""
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
        txtkodetransfer.Clear()
        cmbsales.SelectedIndex = -1
        cmbdarikas.SelectedIndex = -1
        txtnamadarikas.Clear()
        cmbkekas.SelectedIndex = -1
        txtnamakekas.Clear()

        txtsaldotransfer.Clear()
        txtketerangantransfer.Clear()

        Call koneksii()

        cmbsales.Enabled = False
        cmbdarikas.Enabled = False
        cmbkekas.Enabled = False
        txtsaldotransfer.Enabled = False
        dttransaksi.Value = Date.Now
        dttransaksi.Enabled = False
        txtketerangantransfer.Enabled = False

        cbposted.Checked = False
        cbprinted.Checked = False

        GridControl1.Enabled = True
        GridView1.OptionsBehavior.Editable = False
        Call isitabel()

        Call comboboxuser()
        Call comboboxdarikas()
        Call comboboxkekas()
    End Sub

    Sub kolom()
        GridColumn1.Caption = "Kode Transfer Kas"
        GridColumn1.FieldName = "kode_transfer_kas"
        GridColumn1.Width = "40"

        GridColumn2.Caption = "Kode Dari Kas"
        GridColumn2.FieldName = "kode_dari_kas"
        GridColumn2.Width = "40"

        GridColumn3.Caption = "Kode Ke Kas"
        GridColumn3.FieldName = "kode_ke_kas"
        GridColumn3.Width = "40"

        GridColumn4.Caption = "Kode User"
        GridColumn4.FieldName = "kode_user"
        GridColumn4.Width = "40"

        GridColumn5.Caption = "Tanggal Transaksi"
        GridColumn5.FieldName = "tanggal_transaksi"
        GridColumn5.Width = "40"

        GridColumn6.Caption = "Saldo Kas"
        GridColumn6.FieldName = "saldo_kas"
        GridColumn6.Width = "60"
        GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn6.DisplayFormat.FormatString = "Rp ##,#0"
    End Sub
    Sub isitabel()
        'Call koneksii()
        sql = "SELECT * FROM tb_transfer_kas"
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
        cmbdarikas.TabIndex = 7
        cmbkekas.TabIndex = 7
        dttransaksi.TabIndex = 8
        txtsaldotransfer.TabIndex = 9
        txtketerangantransfer.TabIndex = 10
    End Sub

    Function autonumber()
        Call koneksii()
        sql = "SELECT RIGHT(kode_transfer_kas,3) FROM tb_transfer_kas WHERE DATE_FORMAT(MID(`kode_kas_masuk`, 3 , 6), ' %y ')+ MONTH(MID(`kode_kas_masuk`,3 , 6)) + DAY(MID(`kode_kas_masuk`,3, 6)) = DATE_FORMAT(NOW(),' %y ') + month(Curdate()) + day(Curdate()) ORDER BY RIGHT(kode_kas_masuk,3) DESC"
        Dim pesan As String = ""
        Try
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item(0).ToString() + 1).ToString.Length = 1 Then
                    Return "TK" + Format(Now.Date, "yyMMdd") + "00" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                Else
                    If (dr.Item(0).ToString() + 1).ToString.Length = 2 Then
                        Return "TK" + Format(Now.Date, "yyMMdd") + "0" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                    Else
                        If (dr.Item(0).ToString() + 1).ToString.Length = 3 Then
                            Return "TK" + Format(Now.Date, "yyMMdd") + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                        End If
                    End If
                End If
            Else
                Return "TK" + Format(Now.Date, "yyMMdd") + "001"
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
        dttransaksi.Enabled = True
        txtsaldomasuk.Enabled = True
        txtketerangan.Enabled = True
        cmbsales.Focus()
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