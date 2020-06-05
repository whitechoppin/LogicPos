Imports System.Data.Odbc

Public Class fpassword
    Dim kodeuser As String
    Public kodetabel, kodemode As String

    Private Sub fpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = False
        txtpassword.PasswordChar = "•"
    End Sub

    Sub proceed()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE password_user= '" + txtpassword.Text + "' AND jabatan_user ='Owner' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            '=== coba pake password generate ===

            Call koneksii()
            sql = "SELECT * FROM tb_password WHERE kode_password = '" + txtpassword.Text + "'AND status = 0 LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()

            '===================================

            If dr.HasRows = 0 Then
                MsgBox("Password salah !", MsgBoxStyle.Exclamation, "Error Login")
                txtpassword.Text = ""
            Else
                If passwordid = 1 Then
                    'modal barang
                    fbarang.txthidden.Visible = False
                ElseIf passwordid = 2 Then
                    'modal barang
                    flaporanstokbarang.LabelHarga.Visible = True
                ElseIf passwordid = 3 Then
                    'modal barang
                    fcaristok.LabelHarga.Visible = True
                ElseIf passwordid = 4 Then
                    'modal barang
                    fcaribarang.LabelHarga.Visible = True
                ElseIf passwordid = 5 Then
                    'modal barang
                    fpricelist.txthidden.Visible = False
                ElseIf passwordid = 6 Then
                    'cetakan
                    fpembelian.statusizincetak = True
                ElseIf passwordid = 7 Then
                    'cetakan
                    fpenjualan.statusizincetak = True
                End If

                Call koneksii()
                sql = "UPDATE tb_password SET status = 1, kode_user='" & fmenu.statususer.Text & "' WHERE  kode_password='" & txtpassword.Text & "'"
                cmmd = New OdbcCommand(sql, cnn)
                dr = cmmd.ExecuteReader()

                'history user ==========
                Call historysave("Otorisasi Passcode Diberikan dengan kode " + txtpassword.Text + " Kepada " + fmenu.statususer.Text, kodetabel)
                '========================

                txtpassword.Text = ""
                Me.Close()
            End If

        Else
            kodeuser = dr("kode_user")
            If passwordid = 1 Then
                'modal barang
                fbarang.txthidden.Visible = False
            ElseIf passwordid = 2 Then
                'modal barang
                flaporanstokbarang.LabelHarga.Visible = True
            ElseIf passwordid = 3 Then
                'modal barang
                fcaristok.LabelHarga.Visible = True
            ElseIf passwordid = 4 Then
                'modal barang
                fcaribarang.LabelHarga.Visible = True
            ElseIf passwordid = 5 Then
                'modal barang
                fpricelist.txthidden.Visible = False
            ElseIf passwordid = 6 Then
                'cetakan
                fpembelian.statusizincetak = True
            ElseIf passwordid = 7 Then
                'cetakan
                fpenjualan.statusizincetak = True
            End If

            'history user ==========
            Call historysave("Otorisasi Passcode Diberikan Oleh " + kodeuser + " Kepada " + fmenu.statususer.Text, kodetabel)
            '========================

            txtpassword.Text = ""
            Me.Close()
        End If
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Call proceed()
    End Sub

    Private Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call proceed()
        End If
    End Sub
End Class