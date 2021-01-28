Imports System.Data.Odbc

Public Class fchartkasmasuk
    Public namaform As String = "chart-kas_masuk"
    Dim iduser, idkas As Integer
    Public kodeakses As Integer

    Dim exportstatus, printstatus As Boolean
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
    Private Sub fchartkasmasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now

        Call comboboxkas()
        Call comboboxuser()

        cmbsales.Text = ""
        cmbkas.Text = ""

        Select Case kodeakses
            Case 1
                printstatus = True
                exportstatus = False
            Case 3
                printstatus = False
                exportstatus = True
            Case 4
                printstatus = True
                exportstatus = True
        End Select

        Call historysave("Membuka Chart Kas Masuk", "N/A", namaform)
    End Sub

    Sub comboboxuser()
        Call koneksi()
        sql = "SELECT * FROM tb_user"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbsales.DataSource = ds.Tables(0)
        cmbsales.ValueMember = "id"
        cmbsales.DisplayMember = "kode_user"
    End Sub

    Sub comboboxkas()
        Call koneksi()
        sql = "SELECT * FROM tb_kas"
        da = New OdbcDataAdapter(sql, cnn)
        ds = New DataSet
        da.Fill(ds)
        da.Dispose()

        cmbkas.DataSource = ds.Tables(0)
        cmbkas.ValueMember = "id"
        cmbkas.DisplayMember = "kode_kas"
    End Sub

    Sub carikas()
        Call koneksi()
        sql = "SELECT * FROM tb_kas WHERE kode_kas='" & cmbkas.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            idkas = Val(dr("id"))
            txtnamakas.Text = dr("nama_kas")
        Else
            idkas = 0
            txtnamakas.Text = ""
        End If
    End Sub

    Sub cariuser()
        Call koneksi()
        sql = "SELECT * FROM tb_user WHERE kode_user='" & cmbsales.Text & "'"
        cmmd = New OdbcCommand(sql, cnn)
        dr = cmmd.ExecuteReader
        If dr.HasRows Then
            iduser = Val(dr("id"))
            txtnamasales.Text = dr("nama_user")
        Else
            iduser = 0
            txtnamasales.Text = ""
        End If
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        If rbharian.Checked Or rbbulanan.Checked Or rbtahunan.Checked Then
            'Call LoadChart()
        Else
            MsgBox("Pilih grup harian, bulanan atau tahunan", vbInformation)
        End If
    End Sub

    Sub ExportToExcel()
        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            ChartControl1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " & yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        If exportstatus.Equals(True) Then
            ExportToExcel()
            Call historysave("Mengexport Chart Kas Masuk", "N/A", namaform)
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If printstatus.Equals(True) Then
            ChartControl1.ShowPrintPreview()
        Else
            MsgBox("Tidak ada akses")
        End If
    End Sub

    Private Sub btncarikas_Click(sender As Object, e As EventArgs) Handles btncarikas.Click

    End Sub

    Private Sub cmbsales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsales.SelectedIndexChanged

    End Sub

    Private Sub cmbsales_TextChanged(sender As Object, e As EventArgs) Handles cmbsales.TextChanged

    End Sub

    Private Sub cmbkas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkas.SelectedIndexChanged

    End Sub

    Private Sub cmbkas_TextChanged(sender As Object, e As EventArgs) Handles cmbkas.TextChanged

    End Sub
End Class