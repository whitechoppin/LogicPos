Imports System.Drawing.Printing
Imports System.Data.Odbc

Public Class fprinter
    Dim struk, faktur As String
    Private Sub fprinter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call awal()
    End Sub
    Sub awal()
        For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count
            On Error Resume Next
            cmbfaktur.Items.Add(PrinterSettings.InstalledPrinters(i))
            cmbstruk.Items.Add(PrinterSettings.InstalledPrinters(i))
        Next

        Call koneksii()
        sql = "select * from tb_printer where nomor='1'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            struk = dr("nama_printer")
            cmbstruk.Text = struk
        Else
            cmbstruk.SelectedIndex = -1
        End If

        sql = "select * from tb_printer where nomor='2'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            faktur = dr("nama_printer")
            cmbfaktur.Text = faktur
        Else
            cmbfaktur.SelectedIndex = -1
        End If

    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        Call simpan()
    End Sub

    Sub simpan()
        Call koneksii()

        sql = "select * from tb_printer where nomor=1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "update tb_printer set nama_printer='" & cmbstruk.Text & "' where nomor=1"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "insert into tb_printer (nomor, nama_printer) values ('1','" & cmbstruk.Text & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        sql = "select * from tb_printer where nomor=2"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "update tb_printer set nama_printer='" & cmbfaktur.Text & "' where nomor=2"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "insert into tb_printer (nomor, nama_printer) values ('2','" & cmbfaktur.Text & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Pengaturan Printer Di Simpan")
    End Sub
End Class