Public Class fkaskeluar
    Private Sub fkaskeluar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = fmenu
        Call koneksii()
    End Sub

End Class