Public Class flaporanmodalbarang
    Public kodeakses As Integer
    Dim exportstatus, printstatus As Boolean
    Private Sub flaporanmodalbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

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
    End Sub
End Class