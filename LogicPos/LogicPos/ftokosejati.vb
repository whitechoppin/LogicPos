Public Class ftokosejati
    Private Sub ftokosejati_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu


    End Sub

    Private Sub ftokosejati_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub
End Class