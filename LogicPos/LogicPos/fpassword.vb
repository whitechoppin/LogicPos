Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class fpassword
    Public namaform As String = "menu-password"

    Dim kodeuser As String
    Dim kodepassword As String
    Public kodetabel, kodemode, kodejabatan As String
    Dim authuser As String
    Dim statuscode As Boolean = False

    Dim kodeuserrequest, namauserrequest, emailuserrequest, jabatanuserrequest As String

    Private Sub fpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = False
        txtpassword.PasswordChar = "•"
    End Sub

    Sub proceed()
        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE password_user= '" + txtpassword.Text + "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        If dr.HasRows = 0 Then
            '=== coba pake password generate ===

            Call koneksii()
            sql = "SELECT * FROM tb_password WHERE kode_password = '" & txtpassword.Text & "'AND status = 0 LIMIT 1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
            dr.Read()

            '===================================

            If dr.HasRows = 0 Then
                MsgBox("Kode salah !", MsgBoxStyle.Exclamation, "Error Login")
                txtpassword.Text = ""
            Else
                If passwordid = 1 Then
                    'modal barang
                    'fbarang.txthidden.Visible = False

                    MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                    txtpassword.Text = ""
                    statuscode = False
                ElseIf passwordid = 2 Then
                    'modal barang
                    'flaporanstokbarang.LabelHarga.Visible = True

                    MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                    txtpassword.Text = ""
                    statuscode = False
                ElseIf passwordid = 3 Then
                    'modal barang
                    'fcaristok.LabelHarga.Visible = True

                    MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                    txtpassword.Text = ""
                    statuscode = False
                ElseIf passwordid = 4 Then
                    'modal barang
                    'fcaribarang.LabelHarga.Visible = True

                    MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                    txtpassword.Text = ""
                    statuscode = False
                ElseIf passwordid = 5 Then
                    'modal barang
                    'fpricelist.txthidden.Visible = False

                    MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                    txtpassword.Text = ""
                    statuscode = False

                ElseIf passwordid = 6 Then
                    'cetakan
                    fpembelian.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 7 Then
                    'cetakan
                    fpenjualan.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 8 Then
                    'cetakan
                    freturbeli.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 9 Then
                    'cetakan
                    freturjual.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 10 Then
                    'cetakan
                    fbarangmasuk.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 11 Then
                    'cetakan
                    fbarangkeluar.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 12 Then
                    'cetakan
                    ftransferbarang.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 13 Then
                    'cetakan
                    flunasutang.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 14 Then
                    'cetakan
                    flunaspiutang.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 15 Then
                    'cetakan
                    fkasmasuk.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 16 Then
                    'cetakan
                    fkaskeluar.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 17 Then
                    'cetakan
                    ftransferkas.statusizincetak = True
                    statuscode = True
                ElseIf passwordid = 18 Then
                    'cetakan
                    fpenyesuaianstok.statusizincetak = True
                    statuscode = True
                End If

                If statuscode = True Then
                    Call koneksii()
                    sql = "UPDATE tb_password SET status = 1, kode_user='" & fmenu.kodeuser.Text & "' WHERE  kode_password='" & txtpassword.Text & "'"
                    cmmd = New OdbcCommand(sql, cnn)
                    dr = cmmd.ExecuteReader()

                    'history user ==========
                    Call historysave("Otorisasi Passcode Diberikan dengan kode " + txtpassword.Text + " Kepada " + fmenu.kodeuser.Text, kodetabel, namaform)
                    '========================

                    txtpassword.Text = ""
                End If

                Me.Close()
            End If

        Else
            kodeuser = dr("kode_user")
            kodejabatan = dr("jabatan_user")
            authuser = dr("auth_user")

            If kodejabatan.Equals("Owner") Then
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
                ElseIf passwordid = 8 Then
                    'cetakan
                    freturbeli.statusizincetak = True
                ElseIf passwordid = 9 Then
                    'cetakan
                    freturjual.statusizincetak = True
                ElseIf passwordid = 10 Then
                    'cetakan
                    fbarangmasuk.statusizincetak = True
                ElseIf passwordid = 11 Then
                    'cetakan
                    fbarangkeluar.statusizincetak = True
                ElseIf passwordid = 12 Then
                    'cetakan
                    ftransferbarang.statusizincetak = True
                ElseIf passwordid = 13 Then
                    'cetakan
                    flunasutang.statusizincetak = True
                ElseIf passwordid = 14 Then
                    'cetakan
                    flunaspiutang.statusizincetak = True
                ElseIf passwordid = 15 Then
                    'cetakan
                    fkasmasuk.statusizincetak = True
                ElseIf passwordid = 16 Then
                    'cetakan
                    fkaskeluar.statusizincetak = True
                ElseIf passwordid = 17 Then
                    'cetakan
                    ftransferkas.statusizincetak = True
                ElseIf passwordid = 18 Then
                    'cetakan
                    fpenyesuaianstok.statusizincetak = True
                End If

                'history user ==========
                Call historysave("Otorisasi Passcode Diberikan Oleh " + kodeuser + " Kepada " + fmenu.kodeuser.Text, kodetabel, namaform)
                '========================

                txtpassword.Text = ""
                Me.Close()
            Else
                If authuser = 1 Then
                    If passwordid = 1 Then
                        'modal barang
                        'fbarang.txthidden.Visible = False

                        MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                        txtpassword.Text = ""
                        statuscode = False
                    ElseIf passwordid = 2 Then
                        'modal barang
                        'flaporanstokbarang.LabelHarga.Visible = True

                        MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                        txtpassword.Text = ""
                        statuscode = False
                    ElseIf passwordid = 3 Then
                        'modal barang
                        'fcaristok.LabelHarga.Visible = True

                        MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                        txtpassword.Text = ""
                        statuscode = False
                    ElseIf passwordid = 4 Then
                        'modal barang
                        'fcaribarang.LabelHarga.Visible = True

                        MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                        txtpassword.Text = ""
                        statuscode = False
                    ElseIf passwordid = 5 Then
                        'modal barang
                        'fpricelist.txthidden.Visible = False

                        MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                        txtpassword.Text = ""
                        statuscode = False

                    ElseIf passwordid = 6 Then
                        'cetakan
                        fpembelian.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 7 Then
                        'cetakan
                        fpenjualan.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 8 Then
                        'cetakan
                        freturbeli.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 9 Then
                        'cetakan
                        freturjual.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 10 Then
                        'cetakan
                        fbarangmasuk.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 11 Then
                        'cetakan
                        fbarangkeluar.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 12 Then
                        'cetakan
                        ftransferbarang.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 13 Then
                        'cetakan
                        flunasutang.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 14 Then
                        'cetakan
                        flunaspiutang.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 15 Then
                        'cetakan
                        fkasmasuk.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 16 Then
                        'cetakan
                        fkaskeluar.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 17 Then
                        'cetakan
                        ftransferkas.statusizincetak = True
                        statuscode = True
                    ElseIf passwordid = 18 Then
                        'cetakan
                        fpenyesuaianstok.statusizincetak = True
                        statuscode = True
                    End If

                    If statuscode = True Then
                        'history user ==========
                        Call historysave("Otorisasi Passcode Diberikan Oleh " + kodeuser + " Kepada " + fmenu.kodeuser.Text, kodetabel, namaform)
                        '========================

                        txtpassword.Text = ""
                    End If

                    Me.Close()
                Else
                    MsgBox("Tidak Ada Akses !", MsgBoxStyle.Exclamation, "Error Login")
                    txtpassword.Text = ""
                End If
            End If
        End If
    End Sub

    Function trueString() As String
        Dim tResult As String = ""

        tResult = GenerateRandomString(6)

        Call koneksii()
        sql = "SELECT * FROM tb_password WHERE kode_password ='" & kodepassword & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            'tResult = trueString()
            tResult = GenerateRandomString(6)
        End If

        Return tResult
    End Function

    Private Sub labelrequest_DoubleClick(sender As Object, e As EventArgs) Handles labelrequest.DoubleClick
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()

        Call koneksii()
        kodepassword = trueString()

        sql = "INSERT INTO tb_password (kode_password, created_by, date_created) VALUES ('" & kodepassword & "','" & fmenu.kodeuser.Text & "',now())"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()

        Call koneksii()
        sql = "SELECT * FROM tb_user WHERE jabatan_user = 'Owner' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        dr.Read()

        kodeuser = dr("kode_user")
        namauserrequest = dr("nama_user")
        emailuserrequest = dr("email_user")
        jabatanuserrequest = dr("jabatan_user")

        Try
            SmtpServer.UseDefaultCredentials = False
            SmtpServer.Credentials = New Net.NetworkCredential("requestlogicpos@rumahlogika.id", "RumahLogika07092019")

            SmtpServer.Port = 587
            SmtpServer.Host = "mail.rumahlogika.id"
            SmtpServer.EnableSsl = True
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            mail = New MailMessage()
            mail.From = New MailAddress("requestlogicpos@rumahlogika.id")
            mail.To.Add(emailuserrequest)
            mail.Subject = "PIN/Password Logic POS"
            mail.Body = "Kode PIN/Password Anda : " & kodepassword
            SmtpServer.Send(mail)
            MsgBox("PIN/Password telah dikirim !")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub labelrequest_Click(sender As Object, e As EventArgs) Handles labelrequest.Click

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