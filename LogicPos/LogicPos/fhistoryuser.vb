Imports System.Data.Odbc

Public Class fhistoryuser
    Dim oleh, kode As String

    Private Sub fhistoryuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        dtawal.MaxDate = Now
        dtakhir.MaxDate = Now

        Call historysave("Membuka Form History", "N/A")
    End Sub

    Private Sub fhistoryuser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub grid_history()
        GridColumn1.Caption = "Kode History"
        GridColumn1.FieldName = "kode_history"
        GridColumn1.Width = 10

        GridColumn2.Caption = "Keterangan"
        GridColumn2.FieldName = "keterangan_history"
        GridColumn2.Width = 50

        GridColumn3.Caption = "Kode Tabel"
        GridColumn3.FieldName = "kode_tabel"
        GridColumn3.Width = 10

        GridColumn4.Caption = "Created By"
        GridColumn4.FieldName = "created_by"
        GridColumn4.Width = 10

        GridColumn5.Caption = "Date Created"
        GridColumn5.FieldName = "date_created"
        GridColumn5.Width = 10
        GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn5.DisplayFormat.FormatString = "dd/MM/yyy HH:mm:ss"

    End Sub

    Sub tabel_history()
        Call koneksii()
        kode = txtkodetabel.Text
        oleh = txtoleh.Text

        Using cnn As New OdbcConnection(strConn)
            If dtawal.Value.Equals(dtakhir.Value) Then
                sql = "SELECT * FROM tb_history_user WHERE DATE(date_created) = '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND kode_tabel LIKE '%" + kode + "%' AND created_by LIKE '%" + oleh + "%'"
            Else
                sql = "SELECT * FROM tb_history_user WHERE kode_tabel LIKE '%" + kode + "%' AND created_by LIKE '%" + oleh + "%' AND date_created BETWEEN '" & Format(dtawal.Value, "yyyy-MM-dd") & "' AND '" & Format(dtakhir.Value, "yyyy-MM-dd") & "' + INTERVAL 1 DAY"
            End If
            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid_history()
        End Using
    End Sub

    Sub ExportToExcel()

        Dim filename As String = InputBox("Nama File", "Input Nama file ")
        Dim pathdata As String = "C:\ExportLogicPos"
        Dim yourpath As String = "C:\ExportLogicPos\" + filename + ".xls"

        If filename <> "" Then
            If (Not System.IO.Directory.Exists(pathdata)) Then
                System.IO.Directory.CreateDirectory(pathdata)
            End If

            GridView1.ExportToXls(yourpath)
            MsgBox("Data tersimpan di " + yourpath, MsgBoxStyle.Information, "Success")
            ' Do something
        ElseIf DialogResult.Cancel Then
            MsgBox("You've canceled")
        End If
    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        Call ExportToExcel()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_history()

    End Sub
End Class