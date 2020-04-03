Imports System.Data.Odbc

Public Class fperusahaan
    Dim namaawal, namaakhir As String
    Dim alamatawal, alamatakhir As String
    Dim telpawal, telpakhir As String
    Dim rekawal, rekakhir As String
    Private Sub falamat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
        Call historysave("Membuka Setting Alamat", "N/A")
    End Sub

    Private Sub falamat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnupload_Click(sender As Object, e As EventArgs) Handles btnupload.Click

    End Sub

    Sub awal()
        Call koneksii()
        sql = "SELECT * FROM tb_info_perusahaan WHERE nomor =1 LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            alamatawal = dr("alamat")
            telpawal = dr("telepon")
            rekawal = dr("rekening")

            txtalamat.Text = alamatawal
            txttelepon.Text = telpawal
            txtrekening.Text = rekawal
        Else
            txtalamat.Text = ""
            txttelepon.Text = ""
            txtrekening.Text = ""
        End If
    End Sub

    Sub simpan()
        Call koneksii()
        namaakhir = txtnama.Text
        alamatakhir = txtalamat.Text
        telpakhir = txttelepon.Text
        rekakhir = txtrekening.Text

        sql = "SELECT * FROM tb_info_perusahaan WHERE nomor=1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "UPDATE tb_info_perusahaan SET alamat='" & alamatakhir & "', telepon='" & telpakhir & "', rekening='" & rekakhir & "' WHERE nomor=1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "INSERT INTO tb_info_perusahaan (alamat ,telepon ,rekening ) VALUES ('" & alamatakhir & "','" & telpakhir & "','" & rekakhir & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Pengaturan Alamat Di Simpan")
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Call awal()
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call simpan()
        Call historysave("Mengupdate Setting Alamat", "N/A")
    End Sub
End Class