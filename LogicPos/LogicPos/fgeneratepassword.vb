Imports System.Data.Odbc

Public Class fgeneratepassword
    Dim banyak As Integer
    Dim kode As String

    Private Sub fgeneratepassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call awal()
    End Sub

    Sub awal()
        txtbanyak.Enabled = True
        txtkodepassword.Enabled = False

        txtbanyak.Text = 1
        txtkodepassword.Clear()

        btntambah.Enabled = True
        btnhapus.Enabled = False
        btnbatal.Enabled = True

        GridControl1.Enabled = True
        Call isitabel()
    End Sub

    Sub hapus()
        txtbanyak.Enabled = False
        txtkodepassword.Enabled = False

        btntambah.Enabled = False
        btnhapus.Enabled = True
        btnbatal.Enabled = True
    End Sub

    Sub index()
        btntambah.TabIndex = 0
        btnhapus.TabIndex = 1
        btnbatal.TabIndex = 2
        txtbanyak.TabIndex = 3
    End Sub

    Sub isitabel()
        Call koneksii()
        sql = "SELECT * FROM tb_password"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        GridControl1.DataSource = Nothing
        GridControl1.DataSource = ds.Tables(0)
        Call kolom()
    End Sub

    Sub kolom()
        GridColumn1.Caption = "Kode Password"
        GridColumn1.FieldName = "kode_password"
        GridColumn2.Caption = "Kode User"
        GridColumn2.FieldName = "kode_user"
        GridColumn3.Caption = "status"
        GridColumn3.FieldName = "status"
    End Sub

    Function trueString() As String
        Dim tResult As String = ""

        tResult = GenerateRandomString(6)

        Call koneksii()
        sql = "SELECT * FROM tb_password WHERE kode_password ='" & kode & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            'tResult = trueString()
            tResult = GenerateRandomString(6)
        End If

        Return tResult
    End Function

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        'txtkodepassword.Text = GenerateRandomString(6)
        Call koneksii()
        For i As Integer = 0 To banyak - 1
            kode = trueString()

            sql = "INSERT INTO tb_password (kode_password, created_by, date_created) VALUES ('" & kode & "','" & fmenu.kodeuser.Text & "',now())"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Next

        Call isitabel()
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        Call koneksii()
        sql = "DELETE FROM tb_password WHERE kode_password ='" & txtkodepassword.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader

        Call awal()
    End Sub

    Private Sub btnbatal_Click(sender As Object, e As EventArgs) Handles btnbatal.Click
        Call awal()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        txtkodepassword.Text = GridView1.GetFocusedRowCellValue("kode_password")

        Call hapus()
    End Sub

    Private Sub txtbanyak_TextChanged(sender As Object, e As EventArgs) Handles txtbanyak.TextChanged
        If txtbanyak.Text = "" Or txtbanyak.Text = "0" Then
            txtbanyak.Text = 1
        Else
            banyak = txtbanyak.Text
            txtbanyak.Text = Format(banyak, "##,##0")
            txtbanyak.SelectionStart = Len(txtbanyak.Text)
        End If
    End Sub

    Private Sub txtbanyak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbanyak.KeyPress
        e.Handled = ValidAngka(e)
    End Sub
End Class