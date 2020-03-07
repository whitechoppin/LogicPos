Public Class flogichouse
    Private Sub flogichouse_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub flogichouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu

    End Sub
End Class