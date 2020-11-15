Imports System.Drawing.Printing
Imports System.Data.Odbc

Public Class fprinter
    Dim cpuid As String = flogin.CPUIDPOS
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
        sql = "SELECT * FROM tb_printer WHERE nomor='1' AND computer_id='" & cpuid & "' LIMIT 1"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            struk = dr("nama_printer")
            cmbstruk.Text = struk
        Else
            cmbstruk.SelectedIndex = -1
        End If

        sql = "SELECT * FROM tb_printer WHERE nomor='2' AND computer_id='" & cpuid & "' LIMIT 1"
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

    Private Sub fprinter_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub simpan()
        Call koneksii()

        sql = "SELECT * FROM tb_printer WHERE nomor=1 AND computer_id='" & cpuid & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "UPDATE tb_printer SET nama_printer='" & cmbstruk.Text & "' WHERE nomor=1 AND computer_id='" & cpuid & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "INSERT INTO tb_printer (nomor, nama_printer, computer_id) VALUES ('1','" & cmbstruk.Text & "','" & cpuid & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        sql = "SELECT * FROM tb_printer WHERE nomor=2 AND computer_id='" & cpuid & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader()
        If dr.HasRows Then
            sql = "UPDATE tb_printer SET nama_printer='" & cmbfaktur.Text & "' WHERE nomor=2 AND computer_id='" & cpuid & "'"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        Else
            sql = "INSERT INTO tb_printer (nomor, nama_printer, computer_id) VALUES ('2','" & cmbfaktur.Text & "','" & cpuid & "')"
            cmmd = New OdbcCommand(sql, cnn)
            dr = cmmd.ExecuteReader()
        End If

        MsgBox("Pengaturan Printer Di Simpan")
    End Sub
End Class