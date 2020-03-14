Public Class falamat
    Private Sub falamat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
    End Sub

    Private Sub falamat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        fmenu.ActiveMdiChild_FormClosed(sender)
    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click

    End Sub
End Class