Imports System.Data.Odbc

Public Class fhistoryuser
    Private Sub fhistoryuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

        DateTimePicker1.MaxDate = Now
        DateTimePicker2.MaxDate = Now

        Call koneksii()
        'Call tabel_history()
    End Sub

    Private Sub fhistoryuser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Sub grid_history()
        GridColumn1.Caption = "No"
        GridColumn1.FieldName = "kode_history"

        GridColumn2.Caption = "Keterangan"
        GridColumn2.FieldName = "keterangan_history"

        GridColumn3.Caption = "Created By"
        GridColumn3.FieldName = "created_by"

        GridColumn4.Caption = "Date Created"
        GridColumn4.FieldName = "date_created"
        GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        GridColumn4.DisplayFormat.FormatString = "dd/MM/yyy"

    End Sub

    Sub tabel_history()
        Call koneksii()

        Using cnn As New OdbcConnection(strConn)
            If DateTimePicker1.Value.Equals(DateTimePicker2.Value) Then
                sql = "SELECT * FROM tb_history_user WHERE DATE(date_created) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
            Else
                sql = "SELECT * FROM tb_history_user WHERE date_created BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
            End If
            da = New OdbcDataAdapter(sql, cnn)
            ds = New DataSet
            da.Fill(ds)
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = ds.Tables(0)
            Call grid_history()
        End Using
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Call tabel_history()
    End Sub
End Class